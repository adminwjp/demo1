using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Autofac;
using Autofac.Extras.DynamicProxy;
using NHibernate.Mapping.ByCode;
using NHibernate.Tool.hbm2ddl;
using SocialContact.Infrastructure.EntityMappings;
using Utility;
using Utility.Application.Services;
using Utility.Demo;
using Utility.Demo.Domain.Entities;
using Utility.Demo.Nhibernate.EntityMappings;
using Utility.Domain.Repositories;
using Utility.Domain.Uow;
using Utility.Helpers;
using Utility.IO;
using Utility.Nhibernate;
using Utility.Nhibernate.Infrastructure;
using Utility.Nhibernate.Repositories;
using Utility.Nhibernate.Uow;
using Utility.Mappers;
using Utility.Interceptors;
using SocialContact.Domain.Entities;
using SocialContact.Infrastructure.Repositories;
using Utility.Extensions;
using Utility.Logs;

namespace SocialContact
{
    public class SocialContactNhibernateModule:Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            base.Load(builder);
            builder.Register<IMapper>(it => AutoMapperMapper.Empty).SingleInstance();

            //SocialContact
            builder.RegisterInstance(new AppSessionFactory(config => {
                config = config.Configure("Config/SocialContact/hibernate.cfg.xml");
                //config.AddXmlFile("Config/hbm/social_contact.hbm.xml");
                // config.Interceptor = new SQLWatcher(_loggerFactory);
                //移动 位置 xml 需要 改变程序集 名称
                //直接使用 map
              
                //难道用法改变 了 ?
                //最好 使用 hbm.xml
                //如果 启动后 使用出现其它问题 最好 降版本
                //fk many 这个 版本 有问题(mapp) hbm 没问题
                if(ConfigHelper.OrmFlag== OrmFlag.Map)
                {
                    var mapper = new ModelMapper();
                    mapper.AddMapping(typeof(WorkMap));
                    mapper.AddMapping(typeof(CategoryMap));
                    mapper.AddMapping(typeof(EdutionMap));
                    mapper.AddMapping(typeof(IconMap));
                    mapper.AddMapping(typeof(UserMenuMap));
                    //foreach (var item in type.Assembly.GetTypes())
                    //{
                    //    if (item.Namespace == type.Namespace && TypeHelper.IsCustomClass(item))
                    //    {
                    //        mapper.AddMapping(item);
                    //    }
                    //}

                    //不支持 泛型 mapp 最好 不要 使用泛型 
                    //ex  Could not determine type for
                    mapper.AddMapping(typeof(MenuMap));
                    mapper.AddMapping(typeof(SourceMaterialMap));
                    var domainMapping = mapper.CompileMappingForAllExplicitlyAddedEntities();
                    config.AddMapping(domainMapping);
                }
                else if (ConfigHelper.OrmFlag == OrmFlag.Xml)
                {
                    foreach (var item in Directory.GetFiles("Config/SocialContact/hbm", "*.hbm.xml"))
                    {
                        if (item.Contains("demo"))
                        {
                           // continue;
                        }
                        config.AddXmlFile(item);
                    }
                    // var mapper = new ModelMapper();
                    //mapper.AddMapping(typeof(WorkMap));
                    //mapper.AddMapping(typeof(CategoryMap));
                    //mapper.AddMapping(typeof(EdutionMap));
                    //mapper.AddMapping(typeof(IconMap));
                    //mapper.AddMapping(typeof(UserMenuMap)); 
                    //var domainMapping = mapper.CompileMappingForAllExplicitlyAddedEntities();
                    //config.AddMapping(domainMapping);

                    // config.AddXmlFile("Config/hbm/demo.hbm.xml");
                    //config.AddXmlFile("Config/SocialContact/hbm/admin.hbm.xml");
                    //config.AddXmlFile("Config/SocialContact/hbm/catagory.hbm.xml");

                }

                SchemaExport export = new SchemaExport(config);
                var dir = Directory.GetCurrentDirectory() + "/Sql/SocialContact";
                FileHelper.CreateDirectory(dir);
                export.SetOutputFile(Path.Combine(dir, Utility.ConfigHelper.DbFlag.ToString()+".sql")); //设置输出目录
                // export.Drop(true, true);//设置生成表结构存在性判断,并删除
                export.Create(false, false);//设置是否生成脚本,是否导出来
            }))
                .Named<AppSessionFactory>("SocialContactAppSessionFactory").SingleInstance();

            builder.Register(it=>new SessionProvider(
                it.ResolveNamed<AppSessionFactory>("SocialContactAppSessionFactory")
                .SessionFactory)).As<SessionProvider>().Named<SessionProvider>("SocialContactSessionProvider")
                .InstancePerDependency();

            builder.RegisterType<NhibernateUnitWork>().As<IUnitWork>().InstancePerDependency().
                Named<IUnitWork>("SocialContactUnitWork")
                 .InterceptedBy(typeof(IocTranstationAopInterceptor)).EnableClassInterceptors();

            //autofac is not assignable to service 
           
            builder.Register(it=>new AdminRepository(
                it.ResolveNamed<SessionProvider>("SocialContactSessionProvider"),
                it.Resolve<ILog<AdminRepository>>()
            )).As(typeof(IRepository<AdminEntity, long>))
            .InstancePerDependency()
            .InterceptedBy(typeof(IocTranstationAopInterceptor)).EnableInterfaceInterceptors();

            builder.Register(it => new EdutionRepository(
                 it.ResolveNamed<SessionProvider>("SocialContactSessionProvider"),
                 it.Resolve<ILog<EdutionRepository>>()
             )).As(typeof(IRepository<EdutionEntity, long>))
             .InstancePerDependency()
             .InterceptedBy(typeof(IocTranstationAopInterceptor)).EnableInterfaceInterceptors();

            builder.Register(it => new WorkRepository(
                it.ResolveNamed<SessionProvider>("SocialContactSessionProvider"),
                it.Resolve<ILog<WorkRepository>>()
            )).As(typeof(IRepository<WorkEntity, long>))
            .InstancePerDependency()
            .InterceptedBy(typeof(IocTranstationAopInterceptor)).EnableInterfaceInterceptors();


            builder.Register(it => new CatagoryRepository(
                it.ResolveNamed<SessionProvider>("SocialContactSessionProvider")
            )).As(typeof(IRepository<CatagoryEntity, long>))
            .InstancePerDependency()
            .InterceptedBy(typeof(IocTranstationAopInterceptor)).EnableInterfaceInterceptors();



            builder.RegisterGeneric(
                //typeof(BaseNhibernateRepository<,>)
                (it, types1) => {
                    var type = typeof(BaseNhibernateRepository<,>);
                    var obj = type.MakeGenericType(types1);
                        //根据 泛型 创建
                        var val = Activator.CreateInstance(obj, new object[] {
                                it.ResolveNamed<SessionProvider>("SocialContactSessionProvider")
                        });
                    return val;
                }
            ).As(typeof(IRepository<,>)).InstancePerDependency()
            .Named("SocialContactRepository", typeof(IRepository<,>))
           .InterceptedBy(typeof(IocTranstationAopInterceptor)).EnableInterfaceInterceptors();

            builder.RegisterGeneric(
             //typeof(CrudAppService<,>)
                 (it, types1) => {
                     var type = typeof(CrudAppService<,>);
                     var t = typeof(IRepository<,>);
                     var obj = type.MakeGenericType(types1);
                     var r = t.MakeGenericType(types1);
                        //根据 泛型 创建
                        var val = Activator.CreateInstance(obj, new object[] {
                                    it.ResolveNamed("SocialContactRepository",r)
                         });
                     return val;
                 }
             ).As(typeof(CrudAppService<,>)).InstancePerDependency()
             .Named("SocialContactAppService", typeof(CrudAppService<,>));
            //builder.RegisterAssemblyTypes(typeof(NhibernateUnitWork).Assembly);
        }
    }
}
