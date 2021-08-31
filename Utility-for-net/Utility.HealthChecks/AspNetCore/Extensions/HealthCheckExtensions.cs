#if NETCOREAPP2_0 || NETCOREAPP2_1 || NETCOREAPP2_2 || NETCOREAPP3_0 || NETCOREAPP3_1 || NET5_0 || NET6_0 || NETSTANDARD2_0 || NETSTANDARD2_1
using Microsoft.Extensions.DependencyInjection;
#if NETCOREAPP3_0 || NETCOREAPP3_1 || NET5_0 || NET6_0 || NETSTANDARD2_0 || NETSTANDARD2_1
using Microsoft.Extensions.Diagnostics.HealthChecks;
#endif

namespace Utility.AspNetCore.Extensions
{
    /// <summary>
    /// ServiceCollection 扩展类
    /// </summary>
    public static class HealthCheckExtensions
    {

        /// <summary>
        /// 数据库健康检测
        /// </summary>
        /// <param name="services"></param>
        /// <param name="connectionString"></param>
        /// <param name="name"></param>
        /// <param name="flag"></param>
        /// <returns></returns>
#if NETCOREAPP3_0 || NETCOREAPP3_1 || NET5_0 || NET6_0
        public static IHealthChecksBuilder UseHealthCheck(this IServiceCollection services, string  connectionString,string name,DbFlag flag) 
        {
            var hcBuilder = services.AddHealthChecks();
            //健康检查
            hcBuilder.AddCheck("self", () => HealthCheckResult.Healthy());
            switch (flag)
            {
                case DbFlag.MySql:
                    hcBuilder
                   .AddMySql(
                       connectionString,
                       name: $"{name}-mysql-check",
                       tags: new string[] { $"{name} mysql" });
                    break;
                case DbFlag.SqlServer:
                    hcBuilder
                   .AddSqlServer(
                       connectionString,
                       name: $"{name}-sqlserver-check",
                       tags: new string[] { $"{name} sqlserver" });
                    break;
                case DbFlag.Oracle:
                    hcBuilder
                      .AddOracle(
                          connectionString,
                       name: $"{name}-oracle-check",
                       tags: new string[] { $"{name} oracle" });
                    break;
                case DbFlag.Postgre:
                    hcBuilder
                    .AddNpgSql(
                        connectionString,
                       name: $"{name}-postgre-check",
                       tags: new string[] { $"{name} postgre" });
                    break;
                case DbFlag.Sqlite:
                    hcBuilder
                    .AddSqlite(
                         connectionString,
                       name: $"{name}-sqlite-check",
                       tags: new string[] { $"{name} sqlite" });
                    break;
                default:
                    break;
            }
            //hcBuilder
            //       .AddRedis(
            //           Configuration.GetConnectionString("RedisConnectionString"),
            //           name: $"{name}-redis-check",
            //           tags: new string[] { $"{name}-redis" });
            //hcBuilder
            //      .AddElasticsearch(
            //           Configuration.GetConnectionString("ElasticsearchConnectionString"),
            //          name: $"{name}-es-check",
            //          tags: new string[] { $"{name}-es" });

            // prevent from mapping "sub" claim to nameidentifier.
            //JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Remove("sub");

            //var identityUrl = Configuration.GetValue<string>("IdentityUrl");

            //services.AddAuthentication(options =>
            //{
            //    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            //    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;

            //}).AddJwtBearer(options =>
            //{
            //    options.Authority = identityUrl;
            //    options.RequireHttpsMetadata = false;
            //    options.Audience = "email";
            //});
			return hcBuilder;
        }
#endif
       
    
    }
}
#endif