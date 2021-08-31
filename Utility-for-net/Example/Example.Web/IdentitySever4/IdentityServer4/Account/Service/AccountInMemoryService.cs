using IdentityModel;
using IdentityServer4;
using IdentityServer4.Services;
using IdentityServer4.Stores;
using IdentityServer4.Test;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using Utility.IdentityServer4;

namespace IdentityServer.IdentityServer4.Service
{
    /// <summary>
    /// 基于  内存 实现
    /// </summary>
    public class AccountInMemoryService : IAccountService
    {
        private readonly TestUserStore _users;
        private readonly IIdentityServerInteractionService _interaction;
        private readonly IClientStore _clientStore;
        private readonly IAuthenticationSchemeProvider _schemeProvider;
        private readonly IEventService _events;

        public AccountInMemoryService(
            IIdentityServerInteractionService interaction,
            IClientStore clientStore,
            IAuthenticationSchemeProvider schemeProvider,
            IEventService events,
            TestUserStore users = null)
        {
            // if the TestUserStore is not in DI, then we'll just use the global users collection
            // this is where you would plug in your own custom identity management library (e.g. ASP.NET Identity)
            _users = users ?? new TestUserStore(InMemoryConfig.Users);

            _interaction = interaction;
            _clientStore = clientStore;
            _schemeProvider = schemeProvider;
            _events = events;
        }

        public HttpContext HttpContext { get; set; }
        public ClaimsPrincipal User { get; set; }

        public bool Login(LoginInputModel login)
        {
            var testUser = _users.ValidateCredentials(login.Username, login.Password);
            if (testUser)
            {
                ////InvalidOperationException: sub claim is missing
                // return SignIn(new ClaimsPrincipal(new ClaimsIdentity(_users.FindByUsername(login.Username).Claims)) { }, CookieAuthenticationDefaults.AuthenticationScheme);

                //SignInAsync when principal.Identity.IsAuthenticated is false is not allowed when AuthenticationOptions.RequireAuthenticatedSignIn is true.
                // return SignIn(new ClaimsPrincipal(new ClaimsIdentity(_users.FindByUsername(login.Username).Claims)) { }, "IdentityApiKey");

                //Account/Login?ReturnUrl=%2Fhome%2FIndex 框架 内部 封装死 了 怎么 改变 路劲 了
                //HttpContext.SignInAsync(
                //    new ClaimsPrincipal(
                //        new ClaimsIdentity(_users.FindByUsername(login.Username).Claims)
                //        ),
                //    new AuthenticationProperties() { IsPersistent=true, ExpiresUtc = DateTimeOffset.UtcNow.Add(TimeSpan.FromDays(30)) }
                //    );

                HttpContext.SignInAsync(
                 new IdentityServerUser(_users.FindByUsername(login.Username).SubjectId),
                 new AuthenticationProperties() { IsPersistent = true, ExpiresUtc = DateTimeOffset.UtcNow.Add(TimeSpan.FromDays(30)) }
                 );
                return true;
            }
            else
            {
                return false;
            }
        }


        public bool Register(LoginInputModel register)
        {
            var testUser = _users.FindByUsername(register.Username);
            if (testUser != null)
            {
                return false;
            }
            else
            {
                var claims = new List<Claim> {
                    new Claim(JwtClaimTypes.Name, register.Username),
                    new Claim(JwtClaimTypes.GivenName, "Alice"),
                    new Claim(JwtClaimTypes.FamilyName, "Smith"),
                    new Claim(JwtClaimTypes.Email, "AliceSmith@email.com"),
                    new Claim(JwtClaimTypes.EmailVerified, "true", ClaimValueTypes.Boolean),
                    new Claim(JwtClaimTypes.WebSite, "http://alice.com"),
                    new Claim(JwtClaimTypes.Address, @"{ 'street_address': 'One Hacker Way', 'locality': 'Heidelberg', 'postal_code': 69118, 'country': 'Germany' }", IdentityServerConstants.ClaimValueTypes.Json)
                };
                // _users.AutoProvisionUser("test", "test", claims);//添加一个新用户 密码无,  找不到 name 账户随机 这个 无法设置 密码
                InMemoryConfig.Users.Add(new TestUser() { Username = register.Username, Password = register.Password, SubjectId = register.Username, Claims = claims });
            }
            return true;
        }


        public async Task<bool> Logout(string logoutId)
        {
            if (!string.IsNullOrEmpty(logoutId))
            {
                var testUser = _interaction.GetLogoutContextAsync(logoutId).Result;//不存在 对象 值 都 为 null
                if (testUser.SessionId != null)
                {
                    return true;
                }
            }
            else
            {
                if (User?.Identity.IsAuthenticated == true)
                {
                    logoutId = _interaction.CreateLogoutContextAsync().Result;//为 null
                    //Response.Cookies.Append("logoutId", logoutId);
                    //Account/Login?ReturnUrl=%2Fhome%2FLogout
                    await HttpContext.SignOutAsync();//删除 cookie
                    //return  SignOut(CookieAuthenticationDefaults.AuthenticationScheme); //内部 怎么 处理 的了
                    //这样不会退出 还属于登录状态
                    //SignOut(CookieAuthenticationDefaults.AuthenticationScheme);
                    return true;

                }
            }
            return false;
        }
    }
}
