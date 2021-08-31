//------------------------------------------------------------------------------
// <copyright company="Tunynet">
//     Copyright (c) Tunynet Inc.  All rights reserved.
// </copyright>
//------------------------------------------------------------------------------

using System;
using Tunynet.Common;

namespace Tunynet.Logging
{
    /// <summary>
    /// 操作日志实体
    /// </summary>
    [Serializable]
    public class OperationLog 
    {

        /// <summary>
        ///Id
        /// </summary>
        public long Id { get; set; }

        /// <summary>
        ///租户Id
        /// </summary>
        public string TenantTypeId { get; set; }

        /// <summary>
        ///操作类型标识
        /// </summary>
        public string OperationType { get; set; }

        /// <summary>
        ///操作对象名称
        /// </summary>
        public string OperationObjectName { get; set; }

        /// <summary>
        ///OperationObjectId
        /// </summary>
        public long OperationObjectId { get; set; }

        /// <summary>
        ///操作描述
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        ///操作者UserId
        /// </summary>
        public long OperationUserId { get; set; }

        /// <summary>
        ///操作者角色
        /// </summary>
        public string OperationUserRole { get; set; }

        /// <summary>
        ///操作者名称
        /// </summary>
        public string Operator { get; set; }

        /// <summary>
        ///操作者IP
        /// </summary>
        public string OperatorIP { get; set; }

        /// <summary>
        ///操作访问的url
        /// </summary>
        public string AccessUrl { get; set; }

        /// <summary>
        ///创建日期
        /// </summary>
        public DateTime DateCreated { get; set; }



     
    }
}