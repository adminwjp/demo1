#if NET10 || NET11 || NET20 || NET30 || NET35 || NET40 || NET45 || NET451 || NET452 || NET46 || NET461 || NET462 || NET47 || NET471 || NET472  || NET48
// 模型 注解 >=40

using System;
using RemoteObject = Utility.Remote.Object<Utility.Domain.Repositories.IRepository<Config.Domain.Entities.ServiceEntity, string>,Config.Remote.Entities.ServiceObjectResultEntity, Config.Remote.Entities.ServiceObjectListEntity,
    Config.Remote.Entities.ServiceObjectEntity, Config.Domain.Entities.ServiceEntity, string>;

namespace Config.Remote
{

    /// <summary>
    /// 服务信息  Remote 实现  
    /// 类型“System.Threading.CancellationToken”未标记为可序列化。 不能用
    /// Soap 序列化程序不支持序列化一般类型: System.Collections.Generic.List
    /// int? 
    /// </summary>
    public class ServiceObject: RemoteObject,IServiceObject
    {
        /// <summary>
        /// 
        /// </summary>
        public ServiceObject()
        {
            //注意只能无参构造函数 传参数不支持怎么传 支持注册类型 这玩意服务器端怎么绑定了
            //手动调用
            Repository = ServiceRemoteHelper.ConfigManager.Service;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dAL"></param>
        public ServiceObject(Utility.Domain.Repositories.IRepository<Config.Domain.Entities.ServiceEntity, string> dAL)
        {
            Repository = dAL;
        }

     
       
    }
}
#endif
