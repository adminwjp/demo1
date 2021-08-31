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
    /// <summary>UserLogEntity  </summary>
    public  class UserLogMap : BaseEfMapp<UserLogEntity>
    {
        public UserLogMap()
        {
            this.TableName = "t_user_log";
         }

        protected override void Set(EntityTypeBuilder<UserLogEntity> builder)
        {
            builder.HasKey(it => it.Id);//Id
            builder.Property(it => it.Id).HasColumnName("id");//Id

            builder.Property(it => it.UserId).HasColumnName("user_id");//UserId

            builder.Property(it => it.Account).HasColumnName("account").HasMaxLength(255);//Account

            builder.Property(it => it.Email).HasColumnName("email").HasMaxLength(255);//Email

            builder.Property(it => it.Phone).HasColumnName("phone").HasMaxLength(255);//Phone

            builder.Property(it => it.OldPwd).HasColumnName("old_pwd").HasMaxLength(255);//OldPwd

            builder.Property(it => it.NewPwd).HasColumnName("new_pwd").HasMaxLength(255);//NewPwd

            builder.Property(it => it.NewAccount).HasColumnName("new_account").HasMaxLength(255);//NewAccount

            builder.Property(it => it.NewEmail).HasColumnName("new_email").HasMaxLength(255);//NewEmail

            builder.Property(it => it.NewPhone).HasColumnName("new_phone").HasMaxLength(255);//NewPhone

            builder.Property(it => it.AddDate).HasColumnName("add_date");//AddDate

            builder.Property(it => it.Flag).HasColumnName("flag");//Flag
            
           
        }
    }

#elif NET20 || NET30 || NET35 || NET40 || NET45 || NET451 || NET452 || NET46 || NET461 || NET462|| NET47 || NET471 || NET472|| NET48 
    /// <summary>UserLogEntity  </summary>
    public  class UserLogMap : BaseEfMapp<UserLogEntity>
    {
        public UserLogMap(): base("t_user_log")
        {
         }

        protected override void Set()
        {
            Property(it => it.Id).HasColumnName("id");//Id

            Property(it => it.UserId).HasColumnName("user_id");//UserId

            Property(it => it.Account).HasColumnName("account").HasMaxLength(255);//Account

            Property(it => it.Email).HasColumnName("email").HasMaxLength(255);//Email

            Property(it => it.Phone).HasColumnName("phone").HasMaxLength(255);//Phone

            Property(it => it.OldPwd).HasColumnName("old_pwd").HasMaxLength(255);//OldPwd

            Property(it => it.NewPwd).HasColumnName("new_pwd").HasMaxLength(255);//NewPwd

            Property(it => it.NewAccount).HasColumnName("new_account").HasMaxLength(255);//NewAccount

            Property(it => it.NewEmail).HasColumnName("new_email").HasMaxLength(255);//NewEmail

            Property(it => it.NewPhone).HasColumnName("new_phone").HasMaxLength(255);//NewPhone

            Property(it => it.AddDate).HasColumnName("add_date");//AddDate

            Property(it => it.Flag).HasColumnName("flag");//Flag

        }
    }

#endif
}
