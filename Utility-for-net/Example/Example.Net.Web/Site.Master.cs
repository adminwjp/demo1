using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Example.Web
{
    public partial class Site : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
       public void loginInfo()
        {
            Response.Write("<a href=\"Logout.aspx\">[退出]</a>&nbsp;&nbsp;|");
            Response.Write("<a href=\"Register.aspx\">[注册]</a>");
            Response.Write("</li>");
        }
    }
}