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
    /// <summary>UserEntity  </summary>
    public class UserMap: UserMap<UserEntity,long>
    {
        public UserMap()
        {
            this.TableName = "t_user";
        }
    }
    /// <summary>UserEntity  </summary>
    public  class UserMap<User,Key> : BaseUserMap<User,Key>
    where User:UserEntity<User,Key>
    {

        public override void Configure(EntityTypeBuilder<User> builder)
        {
            base.Configure(builder);
        
            builder.Property(it => it.Edution).HasColumnName("edution").HasMaxLength(255);//Edution

            builder.Property(it => it.School).HasColumnName("school").HasMaxLength(255);//School

            builder.Property(it => it.JobCompany).HasColumnName("job_company").HasMaxLength(255);//JobCompany

            builder.Property(it => it.Job).HasColumnName("job").HasMaxLength(255);//Job

            builder.Property(it => it.Likes).HasColumnName("likes").HasMaxLength(255);//Likes

            builder.Property(it => it.Marital).HasColumnName("marital");//Marital

            builder.Property(it => it.CardId).HasColumnName("card_id").HasMaxLength(255);//CardId

            builder.Property(it => it.CardPhoto1).HasColumnName("card_photo1").HasMaxLength(255);//CardPhoto1

            builder.Property(it => it.CardPhoto2).HasColumnName("card_photo2").HasMaxLength(255);//CardPhoto2

            builder.Property(it => it.HandCardPhoto1).HasColumnName("hand_card_photo1").HasMaxLength(255);//HandCardPhoto1

            builder.Property(it => it.HandCardPhoto2).HasColumnName("hand_card_photo2").HasMaxLength(255);//HandCardPhoto2

            builder.Property(it => it.CardPhotoStatus).HasColumnName("card_photo_status");//CardPhotoStatus

            builder.Property(it => it.CardPhotoId1).HasColumnName("card_photo_id1").HasMaxLength(255);//CardPhotoId1

            builder.Property(it => it.CardPhotoId2).HasColumnName("card_photo_id2").HasMaxLength(255);//CardPhotoId2

            builder.Property(it => it.HandCardPhotoId1).HasColumnName("hand_card_photo_id1").HasMaxLength(255);//HandCardPhotoId1

            builder.Property(it => it.HandCardPhotoId2).HasColumnName("hand_card_photo_id2").HasMaxLength(255);//HandCardPhotoId2

            builder.Property(it => it.Level).HasColumnName("level");//Level

            builder.Property(it => it.BankId).HasColumnName("bank_id").HasMaxLength(255);//BankId
            

        }
    }

#elif NET20 || NET30 || NET35 || NET40 || NET45 || NET451 || NET452 || NET46 || NET461 || NET462|| NET47 || NET471 || NET472|| NET48 
    /// <summary>UserEntity  </summary>
    public  class UserMap : BaseEfMapp<UserEntity>
    {
        public UserMap(): base("t_user")
        {
         }

        protected override void Set()
        {
            Property(it => it.Edution).HasColumnName("edution").HasMaxLength(255);//Edution

            Property(it => it.School).HasColumnName("school").HasMaxLength(255);//School

            Property(it => it.JobCompany).HasColumnName("job_company").HasMaxLength(255);//JobCompany

            Property(it => it.Job).HasColumnName("job").HasMaxLength(255);//Job

            Property(it => it.Likes).HasColumnName("likes").HasMaxLength(255);//Likes

            Property(it => it.Marital).HasColumnName("marital");//Marital

            Property(it => it.CardId).HasColumnName("card_id").HasMaxLength(255);//CardId

            Property(it => it.CardPhoto1).HasColumnName("card_photo1").HasMaxLength(255);//CardPhoto1

            Property(it => it.CardPhoto2).HasColumnName("card_photo2").HasMaxLength(255);//CardPhoto2

            Property(it => it.HandCardPhoto1).HasColumnName("hand_card_photo1").HasMaxLength(255);//HandCardPhoto1

            Property(it => it.HandCardPhoto2).HasColumnName("hand_card_photo2").HasMaxLength(255);//HandCardPhoto2

            Property(it => it.CardPhotoStatus).HasColumnName("card_photo_status");//CardPhotoStatus

            Property(it => it.CardPhotoId1).HasColumnName("card_photo_id1").HasMaxLength(255);//CardPhotoId1

            Property(it => it.CardPhotoId2).HasColumnName("card_photo_id2").HasMaxLength(255);//CardPhotoId2

            Property(it => it.HandCardPhotoId1).HasColumnName("hand_card_photo_id1").HasMaxLength(255);//HandCardPhotoId1

            Property(it => it.HandCardPhotoId2).HasColumnName("hand_card_photo_id2").HasMaxLength(255);//HandCardPhotoId2

            Property(it => it.Level).HasColumnName("level");//Level

            Property(it => it.BankId).HasColumnName("bank_id").HasMaxLength(255);//BankId

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
