#if  NET45 || NET451 || NET452 || NET46 || NET461 || NET462 || NET47 || NET471 || NET472 || NET48
using System;
using Utility.Wcf;

namespace Config.Wcf
{
    /// <summary>wcf 客户端通道  svcutil.exe 生成的有 通信 强制转换 关闭 实现未知</summary>
    public interface IServiceApiServiceChannel : IServiceApiService, IServiceApiChannel<Config.Domain.Entities.ServiceEntity, string>,
        System.ServiceModel.IClientChannel
    {
    }

}
#endif
