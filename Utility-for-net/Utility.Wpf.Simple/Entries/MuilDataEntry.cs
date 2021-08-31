using System;
using System.Collections.Generic;
using System.Text;

namespace Utility.Wpf.Entries
{
    /// <summary>
    /// 查询表单 和 列表 组合
    /// </summary>
    public class MuilDataEntry
    {
        /// <summary>
        /// 标识
        /// </summary>
        public string Flag { get; set; }
        /// <summary>
        /// 标题
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// 布局 方式 暂时 未实现
        /// </summary>
        public int Layout { get; set; }

        /// <summary>
        ///  查询表单 和 列表 组合
        /// </summary>
        public List<DataEntry> Data { get; set; }
    }
}
