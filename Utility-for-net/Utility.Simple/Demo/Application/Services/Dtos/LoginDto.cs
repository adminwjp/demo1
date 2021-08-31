using System;
using System.Collections.Generic;
using System.Text;
using Utility.Application.Services.Dtos;

namespace Utility.Demo.Application.Services.Dtos
{
    public class LoginDto:EntityDto<long>
    {
        private string account;
        private string pwd;
        private string code;
        private string returnUrl;
        private bool rember;
        private bool autoLogin;

        public virtual string Account { get => account; set { Set(ref account, value, "Account"); } }
        public virtual string Pwd { get => pwd; set { Set(ref pwd, value, "Pwd"); } }

        public virtual string Code { get => code; set { Set(ref code, value, "Code"); } }
        public virtual string ReturnUrl { get => returnUrl; set { Set(ref returnUrl, value, "ReturnUrl"); } }

        public virtual bool Rember { get => rember; set { Set(ref rember, value, "Rember"); } }
        public virtual bool AutoLogin { get => autoLogin; set { Set(ref autoLogin, value, "AutoLogin"); } }
    }
}
