//------------------------------------------------------------------------------
// <copyright company="Tunynet">
//     Copyright (c) Tunynet Inc.  All rights reserved.
// </copyright>
//------------------------------------------------------------------------------

using Core;
using System.Linq;

namespace Tunynet.Common
{
    /// <summary>
    /// 通过用户数据仓储实现查询
    /// </summary>
    public class DefaultUserIdToUserGuIdDictionary : UserIdToUserGuidDictionary
    {

        /// <summary>
        /// 根据用户Id获取UserGUid
        /// </summary>
        /// <returns>
        /// UserGuid
        /// </returns>
        protected override string GetUserGuidByUserId(long userId)
        {
           return GlobalHelper.GetUnitWork().Find<User>(it=>it.UserId==userId).Select(it=>it.UserGuid).FirstOrDefault();
        }

        /// <summary>
        /// 根据GUId获取用户Id
        /// </summary>
        /// <returns>
        /// Userid
        /// </returns>
        protected override long GetUserIdByUserGuid(string userGUId)
        {
            return GlobalHelper.GetUnitWork().Find<User>(it => it.UserGuid == userGUId).Select(it => it.UserId).FirstOrDefault();
        }
    }
}