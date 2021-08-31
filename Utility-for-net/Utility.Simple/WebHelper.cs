using System;
using System.Collections.Generic;
using System.Text;
using System.Web;

namespace Utility
{
    /// <summary>
    /// web 扩展 类
    /// </summary>
    public static class WebExtensions
    {
        /// <summary>
        ///? 1=1 nbsp; 2=2
        /// </summary>
        /// <param name="queryString"></param>
        /// <returns></returns>
        public static Dictionary<string, string> GetQueryString(this string queryString)
        {
            return WebHelper.GetQueryString(queryString);
        }


        /// <summary>
        /// {1:1,2:2 } 1=1 nbsp; 2=2
        /// </summary>
        /// <param name="queryStrings"></param>
        /// <returns></returns>
        public static string GetQueryString(this Dictionary<string, string> queryStrings)
        {
            return WebHelper.GetQueryString(queryStrings);
        }
    }

    /// <summary>
    /// web 帮助 类
    /// </summary>
    public partial class WebHelper
    {

        /// <summary>
        ///? 1=1 nbsp; 2=2
        /// </summary>
        /// <param name="queryString"></param>
        /// <returns></returns>
        public static Dictionary<string,string> GetQueryString(string queryString)
        {
            var strs = queryString.Split('&');
            if (strs != null && strs.Length > 0)
            {
                Dictionary<string, string> queryStrings = new Dictionary<string, string>();
                foreach (var item in strs)
                {
                    var vals = item.Split('=');
                    if (vals.Length == 2)
                    {
                        var key = HttpUtility.UrlDecode(vals[0]);
                        var val = HttpUtility.UrlDecode(vals[1]);
                        if (!queryStrings.ContainsKey(key))
                        {
                            queryStrings.Add(key, val);
                        }
                    }
                }
                return queryStrings;
            }
            return null;
        }


        /// <summary>
        /// {1:1,2:2 } 1=1 nbsp; 2=2
        /// </summary>
        /// <param name="queryStrings"></param>
        /// <returns></returns>
        public static string GetQueryString(Dictionary<string, string> queryStrings)
        {
            var queryString = string.Empty;
            if (queryString != null)
            {
                var keys = new string[queryStrings.Keys.Count];
                queryStrings.Keys.CopyTo(keys,0);
                var vals = new string[queryStrings.Values.Count];
                queryStrings.Values.CopyTo(vals, 0);
                for (int i = 0; i < queryStrings.Count; i++)
                {
                    var key = HttpUtility.UrlEncode(keys[0]);
                    var val = HttpUtility.UrlEncode(vals[1]);
                    if(i>0&&i< queryStrings.Count - 1)
                    {
                        queryString += "&";
                    }
                    queryString += $"{key}={val}";
                }
            }
            return queryString;
        }
    }
}
