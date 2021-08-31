﻿// Copyright (c) Brock Allen & Dominick Baier. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
//#define V4 //identityserver >=4.0 新版本 默认 使用 这个 版本  其他 版本 的出错 内容 不一致 https://www.cnblogs.com/zhanghm1/p/13366458.html git 样例没变
//#define V3 //identityserver <4.0 新版本

using Utility.IdentityServer.Data;
using Utility.IdentityServer.Models;
using IdentityServer.Validation;
using IdentityServer4;
using IdentityServer4.AspNetIdentity;
using IdentityServer4.Configuration;
using IdentityServer4.EntityFramework.DbContexts;
using IdentityServer4.EntityFramework.Interfaces;
using IdentityServer4.EntityFramework.Mappers;
using IdentityServerHost.Quickstart.UI;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.AspNetCore.Mvc.Versioning;
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

namespace IdentityServer
{
    public class IdentitySever4Startup
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
     
        public IdentitySever4Startup(IConfiguration configuration)
        {
            
               Configuration = configuration;
        }

        public IConfiguration Configuration { get; }
        public void ConfigureServices(IServiceCollection services)
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
            services.AddApiVersioning((Action<ApiVersioningOptions>)delegate (ApiVersioningOptions options)
            {
                options.ReportApiVersions = true;
                options.DefaultApiVersion = new ApiVersion(1, 0);
                options.AssumeDefaultVersionWhenUnspecified = true;
            });
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
            services.AddMvc(options =>
            {
                
               // options.ModelBinderProviders.Insert(0, new JsonBinderProvider());
                // options.ModelBinderProviders.Insert(0, new CustomModelBinderProvider());
                //options.InputFormatters.Insert(0, new XDocumentInputFormatter());

            })
            //全局配置Json序列化处理 方案1
            .AddNewtonsoftJson(options =>
            {
                // options.SerializerSettings.NullValueHandling = NullValueHandling.Ignore;
                //忽略循环引用
                options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
                //使用 AbC ab_c
                options.SerializerSettings.ContractResolver = JsonContractResolver.ObjectResolverJson;
                //设置时间格式
                options.SerializerSettings.DateFormatString = "yyyy-MM-dd HH:mm:ss";
            }
          )
          .AddXmlSerializerFormatters()
          .SetCompatibilityVersion(CompatibilityVersion.Latest);

            // uncomment, if you want to add an MVC-based UI
            services.AddControllersWithViews();
            services.AddRazorPages();
            //services.AddRazorPages(it=> {
            //    it.Conventions.AddAreaFolderApplicationModelConvention("admin", "/Areas/Admin/Views", page=> { 
                
            //    });
            //});
            //services.AddControllers();
            services.AddSwaggerGen(it => {
                it.SwaggerDoc("V1", new Microsoft.OpenApi.Models.OpenApiInfo() { Title = "IdentityServer", Version = "V1" });
            });
  

            string name = string.Empty;
            //https://www.cnblogs.com/jellydong/p/13542474.html 不然 http 登录不成功 
            // 配置cookie策略
            services.Configure<CookiePolicyOptions>(options =>
            {
                //https://docs.microsoft.com/zh-cn/aspnet/core/security/samesite?view=aspnetcore-3.1&viewFallbackFrom=aspnetcore-3
                options.MinimumSameSitePolicy = Microsoft.AspNetCore.Http.SameSiteMode.Lax;
            });

            services.AddDataProtection();
            string connectionString = string.Empty;
            //services.AddSingleton<IPersonalDataProtector, DefaultPersonalDataProtector>();
            //services.AddDbContext<TestDbContext>(it=> { 
            //    it.UseMySql("Database=IdentityServer;Data Source=localhost;User Id=root;Password=wjp930514.;Old Guids=True;charset=utf8;");
            //});//迁移 数据库 测试 
            IIdentityServerBuilder builder;
            if (!IdentityServer4Config.InMemory)
            { //无法创建表 语法错误 mysql sqlserver 基于 数据库 操作

                name = $"{ConfigHelper.DbFlag.ToString()}ConnectionString";

                connectionString = Configuration.GetConnectionString(name);
                Console.WriteLine(connectionString);
                var migrationsAssembly = typeof(IdentitySever4Startup).GetTypeInfo().Assembly.GetName().Name;
                //var bulder = new DbContextOptionsBuilder<TestDbContext>();
                //bulder.UseOracle("DATA SOURCE=192.168.99.101:1521/orcl;USER ID=myuser;PASSWORD=123456;", it=>it.UseOracleSQLCompatibility("12"));
                //var test = new TestDbContext(bulder.Options);//error ORA-00972: 标识符过长 

                //try
                //{
                //    var oracleConnection = new Oracle.ManagedDataAccess.Client.OracleConnection("DATA SOURCE=192.168.99.101:1521/orcl;USER ID=myuser;PASSWORD=123456;");
                //    oracleConnection.Open();//pass
                //}
                //catch (Exception e)
                //{

                //}

                // Add framework services.
                services.UseEf<ApplicationDbContext>(connectionString, migrationsAssembly, ConfigHelper.DbFlag, false, "");
                services.AddIdentity<ApplicationUser, IdentityRole>()
                    .AddEntityFrameworkStores<ApplicationDbContext>()
                    .AddDefaultTokenProviders();


                // Adds IdentityServer
                builder = services.AddIdentityServer(options =>
                {
                    if (IdentityServer4Config.Driver == Driver.Sqlite)
                    {
                        options.Events.RaiseErrorEvents = true;
                        options.Events.RaiseInformationEvents = true;
                        options.Events.RaiseFailureEvents = true;
                        options.Events.RaiseSuccessEvents = true;
                        //options.EmitLegacyResourceAudienceClaim = "native.code";
                        //options.ClientSecret = "secret";
                        //options.ResponseType = "code";
                        //admin
                        options.UserInteraction = new UserInteractionOptions
                        {
                            LogoutUrl = "/Account/Logout",
                            LoginUrl = "/Account/Login",
                            LoginReturnUrlParameter = "returnUrl"
                        };
                    }
                    options.IssuerUri = "null";
                    options.Authentication.CookieLifetime = TimeSpan.FromHours(2);
                })
                   .AddResourceOwnerValidator<ResourceOwnerPasswordValidator<ApplicationUser>>()
                //.AddResourceOwnerValidator<ResourceOwnerPasswordValidator>()//test
                //.AddTestUsers(TestUsers.Users)//测试 接口 用他的模板 不然需要改变接口
                // .AddSigningCredential(Certificate.Get())
                .AddAspNetIdentity<ApplicationUser>()
                //oracle 列过长 需要用到
                .AddConfigurationStore<ConfigurationDbContextWrapper>(options =>
                {

                    //services.AddSingleton<ConfigurationStoreOptions>(options);
                    options.ConfigureDbContext = builder => EfConnectionHelper.GetDbContextOptions(builder, connectionString, ConfigHelper.DbFlag);
                })
                .AddOperationalStore(options =>
                {
                    //services.AddSingleton<OperationalStoreOptions>(options);
                    options.ConfigureDbContext = builder => EfConnectionHelper.GetDbContextOptions(builder, connectionString, ConfigHelper.DbFlag);
                });
                //services.AddSingleton<ConfigurationDbContextWrapper, ConfigurationDbContextWrapper>();
                //services.AddSingleton<PersistedGrantDbContextWrapper, PersistedGrantDbContextWrapper>();
            }
            else 
            { //基于 内存 操作  
                builder = services.AddIdentityServer();
                if (IdentityServer4Config.Version == IdentityServer4Version.V4)
                {
                    builder.AddInMemoryApiScopes(Config.Scopes)//新版本 才有 必须存在

                            //#elif V3 
                            .AddInMemoryApiResources(Config.Apis)//connect/introspect 此接口 才 可以 调通 必须存在
                                                                 //#endif

                    .AddInMemoryIdentityResources(Config.Ids)
                    .AddInMemoryClients(Config.Clients)
                    .AddTestUsers(TestUsers.Users);
                }
            }
                services.AddAuthentication()
                      .AddCookie(options =>
                      {
                          options.Cookie.IsEssential = true;
                          options.Cookie.SameSite = SameSiteMode.None;
                      })
                   .AddGoogle("Google", options =>
                   {
                       options.SignInScheme = IdentityServerConstants.ExternalCookieAuthenticationScheme;

                       options.ClientId = "<insert here>";
                       options.ClientSecret = "<insert here>";
                   })
                   .AddOpenIdConnect("oidc", "Demo IdentityServer", options =>
                   {
                       options.SignInScheme = IdentityServerConstants.ExternalCookieAuthenticationScheme;
                       options.SignOutScheme = IdentityServerConstants.SignoutScheme;
                       options.SaveTokens = true;

                       options.Authority = "https://demo.identityserver.io/";
                       options.ClientId = "native.code";
                       options.ClientSecret = "secret";
                       options.ResponseType = "code";

                       options.TokenValidationParameters = new TokenValidationParameters
                       {
                           NameClaimType = "name",
                           RoleClaimType = "role"
                       };
                   });

            builder.AddDeveloperSigningCredential();

        }

        internal static Func<DbContextOptionsBuilder, DbContextOptionsBuilder> GetFunc(string v, string migrationsAssembly)
        {
            return it =>
            {
                EfConnectionHelper.GetDbContextOptions(it, v, ConfigHelper.DbFlag);
                return it;
            };
        }

        public void Configure(IApplicationBuilder app)
        {
            // this will do the initial DB population
            if (!IdentityServer4Config.InMemory)
                SeedData.EnsureSeedData(app);

        
            app.UseCookiePolicy();
            //app.UseCors(it=> {
            //    it.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin();
            //});
            app.UseSession();
            app.UseIdentityServer();
           
         
            //app.UseMvc(routes =>
            //{
            //    routes.MapRoute(
            //      name: "areas",
            //      template: "{area:exists}/{controller=Home}/{action=Index}/{id?}"
            //    );
            //});
            // app.UseMvcWithDefaultRoute();
        
        }
    }
}