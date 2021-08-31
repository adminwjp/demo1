#if NET45 || NET451 || NET452 || NET46 || NET461 || NET462 || NET47 || NET471 || NET472 || NET48 || NETCOREAPP2_0 || NETCOREAPP2_1 || NETCOREAPP2_2 || NETCOREAPP3_0 || NETCOREAPP3_1 || NET5_0 ||  NET6_0  || NETSTANDARD2_0 || NETSTANDARD2_1
using Autofac;
using Autofac.Annotation;
using Microsoft.Extensions.Logging;
using Product.Application.Services.Brands;
using Product.Application.Services.IntegrationEvents.EventHandling;
using Product.Application.Services.ProductAttributes;
using Product.Application.Services.ProductCatagories;
using Product.Application.Services.ProductCatagoryAttributes;
using Product.Application.Services.Products;
using Product.Application.Services.Specs;
using Product.Domain.Entities;
using Product.Domain.Repositories;
using System.Reflection;
using Utility.Cache;
using Utility.Domain.Repositories;
using Utility.EventBus.Abstractions;
using Utility.Extensions;
using Utility.Interceptors;
using Utility.Mappers;

namespace Product.Application.Services.AutofacModules
{

    public class ApplicationModule
        : Utility.Infrastructure.AutofacModules.ApplicationModule
    {

        public string QueriesConnectionString { get; }

        public ApplicationModule(string qconstr)
        {
            QueriesConnectionString = qconstr;

        }

        protected override void Load(ContainerBuilder builder)
        {


            //builder.RegisterType<RequestManager>()
            //   .As<IRequestManager>()
            //   .InstancePerLifetimeScope();
           builder.RegisterBuildCallback(container =>
            {
                Utility.Ioc.AutofacIocManager.Instance.Container = (IContainer)container;
            });
            if (Utility.ConfigHelper.AnnationIoc)
            { 
                // 注册autofac打标签模式
               builder.RegisterTypes(GetType().Assembly.GetExportedTypes()).PropertiesAutowired();
               //如果需要开启支持循环注入
               builder.RegisterModule(new AutofacAnnotationModule(GetType().Assembly).SetAllowCircularDependencies(false));

                return;
            }
            builder.RegisterAssemblyTypes(typeof(NumberChangeIntegrationEventHandler).GetTypeInfo().Assembly)
                .AsClosedTypesOf(typeof(IIntegrationEventHandler<>));

            builder.Register(it => new BrandAppService(
                    it.ResolveNamed<IBrandRepository>("BrandRepository"),
                    it.Resolve<IMapper>(),
                    it.Resolve<ICacheContent>()
                )).As<IBrandAppService>().InstancePerDependency()
                .Named<IBrandAppService>("BrandAppService")
                .InterceptedBy(typeof(IocTranstationAopInterceptor))
               .EnableInterfaceInterceptorsAsync(typeof(IBrandAppService));


            builder.Register(it => new ProductAttributeAppService(
                 it.ResolveNamed<IProductAttributeRepository>("ProductAttributeEfRepository"),
                 it.Resolve<IMapper>(),
                 it.Resolve<ICacheContent>()
             )).As<IProductAttributeAppService>().InstancePerDependency()
            .InterceptedBy(typeof(IocTranstationAopInterceptor)).Named<IProductAttributeAppService>("ProductAttributeAppService")
            .EnableInterfaceInterceptorsAsync(typeof(IProductAttributeRepository));

            builder.Register(it => new ProductCatagoryAppService(
                 it.ResolveNamed<IProductCatagoryRepository>("ProductCatagoryEfRepository"),
                 it.Resolve<ILogger<ProductCatagoryAppService>>()
             )).As<IProductCatagoryAppService>().InstancePerDependency()
            .InterceptedBy(typeof(IocTranstationAopInterceptor)).Named<IProductCatagoryAppService>("ProductCatagoryAppService")
            .EnableInterfaceInterceptorsAsync(typeof(IProductCatagoryRepository));

            builder.Register(it => new ProductCatagoryAttributeAppService(
                 it.ResolveNamed<IProductCatagoryAttributeRepository>("ProductCatagoryAttributeEfRepository"),
                 it.Resolve<IMapper>(),
                 it.Resolve<ICacheContent>()
             )).As<IProductCatagoryAttributeAppService>().InstancePerDependency()
            .InterceptedBy(typeof(IocTranstationAopInterceptor)).Named<IProductCatagoryAttributeAppService>("ProductCatagoryAttributeAppService")
            .EnableInterfaceInterceptorsAsync(typeof(IProductCatagoryAttributeRepository));

       

            builder.RegisterType<ProductAppService>().As<IProductAppService>().InstancePerDependency()
                .InterceptedBy(typeof(IocTranstationAopInterceptor))
                .EnableInterfaceInterceptorsAsync(typeof(IProductAppService));

            builder.RegisterType<SpecAppService>().As<ISpecAppService>().InstancePerDependency()
                .InterceptedBy(typeof(IocTranstationAopInterceptor))
                .EnableInterfaceInterceptorsAsync(typeof(ISpecRepository));


            
        }
    }
}
#endif