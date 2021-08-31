//------------------------------------------------------------------------------
// <copyright company="Tunynet">
//     Copyright (c) Tunynet Inc.  All rights reserved.
// </copyright>
//------------------------------------------------------------------------------

using Core;
using Core.Users.Events;
using Dapper;
using NHibernate;
using NHibernate.Criterion;
using System;
using System.Collections.Generic;
using System.Linq;
using Tunynet.Common.Configuration;
using Tunynet.Events;
using Tunynet.Settings;
using Tunynet.Utilities;
using Utility.Domain.Uow;
using NHibernate.Linq;

namespace Tunynet.Common
{
    /// <summary>
    /// 用户业务逻辑
    /// </summary>
    public partial class UserService 
    {
        private static IUnitWork unitWork;
        static UserService()
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
        /// 创建用户
        /// </summary>
        /// <param name="user">待创建的用户</param>
        /// <param name="ignoreDisallowedUsername">是否忽略禁用的用户名称</param>
        /// <param name="userCreateStatus">用户帐号创建状态</param>
        /// <returns>创建成功返回IUser，创建失败返回null</returns>
        public static  IUser CreateUser(ISession session, User user, bool ignoreDisallowedUsername, out UserCreateStatus userCreateStatus)
        {
            userCreateStatus = UserCreateStatus.UnknownFailure;

            UserSettings userSettings = SettingManager<UserSettings>.Get();
            if (!ignoreDisallowedUsername)
            {
                if (userSettings.DisallowedUserNames.Split(new char[] { ',', '，' }).Any(n => n.Equals(user.UserName, StringComparison.CurrentCultureIgnoreCase)))
                {
                    //用户输入字段为禁用字段
                    userCreateStatus = UserCreateStatus.DisallowedUsername;
                    return null;
                }
            }

            if (GetUserByUserName(session,user.UserName) != null)
            {
                userCreateStatus = UserCreateStatus.DuplicateUsername;
                return null;
            }

            //判断邮箱是否唯一
            if (!string.IsNullOrEmpty(user.AccountEmail) && GetUserByEmail(session,user.AccountEmail)!=null)
            {
                userCreateStatus = UserCreateStatus.DuplicateEmailAddress;
                return null;
            }
            //如果不允许手机号重复的时候
            if (userSettings.RequiresUniqueMobile)
            {
                var user1 = GetUserByMobile(session, user.AccountMobile);
                if (user1.Status == UserStatus.IsActivated)
                {
                    userCreateStatus = UserCreateStatus.DuplicateMobile;
                    return null;
                }
            }
            user.UserId = IdGenerator.Next();
            session.Save(user);
            userCreateStatus = UserCreateStatus.Created;
            return user;
        }

        /// <summary>
        /// 用户名验证
        /// </summary>
        /// <param name="userName">待创建的用户名</param>
        /// <param name="ignoreDisallowedUsername">是否忽略禁用的用户名称</param>
        /// <param name="userCreateStatus">用户帐号创建状态</param>
        public static void RegisterValidate(ISession session, string userName, bool ignoreDisallowedUsername, out UserCreateStatus userCreateStatus)
        {
            userCreateStatus = UserCreateStatus.UnknownFailure;
            UserSettings userSettings = SettingManager<UserSettings>.Get();
            if (!ignoreDisallowedUsername)
            {
                if (userSettings.DisallowedUserNames.Split(new char[] { ',', '，' }).Any(n => n.Equals(userName, StringComparison.CurrentCultureIgnoreCase)))
                {
                    //用户输入字段为禁用字段
                    userCreateStatus = UserCreateStatus.DisallowedUsername;
                    return;
                }
            }
            //判断用户名是否唯一
            if (GetUserIdByUserName(session,userName) > 0)
            {
                userCreateStatus = UserCreateStatus.DuplicateUsername;
                return;
            }
            if (GetUser(GetUserIdByUserName(session, userName)) != null)
            {
                userCreateStatus = UserCreateStatus.DuplicateUsername;
                return;
            }
            else
            {
                userCreateStatus = UserCreateStatus.Created;
                return;
            }
        }

        ///	<summary>
        ///	重设密码（无需验证当前密码，供管理员或忘记密码时使用）
        ///	</summary>
        /// <param name="user">用户</param>
        ///	<param name="newPassword">新密码</param>
        ///	<returns>更新成功返回true，否则返回false</returns>
        public static bool ResetPassword(ISession session, User user, string newPassword)
        {
            if (user == null)
                return false;
            return session.Connection.Execute("update tn_Users set Password=@newPassword where UserId = @UserId", new { newPassword, user.UserId }) > 0;
        }

        /// <summary>
        /// 解除符合解除管制标准的用户（永久管制的用户不会自动解除管制）
        /// </summary>
        /// <param name="noModeratedUserPoint">用户自动接触管制状态所需的积分（用户综合积分）</param>
        /// <returns>被解除管制的用户集合</returns>
        public static IEnumerable<User> NoModeratedUsers(ISession session,int noModeratedUserPoint)
        {
            if (noModeratedUserPoint <= 0)
                return new List<User>();
            var unitWork = GlobalHelper.GetUnitWork();
            try
            {
                PointSettings pointSettings = SettingManager<PointSettings>.Get();
                IEnumerable<User> users_object = session.Query<User>().Where(it=>it.IsForceModerated==false&&it.IsModerated==true
                && (it.ExperiencePoints* pointSettings.ExperiencePointsCoefficient+it.ReputationPoints* pointSettings.ReputationPointsCoefficient)> noModeratedUserPoint)
                    .ToList();

                session.Query<User>().Where(it => it.IsForceModerated == false && it.IsModerated == true
                && (it.ExperiencePoints * pointSettings.ExperiencePointsCoefficient + it.ReputationPoints * pointSettings.ReputationPointsCoefficient)
                > noModeratedUserPoint).Update(it => new User() { IsModerated = false });

                return users_object;
            }
            finally
            {
                GlobalHelper.ReturnUnitWork(unitWork);
            }
        }

        /// <summary>
        /// 更新用户等级
        /// </summary>
        /// <param name="user">用户</param>
        /// <param name="rank">用户等级</param>
        /// <returns>受影响条数</returns>
        public static long UpdateRank(IUser user, int rank)
        {
            if (user == null)
                return -1;
            var connection = GlobalHelper.GetConnection(false);
            try
            {
               return connection.Execute("update tn_Users set Rank=@rank where UserId = @UserId", new { rank, user.UserId });
            }
            finally
            {
                GlobalHelper.ReturnConnection(connection, false);
            }
        }

  

        /// <summary>
        /// 更新用户封面图
        /// </summary>
        /// <param name="user">用户</param>
        /// <param name="hasCover">是否有用户封面图</param>
        public static void UpdateCover(ISession session,IUser user, int hasCover)
        {
            if (user == null)
                return;
            session.Connection.Execute("update tn_Users set HasCover=1 where UserId = @UserId", new { user.UserId });
        }

        #endregion Create/Update/Delete

        /// <summary>
        /// 封禁用户
        /// </summary>
        /// <param name="userId">用户Id</param>
        /// <param name="banDeadline">封禁截止日期</param>
        /// <param name="banReason">封禁原因</param>
        public static void BanUser(ISession session, long userId, DateTime banDeadline, string banReason)
        {
            if (banDeadline <= DateTime.Now)
                return;
            SetUser( session, userId,banDeadline,banReason, EventOperationType.Instance().BanUser(),true);
        }

        public static void SetUser(ISession session, long userId, DateTime banDeadline, string banReason, string eventOperationType,bool ban=true)
        {
            User user = session.Query<Common.User>().Where(it => it.UserId == userId).FirstOrDefault();
            user.IsBanned = ban;
            user.BanReason = banReason;
            user.BanDeadline = banDeadline;
            user.ForceLogin = ban;
            session.Update(user);
            EventBus<User>.Instance().OnAfter(user, new CommonEventArgs(eventOperationType));
        }

        /// <summary>
        /// 解禁用户
        /// </summary>
        /// <param name="userId">用户Id</param>
        public static void UnbanUser(ISession session, long userId)
        {
            SetUser(session,userId, DateTime.Now, string.Empty, EventOperationType.Instance().UnbanUser(), false);
        }

        /// <summary>
        /// 设置用户管制状态
        /// </summary>
        /// <param name="userIds">用户Id集合</param>
        /// <param name="isModerated">是否被管制</param>
        public static void SetModeratedStatus(IEnumerable<long> userIds, bool isModerated)
        {
            List<User> users = new List<User>();
            foreach (var userId in userIds)
            {
                User user = unitWork.FindSingle<User>(it => it.UserId == userId);
                if (user == null)
                    continue;
                if (user.IsModerated == isModerated)
                    continue;
                user.IsModerated = isModerated;
                user.IsForceModerated = isModerated;
                unitWork.Update(user);
                users.Add(user);
            }
            if (users.Count > 0)
            {
                string eventOperationType = isModerated ? EventOperationType.Instance().ModerateUser() : EventOperationType.Instance().CancelModerateUser();
                foreach (var user in users)
                {
                    EventBus<User>.Instance().OnAfter(user, new CommonEventArgs(eventOperationType));
                }
            }
        }

  

        /// <summary>
        /// 更新用户等级
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="rank"></param>
        public static void UpdateRank(long userId, int rank)
        {
            var connection = GlobalHelper.GetConnection(false);
            try
            {
                User user = connection.QueryFirstOrDefault<User>("select * from tn_Users where UserId = @userId ", new { userId });
                if (user == null)
                    return;
                EventBus<User, UpdateRankEventArgs>.Instance().OnBefore(user, new UpdateRankEventArgs(rank));
                if (connection.Execute("update tn_Users set Rank=@rank where UserId = @userId", new { userId , rank }) > 0)
                    EventBus<IUser, UpdateRankEventArgs>.Instance().OnAfter(user, new UpdateRankEventArgs(rank));
            }
            finally
            {
                GlobalHelper.ReturnConnection(connection, false);
            }
           
        }

        /// <summary>
        /// 更新用户积分
        /// </summary>
        /// <param name="userId">用户id</param>
        /// <param name="experiencePoints">经验值</param>
        /// <param name="reputationPoints">威望值</param>
        /// <param name="tradePoints">交易经验值</param>
        /// <param name="tradePoints2">交易经验值2</param>
        /// <param name="tradePoints3">交易经验值3</param>
        /// <param name="tradePoints4">交易经验值4</param>
        public static void ChangePoints(long userId, int experiencePoints, int reputationPoints, int tradePoints, int tradePoints2 = 0, int tradePoints3 = 0, int tradePoints4 = 0)
        {

            var ev = new UserChangePointEvent(userId, experiencePoints, reputationPoints, tradePoints, tradePoints2, tradePoints3, tradePoints4);
            GlobalHelper.Mediator.Publish(ev);
        }

        /// <summary>
        /// 冻结用户的交易积分
        /// </summary>
        /// <param name="userId">用户id</param>
        /// <param name="tradePoints">准备冻结的积分</param>
        public static void FreezeTradePoints(long userId, int tradePoints)
        {
            var ev = new UserTradePointEvent(userId,tradePoints, FreezeFlag.Freeze);
            GlobalHelper.Mediator.Publish(ev);
        }

        /// <summary>
        /// 解冻用户的交易积分
        /// </summary>
        /// <param name="userId">用户id</param>
        /// <param name="tradePoints">解冻的积分</param>
        public static void UnfreezeTradePoints(long userId, int tradePoints)
        {
            var ev = new UserTradePointEvent(userId, tradePoints, FreezeFlag.NoFreeze);
            GlobalHelper.Mediator.Publish(ev);
        }

        /// <summary>
        /// 减少冻结的交易积分（完成交易时使用）
        /// </summary>
        /// <param name="userId">用户Id</param>
        /// <param name="frozenTradePoints">需减少的冻结交易积分值</param>
        public static  void ReduceFrozenTradePoints(long userId, int frozenTradePoints)
        {
            var ev = new UserTradePointEvent(userId, frozenTradePoints, FreezeFlag.None);
            GlobalHelper.Mediator.Publish(ev);
        }

        /// <summary>
        /// 根据用户Id集合获取实体集合
        /// </summary>
        /// <param name="userIds">用户Id集合</param>
        /// <param name="topNumber">获取记录数</param>
        /// <returns></returns>
        public static IEnumerable<IUser> GetUsers(IEnumerable<long> userIds, int topNumber)
        {
            if (userIds == null)
                return new List<IUser>();
            var connection = GlobalHelper.GetConnection(true);
            try
            {
                return connection.Query<User>("select * from tn_Users where UserId in (@userIds) ", new { userIds });
            }
            finally
            {
                GlobalHelper.ReturnConnection(connection, true);
            }
        }

        /// <summary>
        /// 查询用户
        /// </summary>
        /// <param name="roleIds">用户角色Id</param>
        /// <param name="minRank">最低等级</param>
        /// <param name="maxRank">最高等级</param>
        public static IEnumerable<IUser> GetUsers(List<long> roleIds, int minRank = 0, int maxRank = 0)
        {
            var connection = GlobalHelper.GetConnection(true);
            try
            {
                string sql = "select tn_Users.UserId from tn_Users ";
                if (roleIds != null && roleIds.Count() > 0)
                {
                    sql+=" inner join tn_UsersInRoles on tn_Users.UserId=tn_UsersInRoles.UserId"+
                        " where tn_UsersInRoles.RoleId in (@roleIds)";
                }
                else
                {
                    sql += " where 1=1 ";
                }
                if (minRank > 0)
                {
                    sql += " or  tn_Users.Rank>=@Rank1 ";
                }
                if (maxRank > 0)
                {
                    sql += " or tn_Users.Rank<=@Rank2 ";
                }
                IEnumerable<long> userIds = connection.Query<long>(sql).ToList();
                return connection.Query<User>("select * from tn_Users where UserId in (@userIds) ", new { userIds });
            }
            finally
            {
                GlobalHelper.ReturnConnection(connection, true);
            }
        }

        /// <summary>
        /// 依据UserGuid集合组装IUser集合
        /// </summary>
        /// <param name="userGuids">用户Guid集合</param>
        /// <returns></returns>
        public static IEnumerable<User> GetUsersByGuids(IEnumerable<string> userGuids)
        {
            var connection = GlobalHelper.GetConnection(true);
            try
            {
                return connection.Query<User>("select * from tn_Users where UserGuid in (@userGuids) ", new { userGuids });
            }
            finally
            {
                GlobalHelper.ReturnConnection(connection,true);
            }
        }

        /// <summary>
        /// 获取所有用户
        /// *由于封装的GetAll在大数据处理会有问题 所以重写的GetAll
        /// </summary>
        /// <returns></returns>
        public  static IEnumerable<IUser> GetAll()
        {
            IEnumerable<IUser> AllUsers = null;
            string cachekey = "AllUsersCachekey";
            AllUsers = GlobalHelper.Cache.Get<IEnumerable<IUser>>(cachekey);
            if (AllUsers != null && AllUsers.Any())
                return AllUsers;
            var unitWork = GlobalHelper.GetUnitWork();
            try
            {
                AllUsers = unitWork.Find<User>().ToList();
                GlobalHelper.Cache.Set(cachekey, AllUsers,DateTime.Now.AddHours(24));
                return AllUsers;
            }
            finally
            {
                GlobalHelper.ReturnUnitWork(unitWork);
            }
        }
    }
}