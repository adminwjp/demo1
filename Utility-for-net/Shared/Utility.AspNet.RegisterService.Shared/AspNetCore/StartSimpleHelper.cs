#if NETCOREAPP2_0 || NETCOREAPP2_1 || NETCOREAPP2_2 || NETCOREAPP3_0 || NETCOREAPP3_1 || NET5_0 || NET6_0 || NETSTANDARD2_0 || NETSTANDARD2_1
using System;
using System.IO;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using System.Net;
//using System.Runtime.InteropServices;
using System.Threading;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.DependencyInjection;
namespace Utility.AspNetCore
{
    /// <summary>
    /// aspnet core 启动帮助类
    /// </summary>
    public class StartSimpleHelper
    {

       /// <summary>
        /// Triggered when the application host has fully started and is about to wait for
        /// a graceful shutdown.
        /// </summary>
        public static  CancellationToken ApplicationStarted
        {
            get; set;
        }

         /// <summary>
        ///  Triggered when the application host is performing a graceful shutdown. All requests
        /// should be complete at this point. Shutdown will block until this event completes.
        /// </summary>
        public static CancellationToken ApplicationStopped
        {
            get; set;
        }
 
        /// <summary>
        ///   Triggered when the application host is performing a graceful shutdown.Requests
        ///  may still be in flight. Shutdown will block until this event completes.  
        /// </summary>
        public static CancellationToken ApplicationStopping
        {
            get;set;
        }

        /// <summary>
        /// 设置服务端口
        /// </summary>
        public static int WindowServicePort { get; set; }
        /// <summary>
        /// win service 启动
        /// </summary>
        public static bool IsWindowService { get; set; }

        /// <summary>
        /// 启动
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="title"></param>
        /// <param name="args"></param>
        public static void Start<T>(string title, string[] args) where T : class
        {
            Console.Title = title;
            var config = LogSimpleHelper.Initial(args);
            ////https://autofaccn.readthedocs.io/en/latest/integration/aspnetcore.html#asp-net-core-3-0-and-generic-hosting
#if NETCOREAPP3_0 || NETCOREAPP3_1 || NET5_0 || NET6_0
            CreateHostBuilder<T>(config, args).Run();
#else
            CreateWebHostBuilder<T>(config, args).Run();
#endif
        }


        /// <summary>
        /// host 启动
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="configuration"></param>
        /// <param name="args"></param>
        /// <param name="config"></param>
        /// <returns></returns>

#if NETCOREAPP3_0 || NETCOREAPP3_1 || NET5_0 || NET6_0
        public static IHost CreateHostBuilder<T>(IConfiguration configuration, string[] args,Func<IHostBuilder, IHostBuilder> config=null) where T : class
        {
          
            //WebHost 1.1-2.0+
            //Host3.0+
            // ASP.NET Core 3.0+:
            // The UseServiceProviderFactory call attaches the
            // Autofac provider to the generic hosting mechanism.
			//设置 地址 无效 dotnet *.dll  --urls="http://*:6001;"
            var host = Host.CreateDefaultBuilder(args)
                // .ConfigureServices(it=>it.AddAutofac())
                ;
            //if (IsWindowService&&RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            //{
            //    host = host.UseWindowsService();
            //}
            host= host
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    Use<T>(configuration, webBuilder);
                }); 
            host.ConfigureLogging(it => {
                it.Services.AddSingleton<ILoggerFactory, LoggerFactory>();

            });
            if (config != null)
            {
                host = config(host);
            }
            return host.Build();
          }
#endif

        /// <summary>
        /// 启动 配置
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="configuration"></param>
        /// <param name="webBuilder"></param>
        /// <param name="config"></param>
        public static IWebHostBuilder Use<T>(IConfiguration configuration, IWebHostBuilder webBuilder,Action<IWebHostBuilder> config=null) where T : class
        {

            if (IsWindowService)
            {
                webBuilder.ConfigureKestrel(serverOptions =>
                {
                    serverOptions.Listen(IPAddress.Any, WindowServicePort);
                    serverOptions.Limits.MaxRequestBodySize = null;
                });
            }
          
            webBuilder.ConfigureAppConfiguration((hostingContext, config) =>

            {
                //asp.net core 5.0 怎么进不来 弄错了
                //https://www.cnblogs.com/Vincent-yuan/p/11186196.html 优先级 太慢 启动时需要
                //Files(appsettings.json, appsettings.{ Environment}.json, { Environment}
                //是应用当前的运行环境)
                //Azure Key Vault
                //User secrets(Secret Manager)(仅用在开发环境)
                //Environment variables
                //Command - line arguments
                //config.AddEFConfiguration( options => options.UseInMemoryDatabase("InMemoryDb"));
                if (ConfigHelper.ServiceFlag!= ServiceFlag.None)
                {
                    var env = hostingContext.HostingEnvironment;
                    //hostingContext.Configuration = config.Build();
                    // string consul_url = hostingContext.Configuration["Consul_Url"];
                    string consul_url = configuration["ConsulUrl"];
                    string key = configuration["ConsulKey"];
                    Console.WriteLine(consul_url);
                    Console.WriteLine(key);
                    Console.WriteLine(env.ApplicationName);
                    Console.WriteLine(env.EnvironmentName);
                    if(!string.IsNullOrEmpty(key))
                    {
                        config = LogSimpleHelper.DynamicConfig(config, ConfigHelper.ServiceFlag, key, consul_url);
                        hostingContext.Configuration = config.Build();
                        configuration = hostingContext.Configuration;//更新 否则 consul 失效
                    }
                }
         
            });
            webBuilder.CaptureStartupErrors(false)
            .UseDefaultServiceProvider(options => { options.ValidateScopes = false; })
            //.UseApplicationInsights()
            .UseContentRoot(Directory.GetCurrentDirectory())
             .UseIISIntegration()
            .UseConfiguration(configuration);//地址 不生效 akka.net 
            //.UseUrls(configuration["urls"]?.Split(new char[] { ';' }))
            if (config != null)
            {
                config(webBuilder);
            }
            webBuilder.ConfigureLogging(it => {
                it.Services.AddSingleton<ILoggerFactory, LoggerFactory>();

            });
            webBuilder.UseStartup<T>();
            return webBuilder;
        }

        /// <summary>
        /// web  host 启动
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="configuration"></param>
        /// <param name="args"></param>
        /// <param name="config"></param>
        /// <returns></returns>
        public static IWebHost CreateWebHostBuilder<T>(IConfiguration configuration, string[] args, Action<IWebHostBuilder> config = null) where T : class =>
                
                Use<T>(configuration, WebHost.CreateDefaultBuilder(args), config)
                .Build();
    }

}
#endif
