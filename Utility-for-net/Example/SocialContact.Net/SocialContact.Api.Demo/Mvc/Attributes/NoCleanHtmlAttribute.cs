//------------------------------------------------------------------------------
// <copyright company="Tunynet">
//     Copyright (c) Tunynet Inc.  All rights reserved.
// </copyright>
//------------------------------------------------------------------------------

using System;

namespace Tunynet.Common
{
    /// <summary>
    /// 表示不清理Html
    /// </summary>
    [AttributeUsage(AttributeTargets.Property, Inherited = true, AllowMultiple = true)]
    public class NoCleanHtmlAttribute : Attribute
    {
    }
}