//------------------------------------------------------------------------------
// <copyright company="Tunynet">
//     Copyright (c) Tunynet Inc.  All rights reserved.
// </copyright>
//------------------------------------------------------------------------------

using Core;
using Microsoft.Extensions.Logging;
using NHibernate;
using Serilog.Context;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using Tunynet.Common.Configuration;
using Tunynet.Events;
using Tunynet.Settings;
using Utility.Collections;
using Utility.Domain.Uow;
using Utility.Regexs;

namespace Tunynet.Common
{
    public partial class UserCache
    {
        internal volatile static IList<string> Phones = new Array<string>(10 * 100000) { ResizeLength=10000};
        internal volatile static IList<string> Emails = new Array<string>(10 * 100000) { ResizeLength = 10000 };
        internal volatile static IList<long> UserIds = new Array<long>(10 * 100000) { ResizeLength = 10000 };
        internal  static volatile  IDictionary<long, User> Users = new Dictionary<long, User>(10000);
        private static ILogger<UserCache> logger;
        public static int GetPhone(string account)
        {
            int index = Phones.IndexOf(account);
            if (index == -1)
            {

            }
            return index;
        }

        public static int GetEmail(string email)
        {
            int index= Emails.IndexOf(email);
            if (index == -1)
            {

            }
            return index;
        }
        public static User GetByEmail(string email)
        {
            int index = GetEmail(email);
            if (index == -1)
            {
                return null;
            }
            long userId = GetUserId(index);
            return GetUser(userId);
        }

        public static User GetByPhone(string phone)
        {
            int index = GetPhone(phone);
            if (index == -1)
            {
                return null;
            }
            long userId = GetUserId(index);
            return GetUser(userId);
        }

        public static string GetEmail(int index)
        {
            if (index > -1)
            {
                if (index < Emails.Count)
                {
                    string email = Emails[index];
                    return email;
                }
                else
                {

                }
            }
            return string.Empty;
        }

        public static string GetPhone(int index)
        {
            if (index > -1)
            {
                if (index < Phones.Count)
                {
                    string phone = Phones[index];
                    return phone;
                }
                else
                {

                }
            }
            return string.Empty;
        }

        public static long GetUserId(int index)
        {
            if (index > -1)
            {
                if (index < UserIds.Count)
                {
                    long userId = UserIds[index];
                    return userId;
                }
                else
                {

                }
            }
            return 0L;
        }

        public static User GetUser(long userId)
        {
            if(Users.ContainsKey(userId))
            {
                return Users[userId];
            }
            else
            {
                return null;
            }
        }
    }
    /// <summary>
    /// 用户账户业务逻辑
    /// </summary>
    public class MembershipService
    {
        private static IUnitWork unitWork;
      
        public static void Return()
        {
            if (unitWork != null)
            {
                GlobalHelper.ReturnUnitWork(unitWork);
            }
        }
        private static ILogger<MembershipService> logger;
        static MembershipService()
        {
            if (logger == null)
                lock (logger)
                    if (logger == null)
                    {
                        unitWork = GlobalHelper.GetUnitWork();
                        logger = GlobalHelper.LoggerFactory.CreateLogger<MembershipService>();
                    }
        }
        /// <summary>
        /// 创建用户
        /// </summary>
        /// <param name="user">待创建的用户</param>
        /// <param name="password">密码</param>
        /// <param name="userCreateStatus">用户帐号创建状态</param>
        /// <returns>创建成功返回IUser，创建失败返回null</returns>
        public static IUser CreateUser(ISession session,IUser user, string password, out UserCreateStatus userCreateStatus)
        {
            return CreateUser(session,user, password, string.Empty, string.Empty, false, out userCreateStatus);
        }

        /// <summary>
        /// 创建用户
        /// </summary>
        /// <param name="user">待创建的用户</param>
        /// <param name="password">密码</param>
        /// <param name="passwordQuestion">密码问题</param>
        /// <param name="passwordAnswer">密码答案</param>
        /// <param name="ignoreDisallowedUsername">是否忽略禁用的用户名称</param>
        /// <param name="userCreateStatus">用户帐号创建状态</param>
        /// <returns>创建成功返回IUser，创建失败返回null</returns>
        public static IUser CreateUser(ISession session, IUser user, string password, string passwordQuestion, string passwordAnswer, bool ignoreDisallowedUsername, out UserCreateStatus userCreateStatus)
        {
            User user_object = user as User;
            if (user_object == null)
            {
                userCreateStatus = UserCreateStatus.UnknownFailure;
                return null;
            }

            UserSettings userSettings = SettingManager<UserSettings>.Get();
            user_object.PasswordFormat = (int)userSettings.UserPasswordFormat;
            user_object.Password = UserPasswordHelper.EncodePassword(password, userSettings.UserPasswordFormat);
            user_object.IsModerated = userSettings.AutomaticModerated;
            user = UserService.CreateUser(session,user_object, ignoreDisallowedUsername, out userCreateStatus);

            if (userCreateStatus == UserCreateStatus.Created)
            {
                UserIdToUserNameDictionary.RemoveUserId(user.UserId);
                UserIdToUserNameDictionary.RemoveUserName(user.UserName);
            }

            return user;
        }


        /// <summary>
        /// 删除用户
        /// </summary>
        /// <param name="userId">用户Id</param>
        /// <param name="takeOverUserName">用于接管删除用户时不能删除的内容(例如：用户创建的群组)</param>
        /// <returns></returns>
        public static UserDeleteStatus DeleteUser(long userId, string takeOverUserName)
        {
            return DeleteUser(userId, takeOverUserName, false);
        }

        /// <summary>
        /// 删除用户
        /// </summary>
        /// <param name="userId">用户Id</param>
        /// <param name="takeOverUserName">用于接管删除用户的内容(例如：用户创建的群组)</param>
        /// <param name="takeOverAll">是否接管被删除用户的所有内容</param>
        /// <param name="deleteContent">是否删除所有用户相关的内容</param>
        /// <remarks>接管被删除用户的所有内容</remarks>
        /// <returns></returns>
        public static UserDeleteStatus DeleteUser(long userId, string takeOverUserName, bool takeOverAll, bool deleteContent = false)
        {
            User user = UserService.GetUser(userId);
            if (user == null)
                return UserDeleteStatus.DeletingUserNotFound;

            if (takeOverAll)
            {
                User takeOverUser = UserService.GetUserByUserName(takeOverUserName);
                if (takeOverUser == null)
                    return UserDeleteStatus.InvalidTakeOverUsername;
            }

            user.Status = UserStatus.Delete;
            unitWork.Update(user);
            UserIdToUserNameDictionary.RemoveUserId(userId);
            UserIdToUserNameDictionary.RemoveUserName(user.UserName);
            if (deleteContent)
            {
                //这个事件只用于删除用户相关数据 请不要拿来干别的
                EventBus<User, DeleteUserEventArgs>.Instance().OnAfter(user, new DeleteUserEventArgs(takeOverUserName, takeOverAll));
            }
            EventBus<User>.Instance().OnAfter(user, new CommonEventArgs(EventOperationType.Instance().Delete()));
            return UserDeleteStatus.Deleted;
        }

        /// <summary>
        /// 更新用户
        /// </summary>
        /// <param name="user"></param>
        /// <param name="historyData">历史数据</param>
        public static void UpdateUser(IUser user, IUser historyData = null)
        {
            User user_object = user as User;
            User history_user_object = historyData as User;
            if (user_object == null)
                return;
            EventBus<User>.Instance().OnBefore(user_object, new CommonEventArgs(EventOperationType.Instance().Update()));
            unitWork.Update(user_object);
            EventBus<User>.Instance().OnAfter(user_object, new CommonEventArgs(EventOperationType.Instance().Update()));
            EventBus<User>.Instance().OnAfterWithHistory(user_object, new CommonEventArgs(EventOperationType.Instance().Update()), history_user_object);

        }

        /// <summary>
        /// 批量激活用户
        /// </summary>
        /// <param name="userIds">用户Id集合</param>
        /// <param name="status">用户账号状态(-1=已删除,1=已激活,0=未激活)</param>
        public static void ActivateUsers(IEnumerable<long> userIds, UserStatus status = UserStatus.IsActivated)
        {
            List<User> users = new List<User>();
            foreach (var userId in userIds)
            {
                User user = UserService.GetUser(userId);
                if (user == null)
                    continue;

                if (user.Status == status)
                    continue;

                user.Status = status;
                user.ForceLogin = false;
                unitWork.Update(user);
                users.Add(user);
            }
            if (users.Count > 0)
            {
                string eventOperationType = string.Empty;
                switch (status)
                {
                    case UserStatus.Delete:
                        eventOperationType = EventOperationType.Instance().DeleteUser();
                        break;

                    case UserStatus.IsActivated:
                        eventOperationType = EventOperationType.Instance().ActivateUser();
                        break;

                    case UserStatus.NoActivated:
                        eventOperationType = EventOperationType.Instance().CancelActivateUser();
                        break;
                }

                foreach (var user in users)
                {
                    EventBus<User>.Instance().OnAfter(user, new CommonEventArgs(eventOperationType));
                }
            }
        }

        ///	<summary>
        ///	更新密码（需要验证当前密码）
        ///	</summary>
        /// <param name="username">用户名</param>
        ///	<param name="password">当前密码</param>
        ///	<param name="newPassword">新密码</param>
        ///	<returns>更新成功返回true，否则返回false</returns>
        public static bool ChangePassword(string username, string password, string newPassword)
        {
            if (ValidateUser(username, password,out User user) == UserLoginStatus.Success)
            {
                EventBus<User>.Instance().OnAfter(user, new CommonEventArgs(EventOperationType.Instance().ResetPassword()));
                return ResetPassword(username, newPassword);
            }

            return false;
        }

        ///	<summary>
        ///	重设密码（无需验证当前密码，供管理员或忘记密码时使用含手机和邮箱重置）
        ///	</summary>
        /// <param name="username">用户名</param>
        ///	<param name="newPassword">新密码</param>
        ///	<remarks>成功时，会自动发送密码已修改邮件</remarks>
        ///	<returns>更新成功返回true，否则返回false</returns>
        public static bool ResetPassword(string username, string newPassword)
        {
            var  user = UserService.GetUserByUserName(username);
            if (user == null)
            {
                if (RegexHelper.IsPhone(username))
                {
                    user = UserService.GetUserByMobile(username);
                }
            }
            if (user == null)
            {
                if (RegexHelper.IsEmail(username))
                {
                    user = UserService.GetUserByEmail(username);
                }
            }
            if (user == null)
                return false;

            string storedPassword = UserPasswordHelper.EncodePassword(newPassword, (UserPasswordFormat)user.PasswordFormat);
            EventBus<User>.Instance().OnBefore(user, new CommonEventArgs(EventOperationType.Instance().ResetPassword()));
            bool result = UserService.ResetPassword(user, storedPassword);

            if (result)
                EventBus<User>.Instance().OnAfter(user, new CommonEventArgs(EventOperationType.Instance().ResetPassword()));

            return result;
        }

    

        /// <summary>
        /// 验证提供的用户名和密码是否匹配(含手机登录和邮箱登录)
        /// </summary>
        /// <param name="username">用户名</param>
        /// <param name="password">密码</param>
        /// <param name="loginUser">当前用户实体</param>
        /// <returns>返回<see cref="UserLoginStatus"/></returns>
        public static UserLoginStatus ValidateUser(string username, string password, out User loginUser)
        {
            var userSetting = SettingManager<UserSettings>.Get();
            long userId = UserIdToUserNameDictionary.GetUserId(username);
            loginUser = null;
            switch (userSetting.RegisterType)
            {
                case RegisterType.Mobile:
                    //如果是手机号登录
                    if (RegexHelper.IsPhone(username))
                        loginUser = UserCache.GetByPhone(username) ?? UserService.GetUserByMobile(username);
                    if (loginUser == null)
                        return UserLoginStatus.NoMobile;
                    break;

                case RegisterType.Email:
                    //如果是邮箱登录
                    if (RegexHelper.IsEmail(username))
                        loginUser = UserCache.GetByEmail(username) ?? UserService.GetUserByEmail(username);
                    if (loginUser == null)
                        return UserLoginStatus.NoEmail;
                    break;

                case RegisterType.MobileOrEmail:
                case RegisterType.EmailOrMobile:
                    if (RegexHelper.IsEmail(username))
                        loginUser = UserCache.GetByEmail(username) ?? UserService.GetUserByEmail(username);
                    else if (RegexHelper.IsPhone(username))
                        loginUser = UserCache.GetByPhone(username) ?? UserService.GetUserByMobile(username);
                    else
                        return UserLoginStatus.InvalidAccount;
                    break;
            }
            if (loginUser == null)
            {
                logger.LogWarning($"根据 手机 号  或 邮箱 : {username} 查找 用户 成功 ,登录 方式 :{RegisterType.Mobile.ToString()}，但 查询 用户信息 失败,缓存 或 数据库 命中 失败");
                //LogContext.Push("");
                return UserLoginStatus.InvalidCredentials;
            }

            if (!UserPasswordHelper.CheckPassword(password, loginUser.Password, (UserPasswordFormat)loginUser.PasswordFormat))
            {
                logger.LogWarning($"根据 手机 号  或 邮箱 {username} 查找 用户id {userId} 成功，登录 方式 :{RegisterType.Mobile.ToString()}，但 查询 用户信息 成功,密码 匹配失败 ");
                return UserLoginStatus.InvalidCredentials;
            }

            if (loginUser.Status != UserStatus.IsActivated)
                return UserLoginStatus.NotActivated;
            if (loginUser.IsBanned)
            {
                if (loginUser.BanDeadline >= DateTime.Now)
                    return UserLoginStatus.Banned;
                else
                {
                    loginUser.IsBanned = false;
                    loginUser.BanDeadline = DateTime.Now;
                    unitWork.Update(loginUser);
                }
            }
            return UserLoginStatus.Success;
        }
    }
}