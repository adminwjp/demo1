#if  NET45 || NET451 || NET452 || NET46 || NET461 || NET462 || NET47 || NET471 || NET472 || NET48
using Config.Domain.Entities;
using System;
using Utility.Wcf;

namespace Config.Wcf
{
    /// <summary>WCF 服务信息 契约接口实现  </summary>
    //[System.ServiceModel.ServiceBehavior(InstanceContextMode = System.ServiceModel.InstanceContextMode.Single)]
    public class ServiceApiServiceClient : ServiceApiClient<IServiceApiService, ServiceEntity,  string>, IServiceApiServiceClient,
        IServiceApiClient<ServiceEntity, string>
    {

        /// <summary>
        /// 
        /// </summary>
        public ServiceApiServiceClient()
        {

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="endpointConfigurationName"></param>
        public ServiceApiServiceClient(string endpointConfigurationName) : base(endpointConfigurationName)
        {

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="endpointConfigurationName"></param>
        /// <param name="remoteAddress"></param>
        public ServiceApiServiceClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : base(endpointConfigurationName, remoteAddress)
        {


        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="endpointConfigurationName"></param>
        /// <param name="remoteAddress"></param>

        public ServiceApiServiceClient(string endpointConfigurationName, string remoteAddress) : base(endpointConfigurationName, remoteAddress)
        {

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="binding"></param>
        /// <param name="remoteAddress"></param>
        public ServiceApiServiceClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : base(binding, remoteAddress)
        {

        }
    }
}
#endif
