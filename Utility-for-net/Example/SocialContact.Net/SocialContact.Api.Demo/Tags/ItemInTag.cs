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
    /// 标签与内容的关联项实体
    /// </summary>
    [TableName("tn_ItemsInTags")]
    [PrimaryKey("Id", autoIncrement = true)]
    [CacheSetting(true, PropertyNamesOfArea = "ItemId,TagName")]
    [Serializable]
    public class ItemInTag : IEntity
    {
        /// <summary>
        /// 新建实体时使用
        /// </summary>
        public static ItemInTag New()
        {
            ItemInTag tagInTag = new ItemInTag();
            return tagInTag;
        }

        #region 需持久化属性

        /// <summary>
        ///Id
        /// </summary>
        public long Id { get; set; }

        /// <summary>
        /// 标签名称
        /// </summary>
        public string TagName { get; set; }

        /// <summary>
        ///内容项Id
        /// </summary>
        public long ItemId { get; set; }

        /// <summary>
        /// 租户类型Id
        /// </summary>
        public string TenantTypeId { get; set; }

        #endregion 需持久化属性

        #region 扩展属性

        /// <summary>
        /// 标签
        /// </summary>
        public Tag Tag
        {
            get
            {
                TagService tagService = DIContainer.Resolve<TagService>();
                return tagService.Get(this.TagName);
            }
        }

        #endregion

        #region IEntity 成员

        object IEntity.EntityId { get { return this.Id; } }

        bool IEntity.IsDeletedInDatabase { get; set; }

        #endregion IEntity 成员
    }
}