using System;
using System.Collections.Generic;
using System.Text;
using Utility.Domain.Entities;

namespace Utility.Demo.Domain.Entities
{
    /// <summary>用户日志 </summary>
    public class UserLogEntity: UserLogEntity<long>
    {

    }
    /// <summary>用户日志 </summary>
    public class UserLogEntity<Key> : Entity<Key>
    {
        private UserLogFlag flag;
        private long addDate;
        private string newPhone;
        private string newEmail;
        private string newAccount;
        private string newPwd;
        private Key userId;
        private string account;
        private string email;
        private string phone;
        private string oldPwd;
        /// <summary>用户id </summary>
        public virtual Key UserId { get => userId; set { Set(ref userId, value, "UserId"); } }
        /// <summary>用户账号 </summary>
        public virtual string Account { get => account; set { Set(ref account, value, "Account"); } }
        /// <summary>用户 邮箱</summary>
        public virtual string Email { get => email; set { Set(ref email, value, "Email"); } }
        /// <summary>用户手机号 </summary>
        public virtual string Phone { get => phone; set { Set(ref phone, value, "Phone"); } }
        /// <summary>用户旧密码 </summary>
        public virtual string OldPwd { get => oldPwd; set { Set(ref oldPwd, value, "OldPwd"); } }

        /// <summary>用户新密码 </summary>
        public virtual string NewPwd { get => newPwd; set { Set(ref newPwd, value, "NewPwd"); } }

        /// <summary>用户新账号 </summary>
        public virtual string NewAccount { get => newAccount; set { Set(ref newAccount, value, "NewAccount"); } }
        /// <summary>用户 新邮箱</summary>
        public virtual string NewEmail { get => newEmail; set { Set(ref newEmail, value, "NewEmail"); } }
        /// <summary>用户新手机号 </summary>
        public virtual string NewPhone { get => newPhone; set { Set(ref newPhone, value, "NewPhone"); } }

        /// <summary>用户日志添加时间 </summary>
        public virtual long AddDate { get => addDate; set { Set(ref addDate, value, "AddDate"); } }
        /// <summary>用户日志标识 </summary>
        public virtual UserLogFlag Flag { get => flag; set { Set(ref flag, value, "Flag"); } }
    }
    /// <summary>用户日志标识 </summary>
    public enum UserLogFlag
    {
        /// <summary>未知 </summary>
        None,
        /// <summary>修改密码 </summary>
        UpdatePwd,
        /// <summary>修改邮箱 </summary>
        UpdateEmail,
        /// <summary>修改手机号 </summary>
        UpdatePhone,
    }
}
