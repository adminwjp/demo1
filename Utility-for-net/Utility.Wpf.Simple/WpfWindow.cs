using System.ComponentModel;
using System.Windows;

namespace Utility.Wpf
{
    /// <summary>
    /// 统一 窗口 关闭 实现 隐藏
    /// </summary>
    public  class WpfWindow:Window
    {
        /// <summary>
        /// 关闭 实现 隐藏
        /// </summary>
        /// <param name="e"></param>
        protected override void OnClosing(CancelEventArgs e)
        {
            e.Cancel = true;
            this.Hide();
        }
    }
}
