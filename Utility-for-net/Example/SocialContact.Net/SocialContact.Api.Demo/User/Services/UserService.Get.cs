using Core;
using Dapper;
using NHibernate;
using NHibernate.Criterion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tunynet.Common;
using Tunynet.Utilities;
using Utility.Domain.Uow;
using Utility.Nhibernate;
using Utility.Nhibernate.Uow;

namespace Tunynet.Common
{
    public partial class UserService
    {

        #region Get && Gets

        /// <summary>
        /// 根据用户名获取用户Id
        /// </summary>
        /// <param name="userName">用户名</param>
        /// <returns>用户Id</returns>
        public static long GetUserIdByUserName(ISession session, string userName)
        {
            return session.Query<User>().Where(it => it.UserName == userName && it.Status != UserStatus.Delete)
                .Select(it => it.UserId).FirstOrDefault();
        }


        /// <summary>
        /// 查询用户
        /// </summary>
        /// <param name="userQuery">查询用户条件</param>
        /// <param name="pageSize">页面显示条数</param>
        /// <param name="pageIndex">页码</param>
        /// <returns>用户分页集合</returns>
        public static PagingDataSet<User> GetUsers(ISession session, UserQuery userQuery, int pageSize, int pageIndex)
        {
            NhibernateUnitWork nhibernateUnitWork = unitWork as NhibernateUnitWork;
            if (nhibernateUnitWork == null)
            {
                return null;
            }
            var criteria = session.CreateCriteria<User>();
            List<AbstractCriterion> ands = new List<AbstractCriterion>();
            buildSqlWhere(userQuery, ands, unitWork);
            switch (userQuery.UserSortBy)
            {
                case UserSortBy.UserId:
                    criteria.AddOrder(Order.Asc("UserId"));
                    break;
                case UserSortBy.UserId_Desc:
                    criteria.AddOrder(Order.Desc("UserId"));
                    break;
                case UserSortBy.LastActivityTime:
                    criteria.AddOrder(Order.Asc("LastActivityTime"));
                    break;
                case UserSortBy.LastActivityTime_Desc:
                    criteria.AddOrder(Order.Desc("LastActivityTime"));
                    break;
                case UserSortBy.IsActivated:
                    criteria.AddOrder(Order.Asc("Status"));
                    break;
                case UserSortBy.IsActivated_Desc:
                    criteria.AddOrder(Order.Desc("Status"));
                    break;
                case UserSortBy.IsModerated:
                    criteria.AddOrder(Order.Asc("IsModerated"));
                    break;
                case UserSortBy.IsModerated_Desc:
                    criteria.AddOrder(Order.Desc("IsModerated"));
                    break;
                default:
                    criteria.AddOrder(Order.Desc("UserId"));
                    break;
            }
            var data = criteria.SetFirstResult((pageIndex - 1) * pageSize).SetMaxResults(pageSize).List<User>();
            criteria.ClearOrders();
            var c = (NHibernate.ICriteria)criteria.Clone();
            var count = NhibernateTemplate.Empty.GetCount<User>(session, c);
            return new PagingDataSet<User>(data) { PageIndex = pageIndex, PageSize = pageSize, TotalRecords = count };
        }



        /// <summary>
        /// 根据用户状态获取用户数
        /// </summary>
        /// <param name="status">用户账号状态(-1=已删除,1=已激活,0=未激活)</param>
        /// <param name="isBanned">是否封禁</param>
        /// <param name="isModerated">是否管制</param>
        public static Dictionary<UserManageableCountType, int> GetManageableCounts(UserStatus status, bool isBanned, bool isModerated)
        {
            var connection = GlobalHelper.GetConnection(true);
            try
            {
                //todo:by mazq,20170406,@zhangzh 用status合成sql语句怎么能断定就是激活状态？
                Dictionary<UserManageableCountType, int> countType = new Dictionary<UserManageableCountType, int>();

                countType[UserManageableCountType.IsActivated] = connection.RecordCount<User>("Status=@status", new { status });
                countType[UserManageableCountType.IsBanned] = connection.RecordCount<User>("IsBanned=@isBanned", new { isBanned });

                countType[UserManageableCountType.IsModerated] = connection.RecordCount<User>("IsModerated=@isModerated", new { isModerated });


                countType[UserManageableCountType.IsAll] = connection.RecordCount<User>();


                countType[UserManageableCountType.IsLast24] = connection.RecordCount<User>("DateCreated > @DateCreated and  DateCreated < @DateCreated1", new { DateCreated = DateTime.Now.AddDays(-1), 
                    DateCreated1= DateTime.Now.AddDays(1) });

                return countType;
            }
            finally
            {
                GlobalHelper.ReturnConnection(connection, true);
            }
        }

        /// <summary>
        /// 获取前N个用户
        /// </summary>
        /// <param name="topNumber">获取用户数</param>
        /// <param name="sortBy">排序字段</param>
        /// <returns></returns>
        public static IEnumerable<User> GetTopUsers(int topNumber, SortBy_User sortBy)
        {
            var connection = GlobalHelper.GetConnection(false);
            try
            {
                IEnumerable<User> topUsers = null;

                string cacheKey = string.Format("TopUsers:SortBy-{0}", (int)sortBy);
                topUsers = GlobalHelper.Cache.Get<IEnumerable<User>>(cacheKey);
                if (topUsers == null || true)
                {
                    var sql = string.Empty;
                    string orderBy = string.Empty;
                    //Zhangzh修改计数调用
                    //CountService countService = new CountService(TenantTypeIds.Instance().User());
                    StageCountTypeManager stageCountTypeManager = StageCountTypeManager.Instance(TenantTypeIds.Instance().User());
                    //string countTableName = countService.GetTableName_Counts();
                    int stageCountDays;
                    string stageCountType;

                    switch (sortBy)
                    {
                        case SortBy_User.FollowerCount:
                            orderBy = "FollowerCount desc";
                            break;

                        case SortBy_User.ReputationPoints:
                            orderBy = "ReputationPoints desc";
                            break;

                        case SortBy_User.DateCreated:
                            orderBy = "UserId desc";
                            break;

                        case SortBy_User.PreWeekHitTimes:
                            stageCountDays = 7;
                            stageCountType = stageCountTypeManager.GetStageCountType(CountTypes.Instance().HitTimes(), stageCountDays);
                            sql += string.Format(" Left Join (select * from tn_Counts WHERE (tn_Counts.CountType = '{1}' and tn_Counts.TenantTypeId= '{0}')) c", TenantTypeIds.Instance().User(), stageCountType)
                            + " On UserId = c.ObjectId";
                            orderBy = "c.StatisticsCount desc";
                            break;

                        case SortBy_User.HitTimes:
                            sql += string.Format(" Left Join (select * from tn_Counts WHERE (tn_Counts.CountType = '{1}' and tn_Counts.TenantTypeId= '{0}')) c", TenantTypeIds.Instance().User(), CountTypes.Instance().HitTimes())
                            + " On UserId = c.ObjectId";
                            orderBy = "c.StatisticsCount desc";
                            break;

                        case SortBy_User.PreWeekReputationPoints:
                            stageCountDays = 7;
                            stageCountType = stageCountTypeManager.GetStageCountType(CountTypes.Instance().ReputationPointsCounts(), stageCountDays);
                            sql += string.Format("  Left Join  (select * from tn_Counts WHERE ({0}.CountType = '{1}' and tn_Counts.TenantTypeId= '{0}')) c", TenantTypeIds.Instance().User(), stageCountType)
                            + " On UserId = c.ObjectId";
                            orderBy = "c.StatisticsCount desc";
                            break;

                        default:
                            orderBy = "FollowerCount desc";
                            break;
                    }
                    sql = //"select tn_Users.* from  tn_Users "+
                        sql + " where Status =1 and IsBanned = 0 ";
                    topUsers = connection.GetListPaged<User>(topNumber, 1, sql, "", orderBy);
                    GlobalHelper.Cache.Set(cacheKey, topUsers, DateTime.Now.AddDays(1));
                }
                return topUsers;
            }
            finally
            {
                GlobalHelper.ReturnConnection(connection, false);
            }
        }

        /// <summary>
        /// 根据排序条件分页显示用户
        /// </summary>
        /// <param name="sortBy">排序条件</param>
        /// <param name="pageIndex">当前页码</param>
        /// <param name="pageSize">每页记录</param>
        /// <returns>根据排序条件倒排序分页显示用户</returns>
        public static PagingDataSet<User> GetPagingUsers(SortBy_User? sortBy, int pageIndex, int pageSize)
        {
            var connection = GlobalHelper.GetConnection(false);
            try
            {
                var sql = string.Empty;
                var orderSql =string.Empty;
                //Zhangzh修改计数调用
                //CountService countService = new CountService(TenantTypeIds.Instance().User());
                StageCountTypeManager stageCountTypeManager = StageCountTypeManager.Instance(TenantTypeIds.Instance().User());
                //string countTableName = countService.GetTableName_Counts();
                int stageCountDays;
                string stageCountType;

                if (sortBy.HasValue)
                {
                    switch (sortBy)
                    {
                        case SortBy_User.FollowerCount:
                            orderSql="FollowerCount desc";
                            break;
                        case SortBy_User.Rank:
                            orderSql = "Rank desc";
                            break;
                        case SortBy_User.ReputationPoints:
                            orderSql = "ReputationPoints desc";
                            break;
                        case SortBy_User.TradePoints:
                            orderSql = "TradePoints desc";
                            break;
                        case SortBy_User.DateCreated:
                            orderSql = "UserId desc";
                            break;
                        case SortBy_User.PreWeekHitTimes:
                            stageCountDays = 7;
                            stageCountType = stageCountTypeManager.GetStageCountType(CountTypes.Instance().HitTimes(), stageCountDays);
                            sql+=string.Format(" Left Join (select * from tn_Counts WHERE ({0}.CountType = '{1}' and tn_Counts.TenantTypeId= '{0}')) c", TenantTypeIds.Instance().User(), stageCountType)
                            +" On UserId = c.ObjectId";
                            orderSql = "c.StatisticsCount desc";
                            break;
                        case SortBy_User.HitTimes:
                            sql+=string.Format("  Left Join (select * from tn_Counts WHERE ({0}.CountType = '{1}' and tn_Counts.TenantTypeId= '{0}')) c", TenantTypeIds.Instance().User(), CountTypes.Instance().HitTimes())
                            +" On UserId = c.ObjectId";
                            orderSql = "c.StatisticsCount desc";
                            break;
                        case SortBy_User.PreWeekReputationPoints:
                            stageCountDays = 7;
                            stageCountType = stageCountTypeManager.GetStageCountType(CountTypes.Instance().ReputationPointsCounts(), stageCountDays);
                            sql+=string.Format(" Left Join (select * from tn_Counts WHERE ({0}.CountType = '{1}' and tn_Counts.TenantTypeId= '{0}')) c", TenantTypeIds.Instance().User(), stageCountType)
                            +" OnUserId = c.ObjectId";
                            orderSql = "c.StatisticsCount desc";
                            break;

                        default:
                            orderSql = "UserId desc";
                            break;
                    }
                }
                var data = connection.GetListPaged<User>(pageSize, pageIndex, sql+" where Status =1 and IsBanned = 0", orderSql);
                var count = connection.RecordCount<User>(sql + " where Status =1 and IsBanned = 0");
                return new PagingDataSet<User>(data) { PageIndex=pageIndex,PageSize=pageSize,TotalRecords=count};
            }
            finally
            {
                GlobalHelper.ReturnConnection(connection, false);
            }
        }

        #endregion Get && Gets

        /// <summary>
        /// 从UserQuery构建PetaPoco.Sql的where条件
        /// </summary>
        /// <param name="userQuery">UserQuery查询条件</param>
        /// <param name="sql">PetaPoco.Sql对象</param>
        private static void buildSqlWhere(UserQuery userQuery, List<AbstractCriterion> ands, IUnitWork unitWork)
        {

            if (!string.IsNullOrEmpty(userQuery.AccountEmailFilter))
                ands.Add(Expression.Like("AccountEmail", "%" + StringUtility.StripSQLInjection(userQuery.AccountEmailFilter) + "%"));
            if (userQuery.status.HasValue)
                ands.Add(Expression.Eq("Status", userQuery.status));
            else
                ands.Add(Expression.Eq("Status", UserStatus.Delete));
            if (userQuery.IsBanned.HasValue)
                ands.Add(Expression.Eq("IsBanned", userQuery.IsBanned));
            if (userQuery.IsModerated.HasValue)
            {
                ands.Add(Expression.Or(Expression.Eq("IsModerated", userQuery.IsModerated),
                    Expression.Eq("IsForceModerated", userQuery.IsModerated)
                    ));
            }
            if (!string.IsNullOrEmpty(userQuery.Keyword))
            {
                string str = "%" + StringUtility.StripSQLInjection(userQuery.Keyword) + "%";
                ands.Add(Expression.Or(Expression.Like("UserName", str),
                 Expression.Or(Expression.Like("TrueName", str),
                  Expression.Or(Expression.Like("AccountMobile", str), Expression.Like("AccountEmail", str)))
                 ));
            }
            if (userQuery.RoleId != 0)
                ands.Add(Expression.In("UserId", unitWork.Find<UserInRole>(it => it.RoleId == userQuery.RoleId)
                    .Select(it => it.UserId).ToList()
                 ));
            if (userQuery.RegisterTimeLowerLimit.HasValue)
                ands.Add(Expression.Ge("DateCreated", userQuery.RegisterTimeUpperLimit.Value.AddDays(1).ToUniversalTime()));
            if (userQuery.RegisterTimeUpperLimit.HasValue)
                ands.Add(Expression.Le("DateCreated", userQuery.RegisterTimeUpperLimit.Value.AddDays(1).ToUniversalTime()));
            if (userQuery.UserRankLowerLimit.HasValue)
                ands.Add(Expression.Ge("Rank", userQuery.UserRankLowerLimit));
            if (userQuery.UserRankUpperLimit.HasValue)
                ands.Add(Expression.Le("Rank", userQuery.UserRankUpperLimit));
        }


        #region Get & Gets

        /// <summary>
        /// 获取用户
        /// </summary>
        /// <param name="userId">用户ID</param>
        public static User GetUser(ISession session,long userId)
        {
            if (userId <= 0)
                return null;
            return session.Query<User>().Where(it => it.UserId == userId).FirstOrDefault();
        }

        /// <summary>
        /// 获取用户
        /// </summary>
        /// <param name="userGuid">用户Guid</param>
        public static User GetUserByGuid(string userGuid)
        {
            if (!string.IsNullOrEmpty(userGuid))
            {
                var unitWork = GlobalHelper.GetUnitWork();
                try
                {
                    return unitWork.FindSingle<User>(it => it.UserGuid == userGuid);
                }
                finally
                {
                    GlobalHelper.ReturnUnitWork(unitWork);
                }
            }
            return null;
        }

     

        /// <summary>
        /// 根据昵称获取用户
        /// </summary>
        /// <param name="userName">昵称</param>
        public static User GetUserByUserName(ISession session, string userName)
        {
            return session.Query<User>().Where(it => it.UserName == userName).FirstOrDefault();
        }

        /// <summary>
        /// 根据帐号邮箱获取用户
        /// </summary>
        /// <param name="accountEmail">帐号邮箱</param>
        /// <param name="statue">帐号状态</param>
        public static User GetUserByEmail(ISession session, string accountEmail, UserStatus? statue = UserStatus.IsActivated)
        {
            User user = UserCache.GetByEmail(accountEmail);
            if (user != null)
            {
                //if (user.Status == statue)
                //{
                //    return user;
                //}
                return user;
            }
            return session.Query<User>().Where(it => it.AccountEmail == accountEmail && it.Status == statue).FirstOrDefault();
        }

        /// <summary>
        /// 根据手机号获取用户
        /// </summary>
        /// <param name="accountMobile">手机号</param>
        /// <param name="statue">帐号状态</param>
        public static User GetUserByMobile(ISession session, string accountMobile, UserStatus? statue = UserStatus.IsActivated)
        {
            User user = UserCache.GetByPhone(accountMobile);
            if (user != null)
            {
                //if (user.Status == statue)
                //{
                //    return user;
                //}
                return user;
            }
            return session.Query<User>().Where(it => it.AccountMobile == accountMobile && it.Status == statue).FirstOrDefault();
        }

        /// <summary>
        /// 依据UserId集合组装IUser集合
        /// </summary>
        /// <param name="userIds"></param>
        /// <returns></returns>
        public static IEnumerable<User> GetUsers(ISession session,IEnumerable<long> userIds)
        {
            var connection = session.Connection;
            return connection.Query<User>("select * from tn_Users where UserId in (@userIds) ", new { userIds });
        }

        #endregion Get & Gets
    }
}
