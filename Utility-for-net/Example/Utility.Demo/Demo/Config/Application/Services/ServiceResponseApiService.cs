//net40 - net48 netcoreapp2.0 - net5.0 netstandard2.0 - netstandard2.1
#if !(NET10 || NET11 || NET20 || NET30 || NET35 || NETCOREAPP1_0 || NETCOREAPP1_1 || NETCOREAPP1_2 || NETSTANDARD1_0 || NETSTANDARD1_1 || NETSTANDARD1_2 || NETSTANDARD1_3 || NETSTANDARD1_4 || NETSTANDARD1_5 || NETSTANDARD1_6 )

using Config.Domain.Entities;
using Utility.Application.Services;

namespace Config.Application.Services
{
    /// <summary>
    /// 
    /// </summary>
    public class ServiceResponseApiService : ResponseApiService<ServiceService, ServiceEntity, string>
    {
        //public ServiceResponseApiBLL(DAL.IConfigManager manager):base(new ServiceBLL(manager.ServiceDAL))
        //{
        //}

        public ServiceResponseApiService(ServiceService bLL) : base(bLL)
        {

        }
    }
}
#endif