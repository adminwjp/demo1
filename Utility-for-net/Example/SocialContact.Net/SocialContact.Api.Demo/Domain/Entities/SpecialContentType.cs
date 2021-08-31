//------------------------------------------------------------------------------
// <copyright company="Tunynet">
//     Copyright (c) Tunynet Inc.  All rights reserved.
// </copyright>
//------------------------------------------------------------------------------


namespace Tunynet.Common
{
    /// <summary>
    /// 推荐的类别
    /// </summary>
    public class SpecialContentType
    {
        

        /// <summary>
        /// 类型ID（创建后不允许修改）
        /// </summary>
        public int TypeId { get; set; }

        /// <summary>
        /// 推荐类型名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 推荐类型描述
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// 租户ID
        /// </summary>
        public string TenantTypeId { get; set; }

        /// <summary>
        /// 是否需要截止日期
        /// </summary>
        public bool RequireExpiredDate { get; set; }

        /// <summary>
        /// 是否包含标题图
        /// </summary>
        public bool RequireFeaturedImage { get; set; }

        /// <summary>
        /// 是否允许添加外链
        /// </summary>
        public bool AllowExternalLink { get; set; }

        /// <summary>
        /// 是否系统内置
        /// </summary>
        public bool IsBuiltIn { get; set; }

        /// <summary>
        /// 标题图说明
        /// </summary>
        public string FeaturedImageDescrption { get; set; }

    }
}