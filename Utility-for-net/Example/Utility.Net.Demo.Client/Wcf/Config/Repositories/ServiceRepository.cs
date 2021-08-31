#if NET45 || NET451 || NET452 || NET46 || NET461 || NET462 || NET47 || NET471 || NET472 || NET48
using Config.Domain.Entities;
using Config.Wcf;
using System;
using System.Threading.Tasks;
using Utility.Domain.Repositories;
using Utility.Wcf.Repositories;

namespace Config.Wcf.Repositories
{
    /// <summary>WCF 服务 数据访问层接口实现  </summary>
    public    class ServiceRepository: WcfRepository<ServiceServiceClient, IServiceService, ServiceEntity, string>,
        IRepository<ServiceEntity, string>
    {
        const string NamePipe = "NetNamedPipeBinding_IServiceService";
        const string HttpBind = "BasicHttpBinding_IServiceService";
        const string NetTcpBinding = "NetTcpBinding_IServiceService";
        /// <summary>
        /// 
        /// </summary>
        public ServiceRepository() 
        {
            this.Name= NetTcpBinding;
           // ClientProxy = new ServiceServiceClient(Name);
        }
    }
}
#endif