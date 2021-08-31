using IdentityModel;
using IdentityServer4;
using IdentityServer4.Extensions;
using IdentityServer4.Services;
using IdentityServer4.Stores;
using IdentityServer4.Test;
using IdentityServerHost.Quickstart.UI;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Utility.IdentityServer4.Controllers
{
    public class TestController : Controller
    {

        private readonly TestUserStore _users;
        private readonly IIdentityServerInteractionService _interaction;
        private readonly IClientStore _clientStore;
        private readonly IAuthenticationSchemeProvider _schemeProvider;
        private readonly IEventService _events;

        public TestController(
            IIdentityServerInteractionService interaction,
            IClientStore clientStore,
            IAuthenticationSchemeProvider schemeProvider,
            IEventService events,
            TestUserStore users = null)
        {
            // if the TestUserStore is not in DI, then we'll just use the global users collection
            // this is where you would plug in your own custom identity management library (e.g. ASP.NET Identity)
            _users = users ?? new TestUserStore(TestUsers.Users);

            _interaction = interaction;
            _clientStore = clientStore;
            _schemeProvider = schemeProvider;
            _events = events;
        }

        [HttpGet]
        public IActionResult Get()
        {
            return new JsonResult(from c in User.Claims select new { c.Type, c.Value });
        }

        [AllowAnonymous]
        [HttpGet]
        public IActionResult GetUser()
        {
            return new JsonResult(new { TestUsers.Users });
        }
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [AllowAnonymous]
        [HttpGet]
        public IActionResult Login(string returnUrl)
        {
            if (!string.IsNullOrEmpty(returnUrl))
            {
                return Redirect(returnUrl);
            }
            return View();
        }

        [AllowAnonymous]
        [HttpPost]
        public IActionResult Login(string account, string pwd)
        {
            var testUser = _users.ValidateCredentials(account, pwd);
            if (testUser)
            {
                ////InvalidOperationException: sub claim is missing
                // return SignIn(new ClaimsPrincipal(new ClaimsIdentity(_users.FindByUsername(account).Claims)) { }, CookieAuthenticationDefaults.AuthenticationScheme);

                //SignInAsync when principal.Identity.IsAuthenticated is false is not allowed when AuthenticationOptions.RequireAuthenticatedSignIn is true.
                // return SignIn(new ClaimsPrincipal(new ClaimsIdentity(_users.FindByUsername(account).Claims)) { }, "IdentityApiKey");

                //Account/Login?ReturnUrl=%2Fhome%2FIndex 框架 内部 封装死 了 怎么 改变 路劲 了
                //HttpContext.SignInAsync(
                //    new ClaimsPrincipal(
                //        new ClaimsIdentity(_users.FindByUsername(account).Claims)
                //        ),
                //    new AuthenticationProperties() { IsPersistent=true, ExpiresUtc = DateTimeOffset.UtcNow.Add(TimeSpan.FromDays(30)) }
                //    );

                //HttpContext.SignInAsync(
                // new IdentityServerUser(_users.FindByUsername(account).SubjectId),
                // new AuthenticationProperties() { IsPersistent = true, ExpiresUtc = DateTimeOffset.UtcNow.Add(TimeSpan.FromDays(30)) }
                // );
                return Redirect("Index");
            }
            else
            {
                return View();
            }
        }

        //退出时 按理可以 访问 匿名 时不能访问
        [AllowAnonymous]
        [HttpGet]
        public IActionResult Logout()
        {
            return View();
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> Logout(string logoutId)
        {
            if (!string.IsNullOrEmpty(logoutId))
            {
                var testUser = _interaction.GetLogoutContextAsync(logoutId).Result;
                if (testUser.SessionId != null)
                {
                    ViewBag.userName = _users.FindBySubjectId(testUser.SubjectId).Username;
                    // Response.Cookies.Append("logoutId", testUser.SubjectId);
                    return Redirect("Logout");
                    //return Content("<script>alert('account exises!);</script>");
                }
            }
            else
            {
                if (User?.Identity.IsAuthenticated == true)
                {
                    logoutId = _interaction.CreateLogoutContextAsync().Result;
                    //Response.Cookies.Append("logoutId", logoutId);
                    ViewBag.userName = _users.FindBySubjectId(User?.Identity.GetSubjectId()).Username;
                    //Account/Login?ReturnUrl=%2Fhome%2FLogout
                    await HttpContext.SignOutAsync();//删除 cookie
                                                     //return  SignOut(CookieAuthenticationDefaults.AuthenticationScheme); //内部 怎么 处理 的了
                                                     //这样不会退出 还属于登录状态
                                                     //SignOut(CookieAuthenticationDefaults.AuthenticationScheme);

                    return Redirect("Logout");

                }
            }
            return Content("<script>alert('account exises!);</script>");
        }

        [AllowAnonymous]
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [AllowAnonymous]
        [HttpPost]
        public IActionResult Register(string account, string pwd)
        {
            var testUser = _users.FindByUsername(account);
            if (testUser != null)
            {
                return Content("<script>alert('account exises!);</script>");
            }
            else
            {
                var claims = new List<Claim> {
                    new Claim(JwtClaimTypes.Name, account),
                    new Claim(JwtClaimTypes.GivenName, "Alice"),
                    new Claim(JwtClaimTypes.FamilyName, "Smith"),
                    new Claim(JwtClaimTypes.Email, "AliceSmith@email.com"),
                    new Claim(JwtClaimTypes.EmailVerified, "true", ClaimValueTypes.Boolean),
                    new Claim(JwtClaimTypes.WebSite, "http://alice.com"),
                    new Claim(JwtClaimTypes.Address, @"{ 'street_address': 'One Hacker Way', 'locality': 'Heidelberg', 'postal_code': 69118, 'country': 'Germany' }", IdentityServerConstants.ClaimValueTypes.Json)
                };
                // _users.AutoProvisionUser("test", "test", claims);//添加一个新用户 密码无,  找不到 name 账户随机
                TestUsers.Users.Add(new TestUser() { Username = account, Password = pwd, SubjectId = account, Claims = claims });
            }
            return Redirect("Login");
        }
    }
}
