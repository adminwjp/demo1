using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Threading.Tasks;
using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Microsoft.Extensions.Hosting;
using Ocelot.DependencyInjection;
using Ocelot.Middleware;
using Ocelot.Provider.Consul;
//using Ocelot.Provider.Eureka;
using Ocelot.Provider.Polly;

namespace ApiGateways
{
    /// <summary>
    ///参考 https://www.cnblogs.com/SuperDust/p/12595299.html 基于  consul ocelot 微服务 搭配 使用
    /// </summary>
    public class Startup
    {
        private readonly IConfiguration _cfg;

        public Startup(IConfiguration configuration)
        {
            _cfg = configuration;
        }
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public  void ConfigureServices(IServiceCollection services)
        {
            var identityUrl = _cfg.GetValue<string>("IdentityUrl");
            var authenticationProviderKey = "IdentityApiKey";

            //services.AddHealthChecks()
            //    .AddCheck("self", () => HealthCheckResult.Healthy())
            //    .AddUrlGroup(new Uri(_cfg["CatalogUrlHC"]), name: "catalogapi-check", tags: new string[] { "catalogapi" })
            //    .AddUrlGroup(new Uri(_cfg["OrderingUrlHC"]), name: "orderingapi-check", tags: new string[] { "orderingapi" })
            //    .AddUrlGroup(new Uri(_cfg["BasketUrlHC"]), name: "basketapi-check", tags: new string[] { "basketapi" })
            //    .AddUrlGroup(new Uri(_cfg["IdentityUrlHC"]), name: "identityapi-check", tags: new string[] { "identityapi" })
            //    .AddUrlGroup(new Uri(_cfg["MarketingUrlHC"]), name: "marketingapi-check", tags: new string[] { "marketingapi" })
            //    .AddUrlGroup(new Uri(_cfg["PaymentUrlHC"]), name: "paymentapi-check", tags: new string[] { "paymentapi" })
            //    .AddUrlGroup(new Uri(_cfg["LocationUrlHC"]), name: "locationapi-check", tags: new string[] { "locationapi" });

            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy",
                    builder => builder
                    .AllowAnyMethod()
                    .AllowAnyHeader()
                    .SetIsOriginAllowed((host) => true)
                    .AllowCredentials());
            });

            //services.AddAuthentication()
            //    .AddJwtBearer(authenticationProviderKey, x =>
            //    {

            //        x.Authority = "https://localhost:44377";
            //        x.RequireHttpsMetadata = false;
            //        x.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters()
            //        {
            //            ValidAudiences = new[] { "orders", "basket", "locations", "marketing", "mobileshoppingagg", "webshoppingagg" }
            //        };
            //        x.Events = new Microsoft.AspNetCore.Authentication.JwtBearer.JwtBearerEvents()
            //        {
            //            OnAuthenticationFailed = async ctx =>
            //            {
            //                int i = 0;
            //            },
            //            OnTokenValidated = async ctx =>
            //            {
            //                int i = 0;
            //            },

            //            OnMessageReceived = async ctx =>
            //            {
            //                int i = 0;
            //            }
            //        };
            //    });

            services.AddOcelot(_cfg)
                       .AddConsul()
                      //.AddEureka()
                      .AddPolly();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            //if (env.IsDevelopment())
            //{
            //    app.UseDeveloperExceptionPage();
            //}

        

          

            var pathBase = _cfg["PATH_BASE"];

            if (!string.IsNullOrEmpty(pathBase))
            {
                app.UsePathBase(pathBase);
            }

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            //app.UseHealthChecks("/hc", new HealthCheckOptions()
            //{
            //    Predicate = _ => true,
            //    ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
            //});

            //app.UseHealthChecks("/liveness", new HealthCheckOptions
            //{
            //    Predicate = r => r.Name.Contains("self")
            //});

            app.UseCors("CorsPolicy");
            app.UseOcelot().Wait();


         
        }
    }
}
