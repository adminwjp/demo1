using Config.Application.Services;
using Config.Domain.Entities;
using Utility.Application.Services;
using Utility.AspNet.Handlers;

namespace Example.Web.Config.Ashx
{
    /// <summary>
    /// ConfigHandler 的摘要说明
    /// </summary>
    public class ConfigHandler :  BaseHandler<ResponseApiService<ConfigService, ConfigEntity, string>,ConfigService, ConfigEntity, string>
    {
        public ConfigHandler()
        {
            ApiService = MvcApplication.IocManager.Get<ResponseApiService<ConfigService, ConfigEntity, string>>();
        }
     
    }
}