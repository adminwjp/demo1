#if NET45 || NET451 || NET452 || NET46 || NET461 || NET462 || NET47 || NET471 || NET472 || NET48 || NETCOREAPP2_0 || NETCOREAPP2_1 || NETCOREAPP2_2 || NETCOREAPP3_0 || NETCOREAPP3_1 || NET5_0 ||  NET6_0  || NETSTANDARD2_0 || NETSTANDARD2_1

using System.ComponentModel.DataAnnotations.Schema;
using Utility.Domain.Entities;

namespace Product.Domain.Entities
{
    /// <summary>
    /// 基类 实体
    /// </summary>
    public class BaseEntity:DomainEvent<long>,
        IEntity<long>,IHasCreationTime,IHasModificationTime,IHasDeletionTime,IDomainEvent
    {
       
        /// <summary>
        /// 创建 时间 datetime(6) mysql 不支持 ; datetime mysql 支持
        /// </summary>
        [System.ComponentModel.DataAnnotations.Schema.Column("creation_time")]
        public virtual long CreationTime { get; set; }
        /// <summary>
        /// 修改 时间
        /// </summary>
        [System.ComponentModel.DataAnnotations.Schema.Column("last_modification_time" )]
        public virtual long LastModificationTime { get; set; }
        /// <summary>
        /// 删除时间
        /// </summary>
        [System.ComponentModel.DataAnnotations.Schema.Column("deletion_time")]
        public virtual long DeletionTime { get; set; }
        /// <summary>
        /// 软删除 标识
        /// </summary>
        [Column("is_deleted")]
        public virtual bool IsDeleted { get; set; }
      
        public virtual void Update(BaseEntity baseEntity)
        {
            this.Id = baseEntity.Id;
            this.CreationTime = baseEntity.CreationTime;
            this.LastModificationTime = baseEntity.LastModificationTime;
            this.DeletionTime = baseEntity.DeletionTime;
            this.IsDeleted = baseEntity.IsDeleted;
        }
    
        public virtual new bool IsTransient()
        {
            return this.Id == 0;
        }

    }
}
#endif