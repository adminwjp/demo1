

using Autofac;
using Autofac.Extras.DynamicProxy;
using NHibernate.Mapping.ByCode;
using NHibernate.Tool.hbm2ddl;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utility.Application.Services;
using Utility.Demo.Application.Services;
using Utility.Demo.Domain.Entities;
using Utility.Demo.Domain.Repositories;
using Utility.Demo.Nhibernate.EntityMappings;
using Utility.Domain.Repositories;
using Utility.Domain.Uow;
using Utility.Extensions;
using Utility.Interceptors;
using Utility.IO;
using Utility.Ioc;
using Utility.Nhibernate;
using Utility.Nhibernate.Infrastructure;
using Utility.Nhibernate.Repositories;
using Utility.Nhibernate.Uow;

namespace Utility.Demo
{
    public enum DemoOrmFlag
    {
        NHibernate,
        Ef,
        Dapper
    }
    //oa company socialcontact demo 哪些 共享
    //注意 要给 name 不然 默认 使用注入的最后一个服务对象
    //最后 注入 module 也没用
    //服务对象怎么 排序的不好控制 相同服务最后跟名称拿 泛型(普通类型)不可控
    //根据 name aop 无法使用了不好控制 ioc 直接 实例 中获取
    //必须 要 定义接口(最好 按规则 来 不然牵一发动全身) 才 行麻烦啊 妈的 混合 一起 到处出现问题 不好改啊
    public class DemoModule : Module
    {
        public static DemoOrmFlag Flag = DemoOrmFlag.NHibernate;
        string name;
        IIocManager iocManager;
        public DemoModule()
        {

        }
        public DemoModule(string name, IIocManager iocManager)
        {
            this.name = name;
            this.iocManager = iocManager;
        }

        protected override void Load(ContainerBuilder builder)
        {
            base.Load(builder);
            if(Flag== DemoOrmFlag.NHibernate)
            {
                builder.RegisterInstance<IAppSessionFactory>(new AppSessionFactory(config => {
                    config = config.Configure("Config/Demo/hibernate.cfg.xml");
                    foreach (var item in Directory.GetFiles("Config/Demo/hbm", "*.hbm.xml"))
                    {
                        config.AddXmlFile(item);
                    }
                    var mapper = new ModelMapper();
                    mapper.AddMapping(typeof(AddressMap));
                    mapper.AddMapping(typeof(SellerAddressMap));
                    mapper.AddMapping(typeof(AgentAddressMap));
                    mapper.AddMapping(typeof(ManufacturerAddressMap));

                    mapper.AddMapping(typeof(BankMap));

                    //Generic children mapp err xml pass
                    //mapper.AddMapping(typeof(CityMap));
                    //mapper.AddMapping(typeof(MenuMap));
                    
                    mapper.AddMapping(typeof(SmsMap));
                    mapper.AddMapping(typeof(SourceMaterialMap));
                    mapper.AddMapping(typeof(TokenMap));
                    mapper.AddMapping(typeof(UserFriendMap));
                    mapper.AddMapping(typeof(UserLogMap));
                    mapper.AddMapping(typeof(AdminMap));
                    mapper.AddMapping(typeof(UserMap));

                 
                    var domainMapping = mapper.CompileMappingForAllExplicitlyAddedEntities();
                    config.AddMapping(domainMapping);

                    SchemaExport export = new SchemaExport(config);
                    var dir = Directory.GetCurrentDirectory() + "/Sql/Demo";
                    FileHelper.CreateDirectory(dir);
                    export.SetOutputFile(Path.Combine(dir, Utility.ConfigHelper.DbFlag.ToString() + ".sql")); //设置输出目录
                                                                                                              // export.Drop(true, true);//设置生成表结构存在性判断,并删除
                    export.Create(false, false);//设置是否生成脚本,是否导出来
                })).Named("DemoAppSessionFactory", typeof(IAppSessionFactory))
                .SingleInstance();

                builder.Register(it => new SessionProvider(
                    it.ResolveNamed<IAppSessionFactory>("DemoAppSessionFactory")
                    //it.Resolve<AppSessionFactory>()
                    .SessionFactory)).As<SessionProvider>()
                    .Named<SessionProvider>("DemoSessionProvider")
                    .InstancePerDependency();

                builder.Register(it=>new NhibernateUnitWork(
                    it.ResolveNamed<SessionProvider>("DemoSessionProvider")
                    //it.Resolve<SessionProvider>()
                    ))
                    .As<IUnitWork>()
                    .Named<IUnitWork>("DemoUnitWork")
                    .InstancePerDependency()
                     .InterceptedBy(typeof(IocTranstationAopInterceptor)).EnableInterfaceInterceptors();

                builder.RegisterGeneric(
                     //typeof(BaseNhibernateRepository<,>)
                    (it, types) => {
                        var type = typeof(BaseNhibernateRepository<,>);
                        var obj = type.MakeGenericType(types);
                        //根据 泛型 创建
                        var val = Activator.CreateInstance(obj, new object[] { 
                            it.ResolveNamed<SessionProvider>("DemoSessionProvider")
                          // it.Resolve<SessionProvider>()
                        });
                        return val;
                    }
                    ).As(typeof(IRepository<,>))
                    .Named("DemoNhibernateRepository", typeof(IRepository<,>))
                    .InstancePerDependency()
                    .InterceptedBy(typeof(IocTranstationAopInterceptor)).EnableInterfaceInterceptors();
            }


            if (IocTranstationAopInterceptor.Many)
            {
                builder.RegisterGeneric(
                        //typeof(IsDefaultAppService<>)
                        (it, types) => {
                            var type = typeof(IsDefaultAppService<>);
                            var t= typeof(IRepository<,>);
                            var obj = type.MakeGenericType(types);
                            var r = t.MakeGenericType(new Type[] { types[0],typeof(long)});
                            //根据 泛型 创建
                            var val = Activator.CreateInstance(obj, new object[] {
                            it.ResolveNamed("DemoNhibernateRepository",r)
                            });
                            return val;
                        }
                )
                .As(typeof(IIsDefaultAppService<>))
                .InstancePerDependency().Named("IsDefaultAppService", typeof(IIsDefaultAppService<>))
                                .InterceptedBy(typeof(IocTranstationAopInterceptor))
                                .EnableInterfaceInterceptors();

                builder.RegisterGeneric(
                     //typeof(UserAppService<,>)
                     (it, types) => {
                         var type = typeof(UserAppService<,>);
                         var t = typeof(IRepository<,>);
                         var obj = type.MakeGenericType(types);
                         var r = t.MakeGenericType(types);
                         //根据 泛型 创建
                         var val = Activator.CreateInstance(obj, new object[] {
                            it.ResolveNamed("DemoNhibernateRepository",r)
                            });
                         return val;
                     }
                )
                .As(typeof(IUserAppService<,>))
                .InstancePerDependency().Named("UserAppService", typeof(IUserAppService<,>))
                .InterceptedBy(typeof(IocTranstationAopInterceptor))
                .EnableInterfaceInterceptors();
            }
            else
            {
                var types = new Type[]{ typeof(IsDefaultAppService<AddressEntity>),
                        typeof(IsDefaultAppService<BankEntity>),
                        typeof(UserAppService<AdminEntity,long>),
                        typeof(UserAppService<UserEntity,long>),
                    };
                foreach (var t in types)
                {
                    var n = $"Demo{t.Name.Split('`')[0]}";
                    builder.RegisterType(t)
                                .As(t).InstancePerDependency().Named(n, t)
                                  .InterceptedBy(typeof(IocTranstationAopInterceptor))
                                  .EnableClassInterceptors();
                }
            }

            //ex autofac 重新定义

            if (IocTranstationAopInterceptor.Many)
            {
                builder.Register(it => new CrudAppService<SourceMaterialEntity, long>(
                        it.ResolveNamed<IRepository<SourceMaterialEntity, long>>("DemoNhibernateRepository")))
                .As(typeof(ICrudAppService<SourceMaterialEntity, long>))
                .Named("DemoCrudAppService", typeof(ICrudAppService<SourceMaterialEntity, long>))
                    .InstancePerDependency()
                    .InterceptedBy(typeof(IocTranstationAopInterceptor))
                    .EnableInterfaceInterceptors();

                builder.Register(it => new MenuService(
                       it.ResolveNamed<IMenuRepository>("DemoMenuRepository"),
                       null))
               .As(typeof(ICrudAppService<MenuEntity, long>))
               .Named("DemoMenuService", typeof(ICrudAppService<MenuEntity, long>))
                   .InstancePerDependency()
                   .InterceptedBy(typeof(IocTranstationAopInterceptor))
                   .EnableInterfaceInterceptors();

                builder.Register(it => new  UserFriendAppService(
                       it.ResolveNamed<IRepository<UserFriendEntity, long>>("DemoNhibernateRepository")))
               .As(typeof(IUserFriendAppService))
               .Named("UserFriendAppService", typeof(IUserFriendAppService))
                   .InstancePerDependency()
                   .InterceptedBy(typeof(IocTranstationAopInterceptor))
                   .EnableInterfaceInterceptors();
            }
            else
            {
                var ts = new Type[] { typeof(CrudAppService<SourceMaterialEntity, long>) ,
                    typeof(MenuService),typeof(UserFriendAppService)};
                foreach (var t in ts)
                {
                    builder.RegisterType(t)
                                .As(t).Named($"Demo{t.Name.Split('`')[0]}", t)
                                  .InstancePerDependency()
                                  .InterceptedBy(typeof(IocTranstationAopInterceptor))
                                  .EnableClassInterceptors();
                }
            }


            builder.RegisterType<IocTranstationAopInterceptor>();
            builder.RegisterType<IocTranstationAopInterceptorAsync>();
            builder.RegisterGeneric(typeof(AsyncAopInterceptor<>));

        }
    }
}
