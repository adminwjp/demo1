using Config.Application.Services;
using Config.Domain.Entities;
using System;
using Utility.Application.Services;
using Utility.AspNet.Aspxs;

namespace Example.Web.Config.Aspx
{
    public partial class Config : BaseAspx<ResponseApiService<ConfigService, ConfigEntity, string>, ConfigService, ConfigEntity, string>
    {
        protected override void Page_Load(object sender, EventArgs e)
        {
            ApiService = MvcApplication.IocManager.Get<ResponseApiService<ConfigService, ConfigEntity, string>>();
            base.Page_Load(sender, e);
        }
    }
}