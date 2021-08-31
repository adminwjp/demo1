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
    public class AdminMap: BaseUserMap<AdminEntity,long>
    {
        public AdminMap()
        {
            this.TableName = "t_admin";
        }
        public override void Configure(EntityTypeBuilder<AdminEntity> builder)
        {
            base.Configure(builder);
            builder.Property(it => it.RoleId).HasColumnName("role_id");//RoleId
        }
      
    }

    /// <summary>AdminEntity  </summary>
    public  class BaseUserMap<BaseUser,Key> : BaseEfMapp<BaseUser>
    where BaseUser:BaseUserEntity<BaseUser, Key>
    {

        protected override void Set(EntityTypeBuilder<BaseUser> builder)
        {
            builder.Ignore(it => it.LoginDates);

            builder.HasKey(it => it.Id);//Id
            builder.Property(it => it.Id).HasColumnName("id");//Id

            builder.Property(it => it.Account).HasColumnName("account").HasMaxLength(255);//Account

            builder.Property(it => it.Email).HasColumnName("email").HasMaxLength(255);//Email

            builder.Property(it => it.Phone).HasColumnName("phone").HasMaxLength(255);//Phone

            builder.Property(it => it.Pwd).HasColumnName("pwd").HasMaxLength(255);//Pwd

            builder.Property(it => it.NickName).HasColumnName("nick_name").HasMaxLength(255);//NickName

            builder.Property(it => it.RealName).HasColumnName("real_name").HasMaxLength(255);//RealName

            builder.Property(it => it.Sex).HasColumnName("sex");//Sex

            builder.Property(it => it.Birthday).HasColumnName("birthday");//Birthday

            builder.Property(it => it.HeadPic).HasColumnName("head_pic").HasMaxLength(255);//HeadPic

            builder.Property(it => it.HeadPicId).HasColumnName("head_pic_id");//HeadPicId

            builder.Property(it => it.Status).HasColumnName("status");//Status

            builder.Property(it => it.RegisterDate).HasColumnName("register_date");//RegisterDate

            builder.Property(it => it.LoginDate).HasColumnName("login_date");//LoginDate


            builder.Property(it => it.RegisterIp).HasColumnName("register_ip");//RegisterIp

            builder.Property(it => it.LoginIp).HasColumnName("login_ip");//LoginIp

            builder.Property(it => it.Description).HasColumnName("description").HasMaxLength(255);//Description

            builder.Property(it => it.FailCount).HasColumnName("fail_count");//FailCount

            builder.Property(it => it.parent_id).HasColumnName("parent_id");//ParentId

          //  builder.HasOne(it => it.Parent).WithMany(it => it.Children).HasForeignKey("parent_id").HasConstraintName("fk_parent_id");//Parent

           // builder.HasMany(it => it.Children).WithOne(it => it.Parent).HasForeignKey("parent_id").HasConstraintName("fk_parent_id");//Children

        }
    }

#elif NET20 || NET30 || NET35 || NET40 || NET45 || NET451 || NET452 || NET46 || NET461 || NET462|| NET47 || NET471 || NET472|| NET48 
    /// <summary>AdminEntity  </summary>
    public  class AdminMap : BaseEfMapp<AdminEntity>
    {
        public AdminMap(): base("t_admin")
        {
         }
         protected override void Set(){
             base.Set();
            Property(it => it.RoleId).HasColumnName("role_id");//RoleId
        }
     }
/// <summary>AdminEntity  </summary>
    public  class BaseUserMap<BaseUser,Key> : BaseEfMapp<    public  class BaseUserMap<BaseUser,Key> : BaseEfMapp<AdminEntity>
>
    {
        public BaseUserMap(string table): base(table)
        {
         }

        protected override void Set()
        {
        
            Property(it => it.Id).HasColumnName("id");//Id

            Property(it => it.Account).HasColumnName("account").HasMaxLength(255);//Account

            Property(it => it.Email).HasColumnName("email").HasMaxLength(255);//Email

            Property(it => it.Phone).HasColumnName("phone").HasMaxLength(255);//Phone

            Property(it => it.Pwd).HasColumnName("pwd").HasMaxLength(255);//Pwd

            Property(it => it.NickName).HasColumnName("nick_name").HasMaxLength(255);//NickName

            Property(it => it.RealName).HasColumnName("real_name").HasMaxLength(255);//RealName

            Property(it => it.Sex).HasColumnName("sex");//Sex

            Property(it => it.Birthday).HasColumnName("birthday");//Birthday

            Property(it => it.HeadPic).HasColumnName("head_pic").HasMaxLength(255);//HeadPic

            Property(it => it.HeadPicId).HasColumnName("head_pic_id");//HeadPicId

            Property(it => it.Status).HasColumnName("status");//Status

            Property(it => it.RegisterDate).HasColumnName("register_date");//RegisterDate

            Property(it => it.LoginDate).HasColumnName("login_date");//LoginDate

            Property(it => it.LoginDates).HasColumnName("login_dates");//LoginDates

            Property(it => it.RegisterIp).HasColumnName("register_ip");//RegisterIp

            Property(it => it.LoginIp).HasColumnName("login_ip");//LoginIp

            Property(it => it.Description).HasColumnName("description").HasMaxLength(255);//Description

            Property(it => it.FailCount).HasColumnName("fail_count");//FailCount

            Property(it => it.ParentId).HasColumnName("parent_id");//ParentId

            Property(it => it.Parent).HasColumnName("parent");//Parent

            Property(it => it.Children).HasColumnName("children");//Children

        }
    }

#endif
}
