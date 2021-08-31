
using IdentityServer4.EntityFramework.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Utility.IdentityServer4.EntityConfigurations
{
    public class IdentityRoleEntityConfiguration : IEntityTypeConfiguration<IdentityRole>
    {
        public void Configure(EntityTypeBuilder<IdentityRole> builder)
        {
            builder.HasKey(r => r.Id);
            builder.Property(u => u.Id).HasMaxLength(IdentityServer4Config.KeyMaxLength);
            builder.HasIndex(r => r.NormalizedName).HasName("RoleNameIndex").IsUnique();
            builder.ToTable("AspNetRoles");
            builder.Property(r => r.ConcurrencyStamp).IsConcurrencyToken();
            builder.Property(u => u.Name).HasMaxLength(256);
            builder.Property(u=> u.NormalizedName).HasMaxLength(256);
            builder.HasMany<IdentityUserRole<string>>().WithOne().HasForeignKey(ur => ur.RoleId)
                .IsRequired();
            builder.HasMany<IdentityRoleClaim<string>>().WithOne().HasForeignKey(rc => rc.RoleId)
                .IsRequired();
        }

    }
}
