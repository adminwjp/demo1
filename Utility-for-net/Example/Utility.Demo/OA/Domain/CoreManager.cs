using Autofac;
using Microsoft.Extensions.DependencyInjection;
using OA.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using Utility;
using Utility.Domain.Repositories;
using Utility.Ioc;
using Utility.Nhibernate;
using Utility.Nhibernate.Infrastructure;
using Utility.Nhibernate.Repositories;
using N = NHibernate;

namespace OA.Domain
{
    public class CoreManager
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="services"></param>
        /// <param name="aspnetEntities"></param>
        public static void RegisterIoc(IServiceCollection services, bool aspnetEntities = false)
        {
            if (!aspnetEntities)
            {
                services.AddSingleton<Microsoft.Extensions.Logging.ILoggerFactory,Microsoft.Extensions.Logging.LoggerFactory>();
            }
            services.AddSingleton<AppSessionFactory>(it => new AppSessionFactory(config => {
                OA.Domain.NhibernateManger.Initial(config, it.GetService<Microsoft.Extensions.Logging.LoggerFactory>());
            }));
            services.AddScoped(it => new SessionProvider() { Session = it.GetService<AppSessionFactory>().OpenSession() });
            services.AddScoped(typeof(IRepository<>),typeof(BaseNhibernateRepository<>));
        }
        /// <summary>
        /// /
        /// </summary>
        /// <param name="aspnetEntities"></param>
        public static void RegisterAutofac(ContainerBuilder builder, bool aspnetEntities = false)
        {
            //if (!aspnetEntities)
            //{
            //    builder.RegisterType<Microsoft.Extensions.Logging.LoggerFactory>().As<Microsoft.Extensions.Logging.ILoggerFactory>().SingleInstance();
            //}
            builder.Register<AppSessionFactory>(it=> new AppSessionFactory(config=> {
                OA.Domain.NhibernateManger.Initial(config);
            })).Named<AppSessionFactory>("OAAppSessionFactory").SingleInstance();
            builder.Register<SessionProvider>(it=> new SessionProvider() { Session = it.ResolveNamed<AppSessionFactory>("OAAppSessionFactory").OpenSession() }).Named<SessionProvider>("OASession").InstancePerLifetimeScope();
            // builder.RegisterGeneric(typeof(BaseNhibernateRepository<>)).As(typeof(IRepository<>)).InstancePerLifetimeScope();
        }

        //public void InitModule()
        //{
        //    List<ModuleInfo> moduleInfos = new List<ModuleInfo>();
        //    moduleInfos.AddRange(new ModuleInfo[] {
        //        new ModuleInfo()
        //        {
        //            Name="人事管理",
        //            Modules=new List<ModuleInfo>() {
        //                new ModuleInfo(){ Name="档案管理"},
        //                new ModuleInfo(){ Name="考勤管理"},
        //                new ModuleInfo(){ Name="奖惩管理"},
        //                new ModuleInfo(){ Name="培训管理"}
        //            }
        //        },
        //        new ModuleInfo()
        //        {
        //            Name="待遇管理",
        //            Modules=new List<ModuleInfo>() {
        //                new ModuleInfo(){ Name="账套管理"},
        //                new ModuleInfo(){ Name="人员设置"},
        //                new ModuleInfo(){ Name="统计报表"}
        //            }
        //        },
        //        new ModuleInfo()
        //        {
        //            Name="系统维护",
        //            Modules=new List<ModuleInfo>() {
        //                new ModuleInfo(){ Name="企业架构"},
        //                new ModuleInfo(){ Name="基本资料"},
        //                new ModuleInfo(){ Name="初始化系统"}
        //            }
        //        },
        //        new ModuleInfo()
        //        {
        //            Name="用户管理",
        //            Modules=new List<ModuleInfo>() {
        //                new ModuleInfo(){ Name="新增用户"}
        //            }
        //        },
        //         new ModuleInfo()
        //        {
        //            Name="系统工具",
        //            Modules=new List<ModuleInfo>() {
        //                new ModuleInfo(){ Name="打开计算器"},
        //                new ModuleInfo(){ Name="打开Word"},
        //                new ModuleInfo(){ Name="打开Excel"}
        //            }
        //        }
        //    }
        //    );
        //}
    }
}
