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
using Comment.Ef;
using Comment.Application;
using Akka.Configuration;
using Akka.Actor;
using System.IO;
using Akka.DependencyInjection;
using A = Akka.DependencyInjection;
using Akka.Routing;
using Utility;
using Microsoft.Extensions.Hosting;

namespace Comment
{
    public class CommentStart
    {
        public CommentStart(IConfiguration configuration)
        {
            this.Configuration = configuration;
        }
        public IConfiguration Configuration { get; }
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public virtual IServiceProvider ConfigureServices(IServiceCollection services)
        {
            //string sqlConnectionString = Configuration.GetConnectionString($"{ConfigHelper.DbFlag}ConnectionString");
            // sqlConnectionString = ConfigManager.GetByConsul($"ShopComment/{DbConfig.Flag}ConnectionString");

          //  services.UseEf<CommentDbContent>(CommentDbContent.ConnectionString); //abp 起冲突 多数据库 sqlite sqlserver mysql oracle postgre //直接 dbcontext 直接 声明数据库使用类型


            // set up a simple service we're going to hash
            services.AddScoped<ICommentService, CommentServiceImpl>();

            // creates instance of IPublicHashingService that can be accessed by ASP.NET
           services.AddSingleton<IPublicCommentService, PublicHashingServiceImpl>();

            //IHostedService 只能启动一个
            //多个演员 难道 定义多个 ?
            // starts the IHostedService, which creates the ActorSystem and actors
            //services.AddHostedService(sp => (IHostedService)sp.GetRequiredService<IPublicCommentService>());


            return null;
        }

        public virtual void ConfigureContainer(ContainerBuilder builder)
        {
            builder.Register(it => {
                DesignTimeDbContextFactory designTimeDbContextFactory = new DesignTimeDbContextFactory();
                var db = designTimeDbContextFactory.CreateDbContext(null);
                return db;
            }).As<CommentDbContent>().InstancePerDependency();
        }
        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public virtual void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILoggerFactory loggerFactory, Microsoft.Extensions.Hosting.IApplicationLifetime lifetime)
        {
            lifetime.ApplicationStarted.Register(() => {
                var hocon = ConfigurationFactory.ParseString(File.ReadAllText("app.conf"));
                var bootstrap = BootstrapSetup.Create().WithConfig(hocon);
                var di = ServiceProviderSetup.Create(app.ApplicationServices);
                var actorSystemSetup = bootstrap.And(di);
                CommentManager.ActorSystem = ActorSystem.Create("AspNetDemo", actorSystemSetup);
                // </AkkaServiceSetup>

                // <ServiceProviderFor>
                // props created via IServiceProvider dependency injection
                var hasherProps = A.ServiceProvider.For(CommentManager.ActorSystem).Props<CommentActor>();
                CommentManager.RouterActor = CommentManager.ActorSystem.ActorOf(hasherProps.WithRouter(FromConfig.Instance), "hasher");
                // </ServiceProviderFor>
            });

            lifetime.ApplicationStopped.Register(() => {

                // theoretically, shouldn't even need this - will be invoked automatically via CLR exit hook
                // but it's good practice to actually terminate IHostedServices when ASP.NET asks you to
                CoordinatedShutdown.Get(CommentManager.ActorSystem).Run(CoordinatedShutdown.ClrExitReason.Instance).Wait();
            });
        }
        public virtual void Configure(IEndpointRouteBuilder configure)
        {
        
        }
    }
}
