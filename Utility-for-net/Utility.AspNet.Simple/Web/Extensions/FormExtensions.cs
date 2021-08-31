#if NETCOREAPP2_0 || NETCOREAPP2_1 || NETCOREAPP2_2 || NETCOREAPP3_0 || NETCOREAPP3_1 || NET5_0 || NET6_0 || NETSTANDARD2_0 || NETSTANDARD2_

using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utility.Web.Extensions
{
    /// <summary>
    /// 表单  操作 类
    /// </summary>
    public static class FormExtensions
    {
        /// <summary>
        /// 获取 值 手动 转换
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="form"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public static T Get<T>(this IFormCollection form, string key)
        {
            return Get(form, key, default(T));
        }

        /// <summary>
        /// 获取 值 手动 转换
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="form"></param>
        /// <param name="key"></param>
        /// <param name="val"></param>
        /// <returns></returns>
        public static T Get<T>(this IFormCollection form, string key, T val)
        {
            try
            {
                return (T)Convert.ChangeType(form[key], typeof(T));
            }
            catch
            {
                return val;
            }
        }
    }
}
#endif