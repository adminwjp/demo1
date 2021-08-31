using System;
using System.Timers;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using System.Windows.Threading;
using Tool.Ctrls;

namespace Tool
{
    public class MainFactoty
    {
        private readonly Timer _timer = new Timer();
        private bool _initial = false;
        public TabControl TabControl { get; set; }
        public void MainMenu(System.Windows.Controls.Menu menu)
        {
            MenuItem menuItem = new MenuItem();
            menu.Items.Add(menuItem);
            menuItem.Header = "工具";
            Set(menuItem, "Tab测试", MenuFlag.TabTest);
            Set(menuItem, "Net平台", MenuFlag.Paltform);
            Set(menuItem, "字符串转义", MenuFlag.StringEacse);
            Set(menuItem, "代码生成", MenuFlag.CodeGenator);
            Set(menuItem, "Docker工具", MenuFlag.Docker);
            Set(menuItem, "RabbitMQ工具", MenuFlag.RabbitMQ);
            Set(menuItem, "Kafka工具", MenuFlag.Kafka);
            Set(menuItem, "Elasticsearch工具", MenuFlag.Elasticsearch);
            Set(menuItem, "Zookeeper工具", MenuFlag.Zookeeper);

        }
        private void Set(MenuItem menuItem,string header, MenuFlag menuFlag = MenuFlag.Paltform)
        {
            MenuItem menuItem1 = new MenuItem();
            menuItem.Items.Add(menuItem1);
            menuItem1.Header = header;
            menuItem1.Tag = menuFlag;
            menuItem1.Click += MenuItem1_Click;
        }

        private void MenuItem1_Click(object sender, RoutedEventArgs e)
        {
            if(sender is MenuItem menuItem)
            {
                if(menuItem.Tag is MenuFlag menuFlag)
                {
                    Click(menuFlag, menuItem.Header.ToString());
                }
                else
                {
                    MessageBox.Show("sender is  MenuItem,get tag fail");
                }
            }
            else
            {
                MessageBox.Show("sender is not MenuItem");
            }
        }
        private void Click(MenuFlag menuFlag,string header)
        {
            switch (menuFlag)
            {
                case MenuFlag.Paltform:
                    TabClick<PlatCtrl>(this.TabControl, header);
                    break;
                case MenuFlag.StringEacse:
                    TabClick<StrCtrl>(this.TabControl, header);
                    break;
                case MenuFlag.Docker:
                    TabClick<DockerToolControl>(this.TabControl, header);
                    break;
                case MenuFlag.RabbitMQ:
                    break;
                case MenuFlag.Kafka:
                    break;
                case MenuFlag.Elasticsearch:
                    break;
                case MenuFlag.Zookeeper:
                    break;
                case MenuFlag.CodeGenator:
                    TabClick<CodeControl>(this.TabControl, header);
                    break;
                case MenuFlag.TabTest:
                    TabClick<TabTestControl>(this.TabControl, header);
                    break;
                default:
                    break;
            }
        }
        private void TabClick<T>(TabControl tabs,string title)
        {
            var exists = false;
            int i = 0;
            foreach (var item in tabs.Items)
            {
                if (item is UCTabItemWithClose tab)
                // if (item is TabItem tab)
                {
                    if (tab.Content is T obj)
                    {
                        exists = true;
                        break;
                    }
                }
                i++;
            }
            if (exists)
            {
                tabs.SelectedIndex = i;
            }
            else
            {
                var tabItem = new UCTabItemWithClose();
                //var tabItem = new TabItem();

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

                tabItem.Header = title;

                tabItem.Content = Activator.CreateInstance<T>();
                tabItem.ToolTip = title;
                tabItem.Margin = new Thickness(0, 0, 1, 0);
                tabItem.Height = 28;

                var index = tabs.Items.Add(tabItem);
                tabs.SelectedIndex = index;

            }
        }
        private void Set(TreeViewItem  treeViewItem, string header,MenuFlag menuFlag= MenuFlag.Paltform)
        {
            TreeViewItem treeViewItem1 = new TreeViewItem();
            treeViewItem.Items.Add(treeViewItem1);
            treeViewItem1.Header = header;
            treeViewItem1.Tag = menuFlag;
            treeViewItem1.MouseDoubleClick += TreeViewItem1_MouseDoubleClick; ;
        }

        private void TreeViewItem1_MouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (sender is TreeViewItem treeViewItem)
            {
                if (treeViewItem.Tag is MenuFlag menuFlag)
                {
                    Click(menuFlag, treeViewItem.Header.ToString());
                }
                else
                {
                    MessageBox.Show("sender is  TreeViewItem,get tag fail");
                }
            }
            else
            {
                MessageBox.Show("sender is not TreeViewItem");
            }
        }

        public void MainTree(TreeView treeView)
        {
            TreeViewItem  treeViewItem = new TreeViewItem();
            treeView.Items.Add(treeViewItem);
            treeViewItem.Header = "工具";


            Set(treeViewItem, "Net平台", MenuFlag.Paltform);
            Set(treeViewItem, "字符串转义", MenuFlag.StringEacse);
            Set(treeViewItem, "代码生成", MenuFlag.CodeGenator);
            Set(treeViewItem, "Docker工具", MenuFlag.Docker);
            Set(treeViewItem, "RabbitMQ工具", MenuFlag.RabbitMQ);
            Set(treeViewItem, "Kafka工具", MenuFlag.Kafka);
            Set(treeViewItem, "Elasticsearch工具", MenuFlag.Elasticsearch);
            Set(treeViewItem, "Zookeeper工具", MenuFlag.Zookeeper);
        }
        public void Start(Label show)
        {
            if (!this._initial)
            {
                InitTimer(show);
                this._initial = true;
            }
            this._timer.Start();
        }
        public void Stop()
        {
            this._timer.Stop();
        }
        private void InitTimer(Label show)
        {
            this._timer.Interval = 1000;
            this._timer.AutoReset = true;
            this._timer.Elapsed += (object sender, ElapsedEventArgs e) => {
                ModifyLabelShow(show);
            };
        }
        private void ModifyLabelShow(Label show)
        {
            //this.Dispatcher.Invoke(() => show.Content = $"当前时间为:{DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss")}") ;
            Dispatcher.CurrentDispatcher.BeginInvoke(new Action(() => { show.Content = $"当前时间为:{DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss")}"; }), null);
        }

    }
    public enum MenuFlag
    {
        Paltform,
        StringEacse,
        Docker,
        RabbitMQ,
        Kafka,
        Elasticsearch,
        Zookeeper,
        CodeGenator,
        TabTest,

    }
}
