//------------------------------------------------------------------------------
// <copyright company="Tunynet">
//     Copyright (c) Tunynet Inc.  All rights reserved.
// </copyright>
//------------------------------------------------------------------------------

using Microsoft.AspNetCore.Http;
using System.Web;
using Tunynet.Utilities;

namespace Tunynet.Common
{
    /// <summary>
    /// 当前操作者信息获取器
    /// </summary>
    public class OperatorInfoGetter //: IOperatorInfoGetter
    {
        /// <summary>
        /// 获取当前操作者信息
        /// </summary>
        /// <returns></returns>
        public static OperatorInfo GetOperatorInfo(HttpContext httpContext)
        {
            OperatorInfo operatorInfo = new OperatorInfo();
            if (httpContext == null)
                return operatorInfo;
            IUser currentUser = UserContext.CurrentUser;
            operatorInfo.OperatorIP = WebUtility.GetIP();
            operatorInfo.AccessUrl = httpContext.Request.Path;
            operatorInfo.OperationUserId = currentUser != null ? currentUser.UserId : 0;
            operatorInfo.Operator = currentUser != null ? currentUser.DisplayName : string.Empty;
            return operatorInfo;
        }
    }
}