using Autofac;
using Config.Application;
using Config.Ef;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Reflection;
using Utility.Ef;

namespace Config.Api
{
    public class ConfigStart
    {
        public ConfigStart(IConfiguration configuration)
        {
            this.Configuration = configuration;
        }
        public IConfiguration Configuration { get; }
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public virtual IServiceProvider ConfigureServices(IServiceCollection services)
        {
            //迁移 没用  只能 在 上下文 该 类库 下 迁移 不然找不到
            var migrationsAssembly = typeof(ConfigDbContext).GetTypeInfo().Assembly.GetName().Name;
            string config = "Data Source=E:/work/utility/Utility-for-net/Example/Example.Web/config.db;Pooling=true;FailIfMissing=false;";
            services.UseEf<ConfigDbContext>(config, migrationsAssembly, Utility.ConfigHelper.DbFlag, false, "");
            return null;
        }

        public virtual void ConfigureContainer(ContainerBuilder builder)
        {
            builder.RegisterModule(new ConfigEfModule());
            builder.RegisterModule(new ConfigServiceModule());
        }
        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public virtual void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILoggerFactory loggerFactory, Microsoft.Extensions.Hosting.IApplicationLifetime lifetime)
        {

        }
        public virtual void Configure(IEndpointRouteBuilder configure)
        {
       
        }
    }
}
