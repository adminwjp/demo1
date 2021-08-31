using Autofac;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System.Reflection;
using Utility.Ef;
using Product.Infrastructure;
using Microsoft.AspNetCore.Routing;
using System;
using MediatR;
using Utility.Infrastructure;

namespace Product.Api
{
    public class ProductStart
    {
        public ProductStart(IConfiguration configuration)
        {
            this.Configuration = configuration;
        }
        public IConfiguration Configuration { get; }
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public virtual IServiceProvider ConfigureServices(IServiceCollection services)
        {
            //迁移 没用  只能 在 上下文 该 类库 下 迁移 不然找不到
            var migrationsAssembly = typeof(ProductDbContext).GetTypeInfo().Assembly.GetName().Name;
            //Pooling=true;FailIfMissing=false;
            string product = "Data Source=E:/work/utility/Utility-for-net/Example/Example.Web/demo.db;";
            //services.AddSingleton<IMediator, NoMediator>();
            services.UseEf<ProductDbContext>(product, migrationsAssembly, Utility.ConfigHelper.DbFlag, false, "");
            services.AddTransient(typeof(DbContextProvider<ProductDbContext>));
            return null;
        }
      
        public virtual void ConfigureContainer(ContainerBuilder builder)
        {
            builder.RegisterModule(new Product.Application.Services.AutofacModules.MediatorModule());
            builder.RegisterModule(new Product.Application.Services.AutofacModules.ApplicationModule(""));
            if (!Utility.ConfigHelper.AnnationIoc)
            {
                builder.RegisterModule(new Product.Infrastructure.AutofacModules.InfrastructureModule());
            }
        }
        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public virtual void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILoggerFactory loggerFactory, Microsoft.Extensions.Hosting.IApplicationLifetime lifetime)
        {

        }
        public virtual void Configure(IEndpointRouteBuilder configure)
        {
            //configure.MapAreaControllerRoute(
            //       name: "product",
            //       areaName: "product",
            //       pattern: "{product:exists}/{controller}/{action}/{id?}"
            //     );
        }
    }
}
