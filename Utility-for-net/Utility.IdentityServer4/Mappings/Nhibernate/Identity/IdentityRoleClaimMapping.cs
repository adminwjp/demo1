using Microsoft.AspNetCore.Identity;
using NHibernate.Mapping.ByCode.Conformist;
using System;
using System.Linq.Expressions;

namespace Utility.IdentityServer4.Mappings
{
    public class IdentityRoleClaimMapping : ClassMapping<IdentityRoleClaim<string>>
    {
        public IdentityRoleClaimMapping()
        {
            Lazy(false);
            Id((Expression<Func<IdentityRoleClaim<string>, object>>)((IdentityRoleClaim<string> rc) => rc.Id));
            Table("AspNetRoleClaims");

        }
    }
}
