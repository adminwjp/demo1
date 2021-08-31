using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using Utility.Menus;
using Utility.Wpf.Components;

namespace Utility.Wpf.Pages
{
    /// <summary>
    /// home page
    /// </summary>
    public  class HomePage : System.Windows.Controls.Page
    {
        /// <summary>
        /// home  组件
        /// </summary>
        public  HomeComponent HomeComponent=new HomeComponent();

        /// <summary>
        ///  home page
        /// </summary>
        public HomePage()
        {
            Init(); 
        }

        /// <summary>
        ///  home page 初始化 数据
        /// </summary>
        /// <param name="xml"></param>
        public HomePage(string xml) : this()
        {
            HomeComponent.LoadXml(xml);
            HomeComponent.LoadData(HomeComponent.Menus);
            HomeComponent.IsShow = true;
        }

        /// <summary>
        /// home page 初始化 数据
        /// </summary>
        /// <param name="menus"></param>
        public HomePage(List<MenuEntry> menus) : this()
        {
            HomeComponent.LoadData(menus);
            LoadData();
            HomeComponent.IsShow = true;
        }

        /// <summary>
        /// TabControl
        /// </summary>
        public TabControl TabControl { get; set; }

        /// <summary>
        /// 加载 menu treeview 数据
        /// </summary>
        public void LoadData()
        {
            HomeComponent.Initial(HomeComponent.H_Menu, HomeComponent.H_TreeView);
            TabControl = HomeComponent.H_TabControl;
        }

        /// <summary>
        /// 初始化 布局
        /// </summary>
        private void Init()
        {
            this.Content = HomeComponent.H_DockPanel;
            HomeComponent.Init();
            HomeComponent.H_TabControl.ItemTemplate =(DataTemplate) this.Resources["D1"];
        }


        /// <summary>
        /// 加载 资源 文件
        /// </summary>
        protected virtual void LoadResource()
        {
            DataTemplate dt = new DataTemplate();
            this.Resources["D1"] = dt;

            FrameworkElementFactory fef = new FrameworkElementFactory(typeof(ClosePage));
           // Binding binding = new Binding("MarketIndicator");
            dt.VisualTree = fef;
        }
        private class ClosePage : System.Windows.Controls.Page
        {
            protected CloseComponent CloseCommpenont = new CloseComponent();
            public ClosePage()
            {
                this.Content = CloseCommpenont.GridLayout;
            }
        }

    }
}
