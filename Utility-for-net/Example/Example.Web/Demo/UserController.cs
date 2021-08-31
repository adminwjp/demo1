#if NET45 || NET451 || NET452 || NET46 || NET461 || NET462 || NET47 || NET471 || NET472 || NET48

using Microsoft.Extensions.Logging;
using SocialContact.Application.Services;
using SocialContact.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Serialization;
using Utility;
using Utility.Attributes;
using Utility.Json;
using Utility.Demo.Application.Services.Dtos;
using Utility.Extensions;
using Utility.Demo.Domain.Entities;
using Utility.Demo.Application.Services;
using System.Web.Http;

namespace Utility.Demo
{
   [UnitWork]
    public class UserController<User,Key>: ApiController
        where User:UserEntity<User,Key>,new()
    {
        protected UserAppService<User, Key> UserAppService;
        protected ILogger Logger;
        protected virtual void CheckUser(string userType)
        {
            if (userType?.ToLower() != "user")
            {
                throw new NotImplementedException();
            }
        }
        public UserController(UserAppService<User, Key> userAppService, ILogger logger)
        {
            this.UserAppService = userAppService;
            this.Logger = logger;
        }

        [Route("login")]
        [HttpPost]
        public virtual ResponseApi Login([FromBody] LoginDto loginDto)
        {

            User user;
            var msg = "Account Login:===>>";
            if (loginDto.Account.Contains("@"))
            {
                msg = "Email Login:===>>";
                user = UserAppService.LoginByEmailAndPwd(loginDto.Account, loginDto.Pwd);

            }
            else if (loginDto.Account.IsPhone())
            {
                msg = "Phone Login:===>>";
                user = UserAppService.LoginByPhoneAndPwd(loginDto.Account, loginDto.Pwd);
            }
            else
            {
                user = UserAppService.LoginByAccountAndPwd(loginDto.Account, loginDto.Pwd);
            }
            if (user == null)
            {
                Logger.LogWarning("{msg} Account:{Account},Pwd:{Pwd} login fail ", msg, loginDto.Account, loginDto.Pwd);
                return ResponseApi.FailByEnglish;
            }
            else
            {
                Logger.LogWarning("{msg} Account:{Account},Pwd:{Pwd} login success ", msg, loginDto.Account, loginDto.Pwd);
                return ResponseApi.CreateSuccess();
            }
        }
        [Route("exists")]
        [HttpGet]
        public virtual bool Exists(string account)
        {
            var exists = false;
            var msg = string.Empty;
            if (account.Contains("@"))
            {
                msg = "Email Exists:===>>";
                if (UserAppService.ExistsEmail(account))
                {
                    exists = true;
                    Logger.LogWarning("{msg} Account:{Account}", msg, account);
                }
            }
            else if (account.IsPhone())
            {
                msg = "Phone Register:===>>";
                if (UserAppService.ExistsPhone(account))
                {
                    exists = true;
                    Logger.LogWarning("{msg} Account:{Account},Exists Phone", msg, account);
                }
            }
            return exists;
        }
        [Route("register")]
        [HttpPost]
        public virtual ResponseApi Register([FromBody] LoginDto loginDto)
        {
            var register = false;
            var msg = string.Empty;
            if (loginDto.Account.Contains("@"))
            {
                msg = "Email Register:===>>";
                if (UserAppService.ExistsEmail(loginDto.Account))
                {
                    Logger.LogWarning("{msg} Account:{Account},Pwd:{Pwd} register fail ,Exists Email", msg, loginDto.Account, loginDto.Pwd);
                }
                else
                {
                    register = UserAppService.RegisterByEmailAndPwd(loginDto.Account, loginDto.Pwd);
                }
            }
            else if (loginDto.Account.IsPhone())
            {
                msg = "Phone Register:===>>";
                if (UserAppService.ExistsPhone(loginDto.Account))
                {
                    Logger.LogWarning("{msg} Account:{Account},Pwd:{Pwd} register fail ,Exists Email", msg, loginDto.Account, loginDto.Pwd);
                }
                else
                {
                    register = UserAppService.RegisterByPhoneAndPwd(loginDto.Account, loginDto.Pwd);
                }
            }
            else
            {
                return ResponseApi.FailByEnglish;
            }
            if (!register)
            {
                Logger.LogWarning("{msg} Account:{Account},Pwd:{Pwd} register fail ", msg, loginDto.Account, loginDto.Pwd);
                return ResponseApi.FailByEnglish;
            }
            else
            {
                Logger.LogWarning("{msg} Account:{Account},Pwd:{Pwd} register success ", msg, loginDto.Account, loginDto.Pwd);
                return ResponseApi.CreateSuccess();
            }
        }
      
    }
}
#endif

#if NETCOREAPP2_0 || NETCOREAPP2_1 || NETCOREAPP2_2 || NETCOREAPP3_0 || NETCOREAPP3_1 || NET5_0 || NET6_0 || NETSTANDARD2_0 || NETSTANDARD2_
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SocialContact.Application.Services;
using SocialContact.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Serialization;
using Utility;
using Utility.Attributes;
using Utility.Json;
using Utility.Demo.Application.Services.Dtos;
using Utility.Extensions;
using Utility.Demo.Domain.Entities;
using Utility.Demo.Application.Services;
using Utility.AspNetCore;
using Utility.AspNetCore.Controllers;
using Microsoft.AspNetCore.Authentication;
using Utility.Application.Services.Dtos;

namespace Utility.Demo
{
    [ProducesResponseType(typeof(ResponseApi), StatusCodes.Status200OK)]
    [Transtation]
    public abstract class UserByTokenController<User,Token> : UserController<User, long>
        where User: Domain.Entities.UserEntity<User,long>,new()
         where Token : Domain.Entities.TokenEntity<long>, new()
    {

        public UserByTokenController(UserAppService<User, long> userAppService, IHttpContextAccessor contextAccessor, ILogger logger)
            :base(userAppService,contextAccessor,logger)
        {


        }
        protected override void SaveToken(LoginDto loginDto, JwtDto jwt)
        {
            Token tokenEntity = new Token()
            {
                UserId = Convert.ToInt64(jwt.UserId),
                Token = jwt.Token,
                RefreshToken = jwt.RefreshToken,
                CreateDate = jwt.Auths,
                RefreshTokenExpried = jwt.Expires,
                TokenExpried = jwt.Expires,
                Flag=typeof(User).Name.GetHashCode()
            };
            base.UnitWork.Insert(tokenEntity);
            base.SaveToken(loginDto, jwt);
        }
    }
    [ProducesResponseType(typeof(ResponseApi), StatusCodes.Status200OK)]
    [Transtation]
    public class UserController<User,Key>: BaseController
        where User:BaseUserEntity<User,Key>,new()
    {
        /// <summary>
        /// 支持任何 类型账号 登录 或注册 
        /// </summary>
        protected  bool Test = false;
        protected UserAppService<User, Key> UserAppService;
        protected ILogger Logger;
        public UserController(UserAppService<User, Key> userAppService, IHttpContextAccessor contextAccessor, ILogger logger)
        {
            this.UserAppService = userAppService;
            this.Service = userAppService;
            this.ContextAccessor = contextAccessor;
            this.Logger = logger;
        }

        [HttpGet("{userType}/exists")]
        public virtual bool Exists(string userType, string account)
        {
            var exists = false;
            var msg = string.Empty;
            if (account.Contains("@"))
            {
                msg = "Email Exists:===>>";
                if (UserAppService.ExistsEmail(account))
                {
                    exists = true;
                    Logger.LogWarning("{msg} Account:{Account}", msg, account);
                }
            }
            else if (account.IsPhone())
            {
                msg = "Phone Exists:===>>";
                if (UserAppService.ExistsPhone(account))
                {
                    exists = true;
                    Logger.LogWarning("{msg} Account:{Account},Exists Phone", msg, account);
                }
            }else if (Test)
            {
                msg = "Account Exists:===>>";
                if (UserAppService.ExistsAccount(account))
                {
                    exists = true;
                    Logger.LogWarning("{msg} Account:{Account},Exists Account", msg, account);
                }
            }
            return exists;
        }
        [HttpPost("{userType}/login")]
        public virtual ResponseApi Login(string userType, [FromForm][FromBody] LoginDto loginDto)
        {

            User user;
            var msg = "Account Login:===>>";
            if (loginDto.Account.Contains("@"))
            {
                msg = "Email Login:===>>";
                user = UserAppService.LoginByEmailAndPwd(loginDto.Account, loginDto.Pwd);

            }
            else if (loginDto.Account.IsPhone())
            {
                msg = "Phone Login:===>>";
                user = UserAppService.LoginByPhoneAndPwd(loginDto.Account, loginDto.Pwd);
            }
            else 
            { 
                user = UserAppService.LoginByAccountAndPwd(loginDto.Account, loginDto.Pwd);
            }
            if (user == null)
            {
                Logger.LogWarning("{msg} Account:{Account},Pwd:{Pwd} login fail ", msg, loginDto.Account, loginDto.Pwd);
                return ResponseApi.FailByEnglish;
            }
            else
            {
                Logger.LogWarning("{msg} Account:{Account},Pwd:{Pwd} login success ", msg, loginDto.Account, loginDto.Pwd);
                var jwt = new JwtHelper();
                jwt.Account = loginDto.Account;
                jwt.ExpireMinutes = 60;
                jwt.Email = user.Email;
                jwt.UserId = user.Id.ToString();
                jwt.Phone = user.Phone;
                jwt.UserName = user.NickName;
                jwt.ContextAccessor = ContextAccessor;
                var jwtDto=jwt.CreateToken();
                SaveToken(loginDto, jwtDto);
                Cache.Set(user.Id.ToString(), loginDto,jwt.Expiration);
                var logoutId = Guid.NewGuid().ToString("n");
                Cache.Set(user.Id.ToString()+"l", logoutId, jwt.Expiration);
                Cache.Set(logoutId, jwt.Account+":"+ user.Id, jwt.Expiration);

                return ResponseApi.CreateSuccess().SetData(jwtDto);
            }
        }
        protected virtual void SaveToken(LoginDto loginDto, JwtDto jwt)
        {

        }
        [HttpPost("{userType}/register")]
        public virtual ResponseApi Register(string userType, [FromForm] LoginDto loginDto)
        {
            var register = false;
            var msg = string.Empty;
            if (loginDto.Account.Contains("@"))
            {
                msg = "Email Register:===>>";
                if (UserAppService.ExistsEmail(loginDto.Account))
                {
                    Logger.LogWarning("{msg} Account:{Account},Pwd:{Pwd} register fail ,Exists Email", msg, loginDto.Account, loginDto.Pwd);
                }
                else
                {
                    register = UserAppService.RegisterByEmailAndPwd(loginDto.Account, loginDto.Pwd);
                }
            }
            else if (loginDto.Account.IsPhone())
            {
                msg = "Phone Register:===>>";
                if (UserAppService.ExistsPhone(loginDto.Account))
                {
                    Logger.LogWarning("{msg} Account:{Account},Pwd:{Pwd} register fail ,Exists Email", msg, loginDto.Account, loginDto.Pwd);
                }
                else
                {
                    register = UserAppService.RegisterByPhoneAndPwd(loginDto.Account, loginDto.Pwd);
                }
            }
            else 
            {
                if (Test)
                {
                    msg = "Account Register:===>>";
                    if (UserAppService.ExistsAccount(loginDto.Account))
                    {
                        Logger.LogWarning("{msg} Account:{Account},Pwd:{Pwd} register fail ,Exists Account", msg, loginDto.Account, loginDto.Pwd);
                    }
                    else
                    {
                        register = UserAppService.Insert(new User() { Account=loginDto.Account,Pwd=loginDto.Pwd})>0;
                    }
                }
                else
                {
                    return ResponseApi.FailByEnglish;
                }
            }
            if (!register)
            {
                Logger.LogWarning("{msg} Account:{Account},Pwd:{Pwd} register fail ", msg, loginDto.Account, loginDto.Pwd);
                return ResponseApi.FailByEnglish;
            }
            else
            {
                Logger.LogWarning("{msg} Account:{Account},Pwd:{Pwd} register success ", msg, loginDto.Account, loginDto.Pwd);
                return ResponseApi.CreateSuccess();
            }
        }
        [HttpPost("{userType}/logout/{logout_id}")]
        public virtual ResponseApi Logout(string userType, string logout_id)
        {
            //acount:user_id
            var account = Cache.Get<string>(logout_id);
            if ( string.IsNullOrEmpty(account))
            {
                return ResponseApi.CreateFail();
            }
            Cache.Remove(logout_id);
            Cache.Remove(account.Split(':')[1]);
            var msg = "Account Logout:===>>";
            Logger.LogWarning("{msg} Account:{Account} Logout success ", msg, account);
            ContextAccessor.HttpContext.SignOutAsync();
            return ResponseApi.CreateSuccess();
           
        }
        [HttpPost("list/{page}/{size}")]
        [HttpGet("list/{page}/{size}")]
        public virtual ResponseApi List(int page, int size)
        {
            var data = UserAppService.FindListByPage(null, page, size);
            var count = UserAppService.Count((User)null);
            return ResponseApi.CreateSuccess().SetData(new ResultDto<User>(data, page, size, count));
        }
    }
}
#endif