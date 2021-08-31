using Microsoft.AspNetCore.Http;
using System.Security.Claims;
using System.Threading.Tasks;
using Utility.IdentityServer4;

namespace IdentityServer.IdentityServer4.Service
{
    /// <summary>
    /// 账户服务
    /// </summary>
    public interface IAccountService
    {
        HttpContext HttpContext { get; set; }
        ClaimsPrincipal User { get; set; }
        /// <summary>
        /// 登录
        /// </summary>
        /// <param name="login"></param>
        /// <returns></returns>
        bool Login(LoginInputModel login);

        /// <summary>
        /// 注册
        /// </summary>
        /// <param name="register"></param>
        /// <param name="pwd"></param>
        /// <returns></returns>
        bool Register(LoginInputModel register);

        /// <summary>
        /// 退出
        /// </summary>
        /// <param name="logoutId"></param>
        /// <returns></returns>

        Task<bool> Logout(string logoutId);
    }
}
