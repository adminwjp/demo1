//------------------------------------------------------------------------------
// <copyright company="Tunynet">
//     Copyright (c) Tunynet Inc.  All rights reserved.
// </copyright>
//------------------------------------------------------------------------------

using System;
namespace Tunynet.Common
{
    /// <summary>
    /// 租户数据实体类
    /// </summary>
    [Serializable]
    public class TenantType 
    {
        /// <summary>
        /// 租户类型Id
        /// </summary>
        public string TenantTypeId { get; set; }

        /// <summary>
        /// 租户类型名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 类型
        /// </summary>
        public string ClassType { get; set; }


    }
}