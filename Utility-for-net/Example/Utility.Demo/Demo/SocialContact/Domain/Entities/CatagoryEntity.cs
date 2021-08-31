using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utility.Demo.Domain.Entities;
using Utility.Domain.Entities;

namespace SocialContact.Domain.Entities
{
    /// <summary>
    /// 分类
    /// </summary>
    public class CatagoryEntity : Entity<long>
    {

        public virtual string Code { get; set; }
        /// <summary>
        /// 分类名称
        /// </summary>
        public virtual string Category { get; set; }

        /// <summary>
        /// 分类描述
        /// </summary>
        public virtual string Description { get; set; }
        /// <summary>
        /// 文件 接受协议
        /// </summary>
        public virtual string Accept { get; set; }

        /// <summary>
        /// 分类标识
        /// </summary>
        public virtual CatalogFlag Flag { get; set; }

        /// <summary>
        /// 父分类
        /// </summary>
        public virtual CatagoryEntity Parent { get; set; }

        /// <summary>
        /// 子分类集合
        /// </summary>
        public virtual ISet<CatagoryEntity> Children { get; set; }



        /// <summary>
        /// 父分类
        /// </summary>
        public virtual long? ParentId { get; set; }

        public virtual ISet<AdminEntity> Admins { get; set; }

        public virtual ISet<EdutionEntity> Edutions { get; set; }

        public virtual ISet<UserMenuEntity> UserMenus { get; set; }
        public virtual ISet<WorkEntity> Works { get; set; }
    }
}
