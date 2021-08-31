//------------------------------------------------------------------------------
// <copyright company="Tunynet">
//     Copyright (c) Tunynet Inc.  All rights reserved.
// </copyright>
//------------------------------------------------------------------------------


using System;

namespace Tunynet.Common
{
    /// <summary>计数实体</summary>
    [Serializable]
    public class CountEntity 
    {
        #region 需持久化属性

        /// <summary>
        ///id
        /// </summary>
        public long CountId { get; set; }

        /// <summary>
        ///拥有者id
        /// </summary>
        public long OwnerId { get; set; }

        /// <summary>
        ///租户类型Id
        /// </summary>
        public string TenantTypeId { get; set; }

        /// <summary>
        ///计数对象id
        /// </summary>
        public long ObjectId { get; set; }

        /// <summary>
        ///计数类型
        /// </summary>
        public string CountType { get; set; }

        /// <summary>
        ///计数
        /// </summary>
        public int StatisticsCount { get; set; }

        #endregion 需持久化属性


    }
}