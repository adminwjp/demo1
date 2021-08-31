#if  NET45 || NET451 || NET452 || NET46 || NET461 || NET462 || NET47 || NET471 || NET472 || NET48
using System;
using Utility.Wcf;

namespace Config.Wcf
{
    /// <summary>
    /// wcf 服务信息 契约接口(前提必须要有System.ServiceModel.ServiceContract注解),注意方法名不能相同 不然 提示签名有多个这样的错误(要么在注解上改名称) 
    ///  wcf 不支持 System.Threading.CancellationToken task 一直假死 不调可以
    /// </summary>
    [System.ServiceModel.ServiceContract]//wcf 契约标识 接口上必须有 否则wcf不支持
    public interface IServiceApiService : IServiceApi<Config.Domain.Entities.ServiceEntity, string>
    {  
        
    }

}
#endif