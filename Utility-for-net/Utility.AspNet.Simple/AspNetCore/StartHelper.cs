#if NETCOREAPP2_0 || NETCOREAPP2_1 || NETCOREAPP2_2 || NETCOREAPP3_0 || NETCOREAPP3_1 || NET5_0 || NET6_0 || NETSTANDARD2_0 || NETSTANDARD2_1
using System;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Autofac.Extensions.DependencyInjection;
using System.Runtime.InteropServices;
using Serilog;

namespace Utility.AspNetCore
{
    /// <summary>
    /// aspnet core 启动帮助类
    /// </summary>
    public class StartHelper
    {
        public static string BucktName { get; private set; }
        /// <summary>
        /// $"{Environment.CurrentDirectory}/wwroot/shop/{objectName}"
        /// </summary>
        public static string BucktPath { get; private set; }
        public static AutofacServiceProviderFactory AutofacServiceProviderFactory { get; set; } = new AutofacServiceProviderFactory();
        /// <summary>
        /// 启动
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="title"></param>
        /// <param name="args"></param>
        /// <param name="abp"></param>
        public static void Start<T>(string title, string[] args,bool abp=false) where T : class
        {
            Console.Title = title;
            var config = LogSimpleHelper.Initial(args);
            ////https://autofaccn.readthedocs.io/en/latest/integration/aspnetcore.html#asp-net-core-3-0-and-generic-hosting
#if NETCOREAPP3_0 || NETCOREAPP3_1 || NET5_0 || NET6_0
            StartSimpleHelper.CreateHostBuilder<T>(config, args, host=> {
              
                //注意 必须放在后面 
                //InvalidCastException: Unable to cast object of type 'Microsoft.Extensions.DependencyInjection.ServiceCollection' to type 'Autofac.ContainerBuilder'.
                if (!abp)
                {
                    host = host.UseServiceProviderFactory(AutofacServiceProviderFactory);
                }
                BucktName = config["BucktName"]?.ToLower();//Bucket name cannot have upper case characters
                host.UseSerilog();
                return host;
            }).Run();
#else
            StartSimpleHelper.CreateWebHostBuilder<T>(config, args,host=>{
                if (StartSimpleHelper.IsWindowService&&RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
                {
                   // host = host.UseWindowsService();
                }
                BucktName = config["BucktName"]?.ToLower();//Bucket name cannot have upper case characters
                it.UseSerilog();
            }).Run();
#endif
        }

        /// <summary>
        /// 启动 lt asp.net core 3.0
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="title"></param>
        /// <param name="args"></param>
        public static void StartWebHost<T>(string title, string[] args) where T : class
        {
            Console.Title = title;
            var config = LogSimpleHelper.Initial(args);

            StartSimpleHelper.CreateWebHostBuilder<T>(config, args, host => {
                if (StartSimpleHelper.IsWindowService && RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
                {
                    //>= asp.net core3.0
                   // host = host.UseWindowsService();
                }
                BucktName = config["BucktName"]?.ToLower();//Bucket name cannot have upper case characters
                host.UseSerilog();
            }).Run();
        }

    }

}
#endif
