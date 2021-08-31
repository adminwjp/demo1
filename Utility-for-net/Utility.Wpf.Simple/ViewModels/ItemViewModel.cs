using System;
using System.Collections.Generic;
using System.Text;

namespace Utility.Wpf.ViewModels
{
    /// <summary>
    /// 下拉框 数据源 格式
    /// </summary>
    public class ItemViewModel
    {
        /// <summary>
        /// 显示 属性名称
        /// </summary>
        public object Label { get; set; }
        /// <summary>
        /// 选中 值  属性名称 
        /// </summary>
        public object Value { get; set; }
    }
}
