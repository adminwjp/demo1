using Autofac;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System.Reflection;
using Utility.Ef;
using Microsoft.AspNetCore.Routing;
using System;

namespace Company.Api
{
    public class CompanyStart
    {
        public CompanyStart(IConfiguration configuration)
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
            builder.RegisterModule(new Company.CompanyModule());
        }
        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public virtual void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILoggerFactory loggerFactory, Microsoft.Extensions.Hosting.IApplicationLifetime lifetime)
        {

        }
        public virtual void Configure(IEndpointRouteBuilder configure)
        {
            configure.MapAreaControllerRoute(
               name: "company",
               areaName: "company",
               pattern: "{company:exists}/{controller}/{action}/{id?}"
             );
        }
    }
}
