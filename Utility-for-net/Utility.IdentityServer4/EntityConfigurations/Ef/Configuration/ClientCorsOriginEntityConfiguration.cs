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
    public class ClientCorsOriginEntityConfiguration : IEntityTypeConfiguration<ClientCorsOrigin>
    {
        public void Configure(EntityTypeBuilder<ClientCorsOrigin> corsOrigin)
        {
            IdentityServer4Config.ToTable(corsOrigin,IdentityServer4Config.ConfigurationStore.ClientCorsOrigin);
            corsOrigin.HasKey(it => it.Id);
            corsOrigin.Property(x => x.Origin).HasMaxLength(150).IsRequired();
        }
    }
}
