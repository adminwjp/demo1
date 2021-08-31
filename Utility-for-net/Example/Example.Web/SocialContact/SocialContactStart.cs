using Autofac;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Security.Claims;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using Utility.AspNetCore.Extensions;

namespace SocialContact
{
    public class SocialContactStart
    {
        public class MyUserAgentDetectionLib
        {
            public static bool DisallowsSameSiteNone(string userAgent)
            {
                // Cover all iOS based browsers here. This includes:
                // - Safari on iOS 12 for iPhone, iPod Touch, iPad
                // - WkWebview on iOS 12 for iPhone, iPod Touch, iPad
                // - Chrome on iOS 12 for iPhone, iPod Touch, iPad
                // All of which are broken by SameSite=None, because they use the iOS networking
                // stack.
                if (userAgent.Contains("CPU iPhone OS 12") ||
                    userAgent.Contains("iPad; CPU OS 12"))
                {
                    return true;
                }

                // Cover Mac OS X based browsers that use the Mac OS networking stack. 
                // This includes:
                // - Safari on Mac OS X.
                // This does not include:
                // - Chrome on Mac OS X
                // Because they do not use the Mac OS networking stack.
                if (userAgent.Contains("Macintosh; Intel Mac OS X 10_14") &&
                    userAgent.Contains("Version/") && userAgent.Contains("Safari"))
                {
                    return true;
                }

                // Cover Chrome 50-69, because some versions are broken by SameSite=None, 
                // and none in this range require it.
                // Note: this covers some pre-Chromium Edge versions, 
                // but pre-Chromium Edge does not require SameSite=None.
                if (userAgent.Contains("Chrome/5") || userAgent.Contains("Chrome/6"))
                {
                    return true;
                }

                return false;
            }
        }

        public class ApiAuthHandler : AuthenticationHandler<AuthenticationSchemeOptions>
        {
            private readonly ClaimsPrincipal _id;

            public ApiAuthHandler(IOptionsMonitor<AuthenticationSchemeOptions> options, ILoggerFactory logger, UrlEncoder encoder, ISystemClock clock) : base(options, logger, encoder, clock)
            {
                var id = new ClaimsIdentity("Api");
                id.AddClaim(new Claim(ClaimTypes.Name, "Hao", ClaimValueTypes.String, "Api"));
                _id = new ClaimsPrincipal(id);
            }

            protected override Task<AuthenticateResult> HandleAuthenticateAsync()
                => Task.FromResult(AuthenticateResult.Success(new AuthenticationTicket(_id, "Api")));
        }
        private void CheckSameSite(HttpContext httpContext, CookieOptions options)
        {
            if (options.SameSite == SameSiteMode.None)
            {
                var userAgent = httpContext.Request.Headers["User-Agent"].ToString();
                if (MyUserAgentDetectionLib.DisallowsSameSiteNone(userAgent))
                {
                    options.SameSite = (SameSiteMode)(-1);
                }

            }
        }



        public SocialContactStart(IConfiguration configuration)
        {
            this.Configuration = configuration;
        }
        public IConfiguration Configuration { get; }
       

        // This method gets called by the runtime. Use this method to add services to the container.
        public virtual IServiceProvider ConfigureServices(IServiceCollection services)
        {
            return null;
        }
        public virtual void ConfigureContainer(ContainerBuilder builder)
        {
            //Utility.Ioc.AutofacIocManager.Instance.Builder = builder;
            builder.RegisterModule(new SocialContactNhibernateModule());
            builder.RegisterModule(new SocialContactModule());
           
            //base.ConfigureContainer(builder);
        }
        private static void HandleMapTest(IApplicationBuilder app)
        {
            app.Run(async context =>
            {
                await context.Response.WriteAsync("Map Test ");
            });
        }
        private static void HandleBranch(IApplicationBuilder app)
        {
            app.Run(async context =>
            {
                var branchVer = context.Request.Query["token"];
                await context.Response.WriteAsync($"Branch used = {branchVer}");
            });
        }
        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public virtual void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
          
            //app.UseSession();
            //var core = app.ApplicationServices.GetService<Data.Core>();
            //core.Initial();//订阅信息初始化
            //core.InitialMain();//发布信息初始化
            app.MapWhen(context => context.Request.Query.ContainsKey("token"),
                                 HandleBranch);
            app.Map("/map", HandleMapTest);
            //app.Use(async (context, next) =>
            //{
            //    // Do work that doesn't write to the Response.
            //    await next.Invoke();
            //    // Do logging or other work that doesn't write to the Response.
            //});
          
         }

        public virtual void Configure(IEndpointRouteBuilder configure)
        {
            configure.MapAreaControllerRoute(
                  name: "social_contact",
                  areaName: "social_contact",
                  pattern: "{social_contact:exists}/{controller}/{action}/{id?}"
                );
        }
    }
}
