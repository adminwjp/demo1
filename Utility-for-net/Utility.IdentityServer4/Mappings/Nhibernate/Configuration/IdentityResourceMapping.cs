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
    public class IdentityResourceMapping : IEntityTypeConfiguration<IdentityResource>
    {
        public void Configure(EntityTypeBuilder<IdentityResource> identityResource)
        {
            //MySql || SqlServer
            if (IdentityServer4Config.Driver== Driver.MySql5_5)
            {
                //mysql
                identityResource.Property((Expression<Func<IdentityResource, DateTime>>)((IdentityResource x) => x.Created)).HasColumnType("datetime");
                identityResource.Property((Expression<Func<IdentityResource, DateTime?>>)((IdentityResource x) => x.Updated)).HasColumnType("datetime");
            }

            identityResource.HasKey(it => it.Id);
            IdentityServer4Config.ToTable(identityResource, IdentityServer4Config.ConfigurationStore.IdentityResource).HasKey((Expression<Func<IdentityResource, object>>)((IdentityResource x) => x.Id));
            identityResource.Property((Expression<Func<IdentityResource, string>>)((IdentityResource x) => x.Name)).HasMaxLength(200).IsRequired();
            identityResource.Property((Expression<Func<IdentityResource, string>>)((IdentityResource x) => x.DisplayName)).HasMaxLength(200);
            identityResource.Property((Expression<Func<IdentityResource, string>>)((IdentityResource x) => x.Description)).HasMaxLength(1000);
            identityResource.HasIndex((Expression<Func<IdentityResource, object>>)((IdentityResource x) => x.Name)).IsUnique();
            identityResource.HasMany((Expression<Func<IdentityResource, IEnumerable<IdentityResourceClaim>>>)((IdentityResource x) => x.UserClaims)).WithOne((Expression<Func<IdentityResourceClaim, IdentityResource>>)((IdentityResourceClaim x) => x.IdentityResource)).HasForeignKey((Expression<Func<IdentityResourceClaim, object>>)((IdentityResourceClaim x) => x.IdentityResourceId))
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade);
            identityResource.HasMany((Expression<Func<IdentityResource, IEnumerable<IdentityResourceProperty>>>)((IdentityResource x) => x.Properties)).WithOne((Expression<Func<IdentityResourceProperty, IdentityResource>>)((IdentityResourceProperty x) => x.IdentityResource)).HasForeignKey((Expression<Func<IdentityResourceProperty, object>>)((IdentityResourceProperty x) => x.IdentityResourceId))
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
