#if NET45 || NET451 || NET452 || NET46 || NET461 || NET462 || NET47 || NET471 || NET472 || NET48 || NETCOREAPP2_0 || NETCOREAPP2_1 || NETCOREAPP2_2 || NETCOREAPP3_0 || NETCOREAPP3_1  || NET5_0 ||  NET6_0  || NETSTANDARD2_0 || NETSTANDARD2_1
using System.Reflection;
using Autofac;
using MediatR;
using Utility.Application.Behaviors;

namespace Utility.Infrastructure.AutofacModules
{
    /// <summary>
    /// media 集成事件 模块
    /// </summary>
    public class MediatorModule : Autofac.Module
    {
        /// <summary>
        /// 默认 注入 日志事件 中间件
        /// 验证事件 中间件
        /// 事务事件 中间件
        /// </summary>
        /// <param name="builder"></param>
        protected override void Load(ContainerBuilder builder)
        {
            //处理下 事务优先级 事务 共享  无论 media 实现 还是 自定义实现
            //IMediator 
            //Transient 不能使用 ioc 自动 获取新的 还是 原有旧的 对象？
            //transtion 冲突 如何 解决 IMediator 自带事务 
            builder.RegisterAssemblyTypes(typeof(IMediator).GetTypeInfo().Assembly)
                .AsImplementedInterfaces();

            DefaultLoad(builder);


            builder.Register<ServiceFactory>(context =>
            {
                var componentContext = context.Resolve<IComponentContext>();
                return t => { object o; return componentContext.TryResolve(t, out o) ? o : null; };
            });

            builder.RegisterGeneric(typeof(LoggingBehavior<,>)).As(typeof(IPipelineBehavior<,>));
            builder.RegisterGeneric(typeof(ValidatorBehavior<,>)).As(typeof(IPipelineBehavior<,>));
            builder.RegisterGeneric(typeof(TransactionBehaviour<,,,>)).As(typeof(IPipelineBehavior<,>));

        }

        /// <summary>
        /// 默认 注入 无任何注入
        /// </summary>
        /// <param name="builder"></param>
        protected virtual void DefaultLoad(ContainerBuilder builder)
        {
          
        }
    }
}
#endif