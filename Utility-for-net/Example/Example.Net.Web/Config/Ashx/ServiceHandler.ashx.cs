using Config.Application.Services;
using Config.Domain.Entities;
using Utility.Application.Services;
using Utility.AspNet.Handlers;

namespace Example.Web.Config.Ashx
{
    /// <summary>
    /// ServiceHandler 的摘要说明
    /// </summary>
    public class ServiceHandler : BaseHandler<ResponseApiService<ServiceService, ServiceEntity, string>, ServiceService, ServiceEntity, string>
    {
        public ServiceHandler()
        {
            ApiService = MvcApplication.IocManager.Get<ResponseApiService<ServiceService, ServiceEntity, string>>();
        }
        
    }
}