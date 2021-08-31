//------------------------------------------------------------------------------
// <copyright company="Tunynet">
//     Copyright (c) Tunynet Inc.  All rights reserved.
// </copyright>
//------------------------------------------------------------------------------

using Microsoft.AspNetCore.Http;
using System;
using System.Web;
using Tunynet.Common.Repositories;
using Tunynet.Events;

namespace Tunynet.Common
{
    /// <summary>
    /// 基于Form的身份认证服务实现
    /// </summary>

    public class FormsAuthenticationService : IAuthenticationService
    {
        public HttpContext Context;
        /// <summary>
        /// 登录
        /// </summary>
        /// <param name="user">登录的用户</param>
        /// <param name="rememberPassword">是否记住密码(是否长时登录)</param>
        /// <param name="cookiePath"></param>
        public void SignIn(IUser user, bool rememberPassword, string cookiePath = "")
        {
            Context.Response.Cookies.Append("user_id",user.UserId.ToString(),new CookieOptions() { Path= cookiePath ,MaxAge=new TimeSpan(30,24,60,60),SameSite= SameSiteMode.Lax});
   
            IUser _signedInUser = GetAuthenticatedUser();
            User user_object = user as User;
            if (user_object != null)
                EventBus<User>.Instance().OnAfter(user_object, new CommonEventArgs(EventOperationType.Instance().SignIn()));
        }

        /// <summary>
        /// 注销
        /// </summary>
        public void SignOut()
        {
            _signedInUser = GetAuthenticatedUser();
            User user_object = _signedInUser as User;
            //FormsAuthentication.SignOut();

            if (user_object != null)
            {
                EventBus<User>.Instance().OnAfter(user_object, new CommonEventArgs(EventOperationType.Instance().SignOut()));
                new OnlineUserRepository().Offline(user_object.UserName);
            }
        }

        private IUser _signedInUser = null;

        /// <summary>
        /// 获取当前认证的用户
        /// </summary>
        /// <returns>
        /// 当前用户未通过认证则返回null
        /// </returns>
        public IUser GetAuthenticatedUser()
        {
            HttpContext httpContext = Context;
            if (httpContext == null || httpContext.User==null || (httpContext.User.Identity==null ))
            {
                return null;
            }
            long userId;
            if (long.TryParse(httpContext.User.Identity.Name, out userId))
            {
                _signedInUser = UserService.GetFullUser(userId);
            }

            return _signedInUser;
        }
    }
}