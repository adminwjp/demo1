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
    /// <summary>BankEntity  </summary>
    public  class BankMap : BaseEfMapp<BankEntity>
    {
        public BankMap()
        {
            this.TableName = "t_bank";
         }

        protected override void Set(EntityTypeBuilder<BankEntity> builder)
        {
            builder.HasKey(it => it.Id);//Id
            builder.Property(it => it.Id).HasColumnName("id");//Id

            builder.Property(it => it.BankId).HasColumnName("bank_id").HasMaxLength(255);//BankId

            builder.Property(it => it.BankName).HasColumnName("bank_name").HasMaxLength(255);//BankName

            builder.Property(it => it.BankPhoto1).HasColumnName("bank_photo1").HasMaxLength(255);//BankPhoto1

            builder.Property(it => it.BankPhoto2).HasColumnName("bank_photo2").HasMaxLength(255);//BankPhoto2

            builder.Property(it => it.BankPhotoId1).HasColumnName("bank_photo_id1");//BankPhotoId1

            builder.Property(it => it.BankPhotoId2).HasColumnName("bank_photo_id2");//BankPhotoId2

            builder.Property(it => it.BankAddress).HasColumnName("bank_address").HasMaxLength(255);//BankAddress

            builder.Property(it => it.BankUserName).HasColumnName("bank_user_name").HasMaxLength(255);//BankUserName

            builder.Property(it => it.BankUserAddress).HasColumnName("bank_user_address").HasMaxLength(255);//BankUserAddress

            builder.Property(it => it.IsDefault).HasColumnName("is_default");//IsDefault

            builder.Property(it => it.UserId).HasColumnName("user_id");//UserId

        }
    }

#elif NET20 || NET30 || NET35 || NET40 || NET45 || NET451 || NET452 || NET46 || NET461 || NET462|| NET47 || NET471 || NET472|| NET48 
    /// <summary>BankEntity  </summary>
    public  class BankMap : BaseEfMapp<BankEntity>
    {
        public BankMap(): base("t_bank")
        {
         }

        protected override void Set()
        {
            Property(it => it.Id).HasColumnName("id");//Id

            Property(it => it.BankId).HasColumnName("bank_id").HasMaxLength(255);//BankId

            Property(it => it.BankName).HasColumnName("bank_name").HasMaxLength(255);//BankName

            Property(it => it.BankPhoto1).HasColumnName("bank_photo1").HasMaxLength(255);//BankPhoto1

            Property(it => it.BankPhoto2).HasColumnName("bank_photo2").HasMaxLength(255);//BankPhoto2

            Property(it => it.BankPhotoId1).HasColumnName("bank_photo_id1");//BankPhotoId1

            Property(it => it.BankPhotoId2).HasColumnName("bank_photo_id2");//BankPhotoId2

            Property(it => it.BankAddress).HasColumnName("bank_address").HasMaxLength(255);//BankAddress

            Property(it => it.BankUserName).HasColumnName("bank_user_name").HasMaxLength(255);//BankUserName

            Property(it => it.BankUserAddress).HasColumnName("bank_user_address").HasMaxLength(255);//BankUserAddress

            Property(it => it.IsDefault).HasColumnName("is_default");//IsDefault

            Property(it => it.UserId).HasColumnName("user_id");//UserId

        }
    }

#endif
}
