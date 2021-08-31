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
    public class ClientPostLogoutRedirectUriMapping : IEntityTypeConfiguration<ClientPostLogoutRedirectUri>
    {
        public void Configure(EntityTypeBuilder<ClientPostLogoutRedirectUri> postLogoutRedirectUri)
        {
            IdentityServer4Config.ToTable(postLogoutRedirectUri,IdentityServer4Config.ConfigurationStore.ClientPostLogoutRedirectUri);
            postLogoutRedirectUri.Property((Expression<Func<ClientPostLogoutRedirectUri, string>>)((ClientPostLogoutRedirectUri x) => x.PostLogoutRedirectUri)).HasMaxLength(2000).IsRequired();
        }
    }
}
