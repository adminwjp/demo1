
using NHibernate.Mapping.ByCode;
using Utility.Demo.Domain.Entities;
using Utility.Nhibernate.EntityMappings;

namespace Utility.Demo.Nhibernate.EntityMappings
{
    public class AdminMap : BaseUserMap<AdminEntity, long>
    {
        public AdminMap() : base("t_admin")
        {
            Id(x => x.Id, it => { it.Column("id"); it.Generator(Generators.Identity); });//Id
            Property(x => x.RoleId, it => { it.Column("role_id"); });//RoleId
            //  ManyToOne(x => x.Parent, map => { map.Column("parent_id"); map.ForeignKey("fk_parent_id"); map.NotFound(NotFoundMode.Ignore); map.Cascade(Cascade.All); });

            //  Set(x => x.Children, map => { map.Key(it => { it.Column("parent_id"); it.ForeignKey("fk_parent_id"); }); });
            //ManyToOne(map => map.Role, map => { map.Column("role_id"); map.ForeignKey("fk_role_id"); });
        }

    }
    public class UserMap : UserMap<UserEntity, long>
    {
        public UserMap() : base("t_user")
        {
            Id(x => x.Id, it => { it.Column("id"); it.Generator(Generators.Identity); });//Id
        }

    } /// <summary> UserEntity nhibernate映射  </summary>
    public class BaseUserMap<T, Key> : BaseNhibernateMapp<T>
           where T : BaseUserEntity<T, Key>
    {
        public BaseUserMap(string tableName) : base(tableName)
        {

        }

        protected override void Set()
        {
            //  Id(x => x.Id, it => { it.Column("id"); });//Id

            Property(x => x.Account, it => { it.Column("account"); it.Length(255); });//Account

            Property(x => x.Email, it => { it.Column("email"); it.Length(255); });//Email

            Property(x => x.Phone, it => { it.Column("phone"); it.Length(255); });//Phone

            Property(x => x.Pwd, it => { it.Column("pwd"); it.Length(255); });//Pwd

            Property(x => x.NickName, it => { it.Column("nick_name"); it.Length(255); });//NickName

            Property(x => x.RealName, it => { it.Column("real_name"); it.Length(255); });//RealName

            Property(x => x.Sex, it => { it.Column("sex"); });//Sex

            Property(x => x.Birthday, it => { it.Column("birthday"); });//Birthday

            Property(x => x.HeadPic, it => { it.Column("head_pic"); it.Length(255); });//HeadPic

            Property(x => x.HeadPicId, it => { it.Column("head_pic_id"); });//HeadPicId

            Property(x => x.Status, it => { it.Column("status"); });//Status

            Property(x => x.RegisterDate, it => { it.Column("register_date"); });//RegisterDate

            Property(x => x.LoginDate, it => { it.Column("login_date"); });//LoginDate

            Property(x => x.RegisterIp, it => { it.Column("register_ip"); });//RegisterIp

            Property(x => x.LoginIp, it => { it.Column("login_ip"); });//LoginIp

            Property(x => x.Description, it => { it.Column("description"); it.Length(255); });//Description

        
            Property(x => x.FailCount, it => { it.Column("fail_count"); });//FailCount

            // Property(x => x.RoleId, it => { it.Column("role_id"); });//RoleId

            Property(x => x.parent_id, it => { it.Column("parent_id"); });//ParentId

            // ManyToOne(x => x.Parent, map => { map.Column("parent_id"); map.ForeignKey("fk_parent_id"); map.NotFound(NotFoundMode.Ignore); map.Cascade(Cascade.All); });

            //Set(x => x.Children, map => { map.Key(it => { it.Column("parent_id"); it.ForeignKey("fk_parent_id"); }); });

        }
    }
    /// <summary> UserEntity nhibernate映射  </summary>
    public class UserMap<T, Key> : BaseUserMap<T, Key>
           where T : UserEntity<T, Key>
    {
        public UserMap(string tableName) : base(tableName)
        {

        }

        protected override void Set()
        {
          //  Id(x => x.Id, it => { it.Column("id"); });//Id

            Property(x => x.Edution, it => { it.Column("edution"); it.Length(255); });//Edution

            Property(x => x.School, it => { it.Column("school"); it.Length(255); });//School

            Property(x => x.JobCompany, it => { it.Column("job_company"); it.Length(255); });//JobCompany

            Property(x => x.Job, it => { it.Column("job"); it.Length(255); });//Job

            Property(x => x.Likes, it => { it.Column("likes"); it.Length(255); });//Likes

            Property(x => x.Marital, it => { it.Column("marital"); });//Marital

            Property(x => x.Description, it => { it.Column("description"); it.Length(255); });//Description

            Property(x => x.CardId, it => { it.Column("card_id"); it.Length(255); });//CardId

            Property(x => x.CardPhoto1, it => { it.Column("card_photo1"); it.Length(255); });//CardPhoto1

            Property(x => x.CardPhoto2, it => { it.Column("card_photo2"); it.Length(255); });//CardPhoto2

            Property(x => x.HandCardPhoto1, it => { it.Column("hand_card_photo1"); it.Length(255); });//HandCardPhoto1

            Property(x => x.HandCardPhoto2, it => { it.Column("hand_card_photo2"); it.Length(255); });//HandCardPhoto2

            Property(x => x.CardPhotoStatus, it => { it.Column("card_photo_status"); });//CardPhotoStatus

            Property(x => x.CardPhotoId1, it => { it.Column("card_photo_id1"); it.Length(255); });//CardPhotoId1

            Property(x => x.CardPhotoId2, it => { it.Column("card_photo_id2"); it.Length(255); });//CardPhotoId2

            Property(x => x.HandCardPhotoId1, it => { it.Column("hand_card_photo_id1"); it.Length(255); });//HandCardPhotoId1

            Property(x => x.HandCardPhotoId2, it => { it.Column("hand_card_photo_id2"); it.Length(255); });//HandCardPhotoId2

            Property(x => x.Level, it => { it.Column("level"); });//Level

            Property(x => x.BankId, it => { it.Column("bank_id"); it.Length(255); });//BankId


        }
    }
}
