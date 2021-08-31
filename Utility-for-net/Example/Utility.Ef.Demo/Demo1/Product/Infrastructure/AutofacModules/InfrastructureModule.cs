#if NET45 || NET451 || NET452 || NET46 || NET461 || NET462 || NET47 || NET471 || NET472 || NET48 || NETCOREAPP2_0 || NETCOREAPP2_1 || NETCOREAPP2_2 || NETCOREAPP3_0 || NETCOREAPP3_1 || NET5_0 ||  NET6_0  || NETSTANDARD2_0 || NETSTANDARD2_1

using Autofac;
using Autofac.Annotation;
using Product.Domain.Entities;
using Product.Domain.Repositories;
using Product.Infrastructure.Repositories;
using System.Reflection;
using Utility.Domain.Repositories;
using Utility.EventBus.Abstractions;
using Utility.Extensions;
using Utility.Interceptors;

namespace Product.Infrastructure.AutofacModules
{

    public class InfrastructureModule
        : Autofac.Module
    {

        protected override void Load(ContainerBuilder builder)
        {
            //ex 
            builder.RegisterType<BrandRepository>().As<IBrandRepository>().InstancePerDependency()
                .Named<IBrandRepository>("BrandRepository")
                .InterceptedBy(typeof(IocTranstationAopInterceptor))
                //.InterceptedBy(typeof(AsyncAopInterceptor<IocTranstationAopInterceptorAsync>))
                .EnableInterfaceInterceptorsAsync(typeof(IBrandRepository));

            builder.RegisterType<CatagoryAttributeEfRepository>().As<IProductCatagoryAttributeRepository>().InstancePerDependency()
                 .Named<IProductCatagoryAttributeRepository>("CatagoryAttributeEfRepository")
                    .InterceptedBy(typeof(IocTranstationAopInterceptor))
                .EnableInterfaceInterceptorsAsync(typeof(IProductCatagoryAttributeRepository));

            builder.RegisterType<CatagoryEfRepository>().As<IProductCatagoryRepository>().InstancePerDependency()
                .InterceptedBy(typeof(IocTranstationAopInterceptorAsync)).Named<IProductCatagoryRepository>("CatagoryEfRepository")
                .EnableInterfaceInterceptorsAsync(typeof(IProductCatagoryRepository));

            builder.RegisterType<ProductAttributeEfRepository>().As<IProductAttributeRepository>().InstancePerDependency()
                .InterceptedBy(typeof(IocTranstationAopInterceptorAsync)).Named<IProductAttributeRepository>("ProductAttributeEfRepository")
                .EnableInterfaceInterceptorsAsync(typeof(IProductAttributeRepository));

            builder.RegisterType<ProductEfRepository>().As<IProductRepository>().InstancePerDependency()
                .InterceptedBy(typeof(IocTranstationAopInterceptorAsync)).Named<IProductRepository>("ProductEfRepository")
                .EnableInterfaceInterceptorsAsync(typeof(IProductRepository));

            builder.RegisterType<SpecEfRepository>().As<ISpecRepository>().InstancePerDependency()
                .InterceptedBy(typeof(IocTranstationAopInterceptorAsync)).Named<ISpecRepository>("SpecEfRepository")
                .EnableInterfaceInterceptorsAsync(typeof(ISpecRepository));


            //注解拦截器异常
            // 注册autofac打标签模式
            //builder.RegisterTypes(GetType().Assembly.GetExportedTypes()).PropertiesAutowired();
            //如果需要开启支持循环注入
            // builder.RegisterModule(new AutofacAnnotationModule(GetType().Assembly).SetAllowCircularDependencies(false));

        }
    }
}
#endif