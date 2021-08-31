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
    /// <summary>AddressEntity  </summary>
    public  class AddressMap : BaseEfMapp<AddressEntity>
    {
        public AddressMap()
        {
            this.TableName = "t_address";
         }

        protected override void Set(EntityTypeBuilder<AddressEntity> builder)
        {
            builder.HasKey(it => it.Id);//Id
            builder.Property(it => it.Id).HasColumnName("id");//Id
            builder.Property(it => it.ContactName).HasColumnName("contact_name").HasMaxLength(255);//ContactName

            builder.Property(it => it.Address).HasColumnName("address").HasMaxLength(255);//Address

            builder.Property(it => it.Area).HasColumnName("area").HasMaxLength(255);//Area

            builder.Property(it => it.City).HasColumnName("city").HasMaxLength(255);//City

            builder.Property(it => it.Province).HasColumnName("province").HasMaxLength(255);//Province

            builder.Property(it => it.Country).HasColumnName("country").HasMaxLength(255);//Country

            builder.Property(it => it.Memo).HasColumnName("memo").HasMaxLength(255);//Memo

            builder.Property(it => it.Phone).HasColumnName("phone").HasMaxLength(255);//Phone

            builder.Property(it => it.PostCode).HasColumnName("post_code").HasMaxLength(255);//PostCode

            builder.Property(it => it.IsDefault).HasColumnName("is_default");//IsDefault

            builder.Property(it => it.UserId).HasColumnName("user_id");//UserId

        }
    }

#elif NET20 || NET30 || NET35 || NET40 || NET45 || NET451 || NET452 || NET46 || NET461 || NET462|| NET47 || NET471 || NET472|| NET48 
    /// <summary>AddressEntity  </summary>
    public  class AddressMap : BaseEfMapp<AddressEntity>
    {
        public AddressMap(): base("t_address")
        {
         }

        protected override void Set()
        {
            Property(it => it.Id).HasColumnName("id");//Id

            Property(it => it.ContactName).HasColumnName("contact_name").HasMaxLength(255);//ContactName

            Property(it => it.Address).HasColumnName("address").HasMaxLength(255);//Address

            Property(it => it.Area).HasColumnName("area").HasMaxLength(255);//Area

            Property(it => it.City).HasColumnName("city").HasMaxLength(255);//City

            Property(it => it.Province).HasColumnName("province").HasMaxLength(255);//Province

            Property(it => it.Country).HasColumnName("country").HasMaxLength(255);//Country

            Property(it => it.Memo).HasColumnName("memo").HasMaxLength(255);//Memo

            Property(it => it.Phone).HasColumnName("phone").HasMaxLength(255);//Phone

            Property(it => it.PostCode).HasColumnName("post_code").HasMaxLength(255);//PostCode

            Property(it => it.IsDefault).HasColumnName("is_default");//IsDefault

            Property(it => it.UserId).HasColumnName("user_id");//UserId

        }
    }

#endif
}
