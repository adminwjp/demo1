//------------------------------------------------------------------------------
// <copyright company="Tunynet">
//     Copyright (c) Tunynet Inc.  All rights reserved.
// </copyright>
//------------------------------------------------------------------------------

namespace Tunynet.Common
{
    /// <summary>
    /// 用户上下文
    /// </summary>
    public static class UserContext
    {
        /// <summary>
        /// 获取当前用户
        /// </summary>
        public static IUser CurrentUser
        {
            get
            {
                IUserService userService = DIContainer.Resolve<IUserService>();
                IAuthenticationService authenticationService = DIContainer.ResolvePerHttpRequest<FormsAuthenticationService>();
                var currentUser = authenticationService.GetAuthenticatedUser();
                if (currentUser != null)
                    return currentUser;

                #region cookie禁用情况

                //string token = string.Empty;
                //if (HttpContext.Current != null && HttpContext.Current.Request != null)
                //{
                //    token = HttpContext.Current.Request.Form.Get<string>("CurrentUserIdToken", string.Empty);

                //    if (string.IsNullOrEmpty(token))
                //        token = HttpContext.Current.Request.QueryString.Get<string>("CurrentUserIdToken", string.Empty);
                //}

                //if (!string.IsNullOrEmpty(token))
                //{
                //    token = Tunynet.Utilities.WebUtility.UrlDecode(token);
                //    bool isTimeOut = false;
                //    long userId = Utility.DecryptTokenForUploadfile(token.ToString(), out isTimeOut);
                //    if (userId > 0)
                //    {
                //        currentUser = userService.GetUser(userId);
                //        if (currentUser != null)
                //        {
                //            return currentUser;
                //        }
                //    }
                //}

                #endregion cookie禁用情况

                return null;
            }
        }
    }
}