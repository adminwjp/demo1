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
    public class ApiResourcePropertyEntityConfiguration : IEntityTypeConfiguration<ApiResourceProperty>
    {
        public void Configure(EntityTypeBuilder<ApiResourceProperty> property)
        {
            IdentityServer4Config.ToTable(property, IdentityServer4Config.ConfigurationStore.ApiResourceProperty);
            property.Property((Expression<Func<ApiResourceProperty, string>>)((ApiResourceProperty x) => x.Key)).HasMaxLength(250).IsRequired();
            property.Property((Expression<Func<ApiResourceProperty, string>>)((ApiResourceProperty x) => x.Value)).HasMaxLength(2000).IsRequired();
        }
    }
}
