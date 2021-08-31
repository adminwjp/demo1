using System;
using System.Collections.Generic;
using System.Timers;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Threading;
using System.Xml;
using Utility.Menus;

namespace Utility.Wpf.Components
{
    /// <summary>
    ///布局 组装 menu(头部) treeview(左边) tabcontrol(右边) 
    /// </summary>
    public  class HomeComponent
    {
        /// <summary>
        /// 定时器 刷新 时间
        /// </summary>
        protected System.Timers.Timer Timer;
        /// <summary>
        /// 布局 容器
        /// </summary>
        public readonly DockPanel H_DockPanel = new DockPanel();
        /// <summary>
        /// 布局: StackPanel 放 Label(显示 时间(底部左边) )
        /// </summary>
        public readonly StackPanel H_StackPanel = new StackPanel() { Visibility = Visibility.Collapsed ,   Orientation = Orientation.Horizontal, HorizontalAlignment = HorizontalAlignment.Left };
        /// <summary>
        /// Menu (头部)
        /// </summary>
        public  System.Windows.Controls.Menu H_Menu { get; set; } = new System.Windows.Controls.Menu();
        /// <summary>
        /// TreeView(左边)
        /// </summary>
        public  TreeView H_TreeView { get; set; } = new TreeView() { Width=150};
        /// <summary>
        /// TabControl(右边) 
        /// </summary>
        public  TabControl H_TabControl { get; set; } = new TabControl() { Tag = 100 };
        /// <summary>
        /// 显示 时间(底部左边) 
        /// </summary>
        internal readonly Label _show = new Label();
        /// <summary>
        /// 初始化 布局
        /// </summary>
        public void Init()
        {
            H_DockPanel.Children.Add(H_StackPanel);
            DockPanel.SetDock(H_StackPanel, Dock.Bottom);
            H_StackPanel.Children.Add(_show);

            H_DockPanel.Children.Add(H_Menu);
            DockPanel.SetDock(H_Menu, Dock.Top);

            H_DockPanel.Children.Add(H_TreeView);
            DockPanel.SetDock(H_TreeView, Dock.Left);

            H_DockPanel.Children.Add(H_TabControl);
        }


        private bool _isShow;
        /// <summary>
        /// 显示时间
        /// </summary>
        public bool IsShow { get { return _isShow; } set { if (_isShow != value) { Show(value); _isShow = value; } } }

        /// <summary>
        /// wpf 线程失效 未更新数据(只能 放在控件里 更新)
        /// </summary>
        public Action ShowBar { get; set; }
        /// <summary>
        /// 定时 刷新 时间
        /// </summary>
        /// <param name="bar"></param>
        public void Show(bool bar = false)
        {
            if (bar)
            {
                ShowBar();
                if(this.Timer==null)
                {
                    this.Timer = new System.Timers.Timer();
                    this.Timer.AutoReset = true;
                    this.Timer.Interval = 1000;
                    this.Timer.Elapsed += _timer_Elapsed;
                    this.Timer.Start();
                }

            }
            this.H_StackPanel.Visibility = bar ? Visibility.Visible : Visibility.Collapsed;
        }

        private void _timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            ShowBar();
        }
     
        /// <summary>
        /// 菜单, 绑定 Menu 和 TreeView 控件数据
        /// </summary>
        private readonly List<MenuEntry> _menus = new List<MenuEntry>();
        /// <summary>
        /// 菜单, 绑定 Menu 和 TreeView 控件数据
        /// </summary>
        public List<MenuEntry> Menus => this._menus;
        /// <summary>
        /// 菜单, 绑定 Menu 和 TreeView 控件数据
        /// </summary>
        /// <param name="menu"></param>
        /// <param name="treeView"></param>
        public void Initial(System.Windows.Controls.Menu menu, TreeView treeView)
        {
            WpfComponent.T_TabControl = H_TabControl;
            WpfComponent.Initial(menu,treeView, Menus);
        }
        /// <summary>
        /// 加载 菜单数据
        /// </summary>
        /// <param name="menus"></param>
        public void LoadData(List<MenuEntry> menus)
        {
            this._menus.AddRange(menus);
        }
        /// <summary>
        /// 加载 数据
        /// </summary>
        /// <param name="xml"></param>
        public void LoadXml(string xml)
        {
            MenuHelper.LoadXml(xml, _menus);
        }
    
    }

}
