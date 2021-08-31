#if NET45 || NET451 || NET452 || NET46 || NET461 || NET462 || NET47 || NET471 || NET472 || NET48
using Config.Domain.Entities;
using System;
using Utility.Domain.Repositories;

namespace Config.Wcf
{
    /// <summary>WCF 服务信息 契约接口实现  </summary>
    //[System.ServiceModel.ServiceBehavior(InstanceContextMode = System.ServiceModel.InstanceContextMode.Single)]
    public    class ServiceService: Utility.Wcf.Service<IRepository<ServiceEntity, string>, ServiceEntity, string>,IServiceService, Utility.Wcf.IService<ServiceEntity, string>
    {
        /// <summary>
        /// 
        /// </summary>
        public ServiceService()
        {
            //依赖注入 手动调用 
            Repository = ConfigWcfHelper.ConfigManager.Service;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dAL"></param>
        public ServiceService(IRepository<ServiceEntity, string> dAL)
        {
            Repository = dAL;
        }
    }
}
#endif
