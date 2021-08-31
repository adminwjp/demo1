using System.Collections.Generic;

namespace Utility.Wpf.Entries
{
    /// <summary>
    /// 列
    /// </summary>
    public class ColumnEntry
    {
        /// <summary>
        /// 绑定名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 标题
        /// </summary>
        public string Header { get; set; }
        /// <summary>
        /// 下拉框 显示 绑定名称
        /// </summary>
        public string DisplayMemberPath { get; set; }
        /// <summary>
        /// 下拉框 选中值 绑定值
        /// </summary>
        public string SelectedValuePath { get; set; }
        /// <summary>
        /// 是否非空
        /// </summary>
        public bool Required { get; set; }
        /// <summary>
        /// 格式化
        /// </summary>
        public string StringFormat { get; set; }
        /// <summary>
        /// 文本框 操作 方式 (隐藏 可读 编辑)
        /// </summary>
        public ColumnEditFlag Flag { get; set; }
        /// <summary>
        /// 最大长度
        /// </summary>
        public int MaxLength { get; set; }
        /// <summary>
        /// 文本框 类型 (文本框 下拉框 复选框 数字 框)
        /// </summary>
        public ColumnType ColumnType { get; set; }
        /// <summary>
        /// 下拉框 数据源
        /// </summary>
        public List<object> Items { get; set; }
        /// <summary>
        /// 下拉框 数组数据源
        /// </summary>
        public bool SingleItems { get; set; } //[1,2,3]
        /// <summary>
        /// 下拉框 数据源 key 用于获取或更新数据源
        /// </summary>
        public string Key { get; set; }//数据源 key
    }
}
