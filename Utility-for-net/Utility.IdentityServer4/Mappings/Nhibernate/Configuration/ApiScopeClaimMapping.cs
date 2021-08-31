using IdentityServer4.EntityFramework.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Utility.IdentityServer4.Mappings
{
    public class ApiScopeClaimMapping : IEntityTypeConfiguration<ApiScopeClaim>
    {
        public void Configure(EntityTypeBuilder<ApiScopeClaim> scopeClaim)
        {
            scopeClaim.HasKey(it => it.Id);
            IdentityServer4Config.ToTable(scopeClaim, IdentityServer4Config.ConfigurationStore.ApiScopeClaim).HasKey((Expression<Func<ApiScopeClaim, object>>)((ApiScopeClaim x) => x.Id));
            scopeClaim.Property((Expression<Func<ApiScopeClaim, string>>)((ApiScopeClaim x) => x.Type)).HasMaxLength(200).IsRequired();
        }
    }
}
