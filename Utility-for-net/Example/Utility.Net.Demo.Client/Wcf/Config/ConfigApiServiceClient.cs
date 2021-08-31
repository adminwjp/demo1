#if  NET45 || NET451 || NET452 || NET46 || NET461 || NET462 || NET47 || NET471 || NET472 || NET48
using Config.Domain.Entities;
using System;
using Utility.Wcf;

namespace Config.Wcf
{
    /// <summary>WCF 配置信息 契约接口实现  </summary>
    //[System.ServiceModel.ServiceBehavior(InstanceContextMode = System.ServiceModel.InstanceContextMode.Single)]
    public class ConfigApiServiceClient : ServiceApiClient<IConfigApiService, ConfigEntity,  string>, IConfigApiServiceClient,
        IServiceApiClient<ConfigEntity, string>
    {

        /// <summary>
        /// 
        /// </summary>
        public ConfigApiServiceClient()
        {

        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="endpointConfigurationName"></param>
        public ConfigApiServiceClient(string endpointConfigurationName) : base(endpointConfigurationName)
        {

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="endpointConfigurationName"></param>
        /// <param name="remoteAddress"></param>
        public ConfigApiServiceClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : base(endpointConfigurationName, remoteAddress)
        {

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="endpointConfigurationName"></param>
        /// <param name="remoteAddress"></param>
        public ConfigApiServiceClient(string endpointConfigurationName, string remoteAddress) : base(endpointConfigurationName, remoteAddress)
        {

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="binding"></param>
        /// <param name="remoteAddress"></param>
        public ConfigApiServiceClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : base(binding, remoteAddress)
        {

        }
    }
}
#endif
