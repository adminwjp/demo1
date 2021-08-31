

using Utility.Application.Services;
using System;
using Utility.Mappers;
using Utility.Domain.Repositories;
using Utility.Helpers;
using Utility.Demo.Domain.Entities;
using Utility.Attributes;

namespace Utility.Demo.Application.Services
{
    public interface IUserAppService<User, Key>: ICrudAppService<User, Key>
         where User : BaseUserEntity<User, Key>, new()
    {
        User LoginByAccountAndPwd(string account, string pwd);
        User LoginByPhoneAndPwd(string phone, string pwd);
        User LoginByEmailAndPwd(string email, string pwd);
        bool RegisterByPhoneAndPwd(string phone, string pwd);
        bool RegisterByEmailAndPwd(string email, string pwd);
        bool ExistsPhone(string phone);
        bool ExistsAccount(string account);
        bool ExistsEmail(string email);
    }
    [Transtation]
    public class UserAppService<User,Key> : CrudAppService<IRepository<User, Key>, User, Key>, IUserAppService<User, Key>
        where User: BaseUserEntity<User,Key>,new()
    {
        public UserAppService(IRepository<User, Key> repository) : base(repository)
        {
        }
        public virtual User LoginByAccountAndPwd(string account, string pwd)
        {
            return base.Repository.FindSingle(it => it.Account == account && it.Pwd == pwd);
        }
        public virtual User LoginByPhoneAndPwd(string phone, string pwd)
        {
            return base.Repository.FindSingle(it => it.Phone == phone && it.Pwd == pwd);
        }
        public virtual User LoginByEmailAndPwd(string email, string pwd)
        {
            return base.Repository.FindSingle(it => it.Email == email && it.Pwd == pwd);
        }


        public virtual bool RegisterByPhoneAndPwd(string phone, string pwd)
        {
            base.Repository.Insert(new User() { Phone = phone, Pwd = pwd, RegisterDate = CommonHelper.TotalMilliseconds() });
            return true;
        }
        public virtual bool RegisterByEmailAndPwd(string email, string pwd)
        {
            base.Repository.Insert(new User() { Email = email, Pwd = pwd, RegisterDate = CommonHelper.TotalMilliseconds() });
            return true;
        }

        public virtual bool ExistsPhone(string phone)
        {
            return base.Repository.Count(it => it.Phone == phone) > 0;
        }
        public virtual bool ExistsAccount(string account)
        {
            return base.Repository.Count(it => it.Account == account) > 0;
        }
        public virtual bool ExistsEmail(string email)
        {
            return base.Repository.Count(it => it.Email == email) > 0;
        }
    }
}
