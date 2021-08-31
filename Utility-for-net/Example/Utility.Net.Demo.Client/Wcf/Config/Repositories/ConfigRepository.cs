#if NET45 || NET451 || NET452 || NET46 || NET461 || NET462 || NET47 || NET471 || NET472 || NET48
using Config.Domain.Entities;
using Config.Wcf;
using System;
using System.Threading.Tasks;
using Utility.Domain.Repositories;
using Utility.Wcf.Repositories;

namespace Config.Wcf.Repositories
{
    /// <summary>WCF 配置 数据访问层接口实现  </summary>
    public    class ConfigRepository : WcfRepository<ConfigServiceClient, IConfigService, ConfigEntity, string>,
        IRepository<ConfigEntity, string>
    {
        const string NamePipe = "NetNamedPipeBinding_IConfigService";
        const string HttpBind = "BasicHttpBinding_IConfigService";
        const string NetTcpBinding = "NetTcpBinding_IConfigService";

        /// <summary>
        /// 
        /// </summary>
        public ConfigRepository() 
        {
            this.Name= NetTcpBinding;
            // ClientProxy = new ConfigServiceClient(Name);
        }
    }
}
#endif