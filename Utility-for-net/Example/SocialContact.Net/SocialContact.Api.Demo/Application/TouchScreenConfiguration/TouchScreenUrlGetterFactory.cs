//------------------------------------------------------------------------------
// <copyright company="Tunynet">
//     Copyright (c) Tunynet Inc.  All rights reserved.
// </copyright>
//------------------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Linq;

namespace Tunynet.Common
{
    /// <summary>
    /// 触屏版详情URL获取器工厂
    /// </summary>
    public static class TouchScreenUrlGetterFactory
    {
        /// <summary>
        /// 依据tenantTypeId获取ITouchScreenUrlGetter
        /// </summary>
        /// <returns></returns>
        public static ITouchScreenUrlGetter Get(string tenantTypeId)
        {
            return DIContainer.Resolve<IEnumerable<ITouchScreenUrlGetter>>().Where(g => g.TenantTypeId.Equals(tenantTypeId, StringComparison.InvariantCultureIgnoreCase)).FirstOrDefault();
        }
    }
}