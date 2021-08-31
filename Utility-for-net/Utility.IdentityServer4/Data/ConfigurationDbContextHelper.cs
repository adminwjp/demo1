using IdentityServer4.EntityFramework.Entities;
using IdentityServer4.EntityFramework.Options;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Utility.EntityConfigurations;
using Utility.IdentityServer4.EntityConfigurations;

namespace Utility.IdentityServer.Data
{
    public static class ConfigurationDbContextHelper
    {
        public static EntityTypeBuilder<TEntity> ToTable<TEntity>(this EntityTypeBuilder<TEntity> entityTypeBuilder, TableConfiguration configuration) where TEntity : class
        {
            if (!string.IsNullOrWhiteSpace(configuration.Schema))
            {
                return entityTypeBuilder.ToTable(configuration.Name, configuration.Schema);
            }

            return entityTypeBuilder.ToTable(configuration.Name);
        }
        public static void ConfigureClientContext( ModelBuilder modelBuilder, ConfigurationStoreOptions storeOptions)
        {
            if (!string.IsNullOrWhiteSpace(storeOptions.DefaultSchema))
            {
                modelBuilder.HasDefaultSchema(storeOptions.DefaultSchema);
            }

            modelBuilder.ApplyConfiguration(new ClientEntityConfiguration());
            modelBuilder.ApplyConfiguration(new ClientGrantTypeEntityConfiguration());
            modelBuilder.ApplyConfiguration(new ClientRedirectUriEntityConfiguration());
            modelBuilder.ApplyConfiguration(new ClientPostLogoutRedirectUriEntityConfiguration());
            modelBuilder.ApplyConfiguration(new ClientScopeEntityConfiguration());
            modelBuilder.ApplyConfiguration(new ClientSecretEntityConfiguration());
            modelBuilder.ApplyConfiguration(new ClientClaimEntityConfiguration());
            modelBuilder.ApplyConfiguration(new ClientIdPRestrictionEntityConfiguration());
            modelBuilder.ApplyConfiguration(new ClientCorsOriginEntityConfiguration());
            modelBuilder.ApplyConfiguration(new ClientPropertyEntityConfiguration());
        }


        public static void ConfigureResourcesContext( ModelBuilder modelBuilder, ConfigurationStoreOptions storeOptions)
        {
            if (!string.IsNullOrWhiteSpace(storeOptions.DefaultSchema))
            {
                modelBuilder.HasDefaultSchema(storeOptions.DefaultSchema);
            }

            modelBuilder.ApplyConfiguration(new IdentityResourceEntityConfiguration());
            modelBuilder.ApplyConfiguration(new IdentityResourceClaimEntityConfiguration());
            modelBuilder.ApplyConfiguration(new IdentityResourcePropertyEntityConfiguration());
            modelBuilder.ApplyConfiguration(new ApiResourceEntityConfiguration());
            modelBuilder.ApplyConfiguration(new ApiResourceSecretEntityConfiguration());
            modelBuilder.ApplyConfiguration(new ApiResourceClaimEntityConfiguration());
            modelBuilder.ApplyConfiguration(new ApiResourceScopeEntityConfiguration());
            modelBuilder.ApplyConfiguration(new ApiScopePropertyEntityConfiguration());
            modelBuilder.ApplyConfiguration(new ApiScopeEntityConfiguration());
            modelBuilder.ApplyConfiguration(new ApiScopeClaimEntityConfiguration());
            modelBuilder.ApplyConfiguration(new ApiScopePropertyEntityConfiguration());
        }
    }
}
