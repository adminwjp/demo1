//------------------------------------------------------------------------------
// <copyright company="Tunynet">
//     Copyright (c) Tunynet Inc.  All rights reserved.
// </copyright>
//------------------------------------------------------------------------------

using System;

namespace Tunynet.Common
{
    /// <summary>
    /// 表示不过滤敏感词
    /// </summary>
    [AttributeUsage(AttributeTargets.Property, Inherited = true, AllowMultiple = true)]
    public class NoFilterWordAttribute : Attribute
    {
    }
}