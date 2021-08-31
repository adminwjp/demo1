//------------------------------------------------------------------------------
// <copyright company="Tunynet">
//     Copyright (c) Tunynet Inc.  All rights reserved.
// </copyright>
//------------------------------------------------------------------------------

using PetaPoco;
using System;
using Tunynet.Caching;

namespace Tunynet.Common
{
    /// <summary>
    /// 短网址实体
    /// </summary>
    [Serializable]
    public class ShortUrlEntity 
    {
        /// <summary>
        /// Url别名
        /// </summary>
        public string Alias { get; set; }

        /// <summary>
        /// 实际的Url地址
        /// </summary>
        public string Url { get; set; }

        /// <summary>
        /// 第三方服务处理后的短网址
        /// </summary>
        public string OtherShortUrl { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime DateCreated { get; set; }

    }
}