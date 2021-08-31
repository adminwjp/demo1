#if !(NET10 || NET11 || NET20 || NET30 || NET35 || NETCOREAPP1_0 || NETCOREAPP1_1 || NETSTANDARD1_0 || NETSTANDARD1_1 || NETSTANDARD1_2 || NETSTANDARD1_3 || NETSTANDARD1_4 || NETSTANDARD1_5 || NETSTANDARD1_6)
using NHibernate.Mapping.ByCode;
using SocialContact.Domain.Entities;

namespace SocialContact.Infrastructure.EntityMappings
{
    public class EdutionMap:NHibernate.Mapping.ByCode.Conformist.ClassMapping<EdutionEntity>
    {
        public EdutionMap() : this("t_edution")
        {
        }
        public EdutionMap(string tableName)
        {
            Table(tableName);
            Lazy(false);
            //Id(x => x.Id, map =>{});
            Set();
        }
        protected virtual void Set(){
            // ManyToOne(x => x.Catagory, map => { map.Cascade(NHibernate.Mapping.ByCode.Cascade.None); map.Access(NHibernate.Mapping.ByCode.Accessor.None); map.Column("category_id"); map.ForeignKey("fk_category_id"); });
            //ManyToOne(x => x.User, map => { map.Cascade(NHibernate.Mapping.ByCode.Cascade.None);map.Access(NHibernate.Mapping.ByCode.Accessor.None); map.Column("user_id"); map.ForeignKey("fk_user_id"); });
            Id(x => x.Id, it => { it.Column("id"); it.Generator(Generators.Identity); });//Id
            Property(x => x.CreateDate, map =>{ map.Column("create_date"); } );
            Property(x => x.UpdateDate, map =>{ map.Column("update_date"); } );
           
            Property(x => x.FirstEdution, map => { map.Column("first_edution"); });
            Property(x => x.FirstSchool, map => { map.Column("first_school"); });
            Property(x => x.FirstStartDate, map => { map.Column("first_start_date"); });
            Property(x => x.FirstEndDate, map => { map.Column("first_end_date"); });

            Property(x => x.SecondEdution, map => { map.Column("second_edution"); });
            Property(x => x.SecondSchool, map => { map.Column("second_school"); });
            Property(x => x.SecondStartDate, map => { map.Column("second_start_date"); });
            Property(x => x.SecondEndDate, map => { map.Column("second_end_date"); });

            Property(x => x.ThreeEdution, map => { map.Column("three_edution"); });
            Property(x => x.ThreeSchool, map => { map.Column("three_school"); });
            Property(x => x.ThreeStartDate, map => { map.Column("three_start_date"); });
            Property(x => x.ThreeEndDate, map => { map.Column("three_end_date"); });

            Property(x => x.FourEdution, map => { map.Column("four_edution"); });
            Property(x => x.FourSchool, map => { map.Column("four_school"); });
            Property(x => x.FourStartDate, map => { map.Column("four_start_date"); });
            Property(x => x.FourEndDate, map => { map.Column("four_end_date"); });

            Property(x => x.FiveEdution, map => { map.Column("five_edution"); });
            Property(x => x.FiveSchool, map => { map.Column("five_school"); });
            Property(x => x.FiveStartDate, map => { map.Column("five_start_date"); });
            Property(x => x.FiveEndDate, map => { map.Column("five_end_date"); });

        }
    }
}
#endif