using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using Utility.Menus;
using Utility.Wpf.Components;

namespace Utility.Wpf.Ctrls
{
    /// <summary>
    ///用户控件 组装 menu treeview tabcontrol 组件 
    /// </summary>
    public  class HomeCtrl : UserControl
    {
        /// <summary>
        /// 组装 menu treeview tabcontrol 组件 
        /// </summary>
        public HomeComponent HomeComponent=new HomeComponent() { };
        /// <summary>
        /// 
        /// </summary>
        public HomeCtrl()
        {
            HomeComponent.ShowBar = ShowBar;
            Init(); //默认使用代码 生成 控件
        }
        void ShowBar()
        {
            this.Dispatcher.BeginInvoke(new Action(() => {
                var msg = $"当前时间为:{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")}";
                HomeComponent._show.Content = msg;
            }));
        }
        /// <summary>
        /// 默认使用代码 生成 控件,加载 数据 绑定控件
        /// </summary>
        /// <param name="xml"></param>
        public HomeCtrl(string xml) : this()
        {
            HomeComponent.LoadXml(xml);
            LoadData();
        }
        /// <summary>
        ///  默认使用代码 生成 控件,加载 数据 绑定控件
        /// </summary>
        /// <param name="menus"></param>
        public HomeCtrl(List<MenuEntry> menus) : this()
        {
            HomeComponent.LoadData(menus);
            LoadData();
        }
        private bool _isShow;
        /// <summary>
        /// 显示时间
        /// </summary>
        public bool IsShow { get { return _isShow; } set { if (_isShow != value) { HomeComponent.IsShow=value; _isShow = value; } } }
        /// <summary>
        ///TabControl 控件
        /// </summary> 
        public TabControl TabControl { get; set; }
        /// <summary>
        /// 加载 数据 绑定控件
        /// </summary>
        public void LoadData()
        {
            HomeComponent.Initial(HomeComponent.H_Menu, HomeComponent.H_TreeView);
            TabControl = HomeComponent.H_TabControl;
        }
        /// <summary>
        /// 布局 绑定
        /// </summary>
        private void Init()
        {
            this.Content = HomeComponent.H_DockPanel;
            HomeComponent.Init();
           // HomeComponent.H_TabControl.ItemTemplate =(DataTemplate) this.Resources["D1"];
        }
        /// <summary>
        /// 加载 资源 文件
        /// </summary>
        protected virtual void LoadResource()
        {
            DataTemplate dt = new DataTemplate();
            this.Resources["D1"] = dt;
            /**
             <Window.Resources>
                <DataTemplate x:Key="D1">
                    <Grid>
                        <TextBlock Text="{Binding Header}"/>
                        <Grid Height="20"  Width="20" Background="Transparent" Tag="{Binding Header}" MouseLeftButtonUp="Grid_MouseLeftButtonUp" Margin="70,0,0,0">
                            <Path   Data="M307,28.093333 L52.333333,250.76" Fill="Red" Stretch="Fill"   Stroke="#FFEA1717" HorizontalAlignment="Right" />
                            <Path   Data="M56.666667,53 L352,265.66667" Fill="Red"  Stretch="Fill"   Stroke="#FFEA1717" HorizontalAlignment="Right" />
                        </Grid>
                    </Grid>
                </DataTemplate>
            </Window.Resources>
             */

            FrameworkElementFactory fef = new FrameworkElementFactory(typeof(CloseCtr));
           // Binding binding = new Binding("MarketIndicator");
            dt.VisualTree = fef;
        }
        private class CloseCtr : UserControl
        {
            protected CloseComponent CloseCommpenont = new CloseComponent();
            public CloseCtr()
            {
                this.Content = CloseCommpenont.GridLayout;
            }
        }

    }
}
