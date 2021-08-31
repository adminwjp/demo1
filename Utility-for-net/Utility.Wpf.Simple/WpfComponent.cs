using System;
using System.Collections.Generic;
using System.Windows.Controls;
using Utility.Menus;

namespace Utility.Wpf
{
    /// <summary>
    /// wpf 其他组件
    /// </summary>
    public class WpfComponent
    {
        public static TabControl T_TabControl { get; set; }
        /// <summary>
        /// 菜单, 绑定 Menu 和 TreeView 控件数据
        /// </summary>
        /// <param name="menu"></param>
        /// <param name="treeView"></param>
        public static void Initial(System.Windows.Controls.Menu menu, TreeView treeView, List<MenuEntry> menus)
        {
            foreach (var item in menus)
            {
                var men = new MenuItem();
                menu.Items.Add(men);
                Set(men, item);

                var tree = new TreeViewItem();
                treeView.Items.Add(tree);
                Set(tree, item);
            }
        }
       
        /// <summary>
        /// 绑定 Menu 控件数据 
        /// </summary>
        /// <param name="menuItem"></param>
        /// <param name="menuEntry"></param>
        private static void Set(MenuItem menuItem, MenuEntry menuEntry)
        {
            menuItem.Header = menuEntry.Header;
            if (menuEntry.TypeName != null)
            {
                menuItem.Click -= MenuItem_Click;
                menuItem.Click += MenuItem_Click;
                menuItem.Tag = menuEntry;
            }
            if (menuEntry.Children != null && menuEntry.Children.Count > 0)
            {
                foreach (var item in menuEntry.Children)
                {
                    var chil = new MenuItem() { Header = item.Header };
                    if (item.TypeName != null)
                    {
                        menuItem.Click -= MenuItem_Click;
                        chil.Click += MenuItem_Click;
                        chil.Tag = item;
                    }
                    menuItem.Items.Add(chil);
                    Set(chil, item);
                }
            }
        }

        /// <summary>
        /// Menu 控件 点击 触发 事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private static void MenuItem_Click(object sender, System.Windows.RoutedEventArgs e)
        {

            if (sender is MenuItem menuItem)
            {
                TabClick(menuItem.Tag as MenuEntry, T_TabControl);
            }
        }
        /// <summary>
        /// Menu 和 TreeView 控件 点击 触发 事件
        /// </summary>
        /// <param name="menu"></param>
        protected static  void TabClick(MenuEntry menu,TabControl tabControl)
        {
            if (string.IsNullOrEmpty(menu.TypeName))
            {
                return;
            }
            var exists = false;
            int i = 0;
            foreach (var item in tabControl.Items)
            {
                if (item is TabItem tab)
                {
                    if (/*tab.Content.GetType().ToString()== menu.TypeName&&*/ tab.Tag is MenuEntry menuEntry && menuEntry == menu)
                    {
                        //this.TabControl.SelectedItem = item;
                        tabControl.SelectedIndex = i;
                        exists = true;
                        break;
                    }
                }
                i++;
            }
            if (exists)
            {
                tabControl.SelectedIndex = i;
            }
            else
            {
                //var tabItem = new Tool.Ctrls.UCTabItemWithClose();//TabItem();
                var tabItem = new TabItem();
                //var wrapPanel = new WrapPanel();
                //wrapPanel.VerticalAlignment = VerticalAlignment.Center;
                //var label = new Label();
                //label.Content = title;
                //label.Height = label.Height;
                //wrapPanel.Children.Add(label);

                //Button button = new Button();
                //var img = new Image();
                //BitmapImage source = Utility.WpfUtils.ByteArrayToBitmapImage(Tool.Wpf.Properties.Resources.close);
                //img.Source = source;
                // ((IUriContext)img).BaseUri = new Uri(Environment.CurrentDirectory + "/imgs/close.jpg"); 显示 不出图片
                //img.Source = new BitmapImage(new Uri(Environment.CurrentDirectory+"/imgs/close.jpg"));
                //img.Height = 20;
                //img.Width = 20;
                //img.VerticalAlignment = VerticalAlignment.Center;
                //img.Stretch = Stretch.Fill;
                //img.Margin = new Thickness(10, 0, 0, 0);
                //wrapPanel.Children.Add(img);
                //tabItem.Header = wrapPanel;

                //button.Content = wrapPanel;
                //tabItem.Header = button;
                tabItem.Tag = menu;
                tabItem.Header = menu.Header;
                tabItem.Height = 28;
                if (menu.Arags != null && menu.Arags.Count > 0)
                {
                    tabItem.Content = Activator.CreateInstance(Type.GetType(menu.TypeName), menu.ToConstractorArags());
                }
                else if (!string.IsNullOrEmpty(menu.Flag))
                {
                    var obj = CacheListModelManager.CacheFlagMuilDataEntry.ContainsKey(menu.Flag) ? CacheListModelManager.CacheFlagMuilDataEntry[menu.Flag] : null;
                    var sObj = obj == null ? (CacheListModelManager.CacheFlagListEntry.ContainsKey(menu.Flag) ? CacheListModelManager.CacheFlagListEntry[menu.Flag] : null) : null;
                    var method = CacheListModelManager.CacheFlagMethod.ContainsKey(menu.Flag) ? CacheListModelManager.CacheFlagMethod[menu.Flag] : null;
                    object[] args = new object[2] { obj, method };
                    if (obj == null)
                    {
                        args[0] = sObj;
                    }
                    var type = Type.GetType(menu.TypeName);//注意 一旦 改变 了程序集 最好 用 完全 限定名  不然 找不到
                    tabItem.Content = Activator.CreateInstance(type, args);
                }
                else
                {
                    tabItem.Content = Activator.CreateInstance(Type.GetType(menu.TypeName));
                }
                var index = tabControl.Items.Add(tabItem);
                tabControl.SelectedIndex = index;
            }
        }

        /// <summary>
        /// 绑定  TreeView 控件数据
        /// </summary>
        /// <param name="treeViewItem"></param>
        /// <param name="menuEntry"></param>
        private static void Set(TreeViewItem treeViewItem, MenuEntry menuEntry)
        {
            treeViewItem.Header = menuEntry.Header;
            if (menuEntry.TypeName != null)
            {
                treeViewItem.MouseDoubleClick -= TreeViewItem_MouseDoubleClick;
                treeViewItem.MouseDoubleClick += TreeViewItem_MouseDoubleClick;
                treeViewItem.Tag = menuEntry;
            }
            if (menuEntry.Children != null && menuEntry.Children.Count > 0)
            {
                foreach (var item in menuEntry.Children)
                {
                    var chil = new TreeViewItem() { Header = item.Header };
                    if (item.TypeName != null)
                    {
                        chil.MouseDoubleClick -= TreeViewItem_MouseDoubleClick;
                        chil.MouseDoubleClick += TreeViewItem_MouseDoubleClick;
                        chil.Tag = menuEntry;
                    }
                    treeViewItem.Items.Add(chil);
                    Set(chil, item);
                }
            }
        }

        /// <summary>
        ///  TreeView 控件 点击 触发 事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>

        private static void TreeViewItem_MouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (sender is TreeViewItem treeViewItem)
            {
                TabClick(treeViewItem.Tag as MenuEntry,T_TabControl);
            }
        }
    }
}
