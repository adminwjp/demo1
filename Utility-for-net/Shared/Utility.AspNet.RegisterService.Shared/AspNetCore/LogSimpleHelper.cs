#if NETCOREAPP2_0 || NETCOREAPP2_1 || NETCOREAPP2_2 || NETCOREAPP3_0 || NETCOREAPP3_1 || NET5_0 || NET6_0 || NETSTANDARD2_0 || NETSTANDARD2_1
using Microsoft.Extensions.Configuration;
using System;
using Winton.Extensions.Configuration.Consul;

namespace Utility.AspNetCore
{
    /// <summary>
    /// asp.net core 日志 帮助类
    /// </summary>
    public class LogSimpleHelper
    {
        /// <summary>
        /// 日志写入方式
        /// </summary>
        public static LogFlag Flag = LogFlag.File;
        /// <summary>
        /// 动态配置服务地址 硬代码编辑麻烦
        /// </summary>
       // public static string Address { get; set; }
        /// <summary>
        /// 初始化 日志配置
        /// </summary>
		/// <param name="args">设置 地址  dotnet *.dll  --urls "http://*:6001;"</param>
        /// <returns></returns>
        public static IConfiguration Initial(string[] args)
        {
            IConfigurationBuilder configurationBuilder = Builder(args,true);
            return LogConfig!=null?LogConfig(configurationBuilder.Build()): configurationBuilder.Build();
        }

        /// <summary>
        /// 默认 初始化 配置文件
        /// </summary>
		/// <param name="args">设置 地址  dotnet *.dll  --urls "http://*:6001;"</param>
        /// <param name="dynamic"></param>
        /// <returns></returns>
        public static IConfigurationBuilder Builder(string[] args,bool dynamic=false)
        {
            string text = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
            if (string.IsNullOrEmpty(text))
            {
                text = "Development";
            }
            Console.WriteLine(text);//Development
            IConfigurationBuilder configurationBuilder = new ConfigurationBuilder()
				.AddCommandLine(args)
                .SetBasePath(Environment.CurrentDirectory)
                .AddJsonFile("appsettings." + text + ".json", optional: false, reloadOnChange: true)
                .AddEnvironmentVariables();
            //加载动态配置 则设置端口无效
            //if (dynamic)
            //{
            //    configurationBuilder = DynamicConfig(configurationBuilder, ServiceConfig.Flag,StartHelper.Key,Address);
            //}
            return configurationBuilder;
        }
        /// <summary>
        /// 动态 配置
        /// </summary>
        /// <param name="configurationBuilder"></param>
        /// <param name="flag"></param>
        /// <param name="address"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public static IConfigurationBuilder DynamicConfig(IConfigurationBuilder configurationBuilder,ServiceFlag flag,string key,string address)
        {
            switch (flag)
            {
                case ServiceFlag.None:
                    break;
                case ServiceFlag.Eureka:
                    break;
                case ServiceFlag.Consul:
                    configurationBuilder = configurationBuilder.AddConsul(key,
                                 options =>
                                 {
                                     options.Optional = true;
                                     options.ReloadOnChange = true;
                                     options.OnLoadException = exceptionContext => { exceptionContext.Ignore = true; };
                                     options.ConsulConfigurationOptions = cco => { cco.Address = new Uri(address); };

                                 }
                             );
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
            return configurationBuilder;
        }

        /// <summary>
        /// 日志 配置
        /// </summary>
        /// <returns></returns>
       public static Func<IConfiguration, IConfiguration> LogConfig { get; set; }
    }

    /// <summary>
    /// 日志写入方式
    /// </summary>
    [Flags]
    public enum LogFlag
    {
        /// <summary>
        /// 文件
        /// </summary>
        File,
        /// <summary>
        /// ElaticSearch
        /// </summary>
        Es
    }
}
#endif