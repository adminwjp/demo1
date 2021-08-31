//------------------------------------------------------------------------------
// <copyright company="Tunynet">
//     Copyright (c) Tunynet Inc.  All rights reserved.
// </copyright>
//------------------------------------------------------------------------------

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.Diagnostics;
using System.Text.Encodings.Web;

namespace Tunynet.Common
{
    /// <summary>
    /// 自定义的视图引擎，支持前台多个Theme和独立的后台（Console）
    /// </summary>
    public class TunynetViewEngine : RazorViewEngine
    {

        public TunynetViewEngine(IRazorPageFactoryProvider pageFactory, IRazorPageActivator pageActivator, HtmlEncoder htmlEncoder, IOptions<RazorViewEngineOptions> optionsAccessor, ILoggerFactory loggerFactory, DiagnosticListener diagnosticListener)
            : base(pageFactory,pageActivator,htmlEncoder,optionsAccessor,loggerFactory,diagnosticListener)
        {
            string theme = System.Configuration.ConfigurationManager.AppSettings["theme"];
            if (string.IsNullOrEmpty(theme))
                theme = "Default";

            //ViewLocationFormats = new string[]
            //{
            //    "~/Themes/" + theme + "/{1}/{0}.cshtml",
            //    "~/Themes/" + theme + "/Shared/{0}.cshtml"
            //};

            //MasterLocationFormats = new string[]
            //{
            //    "~/Themes/" + theme + "/{1}/{0}.cshtml",
            //    "~/Themes/" + theme + "/Shared/{0}.cshtml"
            //};

            //PartialViewLocationFormats = new string[]
            //{
            //    "~/Themes/" + theme + "/{1}/{0}.cshtml",
            //    "~/Themes/" + theme + "/Shared/{0}.cshtml"
            //};

            //AreaViewLocationFormats = new string[]
            //{
            //    "~/{2}/{1}/{0}.cshtml",
            //    "~/{2}/Shared/{0}.cshtml"
            //};

            //AreaMasterLocationFormats = new string[]
            //{
            //    "~/{2}/{1}/{0}.cshtml",
            //    "~/{2}/Shared/{0}.cshtml"
            //};

            //AreaPartialViewLocationFormats = new string[]
            //{
            //    "~/{2}/{1}/{0}.cshtml",
            //    "~/{2}/Shared/{0}.cshtml"
            //};

            //FileExtensions = new string[]
            //{
            //    "cshtml"
            //};
        }
    }
}