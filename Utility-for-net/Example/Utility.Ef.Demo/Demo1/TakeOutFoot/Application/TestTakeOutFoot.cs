#if NET40 || NET45 || NET451 || NET452 || NET46 || NET461 || NET462 || NET47 || NET471 || NET472 || NET48 || NETCOREAPP2_0 || NETCOREAPP2_1 || NETCOREAPP2_2 || NETCOREAPP3_0 || NETCOREAPP3_1 || NET5_0 || NETSTANDARD2_0 || NETSTANDARD2_1
using Autofac;
using Autofac.Annotation;
using MediatR;
using System.IO;
using System.Reflection;
using System.Text;

namespace TakeOutFoot.Application
{
    public class FileTextWriter : TextWriter
    {
        public override Encoding Encoding => Encoding.UTF8;
        public override void Write(string value)
        {
            base.Write(value);
        }
        public override void WriteLine(string value)
        {
            base.WriteLine(value);
        }
    }
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

            //builder.RegisterAssemblyTypes(typeof(BaseIntegrationEventHandler<,,>).GetTypeInfo().Assembly)
            //    .AsClosedTypesOf(typeof(IIntegrationEventHandler<>));
            // 注册autofac打标签模式
           // builder.RegisterTypes(GetType().Assembly.GetExportedTypes()).PropertiesAutowired();
            //如果需要开启支持循环注入
           // builder.RegisterModule(new AutofacAnnotationModule(GetType().Assembly).SetAllowCircularDependencies(false));
            builder.RegisterBuildCallback(container =>
            {
                Utility.Ioc.AutofacIocManager.Instance.Container = (IContainer)container;
            });
        }
    }


    public class MediatorModule : Utility.Infrastructure.AutofacModules.MediatorModule
    {
        protected override void DefaultLoad(ContainerBuilder builder)
        {
            // Register all the Command classes (they implement IRequestHandler) in assembly holding the Commands
            builder.RegisterAssemblyTypes(typeof(MediatorModule).GetTypeInfo().Assembly)
                .AsClosedTypesOf(typeof(IRequestHandler<,>));

            // Register the DomainEventHandler classes (they implement INotificationHandler<>) in assembly holding the Domain Events
            builder.RegisterAssemblyTypes(typeof(MediatorModule).GetTypeInfo().Assembly)
                .AsClosedTypesOf(typeof(INotificationHandler<>));
            base.DefaultLoad(builder);
        }
    }

    public class InfrastructureModule
      : Autofac.Module
    {

        protected override void Load(ContainerBuilder builder)
        {
            // 注册autofac打标签模式
            builder.RegisterTypes(GetType().Assembly.GetExportedTypes()).PropertiesAutowired();
            //如果需要开启支持循环注入
            builder.RegisterModule(new AutofacAnnotationModule(GetType().Assembly).SetAllowCircularDependencies(false));
           
        }
    }
   
}
#endif