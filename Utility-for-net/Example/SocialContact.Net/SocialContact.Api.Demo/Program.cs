using Autofac.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Serilog;
using Serilog.Events;
using Serilog.Sinks.SystemConsole.Themes;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace SocialContact.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Utility.DbConfig.Flag = Utility.DbFlag.Sqlite;
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args)
        {

            /*Unhandled exception. System.InvalidOperationException: Unable to resolve service
            for type 'Microsoft.Extensions.Logging.ILoggerFactory' while attempting to acti
            vate 'SocialContact.Api.Startup'.*/
            string text = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
            if (string.IsNullOrEmpty(text))
            {
                text = "Development";
            }
            Console.WriteLine(text);//Development
            IConfigurationBuilder configurationBuilder = new ConfigurationBuilder()
                .AddCommandLine(args)
                .SetBasePath(Environment.CurrentDirectory)
                .AddJsonFile("appsettings." + text + ".json", optional: false, reloadOnChange: true)
                .AddEnvironmentVariables();
            var configuration = configurationBuilder.Build();
            Log.Logger = new LoggerConfiguration().MinimumLevel.Debug()
                        .MinimumLevel.Override("Default", LogEventLevel.Information)
                        .MinimumLevel.Override("Microsoft", LogEventLevel.Warning)
                        .MinimumLevel.Override("System", LogEventLevel.Warning)
                        .MinimumLevel.Override("Microsoft.Hosting.Lifetime", LogEventLevel.Information)
                        .MinimumLevel.Override("Microsoft.AspNetCore.Authentication", LogEventLevel.Information)
                        .Enrich.FromLogContext()
                        //file
                        .WriteTo.File(Path.Combine("logs", "log.txt"), LogEventLevel.Verbose,
                        "{Timestamp:yyyy-MM-dd HH:mm:ss.fff zzz} [{Level:u3}] {Message:lj}{NewLine}{Exception}", null,
                        1073741824L, null, buffered: false, shared: false, null, RollingInterval.Hour,
                        rollOnFileSizeLimit: false, 31)
                        .WriteTo.Console(LogEventLevel.Verbose,
                        "[{Timestamp:HH:mm:ss} {Level}] {SourceContext}{NewLine}{Message:lj}{NewLine}{Exception}{NewLine}",
                        null, null, null, AnsiConsoleTheme.Literate)
                        .ReadFrom.Configuration(configuration)
                        .CreateLogger();
            var host = Host.CreateDefaultBuilder(args)
                 .ConfigureWebHostDefaults(webBuilder =>
                 {
                     webBuilder.UseStartup<Startup>();


                     webBuilder.CaptureStartupErrors(false)
                     .UseDefaultServiceProvider(options => { options.ValidateScopes = false; })
                     //.UseApplicationInsights()
                     .UseContentRoot(Directory.GetCurrentDirectory())
                      .UseIISIntegration()
                     .UseConfiguration(configuration)
                     .UseSerilog();
                 });
            host = host.UseServiceProviderFactory(new AutofacServiceProviderFactory());
            return host;
        }
    }
}
