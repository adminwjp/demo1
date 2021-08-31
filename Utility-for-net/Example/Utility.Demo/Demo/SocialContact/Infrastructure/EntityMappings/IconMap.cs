#if !(NET10 || NET11 || NET20 || NET30 || NET35 || NETCOREAPP1_0 || NETCOREAPP1_1 || NETSTANDARD1_0 || NETSTANDARD1_1 || NETSTANDARD1_2 || NETSTANDARD1_3 || NETSTANDARD1_4 || NETSTANDARD1_5 || NETSTANDARD1_6)
using NHibernate.Mapping.ByCode;
using SocialContact.Domain.Entities;

namespace SocialContact.Infrastructure.EntityMappings
{
    public class IconMap:NHibernate.Mapping.ByCode.Conformist.ClassMapping<IconEntity>
    {
        public IconMap():this("t_icon")
        {
           
        }
        public IconMap(string tableName)
        {
            Table(tableName);
            Lazy(false);
            //Id(x => x.Id, map =>{});
            Set();
        }
        protected virtual void Set(){
            Property(x => x.Name, map =>{ map.Column("name"); map.Length(50); } );
            Property(x => x.Style, map =>{ map.Column("style"); map.Length(50); } );
            Property(x => x.Description, map =>{ map.Column("description"); map.Length(50); } );
            Id(x => x.Id, it => { it.Column("id"); it.Generator(Generators.Identity); });//Id
            Property(x => x.CreateDate, map =>{ map.Column("create_date"); } );
            Property(x => x.UpdateDate, map =>{ map.Column("update_date"); } );

        }
    }
}
#endif