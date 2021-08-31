using Autofac;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Orleans;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Orleans.Runtime;

namespace Tasks
{
    public class TaskStart
    {
        public static IClusterClient Client { get; private set; }
        public TaskStart(IConfiguration configuration)
        {
            Configuration = configuration;

        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public virtual IServiceProvider ConfigureServices(IServiceCollection services)
        {
           
            return null;
        }

        public virtual void ConfigureContainer(ContainerBuilder builder)
        {
        }
        public virtual void Configure(IEndpointRouteBuilder configure)
        {
          
        }
        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public virtual void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILoggerFactory loggerFactory, Microsoft.Extensions.Hosting.IApplicationLifetime lifetime)
        {
            var logger = loggerFactory.CreateLogger<TaskStart>();
            var loggerProvider = app.ApplicationServices.GetRequiredService<ILoggerProvider>();
            Client = new ClientBuilder()
                  .UseLocalhostClustering()
                  .ConfigureLogging(builder => builder.AddProvider(loggerProvider))
                  .Build();
          lifetime.ApplicationStarted.Register(async () => {
                var attempt = 0;
                var maxAttempts = 100;
                var delay = TimeSpan.FromSeconds(1);
                await Client.Connect(async error =>
                {
                    if (lifetime.ApplicationStarted.IsCancellationRequested)
                    {
                        return false;
                    }

                    if (++attempt < maxAttempts)
                    {
                        logger.LogWarning(error,
                            "Failed to connect to Orleans cluster on attempt {@Attempt} of {@MaxAttempts}.",
                            attempt, maxAttempts);

                        try
                        {
                            Task.Delay(delay, lifetime.ApplicationStarted).Wait();
                        }
                        catch (OperationCanceledException)
                        {
                            return false;
                        }

                        return true;
                    }
                    else
                    {
                        logger.LogError(error,
                            "Failed to connect to Orleans cluster on attempt {@Attempt} of {@MaxAttempts}.",
                            attempt, maxAttempts);

                        return false;
                    }
                });
            });

            lifetime.ApplicationStopped.Register(() => {
                try
                {
                    Client.Close().Wait();
                }
                catch (OrleansException error)
                {
                    logger.LogWarning(error, "Error while gracefully disconnecting from Orleans cluster. Will ignore and continue to shutdown.");
                }
            });
        
        }
    }
}
