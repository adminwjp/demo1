#if !(NET10 || NET11 || NET20 || NET30 || NET35 || NETCOREAPP1_0 || NETCOREAPP1_1 || NETSTANDARD1_0 || NETSTANDARD1_1 || NETSTANDARD1_2 || NETSTANDARD1_3 || NETSTANDARD1_4 || NETSTANDARD1_5 || NETSTANDARD1_6)

using NHibernate.Mapping.ByCode;
using SocialContact.Domain.Entities;

namespace SocialContact.Infrastructure.EntityMappings
{
    public class CategoryMap:NHibernate.Mapping.ByCode.Conformist.ClassMapping<CatagoryEntity>
    {
        public CategoryMap():this("t_catagory")
        {
           
        }
        public CategoryMap(string tableName)
        {
            Table(tableName);
            Lazy(false);
            //Id(x => x.Id, map =>{});
            Set();
        }
        protected virtual void Set(){
            Property(x => x.Category, map =>{ map.Column("category"); map.Length(50); } );
            Property(x => x.Description, map =>{ map.Column("description"); map.Length(50); } );
            Property(x => x.Accept, map => { map.Column("accept"); map.Length(50); });
            Id(x => x.Id, it => { it.Column("id"); it.Generator(Generators.Identity); });//Id
            Property(x => x.Flag, map => { map.Column("flag"); });
            //ManyToOne(x => x.Parent, map => { map.Column("parent_id");map.ForeignKey("fk_parent_id"); map.NotFound(NotFoundMode.Ignore); });
          //  Set(x => x.Children,map=> { map.Key(it=> { it.Column("parent_id"); it.ForeignKey("fk_parent_id");  }); map.Cascade(Cascade.All); });
           
           // Set(x => x.Works, map => { map.Cascade(Cascade.None); map.Access(Accessor.None); map.Key(x=> { x.Column("category_id"); x.ForeignKey("fk_category_id"); }); });

           // Set(x => x.Edutions, map => { map.Cascade(Cascade.None); map.Access(Accessor.None); map.Key(x => { x.Column("category_id"); x.ForeignKey("fk_category_id"); }); });


        }

    }
}
#endif