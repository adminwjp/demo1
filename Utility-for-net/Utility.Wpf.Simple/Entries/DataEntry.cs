using System;
using System.Collections.Generic;
using System.Text;

namespace Utility.Wpf.Entries
{
    /// <summary>
    /// 查询表单 和 列表 组合
    /// </summary>
    public class DataEntry
    { 
        /// <summary>
       ///  查询 表单 
       /// </summary>
        public QueryEntry Query { get; set; }

        /// <summary>
        ///  多 列表  暂时 只支持 一个
        /// </summary>
        public List<ListEntry> Lists { get; set; }

        /// <summary>
        /// 表单 默认 增删改查(Lists[0]) 或 添加
        /// </summary>
        public ListEntry Form { get; set; }

        /// <summary>
        /// 表单 编辑
        /// </summary>
        public ListEntry EditForm { get; set; }

        /// <summary>
        /// 表单  预览 或 删除
        /// </summary>
        public ListEntry PreviewForm { get; set; }

        /// <summary>
        /// 是否使用 expander
        /// </summary>
        public bool Group { get; set; } = false;
    }
}
