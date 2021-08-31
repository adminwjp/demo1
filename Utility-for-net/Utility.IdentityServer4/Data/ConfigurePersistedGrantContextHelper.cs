using IdentityServer4.EntityFramework.Entities;
using IdentityServer4.EntityFramework.Options;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Utility.IdentityServer4.EntityConfigurations;

namespace Utility.IdentityServer.Data
{
    public class ConfigurePersistedGrantContextHelper
    {
        public static EntityTypeBuilder<TEntity> ToTable<TEntity>( EntityTypeBuilder<TEntity> entityTypeBuilder, TableConfiguration configuration) where TEntity : class
        {
            if (!string.IsNullOrWhiteSpace(configuration.Schema))
            {
                return entityTypeBuilder.ToTable(configuration.Name, configuration.Schema);
            }

            return entityTypeBuilder.ToTable(configuration.Name);
        }

        public static void ConfigurePersistedGrantContext( ModelBuilder modelBuilder, OperationalStoreOptions storeOptions)
        {
            if (!string.IsNullOrWhiteSpace(storeOptions.DefaultSchema))
            {
                modelBuilder.HasDefaultSchema(storeOptions.DefaultSchema);
            }

            modelBuilder.ApplyConfiguration(new PersistedGrantEntityConfiguration());
            modelBuilder.ApplyConfiguration(new DeviceFlowCodesEntityConfiguration());
        }

    }
}
