
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
    public class IdentityUserLoginEntityConfiguration : IEntityTypeConfiguration<IdentityUserLogin<string>>
    {
        public void Configure(EntityTypeBuilder<IdentityUserLogin<string>> builder)
        {
            // requires a primary key to be defined. If you intended to use a keyless
            //if(IdentityServer4Config.EfVersion== EfCoreVersion.V5)
            {
                //https://docs.microsoft.com/zh-cn/ef/core/modeling/keyless-entity-types?tabs=fluent-api
                 //builder.HasNoKey();
                // builder.HasKey(l => l.UserId);

            }
            //else
            {
                builder.HasKey(l => new
                {
                    l.LoginProvider,
                    l.ProviderKey
                });
            }
            builder.Property(l => l.LoginProvider).HasMaxLength(IdentityServer4Config.KeyMaxLength);
            builder.Property(l => l.ProviderKey).HasMaxLength(IdentityServer4Config.KeyMaxLength);
            
            builder.Property(l=> l.UserId).HasMaxLength(IdentityServer4Config.KeyMaxLength);
            builder.ToTable("AspNetUserLogins");
        }

    }
}
