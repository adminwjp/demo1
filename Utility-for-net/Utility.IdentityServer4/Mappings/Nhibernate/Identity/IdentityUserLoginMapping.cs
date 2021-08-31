
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
    public class IdentityUserLoginMapping : IEntityTypeConfiguration<IdentityUserLogin<string>>
    {
        public void Configure(EntityTypeBuilder<IdentityUserLogin<string>> builder)
        {
            builder.HasKey((Expression<Func<IdentityUserLogin<string>, object>>)((IdentityUserLogin<string> l) => new
            {
                l.LoginProvider,
                l.ProviderKey
            }));
            builder.Property((Expression<Func<IdentityUserLogin<string>, string>>)((IdentityUserLogin<string> l) => l.LoginProvider)).HasMaxLength(IdentityServer4Config.KeyMaxLength);
            builder.Property((Expression<Func<IdentityUserLogin<string>, string>>)((IdentityUserLogin<string> l) => l.ProviderKey)).HasMaxLength(IdentityServer4Config.KeyMaxLength);
            
            builder.Property((Expression<Func<IdentityUserLogin<string>, string>>)((IdentityUserLogin<string> l) => l.UserId)).HasMaxLength(IdentityServer4Config.KeyMaxLength);
            builder.ToTable("AspNetUserLogins");
        }

    }
}
