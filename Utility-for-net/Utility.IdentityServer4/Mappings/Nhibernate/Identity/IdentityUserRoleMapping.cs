
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

namespace Utility.IdentityServer4.Mappings
{
    public class IdentityUserRoleMapping : IEntityTypeConfiguration<IdentityUserRole<string>>
    {
        public void Configure(EntityTypeBuilder<IdentityUserRole<string>> builder)
        {

            builder.HasKey((Expression<Func<IdentityUserRole<string>, object>>)((IdentityUserRole<string> r) => new
            {
                r.UserId,
                r.RoleId
            }));
            builder.Property((Expression<Func<IdentityUserRole<string>, string>>)((IdentityUserRole<string> u) => u.UserId)).HasMaxLength(IdentityServer4Config.KeyMaxLength);
            builder.Property((Expression<Func<IdentityUserRole<string>, string>>)((IdentityUserRole<string> u) => u.RoleId)).HasMaxLength(IdentityServer4Config.KeyMaxLength);
            builder.ToTable("AspNetUserRoles");
        }

    }
}
