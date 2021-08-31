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
    public class ApiScopePropertyMapping : IEntityTypeConfiguration<ApiScopeProperty>
    {
        public void Configure(EntityTypeBuilder<ApiScopeProperty> property)
        {
            property.HasKey(it => it.Id);
            IdentityServer4Config.ToTable(property, IdentityServer4Config.ConfigurationStore.ApiScopeProperty).HasKey((Expression<Func<ApiScopeProperty, object>>)((ApiScopeProperty x) => x.Id));
            property.Property((Expression<Func<ApiScopeProperty, string>>)((ApiScopeProperty x) => x.Key)).HasMaxLength(250).IsRequired();
            property.Property((Expression<Func<ApiScopeProperty, string>>)((ApiScopeProperty x) => x.Value)).HasMaxLength(2000).IsRequired();
        }
    }
}
