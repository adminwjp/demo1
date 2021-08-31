//------------------------------------------------------------------------------
// <copyright company="Tunynet">
//     Copyright (c) Tunynet Inc.  All rights reserved.
// </copyright>
//------------------------------------------------------------------------------

using System.ComponentModel.DataAnnotations;

namespace Tunynet.Common
{
    /// <summary>
    /// 附件媒体类型
    /// </summary>
    public enum AdvertisingType
    {
        /// <summary>
        /// 代码
        /// </summary>
        [Display(Name = "代码")]
        Script = 0,

        /// <summary>
        /// 文字
        /// </summary>
        [Display(Name = "文字")]
        Text = 1,

        /// <summary>
        /// 图片
        /// </summary>
        [Display(Name = "图片")]
        Image = 2,

        /// <summary>
        /// Flash
        /// </summary>
        [Display(Name = "Flash")]
        Flash = 3
    }

    /// <summary>
    /// 广告状态
    /// </summary>
    public enum AdvertisingStatus
    {
        /// <summary>
        /// 未启用
        /// </summary>
        [Display(Name = "未启用")]
        Disabled = 0,

        /// <summary>
        /// 未投放
        /// </summary>
        [Display(Name = "未投放")]
        NotServing = 1,

        /// <summary>
        /// 已投放
        /// </summary>
        [Display(Name = "已投放")]
        Serving = 2,

        /// <summary>
        /// 已过期
        /// </summary>
        [Display(Name = "已过期")]
        OutOfDate = 3,

        /// <summary>
        /// 已启用
        /// </summary>
        [Display(Name = "已启用")]
        Enabled = 4
    }
}