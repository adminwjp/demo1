using NHibernate.Mapping.ByCode;
using Utility.Demo.Domain.Entities;
using Utility.Nhibernate.EntityMappings;

namespace Utility.Demo.Nhibernate.EntityMappings
{
    public class ManufacturerAddressMap : AddressMap<ManufacturerAddressEntity, long>
    {
        public ManufacturerAddressMap() : base("t_manufacturer_address")
        {
            Id(x => x.Id, it => { it.Column("id"); it.Generator(Generators.Identity); });//Id
        }

    }
    public class SellerAddressMap : AddressMap<SellerAddressEntity, long>
    {
        public SellerAddressMap() : base("t_seller_address")
        {
            Id(x => x.Id, it => { it.Column("id"); it.Generator(Generators.Identity); });//Id
        }

    }

    public class AgentAddressMap : AddressMap<AgentAddressEntity, long>
    {
        public AgentAddressMap() : base("t_agent_address")
        {
            Id(x => x.Id, it => { it.Column("id"); it.Generator(Generators.Identity); });//Id
        }

    }
    /// <summary> AddressEntity nhibernate映射  </summary>
    public class AddressMap : AddressMap<AddressEntity, long>
    {
        public AddressMap() : base("t_address")
        {
            Id(x => x.Id, it => { it.Column("id"); it.Generator(Generators.Identity); });//Id
        }

    }
    /// <summary> AddressEntity nhibernate映射  </summary>
    public class AddressMap<T, Key> : BaseNhibernateMapp<T>
           where T : AddressEntity<Key>
    {
        public AddressMap(string tableName) : base(tableName)
        {

        }

        protected override void Set()
        {
            //Id(x => x.Id, it => { it.Column("id"); });//Id

            Property(x => x.ContactName, it => { it.Column("contact_name"); it.Length(255); });//ContactName

            Property(x => x.Address, it => { it.Column("address"); it.Length(255); });//Address

            Property(x => x.Area, it => { it.Column("area"); it.Length(255); });//Area

            Property(x => x.City, it => { it.Column("city"); it.Length(255); });//City

            Property(x => x.Province, it => { it.Column("province"); it.Length(255); });//Province

            Property(x => x.Country, it => { it.Column("country"); it.Length(255); });//Country

            Property(x => x.Memo, it => { it.Column("memo"); it.Length(255); });//Memo

            Property(x => x.Phone, it => { it.Column("phone"); it.Length(255); });//Phone

            Property(x => x.PostCode, it => { it.Column("post_code"); it.Length(255); });//PostCode

            Property(x => x.IsDefault, it => { it.Column("is_default"); });//IsDefault

            Property(x => x.UserId, it => { it.Column("user_id"); });//UserId

        }
    }
}
