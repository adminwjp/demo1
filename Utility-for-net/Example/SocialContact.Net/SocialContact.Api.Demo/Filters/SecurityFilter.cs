//------------------------------------------------------------------------------
// <copyright company="Tunynet">
//     Copyright (c) Tunynet Inc.  All rights reserved.
// </copyright>
//------------------------------------------------------------------------------

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.IO;

namespace Tunynet.Common
{
    /// <summary>
    /// 安全校验过滤器
    /// </summary>
    public class SecurityFilter : IAuthorizationFilter, IActionFilter
    {
        /// <summary>
        /// 校验CSRF Token
        /// </summary>
        /// <param name="filterContext"></param>
        public void OnAuthorization(AuthorizationFilterContext filterContext)
        {
            var requestPath = filterContext.HttpContext.Request.Path.Value.ToLower();
            #region 验证上传文件
            if (string.Equals("post", filterContext.HttpContext.Request.Method, StringComparison.OrdinalIgnoreCase) &&
            filterContext.HttpContext.Request.Form.Count > 0 &&
             (requestPath.StartsWith(CachedUrlHelper.Action("Uploads", "Common")) ||
            requestPath.StartsWith(CachedUrlHelper.Action("UploadFiles", "Common")) ||
            requestPath.StartsWith(CachedUrlHelper.Action("UploadFilesImg", "Common"))||
            requestPath.StartsWith(CachedUrlHelper.Action("_EditAvatar", "Common"))))
            {
                if (filterContext.HttpContext.Request.Form.Count > 0)
                {
                    var file = filterContext.HttpContext.Request.Form.Files[0];
                    var extension = Path.GetExtension(file.FileName);
                    if (!Utility.UploadExtensions().Contains(extension.ToLower()))
                    {
                        filterContext.Result = new JsonResult(new StatusMessageData(StatusMessageType.Error, "上传文件有误，安全校验失败！")) ;
                    }
                }
                return;
            }
            #endregion

            if (string.Equals("post", filterContext.HttpContext.Request.Method, StringComparison.OrdinalIgnoreCase) &&
                filterContext.HttpContext.Request.Form.Count > 0 &&
                !filterContext.HttpContext.Request.Path.Value.StartsWith(Tunynet.Utilities.WebUtility.ResolveUrl("~/common/")) &&
                !filterContext.HttpContext.Request.Path.Value.StartsWith(Tunynet.Utilities.WebUtility.ResolveUrl("~/common/")) &&
                !filterContext.HttpContext.Request.Path.Value.ToLower().EndsWith("notifyurl"))
            {
                ValidateAntiForgeryTokenAttribute _validator = new ValidateAntiForgeryTokenAttribute();
                try
                {
                   // _validator.OnAuthorization(filterContext);
                }
                catch (Exception)
                {
                    filterContext.Result = new JsonResult(new StatusMessageData(StatusMessageType.Error, "输入有误，安全校验失败！")) ;
                    return;
                }
            }
        }

        /// <summary>
        /// 校验ModelState
        /// </summary>
        /// <param name="filterContext"></param>
        public void OnActionExecuting(ActionExecutingContext filterContext)
        {
            //if (string.Equals("post", filterContext.HttpContext.Request.HttpMethod, StringComparison.OrdinalIgnoreCase) && !filterContext.HttpContext.Request.Path.StartsWith("/account/resetpassword"))
            //{
            //    if (!filterContext.Controller.ViewData.ModelState.IsValid)
            //    {
            //        var errorMessage = new StringBuilder();
            //        foreach (var val in filterContext.Controller.ViewData.ModelState.Values)
            //        {
            //            foreach (var error in val.Errors)
            //            {
            //                errorMessage.Append(error.ErrorMessage).Append(";");
            //            }
            //        }
            //        filterContext.Result = new JsonResult() { Data = new StatusMessageData(StatusMessageType.Error, "输入有误: " + errorMessage) };
            //        return;
            //    }
            //}
        }

        public void OnActionExecuted(ActionExecutedContext filterContext)
        {
        }
    }
}