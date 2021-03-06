//------------------------------------------------------------------------------
// <copyright company="Tunynet">
//     Copyright (c) Tunynet Inc.  All rights reserved.
// </copyright>
//------------------------------------------------------------------------------

namespace Tunynet.Common
{
    /// <summary>
    /// 通过用户数据仓储实现查询
    /// </summary>
    public class DefaultUserIdToUserNameDictionary : UserIdToUserNameDictionary
    {
        

        /// <summary>
        /// 根据用户Id获取用户名
        /// </summary>
        /// <returns>
        /// 用户名
        /// </returns>
        protected override string GetUserNameByUserId(long userId)
        {
            User user = UserService.GetUser(userId);
            if (user != null)
                return user.UserName;
            return null;
        }

        /// <summary>
        /// 根据用户Id获取DisplayName
        /// </summary>
        /// <returns>
        /// DisplayName
        /// </returns>
        protected override string GetDisplayNameByUserId(long userId)
        {
            User user = UserService.GetUser(userId);
            if (user != null)
                return user.DisplayName;
            return null;
        }

        /// <summary>
        /// 根据用户名获取用户Id
        /// </summary>
        /// <param name="userName">用户名</param>
        /// <returns>
        /// 用户Id
        /// </returns>
        protected override long GetUserIdByUserName(string userName)
        {
            return UserService.GetUserIdByUserName(userName);
        }
    }
}