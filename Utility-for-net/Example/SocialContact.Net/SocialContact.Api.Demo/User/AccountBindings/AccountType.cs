//------------------------------------------------------------------------------
// <copyright company="Tunynet">
//     Copyright (c) Tunynet Inc.  All rights reserved.
// </copyright>
//------------------------------------------------------------------------------

using PetaPoco;
using System;
using Tunynet.Caching;

namespace Tunynet.Common
{
    /// <summary>
    /// 第三方帐号类型
    /// </summary>
    [TableName("tn_AccountTypes")]
    [PrimaryKey("AccountTypeKey", autoIncrement = false)]
    [CacheSetting(true, ExpirationPolicy = EntityCacheExpirationPolicies.Stable)]
    [Serializable]
    public class AccountType : IEntity
    {
        #region 构造函数

        /// <summary>
        /// 创建第三方帐号类型
        /// </summary>
        public static AccountType New()
        {
            AccountType accountType = new AccountType()
            {
                AccountTypeKey = string.Empty,
                AppKey = string.Empty,
                AppSecret = string.Empty
            };
            return accountType;
        }

        #endregion 构造函数

        #region 需持久化属性

        /// <summary>
        ///第三方帐号类型标识
        /// </summary>
        public string AccountTypeKey { get; set; }

        /// <summary>
        /// 第三方帐号获取器实现类Type值（如：Spacebuilder.Group.GroupConfig,Spacebuilder.Group）
        /// </summary>
        public string ThirdAccountGetterClassType { get; set; }

        /// <summary>
        ///网站接入应用标识
        /// </summary>
        public string AppKey { get; set; }

        /// <summary>
        ///网站接入应用加密串
        /// </summary>
        public string AppSecret { get; set; }

        /// <summary>
        ///是否启用
        /// </summary>
        public bool IsEnabled { get; set; }

        #endregion 需持久化属性

        #region IEntity 成员

        object IEntity.EntityId { get { return this.AccountTypeKey; } }

        bool IEntity.IsDeletedInDatabase { get; set; }

        #endregion IEntity 成员
    }
}