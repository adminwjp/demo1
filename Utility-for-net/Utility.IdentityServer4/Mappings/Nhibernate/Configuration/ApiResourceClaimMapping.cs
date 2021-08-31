using IdentityServer4.EntityFramework.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NHibernate.Mapping.ByCode.Conformist;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Utility.IdentityServer4.Mappings
{
    public class ApiResourceClaimMapping : ClassMapping<ApiResourceClaim>
    {
        public ApiResourceClaimMapping()
        {
            Lazy(false);
            Id(it => it.Id);
            Table(IdentityServer4Config.ConfigurationStore.ApiResourceClaim.Name);
            Property((Expression<Func<ApiResourceClaim, string>>)((ApiResourceClaim x) => x.Type),x=> { x.Length(200);x.NotNullable(true); });

        }
    }
}
