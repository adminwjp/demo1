using Autofac;
using Autofac.Extras.DynamicProxy;
using Company.Ef;
using Utility.Demo;
using Utility.Domain.Uow;
using Utility.Ef;
using Utility.Ef.Repositories;
using Utility.Ef.Uow;
using Utility.Interceptors;

namespace Company
{
    public class CompanyModule : Autofac.Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            //InstancePerDependency close reader
            builder.Register(it=> {
                DesignTimeDbContextFactory designTimeDbContextFactory = new DesignTimeDbContextFactory();
                var db = designTimeDbContextFactory.CreateDbContext(null);
                return db;
            }).As<CompanyDbContext>().InstancePerDependency();
            builder.RegisterType<DbContextProvider<CompanyDbContext>>().As<DbContextProvider<CompanyDbContext>>().InstancePerDependency();
            builder.RegisterType<EfUnitWork<DbContextProvider<CompanyDbContext>>>().As<IUnitWork>().Named("CompanyUnitWork",typeof(IUnitWork)).InstancePerDependency()
                 .InterceptedBy(typeof(IocTranstationAopInterceptor)).EnableInterfaceInterceptors();

            builder.RegisterGeneric(typeof(BaseEfRepository<,,>)).As(typeof(BaseEfRepository<,,>))
                .Named("CompanyRepository", typeof(BaseEfRepository<,,>)).InstancePerDependency()
                ;//.InterceptedBy(typeof(IocTranstationAopInterceptor)).EnableInterfaceInterceptors();

            base.Load(builder);
        }
    }
}
