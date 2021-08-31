using System;

namespace Adverts
{
    public class BaseEntity
    {
        /// <summary>
        /// 主键 Specified key was too long; max key length is 767 bytes
        /// </summary>
        public virtual string Id { get; set; }
        /// <summary>
        /// 创建 时间 datetime(6) mysql 不支持 ; datetime mysql 支持
        /// </summary>
        public virtual DateTime CreationTime { get; set; }
        /// <summary>
        /// 修改 时间
        /// </summary>
        public virtual DateTime? LastModificationTime { get; set; }
        /// <summary>
        /// 删除时间
        /// </summary>
        public virtual DateTime? DeletionTime { get; set; }
        /// <summary>
        /// 软删除 标识
        /// </summary>
        public virtual bool IsDeleted { get; set; }

        public virtual bool IsTransient()
        {
            return true;
        }
    }
}
