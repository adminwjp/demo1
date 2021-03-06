//------------------------------------------------------------------------------
// <copyright company="Tunynet">
//     Copyright (c) Tunynet Inc.  All rights reserved.
// </copyright>
//------------------------------------------------------------------------------

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using NHibernate.Mapping.Attributes;
using System;
using System.Collections.Generic;
using System.Web;
using Tunynet.Settings;

namespace Tunynet.Common
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, Inherited = true, AllowMultiple = true)]
    public class PauseSiteCheckAttribute : ActionFilterAttribute, IAuthorizationFilter
    {
        public void OnAuthorization(AuthorizationFilterContext filterContext)
        {
            if (filterContext == null)
            {
                throw new ArgumentNullException("filterContext");
            }
                return;

            PauseSiteSettings pauseSiteSettings = DIContainer.Resolve<ISettingsManager<PauseSiteSettings>>().Get();

            HttpContext context = filterContext.HttpContext;
            if (!pauseSiteSettings.IsEnable)
            {
                var routeDataDictionary = filterContext.RouteData.Values;
                if (!routeDataDictionary.ContainsKey("Controller"))
                    return;
                string controllerName = routeDataDictionary["Controller"].ToString();
                if (!controllerName.ToLower().Contains("controlpanel") && !controllerName.ToLower().Contains("message"))
                {
                    if (filterContext.ActionDescriptor.DisplayName.Equals("PausePage", StringComparison.CurrentCultureIgnoreCase))
                    {
                        return;
                    }
                    else
                    {
                        Dictionary<string, string> buttonLink = new Dictionary<string, string>();
                        //filterContext.TempData["SystemMessageViewModel"] = new SystemMessageViewModel
                        //{
                        //    Body = pauseSiteSettings.PauseAnnouncement,
                        //    ReturnUrl = SiteUrls.Instance().Home(),
                        //    Title = "暂停站点",
                        //    StatusMessageType = StatusMessageType.Error,
                        //    ButtonLink = buttonLink
                        //};
                        filterContext.Result = new RedirectResult(SiteUrls.Instance().PausePage());
                    }
                    return;
                }
            }
        }
    }
}