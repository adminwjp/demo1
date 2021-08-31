#if NETCOREAPP2_0 || NETCOREAPP2_1 || NETCOREAPP2_2 || NETCOREAPP3_0 || NETCOREAPP3_1 || NET5_0 || NET6_0 || NETSTANDARD2_0 || NETSTANDARD2_1
using Microsoft.Extensions.DependencyInjection;
using Steeltoe.Discovery.Client;
using Microsoft.AspNetCore.Builder;
using System;
#if NETCOREAPP3_0 || NETCOREAPP3_1 || NET5_0 || NET6_0 || NETSTANDARD2_0 || NETSTANDARD2_1
#endif
using Microsoft.Extensions.Configuration;

namespace Utility.AspNetCore.Extensions
{
    /// <summary>
    /// ServiceCollection 扩展类
    /// </summary>
    public static class RegisterServiceExtensions
    {

        /// <summary>
        /// 添加 注册服务
        /// Steeltoe 支持 eureka(优先级高) consul 默认 只给 eureka 配置 要么 2 选 1
        /// consul 单独实现
        /// </summary>
        /// <param name="services"></param>
        /// <param name="configuration"></param>
        /// <param name="flag"></param>
        public static void AddRegisterService(this IServiceCollection services,IConfiguration configuration,ServiceFlag flag)
        {
            switch (flag)
            {
                case ServiceFlag.None:
                    break;
                case ServiceFlag.Eureka:
                    services.AddDiscoveryClient(configuration);//注册微服务 Eureka or  consul 优先级 eureka 高 没有配置则不启动 

                    break;
                case ServiceFlag.Consul:
                    services.Configure<Utility.Consul.ConsulEntity>(configuration.GetSection("Consul"));
                    break;
                case ServiceFlag.Zookeeper:
                    break;
                case ServiceFlag.ServiceFabric:
                    break;
                case ServiceFlag.Redis:
                    break;
                case ServiceFlag.Rabbitmq:
                    break;
                case ServiceFlag.Kubernetes:
                    break;
                default:
                    break;
            }
        }


        /// <summary>
        /// 添加 注册服务 生命周期
        /// Steeltoe 支持 eureka(优先级高) consul 默认 只给 eureka 配置 要么 2 选 1
        /// consul 单独实现
        /// </summary>
        /// <param name="app"></param>
        /// <param name="configuration"></param>
        /// <param name="flag"></param>
        public static void UseService(this IApplicationBuilder app, IConfiguration configuration, ServiceFlag flag)
        {
            switch (flag)
            {
                case ServiceFlag.None:
                    break;
                case ServiceFlag.Eureka:
                    app.UseDiscoveryClient();//注册微服务 Eureka or  consul 优先级 eureka 高 没有配置则不启动 
                    break;
                case ServiceFlag.Consul:
                    app.UseConsul(configuration);
                    break;
                case ServiceFlag.Zookeeper:
                    break;
                case ServiceFlag.ServiceFabric:
                    break;
                case ServiceFlag.Redis:
                    break;
                case ServiceFlag.Rabbitmq:
                    break;
                case ServiceFlag.Kubernetes:
                    break;
                default:
                    break;
            }
        }


        /// <summary>
        /// 创建 ApplicationBuilder
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
#if NETCOREAPP3_0 || NETCOREAPP3_1 || NET5_0
        public static IApplicationBuilder Create(this IServiceCollection services)
        {
            IServiceProvider GetProviderFromFactory(IServiceCollection collection)
            {
                var provider = collection.BuildServiceProvider();
                var factory = provider.GetService<IServiceProviderFactory<IServiceCollection>>();

                if (factory != null && !(factory is DefaultServiceProviderFactory))
                {
                    using (provider)
                    {
                        return factory.CreateServiceProvider(factory.CreateBuilder(collection));
                    }
                }

                return provider;
            }
            IApplicationBuilder builder = new ApplicationBuilder(GetProviderFromFactory(services));
            return builder;
        }
#endif

    }
}
#endif