//------------------------------------------------------------------------------
// <copyright company="Tunynet">
//     Copyright (c) Tunynet Inc.  All rights reserved.
// </copyright>
//------------------------------------------------------------------------------

using Core;
using Dapper;
using System.Collections.Generic;
using System.Linq;
using Tunynet.Common.Repositories;
using Tunynet.Events;
using Tunynet.Settings;
using Utility.Domain.Uow;

namespace Tunynet.Common
{
    /// <summary>
    /// 用户等级的逻辑类
    /// </summary>
    public class UserRankService
    {
        private static IUnitWork unitWork;
        static UserRankService()
        {
            if (unitWork == null)
                lock (unitWork)
                    if (unitWork == null)
                        unitWork = GlobalHelper.GetUnitWork();
        }
        public static void Return()
        {
            if (unitWork != null)
            {
                GlobalHelper.ReturnUnitWork(unitWork);
            }
        }

        #region Create/Update/Delete

        /// <summary>
        /// 添加用户等级
        /// </summary>
        /// <param name="userRank">用户等级</param>
        /// <returns>添加成功返回true，否则返回false</returns>
        public static bool Create(UserRank userRank)
        {
            EventBus<UserRank>.Instance().OnBefore(userRank, new CommonEventArgs(EventOperationType.Instance().Create()));
            if (userRank.Rank < 1)
            {
            }
            else
            {
                UserRank userRank1 = unitWork.FindSingle<UserRank>(userRank.Rank);
                if (userRank1 == null)
                    unitWork.Insert(userRank);
            }
            
            if (userRank.Rank > 0)
            {
                EventBus<UserRank>.Instance().OnAfter(userRank, new CommonEventArgs(EventOperationType.Instance().Create()));
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// 更新用户等级
        /// </summary>
        /// <param name="userRank">用户等级</param>
        public static void Update(UserRank userRank)
        {
            unitWork.Update(userRank);
           
        }

        /// <summary>
        /// 删除用户等级
        /// </summary>
        /// <param name="rank">用户级别</param>
        public static void Delete(int rank)
        {
            unitWork.Delete<UserRank>(rank);
        }

        /// <summary>
        /// 依据现行规则重置所有用户等级
        /// </summary>
        public static void ResetAllUser()
        {
            //按照定义的用户等级设定的积分区间更新用户等级（不考虑缓存即时性）
            PointSettings pointSettings = SettingManager<PointSettings>.Get();
            //todo 这里没处理缓存
            var connection = GlobalHelper.GetConnection(false);
            try
            {
                connection.Execute("update tn_Users set Rank = (select max(Rank) from tn_UserRanks UR where UR.PointLower <= (ExperiencePoints * @ExperiencePointsCoefficient ) or ((ExperiencePoints * @ExperiencePointsCoefficient )<0 and UR.Rank = 1) )",
                    pointSettings);
            }
            finally
            {
                GlobalHelper.ReturnConnection(connection, false);
            }
        }

        #endregion Create/Update/Delete

        #region Get

        /// <summary>
        /// 获取用户等级
        /// </summary>
        /// <param name="rank">用户级别(int)</param>
        /// <returns>用户等级实体</returns>
        public static UserRank Get(int rank)
        {
            var unitWork = GlobalHelper.GetUnitWork();
            try
            {
                return unitWork.FindSingle<UserRank>(rank);
            }
            finally
            {
                GlobalHelper.ReturnUnitWork(unitWork);
            }
        }

        /// <summary>
        /// 获取所有用户等级
        /// </summary>
        /// <remarks>key=rank , value=UserRank</remarks>
        /// <returns>所有用户级别实体集合</returns>
        public static SortedList<int, UserRank> GetAll()
        {
            IEnumerable<UserRank> userRanks = unitWork.Find<UserRank>().ToList();
            SortedList<int, UserRank> lists = new SortedList<int, UserRank>();
            foreach (var userRank in userRanks)
                lists.Add(userRank.Rank, userRank);
            return lists;
        }

        #endregion Get
    }
}