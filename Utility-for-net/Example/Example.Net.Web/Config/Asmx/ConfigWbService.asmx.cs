using Config.Application.Services;
using Config.Domain.Entities;
using System.Web.Services;
using Utility.Application.Services;
using Utility.AspNet.WebServices;

namespace Example.Web.Config.Asmx
{
    /// <summary>
    /// ConfigService 的摘要说明
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // 若要允许使用 ASP.NET AJAX 从脚本中调用此 Web 服务，请取消注释以下行。 
    [System.Web.Script.Services.ScriptService]
    public class ConfigWbService : BaseWebService<ResponseApiService<ConfigService, ConfigEntity, string>, ConfigService, ConfigEntity, string>
    {
        public ConfigWbService()
        {
            ApiService = MvcApplication.IocManager.Get<ResponseApiService<ConfigService, ConfigEntity, string>>();
        }
    }
}
