using System.Windows.Controls;

namespace Utility.Wpf
{
    /// <summary>
    /// wpf Grid 组件硬编码 操作公共类
    /// </summary>
    public class GridFactory
    {
        public static void SetLabel(Panel panel, string content, System.Windows.HorizontalAlignment horizontalAlignment = System.Windows.HorizontalAlignment.Right)
        {
            Label label = new Label();
            label.VerticalAlignment = System.Windows.VerticalAlignment.Center;
            label.HorizontalAlignment = horizontalAlignment;
            label.Content = content;
            panel.Children.Add(label);
        }
        public static void SetLabel(Grid grid, string content, int row, int col, System.Windows.HorizontalAlignment horizontalAlignment = System.Windows.HorizontalAlignment.Right)
        {
            Label label = new Label();
            label.VerticalAlignment = System.Windows.VerticalAlignment.Center;
            label.HorizontalAlignment = horizontalAlignment;
            label.Content = content;
            Grid.SetRow(label, row);
            Grid.SetColumn(label, col);
            grid.Children.Add(label);
        }
    }
}
