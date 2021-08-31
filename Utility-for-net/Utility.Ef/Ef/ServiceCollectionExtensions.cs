#if NETCOREAPP2_0 || NETCOREAPP2_1 || NETCOREAPP2_2 || NETCOREAPP3_0 || NETCOREAPP3_1 || NET5_0 || NET6_0 || NETSTANDARD2_0 || NETSTANDARD2_1
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Pomelo.EntityFrameworkCore.MySql.Storage;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Text;

namespace Utility.Ef
{
    /// <summary>
    /// ServiceCollection 扩展类
    /// </summary>
    public static class ServiceCollectionExtensions
    {

        /// <summary>
        /// 创建数据库上下文 默认 使用连接池
        /// </summary>
        /// <typeparam name="Db"></typeparam>
        /// <param name="services">ioc 服务</param>
        /// <param name="version">ef core net standard 2.1 mysql</param>
        public static void UseEf<Db>(this IServiceCollection services, string version="") where Db : DbContext
        {
            services.UseEf<Db>(ConfigHelper.ConnectionString, version);
        }

        /// <summary>
        /// 创建数据库上下文 默认 使用连接池
        /// </summary>
        /// <typeparam name="Db"></typeparam>
        /// <param name="services">ioc 服务</param>
        /// <param name="connectionString">数据库连接地址</param>
        /// <param name="version">ef core net standard 2.1 mysql</param>
        public static void UseEf<Db>(this IServiceCollection services, string connectionString, string version="") where Db : DbContext{
			services.UseEf<Db>(connectionString,"", ConfigHelper.DbFlag, ConfigHelper.IsAbpEf,version);
		}

        /// <summary>
        /// 创建数据库上下文 默认 使用连接池
        /// </summary>
        /// <typeparam name="Db"></typeparam>
        /// <param name="services">ioc 服务</param>
        /// <param name="connectionString">数据库连接地址</param>
        /// <param name="migrationsAssembly">迁移程序集名称 不需要</param>
        /// <param name="flag">数据库标识</param>
        /// <param name="abp">是否使用 abp 框架</param>
        /// <param name="version">ef core net standard 2.1 mysql</param>
        public static void UseEf<Db>(this IServiceCollection services, string connectionString,string migrationsAssembly, DbFlag flag,bool abp,string version) where Db : DbContext
        {
            string sqlConnectionString = connectionString;
            //var migrationsAssembly = typeof(Startup).GetTypeInfo().Assembly.GetName().Name;
            //ef db
            Func<DbContextOptionsBuilder, DbContextOptionsBuilder> func = options =>
            {
                // services.AddSingleton<DbContextOptions>(options.Options);
                switch (flag)
                {
                    case DbFlag.MySql:
                        //options.UseMySql(sqlConnectionString, ServerVersion.FromString("mysql"));
                        return options.UseMySql(sqlConnectionString,
#if NETSTANDARD2_1
                            ServerVersion.AutoDetect(sqlConnectionString),
#endif
                            mySqlOptionsAction: sqlOptions =>
                            {
                                if(!string.IsNullOrEmpty(migrationsAssembly))
                                {
                                    sqlOptions.MigrationsAssembly(migrationsAssembly);
                                }
                                //Configuring Connection Resiliency: https://docs.microsoft.com/en-us/ef/core/miscellaneous/connection-resiliency 

                                //abp MySqlRetryingExecutionStrategy' does not support user initiated transactions. 
                                //Use the execution strategy returned by 'DbContext.Database.CreateExecutionStrategy()
                                if(!abp)
                                {
                                    sqlOptions.EnableRetryOnFailure(maxRetryCount: 15, maxRetryDelay: TimeSpan.FromSeconds(30), errorNumbersToAdd: null);
                                }
                                
                            });
                    case DbFlag.SqlServer:
                        return options.UseSqlServer(sqlConnectionString,
                            sqlServerOptionsAction: sqlOptions =>
                            {
                                if (!string.IsNullOrEmpty(migrationsAssembly))
                                {
                                    sqlOptions.MigrationsAssembly(migrationsAssembly);
                                }
                                //Configuring Connection Resiliency: https://docs.microsoft.com/en-us/ef/core/miscellaneous/connection-resiliency 

                                //abp MySqlRetryingExecutionStrategy' does not support user initiated transactions. 
                                //Use the execution strategy returned by 'DbContext.Database.CreateExecutionStrategy()
                                if (!abp)
                                {
                                    sqlOptions.EnableRetryOnFailure(maxRetryCount: 15, maxRetryDelay: TimeSpan.FromSeconds(30), errorNumbersToAdd: null);
                                }
                            });
                    case DbFlag.Oracle:
#if NETCOREAPP2_0 || NETCOREAPP2_1 || NETCOREAPP2_2
                         throw new NotSupportedException("not support database ");
#else
                         return options.UseOracle(sqlConnectionString,
                            oracleOptionsAction: sqlOptions =>
                            {
                                if (!string.IsNullOrEmpty(migrationsAssembly))
                                {
                                    sqlOptions.MigrationsAssembly(migrationsAssembly);
                                }
                                //Configuring Connection Resiliency: https://docs.microsoft.com/en-us/ef/core/miscellaneous/connection-resiliency 

                                //abp MySqlRetryingExecutionStrategy' does not support user initiated transactions. 
                                //Use the execution strategy returned by 'DbContext.Database.CreateExecutionStrategy()
                            });
#endif
                       
                    case DbFlag.Postgre:
                        return options.UseNpgsql(sqlConnectionString,
                          npgsqlOptionsAction: sqlOptions =>
                          {
                              if (!string.IsNullOrEmpty(migrationsAssembly))
                              {
                                  sqlOptions.MigrationsAssembly(migrationsAssembly);
                              }
                              //Configuring Connection Resiliency: https://docs.microsoft.com/en-us/ef/core/miscellaneous/connection-resiliency 

                              //abp MySqlRetryingExecutionStrategy' does not support user initiated transactions. 
                              //Use the execution strategy returned by 'DbContext.Database.CreateExecutionStrategy()
                              if (!abp)
                              {
                                sqlOptions.EnableRetryOnFailure(maxRetryCount: 15, maxRetryDelay: TimeSpan.FromSeconds(30), errorCodesToAdd: null);
                              }
                          });
                    case DbFlag.Sqlite:
                        return options.UseSqlite(sqlConnectionString,
                          sqliteOptionsAction: sqlOptions =>
                          {
                              if (!string.IsNullOrEmpty(migrationsAssembly))
                              {
                                  sqlOptions.MigrationsAssembly(migrationsAssembly);
                              }
                              //Configuring Connection Resiliency: https://docs.microsoft.com/en-us/ef/core/miscellaneous/connection-resiliency 

                              //abp MySqlRetryingExecutionStrategy' does not support user initiated transactions. 
                              //Use the execution strategy returned by 'DbContext.Database.CreateExecutionStrategy()
                          });
                    default:
                        throw new NotSupportedException("not support database ");
                }

            };
            // Add framework services.
            //services.AddDbContextPool<Db>((serviceProvider, options) => {
            //    func(options);
            //    options.UseInternalServiceProvider(serviceProvider);
            //});
            //https://www.cnblogs.com/lyzi/p/11491216.html
            services.AddDbContext<Db>(options => func(options), ServiceLifetime.Transient);
        }
    }
}
#endif