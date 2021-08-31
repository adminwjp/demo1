//------------------------------------------------------------------------------
// <copyright company="Tunynet">
//     Copyright (c) Tunynet Inc.  All rights reserved.
// </copyright>
//------------------------------------------------------------------------------


using System;
using System.Collections.Generic;


namespace Tunynet.Common
{
    /// <summary>广告实体</summary>
    [Serializable]
    public class Advertising 
    {
        /// <summary>广告Id </summary>
        public long AdvertisingId { get; set; }

        /// <summary>广告名称</summary>
        public string Name { get; set; }

        /// <summary>呈现方式 </summary>
        public AdvertisingType AdvertisingType { get; set; }

        /// <summary>广告内容</summary>
        public string Body { get; set; }

        /// <summary>图片附件Id </summary>
        public long ImageAttachmentId { get; set; }

        /// <summary>广告链接地址</summary>
        public string LinkUrl { get; set; }

        /// <summary>是否启用</summary>
        public bool IsEnable { get; set; }

        /// <summary>排序 </summary>
        public long DisplayOrder { get; set; }

        /// <summary>是否新开窗口 </summary>
        public bool TargetBlank { get; set; }

        /// <summary>开始时间 </summary>
        public long StartDate { get; set; }

        /// <summary>结束时间</summary>
        public long EndDate { get; set; }

        /// <summary>创建日期 </summary>
        public long DateCreated { get; set; }



        /// <summary>描述</summary>
        public string Description { get; set; }

   

        /// <summary>宽度 </summary>
        public int Width { get; set; }

        /// <summary>高度 </summary>
        public int Height { get; set; }



        /// <summary>是否锁定</summary>
        public bool IsLocked { get; set; }

    }
}