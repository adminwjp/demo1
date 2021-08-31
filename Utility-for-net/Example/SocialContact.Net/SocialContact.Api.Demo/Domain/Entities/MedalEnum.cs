//------------------------------------------------------------------------------
// <copyright company="Tunynet">
//     Copyright (c) Tunynet Inc.  All rights reserved.
// </copyright>
//------------------------------------------------------------------------------

using System.ComponentModel.DataAnnotations;

namespace Tunynet.Common
{
    /// <summary>
    /// 授予状态（可以授予、停止授予）
    /// </summary>
    public enum AwardStatus
    {
        /// <summary>
        /// 可以授予
        /// </summary>
        [Display(Name = "可以授予")]
        AllowAward = 1,

        /// <summary>
        /// 停止授予
        /// </summary>
        [Display(Name = "停止授予")]
        StopAward = 2
    }

    /// <summary>
    /// 授予方式（自主申请、人工授予）
    /// </summary>
    public enum AwardType
    {
        /// <summary>
        /// 自主申请
        /// </summary>
        [Display(Name = "自主申请")]
        AwardBySelf = 1,

        /// <summary>
        /// 人工授予
        /// </summary>
        [Display(Name = "人工授予")]
        AwardByOther = 2
    }

    /// <summary>
    /// 用户授予状态（已授予、已收回、已拒绝、申请中、已放弃）
    /// </summary>
    public enum UserAwardStatus
    {
        /// <summary>
        /// 已授予
        /// </summary>
        [Display(Name = "已授予")]
        AlreadyAward = 1,

        /// <summary>
        /// 已收回
        /// </summary>
        [Display(Name = "已收回")]
        Recovered = 2,

        /// <summary>
        /// 已拒绝
        /// </summary>
        [Display(Name = "已拒绝")]
        Refused = 3,

        /// <summary>
        /// 申请中
        /// </summary>
        [Display(Name = "申请中")]
        Applying = 4,

        /// <summary>
        /// 已放弃
        /// </summary>
        [Display(Name = "已放弃")]
        Abandoned = 5
    }
}