#if NETCOREAPP2_0 || NETCOREAPP2_1 || NETCOREAPP2_2 || NETCOREAPP3_0 || NETCOREAPP3_1 || NET5_0 || NET6_0 || NETSTANDARD2_0 || NETSTANDARD2_1
using Microsoft.Extensions.Configuration;
using Serilog;
using Serilog.Events;
using Serilog.Formatting.Json;
using Serilog.Sinks.Elasticsearch;
using Serilog.Sinks.File;
using Serilog.Sinks.SystemConsole.Themes;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Winton.Extensions.Configuration.Consul;

namespace Utility.AspNetCore
{
    /// <summary>
    /// asp.net core 日志 帮助类
    /// </summary>
    public class LogHelper
    {
        /// <summary>
        ///初始化 日志 配置
        /// </summary>
        public static void InitLog()
        {
            LogSimpleHelper.LogConfig = LogConfig;
        }
        /// <summary>
        /// 日志 配置
        /// </summary>
        /// <param name="configuration"></param>
        /// <returns></returns>
        public static IConfiguration LogConfig(IConfiguration configuration)
        {

            if (LogSimpleHelper.Flag == LogFlag.File)
            {
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
                             null, null, null, AnsiConsoleTheme.Literate).ReadFrom.Configuration(configuration)

                             .CreateLogger();
            }
            else
            {
                Log.Logger = new LoggerConfiguration().MinimumLevel.Debug()
                            .MinimumLevel.Override("Default", LogEventLevel.Information)
                            .MinimumLevel.Override("Microsoft", LogEventLevel.Warning)
                            .MinimumLevel.Override("System", LogEventLevel.Warning)
                            .MinimumLevel.Override("Microsoft.Hosting.Lifetime", LogEventLevel.Information)
                            .MinimumLevel.Override("Microsoft.AspNetCore.Authentication", LogEventLevel.Information)
                            .Enrich.FromLogContext()
                            // es
                            .WriteTo.Elasticsearch(new ElasticsearchSinkOptions(new Uri(configuration.GetConnectionString("ElasticsearchConnectionString"))) // for the docker-compose implementation
                            {
                                AutoRegisterTemplate = true,
                                OverwriteTemplate = true,
                                DetectElasticsearchVersion = true,
                                AutoRegisterTemplateVersion = AutoRegisterTemplateVersion.ESv7,
                                NumberOfReplicas = 1,
                                NumberOfShards = 2,
                                //BufferBaseFilename = "./buffer",
                                RegisterTemplateFailure = RegisterTemplateRecovery.FailSink,
                                FailureCallback = e => Console.WriteLine("Unable to submit event " + e.MessageTemplate),
                                EmitEventFailure = EmitEventFailureHandling.WriteToSelfLog |
                            EmitEventFailureHandling.WriteToFailureSink |
                            EmitEventFailureHandling.RaiseCallback,
#pragma warning disable CS0618 // 类型或成员已过时
                                FailureSink = new FileSink("./fail-{Date}.txt", new JsonFormatter(), null, null)
#pragma warning restore CS0618 // 类型或成员已过时
                            })
                            .WriteTo.Console(LogEventLevel.Verbose,
                            "[{Timestamp:HH:mm:ss} {Level}] {SourceContext}{NewLine}{Message:lj}{NewLine}{Exception}{NewLine}",
                            null, null, null, AnsiConsoleTheme.Literate).ReadFrom.Configuration(configuration)
                            .CreateLogger();
            }
            return configuration;
        }


    }
}
#endif