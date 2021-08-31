//------------------------------------------------------------------------------
// <copyright company="Tunynet">
//     Copyright (c) Tunynet Inc.  All rights reserved.
// </copyright>
//------------------------------------------------------------------------------

using System;

namespace Tunynet.Common
{
    /// <summary>
    /// 短网址实体
    /// </summary>
    [Serializable]
    public class ParsedMedia 
    {
        
        #region 需持久化属性

        /// <summary>
        ///Url别名
        /// </summary>
        public string Alias { get; set; }

        /// <summary>
        ///网址
        /// </summary>
        public string Url { get; set; }

        /// <summary>
        ///多媒体类型
        /// </summary>
        public MediaType MediaType { get; set; }

        /// <summary>
        ///多媒体名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        ///描述
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        ///缩略图地址
        /// </summary>
        public string ThumbnailUrl { get; set; }

        /// <summary>
        ///播放器地址
        /// </summary>
        public string PlayerUrl { get; set; }

        /// <summary>
        ///源文件地址
        /// </summary>
        public string SourceFileUrl { get; set; }

        /// <summary>
        ///创建时间
        /// </summary>
        public DateTime DateCreated { get; set; }

        #endregion 需持久化属性

    }
}