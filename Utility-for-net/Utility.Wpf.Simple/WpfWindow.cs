using System.ComponentModel;
using System.Windows;

namespace Utility.Wpf
{
    /// <summary>
    /// ͳһ ���� �ر� ʵ�� ����
    /// </summary>
    public  class WpfWindow:Window
    {
        /// <summary>
        /// �ر� ʵ�� ����
        /// </summary>
        /// <param name="e"></param>
        protected override void OnClosing(CancelEventArgs e)
        {
            e.Cancel = true;
            this.Hide();
        }
    }
}
