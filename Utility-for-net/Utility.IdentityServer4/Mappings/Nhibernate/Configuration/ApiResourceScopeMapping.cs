using IdentityServer4.EntityFramework.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Utility.IdentityServer4.Mappings
{
    public class ApiResourceScopeMapping : IEntityTypeConfiguration<ApiResourceScope>
    {
        public void Configure(EntityTypeBuilder<ApiResourceScope> apiScope)
        {
            apiScope.HasKey(it => it.Id);
            IdentityServer4Config.ToTable(apiScope, IdentityServer4Config.ConfigurationStore.ApiResourceScope).HasKey((Expression<Func<ApiResourceScope, object>>)((ApiResourceScope x) => x.Id));
            apiScope.Property((Expression<Func<ApiResourceScope, string>>)((ApiResourceScope x) => x.Scope)).HasMaxLength(200).IsRequired();
        }

    }
}
