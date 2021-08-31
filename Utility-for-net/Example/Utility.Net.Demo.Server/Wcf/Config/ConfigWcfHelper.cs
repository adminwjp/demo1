#if NET45 || NET451 || NET452 || NET46 || NET461 || NET462 || NET47 || NET471 || NET472 || NET48

using Config.Domain.Repositories;
using System.Collections.Generic;
using System.ServiceModel;
using Utility.Wcf;

namespace Config.Wcf
{
    /// <summary>
    /// 
    /// </summary>
    public class ConfigWcfHelper:ServerHelper
    {
        class Inner
        {
            public static readonly ConfigWcfHelper Instance = new ConfigWcfHelper();
        }
        /// <summary>
        /// 
        /// </summary>
        public static ConfigWcfHelper Instance
        {
            get {
                return Inner.Instance;
            }
        }
        /// <summary>服务器端无参构造函数用到(手动调用) </summary>
        public static IConfigManager ConfigManager { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public override void RegisterServer()
        {

            //契约实现不能有task 不然一直加载 
            // 有参构造函数 方案
            //这种方式比较麻烦 多余的配置也要注释掉 不然改配置无效
            //< add baseAddress = "net.pipe://localhost/service" />
            //< add baseAddress = "http://localhost:8733/service" />
            // < add baseAddress = "tcp://localhost:8733/service" />
            //这种方式必须指定地址 还需配置以下
            //< serviceHostingEnvironment multipleSiteBindingsEnabled = "true" ></ serviceHostingEnvironment >
            //实现契约 必须注解     [System.ServiceModel.ServiceBehavior(InstanceContextMode = System.ServiceModel.InstanceContextMode.Single)]
            //ServiceHosts.Add(new ServiceHost(new ServiceService(ServiceManager), new System.Uri[] {
            //       new System.Uri("net.pipe://localhost/service"),
            //       new System.Uri("http://localhost:8733/service"),
            //       new System.Uri("net.tcp://localhost:8733/service")
            //   }));

            // 无参构造函数 方案 [System.ServiceModel.ServiceBehavior(InstanceContextMode = System.ServiceModel.InstanceContextMode.Single)] 报错 注释掉 上面是硬代码有参配置的
            ServiceHosts.Add(new ServiceHost(typeof(ServiceService)));
            ServiceHosts.Add(new ServiceHost(typeof(ConfigService)));
            base.RegisterServer();
        }
    }
}
#endif