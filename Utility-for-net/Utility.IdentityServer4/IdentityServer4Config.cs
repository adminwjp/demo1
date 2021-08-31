using IdentityServer4.EntityFramework.Options;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Utility.IdentityServer4
{
    public enum IdentityServer4Version
    {
        V3,
        V4
    }
    public enum EfCoreVersion
    {
        V3,
        V5
    }
    public class IdentityServer4Config
    {
        public static IdentityServer4Version Version = IdentityServer4Version.V3;
        public static EfCoreVersion EfVersion = EfCoreVersion.V5;
        /// <summary>
        /// mysql Specified key was too long; max key length is 767 bytes 默认 统一 给 36 
        /// </summary>

        public static int KeyMaxLength = 36;
        public static bool InMemory = false;
        public static Driver Driver = Driver.MySql5_5;
        public static OperationalStoreOptions OperationalStore = new OperationalStoreOptions();
        public static ConfigurationStoreOptions ConfigurationStore = new ConfigurationStoreOptions();
        public static EntityTypeBuilder<TEntity> ToTable<TEntity>(EntityTypeBuilder<TEntity> entityTypeBuilder, TableConfiguration configuration) where TEntity : class
        {
            if (!string.IsNullOrWhiteSpace(configuration.Schema))
            {
                return entityTypeBuilder.ToTable(configuration.Name, configuration.Schema);
            }

            return entityTypeBuilder.ToTable(configuration.Name);
        }
    }
    public enum Driver
    {
        MySql5_5,
        MySql,
        Sqlite,
        Postgre,
        SqlServer,
        Oracle
    }
}
