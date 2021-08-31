using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace Utility.Wpf.ViewModels
{
    /// <summary>
    /// 复选框 选中 项
    /// </summary>
    public interface IIsSelectedViewModel: INotifyPropertyChanged
    {

        /// <summary>
        /// 复选框 选中 项
        /// </summary>
        bool IsSelected { get; set; }
        ///// <summary>
        ///// 防止 双向绑定时改对象为null 参数 传递不过来
        ///// </summary>
        //void CreateByNullInstance();
        /// <summary>
        ///  复选框 选中或 取消  项 触发 全选 复选框 是否 选中
        /// </summary>
        event Action<IIsSelectedViewModel> AllSelectEvent;
    }
}
