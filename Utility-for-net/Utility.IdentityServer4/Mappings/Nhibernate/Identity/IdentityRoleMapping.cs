using Microsoft.AspNetCore.Identity;
using NHibernate.Mapping.ByCode.Conformist;
using System;
using System.Linq.Expressions;

namespace Utility.IdentityServer4.Mappings
{
    public class IdentityRoleMapping : ClassMapping<IdentityRole>
    {
        public IdentityRoleMapping()
        {
            Lazy(false);
            Id((Expression<Func<IdentityRole, object>>)((IdentityRole r) => r.Id));
            Property((Expression<Func<IdentityRole, string>>)((IdentityRole u) => u.Id)
                ,x=> { x.Length(IdentityServer4Config.KeyMaxLength); });
            Table("AspNetRoles");
            Version((Expression<Func<IdentityRole, string>>)((IdentityRole r) => r.ConcurrencyStamp),x=> { });
            Property((Expression<Func<IdentityRole, string>>)((IdentityRole u) => u.Name), x => { x.Length(256); });
            Property((Expression<Func<IdentityRole, string>>)((IdentityRole u) => u.NormalizedName)
            , x => { x.Length(256); x.Index("RoleNameIndex");x.Unique(true); }
            );

        }
    }
    
}
