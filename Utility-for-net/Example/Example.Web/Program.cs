using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Utility;
using Utility.AspNetCore;
using Utility.Ioc;

namespace Example.Web
{
    //dotnet run --urls="http://*:6002"
    //SQLiteException: database is locked demo.db begin tran
    public class Program
    {
        public static bool ServerStart = false;
        public static IIocManager IocManager { get; set; } = AutofacIocManager.Instance;
        public static void Main(string[] args)
        {
            ConfigHelper.OrmFlag = OrmFlag.Xml;
            //ConfigHelper.IsAbpEf = true;
            ConfigHelper.IsAbpEf = false;
            ConfigHelper.DbFlag = DbFlag.Sqlite;
            ConfigHelper.ServiceFlag = ServiceFlag.Consul;
            //ServiceConfig.Flag = ServiceFlag.None;
            LogHelper.InitLog();
           // StartHelper.AutofacServiceProviderFactory = new Autofac.Extensions.DependencyInjection.AutofacServiceProviderFactory(it => { Utility.Ioc.AutofacIocManager.Instance.Builder = it; });
            //StartHelper.Start<Startup>("Example.Web", args);
            StartHelper.StartWebHost<Startup>("Example.Web", args);
            // CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
