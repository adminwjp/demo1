using System.Windows;
using System.Windows.Controls;
using Utility.Wpf;
using Utility.Wpf.Extensions;

namespace Tool.Ctrls
{
    public  partial class StrCtrl :UserControl
    {
        private readonly RichTextBox _richTextBoxOrigial = new RichTextBox();
        private readonly RichTextBox _richTextBoxCurrent = new RichTextBox();
        private readonly GroupBox _groupBoxOrigial = new GroupBox() { Header="转义内容"};
        private readonly GroupBox _groupBoxCurrent = new GroupBox() { Header = "转义结果" };
        private readonly ComboBox _comboBoxType = new ComboBox() { MaxWidth=300,MinWidth=130,VerticalContentAlignment=  VerticalAlignment.Center};
        public void Initial(Grid grid)
        {
            int[] cols = new int[] { 1, 2,1 };
            for (int i = 0; i < cols.Length; i++)
            {
                grid.ColumnDefinitions.Add(new ColumnDefinition() { Width = new System.Windows.GridLength(cols[i], System.Windows.GridUnitType.Star) });
            }
            int[] rows = new int[] { 1,1, 3, 3, 1 };
            for (int i = 0; i < rows.Length; i++)
            {
                grid.RowDefinitions.Add(new RowDefinition() { Height = new System.Windows.GridLength(rows[i], System.Windows.GridUnitType.Star) });
            }
            _groupBoxOrigial.SetValue(Grid.RowProperty, 2);
            _groupBoxOrigial.SetValue(Grid.ColumnProperty, 0);
            _groupBoxOrigial.SetValue(Grid.ColumnSpanProperty,4);
            _groupBoxOrigial.Content = _richTextBoxOrigial;
            grid.Children.Add(_groupBoxOrigial);

            _groupBoxCurrent.SetValue(Grid.RowProperty, 3);
            _groupBoxCurrent.SetValue(Grid.ColumnProperty, 0);
            _groupBoxCurrent.SetValue(Grid.ColumnSpanProperty, 4);
            _groupBoxCurrent.Content = _richTextBoxCurrent;
            grid.Children.Add(_groupBoxCurrent);


            WrapPanel panel = new WrapPanel() { Orientation= Orientation.Horizontal,HorizontalAlignment= System.Windows.HorizontalAlignment.Center };
            
            GridFactory.SetLabel(panel, "类型：", System.Windows.HorizontalAlignment.Left);
            panel.SetValue(Grid.RowProperty, 1);
            panel.SetValue(Grid.ColumnProperty, 1);
            //Grid.SetRow(panel, 1);
            //Grid.SetColumn(panel, 1);
            grid.Children.Add(panel);

            panel.Children.Add(_comboBoxType);

            Button button = new Button() { Content = "确定", Width=50,Height=30, Margin = new System.Windows.Thickness(10,0,0,0) };
            button.Click += Button_Click;
            panel.Children.Add(button);

            Button clear = new Button() { Content = "取消", Width = 50, Height = 30, Margin = new System.Windows.Thickness(10, 0, 0, 0) };
            clear.Click += Clear_Click;
            panel.Children.Add(clear);
        }

        private void Button_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            if (this._comboBoxType.SelectedItem is StringHelper.StringEntity  entity)
            {
                this._richTextBoxCurrent.Document.Blocks.Clear();
                this._richTextBoxCurrent.AppendText(StringHelper.GetString(_richTextBoxOrigial.GetRichTexboxTextFormat(),entity.StringType));
            }
            //if (this._comboBoxType.SelectedValue is StringHelper.StringType type)
            //{
            //    this._richTextBoxCurrent.Document.Blocks.Clear();
            //    this._richTextBoxCurrent.AppendText(StringHelper.GetString(_richTextBoxOrigial.GetRichTexboxTextFormat(), type));
            //}
        }

        private void Clear_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            this._comboBoxType.SelectedIndex = 0;
            this._richTextBoxOrigial.Document.Blocks.Clear();
            this._richTextBoxCurrent.Document.Blocks.Clear();
        }

        public void LoadData()
        {
            this._comboBoxType.ItemsSource = StringHelper.Initial();
            this._comboBoxType.SelectedValuePath = "StringType";
            this._comboBoxType.DisplayMemberPath = "Name";
            this._comboBoxType.SelectedIndex = 0;
        }
    }
}
