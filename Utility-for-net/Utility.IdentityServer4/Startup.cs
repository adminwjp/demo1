// Copyright (c) Brock Allen & Dominick Baier. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
//#define V4 //identityserver >=4.0 新版本 默认 使用 这个 版本  其他 版本 的出错 内容 不一致 https://www.cnblogs.com/zhanghm1/p/13366458.html git 样例没变
//#define V3 //identityserver <4.0 新版本

using IdentityServer4;
using IdentityServer4.AspNetIdentity;
using IdentityServer4.Configuration;
using IdentityServer4.EntityFramework.DbContexts;
using IdentityServer4.EntityFramework.Interfaces;
using IdentityServer4.EntityFramework.Mappers;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using System;
using System.Linq;
using System.Reflection;
using Utility.Json;
using Utility.Ef;
using Utility;
using Utility.IdentityServer4;
using Utility.IdentityServer.Data;
using Utility.IdentityServer.Models;

namespace Utility.IdentityServer
{
    public class Startup
    {
        public static readonly JsonSerializerSettings JsonSerializerSettings = new JsonSerializerSettings()
        {
            
            //忽略循环引用
            ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
            //使用 ab_c AbC  实际 AbC  ab_c 
            ContractResolver = //JsonContractResolver.ObjectResolverJson,
            JsonContractResolver.JsonResolverObject,
            //设置时间格式
            DateFormatString = "yyyy-MM-dd HH:mm:ss"
        };
     
        public Startup(IConfiguration configuration)
        {
            
               Configuration = configuration;
        }

        public IConfiguration Configuration { get; }
        public virtual void ConfigureServices(IServiceCollection services)
        {          
            // JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear();//InvalidOperationException: sub claim is missing
            //注册Cookie认证服务
            //services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie();
            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });
            services.AddSession();
           
            services.Configure<IISOptions>(iis =>
            {
                iis.AuthenticationDisplayName = "Windows";
                iis.AutomaticAuthentication = false;
            });

            // ..or configures IIS in-proc settings
            services.Configure<IISServerOptions>(iis =>
            {
                iis.AuthenticationDisplayName = "Windows";
                iis.AutomaticAuthentication = false;
                iis.AllowSynchronousIO = true;
            });
            //services.Configure<RazorViewEngineOptions>(options =>
            //{
            //    //options.AreaViewLocationFormats.Clear();
            //    options.AreaViewLocationFormats.Add("/Admin/{2}/Views/{1}/{0}.cshtml");
            //    options.AreaViewLocationFormats.Add("/Admin/{2}/Views/Shared/{0}.cshtml");
            //    options.AreaViewLocationFormats.Add("/Admin/Views/Shared/{0}.cshtml");
            //    options.AreaViewLocationFormats.Add("/Admin/Views/_ViewStart.cshtml");
            //});
            //services.AddApiVersioning(options =>
            //{
            //    options.ReportApiVersions = true;//return versions in a response header
            //    options.DefaultApiVersion = new ApiVersion(1, 0);//default version select 
            //    options.AssumeDefaultVersionWhenUnspecified = true;//if not specifying an api version,show the default version
            //});
           services.AddControllersWithViews();
            services.AddRazorPages();
            
  

            string name = string.Empty;
            //https://www.cnblogs.com/jellydong/p/13542474.html 不然 http 登录不成功 
            // 配置cookie策略
            services.Configure<CookiePolicyOptions>(options =>
            {
                //https://docs.microsoft.com/zh-cn/aspnet/core/security/samesite?view=aspnetcore-3.1&viewFallbackFrom=aspnetcore-3
                options.MinimumSameSitePolicy = Microsoft.AspNetCore.Http.SameSiteMode.Lax;
            });
            services.AddDataProtection();
          

        }

        internal static Func<DbContextOptionsBuilder, DbContextOptionsBuilder> GetFunc(string v, string migrationsAssembly)
        {
            return it =>
            {
                EfConnectionHelper.GetDbContextOptions(it, v, ConfigHelper.DbFlag);
                return it;
            };
        }

        public virtual void Configure(IApplicationBuilder app)
        {
            // uncomment if you want to add MVC
            app.UseStaticFiles();
            app.UseRouting();
            app.UseCookiePolicy();
            //app.UseCors(it=> {
            //    it.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin();
            //});
            app.UseSession();
            // uncomment, if you want to add MVC-based
            app.UseAuthorization();
            //app.UseMvc(routes =>
            //{
            //    routes.MapRoute(
            //      name: "areas",
            //      template: "{area:exists}/{controller=Home}/{action=Index}/{id?}"
            //    );
            //});
            // app.UseMvcWithDefaultRoute();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                   name: "mvc",
                   pattern: "{controller=Home}/{action=Index}/{id?}"
                 );
                endpoints.MapAreaControllerRoute(
                    name: "areas",
                    areaName: "admin",
                    pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}"
                  );
             
                endpoints.MapDefaultControllerRoute();
            });
        }
    }
}