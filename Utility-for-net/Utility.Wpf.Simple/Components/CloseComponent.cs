using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace Utility.Wpf.Components
{
    /// <summary>
    ///  关闭按钮及文本(关闭+按钮图标) 
    /// </summary>
    public class CloseComponent
    {
        /// <summary>
        /// grid 布局 弹出框 关闭按钮及文本(关闭+按钮图标) 
        /// </summary>
        public readonly Grid GridLayout = new Grid();
        /// <summary>
        /// 关闭
        /// </summary>
        public CloseComponent()
        {
        
            TextBlock textBlock = new TextBlock() { };
            GridLayout.Children.Add(textBlock);
            BindHelper.SetBind("Text", textBlock, TextBlock.TextProperty);

            Grid grid1 = new Grid() { Height = 20, Width = 20, Background = Brushes.Transparent, Margin = new Thickness(70, 0, 0, 0) };
            GridLayout.Children.Add(grid1);
            BindHelper.SetBind("Text", grid1, Grid.TagProperty);
            //关闭按钮 点击事件
            grid1.MouseLeftButtonUp -= Grid1_MouseLeftButtonUp;
            grid1.MouseLeftButtonUp += Grid1_MouseLeftButtonUp;

            Ellipse ellipse = new Ellipse() { Width = 15, Height = 15, Fill = Brushes.White, Stretch = Stretch.Fill, Stroke = Brushes.Black, HorizontalAlignment = HorizontalAlignment.Center, VerticalAlignment = VerticalAlignment.Center };
            grid1.Children.Add(ellipse);
            TextBlock textBlock1 = new TextBlock() { FontSize = 10, Text = "X", HorizontalAlignment = HorizontalAlignment.Center, VerticalAlignment = VerticalAlignment.Center };
            grid1.Children.Add(textBlock1);

        }

        private void Grid1_MouseLeftButtonUp(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {

        }
        //    <Grid>
        //    <TextBlock Text = "{Binding Text}" />
        //    < Grid Height="20"  Width="20" Background="Transparent" Tag="{Binding Text}" MouseLeftButtonUp="Grid_MouseLeftButtonUp" Margin="70,0,0,0">
        //        <!--<Path Data = "M307,28.093333 L52.333333,250.76" Fill="Red" Stretch="Fill"   Stroke="#FFEA1717" HorizontalAlignment="Right" />
        //        <Path Data = "M56.666667,53 L352,265.66667" Fill="Red"  Stretch="Fill"   Stroke="#FFEA1717" HorizontalAlignment="Right" />-->
        //        <Ellipse Width = "15" Height="15" HorizontalAlignment="Center" VerticalAlignment="Center" Stroke="Black" Fill="White"></Ellipse>
        //        <TextBlock x:Name="text" FontSize="10" Text="X" HorizontalAlignment="Center" VerticalAlignment="Center" ></TextBlock>
        //    </Grid>
        //</Grid>
    }
}
