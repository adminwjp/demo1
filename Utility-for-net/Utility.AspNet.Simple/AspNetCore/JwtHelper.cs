#if NETCOREAPP2_0 || NETCOREAPP2_1 || NETCOREAPP2_2 || NETCOREAPP3_0 || NETCOREAPP3_1 || NET5_0 || NET6_0 || NETSTANDARD2_0 || NETSTANDARD2_1
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Http;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Utility.Security;

namespace Utility.AspNetCore
{
    public class JwtDto
    {
        public virtual string UserId { get; set; }
        public virtual string Token { get; set; }

        public virtual string RefreshToken { get; set; }

        public virtual long Auths { get; set; }

        public virtual long Expires { get; set; }
        public virtual bool Success { get; set; }


    }
    //https://www.cnblogs.com/danvic712/p/10331976.html
    public class JwtHelper
    {
        private readonly List<JwtDto> tokens = new List<JwtDto>(10000);
        public virtual IHttpContextAccessor ContextAccessor { get; set; }
        public virtual string UserId { get; set; }
        public virtual string Account { get; set; }
        public virtual string UserName { get; set; }
        public virtual string Phone { get; set; }
        public virtual string Email { get; set; }
        public virtual string Pwd { get; set; }
        public virtual string SecurityKey { get; set; } = "Jwt:SecurityKey";
        public virtual string RefreshSecurityKey { get; set; } = "Jwt:RefreshSecurityKey";
        public virtual double ExpireMinutes { get; set; } = 24 * 3600;
        public virtual DateTime Expiration { get; set; } = DateTime.Now.AddMinutes(24 * 3600);
        public virtual string Issuer { get; set; } = "Issuer";
        public virtual string Audience { get; set; } = "Audience";
        JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();

        /// <summary>
        /// 
        /// </summary>
        /// <returns>
        /// Token  Auths Expires
        /// </returns>
        public virtual JwtDto CreateToken()
        {
            DateTime authTime = DateTime.UtcNow;
            DateTime expiresAt = authTime.AddMinutes(ExpireMinutes);
            Expiration = expiresAt;
            //将用户信息添加到 Claim 中
            var identity = new ClaimsIdentity(JwtBearerDefaults.AuthenticationScheme);
          
            IEnumerable<Claim> claims = new Claim[] {
                new Claim(ClaimTypes.Name,UserName??"test"),
               // new Claim(ClaimTypes.Role,dto.Role.ToString()),
               new Claim(ClaimTypes.Email,Email??"test"),
               new Claim(ClaimTypes.MobilePhone,Phone??"test"),
               new Claim(ClaimTypes.WindowsAccountName,Account??"test"),
               new Claim(ClaimTypes.Expiration,expiresAt.ToString())
            };
            identity.AddClaims(claims);

            //签发一个加密后的用户信息凭证，用来标识用户的身份
            ContextAccessor.HttpContext.SignInAsync(JwtBearerDefaults.AuthenticationScheme, new ClaimsPrincipal(identity));

            
            SymmetricSecurityKey key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(SecurityHelper.Md5(SecurityKey)));
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),//创建声明信息
                Issuer = Issuer,//Jwt token 的签发者
                Audience = Audience,//Jwt token 的接收者
                Expires = expiresAt,//过期时间
                SigningCredentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256)//创建 token
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);

            SymmetricSecurityKey refrshKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(SecurityHelper.Md5(RefreshSecurityKey)));
            tokenDescriptor.SigningCredentials = new SigningCredentials(refrshKey, SecurityAlgorithms.HmacSha256);//创建 refrsh token
            var refrshToken = tokenHandler.CreateToken(tokenDescriptor);
            //存储 Token 信息
            var jwt = new JwtDto() { UserId=UserId,
                Token= tokenHandler.WriteToken(token),
                RefreshToken= tokenHandler.WriteToken(refrshToken),
                Auths =new DateTimeOffset(authTime).ToUnixTimeSeconds(),
                Expires=new DateTimeOffset(expiresAt).ToUnixTimeSeconds()
            };
            int index = -1;
            if ((index = tokens.FindIndex(it => it.UserId == UserId))>-1)
            {
                tokens.RemoveAt(index);
            }
            tokens.Add(jwt);
            return jwt;
        }

        public virtual JwtHelper ParseToken(string token)
        {

            var jwt = tokenHandler.ReadJwtToken(token);
            foreach (var cla in jwt.Claims)
            {
                if(cla.Type== ClaimTypes.Name)
                {
                    UserName = cla.Value;
                }
                if (cla.Type == ClaimTypes.Email)
                {
                    Email = cla.Value;
                }
                if (cla.Type == ClaimTypes.MobilePhone)
                {
                    Phone = cla.Value;
                }
                if (cla.Type == ClaimTypes.WindowsAccountName)
                {
                    Account = cla.Value;
                }
                if (cla.Type == ClaimTypes.Expiration)
                {
                    Expiration =DateTime.Parse(cla.Value);
                }
            }
            return this;
        }
    }
}
#endif