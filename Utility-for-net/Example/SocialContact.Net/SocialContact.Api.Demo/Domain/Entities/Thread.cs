//------------------------------------------------------------------------------
// <copyright company="Tunynet">
//     Copyright (c) Tunynet Inc.  All rights reserved.
// </copyright>
//------------------------------------------------------------------------------


using System;
using System.Collections.Generic;
using System.Linq;
using Tunynet.Common;

namespace Tunynet.Post
{
    /// <summary>
    /// 贴子
    /// </summary>
    [Serializable]
    public class Thread 
    {

        #region 需持久化属性

        /// <summary>
        ///ThreadId
        /// </summary>
        public long ThreadId { get; set; }

        /// <summary>
        ///所属贴吧Id
        /// </summary>
        public long SectionId { get; set; }

        /// <summary>
        /// 租户类型Id
        /// </summary>
        public string TenantTypeId { get; set; }

        /// <summary>
        ///所属贴吧拥有者Id（例如：群组Id）
        /// </summary>
        public long OwnerId { get; set; }

        /// <summary>
        ///主题作者用户Id
        /// </summary>
        public long UserId { get; set; }

        /// <summary>
        ///主题作者
        /// </summary>
        public string Author { get; set; }

        /// <summary>
        ///贴子标题
        /// </summary>
        public string Subject { get; set; }

        /// <summary>
        ///贴子内容
        /// </summary>
        public string Body { get; set; }

        /// <summary>
        ///是否锁定
        /// </summary>
        public bool IsLocked { get; set; }

        /// <summary>
        ///是否置顶
        /// </summary>
        public bool IsSticky { get; set; }

        /// <summary>
        ///审批状态
        /// </summary>
        public AuditStatus ApprovalStatus { get; set; }

        /// <summary>
        ///发贴人IP
        /// </summary>
        public string IP { get; set; }

        /// <summary>
        ///创建时间
        /// </summary>
        public DateTime DateCreated { get; set; }

        /// <summary>
        ///最后更新日期（被回复时也需要更新时间）
        /// </summary>
        public DateTime LastModified { get; set; }

        /// <summary>
        /// 贴子类型
        /// </summary>
        public ThreadType ThreadType { get; set; }

        /// <summary>
        /// 关联 Id(活动,投票)
        /// </summary>
        public long AssociateId { get; set; }

        #endregion 需持久化属性

       
    }
}