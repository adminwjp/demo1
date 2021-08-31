using Autofac;
using Autofac.Extras.DynamicProxy;
using Config.Domain.Repositories;
using Config.Dapper.Repositories;
using Utility.Demo;
using Utility.Interceptors;

namespace Config.Ef
{
    public class ConfigEfModule : Autofac.Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<ConfigManager>().As<IConfigManager>().OwnedByLifetimeScope()
                 .InterceptedBy(typeof(IocTranstationAopInterceptor)).EnableInterfaceInterceptors();
            base.Load(builder);
        }
    }
}
