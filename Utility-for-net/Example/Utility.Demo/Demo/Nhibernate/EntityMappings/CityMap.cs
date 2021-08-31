using NHibernate.Mapping.ByCode;
using Utility.Demo.Domain.Entities;
using Utility.Nhibernate.EntityMappings;

namespace Utility.Demo.Nhibernate.EntityMappings
{
    public class CityMap : CityMap<CityEntity, long>
    {
        public CityMap() : base("t_city")
        {
            Id(x => x.Id, it => { it.Column("id"); it.Generator(Generators.Identity); });//Id
            //ex  Could not determine type for
            Property(x => x.ParentId, it => { it.Column("parent_id"); });//Parent

            //ManyToOne(x => x.Parent, it => { it.Column("parent_id"); it.ForeignKey("fk_parent_id"); it.Cascade(NHibernate.Mapping.ByCode.Cascade.All); });//Parent


           // Set(x => x.Children, key => { key.Key(it => { it.ForeignKey("fk_parent_id"); it.Column("parent_id"); }); });//Children

        }


    }
    /// <summary> CityEntity nhibernate映射  </summary>
    public class CityMap<T, Key> : BaseNhibernateMapp<T>
           where T : CityEntity<T, Key>
    {
        public CityMap(string tableName) : base(tableName)
        {

        }

        protected override void Set()
        {
           // Id(x => x.Id, it => { it.Column("id"); });//Id

            Property(x => x.Prodive, it => { it.Column("prodive"); it.Length(255); });//Prodive

            Property(x => x.City, it => { it.Column("city"); it.Length(255); });//City

            Property(x => x.Area, it => { it.Column("area"); it.Length(255); });//Area

            Property(x => x.ProdiveCode, it => { it.Column("prodive_code"); it.Length(255); });//ProdiveCode

            Property(x => x.CityCode, it => { it.Column("city_code"); it.Length(255); });//CityCode

            Property(x => x.AreaCode, it => { it.Column("area_code"); it.Length(255); });//AreaCode

           // Property(x => x.ParentId, it => { it.Column("parent_id"); });//ParentId

         
        }
    }
}
