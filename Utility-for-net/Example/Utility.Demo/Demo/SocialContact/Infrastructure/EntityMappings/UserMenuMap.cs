#if !(NET10 || NET11 || NET20 || NET30 || NET35 || NETCOREAPP1_0 || NETCOREAPP1_1 || NETSTANDARD1_0 || NETSTANDARD1_1 || NETSTANDARD1_2 || NETSTANDARD1_3 || NETSTANDARD1_4 || NETSTANDARD1_5 || NETSTANDARD1_6)
using NHibernate.Mapping.ByCode;
using SocialContact.Domain.Entities;

namespace SocialContact.Infrastructure.EntityMappings
{
    public class UserMenuMap:NHibernate.Mapping.ByCode.Conformist.ClassMapping<UserMenuEntity>
    {
        public UserMenuMap() : this("t_user_menu")
        {
        }
        public UserMenuMap(string tableName)
        {
            Table(tableName);
            Lazy(false);
            //Id(x => x.Id, map =>{});
            Set();
        }
        protected virtual void Set(){
           // OneToOne(x => x.Menu, map => { map.Cascade(NHibernate.Mapping.ByCode.Cascade.None);map.Access(NHibernate.Mapping.ByCode.Accessor.None); map.ForeignKey("menu_id"); });
           // OneToOne(x => x.Admin, map => { map.Cascade(NHibernate.Mapping.ByCode.Cascade.None);map.Access(NHibernate.Mapping.ByCode.Accessor.None); map.ForeignKey("admin_id"); });
           // OneToOne(x => x.Role, map => { map.Cascade(NHibernate.Mapping.ByCode.Cascade.None);map.Access(NHibernate.Mapping.ByCode.Accessor.None); map.ForeignKey("role_id"); });
           Property(x => x.Add, map =>{ map.Column("add1"); } );
            Property(x => x.Modify, map =>{ map.Column("modify1"); } );
            Property(x => x.Delete, map =>{ map.Column("delete1"); } );
            Property(x => x.Query, map =>{ map.Column("query"); } );
            Property(x => x.Enable, map =>{ map.Column("enable"); } );
            Property(x => x.Type, map =>{ map.Column("type"); map.Length(50); } );
            Property(x => x.Val, map =>{ map.Column("val"); } );
            Id(x => x.Id, it => { it.Column("id"); it.Generator(Generators.Identity); });//Id
            Property(x => x.CreateDate, map =>{ map.Column("create_date"); } );
            Property(x => x.UpdateDate, map =>{ map.Column("update_date"); } );

        }
    }
}
#endif