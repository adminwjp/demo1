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
    public class IdentityResourceEntityConfiguration : IEntityTypeConfiguration<IdentityResource>
    {
        public void Configure(EntityTypeBuilder<IdentityResource> identityResource)
        {
            //MySql || SqlServer
            if (IdentityServer4Config.Driver== Driver.MySql5_5)
            {
                //mysql
                identityResource.Property(x => x.Created).HasColumnType("datetime");
                identityResource.Property(x => x.Updated).HasColumnType("datetime");
            }

            identityResource.HasKey(it => it.Id);
            IdentityServer4Config.ToTable(identityResource, IdentityServer4Config.ConfigurationStore.IdentityResource).HasKey(x => x.Id);
            identityResource.Property(x => x.Name).HasMaxLength(200).IsRequired();
            identityResource.Property(x => x.DisplayName).HasMaxLength(200);
            identityResource.Property(x => x.Description).HasMaxLength(1000);
            identityResource.HasIndex(x => x.Name).IsUnique();
            identityResource.HasMany(x => x.UserClaims).WithOne(x => x.IdentityResource).HasForeignKey(x => x.IdentityResourceId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade);
            identityResource.HasMany(x => x.Properties).WithOne(x => x.IdentityResource).HasForeignKey(x => x.IdentityResourceId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
