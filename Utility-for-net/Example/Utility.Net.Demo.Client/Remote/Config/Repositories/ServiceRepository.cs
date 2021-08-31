#if NET10 || NET11 || NET20 || NET30 || NET35 || NET40 || NET45 || NET451 || NET452 || NET46 || NET461 || NET462 || NET47 || NET471 || NET472 || NET48
// 模型 注解 >=40
#if NET40 || NET45 || NET451 || NET452 || NET46 || NET461 || NET462 || NET47 || NET471 || NET472 || NET48
using Config.Domain.Entities;
using Config.Remote.Entities;
using System;
using Utility.Domain.Repositories;
using Utility.Remote.Repositories;

namespace Config.Remote.Repositories
{
    /// <summary>Remote 服务 数据访问层接口实现  </summary>
    public    class ServiceRepository: RemoteRepository<IServiceObject, ServiceObjectResultEntity, ServiceObjectListEntity, ServiceEntity, ServiceObjectEntity, string>,
       IRepository<ServiceEntity, string>
    {
        const string TcpUrl = "TCP://localhost:20001/service";
        const string HttpUrl = "http://localhost:20002/service";

        /// <summary>
        /// 
        /// </summary>
        public ServiceRepository() :base(HttpUrl)
        {
        }
    }
}
#endif
#endif