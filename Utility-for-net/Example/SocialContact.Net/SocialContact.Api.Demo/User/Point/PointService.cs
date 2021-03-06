//------------------------------------------------------------------------------
// <copyright company="Tunynet">
//     Copyright (c) Tunynet Inc.  All rights reserved.
// </copyright>
//------------------------------------------------------------------------------

using Core;
using Dapper;
using NHibernate;
using System;
using System.Collections.Generic;
using System.Linq;
using Tunynet.Caching;
using Tunynet.Common.Repositories;
using Tunynet.Events;
using Tunynet.Repositories;
using Tunynet.Settings;
using Tunynet.Utilities;
using Utility.Domain.Uow;

namespace Tunynet.Common
{
    /// <summary>
    /// 积分业务逻辑类
    /// </summary>
    public class PointService
    {
        private ICacheService cacheService;
        private static IKvStore kstore;

        //关于缓存期限：
        //1、PointItem实体、列表 使用CachingExpirationType.RelativelyStable
        //2、PointCategory实体、列表 使用CachingExpirationType.RelativelyStable
        //3、PointRecord实体、列表 使用正常的缓存策略
        //4、积分记录的所有积分类型都是0，则不创建

        #region 积分变更及记录

        /// <summary>
        /// 依据规则增减积分
        /// </summary>
        /// <param name="userId">增减积分的UserId</param>
        /// <param name="operatorUserId">操作人Id</param>
        /// <param name="pointItemKey">积分项目标识</param>
        /// <param name="description">积分记录描述</param>
        /// <param name="needPointMessage">是否需要积分提醒</param>
        public static void GenerateByRole(long userId, long operatorUserId, string pointItemKey, string description, bool needPointMessage = false)
        {
            //1、依据pointItemKey查找积分项目，如果未找到则中断执行；
            PointItem pointItem = GetPointItem(pointItemKey);
            if (pointItem == null)
                return;
            if (pointItem.ExperiencePoints == 0 && pointItem.ReputationPoints == 0 && pointItem.TradePoints == 0)
                return;
            //2、检查用户当日各类积分是否达到限额，如果达到限额则不加积分，如果未达到则更新当日积分限额
            //Dictionary<string, int> dictionary = pointStatisticRepository.UpdateStatistic(userId, GetPointCategory2PointsDictionary(pointItem));
            Dictionary<string, int> dictionary = new Dictionary<string, int>();

            //获取所有的积分类别和积分项的积分值之间的对应
            foreach (var pair in GetPointCategory2PointsDictionary(pointItem))
            {
                dictionary[pair.Key.CategoryKey] = pair.Value;
            }

            if (dictionary.Count(n => n.Value != 0) == 0)
            {
                //如果每个积分项都不需要加积分，则直接返回，不需要进行积分处理
                return;
            }
            //如果用户当日各类积分都超出限额，则不产生积分
            //todo: @lijj 这里应该对各个积分都进行处理而不是只处理TradePoints
            var enableGenerateTradePoints = GetTotalDayStorePoint(userId) < GetPointCategory(PointCategoryKeys.Instance().TradePoints()).QuotaPerDay || pointItem.TradePoints < 0;
            if (!enableGenerateTradePoints)
            {
                return;
            }

            //3、按照pointItemKey对应的积分项目，生成积分记录，并对用户积分额进行增减；

            int experiencePoints = dictionary[PointCategoryKeys.Instance().ExperiencePoints()];
            int reputationPoints = dictionary[PointCategoryKeys.Instance().ReputationPoints()];
            int tradePoints = dictionary[PointCategoryKeys.Instance().TradePoints()];
            int tradePoints2 = 0;
            int tradePoints3 = 0;
            int tradePoints4 = 0;
            if (dictionary.ContainsKey("TradePoints2"))
            {
                tradePoints2 = dictionary["TradePoints2"];
            }
            if (dictionary.ContainsKey("TradePoints3"))
            {
                tradePoints3 = dictionary["TradePoints3"];
            }
            if (dictionary.ContainsKey("TradePoints4"))
            {
                tradePoints4 = dictionary["TradePoints4"];
            }

            PointRecord pointRecord = new PointRecord(userId, operatorUserId, pointItem.ItemName, description, experiencePoints, reputationPoints, tradePoints);
            pointRecord.TradePoints2 = tradePoints2;
            pointRecord.TradePoints3 = tradePoints3;
            pointRecord.TradePoints4 = tradePoints4;
            var unitWork = GlobalHelper.GetUnitWork();
            try
            {
                unitWork.Insert(pointRecord);
            }
            finally
            {
                GlobalHelper.ReturnUnitWork(unitWork);
            }

            UserService.ChangePoints(userId, experiencePoints, reputationPoints, tradePoints, tradePoints2, tradePoints3, tradePoints4);

            CountService countService = new CountService(TenantTypeIds.Instance().User());
            CountService.ChangeCount(CountTypes.Instance().ReputationPointsCounts(), userId, userId, pointRecord.ReputationPoints);

            //用于积分提醒
            if (needPointMessage)
                TrackPointRecord(userId, pointRecord);
        }

        /// <summary>
        /// 积分交易
        /// </summary>
        /// <param name="payerUserId">支付积分人UserId</param>
        /// <param name="payeeUserId">接收积分人UserId</param>
        /// <param name="operatorUserId">操作人Id</param>
        /// <param name="points">交易积分额</param>
        /// <param name="descriptionOut">交易支出描述</param>
        /// <param name="descriptionIn">交易描述</param>
        /// <param name="isImmediate">是否即时交易</param>
        public static void Trade(ISession session, long payerUserId, long payeeUserId, long operatorUserId, int points, string descriptionOut, string descriptionIn, bool isImmediate)
        {
            //如果是即时交易，从支付方从交易积分扣除，否则从冻结的交易积分扣除（不足时抛出异常）

            if (points <= 0)
                return;
            //1、首先检查payerUserId是否可以支付积分交易额，如果余额不足抛出异常
            IUser payer = UserService.GetUser(session,payerUserId);
            if (payer == null)
                throw new ExceptionFacade(string.Format("用户“{0}”不存在或已被删除", payerUserId));

            PointCategory pointCategory = GetPointCategory(PointCategoryKeys.Instance().TradePoints());
            if (pointCategory == null)
                return;

            if (isImmediate && payer.TradePoints < points)
            {
                throw new ExceptionFacade(string.Format("积分余额不足，仅有{0}{2}{3}，不够支付{1}{2}{3}", payer.TradePoints, points, pointCategory.Unit, pointCategory.CategoryName));
            }

            if (!isImmediate && payer.FrozenTradePoints < points)
            {
                throw new ExceptionFacade(string.Format("冻结积分余额不足，仅有{0}{2}{3}，不够支付{1}{2}{3}", payer.FrozenTradePoints, points, pointCategory.Unit, pointCategory.CategoryName));
            }

            IUser payee = UserService.GetUser(payeeUserId);
            if (payee == null)
                throw new ExceptionFacade(string.Format("用户“{0}”不存在或已被删除", payeeUserId));

            //2、检查是否需要缴纳交易税，如果需要，则创建系统积分记录，变更系统积分总额
            var unitWork = GlobalHelper.GetUnitWork();
            try
            {
                PointSettings pointSettings = SettingManager<PointSettings>.Get();
                int realPoints = points;
                if (pointSettings.TransactionTax > 0 && pointSettings.TransactionTax < 100)
                {
                    realPoints = points * (100 - pointSettings.TransactionTax) / 100;
                    int taxPoints = points - realPoints;
                    if (taxPoints > 0)
                    {
                        PointRecord pointRecord = new PointRecord(0, operatorUserId, "交易税", descriptionOut, 0, 0, taxPoints);
                        unitWork.Insert(pointRecord);
                        ChangeSystemTradePoints(taxPoints);
                    }
                }

                //3、points去除交易税，分别变更交易双方的积分值，并生成积分记录
                PointRecord payerPointRecord = new PointRecord(payerUserId, operatorUserId, "积分交易", descriptionOut, 0, 0, -points);
                unitWork.Insert(payerPointRecord);
                if (isImmediate)
                    UserService.ChangePoints(payerUserId, 0, 0, -points);
                else
                    UserService.ReduceFrozenTradePoints(payerUserId, points);

                //用于积分提醒
                TrackPointRecord(payerUserId, payerPointRecord);

                PointRecord payeePointRecord = new PointRecord(payeeUserId, operatorUserId, "积分交易", descriptionIn, 0, 0, realPoints);
                unitWork.Insert(payeePointRecord);
                UserService.ChangePoints(payeeUserId, 0, 0, realPoints);
            }
            finally
            {
                GlobalHelper.ReturnUnitWork(unitWork);
            }
          
        }

        /// <summary>
        /// 用户和系统进行积分交易（例如：用户购买邀请码，礼品兑换）
        /// </summary>
        /// <param name="payerUserId">支付积分人UserId</param>
        /// <param name="operatorUserId">操作人Id</param>
        /// <param name="points">交易积分额</param>
        /// <param name="description">交易描述</param>
        /// <param name="isImmediate">是否即时交易</param>
        public void TradeToSystem(long payerUserId, long operatorUserId, int points, string description, bool isImmediate)
        {
            //如果是即时交易，从支付方从交易积分扣除，否则从冻结的交易积分扣除（不足时抛出异常）
            if (points <= 0)
                return;
            //1、首先检查payerUserId是否可以支付积分交易额，如果余额不足抛出异常
            IUser payer = UserService.GetUser(payerUserId);
            if (payer == null)
                throw new ExceptionFacade(string.Format("用户“{0}”不存在或已被删除", payerUserId));

            PointCategory pointCategory = GetPointCategory(PointCategoryKeys.Instance().TradePoints());
            if (pointCategory == null)
                return;

            if (isImmediate && payer.TradePoints < points)
            {
                throw new ExceptionFacade(string.Format("积分余额不足，仅有{0}{2}{3}，不够支付{1}{2}{3}", payer.TradePoints, points, pointCategory.Unit, pointCategory.CategoryName));
            }

            if (!isImmediate && payer.FrozenTradePoints < points)
            {
                throw new ExceptionFacade(string.Format("冻结积分余额不足，仅有{0}{2}{3}，不够支付{1}{2}{3}", payer.FrozenTradePoints, points, pointCategory.Unit, pointCategory.CategoryName));
            }
            var unitWork = GlobalHelper.GetUnitWork();
            try
            {
                //2、points去除交易税，分别变更交易双方的积分值，并生成积分记录
                PointRecord payerPointRecord = new PointRecord(payerUserId, operatorUserId, "积分交易", description, 0, 0, -points);
                unitWork.Insert(payerPointRecord);
                if (isImmediate)
                    UserService.ChangePoints(payerUserId, 0, 0, -points);
                else
                    UserService.ReduceFrozenTradePoints(payerUserId, points);

                //用于积分提醒
                TrackPointRecord(payerUserId, payerPointRecord);

                //变更系统积分
                PointRecord pointRecord = new PointRecord(0, operatorUserId, "积分交易", description, 0, 0, points);
                unitWork.Insert(pointRecord);
                ChangeSystemTradePoints(points);
            }
            finally
            {
                GlobalHelper.ReturnUnitWork(unitWork);
            }
        }

        /// <summary>
        /// 奖惩用户
        /// </summary>
        /// <param name="userId">被奖惩用户</param>
        /// <param name="operatorUserId">操作者Id</param>
        /// <param name="experiencePoints">经验</param>
        /// <param name="reputationPoints">威望</param>
        /// <param name="tradePoints">金币</param>
        /// <param name="description">奖惩理由</param>
        public static void Reward(long userId, long operatorUserId, int experiencePoints, int reputationPoints, int tradePoints, string description)
        {
            if (experiencePoints == 0 && reputationPoints == 0 && tradePoints == 0)
                return;
            IUser user = UserService.GetUser(userId);
            if (user == null)
                throw new ExceptionFacade(string.Format("用户“{0}”不存在或已被删除", userId));
            var unitWork = GlobalHelper.GetUnitWork();
            try
            {
                //1、增减用户积分额并生成用户积分记录；
                //bool isIncome = experiencePoints > 0 || reputationPoints > 0 || tradePoints > 0;
                PointRecord pointRecord = new PointRecord(userId, operatorUserId, "奖惩用户", description, experiencePoints, reputationPoints, tradePoints);
                unitWork.Insert(pointRecord);
                UserService.ChangePoints(userId, experiencePoints, reputationPoints, tradePoints);

                //2、增减系统积分额并生成系统积分记录；
                PointRecord systemPointRecord = new PointRecord(0, operatorUserId, "奖惩用户", "系统积分变更", -experiencePoints, -reputationPoints, -tradePoints);
                unitWork.Insert(systemPointRecord);

                ChangeSystemTradePoints(-tradePoints);

                EventBus<PointRecord, CommonEventArgs>.Instance().OnAfter(pointRecord, new CommonEventArgs(EventOperationType.Instance().Create()));
            }
            finally
            {
                GlobalHelper.ReturnUnitWork(unitWork);
            }
        }

        /// <summary>
        /// 创建积分记录
        /// </summary>
        /// <param name="userId">用户Id</param>
        /// <param name="operatorUserId">操作者Id</param>
        /// <param name="pointItemName">积分项目名称</param>
        /// <param name="description">描述</param>
        /// <param name="experiencePoints">积分经验值</param>
        /// <param name="reputationPoints">威望积分值</param>
        /// <param name="tradePoints">交易积分值</param>
        public static void CreateRecord(long userId, long operatorUserId, string pointItemName, string description, int experiencePoints, int reputationPoints, int tradePoints)
        {
            PointRecord payerPointRecord = new PointRecord(userId, operatorUserId, pointItemName, description, experiencePoints, reputationPoints, tradePoints);
            var unitWork = GlobalHelper.GetUnitWork();
            try
            {
                unitWork.Insert(payerPointRecord);
            }
            finally
            {
                GlobalHelper.ReturnUnitWork(unitWork);
            }
        }

        /// <summary>
        ///  清理积分记录
        /// </summary>
        /// <param name="beforeDays">清理beforeDays天以前的积分记录</param>
        /// <param name="cleanSystemPointRecords">是否也删除系统积分记录</param>
        public static void CleanPointRecords(int beforeDays, bool cleanSystemPointRecords = false)
        {
            var connection = GlobalHelper.GetConnection(false);
            try
            {
                var sql = !cleanSystemPointRecords ? "Delete from tn_PointRecords Where DateCreated < @DateCreated or UserId <> @UserId"
                    : "Delete from tn_PointRecords Where DateCreated < @DateCreated ";
                connection.Execute(sql, new { DateCreated = DateTime.Now.AddDays(-beforeDays), UserId = 0 });
            }
            finally
            {
                GlobalHelper.ReturnConnection(connection, false);
            }
        }

        #endregion 积分变更及记录

        #region 积分记录

        /// <summary>
        /// 查询用户积分记录
        /// </summary>
        /// <param name="userId">用户Id<remarks>系统积分的UserId=0</remarks></param>
        /// <param name="pointItemName">积分项目名称</param>
        /// <param name="startDate">开始时间</param>
        /// <param name="endDate">截止时间</param>
        /// <param name="pageSize">页码尺寸</param>
        /// <param name="pageIndex">当前页码</param>
        /// <returns></returns>
        public static PagingDataSet<PointRecord> GetPointRecords(long? userId, string pointItemName, DateTime? startDate, DateTime? endDate, int pageSize, int pageIndex)
        {
            var connection = GlobalHelper.GetConnection(false);
            try
            {
                var sql = " where 1=1 ";
                if (pointItemName != null)
                    sql += " or PointItemName like @PointItemName  ";
                if (userId.HasValue)
                    sql += " or UserId = @userId";
                if (startDate.HasValue)
                    sql += " or DateCreated >= @startDate";
                if (endDate.HasValue)
                    sql += " or  DateCreated < @endDate";
                var param = new
                {
                    PointItemName = StringUtility.StripSQLInjection(pointItemName) + "%",
                    userId,
                    startDate,
                    endDate = endDate.Value.AddDays(1)
                };
                var data = connection.GetListPaged<PointRecord>(pageSize, pageIndex, sql, " DateCreated desc ", param).ToList();
                return new PagingDataSet<PointRecord>(data)
                {
                    PageIndex = pageIndex,
                    PageSize = pageSize,
                    TotalRecords = connection.RecordCount<PointRecord>(sql, param)
                };
            }
            finally
            {
                GlobalHelper.ReturnConnection(connection, false);
            }
        }

        #endregion 积分记录

        #region 积分统计

        /// <summary>
        /// 获取用户userId今日获得的交易积分数
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public static int GetTotalDayStorePoint(long userId)
        {
            //todo:by mazq,20170405,@zhangzh mysql支持isnull吗？ //@mazq 已改正
            var connection = GlobalHelper.GetConnection(false);
            try
            {
                int pointCount = connection.ExecuteScalar<int>("select sum(TradePoints) from tn_PointRecords where UserId = @userId and DateCreated >= @DateCreated and DateCreated < @DateCreated1",
                    new { userId, DateCreated = DateTime.Now.ToString("yyyy-MM-dd"), DateCreated1 = DateTime.Now.AddDays(1).ToString("yyyy-MM-dd") });
                return pointCount;
            }
            finally
            {
                GlobalHelper.ReturnConnection(connection, false);
            }
        }

        #endregion 积分统计

        #region 积分提醒

        /// <summary>
        /// 获取需要提醒的积分记录
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public static PointRecord GetUserLastestRecord(long userId)
        {
            string cacheKey = TrackPointRecordCacheKey(userId);
            PointRecord pointRecord = cacheService.GetFromFirstLevel<PointRecord>(cacheKey);
            if (pointRecord != null)
                cacheService.Remove(cacheKey);
            return pointRecord;
        }

        /// <summary>
        /// 跟踪用户的最新的积分记录
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="pointRecord"></param>
        /// <returns></returns>
        private static void TrackPointRecord(long userId, PointRecord pointRecord)
        {
            string cacheKey = TrackPointRecordCacheKey(userId);
            cacheService.Remove(cacheKey);
            cacheService.Set(cacheKey, pointRecord, new TimeSpan(0, 0, 30));
        }

        #endregion 积分提醒

        #region PointItem

        /// <summary>
        /// 更新积分项目
        /// </summary>
        /// <param name="pointItem">待更新的积分项目</param>
        public static void UpdatePointItem(PointItem pointItem)
        {
            //注意：ItemId、ApplicationId、ItemName、DisplayOrder不允许修改
            var connection = GlobalHelper.GetConnection(false);
            try
            {
                connection.Execute("Update tn_PointItems set ExperiencePoints = @ExperiencePoints, ReputationPoints = @ReputationPoints, TradePoints = @TradePoints, TradePoints2 = @TradePoints2, TradePoints3 = @TradePoints3, TradePoints4 = @TradePoints4, Description = @Description where ItemKey = @ItemKey",
                    pointItem);
            }
            finally
            {
                GlobalHelper.ReturnConnection(connection, false);
            }
            //添加操作日志
            EventBus<PointItem>.Instance().OnAfter(pointItem, new CommonEventArgs(EventOperationType.Instance().Update()));
        }

        /// <summary>
        /// 获取积分项目
        /// </summary>
        /// <param name="itemKey">积分项目标识</param>
        /// <returns>返回itemKey对应的PointItem，如果没有找到返回null</returns>
        public static PointItem GetPointItem(string itemKey)
        {
            var unitWork = GlobalHelper.GetUnitWork();
            try
            {
                return unitWork.FindSingle<PointItem>(itemKey);
            }
            finally
            {
                GlobalHelper.ReturnUnitWork(unitWork);
            }
        }

        /// <summary>
        /// 获取积分项目集合
        /// </summary>
        /// <param name="tenantTypeId">租户Id</param>
        /// <returns>如果无满足条件的积分项目返回空集合</returns>
        public static List<PointItem> GetPointItems(string tenantTypeId = "")
        {
            //排序条件：DisplayOrder正序
            var unitWork = GlobalHelper.GetUnitWork();
            try
            {
                return unitWork.Find<PointItem>(it => it.TenantTypeId == tenantTypeId).OrderBy(it => it.DisplayOrder).ToList();
            }
            finally
            {
                GlobalHelper.ReturnUnitWork(unitWork);
            }
        }

        /// <summary>
        /// 获取增加积分的积分项目集合
        /// </summary>
        /// <returns>如果无满足条件的积分项目返回空集合</returns>
        public static IEnumerable<PointItem> GetPointItemsOfIncome()
        {
            //过滤条件：ExperiencePoints > 0
            //排序条件：DisplayOrder正序
            List<PointItem> pointItems = GetPointItems(null);
            if (pointItems != null)
                return pointItems.Where(n => n.ExperiencePoints > 0 || n.ReputationPoints > 0 || n.TradePoints > 0).OrderBy(n => n.DisplayOrder);
            return new List<PointItem>();
        }

        #endregion PointItem

        #region PointCategory

        /// <summary>
        /// 更新积分类型
        /// </summary>
        /// <param name="pointCategory">待更新的积分类型</param>
        public static void UpdatePointCategory(PointCategory pointCategory)
        {
            //注意：CategoryKey、CategoryName、Description、DisplayOrder不允许修改
            //清除缓存
            var connection = GlobalHelper.GetConnection(false);
            try
            {
                connection.Execute("Update tn_PointCategories set CategoryName=@CategoryName, Unit = @Unit, QuotaPerDay = @QuotaPerDay where CategoryKey = @CategoryKey",
                    pointCategory);
            }
            finally
            {
                GlobalHelper.ReturnConnection(connection, false);
            }
        }

        /// <summary>
        /// 获取积分类型
        /// </summary>
        /// <param name="categoryKey">积分类型标识</param>
        /// <returns>返回itemKey对应的PointCategory，如果没有找到返回null</returns>
        public static PointCategory GetPointCategory(string categoryKey)
        {
            var unitWork = GlobalHelper.GetUnitWork();
            try
            {
                return unitWork.FindSingle<PointCategory>(categoryKey);
            }
            finally
            {
                GlobalHelper.ReturnUnitWork(unitWork);
            }
        }

        /// <summary>
        /// 获取积分类型集合
        /// </summary>
        /// <returns>如果无数据则返回空集合</returns>
        public static IEnumerable<PointCategory> GetPointCategories()
        {
            //排序条件：DisplayOrder正序
            var unitWork = GlobalHelper.GetUnitWork();
            try
            {
                return unitWork.Find<PointCategory>().OrderBy(it=>it.DisplayOrder).ToList();
            }
            finally
            {
                GlobalHelper.ReturnUnitWork(unitWork);
            }
        }


        /// <summary>
        /// 获取积分名称
        /// </summary>
        /// <param name="categoryKey">积分类型标识</param>
        public static string GetPointCategoryName(string categoryKey)
        {
            var PointCategory = GetPointCategory(categoryKey);
            return PointCategory?.CategoryName;
        }

        /// <summary>
        /// 获取积分名称
        /// </summary>
        /// <param name="categoryKey">积分类型标识</param>
        public static string GetPointCategoryNameById(string categoryKey)
        {
            IUnitWork unitWork = GlobalHelper.GetUnitWork();
            var categoryName = unitWork.Find<PointCategory>(it=>it.CategoryKey==categoryKey).Select(it=>it.CategoryName).FirstOrDefault();
            return categoryName;
        }
        #endregion PointCategory

        #region Help Methods

        /// <summary>
        /// 变更系统积分总额
        /// </summary>
        /// <param name="number">变更的积分值<remarks>减积分用负数</remarks></param>
        private static void ChangeSystemTradePoints(long number)
        {
            kstore.Set(KvKeys.Instance().TradePoints(), number);
        }

        /// <summary>
        /// 根据指定积分分类获取积分项目中的积分
        /// </summary>
        /// <param name="pointItem">积分项目</param>
        /// <returns><remarks>key=PointCategory,value=Points</remarks>积分分类-积分字典</returns>
        private static Dictionary<PointCategory, int> GetPointCategory2PointsDictionary(PointItem pointItem)
        {
            Dictionary<PointCategory, int> dictionary = new Dictionary<PointCategory, int>();
            foreach (var category in GetPointCategories())
            {
                int points = 0;
                if (category.CategoryKey == PointCategoryKeys.Instance().ExperiencePoints())
                    points = pointItem.ExperiencePoints;
                else if (category.CategoryKey == PointCategoryKeys.Instance().ReputationPoints())
                    points = pointItem.ReputationPoints;
                else if (category.CategoryKey == PointCategoryKeys.Instance().TradePoints())
                    points = pointItem.TradePoints;
                else if (category.CategoryKey == "TradePoints2")
                    points = pointItem.TradePoints2;
                else if (category.CategoryKey == "TradePoints3")
                    points = pointItem.TradePoints3;
                else if (category.CategoryKey == "TradePoints4")
                    points = pointItem.TradePoints4;
                dictionary[category] = points;
            }
            return dictionary;
        }

        /// <summary>
        /// 积分提醒的Cachekey
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        private static string TrackPointRecordCacheKey(long userId)
        {
            return string.Format("TrackPointRecord::userId-{0}", userId);
        }

        #endregion Help Methods
    }
}