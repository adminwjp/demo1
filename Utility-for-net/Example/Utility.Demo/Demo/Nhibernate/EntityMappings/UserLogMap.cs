using NHibernate.Mapping.ByCode;
using Utility.Demo.Domain.Entities;
using Utility.Nhibernate.EntityMappings;

namespace Utility.Demo.Nhibernate.EntityMappings
{
    public class UserLogMap : UserLogMap<UserLogEntity, long>
    {
        public UserLogMap() : base("t_user_log")
        {
            Id(x => x.Id, it => { it.Column("id"); it.Generator(Generators.Identity); });//Id
        }

    }
    /// <summary> UserLogEntity nhibernate映射  </summary>
    public class UserLogMap<T, Key> : BaseNhibernateMapp<T>
           where T : UserLogEntity<Key>
    {
        public UserLogMap(string tableName) : base(tableName)
        {

        }

        protected override void Set()
        {
           // Id(x => x.Id, it => { it.Column("id"); });//Id

            Property(x => x.UserId, it => { it.Column("user_id"); });//UserId

            Property(x => x.Account, it => { it.Column("account"); it.Length(255); });//Account

            Property(x => x.Email, it => { it.Column("email"); it.Length(255); });//Email

            Property(x => x.Phone, it => { it.Column("phone"); it.Length(255); });//Phone

            Property(x => x.OldPwd, it => { it.Column("old_pwd"); it.Length(255); });//OldPwd

            Property(x => x.NewPwd, it => { it.Column("new_pwd"); it.Length(255); });//NewPwd

            Property(x => x.NewAccount, it => { it.Column("new_account"); it.Length(255); });//NewAccount

            Property(x => x.NewEmail, it => { it.Column("new_email"); it.Length(255); });//NewEmail

            Property(x => x.NewPhone, it => { it.Column("new_phone"); it.Length(255); });//NewPhone

            Property(x => x.AddDate, it => { it.Column("add_date"); });//AddDate

            Property(x => x.Flag, it => { it.Column("flag"); });//Flag

        }
    }
}
