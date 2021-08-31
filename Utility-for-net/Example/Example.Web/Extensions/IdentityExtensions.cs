using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static SocialContact.SocialContactStart;

namespace Example.Web.Extensions
{
    public static class IdentityExtensions
    {
        public static void UseIdentity(this IServiceCollection services, IConfiguration Configuration)
        {
            //services.UseHealthCheck(Utility.Ef.ConnectionHelper.ConnectionString, "Config.Example",Utility.DbConfig.Flag); 统一 用 identity 项目  检测

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
           .AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, options => Configuration.Bind("JwtSettings", options))
           .AddCookie(CookieAuthenticationDefaults.AuthenticationScheme, options => Configuration.Bind("CookieSettings", options));
         

            //模型绑定 特性验证，自定义返回格式
            services.Configure<ApiBehaviorOptions>(options =>
            {
                options.InvalidModelStateResponseFactory = actionContext =>
                {
                    //获取验证失败的模型字段 
                    var errors = actionContext.ModelState
                    .Where(e => e.Value.Errors.Count > 0)
                    .Select(e => e.Value.Errors.First().ErrorMessage)
                    .ToList();
                    var str = string.Join("|", errors);
                    //设置返回内容
                    var result = new
                    {
                        code = 40000,
                        success = false,
                        data = str,
                        message = "fail"
                    };
                    return new BadRequestObjectResult(result);
                };
            });


            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
            .AddScheme<AuthenticationSchemeOptions, ApiAuthHandler>("Api", o => { })
            .AddCookie(options =>
            {
                 // Foward any requests that start with /api to that scheme
                 options.ForwardDefaultSelector = ctx =>
                 {
                return ctx.Request.Path.StartsWithSegments("/api") ? "Api" : null;
            };
                options.AccessDeniedPath = "/account/denied";
                options.LoginPath = "/account/login";
            });
            //https://docs.microsoft.com/zh-cn/aspnet/core/security/cookie-sharing?view=aspnetcore-2.2
            services.AddDataProtection()
            //.PersistKeysToFileSystem("{PATH TO COMMON KEY RING FOLDER}")
            .SetApplicationName("SharedCookieApp");

            services.ConfigureApplicationCookie(options =>
            {
                options.Cookie.Name = ".AspNet.SharedCookie";
            });
            //注册Cookie认证服务
            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie();
            services.Configure<CookiePolicyOptions>(options =>
            {
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;

                 //options.MinimumSameSitePolicy = (SameSiteMode)(-1);
                 //options.OnAppendCookie = cookieContext =>
                 //    CheckSameSite(cookieContext.Context, cookieContext.CookieOptions);
                 //options.OnDeleteCookie = cookieContext =>
                 //    CheckSameSite(cookieContext.Context, cookieContext.CookieOptions);
             });
        }
    }
}
