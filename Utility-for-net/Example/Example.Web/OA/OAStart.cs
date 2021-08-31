using Autofac;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;

namespace OA
{
    public class OAStart
    {
        public OAStart(IConfiguration configuration)
        {
            this.Configuration = configuration;
        }
        public IConfiguration Configuration { get; }
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public virtual IServiceProvider ConfigureServices(IServiceCollection services)
        {
            return null;
        }

        public virtual void ConfigureContainer(ContainerBuilder builder)
        {
            OA.Domain.CoreManager.RegisterAutofac(builder, true);
        }
        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public virtual void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILoggerFactory loggerFactory, Microsoft.Extensions.Hosting.IApplicationLifetime lifetime)
        {

        }
        public virtual void Configure(IEndpointRouteBuilder configure)
        {
            configure.MapAreaControllerRoute(
                 name: "oa",
                 areaName: "oa",
                 pattern: "{oa:exists}/{controller}/{action}/{id?}"
             );
            configure.MapAreaControllerRoute(
                  name: "oa_abp",
                  areaName: "oa_abp",
                  pattern: "{oa_abp:exists}/{controller}/{action}/{id?}"
                );
        }
    }
}
