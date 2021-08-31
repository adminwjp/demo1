using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Utility;
using Utility.AspNetCore.Controllers;
using Utility.Cache;
using Utility.Json.Extensions;
using Microsoft.Extensions.DependencyInjection;
using System.Text;

namespace SocialContact.Filter
{
    public class ActionAuthFilter :IAuthorizationFilter//, IActionFilter
    {
       
        public void OnActionExecuted(ActionExecutedContext context)
        {
           
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
           
        }

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            //var router = context.RouteData.Values;
            var path = context.HttpContext.Request.Path.Value?.ToLower();
            //var contr = router["controller"]?.ToString()?.ToLower();
            if (string.IsNullOrEmpty(path)
                || !path.Contains("social_contact"))
            {
                return;
            }
            if (!path.Contains("login")
                || !path.Contains("register")
                || !path.Contains("logout"))
            {
                return;
            }
            //object controllerBase = context.Controller;
           // var controller = controllerBase as BaseController;
           // if (controller != null)
            {
                var token = context.HttpContext.Request.Query["token"];
                if (string.IsNullOrEmpty(token))
                {
                    token = context.HttpContext.Request.Headers["token"];
                }
                if (string.IsNullOrEmpty(token))
                {
                    token = context.HttpContext.Request.Cookies["token"];
                }
                if (string.IsNullOrEmpty(token))
                {
                    if (context.HttpContext.Session.TryGetValue("token", out byte[] buffer))
                    {
                        token = Encoding.UTF8.GetString(buffer);
                    }
                }
                var cache = context.HttpContext.RequestServices.GetService<ICacheContent>();
                // var cacheToken = controller.Cache.Get<string>("token");
                var cacheToken = cache.Get<string>("token"); 
                if (string.IsNullOrEmpty(token) ||
                    string.IsNullOrEmpty(cacheToken) || cacheToken != token)
                {
                    context.Result = new JsonResult(ResponseApi.CreateError().SetMessage("token error"));

                }
            }
        }
    }
}
