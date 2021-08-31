using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialContact.Domain.Entities
{
    
    /// <summary>
    /// 文章
    /// </summary>
    public class ArticleEntity: Entity
    {
        /// <summary>
        /// 用户id
        /// </summary>
        public virtual long UserId { get; set; }

        /// <summary>
        /// 用户名
        /// </summary>
        public virtual string Username { get; set; }

        /// <summary>
        /// 发布内容
        /// </summary>
        public virtual string Context { get; set; }

        /// <summary>
        /// 素材
        /// </summary>
        public virtual string Images { get; set; }

        /// <summary>
        /// 点赞数
        /// </summary>
        public virtual long Likes { get; set; }

        /// <summary>
        /// 评论数
        /// </summary>
        public virtual long CommentNum { get; set; }

        /// <summary>
        /// 分类id
        /// </summary>
        public virtual long CatalogId { get; set; }

        /// <summary>
        /// 分类
        /// </summary>
        public virtual CatalogEntity Catalog { get; set; }

        /// <summary>
        /// 评论
        /// </summary>
        public virtual List<CommentEntity> Comments { get; set; }
    }
}
