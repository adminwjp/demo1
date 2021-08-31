using Autofac;
using Autofac.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using SocialContact.Application.Infrastructure;
using SocialContact.Domain;
using SocialContact.Nhibernate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Utility.AspNetCore.Extensions;
using Utility.Extensions;
using Utility.Nhibernate.Infrastructure;
using Utility.ObjectMapping;

namespace SocialContact.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration):this(configuration,null)
        {
        }

        /*Unhandled exception. System.InvalidOperationException: Unable to resolve service
          for type 'Microsoft.Extensions.Logging.ILoggerFactory' while attempting to acti
          vate 'SocialContact.Api.Startup'.*/
        protected Startup(IConfiguration configuration, ILoggerFactory loggerFactory)
        {
            Configuration = configuration;
            this.LoggerFactory = loggerFactory;
        }
         public ILoggerFactory LoggerFactory { get; }
        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            AutoHelper.Load();
            //services.AddControllers().AddNewtonsoftJson();
            services.AddControllers().AddJson();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "SocialContact.Api.Demo", Version = "v1" });
            });
           // var builder = new ContainerBuilder();
            //builder.Populate(services);
            //return new AutofacServiceProvider(builder.Build());
        }


        // ConfigureContainer is where you can register things directly
        // with Autofac. This runs after ConfigureServices so the things
        // here will override registrations made in ConfigureServices.
        // Don't build the container; that gets done for you by the factory.
        public virtual void ConfigureContainer(ContainerBuilder builder)
        {
            // Register your own things directly with Autofac here. Don't
            // call builder.Populate(), that happens in AutofacServiceProviderFactory
            // for you.
            //builder.RegisterModule(new DomainManagerModule(LoggerFactory));
            builder.RegisterModule(new DomainManagerModule());
            builder.RegisterModule(new NhibernateManagerModule());
            builder.RegisterModule(new ApplicationModule());
            builder.Register(it=>AutoMapperObjectMapper.Empty).As<IObjectMapper>().SingleInstance();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "SocialContact.Api.Demo v1"));
            }
            var session= app.ApplicationServices.GetService<AppSessionFactory>();
            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
