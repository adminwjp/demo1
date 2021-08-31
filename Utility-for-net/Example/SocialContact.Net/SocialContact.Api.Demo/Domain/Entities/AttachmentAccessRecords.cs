//------------------------------------------------------------------------------
// <copyright company="Tunynet">
//     Copyright (c) Tunynet Inc.  All rights reserved.
// </copyright>
//------------------------------------------------------------------------------
using PetaPoco;
using System;
using Tunynet.Caching;
using Tunynet.Utilities;

namespace Tunynet.Common
{
    /// <summary>
    /// 附件下载记录
    /// </summary>
    [Serializable]
    public class AttachmentAccessRecords 
    {

        #region 需持久化属性

        /// <summary>
        ///Id
        /// </summary>
        public long Id { get; set; }

        /// <summary>
        ///附件Id
        /// </summary>
        public long AttachmentId { get; set; }

        /// <summary>
        ///1=下载；2=浏览
        /// </summary>
        public AccessType AccessType { get; set; }

        /// <summary>
        ///UserId
        /// </summary>
        public long UserId { get; set; }

        /// <summary>
        ///下载人DisplayName
        /// </summary>
        public string UserDisplayName { get; set; }

        /// <summary>
        ///消费的积分
        /// </summary>
        public int Price { get; set; }

        /// <summary>
        ///最近下载日期
        /// </summary>
        public DateTime LastDownloadDate { get; set; }

        /// <summary>
        ///下载日期
        /// </summary>
        public DateTime DownloadDate { get; set; }

        /// <summary>
        ///附件下载人IP
        /// </summary>
        public string IP { get; set; }

        #endregion 需持久化属性


    }
}