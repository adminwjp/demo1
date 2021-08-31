using Autofac;
using NHibernate;
using OA.Domain;
using Serilog;
using Serilog.Events;
//using Serilog.Sinks.SystemConsole.Themes;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Utility;
using Utility.Domain.Uow;
using Utility.Ioc;
using Utility.Nhibernate;
using Utility.Nhibernate.Infrastructure;
using Utility.Nhibernate.Uow;
using Wpf.OA;

namespace Example.WpfApp.Common
{
    public class StartManager
    {
        public static void Start()
        {
            ConfigManager.Load();
            RegisterAutofac();
            OAService.RegisterDataSource();
            OANHibernateManager.Initial();
        }
        private static void RegisterAutofac()
        {
            Log.Logger = new LoggerConfiguration()
              .MinimumLevel.Debug()
              .MinimumLevel.Override("Microsoft", LogEventLevel.Warning)
              .MinimumLevel.Override("System", LogEventLevel.Warning)
              .MinimumLevel.Override("Microsoft.AspNetCore.Authentication", LogEventLevel.Information)
              .Enrich.FromLogContext()
              .WriteTo.File(Path.Combine("logs", @"log.txt"), rollingInterval: RollingInterval.Hour)
              // .WriteTo.Console(outputTemplate: "[{Timestamp:HH:mm:ss} {Level}] {SourceContext}{NewLine}{Message:lj}{NewLine}{Exception}{NewLine}", theme: AnsiConsoleTheme.Literate)
              .CreateLogger();
            AutofacIocManager.Instance.Register(it => {
                it.RegisterType<Microsoft.Extensions.Logging.LoggerFactory>().As<Microsoft.Extensions.Logging.ILoggerFactory>().SingleInstance();
                it.Register<AppSessionFactory>(it=>new AppSessionFactory(config=> {
                    NhibernateManger.Initial(config, AutofacIocManager.Instance.Resolver<Microsoft.Extensions.Logging.ILoggerFactory>());
                },DbFlag.Sqlite)).As<AppSessionFactory>().SingleInstance();
                it.Register(it => { return new SessionProvider(it.Resolve<AppSessionFactory>().OpenSession()); }).OwnedByLifetimeScope();
                it.RegisterType<NhibernateUnitWork>().As<IUnitWork>().OwnedByLifetimeScope();
            });
        }
    }
}
