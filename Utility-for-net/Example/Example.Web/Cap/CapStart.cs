using Autofac;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Utility.Ef;
using Microsoft.AspNetCore.Routing;
using System;
using Shop.Cap.Api.Infrastracture;

namespace Shop.Cap.Api
{
    public class CapStart
    {
        public CapStart(IConfiguration configuration)
        {
            this.Configuration = configuration;
        }
        public IConfiguration Configuration { get; }
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public virtual IServiceProvider ConfigureServices(IServiceCollection services)
        {
            //版本冲突 nuget 源 地址 不匹配  降低版本
            services.UseEf<CarouselDbContext>(CarouselDbContext.ConnectionString, "");

            //特定场景使用
            //You must be config used message queue
            //services.AddCap(x =>
            //{
            //    x.UseEntityFramework<ShopDbContext>();
            //    // x.UseRabbitMQ("192.168.1.3");
            //    x.UseDashboard();
            //    x.FailedRetryCount = 5;
            //    x.FailedThresholdCallback = failed =>
            //    {
            //        var logger = failed.ServiceProvider.GetService<ILogger<Startup>>();
            //        logger.LogError($@"A message of type {failed.MessageType} failed after executing {x.FailedRetryCount} several times, 
            //            requiring manual troubleshooting. Message name: {failed.Message.GetName()}");
            //    };
            //});
            return null;
        }

        public virtual void ConfigureContainer(ContainerBuilder builder)
        {
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
