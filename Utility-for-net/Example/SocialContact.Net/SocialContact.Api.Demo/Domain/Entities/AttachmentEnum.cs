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
    public enum MediaType
    {

        /// <summary>
        /// 图片
        /// </summary>
        [Display(Name = "图片")]
        Image = 1,

        /// <summary>
        /// 视频
        /// </summary>
        [Display(Name = "视频")]
        Video = 2,

        /// <summary>
        /// Flash
        /// </summary>
        [Display(Name = "Flash")]
        Flash = 3,

        /// <summary>
        /// 音乐
        /// </summary>
        [Display(Name = "音乐")]
        Audio = 4,

        /// <summary>
        /// 文档
        /// </summary>
        [Display(Name = "文档")]
        Document = 5,

        /// <summary>
        /// 压缩包
        /// </summary>
        [Display(Name = "压缩包")]
        Compressed = 6,

        /// <summary>
        /// 其他类型
        /// </summary>
        [Display(Name = "其他类型")]
        Other = 99

    }

    /// <summary>
    /// 附件记录访问类型
    /// </summary>
    public enum AccessType
    {

        /// <summary>
        /// 下载附件
        /// </summary>
        [Display(Name = "下载附件")]
        Download = 1,

        /// <summary>
        /// 浏览附件
        /// </summary>
        [Display(Name = "浏览附件")]
        Read = 2

    }

    /// <summary>
    /// 图片尺寸大小
    /// </summary>
    public enum IconSize
    {

        /// <summary>
        /// 小尺寸
        /// </summary>
        [Display(Name = "小尺寸")]
        Small = 10,

        /// <summary>
        /// 中等尺寸
        /// </summary>
        [Display(Name = "中等尺寸")]
        Medium = 20,

        /// <summary>
        /// 大尺寸
        /// </summary>
        [Display(Name = "大尺寸")]
        Big = 30,

    }

    /// <summary>
    /// 文档转换状态
    /// </summary>
    public enum ConvertStatus
    {

        /// <summary>
        /// 等待转换
        /// </summary>
        [Display(Name = "等待转换")]
        Waiting = 0,

        /// <summary>
        /// 转换中
        /// </summary>
        [Display(Name = "转换中")]
        Converting = 1,

        /// <summary>
        /// 已转换
        /// </summary>
        [Display(Name = "已转换")]
        Complete = 2,

        /// <summary>
        /// 转换失败
        /// </summary>
        [Display(Name = "转换失败")]
        Fail = 10,
        /// <summary>
        /// 不可转换
        /// </summary>
        [Display(Name = "不可转换")]
        Other = 254,
        /// <summary>
        /// 全部
        /// </summary>
        [Display(Name = "全部")]
        All = 255,

    }

    /// <summary>
    /// 附件展示位置 
    /// </summary>
    public enum AttachmentPosition
    {

        /// <summary>
        /// 未设置
        /// </summary>
        NotSet = 0,

        /// <summary>
        /// 特别的附件 (例如:标题图)
        /// </summary>
        Featured = 10,

        /// <summary>
        /// 封面图 (例如:视频封面图)
        /// </summary>
        Cover = 11,

        /// <summary>
        /// 主内容区域 (例如:Html编辑器正文中的图片)
        /// </summary>
        Body = 20,

        /// <summary>
        /// 附件列表
        /// </summary>
        AttachmentList = 30

    }
}