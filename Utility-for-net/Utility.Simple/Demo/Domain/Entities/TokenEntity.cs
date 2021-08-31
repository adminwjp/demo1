using System;
using System.Collections.Generic;
using System.Text;
using Utility.Domain.Entities;

namespace Utility.Demo.Domain.Entities
{ 
    /// <summary>用户 token </summary>
    public class TokenEntity : TokenEntity<long>
    {

    }
    /// <summary>用户 token </summary>
    public class TokenEntity<Key> : Entity<Key>
    {
        private string token;
        private long tokenExpried;
        private string refreshToken;
        private long createDate;
        private Key userId;
        private int flag;
        private long refreshTokenExpried;

        /// <summary>用户 token  </summary>
        public virtual string Token { get => token; set { Set(ref token, value, "Token"); } }
        /// <summary>用户 token 过期时间 单位 秒 </summary>
        public virtual long TokenExpried { get => tokenExpried; set { Set(ref tokenExpried, value, "TokenExpried"); } }
        /// <summary>用户 刷新 token  </summary>
        public virtual string RefreshToken { get => refreshToken; set { Set(ref refreshToken, value, "RefreshToken"); } }
        /// <summary>用户 刷新 token 过期时间 单位 秒 </summary>
        public virtual long RefreshTokenExpried { get => refreshTokenExpried; set { Set(ref refreshTokenExpried, value, "RefreshTokenExpried"); } }
        /// <summary>用户 创建 token 时间  </summary>
        public virtual long CreateDate { get => createDate; set { Set(ref createDate, value, "CreateDate"); } }
        /// <summary>用户 id  </summary>
        public virtual Key UserId { get => userId; set { Set(ref userId, value, "UserId"); } }
        /// <summary>标识(pc/mobile) </summary>
        public virtual int Flag { get => flag; set { Set(ref flag, value, "Flag"); } }
    }
}
