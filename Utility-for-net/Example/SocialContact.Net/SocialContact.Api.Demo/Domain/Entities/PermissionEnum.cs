//------------------------------------------------------------------------------
// <copyright company="Tunynet">
//     Copyright (c) Tunynet Inc.  All rights reserved.
// </copyright>
//------------------------------------------------------------------------------

using System.ComponentModel.DataAnnotations;

namespace Tunynet.Common
{
    /// <summary>
    /// 权限许可类型
    /// </summary>
    public enum OwnerType
    {
        /// <summary>
        /// 用户
        /// </summary>
        [Display(Name = "用户")]
        User = 1,

        /// <summary>
        /// 角色
        /// </summary>
        [Display(Name = "角色")]
        Role = 11
    }

    /// <summary>
    /// 权限许可类型
    /// </summary>
    public enum PermissionType
    {
        /// <summary>
        /// 未设置
        /// </summary>
        [Display(Name = "未设置")]
        NotSet = 0,

        /// <summary>
        /// 允许
        /// </summary>
        [Display(Name = "允许")]
        Allow = 1,

        /// <summary>
        /// 拒绝
        /// </summary>
        [Display(Name = "拒绝")]
        Refuse = 2
    }
}