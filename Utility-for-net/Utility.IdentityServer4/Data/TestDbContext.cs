
using IdentityServer4.EntityFramework.Entities;
using IdentityServer4.EntityFramework.Extensions;
using IdentityServer4.EntityFramework.Options;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using System;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using Utility.IdentityServer.Models;
using Utility.IdentityServer4;

namespace Utility.IdentityServer.Data
{
    /// <summary>
    /// 统一 创建 数据库 表 用到的映射 
    /// </summary>
    public class TestDbContextDesignTimeDbContextFactory : IDesignTimeDbContextFactory<TestDbContext>
    {
        public TestDbContext CreateDbContext(string[] args)
        {
            var bulder = new DbContextOptionsBuilder<TestDbContext>();
            if (IdentityServer4Config.Driver == Driver.MySql
                || IdentityServer4Config.Driver == Driver.MySql5_5)
            {
                var conn = "Database=IdentityServer;Data Source=localhost;User Id=root;Password=wjp930514.;Old Guids=True;charset=utf8;";
                bulder.UseMySql(conn,
                    ServerVersion.AutoDetect(conn)
                   // null
                    );
            }
            else  if (IdentityServer4Config.Driver == Driver.Sqlite)
                bulder.UseSqlite("Data Source=IdentityServer.db;");
            else if (IdentityServer4Config.Driver == Driver.SqlServer)
                bulder.UseSqlServer("server=192.168.99.101;database=IdentityServer;user=sa;pwd=wjp930514.;");
            else if (IdentityServer4Config.Driver == Driver.Oracle)
                //bulder.UseOracle("DATA SOURCE=192.168.99.101:1521/orcl;USER ID=sys;PASSWORD=oracle;");
                bulder.UseOracle("DATA SOURCE=192.168.99.101:1521/orcl;USER ID=test;PASSWORD=123456;",
                sqlOptions => sqlOptions.UseOracleSQLCompatibility("12"));
            else if (IdentityServer4Config.Driver == Driver.Postgre)
                bulder.UseNpgsql("User ID=postgres;Password=wjp930514.;Host=localhost;Port=5432;Database=IdentityServer;Pooling=true;");

            return new TestDbContext(bulder.Options);
        }
    }

    /// <summary>
    /// 注意 mysql datetime(6) 改为 datetime
    /// id int or string 要么 重写  最好 统一 
    /// Specified key was too long; max key length is 767 bytes 表语法错误 难 找 问题 ,没 nhibernate 创建 表 灵活 
    /// 出错 在没映射 的实体  检查 外键 关系(实体 主键) 暂时 用的  IdentityServer 4.1.1 版本
    /// Specified key was too long; max key length is 767 bytes mysql 该 错误 忽略 具体 哪里 报错 不知道 实际使用 不影响 
    /// </summary>
    public class TestDbContext:DbContext
    {
        //ef core3.x pass
        //ex core 5.x warn
        // pass but table exists
        public TestDbContext(DbContextOptions<TestDbContext> options)
          : base(options)
        {
            if( IdentityServer4Config.Driver== Driver.Oracle)
                 Database.EnsureDeleted();
            if (IdentityServer4Config.Driver == Driver.Sqlite)
            {
                if (Database.EnsureCreated())//oracle ORA-00972: 标识符过长 
                {
                    Debug.WriteLine("create database ");
                }
            }
        }
        //IdentityDbContext<TUser, TRole, TKey, TUserClaim, TUserRole, TUserLogin, TRoleClaim, TUserToken>
       public virtual DbSet<IdentityUserRole<string>> UserRoles
        {
            get;
            set;
        }

      public virtual DbSet<IdentityRole> Roles
        {
            get;
            set;
        }

        //IdentityUserContext<TUser, TKey, TUserClaim, TUserLogin, TUserToken>
         public virtual DbSet<IdentityRoleClaim<string>> RoleClaims
        {
            get;
            set;
        }


        public virtual DbSet<ApplicationUser> Users
        {
            get;
            set;
        }

      public virtual DbSet<IdentityUserClaim<string>> UserClaims
        {
            get;
            set;
        }

     public virtual DbSet<IdentityUserLogin<string>> UserLogins
        {
            get;
            set;
        }

       public virtual DbSet<IdentityUserToken<string>> UserTokens
        {
            get;
            set;
        }

        //PersistedGrantDbContext<TContext> 

        public DbSet<PersistedGrant> PersistedGrants
        {
            get;
            set;
        }

        public DbSet<DeviceFlowCodes> DeviceFlowCodes
        {
            get;
            set;
        }


        public DbSet<Client> Clients
        {
            get;
            set;
        }

        public DbSet<ClientCorsOrigin> ClientCorsOrigins
        {
            get;
            set;
        }

        public DbSet<IdentityResource> IdentityResources
        {
            get;
            set;
        }

        public DbSet<ApiResource> ApiResources
        {
            get;
            set;
        }

        public DbSet<ApiScope> ApiScopes
        {
            get;
            set;
        }

        private StoreOptions GetStoreOptions()
        {
            return this.GetService<IDbContextOptions>().Extensions.OfType<CoreOptionsExtension>().FirstOrDefault()?.ApplicationServiceProvider?.GetService<IOptions<IdentityOptions>>()?.Value?.Stores;
        }

    
        protected override void OnModelCreating(ModelBuilder builder)
        {
            // base.OnModelCreating(builder);
            // return;
            //判断当前数据库是Oracle
            if (Database.IsOracle())
            {
                //如果使用Oracle必须手动添加Schema，判断当前数据库是Oracle 需要手动添加Schema(DBA提供的数据库账号名称)
                builder.HasDefaultSchema("TEST");//注意：DBA提供的数据库账号名称，必须大写
            }
            //IdentityDbContext<ApplicationUser, IdentityRole, string>
            //IdentityUserContext<TUser, TKey, TUserClaim, TUserLogin, TUserToken>
            StoreOptions storeOptions = GetStoreOptions();
            if (storeOptions != null)
            {
                storeOptions.MaxLengthForKeys = 36;
            }
            //重写 解决 mysql  注意 mysql datetime(6) 改为 datetime Specified key was too long; max key length is 767 bytes 默认 统一 给 36 
            //sqlserver requires a primary key to be defined. If you intended to use a keyless entity type call 'HasNoKey()'. 必须   要 HasNoKey
            //注意 key mysql sqlite 不映射 没关系 sqlserver 必须要映射 key 不统一 长度 算 了 不要mapp映射 直接 使用 映射 key 不符合规则 还是 必须 要 mapp 内部没有用注解 用的 mapp
            //统一长度
            //sqlserver The property 'ApplicationUser.LockoutEnd' is of type 'Nullable<DateTimeOffset>' which is not supported by current database provider. Either change the property CLR type or ignore 
            //the property using the '[NotMapped]' attribute or by using 'EntityTypeBuilder.Ignore' in 'OnModelCreating'.
            //postgre requires a primary key to be defined. If you intended to use a keyless entity type call 'HasNoKey()'. 必须  声明 key
            //datatime mysql sqlserver support
            //oracle  requires a primary key to be defined. If you intended to use a keyless entity type call 'HasNoKey()'. 必须  声明 key ORA-00972: 标识符过长 字符集问题 docker oracle 改不成功 麻烦坑多
            //key 不一定 要给 最好给  

            //IdentityDbContextHelper.OnModelCreating(builder, storeOptions, this.GetService<IPersonalDataProtector>()); 
            //if (IdentityServer4Config.Driver != Driver.Sqlite)
            {
                IdentityDbContextHelper.OnModelCreating(builder, storeOptions, null);//sqlite 不受影响 
            }


            var migrationsAssembly = typeof(Startup).GetTypeInfo().Assembly.GetName().Name;
            Func<DbContextOptionsBuilder, DbContextOptionsBuilder> func = it =>
            {

                if (IdentityServer4Config.Driver == Driver.MySql)
                    return it.UseMySql("Database=IdentityServer;Data Source=localhost;User Id=root;Password=wjp930514.;Old Guids=True;charset=utf8;",
//#if !NETCOREAPP3_1
                        null, 
//#endif
                        sqlOptions => sqlOptions.MigrationsAssembly(migrationsAssembly)
                );
                else if (IdentityServer4Config.Driver == Driver.Sqlite)
                    return it.UseSqlite(@"Data Source=IdentityServer.db;"
               , sqlOptions => sqlOptions.MigrationsAssembly(migrationsAssembly)
                );//迁移不支持
                else if (IdentityServer4Config.Driver == Driver.SqlServer)
                    return it.UseSqlServer("server=192.168.99.101;database=IdentityServer;user=sa;pwd=wjp930514."
                    , sqlOptions => sqlOptions.MigrationsAssembly(migrationsAssembly)
                    );//docker sqlserver
                else if (IdentityServer4Config.Driver == Driver.Postgre)
                    return it.UseNpgsql("User ID=postgres;Password=wjp930514.;Host=localhost;Port=5432;Database=IdentityServer;Pooling=true;"
                , sqlOptions => sqlOptions.MigrationsAssembly(migrationsAssembly)
               );
                else if (IdentityServer4Config.Driver == Driver.Oracle)
                    return it.UseOracle(
                    //"DATA SOURCE=192.168.99.101:1521/orcl;USER ID=sys;PASSWORD=oracle;"
                    "DATA SOURCE=192.168.99.101:1521/orcl;USER ID=test;PASSWORD=123456;"
                    , sqlOptions => sqlOptions.MigrationsAssembly(migrationsAssembly).UseOracleSQLCompatibility("12")
                    );//docker sqlserver
                else
                    return null;
            };
            OperationalStoreOptions options = new OperationalStoreOptions();
            options.ConfigureDbContext = builder => func(builder);

            //PersistedGrantDbContext<TContext> 
            if (IdentityServer4Config.Driver != Driver.Sqlite)
                ConfigurePersistedGrantContextHelper.ConfigurePersistedGrantContext(builder, options);
            else
                ModelBuilderExtensions.ConfigurePersistedGrantContext(builder, options);

            //ConfigurationDbContext<TContext>
            ConfigurationStoreOptions configurationStoreOptions = new ConfigurationStoreOptions();
            configurationStoreOptions.ConfigureDbContext = builder => func(builder);
            if (IdentityServer4Config.Driver != Driver.Sqlite)
            {
                ConfigurationDbContextHelper.ConfigureClientContext(builder, configurationStoreOptions);
            }
            else
            {
                ModelBuilderExtensions.ConfigureClientContext(builder, configurationStoreOptions);
                ModelBuilderExtensions.ConfigureResourcesContext(builder, configurationStoreOptions);
            }
        }
    }
}
