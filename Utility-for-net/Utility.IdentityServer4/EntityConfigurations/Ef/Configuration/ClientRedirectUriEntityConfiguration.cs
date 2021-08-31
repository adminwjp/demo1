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
    public class ClientRedirectUriEntityConfiguration : IEntityTypeConfiguration<ClientRedirectUri>
    {
        public void Configure(EntityTypeBuilder<ClientRedirectUri> redirectUri)
        {
            IdentityServer4Config.ToTable(redirectUri, IdentityServer4Config.ConfigurationStore.ClientRedirectUri);
            redirectUri.HasKey(it => it.Id);
            redirectUri.Property( x => x.RedirectUri).HasMaxLength(2000).IsRequired();
        }
    }
}
