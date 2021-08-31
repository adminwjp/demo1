#if !(NET10 || NET11 || NET20 || NET30 || NET35 || NETCOREAPP1_0 || NETCOREAPP1_1 || NETSTANDARD1_0 || NETSTANDARD1_1 || NETSTANDARD1_2 || NETSTANDARD1_3 || NETSTANDARD1_4 || NETSTANDARD1_5 || NETSTANDARD1_6)
using NHibernate.Mapping.ByCode;
using SocialContact.Domain.Entities;

namespace SocialContact.Infrastructure.EntityMappings
{
    public class WorkMap:NHibernate.Mapping.ByCode.Conformist.ClassMapping<WorkEntity>
    {
        public WorkMap() : this("t_work")
        {
        }
        public WorkMap(string tableName)
        {
            Table(tableName);
            Lazy(false);
            //Id(x => x.Id, map =>{});
            Set();
        }
        protected virtual void Set(){
            Property(x => x.CompanyName, map =>{ map.Column("company_name"); map.Length(50); } );
            Property(x => x.Job, map =>{ map.Column("job"); map.Length(50); } );
            //ManyToOne(x => x.Catagory, map => { map.Cascade(NHibernate.Mapping.ByCode.Cascade.None);map.Access(NHibernate.Mapping.ByCode.Accessor.None); map.Column("category_id"); map.ForeignKey("fk_category_id"); });
            Property(x => x.Description, map =>{ map.Column("description"); map.Length(50); } );
            Property(x => x.StartDate, map =>{ map.Column("start_date"); } );
            Property(x => x.EndDate, map =>{ map.Column("end_date"); } );
            //ManyToOne(x => x.User, map => { map.Cascade(NHibernate.Mapping.ByCode.Cascade.None);map.Access(NHibernate.Mapping.ByCode.Accessor.None); map.Column("user_id"); map.ForeignKey("fk_user_id"); });
            Id(x => x.Id, it => { it.Column("id"); it.Generator(Generators.Identity); });//Id
            Property(x => x.CreateDate, map =>{ map.Column("create_date"); } );
            Property(x => x.UpdateDate, map =>{ map.Column("update_date"); } );
            
        }
    }
}
#endif