//------------------------------------------------------------------------------
// <copyright company="Tunynet">
//     Copyright (c) Tunynet Inc.  All rights reserved.
// </copyright>
//------------------------------------------------------------------------------

using Newtonsoft.Json;
using PetaPoco;
using System;
using System.Collections.Generic;
using Tunynet.Caching;

namespace Tunynet.Common
{
    /// <summary>
    /// 友情链接实体
    /// </summary>
    [TableName("tn_Links")]
    [PrimaryKey("LinkId", autoIncrement = true)]
    [CacheSetting(true, PropertyNamesOfArea = "OwnerId", ExpirationPolicy = EntityCacheExpirationPolicies.Stable)]
    [Serializable]
    public class LinkEntity : SerializablePropertiesBase, IEntity
    {
        /// <summary>
        /// 新建实体时使用
        /// </summary>
        public static LinkEntity New()
        {
            LinkEntity link = new LinkEntity()
            {
                Description = string.Empty,
                DateCreated = DateTime.Now
            };
            return link;
        }

        #region 需持久化属性

        /// <summary>
        ///友情链接ID
        /// </summary>
        public long LinkId { get; set; }

        /// <summary>
        ///链接名称
        /// </summary>
        public string LinkName { get; set; }

        /// <summary>
        ///链接地址
        /// </summary>
        public string LinkUrl { get; set; }

        /// <summary>
        ///图片附件 Id
        /// </summary>
        public long ImageAttachmentId { get; set; }

        /// <summary>
        ///链接说明
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        ///是否启用
        /// </summary>
        public bool IsEnabled { get; set; }

        /// <summary>
        ///排序，默认与主键相同
        /// </summary>
        public long DisplayOrder { get; set; }

        /// <summary>
        ///创建日期
        /// </summary>
        public DateTime DateCreated { get; set; }

        #endregion 需持久化属性

        #region 扩展属性及方法

        /// <summary>
        /// 链接类别
        /// </summary>
        [Ignore]
        [JsonIgnore]
        public IEnumerable<Category> Categories
        {
            get
            {
                return DIContainer.Resolve<CategoryService>().GetCategoriesOfItem(this.LinkId, 0, TenantTypeIds.Instance().Link());
            }
        }

        #endregion 扩展属性及方法

        #region IEntity 成员

        object IEntity.EntityId { get { return this.LinkId; } }

        bool IEntity.IsDeletedInDatabase { get; set; }

        #endregion IEntity 成员

        /// <summary>
        /// 获取标题图
        /// </summary>
        /// <returns></returns>
        public string GetImageUrl(string key)
        {
            AttachmentService attachmentService = new AttachmentService(TenantTypeIds.Instance().Link());
            var imageurl = string.Empty;
            var attachment = attachmentService.Get(this.ImageAttachmentId);
            if (attachment != null)
            {
                imageurl = attachment.GetDirectlyUrl(key);
            }
            return imageurl;
        }
    }
}