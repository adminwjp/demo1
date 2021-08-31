#if NETCOREAPP3_1
using Microsoft.Extensions.DependencyInjection;
using System;
using Utility.Domain.Entities;
using Utility.Nhibernate;
using Utility.Nhibernate.Infrastructure;
using AspNetCoreLogger = Microsoft.Extensions.Logging;
using Utility.Domain.Uow;
using Utility.Nhibernate.Uow;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Utility.Ef;
using Utility.Ef.Uow;
using AutoMapper;
using System.Reflection;
using Utility.Application.Services;
using Menu.HuiAdminMenus;
using Menu.Menus;
using Menu.Bases;
using NHibernate;
using Menu.EasyUIMenus;
using Menu.ElementMenus;
using Utility.Application.Services.Dtos;
using Menu.ElementMenus.Dto;

namespace Menus
{
    public class HuiAdminMenuService : ApplicationService
    {
        private BaseMenuNhibernateRepository<HuiAdminMenuEntity> baseMenuNhibernateRepositry;

        public HuiAdminMenuService(BaseMenuNhibernateRepository<HuiAdminMenuEntity> baseMenuNhibernateRepositry)
        {
            this.baseMenuNhibernateRepositry = baseMenuNhibernateRepositry;
        }

        public void Insert(HuiAdminMenuEntity entity)
        {
            HuiAdminMenuEntity huiAdminMenuEntity = ObjectMapper.Map<HuiAdminMenuEntity>(entity);
        }


        public void Update(HuiAdminMenuEntity entity)
        {

        }


        public void DeleteList(DeleteEntity<Guid> deleteEntity)
        {

        }
    }
}

namespace Menu
{
    public class MenuHelper
    {

        public static void Mapp(IServiceCollection services)
        {
            var ioc = new Utility.ObjectMapping.AutoMapperObjectMapper();
            services.AddSingleton<Utility.ObjectMapping.IObjectMapper>((it) => ioc);
            ioc.Init(config =>
            {
                Type autoMapType = typeof(AutoMapAttribute);
                //注解怎么 使用了 难道手动
                foreach (Type type in typeof(IMenuAppService).Assembly.GetTypes())
                {
                    var attribute = type.GetCustomAttribute(autoMapType, false);
                    if (attribute != null)
                    {
                        AutoMapAttribute autoMapAttribute = (AutoMapAttribute)attribute;
                        config.CreateMap(autoMapAttribute.SourceType, type);
                        config.CreateMap(type, autoMapAttribute.SourceType);
                    }
                }
                config.CreateMap(typeof(ResultDto<>), typeof(ResultDto<>));
                config.CreateMap<ElementMenuEntity, ElementMenuCategoryDto>()
                .ForMember(it=>it.Label,it=>it.MapFrom(it=>it.Name))
                .ForMember(it => it.Value, it => it.MapFrom(it => it.Id))
                .ForMember(it => it.Style, it => it.MapFrom(it => it.Icon))
                .ForMember(it => it.Children, it => it.MapFrom(it => it.Children));
            });
        }
        public static void InitDataByEf(IApplicationBuilder app)
        {
            //ef 
            var dbContext = app.ApplicationServices.GetRequiredService<MenuDbContext>();
            var menuRepository = new MenuEfRepository(dbContext);
            MenuManager.InitData(menuRepository);
        }

        public static void Ef(IServiceCollection services,string connectionString)
        {
            services.AddDbContext<MenuDbContext>(
             b => b
            .UseSqlServer(connectionString, providerOptions =>
            {
                providerOptions.EnableRetryOnFailure();
                   //providerOptions.MigrationsAssembly("");
               })
            .AddInterceptors(new HintCommandInterceptor()));
            services.AddScoped<IUnitWork, EfUnitWork>();//ef
        }

        public static void InitDataByNhibernate(IApplicationBuilder app)
        {
            //nhibernate
            using (var serviceScope = app.ApplicationServices.CreateScope())
            {
                // var session = app.ApplicationServices.GetRequiredService<ISession>();
                var session = serviceScope.ServiceProvider.GetRequiredService<ISession>();
                var menuRepository = new MenuNhibernateRepository(session);
                MenuManager.InitData(menuRepository,true);
                //easyui menu
                IEasyUIMenuRepository easyUIMenuRepository = new EasyUIMenuNhibernateepository(session);
                EasyUIMenuManager.InitDataByNhibernate(easyUIMenuRepository);
                //element menu
                IElementMenuRepository elementMenuRepository = new ElementMenuNhibernateepository(session);
                ElementMenuManager.InitDataByNhibernate(elementMenuRepository);
            }
        }
        

        public static void Nhibernate(IServiceCollection services )
        {
            services.AddSingleton<AppSessionFactory>(it =>
            {
                var loggerFactory = it.GetRequiredService<AspNetCoreLogger.ILoggerFactory>();
                var watcher = new SQLWatcher(loggerFactory);
                return new AppSessionFactory(watcher);
            });

            // mapp(级联添加子集 没添加)
            services.AddSingleton<AppSessionFactory>(it =>
            {
                return new AppSessionFactory(config =>
                {
                    config = config.Configure("Config/hibernate.cfg.xml");
                    var loggerFactory = it.GetRequiredService<AspNetCoreLogger.ILoggerFactory>();
                    config.Interceptor = new SQLWatcher(loggerFactory);
                }, new Type[] { typeof(MenuNhibernateMap), typeof(HuiAdminMenuNhibernateMap), 
                //sqlite 奇葩 这张表创建不了(手动建个表后面会更新) 原来 group 关键词出错 但不报异常 根本不知道
                    typeof(ElementMenuNhibernateMap),
                    typeof(EasyUIMenuNhibernateMap) });
            });
            services.AddScoped(it => it.GetService<AppSessionFactory>().OpenSession());
            services.AddScoped<IUnitWork, NhibernateUnitWork>();
        }
    }
}
#endif