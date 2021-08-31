using NHibernate.Mapping.ByCode;
using Utility.Demo.Domain.Entities;
using Utility.Nhibernate.EntityMappings;

namespace Utility.Demo.Nhibernate.EntityMappings
{
    public class BankMap : BankMap<BankEntity, long>
    {
        public BankMap() : base("t_bank")
        {
            Id(x => x.Id, it => { it.Column("id"); it.Generator(Generators.Identity); });//Id
        }

    }
    /// <summary> BankEntity nhibernate映射  </summary>
    public class BankMap<T, Key> : BaseNhibernateMapp<T>
           where T : BankEntity<Key>
    {
        public BankMap(string tableName) : base(tableName)
        {

        }

        protected override void Set()
        {
            //Id(x => x.Id, it => { it.Column("id"); });//Id

            Property(x => x.BankId, it => { it.Column("bank_id"); it.Length(255); });//BankId

            Property(x => x.BankName, it => { it.Column("bank_name"); it.Length(255); });//BankName

            Property(x => x.BankPhoto1, it => { it.Column("bank_photo1"); it.Length(255); });//BankPhoto1

            Property(x => x.BankPhoto2, it => { it.Column("bank_photo2"); it.Length(255); });//BankPhoto2

            Property(x => x.BankPhotoId1, it => { it.Column("bank_photo_id1"); });//BankPhotoId1

            Property(x => x.BankPhotoId2, it => { it.Column("bank_photo_id2"); });//BankPhotoId2

            Property(x => x.BankAddress, it => { it.Column("bank_address"); it.Length(255); });//BankAddress

            Property(x => x.BankUserName, it => { it.Column("bank_user_name"); it.Length(255); });//BankUserName

            Property(x => x.BankUserAddress, it => { it.Column("bank_user_address"); it.Length(255); });//BankUserAddress

            Property(x => x.IsDefault, it => { it.Column("is_default"); });//IsDefault

            Property(x => x.UserId, it => { it.Column("user_id"); });//UserId

        }
    }
}
