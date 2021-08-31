#if NETCOREAPP2_0 || NETCOREAPP2_1 || NETCOREAPP2_2 || NETCOREAPP3_0 || NETCOREAPP3_1 || NET5_0 || NETSTANDARD2_0 || NETSTANDARD2_1
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Utility.Demo.Domain.Entities;
using Utility.Ef.EntityMappings;
#elif NET20 || NET30 || NET35 || NET40 || NET45 || NET451 || NET452 || NET46 || NET461 || NET462 || NET47 || NET471 || NET472 || NET48
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration;
#endif

namespace Utility.Demo.Ef.EntityMappings
{
#if NETCOREAPP2_0 || NETCOREAPP2_1 || NETCOREAPP2_2 || NETCOREAPP3_0 || NETCOREAPP3_1 || NET5_0 || NETSTANDARD2_0 || NETSTANDARD2_1
    /// <summary>SourceMaterialEntity  </summary>
    public  class SourceMaterialMap : BaseEfMapp<SourceMaterialEntity>
    {
        public SourceMaterialMap()
        {
            this.TableName = "t_source_material";
         }

        protected override void Set(EntityTypeBuilder<SourceMaterialEntity> builder)
        {
            builder.HasKey(it => it.Id);//Id
            builder.Property(it => it.Id).HasColumnName("id");//Id

            builder.Property(it => it.Src).HasColumnName("src").HasMaxLength(255);//Src

            builder.Property(it => it.Key).HasColumnName("key").HasMaxLength(255);//Key

            builder.Property(it => it.Buffer).HasColumnName("buffer");//Buffer

            builder.Property(it => it.Base64).HasColumnName("base64").HasMaxLength(255);//Base64

            builder.Property(it => it.Description).HasColumnName("description").HasMaxLength(255);//Description

            builder.Property(it => it.Buket).HasColumnName("buket").HasMaxLength(255);//Buket

            builder.Property(it => it.ObjectName).HasColumnName("object_name").HasMaxLength(255);//ObjectName

        }
    }

#elif NET20 || NET30 || NET35 || NET40 || NET45 || NET451 || NET452 || NET46 || NET461 || NET462|| NET47 || NET471 || NET472|| NET48 
    /// <summary>SourceMaterialEntity  </summary>
    public  class SourceMaterialMap : BaseEfMapp<SourceMaterialEntity>
    {
        public SourceMaterialMap(): base("t_source_material")
        {
         }

        protected override void Set()
        {
            Property(it => it.Id).HasColumnName("id");//Id

            Property(it => it.Src).HasColumnName("src").HasMaxLength(255);//Src

            Property(it => it.Key).HasColumnName("key").HasMaxLength(255);//Key

            Property(it => it.Buffer).HasColumnName("buffer");//Buffer

            Property(it => it.Base64).HasColumnName("base64").HasMaxLength(255);//Base64

            Property(it => it.Description).HasColumnName("description").HasMaxLength(255);//Description

            Property(it => it.Buket).HasColumnName("buket").HasMaxLength(255);//Buket

            Property(it => it.ObjectName).HasColumnName("object_name").HasMaxLength(255);//ObjectName

        }
    }

#endif
}
