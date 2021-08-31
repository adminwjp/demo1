
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
    public class IdentityUserRoleEntityConfiguration : IEntityTypeConfiguration<IdentityUserRole<string>>
    {
        public void Configure(EntityTypeBuilder<IdentityUserRole<string>> builder)
        {

            builder.HasKey( r => new
            {
                r.UserId,
                r.RoleId
            });
            builder.Property(u => u.UserId).HasMaxLength(IdentityServer4Config.KeyMaxLength);
            builder.Property( u => u.RoleId).HasMaxLength(IdentityServer4Config.KeyMaxLength);
            builder.ToTable("AspNetUserRoles");
        }

    }
}
