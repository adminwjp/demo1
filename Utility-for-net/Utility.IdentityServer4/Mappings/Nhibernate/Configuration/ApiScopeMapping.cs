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
    public class ApiScopeMapping : IEntityTypeConfiguration<ApiScope>
    {
        public void Configure(EntityTypeBuilder<ApiScope> scope)
        {
            scope.HasKey(it => it.Id);
            IdentityServer4Config.ToTable(scope, IdentityServer4Config.ConfigurationStore.ApiScope).HasKey((Expression<Func<ApiScope, object>>)((ApiScope x) => x.Id));
            scope.Property((Expression<Func<ApiScope, string>>)((ApiScope x) => x.Name)).HasMaxLength(200).IsRequired();
            scope.Property((Expression<Func<ApiScope, string>>)((ApiScope x) => x.DisplayName)).HasMaxLength(200);
            scope.Property((Expression<Func<ApiScope, string>>)((ApiScope x) => x.Description)).HasMaxLength(1000);
            scope.HasIndex((Expression<Func<ApiScope, object>>)((ApiScope x) => x.Name)).IsUnique();
            scope.HasMany((Expression<Func<ApiScope, IEnumerable<ApiScopeClaim>>>)((ApiScope x) => x.UserClaims)).WithOne((Expression<Func<ApiScopeClaim, ApiScope>>)((ApiScopeClaim x) => x.Scope)).HasForeignKey((Expression<Func<ApiScopeClaim, object>>)((ApiScopeClaim x) => x.ScopeId))
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
