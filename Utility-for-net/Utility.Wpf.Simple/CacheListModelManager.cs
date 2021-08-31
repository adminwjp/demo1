using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using Utility.Wpf.Entries;

namespace Utility.Wpf
{
    /// <summary>
    /// 缓存 数据
    /// </summary>
    public class CacheListModelManager
    {
        /// <summary>
        /// 存储每个 列表(多个表格组合) 配置信息
        /// </summary>
        public static readonly IDictionary<string, MuilDataEntry> CacheFlagMuilDataEntry = new ConcurrentDictionary<string, MuilDataEntry>();
        /// <summary>
        /// 存储每个 列表 配置信息
        /// </summary>
        public static readonly IDictionary<string, ListEntry> CacheFlagListEntry = new ConcurrentDictionary<string, ListEntry>();
        /// <summary>
        /// 存储每个 列表 操作方法
        /// </summary>
        public static readonly IDictionary<string, MethodTemplateEntry> CacheFlagMethod = new ConcurrentDictionary<string, MethodTemplateEntry>();
        /// <summary>
        /// 存储 combox 等 数据源操作
        /// </summary>
        public static readonly IDictionary<string, Func<bool,object>> CacheDataSource = new ConcurrentDictionary<string, Func<bool, object>>();//缓存 数据源 存储
        /// <summary>
        /// 获取 数据源
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static Func<bool, object> GetData(string key)
        {
            return CacheDataSource.ContainsKey(key)? CacheDataSource[key]:null;
        }
    }

    
}
