#if NET40 || NET45 || NET451 || NET452 || NET46 || NET461 || NET462 || NET47 || NET471 || NET472 || NET48
using Autofac;
using Config.Domain.Repositories;
using Config.EnterpriseLibrary.Repositories;

namespace Config.EnterpriseLibrary
{
    public class ConfigEnterpriseLibraryDALModule : Autofac.Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<ConfigEnterpriseLibraryManager>().As<Config.DAL.IConfigManager>().OwnedByLifetimeScope()
             .InterceptedBy(typeof(IocAopInterceptor)).EnableInterfaceInterceptors();
            base.Load(builder);
        }
    }
}
#endif