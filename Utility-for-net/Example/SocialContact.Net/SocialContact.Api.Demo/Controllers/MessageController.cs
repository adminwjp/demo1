//------------------------------------------------------------------------------
// <copyright company="Tunynet">
//     Copyright (c) Tunynet Inc.  All rights reserved.
// </copyright>
//------------------------------------------------------------------------------

using Microsoft.AspNetCore.Mvc;
using Tunynet.Common;
using Tunynet.Settings;

namespace Tunynet.Spacebuilder
{
    /// <summary>
    /// 通知控制器
    /// </summary>
    public class MessageController : Controller
    {
        private PauseSiteSettings pauseSiteSetting;
        private SiteSettings siteSetting;


        #region

        /// <summary>
        /// 暂停站点
        /// </summary>
        /// <returns></returns>
        public ActionResult PausePage()
        {
            ViewData["siteSettings"] = siteSetting;
            if (pauseSiteSetting.PausePageType)
                return View(pauseSiteSetting);
            else
                return Redirect(pauseSiteSetting.PauseLink);
        }

        #endregion
    }
}