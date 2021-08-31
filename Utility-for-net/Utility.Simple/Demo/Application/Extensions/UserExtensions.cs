using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Utility.Demo.Domain.Entities;

namespace Utility.Demo.Application.Extensions
{
    public static class UserExtensions
    {
        public static User LoginByAccountAndPwd<User, Key>(this IQueryable<User> queryable,string account, string pwd) where User : UserEntity<User, Key>, new()
        {
            return queryable.Where(it => it.Account == account && it.Pwd == pwd).FirstOrDefault();
        }
        public static User LoginByPhoneAndPwd<User, Key>(this IQueryable<User> queryable, string phone, string pwd) where User : UserEntity<User, Key>, new()
        {
            return queryable.Where(it => it.Phone == phone && it.Pwd == pwd).FirstOrDefault();
        }
        public static User LoginByEmailAndPwd<User, Key>(this IQueryable<User> queryable, string email, string pwd) where User : UserEntity<User, Key>, new()
        {
            return queryable.Where(it => it.Email == email && it.Pwd == pwd).FirstOrDefault();
        }

        public static bool ExistsPhone<User, Key>(this IQueryable<User> queryable, string phone) where User : UserEntity<User, Key>, new()
        {
            return queryable.Where(it => it.Phone == phone).Count() > 0;
        }

        public static bool ExistsEmail<User, Key>(this IQueryable<User> queryable, string email) where User : UserEntity<User, Key>, new()
        {
            return queryable.Where(it => it.Email == email).Count() > 0;
        }
        public static bool ExistsAccount<User, Key>(this IQueryable<User> queryable, string account) where User : UserEntity<User, Key>, new()
        {
            return queryable.Where(it => it.Account == account).Count() > 0;
        }
    }
}
