
using IdentityServer4.EntityFramework.DbContexts;
using IdentityServer4.EntityFramework.Extensions;
using IdentityServer4.EntityFramework.Options;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.Extensions.Options;
using System;
using System.Linq;
using System.Reflection;
using Microsoft.Extensions.DependencyInjection;
using Utility.IdentityServer.Models;

namespace Utility.IdentityServer.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        private StoreOptions GetStoreOptions()
        {
            return this.GetService<IDbContextOptions>().Extensions.OfType<CoreOptionsExtension>().FirstOrDefault()?.ApplicationServiceProvider?.GetService<IOptions<IdentityOptions>>()?.Value?.Stores;
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            //base.OnModelCreating(builder);
            //return;
            //MySqlException: Table 'IdentityServer.AspNetUserTokens' doesn't exist
            StoreOptions storeOptions = GetStoreOptions();
            if (storeOptions != null)
            {
                storeOptions.MaxLengthForKeys = 36;
            }
            IdentityDbContextHelper.OnModelCreating(builder, storeOptions, null);//sqlite 不受影响 
            // base.OnModelCreating(builder);

            // Customize the ASP.NET Identity model and override the defaults if needed.
            // For example, you can rename the ASP.NET Identity table names and more.
            // Add your customizations after calling base.OnModelCreating(builder);
        }
    }
    public class ConfigurationDbContextWrapper: ConfigurationDbContext<ConfigurationDbContextWrapper>
    {
        private readonly ConfigurationStoreOptions storeOptions;
        public ConfigurationDbContextWrapper(DbContextOptions<ConfigurationDbContextWrapper> options, ConfigurationStoreOptions storeOptions)
           : base(options, storeOptions)
        {
            this.storeOptions = storeOptions;
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //modelBuilder.ConfigureClientContext(storeOptions);
            //modelBuilder.ConfigureResourcesContext(storeOptions);
            if(IdentityServer4.IdentityServer4Config.Driver== IdentityServer4.Driver.Oracle)
            {
                ConfigurationDbContextHelper.ConfigureClientContext(modelBuilder, storeOptions);
                ConfigurationDbContextHelper.ConfigureResourcesContext(modelBuilder, storeOptions);
            }
            else
            {
                base.OnModelCreating(modelBuilder);
            }

        }
    }
    /// <summary>
    /// 这个 可以 迁移 其他(PersistedGrantDbContextDesignTimeDbContextFactory,ConfigurationDbContextDesignTimeDbContextFactory) 的无法 迁移 之前的版本 可以迁移 
    /// 不需要 IDesignTimeDbContextFactory
    /// </summary>
    public class ApplicationDbContextDesignTimeDbContextFactory : IDesignTimeDbContextFactory<ApplicationDbContext>
    {
        public ApplicationDbContext CreateDbContext(string[] args)
        {
            var bulder = new DbContextOptionsBuilder<ApplicationDbContext>();
            bulder.UseMySql("Database=IdentityServer;Data Source=localhost;User Id=root;Password=wjp930514.;Old Guids=True;charset=utf8;",null);
            return new ApplicationDbContext(bulder.Options);
        }
    }
    public class PersistedGrantDbContextDesignTimeDbContextFactory : IDesignTimeDbContextFactory<PersistedGrantDbContext>
    {
        public PersistedGrantDbContext CreateDbContext(string[] args)
        {
            var migrationsAssembly = typeof(ApplicationDbContext).GetTypeInfo().Assembly.GetName().Name;
            Func<DbContextOptionsBuilder, DbContextOptionsBuilder> func = Startup
                .GetFunc("Database=IdentityServer;Data Source=localhost;User Id=root;Password=wjp930514.;Old Guids=True;charset=utf8;", migrationsAssembly);
            OperationalStoreOptions options = new OperationalStoreOptions();
            options.ConfigureDbContext = builder => func(builder);

            var bulder = new DbContextOptionsBuilder<PersistedGrantDbContext>();
            bulder.UseMySql("Database=IdentityServer;Data Source=localhost;User Id=root;Password=wjp930514.;Old Guids=True;charset=utf8;", null);
            return new PersistedGrantDbContext(bulder.Options, options);
        }
    }
    public class ConfigurationDbContextDesignTimeDbContextFactory : IDesignTimeDbContextFactory<ConfigurationDbContext>
    {
        public ConfigurationDbContext CreateDbContext(string[] args)
        {
            var migrationsAssembly = typeof(Startup).GetTypeInfo().Assembly.GetName().Name;
            Func<DbContextOptionsBuilder, DbContextOptionsBuilder> func = Startup
                .GetFunc("Database=IdentityServer;Data Source=localhost;User Id=root;Password=wjp930514.;Old Guids=True;charset=utf8;", migrationsAssembly);
            var bulder = new DbContextOptionsBuilder<ConfigurationDbContext>();

            bulder.UseMySql("Database=IdentityServer;Data Source=localhost;User Id=root;Password=wjp930514.;Old Guids=True;charset=utf8;",null);
            ConfigurationStoreOptions options = new ConfigurationStoreOptions();
            options.ConfigureDbContext = builder => func(builder);
            return new ConfigurationDbContext(bulder.Options, options);
        }
    }
}
