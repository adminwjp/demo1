#if  NET45 || NET451 || NET452 || NET46 || NET461 || NET462 || NET47 || NET471 || NET472 || NET48
using Config.Domain.Entities;
using System;
using Utility.Domain.Repositories;
using Utility.Wcf;

namespace Config.Wcf
{
    /// <summary>WCF 配置信息 契约接口实现  </summary>
    //[System.ServiceModel.ServiceBehavior(InstanceContextMode = System.ServiceModel.InstanceContextMode.Single)]
    public class ConfigApiService:ServiceApi<IRepository<ConfigEntity, string>, ConfigEntity, string>,IConfigApiService, 
        IServiceApi<ConfigEntity, string>
    {
        /// <summary>
        /// 
        /// </summary>
        public ConfigApiService()
        {
            //依赖注入 手动调用 
            Repository  = ConfigWcfHelper.ConfigManager.Config;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="dAL"></param>
        public ConfigApiService(IRepository<ConfigEntity, string> dAL)
        {
            Repository = dAL;
        }
    }
}
#endif
