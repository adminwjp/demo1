#if   NET40 ||NET45 || NET451 || NET452 || NET46 || NET461 || NET462 || NET47 || NET471 || NET472 || NET48
using Config.Domain.Entities;


namespace Config.Ef.EntityMappings
{
    /// <summary>ef 基类 </summary>
    public abstract class BaseEfMapp<T> :  Utility.Ef.EntityMappings.BaseEfMapp<T, string> where T : BaseEntity
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="tableName"></param>
        public BaseEfMapp(string tableName):base(tableName)
        {

            Property(it => it.Name).HasMaxLength(BaseEntity.NameMaxLength);//名称

            Property(it => it.Ip).HasMaxLength(BaseEntity.IpMaxLength);//ip地址

            Property(it => it.Port);//端口

            Property(it => it.Description).HasMaxLength(BaseEntity.DescriptionMaxLength);//描述

            var create = Property(it => it.CreateDate);//创建时间
            if(ConfigHelper.DbFlag== DbFlag.MySql)
            {
                create.HasColumnType("datetime");
            }
            var update = Property(it => it.LastDate);//修改时间
            if (ConfigHelper.DbFlag == DbFlag.MySql)
            {
                update.HasColumnType("datetime");
            }
        }
       
    }
}
#endif

#if NETCOREAPP2_0 || NETCOREAPP2_1 || NETCOREAPP2_2 || NETCOREAPP3_0 || NETCOREAPP3_1 || NET5_0 || NETSTANDARD2_0 || NETSTANDARD2_1

using Config.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Utility;

namespace Config.Ef.EntityMappings
{
    /// <summary>ef 基类 </summary>
    public abstract class BasEfMapp<T> : Utility.Ef.EntityMappings.BaseEfMapp<T,string> where T : BaseEntity
    {
        /// <summary>
        /// base class mapping
        /// </summary>
        /// <param name="builder"></param>
        public override void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<T> builder)
        {

            builder.Property(it => it.Name).HasMaxLength(BaseEntity.NameMaxLength);//名称

            builder.Property(it => it.Ip).HasMaxLength(BaseEntity.IpMaxLength);//ip地址

            builder.Property(it => it.Port);//端口

            builder.Property(it => it.Description).HasMaxLength(BaseEntity.DescriptionMaxLength);//描述

            var create = builder.Property(it => it.CreateDate);//创建时间
            if(ConfigHelper.DbFlag== DbFlag.MySql)
            {
                create.HasColumnType("datetime");
            }
            var update = builder.Property(it => it.LastDate);//修改时间
            if (ConfigHelper.DbFlag == DbFlag.MySql)
            {
                update.HasColumnType("datetime");
            }
        }
    }
}
#endif
