using System.IO;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Serilog;

namespace ApiGateways
{
    public class Program
    {
        public static void Main(string[] args)
        {
			System.Console.Title="ApiGateways";
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) 
            {
            //return Host.CreateDefaultBuilder(args)
            //    .ConfigureWebHostDefaults(webBuilder =>
            //    {
            //        webBuilder.UseStartup<Startup>();
            //    });
            IHostBuilder builder = Host.CreateDefaultBuilder(args);
            builder.ConfigureServices(s => s.AddSingleton(builder))
                       //.ConfigureAppConfiguration(ic => ic.AddJsonFile(Path.Combine("", "configuration.json")))
                       //.ConfigureAppConfiguration(ic => ic.AddJsonFile(Path.Combine("", "ocelot.json")))
                    .ConfigureAppConfiguration(ic => ic.AddJsonFile(Path.Combine("", "ocelot-eureka.json")))
                       .ConfigureWebHostDefaults(webBuilder =>
                       {
                           webBuilder.UseStartup<Startup>();
                       })
                .ConfigureLogging((hostingContext, loggingbuilder) =>
                {
                    loggingbuilder.AddConfiguration(hostingContext.Configuration.GetSection("Logging"));
                    loggingbuilder.AddConsole();
                    loggingbuilder.AddDebug();
                })
                .UseSerilog((builderContext, config) =>
                {
                    config
                        .MinimumLevel.Information()
                        .Enrich.FromLogContext()
                        .WriteTo.Console();
                });
            return builder;
        }
    }
}
