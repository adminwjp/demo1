using Autofac;
using Config.Domain.Repositories;
using Config.Ef.Repositories;
using Autofac.Extras.DynamicProxy;
using Utility.Demo;
using Utility.Interceptors;

namespace Config.Dapper
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
