using System.Reflection;
using Autofac;
using Autofac.Extras.DynamicProxy;
using Castle.DynamicProxy;
using Config.Application.Interceptors;
using Config.Application.Services;
using Config.Domain.Entities;
using Config.Domain.Repositories;
using Utility.Demo;
using Utility.Domain.Repositories;
using Utility.Interceptors;

namespace Config.Application
{
    public class ConfigServiceModule: Autofac.Module
    {
        protected override void Load(ContainerBuilder builder)
        {
           // builder.RegisterType<Ef.DAL.ConfigEfManager>().As<DAL.IConfigManager>().OwnedByLifetimeScope();

            builder.Register(it => it.Resolve<IConfigManager>().Config).As<IRepository<ConfigEntity,string>>().OwnedByLifetimeScope();

            builder.Register(it => it.Resolve<IConfigManager>().Service).As<IRepository<ServiceEntity, string>>().OwnedByLifetimeScope();

            builder.RegisterType<Utility.Cache.NetCoreCache>().As<Utility.Cache.ICacheContent>();



            //builder.RegisterAssemblyTypes(typeof(BLL.ConfigBLL).GetTypeInfo().Assembly)
            // .AsClosedTypesOf(typeof(Utility.BLL.IBLL<,>)).OwnedByLifetimeScope();


            //NoConstructorsFoundException: No accessible constructors were found for the type 'Config.BLL.IBLL'.
            // builder.RegisterType<IBLL>().EnableInterfaceInterceptors();

            //Type DALImpl contains generic parameters (Parameter 'type')
            //builder.RegisterType(typeof(Utility.BLL.BLL<,,>)).EnableInterfaceInterceptors();


            builder.RegisterGeneric(typeof(Utility.Application.Services.CrudAppService<,>)).InterceptedBy(typeof(ServiceInterceptor)).EnableInterfaceInterceptors()
                 .InterceptedBy(typeof(IocTranstationAopInterceptor)).EnableInterfaceInterceptors();
            //builder.RegisterType<ServiceBLL>().InterceptedBy(typeof(BLLInterceptor)).EnableClassInterceptors();

            builder.RegisterAssemblyTypes(typeof(ConfigResponseApiService).GetTypeInfo().Assembly)
   .AsClosedTypesOf(typeof(Utility.Application.Services.IResponseApiService<,>)).AsSelf().OwnedByLifetimeScope()
    .InterceptedBy(typeof(IocTranstationAopInterceptor)).EnableInterfaceInterceptors();

            builder.RegisterType<ServiceInterceptor>();
           



            base.Load(builder);
        }
    }
}
