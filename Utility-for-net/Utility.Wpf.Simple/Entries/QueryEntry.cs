using System;
using System.Collections.Generic;
using System.Text;

namespace Utility.Wpf.Entries
{
    /// <summary>
    /// 查询 表单
    /// </summary>
    public class QueryEntry
    {
        /// <summary>
        /// 标题
        /// </summary>
        public string Title { get; set; }
        /// <summary>
        /// 查询列
        /// </summary>
        public List<ColumnEntry> Columns { get; set; }
        /// <summary>
        /// 是否 使用 expander
        /// </summary>
        public bool Group { get; set; } = false;
    }
}
