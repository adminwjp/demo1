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
    /// 推荐的内容
    /// </summary>
    [TableName("tn_SpecialContentItems")]
    [PrimaryKey("Id", autoIncrement = true)]
    [CacheSetting(true, PropertyNamesOfArea = "TypeId")]
    [Serializable]
    public class SpecialContentItem : SerializablePropertiesBase, IEntity
    {
        /// <summary>
        /// 推荐id 自增长
        /// </summary>
        public long Id { get; set; }

        /// <summary>
        /// 推荐类别Id
        /// </summary>
        public int TypeId { get; set; }

        /// <summary>
        /// 租户类型ID
        /// </summary>
        public string TenantTypeId { get; set; }

        /// <summary>
        /// 推荐内容所在区域Id（可能是版块、栏目也可能是自定义的数字）
        /// </summary>
        public long RegionId { get; set; }

        /// <summary>
        /// 内容实体ID
        /// </summary>
        public long ItemId { get; set; }

        /// <summary>
        /// 推荐标题（默认为内容名称或标题，允许推荐人修改）
        /// </summary>
        public string ItemName { get; set; }

        /// <summary>
        /// 标题图ID
        /// </summary>
        public long FeaturedImageAttachmentId { get; set; }

        /// <summary>
        /// 推荐人 DisplayName
        /// </summary>
        public string Recommender { get; set; }

        /// <summary>
        /// 推荐人用户 Id
        /// </summary>
        public long RecommenderUserId { get; set; }

        /// <summary>
        /// 推荐日期
        /// </summary>
        public DateTime DateCreated { get; set; }

        /// <summary>
        /// 截止期限
        /// </summary>
        public DateTime ExpiredDate { get; set; }

        /// <summary>
        /// 排序顺序（默认和Id一致）
        /// </summary>
        public long DisplayOrder { get; set; }

    }
}