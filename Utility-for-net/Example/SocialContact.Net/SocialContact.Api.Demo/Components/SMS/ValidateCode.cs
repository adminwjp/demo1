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
    /// 验证码实体
    /// </summary>
    [TableName("jc_ValidateCodes")]
    [PrimaryKey("PhoneNum", autoIncrement = false)]
    [CacheSetting(false)]
    [Serializable]
    public class ValidateCode : IEntity
    {
        #region 需持久化属性

        /// <summary>
        ///用户Id
        /// </summary>
        public long PhoneNum { get; set; }

        /// <summary>
        ///验证码
        /// </summary>
        public string Code { get; set; }

        /// <summary>
        ///验证时间
        /// </summary>
        public DateTime? VerifyTime { get; set; }

        /// <summary>
        ///创建日期
        /// </summary>
        public DateTime DateCreated { get; set; }

        #endregion 需持久化属性

        #region IEntity 成员

        object IEntity.EntityId { get { return this.PhoneNum; } }

        bool IEntity.IsDeletedInDatabase { get; set; }

        #endregion IEntity 成员
    }
}