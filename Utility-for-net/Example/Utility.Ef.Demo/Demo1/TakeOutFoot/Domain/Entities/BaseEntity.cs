#if NET40 || NET45 || NET451 || NET452 || NET46 || NET461 || NET462 || NET47 || NET471 || NET472 || NET48 || NETCOREAPP2_0 || NETCOREAPP2_1 || NETCOREAPP2_2 || NETCOREAPP3_0 || NETCOREAPP3_1 || NET5_0 || NETSTANDARD2_0 || NETSTANDARD2_1

using MediatR;
//using NHibernate.Mapping.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Utility.Domain.Entities;

namespace TakeOutFoot.Domain.Entities
{
    /// <summary>
    /// 基类 实体
    /// </summary>
    public class BaseEntity:DomainEvent<long>,IEntity<long>
    {

        /// <summary>
        /// 创建 时间 datetime(6) mysql 5.5 不支持 ; datetime mysql 支持
        /// </summary>
        [System.ComponentModel.DataAnnotations.Schema.Column("creation_time"
#if MySql5_5
            , TypeName = "datetime"
#endif
            )]
        public virtual DateTime CreationTime { get; set; }

        /// <summary>
        /// 修改 时间
        /// </summary>
        [System.ComponentModel.DataAnnotations.Schema.Column("last_modification_time"
#if MySql
            , TypeName = "datetime"
#endif
            )]
        public virtual DateTime? LastModificationTime { get; set; }

        /// <summary>
        /// 删除时间
        /// </summary>
        [System.ComponentModel.DataAnnotations.Schema.Column("deletion_time"
#if MySql5_5
            , TypeName = "datetime"
#endif
            )]
        public virtual DateTime? DeletionTime { get; set; }

        /// <summary>
        /// 软删除 标识
        /// </summary>
        [System.ComponentModel.DataAnnotations.Schema.Column("is_deleted")]
        public virtual bool IsDeleted { get; set; }

        public virtual new bool IsTransient()
        {
            return true;
        }



    }
}
#endif