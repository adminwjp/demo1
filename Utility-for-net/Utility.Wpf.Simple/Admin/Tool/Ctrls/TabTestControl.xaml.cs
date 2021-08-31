using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Tool.Ctrls
{
    /// <summary>
    /// TabTestControl.xaml 的交互逻辑
    /// </summary>
    public partial class TabTestControl : UserControl
    {
        public TabTestControl()
        {
            InitializeComponent();
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {

            UCTabItemWithClose item = new UCTabItemWithClose();
            item.Header = string.Format("Header{0}", tab_Main.Items.Count);
            item.ToolTip = string.Format("Header{0}", tab_Main.Items.Count);
            item.Margin = new Thickness(0, 0, 1, 0);
            item.Height = 28;
            Label lbl = new Label() { Content = string.Format("Label{0}", tab_Main.Items.Count) };
            Button btn = new Button() { Width = 132, Height = 32, Content = string.Format("Button{0}", tab_Main.Items.Count) };
            WrapPanel sPanel = new WrapPanel();
            sPanel.Children.Add(lbl);
            sPanel.Children.Add(btn);
            item.Content = sPanel;
            tab_Main.Items.Add(item);
        }
    }
}
