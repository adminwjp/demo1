//------------------------------------------------------------------------------
// <copyright company="Tunynet">
//     Copyright (c) Tunynet Inc.  All rights reserved.
// </copyright>
//------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using Tunynet.Common.Configuration;
using Tunynet.Settings;
using Tunynet.Utilities;

namespace Tunynet.Common
{
    /// <summary>
    /// 正则表达式工具类
    /// </summary>
    public class RegexUtility
    {
        /// <summary>
        /// 手机正则表达式
        /// </summary>
        /// <returns></returns>
        public static Regex Mobile()
        {
            return new Regex("^1[3-9]\\d{9}$");
        }

        /// <summary>
        /// 邮箱正则表达式
        /// </summary>
        /// <returns></returns>
        public static Regex Email()
        {
            return new Regex("^([a-zA-Z0-9_.-]+)@([0-9A-Za-z.-]+).([a-zA-Z.]{2,6})$");
        }

        /// <summary>
        /// 获取一段html代码中某种标签的某种属性的值
        /// </summary>
        /// <param name="html">需要解析的html</param>
        /// <param name="tagName">标签名</param>
        /// <param name="attrName">属性名</param>
        /// <remarks> 例如 获取一段html代码中所有img标签的src属性</remarks>
        /// <returns></returns>
        public static string[] GetAttributeValuesFromHtml(string html, string tagName, string attrName)
        {
            Regex regImg = new Regex($@"<{tagName}\b[^<>]*?\b{attrName}[\s\t\r\n]*=[\s\t\r\n]*[""']?[\s\t\r\n]*(?<group>[^\s\t\r\n""'<>]*)[^<>]*?/?[\s\t\r\n]*>", RegexOptions.IgnoreCase);

            // 搜索匹配的字符串   
            MatchCollection matches = regImg.Matches(html);
            int i = 0;
            string[] sUrlList = new string[matches.Count];

            // 取得匹配项列表   
            foreach (Match match in matches)
                sUrlList[i++] = match.Groups["group"].Value;

            return sUrlList;
        }

    }
}