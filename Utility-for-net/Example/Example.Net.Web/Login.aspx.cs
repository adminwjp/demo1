using JWT;
using JWT.Algorithms;
using JWT.Serializers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Utility.Logs;

namespace Example.Web
{
    public partial class Login : System.Web.UI.Page
    {
        static readonly ILog log=new DefaultLog<Login>();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {
                log.Log(LogLevel.Info, "Login IsPostBack event trigger");
            }
        }

        protected void login1_Authenticate(object sender, AuthenticateEventArgs e)
        {
            log.Log(LogLevel.Info, "login Authenticate event trigger,Authenticated->" + e.Authenticated);
        }

        protected void login1_LoggedIn(object sender, EventArgs e)
        {
            log.Log(LogLevel.Info, "login LoggedIn event trigger" );
        }

        protected void login1_LoggingIn(object sender, LoginCancelEventArgs e)
        {
            IJwtAlgorithm algorithm = new HMACSHA256Algorithm();
            IJsonSerializer serializer = new JsonNetSerializer();
            log.Log(LogLevel.Info, "login LoggingIn event trigger");
        }

        protected void login1_LoginError(object sender, EventArgs e)
        {
            log.Log(LogLevel.Info, "login LoginError event trigger");
        }

        protected void loginStatus_LoggingOut(object sender, LoginCancelEventArgs e)
        {
            log.Log(LogLevel.Info, "loginStatus LoggingOut event trigger,Cancel->" + e.Cancel);
        }

        protected void loginStatus_LoggedOut(object sender, EventArgs e)
        {
            log.Log(LogLevel.Info, "loginStatus LoggingOut event trigger");
        }
    }
}