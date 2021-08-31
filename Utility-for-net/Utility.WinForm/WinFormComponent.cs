using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Tool;
using Utility.Menus;

namespace Utility.WinForm
{
    public class WinFormComponent
    {
        public static MenuStrip Menu(List<MenuEntry> menuEntries)
        {
            MenuStrip menuStrip=new MenuStrip();//顶部菜单
            Menu(menuStrip,menuEntries);
            return menuStrip;
        }

        public static MenuStrip Menu(MenuStrip menuStrip,List<MenuEntry> menuEntries)
        {
            CursionMenu(menuStrip, null, menuEntries);
            return menuStrip;
        }

        public static void CursionMenu(MenuStrip menuStrip, ToolStripMenuItem parent, List<MenuEntry> menuEntries)
        {
            foreach (MenuEntry menuEntry in menuEntries)
            {
                ToolStripMenuItem toolStripMenuItem = new ToolStripMenuItem() { Text = menuEntry.Header };
                if (!string.IsNullOrEmpty(menuEntry.TypeName))
                {
                    toolStripMenuItem.Tag = menuEntry;
                    toolStripMenuItem.Click -= ToolStripMenuItem_Click;
                    toolStripMenuItem.Click += ToolStripMenuItem_Click;
                }
           
                if (parent == null)
                {
                    menuStrip.Items.Add(toolStripMenuItem);
                }
                else
                {
                    parent.DropDownItems.Add(toolStripMenuItem);
                }
                if (menuEntry.Children.Count > 0)
                {
                    CursionMenu(menuStrip, toolStripMenuItem, menuEntry.Children);
                }
            }
        }

        private static void ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if(sender is ToolStripMenuItem toolStripMenuItem)
            {
               if(toolStripMenuItem.Tag is MenuEntry menuEntry)
                {
                    menuEntry.ClickEvent?.Invoke(menuEntry);
                }
            }
        }

       public static  bool Exists<T>(TabControl tabControl)
        {
            int i = 0;
            foreach (TabPage page in tabControl.TabPages)
            {
                foreach (var control in page.Controls)
                {
                    if (control is T)
                    {
                        tabControl.SelectedIndex = i;
                        return true;
                    }
                }
                i++;
            }
            return false;
        }
       public static  void CreateTab<T>(TabControl tabControl,string text, Action<T> action = null) where T : UserControl, new()
        {
            var exists = Exists<T>(tabControl);
            if (exists)
            {
                return;
            }
            var tabPage = new TabPage();
            tabControl.TabPages.Add(tabPage);
            tabPage.Text = text;
            var obj = new T();
            tabPage.Controls.Add(obj);
            tabControl.SelectedIndex = tabControl.TabPages.Count - 1;
            obj.Width = tabPage.Width;
            obj.Height = tabPage.Height - 50;
            action?.Invoke(obj);
            tabPage.Resize += (object sender1, EventArgs e1) => {
                WinFormHelper.Set(tabPage, () => {
                    obj.Width = tabPage.Width;
                    obj.Height = tabPage.Height - 50;
                });
            };
        }
    }
}
