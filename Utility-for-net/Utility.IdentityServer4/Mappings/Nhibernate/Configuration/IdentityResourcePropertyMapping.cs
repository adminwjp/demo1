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
    public class IdentityResourcePropertyMapping : IEntityTypeConfiguration<IdentityResourceProperty>
    {
        public void Configure(EntityTypeBuilder<IdentityResourceProperty> property)
        {
            property.HasKey(it => it.Id);
            IdentityServer4Config.ToTable(property,IdentityServer4Config.ConfigurationStore.IdentityResourceProperty);
            property.Property((Expression<Func<IdentityResourceProperty, string>>)((IdentityResourceProperty x) => x.Key)).HasMaxLength(250).IsRequired();
            property.Property((Expression<Func<IdentityResourceProperty, string>>)((IdentityResourceProperty x) => x.Value)).HasMaxLength(2000).IsRequired();
        }
    }
}
