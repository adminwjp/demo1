
using Config.Application.Services;
using Config.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Utility.Application.Services;
using Utility.AspNet.Aspxs;

namespace Example.Web.Config.Aspx
{
    public partial class Service : BaseAspx<ResponseApiService<ServiceService, ServiceEntity, string>, ServiceService, ServiceEntity, string>
    {
        protected override void Page_Load(object sender, EventArgs e)
        {
            ApiService = MvcApplication.IocManager.Get<ResponseApiService<ServiceService, ServiceEntity, string>>();
            base.Page_Load(sender, e);
        }
    }
}