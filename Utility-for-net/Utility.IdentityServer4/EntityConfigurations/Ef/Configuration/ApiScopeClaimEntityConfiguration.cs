using IdentityServer4.EntityFramework.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Utility.IdentityServer4.EntityConfigurations
{
    public class ApiScopeClaimEntityConfiguration : IEntityTypeConfiguration<ApiScopeClaim>
    {
        public void Configure(EntityTypeBuilder<ApiScopeClaim> scopeClaim)
        {
            scopeClaim.HasKey(it => it.Id);
            IdentityServer4Config.ToTable(scopeClaim, IdentityServer4Config.ConfigurationStore.ApiScopeClaim).HasKey(x => x.Id);
            scopeClaim.Property(x => x.Type).HasMaxLength(200).IsRequired();
        }
    }
}
