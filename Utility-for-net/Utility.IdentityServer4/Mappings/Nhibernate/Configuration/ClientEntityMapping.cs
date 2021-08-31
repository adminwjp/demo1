
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
    public class ClientEntityMapping : IEntityTypeConfiguration<Client>
    {
        public void Configure(EntityTypeBuilder<Client> client)
        {
            IdentityServer4Config.ToTable(client, IdentityServer4Config.ConfigurationStore.Client);
            client.HasKey((Expression<Func<Client, object>>)((Client x) => x.Id));
            client.Property((Expression<Func<Client, string>>)((Client x) => x.ClientId)).HasMaxLength(200).IsRequired();
            client.Property((Expression<Func<Client, string>>)((Client x) => x.ProtocolType)).HasMaxLength(200).IsRequired();
            client.Property((Expression<Func<Client, string>>)((Client x) => x.ClientName)).HasMaxLength(200);
            client.Property((Expression<Func<Client, string>>)((Client x) => x.ClientUri)).HasMaxLength(2000);
            client.Property((Expression<Func<Client, string>>)((Client x) => x.LogoUri)).HasMaxLength(2000);
            client.Property((Expression<Func<Client, string>>)((Client x) => x.Description)).HasMaxLength(1000);
            client.Property((Expression<Func<Client, string>>)((Client x) => x.FrontChannelLogoutUri)).HasMaxLength(2000);
            client.Property((Expression<Func<Client, string>>)((Client x) => x.BackChannelLogoutUri)).HasMaxLength(2000);
            client.Property((Expression<Func<Client, string>>)((Client x) => x.ClientClaimsPrefix)).HasMaxLength(200);
            client.Property((Expression<Func<Client, string>>)((Client x) => x.PairWiseSubjectSalt)).HasMaxLength(200);
            client.Property((Expression<Func<Client, string>>)((Client x) => x.UserCodeType)).HasMaxLength(100);
            //列过长报错
            if(IdentityServer4Config.Driver== Driver.Oracle)
            {
                client.Property((Expression<Func<Client, bool>>)((Client x) => x.AlwaysIncludeUserClaimsInIdToken)).HasColumnName("AIUCIIT");
                client.Property((Expression<Func<Client, string>>)((Client x) => x.AllowedIdentityTokenSigningAlgorithms)).HasColumnName("AITSA").HasMaxLength(100);//列过长会报错 
                client.Property((Expression<Func<Client, bool>>)((Client x) => x.FrontChannelLogoutSessionRequired)).HasColumnName("FCLSR");
                client.Property((Expression<Func<Client, bool>>)((Client x) => x.UpdateAccessTokenClaimsOnRefresh)).HasColumnName("UATCOR");
                client.Property((Expression<Func<Client, bool>>)((Client x) => x.BackChannelLogoutSessionRequired)).HasColumnName("BCLSR");
            }
            else
            {
                client.Property((Expression<Func<Client, string>>)((Client x) => x.AllowedIdentityTokenSigningAlgorithms)).HasMaxLength(100);
            }
            client.HasIndex((Expression<Func<Client, object>>)((Client x) => x.ClientId)).IsUnique();

            //MySql || SqlServer
            if (IdentityServer4Config.Driver== Driver.MySql5_5)
            {
                //mysql
                client.Property((Expression<Func<Client, DateTime>>)((Client x) => x.Created)).HasColumnType("datetime");
                client.Property((Expression<Func<Client, DateTime?>>)((Client x) => x.Updated)).HasColumnType("datetime");
                client.Property((Expression<Func<Client, DateTime?>>)((Client x) => x.LastAccessed)).HasColumnType("datetime");
            }
            client.HasMany((Expression<Func<Client, IEnumerable<ClientGrantType>>>)((Client x) => x.AllowedGrantTypes)).WithOne((Expression<Func<ClientGrantType, Client>>)((ClientGrantType x) => x.Client)).HasForeignKey((Expression<Func<ClientGrantType, object>>)((ClientGrantType x) => x.ClientId))
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade);
            client.HasMany((Expression<Func<Client, IEnumerable<ClientRedirectUri>>>)((Client x) => x.RedirectUris)).WithOne((Expression<Func<ClientRedirectUri, Client>>)((ClientRedirectUri x) => x.Client)).HasForeignKey((Expression<Func<ClientRedirectUri, object>>)((ClientRedirectUri x) => x.ClientId))
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade);
            client.HasMany((Expression<Func<Client, IEnumerable<ClientPostLogoutRedirectUri>>>)((Client x) => x.PostLogoutRedirectUris)).WithOne((Expression<Func<ClientPostLogoutRedirectUri, Client>>)((ClientPostLogoutRedirectUri x) => x.Client)).HasForeignKey((Expression<Func<ClientPostLogoutRedirectUri, object>>)((ClientPostLogoutRedirectUri x) => x.ClientId))
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade);
            client.HasMany((Expression<Func<Client, IEnumerable<ClientScope>>>)((Client x) => x.AllowedScopes)).WithOne((Expression<Func<ClientScope, Client>>)((ClientScope x) => x.Client)).HasForeignKey((Expression<Func<ClientScope, object>>)((ClientScope x) => x.ClientId))
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade);
            client.HasMany((Expression<Func<Client, IEnumerable<ClientSecret>>>)((Client x) => x.ClientSecrets)).WithOne((Expression<Func<ClientSecret, Client>>)((ClientSecret x) => x.Client)).HasForeignKey((Expression<Func<ClientSecret, object>>)((ClientSecret x) => x.ClientId))
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade);
            client.HasMany((Expression<Func<Client, IEnumerable<ClientClaim>>>)((Client x) => x.Claims)).WithOne((Expression<Func<ClientClaim, Client>>)((ClientClaim x) => x.Client)).HasForeignKey((Expression<Func<ClientClaim, object>>)((ClientClaim x) => x.ClientId))
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade);
            client.HasMany((Expression<Func<Client, IEnumerable<ClientIdPRestriction>>>)((Client x) => x.IdentityProviderRestrictions)).WithOne((Expression<Func<ClientIdPRestriction, Client>>)((ClientIdPRestriction x) => x.Client)).HasForeignKey((Expression<Func<ClientIdPRestriction, object>>)((ClientIdPRestriction x) => x.ClientId))
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade);
            client.HasMany((Expression<Func<Client, IEnumerable<ClientCorsOrigin>>>)((Client x) => x.AllowedCorsOrigins)).WithOne((Expression<Func<ClientCorsOrigin, Client>>)((ClientCorsOrigin x) => x.Client)).HasForeignKey((Expression<Func<ClientCorsOrigin, object>>)((ClientCorsOrigin x) => x.ClientId))
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade);
            client.HasMany((Expression<Func<Client, IEnumerable<ClientProperty>>>)((Client x) => x.Properties)).WithOne((Expression<Func<ClientProperty, Client>>)((ClientProperty x) => x.Client)).HasForeignKey((Expression<Func<ClientProperty, object>>)((ClientProperty x) => x.ClientId))
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
