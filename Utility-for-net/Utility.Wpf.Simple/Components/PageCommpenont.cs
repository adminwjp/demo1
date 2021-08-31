using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

namespace Utility.Wpf.Components
{
    /// <summary>
    /// 分页 组件
    /// </summary>
    public class PageCommpenont
    {
        ComboBox pageSize;//每页记录
        TextBox page;//页数
        Label show;//显示信息
        /// <summary>
        /// 每页记录
        /// </summary>
        public List<int> PageSizes { get; set; } = new List<int> { 10, 20, 30, 40, 50 };
        /// <summary>
        /// 每页记录 改变 事件
        /// </summary>
        public Action<int> PageSizeChangeEvent { get; set; }
        /// <summary>
        /// 页面 跳转 事件
        /// </summary>
        public Action<int> PageRedirctEvent { get; set; }
        /// <summary>
        /// 分页 组件
        /// </summary>
        /// <returns></returns>
        public Grid GetPageShow()
        {
            Grid grid = new Grid() { Height = 50 };
            grid.ColumnDefinitions.Add(new ColumnDefinition());
            grid.ColumnDefinitions.Add(new ColumnDefinition());
            DockPanel.SetDock(grid, Dock.Bottom);
            Thickness margin = new Thickness(10, 0, 0, 0);//外 间距
            show = new Label() { Content = "共有17条数据,当前页10条数据", HorizontalAlignment = HorizontalAlignment.Left, VerticalAlignment = VerticalAlignment.Center };
            grid.Children.Add(show);
            Grid.SetColumn(show, 0);
            WrapPanel wrapPanel = new WrapPanel() { HorizontalAlignment = HorizontalAlignment.Right, VerticalAlignment = VerticalAlignment.Center, Margin = new Thickness(0, 0, 20, 0) };
            grid.Children.Add(wrapPanel);
            Grid.SetColumn(wrapPanel, 1);
            Label first = new Label() { HorizontalAlignment = HorizontalAlignment.Center, VerticalAlignment = VerticalAlignment.Center, Content = "首页" };
            wrapPanel.Children.Add(first);
            Label pre = new Label() { HorizontalAlignment = HorizontalAlignment.Center, VerticalAlignment = VerticalAlignment.Center, Margin = margin, Content = "上一页" };
            wrapPanel.Children.Add(pre);
            Label one = new Label() { HorizontalAlignment = HorizontalAlignment.Center, VerticalAlignment = VerticalAlignment.Center, Margin = margin, Content = "1" };
            wrapPanel.Children.Add(one);
            Label two = new Label() { HorizontalAlignment = HorizontalAlignment.Center, VerticalAlignment = VerticalAlignment.Center, Margin = margin, Content = "2" };
            wrapPanel.Children.Add(two);
            Label three = new Label() { HorizontalAlignment = HorizontalAlignment.Center, VerticalAlignment = VerticalAlignment.Center, Margin = margin, Content = "....." };
            wrapPanel.Children.Add(three);
            Label four = new Label() { HorizontalAlignment = HorizontalAlignment.Center, VerticalAlignment = VerticalAlignment.Center, Margin = margin, Content = "10" };
            wrapPanel.Children.Add(four);
            Label next = new Label() { HorizontalAlignment = HorizontalAlignment.Center, VerticalAlignment = VerticalAlignment.Center, Margin = margin, Content = "下一页" };
            wrapPanel.Children.Add(next);
            Label last = new Label() { HorizontalAlignment = HorizontalAlignment.Center, VerticalAlignment = VerticalAlignment.Center, Margin = margin, Content = "末页" };
            wrapPanel.Children.Add(last);
            pageSize = new ComboBox() { VerticalContentAlignment = VerticalAlignment.Center, HorizontalContentAlignment = HorizontalAlignment.Center, HorizontalAlignment = HorizontalAlignment.Center, VerticalAlignment = VerticalAlignment.Center, Margin = margin, Width = 50, Height = 30 };
            wrapPanel.Children.Add(pageSize);
            page = new TextBox() { Width = 30, Height = 30, VerticalContentAlignment = VerticalAlignment.Center, HorizontalContentAlignment = HorizontalAlignment.Center, HorizontalAlignment = HorizontalAlignment.Center, VerticalAlignment = VerticalAlignment.Center, Margin = margin, Text = "10" };
            wrapPanel.Children.Add(page);
            Button redirect = new Button() { Width = 40, Height = 30, HorizontalAlignment = HorizontalAlignment.Center, VerticalAlignment = VerticalAlignment.Center, Margin = margin, Content = "跳转" };
            wrapPanel.Children.Add(redirect);

            pageSize.ItemsSource = PageSizes;
            pageSize.SelectedIndex = 0;

            //分页记录改变事件
            pageSize.SelectionChanged -= PageSize_SelectionChanged;
            pageSize.SelectionChanged += PageSize_SelectionChanged;

            //页数跳转事件
            redirect.Click -= Redirect_Click;
            redirect.Click += Redirect_Click;

            return grid;

        }

        private void Redirect_Click(object sender, RoutedEventArgs e)
        {
            PageRedirctEvent?.Invoke(Convert.ToInt32(page.Text));
        }

        private void PageSize_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            PageSizeChangeEvent?.Invoke(Convert.ToInt32(pageSize.SelectedValue));
        }
    }
}
