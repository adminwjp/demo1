#if !(NET451|| NET452|| NETCOREAPP3_0 || NETCOREAPP3_1 ||NET5_0)
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Utility.Wpf
{
    public class NotifyIconHelper
    {
        public static NotifyIcon NotifyIcon = GetNotifyIcon("程序", "提示", "favicon.ico", new ContextMenu (GetMenuItem()));
        public static NotifyIcon GetNotifyIcon(string title,string tip,string icon, ContextMenu  contextMenu)
        {
            NotifyIcon notifyIcon = new NotifyIcon();
            notifyIcon.BalloonTipText = title;
            notifyIcon.Text = tip;
            notifyIcon.Icon = string.IsNullOrEmpty(icon)?null: new System.Drawing.Icon(icon);
            notifyIcon.Visible = true;
            notifyIcon.ContextMenu = contextMenu;
            notifyIcon.ShowBalloonTip(2000);
            return notifyIcon;
        }
        public static MenuItem[] GetMenuItem()
        {
            MenuItem show = new MenuItem() { Text="显示"};
            MenuItem hide = new MenuItem() { Text = "隐藏" };
            MenuItem exit = new MenuItem() { Text = "退出" };
            return  new MenuItem[]{ show,hide, exit };
        }
    }
}
#endif
