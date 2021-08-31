//------------------------------------------------------------------------------
// <copyright company="Tunynet">
//     Copyright (c) Tunynet Inc.  All rights reserved.
// </copyright>
//------------------------------------------------------------------------------
using Core;
using Dapper;
using System;
using System.Collections.Generic;
using System.Linq;
using Tunynet.Common.Repositories;
using Tunynet.Events;
using Tunynet.Repositories;
using Utility.Domain.Uow;

namespace Tunynet.Common
{
    /// <summary>
    /// 帐号绑定业务逻辑类
    /// </summary>
    public class AccountBindingService
    {

        #region 维护帐号绑定
        private static IUnitWork unitWork;
        static AccountBindingService()
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
        /// <summary>
        /// 创建第三方帐号绑定
        /// </summary>
        /// <param name="account"></param>
        public static void CreateAccountBinding(AccountBinding account)
        {
            //设计说明:
            //插入前，需要检查UserId+AccountTypeKey唯一
            EventBus<AccountBinding>.Instance().OnBefore(account, new CommonEventArgs(EventOperationType.Instance().Create()));
            if (!unitWork.IsExist<AccountBinding>(it => it.UserId == account.UserId &&
              it.AccountTypeKey == account.AccountTypeKey)
                  || !unitWork.IsExist<AccountBinding>(it => it.Identification == account.Identification &&
               it.AccountTypeKey == account.AccountTypeKey))
            {
                unitWork.Insert(account);
            }
            EventBus<AccountBinding>.Instance().OnAfter(account, new CommonEventArgs(EventOperationType.Instance().Create()));
        }

        /// <summary>
        /// 更新授权凭据
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="accountTypeKey"></param>
        /// <param name="identification"></param>
        /// <param name="accessToken"></param>
        /// <param name="expires_in"></param>
        public static void UpdateAccessToken(long userId, string accountTypeKey, string identification, string accessToken, int expires_in)
        {
            var unitWork = GlobalHelper.GetUnitWork();
            try
            {
                AccountBinding accountBinding = unitWork.FindSingle<AccountBinding>(it => it.UserId == userId &&
                it.AccountTypeKey == accountTypeKey && it.Identification == identification);
                if (accountBinding != null)
                {
                    accountBinding.AccessToken = accessToken;
                    if (expires_in > 0)
                        accountBinding.ExpiredDate = DateTime.Now.AddSeconds(expires_in);
                    unitWork.Update(accountBinding);
                }
            }
            finally
            {
                GlobalHelper.ReturnUnitWork(unitWork);
            }
        }

        /// <summary>
        /// 删除第三方帐号绑定
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="accountTypeKey">第三方帐号类型Key</param>
        public static void DeleteAccountBinding(long userId, string accountTypeKey)
        {
            var connection = GlobalHelper.GetConnection(false);
            try
            {
                AccountBinding accountBinding = GetAccountBinding(userId, accountTypeKey);
                connection.Execute("delete from tn_AccountBindings Where UserId=@userId and AccountTypeKey=@accountTypeKey",
                    new { userId, accountTypeKey });
                EventBus<AccountBinding>.Instance().OnAfter(accountBinding, new CommonEventArgs(EventOperationType.Instance().Delete()));
            }
            finally
            {
                GlobalHelper.ReturnConnection(connection, false);
            }
           
        }

        #endregion 维护帐号绑定

        #region 获取绑定

        /// <summary>
        /// 获取单个第三方帐号绑定
        /// </summary>
        /// <param name="userId">用户Id</param>
        /// <param name="accountTypeKey">第三方帐号类型Key</param>
        /// <returns></returns>
        public static AccountBinding GetAccountBinding(long userId, string accountTypeKey)
        {
            IEnumerable<AccountBinding> accountBindings = GetAccountBindings(userId);
            if (accountBindings != null && accountBindings.Count() > 0)
                return accountBindings.Where(ab => ab.AccountTypeKey.Equals(accountTypeKey, StringComparison.OrdinalIgnoreCase)).FirstOrDefault();
            else
                return null;
        }

        /// <summary>
        /// 获取某用户的所有第三方帐号绑定
        /// </summary>
        /// <param name="userId"></param>
        /// <returns>若没有，则返回空集合</returns>
        public static IEnumerable<AccountBinding> GetAccountBindings(long userId)
        {
            var connection = GlobalHelper.GetConnection(true);
            try
            {
                IEnumerable<AccountBinding> accountBindings = connection.Query<AccountBinding>(
                      "Select * From tn_AccountBindings where UserId= @userId",
                      new { userId });
                return accountBindings;
            }
            finally
            {
                GlobalHelper.ReturnConnection(connection, true);
            }
        }

        /// <summary>
        /// 获取用户Id
        /// </summary>
        /// <param name="accountTypeKey">第三方帐号类型Key</param>
        /// <param name="Identification">第三方帐号标识</param>
        /// <returns>用户Id</returns>
        public static long GetUserId(string accountTypeKey, string Identification)
        {
            //设计说明:
            //无需缓存
            var connection = GlobalHelper.GetConnection(true);
            try
            {
                long userId = connection.QueryFirst<long>(
                    "Select UserId From tn_AccountBindings where AccountTypeKey= @accountTypeKey and Identification= @Identification",
                    new { accountTypeKey, Identification });
                return userId;
            }
            finally
            {
                GlobalHelper.ReturnConnection(connection, true);
            }
        }

        #endregion 获取绑定

        #region 帐号类型

        /// <summary>
        /// 创建第三方帐号类型
        /// </summary>
        /// <param name="accountType"></param>
        public static void CreateAccountType(AccountType accountType)
        {
            EventBus<AccountType>.Instance().OnBefore(accountType, new CommonEventArgs(EventOperationType.Instance().Create()));
            var unitWork = GlobalHelper.GetUnitWork();
            try
            {
                unitWork.Insert(accountType);
            }
            finally
            {
                GlobalHelper.ReturnUnitWork(unitWork);
            }
            EventBus<AccountType>.Instance().OnAfter(accountType, new CommonEventArgs(EventOperationType.Instance().Create()));
        }

        /// <summary>
        /// 更新第三方帐号类型
        /// </summary>
        /// <param name="accountType"></param>
        public static void UpdateAccountType(AccountType accountType)
        {
            EventBus<AccountType>.Instance().OnBefore(accountType, new CommonEventArgs(EventOperationType.Instance().Update()));
            var unitWork = GlobalHelper.GetUnitWork();
            try
            {
                unitWork.Update(accountType);
            }
            finally
            {
                GlobalHelper.ReturnUnitWork(unitWork);
            }
            EventBus<AccountType>.Instance().OnAfter(accountType, new CommonEventArgs(EventOperationType.Instance().Update()));
        }

        /// <summary>
        /// 删除第三方帐号类型
        /// </summary>
        /// <param name="accountTypeKey"></param>
        public static void DeleteAccountType(string accountTypeKey)
        {
            var unitWork = GlobalHelper.GetUnitWork();
            try
            {
                unitWork.Delete<AccountType>(accountTypeKey);
            }
            finally
            {
                GlobalHelper.ReturnUnitWork(unitWork);
            }
        }

        /// <summary>
        /// 获取第三方帐号类型
        /// </summary>
        /// <param name="accountTypeKey"></param>
        /// <returns></returns>
        public static AccountType GetAccountType(string accountTypeKey)
        {
            var unitWork = GlobalHelper.GetUnitWork();
            try
            {
               return unitWork.FindSingle<AccountType>(accountTypeKey);
            }
            finally
            {
                GlobalHelper.ReturnUnitWork(unitWork);
            }
        }

        /// <summary>
        /// 获取所有第三方帐号类型
        /// </summary>
        /// <returns>若没有，则返回空集合</returns>
        public static IEnumerable<AccountType> GetAccountTypes(bool? isEnabled = null)
        {
            //设计说明:
            //缓存期限：相对稳定，需即时更新
            var unitWork = GlobalHelper.GetUnitWork();
            try
            {
                IEnumerable<AccountType> accountTypes = isEnabled.HasValue?
                    unitWork.Find<AccountType>(n => n.IsEnabled == isEnabled.Value).ToList()
                    : unitWork.Find<AccountType>().ToList();
                return accountTypes;
            }
            finally
            {
                GlobalHelper.ReturnUnitWork(unitWork);
            }
        }

        #endregion 帐号类型
    }
}