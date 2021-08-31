//net40 - net48 netcoreapp2.0 - net5.0 netstandard2.0 - netstandard2.1
#if !(NET10 || NET11 || NET20 || NET30 || NET35 || NETCOREAPP1_0 || NETCOREAPP1_1 || NETCOREAPP1_2 || NETSTANDARD1_0 || NETSTANDARD1_1 || NETSTANDARD1_2 || NETSTANDARD1_3 || NETSTANDARD1_4 || NETSTANDARD1_5 || NETSTANDARD1_6 )
using Config.Domain.Entities;
using Utility.Application.Services;
using Utility.Cache;
using Utility.Domain.Repositories;

namespace Config.Application.Services
{

    /// <summary>这样写三层架构真麻烦 java dao service 层 3层架构 </summary>
    public  class ServiceService:CrudAppService<ServiceEntity, string>
    {
        /// <summary>
        /// 
        /// </summary>
        public ServiceService() : base(null)
        {

        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="serviceDAL"></param>
        public ServiceService(IRepository<ServiceEntity, string> serviceDAL) :base(serviceDAL)
        {

        }
        //注入构造函数 只能有一个 asp.net core 不然 报错  原理未知  没改过 想都支持 自己 去改 
        //public ServiceBLL(Config.DAL.IConfigManager configManager) : base(configManager.ServiceDAL)
        //{

        //}
   
    }
}
#endif
