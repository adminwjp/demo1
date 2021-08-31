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
    /// <summary>CityEntity  </summary>
    public  class CityMap : BaseEfMapp<CityEntity>
    {
        public CityMap()
        {
            this.TableName = "t_city";
         }

        protected override void Set(EntityTypeBuilder<CityEntity> builder)
        {
            builder.Property(it => it.ParentId).HasColumnName("parent_id");//ParentId

            builder.HasOne(it => it.Parent).WithMany(it => it.Children).HasForeignKey("parent_id").HasConstraintName("fk_parent_id");//Parent

            builder.HasMany(it => it.Children).WithOne(it => it.Parent).HasForeignKey("parent_id").HasConstraintName("fk_parent_id");//Children


            builder.HasKey(it => it.Id);//Id
            builder.Property(it => it.Id).HasColumnName("id");//Id

            builder.Property(it => it.Prodive).HasColumnName("prodive").HasMaxLength(255);//Prodive

            builder.Property(it => it.City).HasColumnName("city").HasMaxLength(255);//City

            builder.Property(it => it.Area).HasColumnName("area").HasMaxLength(255);//Area

            builder.Property(it => it.ProdiveCode).HasColumnName("prodive_code").HasMaxLength(255);//ProdiveCode

            builder.Property(it => it.CityCode).HasColumnName("city_code").HasMaxLength(255);//CityCode

            builder.Property(it => it.AreaCode).HasColumnName("area_code").HasMaxLength(255);//AreaCode

        }
    }

#elif NET20 || NET30 || NET35 || NET40 || NET45 || NET451 || NET452 || NET46 || NET461 || NET462|| NET47 || NET471 || NET472|| NET48 
    /// <summary>CityEntity  </summary>
    public  class CityMap : BaseEfMapp<CityEntity>
    {
        public CityMap(): base("t_city")
        {
         }

        protected override void Set()
        {
            Property(it => it.ParentId).HasColumnName("parent_id");//ParentId

            Property(it => it.Parent).HasColumnName("parent");//Parent

            Property(it => it.Children).HasColumnName("children");//Children

            Property(it => it.Id).HasColumnName("id");//Id

            Property(it => it.Prodive).HasColumnName("prodive").HasMaxLength(255);//Prodive

            Property(it => it.City).HasColumnName("city").HasMaxLength(255);//City

            Property(it => it.Area).HasColumnName("area").HasMaxLength(255);//Area

            Property(it => it.ProdiveCode).HasColumnName("prodive_code").HasMaxLength(255);//ProdiveCode

            Property(it => it.CityCode).HasColumnName("city_code").HasMaxLength(255);//CityCode

            Property(it => it.AreaCode).HasColumnName("area_code").HasMaxLength(255);//AreaCode

        }
    }

#endif
}
