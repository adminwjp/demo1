//------------------------------------------------------------------------------
// <copyright company="Tunynet">
//     Copyright (c) Tunynet Inc.  All rights reserved.
// </copyright>
//------------------------------------------------------------------------------

namespace Tunynet.Common
{
    /// <summary>
    /// 触屏版详情Url获取
    /// </summary>
    public interface ITouchScreenUrlGetter
    {
        /// <summary>
        /// 租户类型Id
        /// </summary>
        string TenantTypeId { get; }

        /// <summary>
        /// 详情页的Url
        /// </summary>
        /// <param name="objectId"></param>
        /// <returns></returns>
        string GetTouchScreenDetailUrl(long objectId);
    }
}