//------------------------------------------------------------------------------
// <copyright company="Tunynet">
//     Copyright (c) Tunynet Inc.  All rights reserved.
// </copyright>
//------------------------------------------------------------------------------

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Routing;
using System;
using System.Web;
//using Microsoft.AspNetCore.Mvc;
//using System.Web.Routing;
using Tunynet.Settings;

namespace Tunynet.Common
{
    /// <summary>
    /// 用户身份验证
    /// </summary>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, Inherited = true, AllowMultiple = true)]
    public class UserAuthorizeAttribute : ActionFilterAttribute
    {
        #region IAuthorizationFilter 成员

        //是否匿名过滤
        public bool IsAllowAnonymous { get; set; }

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var user = UserContext.CurrentUser;

            //匿名访问过滤
            if (IsAllowAnonymous)
            {
               
                SiteSettings siteSettings = DIContainer.Resolve<SettingManager<SiteSettings>>().Get();
                if (!siteSettings.EnableAnonymousBrowse && user == null)
                {
                    //if (filterContext.HttpContext.Request.IsAjaxRequest())
                    //    filterContext.Result = new EmptyResult();
                   // else
                        filterContext.Result = new RedirectResult(SiteUrls.Instance().Login(filterContext.HttpContext, HttpUtility.UrlEncode(filterContext.HttpContext.Request.Path.Value)));
                }
            }
            else
            {
                if (user == null)
                {
                    IAuthenticationService authenticationService = DIContainer.ResolvePerHttpRequest<FormsAuthenticationService>();
                    authenticationService.SignOut();
                    filterContext.Result = new RedirectResult(SiteUrls.Instance().Login(filterContext.HttpContext,HttpUtility.UrlEncode(filterContext.HttpContext.Request.Path.Value)));
                }
                else
                {
                }
            }
        }

        #endregion IAuthorizationFilter 成员

        /// <summary>
        /// 从路由数据获取AreaName
        /// </summary>
        /// <param name="routeData"></param>
        /// <returns></returns>
        private string GetAreaName(RouteData routeData)
        {
            object area;
            if (routeData.DataTokens.TryGetValue("area", out area))
            {
                return area as string;
            }

            return GetAreaName(routeData);
        }

        /// <summary>
        /// 从路由数据获取AreaName
        /// </summary>
        /// <param name="route"><see cref="RouteBase"/></param>
        /// <returns>返回路由中的AreaName，如果无AreaName则返回null</returns>
        private string GetAreaName(RouteBase route)
        {
            //IRouteWithArea routeWithArea = route as IRouteWithArea;
            //if (routeWithArea != null)
            //    return routeWithArea.Area;

            Route castRoute = route as Route;
            if (castRoute != null && castRoute.DataTokens != null)
                return castRoute.DataTokens["area"] as string;

            return null;
        }
    }
}