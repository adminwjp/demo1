using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IdentityServer.IdentityServer4.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Utility;

namespace Utility.IdentityServer4
{
    /// <summary>
    ///identity4 账户 控制器 
    /// </summary>
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [ApiVersion("1.0")]
    [Authorize]
    //[ProducesResponseType(typeof(IResponseApi), 200)] swagger exception logger
    public class AccountController : ControllerBase
    {
        IAccountService accountService;
        public AccountController(IAccountService accountService)
        {
            this.accountService = accountService;
            this.accountService.HttpContext = HttpContext;
            this.accountService.User = User;
        }
        /// <summary>
        /// 注册
        /// </summary>
        [AllowAnonymous]
        [HttpPost("Register")]
        
        public ResponseApi Register([FromForm]LoginInputModel register)
        {
            bool res = accountService.Register(register);
            return ResponseApi.Create(Language.Chinese, res ? Code.RegisterSucces : Code.RegisterFail);
        }
        /// <summary>
        /// 登录
        /// </summary>
        [AllowAnonymous]
        [HttpPost]
        [HttpPost("Login")]
        public IResponseApi Login(LoginInputModel login)
        {
            bool res = accountService.Login(login);
            return ResponseApi.Create(Language.Chinese, res ? Code.LoginSuccess : Code.LoginFail);
        }
        /// <summary>
        /// 退出
        /// </summary>
        /// <param name="logoutId"></param>
        [HttpPost("Logout/{logoutId}")]
        public async Task<IResponseApi> Logout(string logoutId)
        {
            bool res =await accountService.Logout(logoutId);
            return ResponseApi.Create(Language.Chinese, res ? Code.LogoutSuccess : Code.LogoutFail);
        }
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        [HttpGet("Get")]
        public IActionResult Get()
        {
            return new JsonResult(from c in User.Claims select new { c.Type, c.Value });
        }
    }
}
