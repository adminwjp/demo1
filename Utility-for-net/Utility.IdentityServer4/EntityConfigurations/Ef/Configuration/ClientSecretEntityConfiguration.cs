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
    public class ClientSecretEntityConfiguration : IEntityTypeConfiguration<ClientSecret>
    {
        public void Configure(EntityTypeBuilder<ClientSecret> secret)
        {
            //MySql || SqlServer
            if (IdentityServer4Config.Driver== Driver.MySql5_5)
            {
                //mysql
                secret.Property(x => x.Created).HasColumnType("datetime");
                secret.Property(x => x.Expiration).HasColumnType("datetime");
            }

            secret.HasKey(it => it.Id);
            IdentityServer4Config.ToTable(secret,IdentityServer4Config.ConfigurationStore.ClientSecret);
            secret.Property(x => x.Value).HasMaxLength(4000).IsRequired();
            secret.Property(x => x.Type).HasMaxLength(250).IsRequired();
            secret.Property(x => x.Description).HasMaxLength(2000);
        }
    }
}
