using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;

namespace IdentityServer.IdentityServer4.Service
{
    /// <summary>
    /// 基于 数据库 ef 实现
    /// </summary>
    public class AccountEfService<User> where User : IdentityUser
    {
        private UserManager<User> _userManager;
        private SignInManager<User> _signInManager;

        public AccountEfService(UserManager<User> userManager, SignInManager<User> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public async Task<User> FindByUsername(string user)
        {
            return await _userManager.FindByEmailAsync(user);
        }

        public async Task<bool> ValidateCredentials(User user, string password)
        {
            return await _userManager.CheckPasswordAsync(user, password);
        }

        public Task SignIn(User user)
        {
            return _signInManager.SignInAsync(user, true);
        }

        public Task SignInAsync(User user, AuthenticationProperties properties, string authenticationMethod = null)
        {
            return _signInManager.SignInAsync(user, properties, authenticationMethod);
        }
    }
}
