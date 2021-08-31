#if NET10 || NET11 || NET20 || NET30 || NET35 || NET40 || NET45 || NET451 || NET452 || NET46 || NET461 || NET462 || NET47 || NET471 || NET472 || NET48
// 模型 注解 >=40
#if NET40 || NET45 || NET451 || NET452 || NET46 || NET461 || NET462 || NET47 || NET471 || NET472  || NET48

using Config.Remote;
using Config.Remote.Entities;
using System;
using Utility.Config;
using Utility.Domain.Repositories;
using Utility.Remote.Repositories;
using Config.Domain.Entities;

namespace Config.Remote.Repositories
{
    /// <summary>Remote 服务 数据访问层接口实现  </summary>
    public    class ConfigRepository : RemoteRepository<IConfigObject, ConfigObjectResultEntity, ConfigObjectListEntity, Config.Domain.Entities.ConfigEntity, ConfigObjectEntity, string>,
       IRepository<Config.Domain.Entities.ConfigEntity, string>
    {
        const string TcpUrl = "TCP://localhost:20001/config";
        const string HttpUrl = "http://localhost:20002/config";

        /// <summary>
        /// 
        /// </summary>
        public ConfigRepository() :base(HttpUrl)
        {
        }
    }
}
#endif
#endif