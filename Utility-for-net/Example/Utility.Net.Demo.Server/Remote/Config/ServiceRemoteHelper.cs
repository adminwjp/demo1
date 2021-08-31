#if NET10 || NET11 || NET20 || NET30 || NET35 || NET40 || NET45 || NET451 || NET452 || NET46 || NET461 || NET462 || NET47 || NET471 || NET472 || NET48
// 模型 注解 >=40
#if NET40 || NET45 || NET451 || NET452 || NET46 || NET461 || NET462 || NET47 || NET471   || NET472 || NET48
using Utility.Remote;
using Config.Domain.Repositories;

namespace Config.Remote
{
    /// <summary>
    /// 
    /// </summary>
    public class ServiceRemoteHelper: RemoteServerManager
    {
        /// <summary>客户端有参造构造函数用到时 客户端无参构造函数用到(客户端不注入参数 服务器端自己手动调用) </summary>
        public static IConfigManager ConfigManager { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public static void RegisterServer()
        {
            //注意只能无参构造函数 传参数不支持怎么传 (特别麻烦 参数 结果 都需要序列化 引用的都需要 )
            //1.tcp：
            //第一种方式
            RemoteManager.RegisterActivatedServiceTcp<ServiceObject>(20001, "service");//http 不支持
            //第2种方式
            // RemoteManager.RegisterServiceTcp<ServiceObject>(20001, "service");
            //第3种方式 配置的
            // RemoteManager.RegisterServiceTcpConfig("Remote.Server.exe.config");

            RemoteManager.RegisterServiceTcp<ConfigObject>(20001, "config");

            //2.Http：
            //第一种方式
            RemoteManager.RegisterServiceHttp<ServiceObject>(20002, "service");
            //第2种方式 配置的
            //RemoteManager.RegisterServiceHttpConfig("Remote.Server.exe.config");
        }
        
    }
}
#endif
#endif