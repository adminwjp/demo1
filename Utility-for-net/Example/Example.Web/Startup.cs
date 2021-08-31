using Autofac;
using Autofac.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using SocialContact;
using SocialContact.Filter;
using System;
using System.Linq;
using Utility.AspNetCore.Extensions;
using Utility.Consul;
using Utility.Extensions;
using Utility.AspNetCore.Filter;
using Utility.Cache;
using OA;
using Product.Api;
using Config.Api;
using Company.Api;
using Shop.Cap.Api;
using Comment;
using Tasks;
using TakeOutFoot.Api;
using Utility.Logs;
using Utility.Ioc;
using Utility.Mappers;
using IdentityServer;
using Utility.Demo;

namespace Example.Web
{
    public class Startup
    {
        SocialContactStart socialContactStart;
        OAStart oAStart;
        ProductStart productStart;
        ConfigStart configStart;
        CompanyStart companyStart;
        CapStart capStart;
        CommentStart commentStart;
        TaskStart taskStart;
        TakeOutFootStartup takeOutFootStartup;
        IdentitySever4Startup identitySever4Startup;
        public Startup(IConfiguration configuration)
        {
            //tran tx
            ValidateActionParamFilter.actionNames.Add("upload");
            this.Configuration = configuration;
            socialContactStart = new SocialContactStart(Configuration);
            oAStart = new OAStart(Configuration);
            productStart = new ProductStart(Configuration);
            configStart = new ConfigStart(Configuration);
            companyStart = new CompanyStart(Configuration);
            capStart = new CapStart(Configuration);
            commentStart = new CommentStart(Configuration);
            taskStart = new TaskStart(Configuration);
            takeOutFootStartup = new TakeOutFootStartup(Configuration);

            identitySever4Startup = new IdentitySever4Startup(Configuration);


        }
        public IConfiguration Configuration { get; }
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public virtual IServiceProvider ConfigureServices(IServiceCollection services)
        {
            //注册微服务 
            //services.AddRegisterService(Configuration, ConfigHelper.ServiceFlag);

            productStart.ConfigureServices(services);
            oAStart.ConfigureServices(services);
            socialContactStart.ConfigureServices(services);
            configStart.ConfigureServices(services);
            companyStart.ConfigureServices(services);
            capStart.ConfigureServices(services);
            commentStart.ConfigureServices(services);
            taskStart.ConfigureServices(services);
            takeOutFootStartup.ConfigureServices(services);
            identitySever4Startup.ConfigureServices(services);
            
            services.AddSingleton(typeof(ILog<>),typeof(SerilogLog<>));
            services.AddSingleton<ICacheContent, NetCoreCache>();
            services.AddSingleton<IIocManager>(it=>AutofacIocManager.Instance);


            services.AddSingleton<IMapper>(it => AutoMapperMapper.Empty);
            AutoMapperMapper.Empty.Init(it=>{
                Product.Application.Services.Infrastructure.
                    AutoMapperHelper.Load(it);
            });

         //services.AddTransient<IStartupFilter,RequestSetOptionsStartupFilter>();
            //看不懂验证 各种绑定 绑定成功了 怎么验证 还是 没绑定的 关联的引用不需要验证 自动验证了
            //formbody 模型验证 不支持 坑 要看源码 我怎么模型验证永远失败

            //https://docs.microsoft.com/zh-cn/aspnet/core/mvc/models/validation?view=aspnetcore-3.1
            //services.AddControllers(options => options.SuppressImplicitRequiredAttributeForNonNullableReferenceTypes = true);
            services.Configure<ConsulEntity>(Configuration.GetSection("Service"));

            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddControllers().AddControllersAsServices();

            //services.AddHttpClient<AdminService>(config =>
            //{
            //    config.BaseAddress = new Uri(Configuration["AdminService:BaseAddress"]);
            //    config.DefaultRequestHeaders.Add("Accept", "application/json");
            //});

            Utility.AspNetCore.Extensions.ServiceCollectionExtensions.AddHttpClient(services);
            //https://www.cnblogs.com/cmt/p/11347507.html
            Utility.AspNetCore.Extensions.ServiceCollectionExtensions.AddIISServerOptions(services);
            services.AddMemoryCache(options => {
                options.ExpirationScanFrequency = TimeSpan.FromHours(2);
            });

            services.AddTransient<IStartupFilter,
               RequestSetOptionsStartupFilter>();


            //services.AddCorsV1(); 

            // services.AddSingleton<Data.EsService>();
            var redisHost = Configuration["redis:connectionString"];//Configuration["RedisConnectionString"];
            var mongoHost = Configuration["mongo:connectionString"];
            var elasticsearchHost = Configuration["elasticsearch:connectionString"];
            //services.AddDistributedRedisCache(options =>
            //{
            //    options.Configuration = redisHost;
            //    options.InstanceName = "Demo";
            //});
            //string[] elasticsearchHosts = JsonHelper.ToObject<string[]>(elasticsearchHost);
            //services.AddSingleton<IRedisCache>(it => new StackExchangeCache(redisHost));
            //services.AddSingleton<ElasticsearchHelper>(it => new ElasticsearchHelper(elasticsearchHosts));

            //services.AddControllers();
            Utility.AspNetCore.Extensions.ServiceCollectionExtensions.AddApiVersioning(services);
            //services.AddSingleton(new System.Text.Json.JsonSerializerOptions() { MaxDepth=0, PropertyNamingPolicy=JsonPropertyNamingPolicy.ObjectResolverJson });
            Utility.AspNetCore.Extensions.ServiceCollectionExtensions.AddFilter(services, new Type[] { 
                typeof(ActionAuthFilter),
                typeof(ValidateActionParamFilter),
                typeof(ResultFilter) }).AddJson()
                .AddControllersAsServices()
                .SetCompatibilityVersion(CompatibilityVersion.Latest);
            Utility.AspNetCore.Extensions.ServiceCollectionExtensions.AddSwaggerV1<Utility.AspNetCore.Filter.EmptySwaggerOperationFilter>(services, "v1", "Example.Web");
            services.AddApiModelValidate();
             socialContactStart.ConfigureServices(services); 
            var builder = GetContainerBuilder();
             builder.Populate(services);
            ConfigureContainer(Utility.Ioc.AutofacIocManager.Instance.Builder);
            return new AutofacServiceProvider(Utility.Ioc.AutofacIocManager.Instance.Container);
        }
        protected virtual ContainerBuilder GetContainerBuilder()
        {
            //ContainerBuilder builder = new ContainerBuilder();
            //ConfigureContainer(builder);
            //return builder;//base.GetContainerBuilder();
            return Utility.Ioc.AutofacIocManager.Instance.Builder;
        }
        public void ConfigureContainer(ContainerBuilder builder)
        {


            oAStart.ConfigureContainer(builder);
            productStart.ConfigureContainer(builder);
            configStart.ConfigureContainer(builder);
            socialContactStart.ConfigureContainer(builder);
            companyStart.ConfigureContainer(builder);
            commentStart.ConfigureContainer(builder);
            capStart.ConfigureContainer(builder);
            taskStart.ConfigureContainer(builder);
            builder.RegisterModule(new DemoModule());
        }
        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILoggerFactory loggerFactory, Microsoft.Extensions.Hosting.IApplicationLifetime lifetime)
        {
          
            var logger = loggerFactory.CreateLogger<Startup>();
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                logger.LogInformation("Development environment");
            }
            else
            {
                // Non-development service configuration
                logger.LogInformation("Environment: {EnvironmentName}", env.EnvironmentName);
            }  // Make work identity server redirections in Edge and lastest versions of browers. WARN: Not valid in a production environment.
               //这一步 不能 要 否则 swagger 有问题 页面显示不出来
            /*     app.Use(async (context, next) =>
                 {
                     context.Response.Headers.Add("Content-Security-Policy", "script-src 'unsafe-inline'");
                     await next();
                 });*/

            app.UseForwardedHeaders();
            Utility.AspNetCore.StartSimpleHelper.ApplicationStarted = lifetime.ApplicationStarted;
            Utility.AspNetCore.StartSimpleHelper.ApplicationStopped = lifetime.ApplicationStopped;
            Utility.AspNetCore.StartSimpleHelper.ApplicationStopping = lifetime.ApplicationStopping;

            Utility.AspNetCore.Extensions.RegisterServiceExtensions.UseService(app, Configuration,Utility.ConfigHelper.ServiceFlag);
            Utility.AspNetCore.Extensions.ZipkinExtensions.UseZipkin(app, loggerFactory, Configuration);

            configStart.Configure(app, env, loggerFactory, lifetime);
            oAStart.Configure(app, env, loggerFactory, lifetime);
            socialContactStart.Configure(app, env);
            productStart.Configure(app, env, loggerFactory, lifetime);
            companyStart.Configure(app, env, loggerFactory, lifetime);
            commentStart.Configure(app, env, loggerFactory, lifetime);
            capStart.Configure(app, env, loggerFactory, lifetime);
            takeOutFootStartup.Configure(app, env);

            identitySever4Startup.Configure(app);
            if (Program.ServerStart)
            {
                taskStart.Configure(app, env, loggerFactory, lifetime);
            }
            // app.UseCors("AllowAllOrigins");

            // app.UseAuthorization();
            //方案1  IServiceCollection 不需要配置
            app.UseCors(options =>
            {
                options.AllowAnyHeader();
                options.AllowAnyMethod();
                options.AllowAnyOrigin();
                //options.AllowCredentials();
            });
            //方案2 IServiceCollection 需要配置
            //app.UseCors("AllowAllOrigins");

            // app.UseAuthorization();

            // Enable middleware to serve generated Swagger as a JSON endpoint.
            app.UseSwagger();
            // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.),
            // specifying the Swagger JSON endpoint.
            //要在应用的根 (http://localhost:<port>/) 处提供 Swagger UI，请将 RoutePrefix 属性设置为空字符串
            app.UseSwaggerUI(c =>
            {
                ////要统一 SwaggerVersion
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Example.Web");
                c.RoutePrefix = string.Empty;
            });
            app.UseSession();
            app.UseApiVersioning();
            app.UseHttpsRedirection();
            var cachePeriod = env.IsDevelopment() ? "600" : "604800";
            app.UseStaticFiles(new StaticFileOptions
            {
                //FileProvider = new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), "MyStaticFiles")),
                //RequestPath = "/StaticFiles",
                OnPrepareResponse = ctx =>
                {
                    // Requires the following import:
                    // using Microsoft.AspNetCore.Http;
                    ctx.Context.Response.Headers.Append("Cache-Control", $"public, max-age={cachePeriod}");
                }
            });
            //app.UseCookiePolicy();
            // app.UseAuthentication();
            // app.UseAuthorization();   
            app.UseRouting();
            // uncomment, if you want to add MVC-based
            app.UseAuthorization();
            //aspnet core > = 3.0
            app.UseEndpoints(options =>
            {
                //iis 不支持 
                options.MapAreaControllerRoute(
                  name: "area",
                  areaName: "areas",
                  pattern: "{area:exists}/{controller}/{action}/{id?}"
                );

                socialContactStart.Configure(options);
                oAStart.Configure(options);
                productStart.Configure(options);
                configStart.Configure(options);
                companyStart.Configure(options);
                capStart.Configure(options);
                commentStart.Configure(options);
                taskStart.Configure(options);


                options.MapControllerRoute(
                name: "mvc",
                pattern: "{controller=Home}/{action=Index}/{id?}"
              );
                options.MapAreaControllerRoute(
                    name: "areas",
                    areaName: "admin",
                    pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}"
                  );

                //options.MapControllerRoute(
                //  name: "default",
                //  pattern: "{controller}/{action}/{id?}"
                //);
                options.MapControllers();
                options.MapGet("/", async context =>
                {
                    await context.Response.WriteAsync("Hello World!");
                });
            });

        }
    }
}
