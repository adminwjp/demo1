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
    /// 帐号绑定实体类
    /// </summary>
    [TableName("tn_AccountBindings")]
    [PrimaryKey("Id", autoIncrement = true)]
    [CacheSetting(true, PropertyNamesOfArea = "UserId")]
    [Serializable]
    public class AccountBinding : IEntity
    {
        #region 构造函数

        /// <summary>
        /// 创建帐号绑定
        /// </summary>
        public static AccountBinding New()
        {
            AccountBinding accountBinding = new AccountBinding()
            {
                AccountTypeKey = string.Empty,
                Identification = string.Empty,
                AccessToken = string.Empty
            };
            return accountBinding;
        }

        #endregion 构造函数

        #region 需持久化属性

        /// <summary>
        ///主键标识
        /// </summary>
        public long Id { get; set; }

        /// <summary>
        ///用户Id
        /// </summary>
        public long UserId { get; set; }

        /// <summary>
        ///第三方帐号类型
        /// </summary>
        public string AccountTypeKey { get; set; }

        /// <summary>
        ///第三方帐号标识
        /// </summary>
        public string Identification { get; set; }

        /// <summary>
        ///oauth授权凭证加密串
        /// </summary>
        public string AccessToken { get; set; }

        /// <summary>
        /// 过期时间
        /// </summary>
        public DateTime ExpiredDate { get; set; }

        #endregion 需持久化属性

        #region IEntity 成员

        object IEntity.EntityId { get { return this.Id; } }

        bool IEntity.IsDeletedInDatabase { get; set; }

        #endregion IEntity 成员
    }
}