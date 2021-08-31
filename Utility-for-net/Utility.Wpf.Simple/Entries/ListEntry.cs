using System.Collections.Generic;
using Utility.Wpf.ViewModels;

namespace Utility.Wpf.Entries
{
    /// <summary>
    /// 表格
    /// </summary>
    public class ListEntry : AbstractNotifyPropertyChanged
    {
        string _title;
        private string id;
        /// <summary>
        /// 表格标题
        /// </summary>
        public string Title { get { return this._title; } set { Set(ref _title, value, "Title"); } }
        /// <summary>
        /// 表格 id
        /// </summary>
        public string Id { get => id??"Id"; set => id = value; }
        /// <summary>
        /// dialog width
        /// </summary>
        public int Width { get; set; }
       /// <summary>
       /// dialog heigh
       /// </summary>
        public int Height { get; set; }
        /// <summary>
        /// dialog 列 datagrid 列
        /// </summary>
        public List<ColumnEntry> Columns { get; set; }
        /// <summary>
        /// 表格 标识 对应 说明
        /// </summary>
        public int Flag { get; set; }
        /// <summary>
        /// 是否 使用 expander
        /// </summary>
        public bool Group { get; set; } = false;

    }
}
