using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using Utility.Wpf;

namespace Tool.Ctrls
{
    public partial class PlatCtrl : UserControl
    {
        private  Platform _platform ;
        private readonly PlatformCodition _platformCodition = new PlatformCodition();
        public readonly CheckBox CheckBoxNet = new CheckBox() { IsChecked=true,Content= "Net" ,VerticalAlignment= System.Windows.VerticalAlignment.Center,HorizontalAlignment= System.Windows.HorizontalAlignment.Left };
        public readonly CheckBox CheckBoxCore = new CheckBox() { Content="Core", VerticalAlignment = System.Windows.VerticalAlignment.Center, HorizontalAlignment = System.Windows.HorizontalAlignment.Left };
        public readonly CheckBox CheckBoxStandard = new CheckBox() { Content = "Standard", VerticalAlignment = System.Windows.VerticalAlignment.Center, HorizontalAlignment = System.Windows.HorizontalAlignment.Left };

        public readonly ComboBox ComboBoxNetMin = new ComboBox() { /*MaxWidth=400,MaxHeight=30,*/ VerticalContentAlignment = VerticalAlignment.Center };
        public readonly ComboBox ComboBoxNetMax = new ComboBox() { /*MaxWidth = 400, MaxHeight = 30,*/ VerticalContentAlignment = VerticalAlignment.Center };


        public readonly ComboBox ComboBoxCoreMin = new ComboBox() { /*MaxWidth = 400, MaxHeight = 30,*/ VerticalContentAlignment = VerticalAlignment.Center };
        public readonly ComboBox ComboBoxCoreMax = new ComboBox() { /*MaxWidth = 400, MaxHeight = 30,*/ VerticalContentAlignment = VerticalAlignment.Center };

        public readonly ComboBox ComboBoxStandardMin = new ComboBox() { /*MaxWidth = 400, MaxHeight = 30,*/ VerticalContentAlignment = VerticalAlignment.Center };
        public readonly ComboBox ComboBoxStandardMax = new ComboBox() { /*MaxWidth = 400, MaxHeight = 30,*/ VerticalContentAlignment = VerticalAlignment.Center };

        public readonly RadioButton RadioButtonAnd = new RadioButton() { Content = "AND", IsChecked=true, VerticalAlignment = System.Windows.VerticalAlignment.Center, HorizontalAlignment = System.Windows.HorizontalAlignment.Left };
        public readonly RadioButton RadioButtonOr = new RadioButton() { Content = "OR", VerticalAlignment = System.Windows.VerticalAlignment.Center, HorizontalAlignment = System.Windows.HorizontalAlignment.Left };

        public readonly CheckBox CheckBoxCompile = new CheckBox() { IsChecked = true, Content = "代码编译", VerticalAlignment = System.Windows.VerticalAlignment.Center, HorizontalAlignment = System.Windows.HorizontalAlignment.Left };

        public readonly CheckBox CheckBoxInputWay = new CheckBox() { IsChecked = true, Content = "完整条件", VerticalAlignment = System.Windows.VerticalAlignment.Center, HorizontalAlignment = System.Windows.HorizontalAlignment.Left };
       
        public readonly RichTextBox RichTextBoxLog = new RichTextBox();
        public void Initial(Grid grid)
        {
             int[] colLens =new int[] { 2, 3, 3, 3, 4, 2, 10, 2 };
            for (int i = 0; i < colLens.Length; i++)
            {
                grid.ColumnDefinitions.Add(new ColumnDefinition() { Width = new System.Windows.GridLength(colLens[i], System.Windows.GridUnitType.Star) });
            }

            for (int i = 0; i <12; i++)
            {
                grid.RowDefinitions.Add(new RowDefinition() { Height = new System.Windows.GridLength(10, System.Windows.GridUnitType.Star) });
                grid.RowDefinitions.Add(new RowDefinition() { Height = new System.Windows.GridLength(30, System.Windows.GridUnitType.Star) });
            }

            GridFactory.SetLabel(grid,"支持平台：",1, 1);
            SetCheck(grid,1,new CheckBox[] { CheckBoxNet , CheckBoxCore, CheckBoxStandard });

            SetRowCombox(grid,3, "Net平台：", ComboBoxNetMin, ComboBoxNetMax);
            SetRowCombox(grid, 5, "Net标准：", ComboBoxStandardMin, ComboBoxStandardMax);
            SetRowCombox(grid, 7, "NetCore：", ComboBoxCoreMin, ComboBoxCoreMax);

            GridFactory.SetLabel(grid, "选择方式：", 9, 1);
            SetRadioButton(grid);

            GridFactory.SetLabel(grid, "编译方式：", 11, 1);
            SetCheck(grid, 11, new CheckBox[] { CheckBoxCompile });

            GridFactory.SetLabel(grid, "输出方式：", 13, 1);
            SetCheck(grid, 13, new CheckBox[] { CheckBoxInputWay });

            Button button = new Button() { VerticalAlignment= System.Windows.VerticalAlignment.Center,HorizontalAlignment= System.Windows.HorizontalAlignment.Left};
            button.Content = "确定";
            button.Click += Button_Click;
            Grid.SetRow(button, 15);
            Grid.SetColumn(button, 2);
            grid.Children.Add(button);

            Grid.SetRow(RichTextBoxLog, 17);
            Grid.SetColumn(RichTextBoxLog, 2);
            Grid.SetColumnSpan(RichTextBoxLog, 5);
            Grid.SetRowSpan(RichTextBoxLog, 5);
            grid.Children.Add(RichTextBoxLog);
        }
        public void LoadData()
        {
            this._platform = Tool.PlatformHelper.GetPlatform();

            Set(this.ComboBoxNetMin, this._platform.Net);
            Set(this.ComboBoxNetMax, this._platform.Net);

            Set(this.ComboBoxCoreMin, this._platform.Core);
            Set(this.ComboBoxCoreMax, this._platform.Core);

            Set(this.ComboBoxStandardMin, this._platform.Standard);
            Set(this.ComboBoxStandardMax, this._platform.Standard);

        }
        private void Set(ComboBox comboBox,List<PlatformCategory> platformCategories)
        {
            comboBox.ItemsSource = platformCategories;
            comboBox.SelectedValuePath = "Id";
            comboBox.DisplayMemberPath = "Name";
            comboBox.Tag = platformCategories;
            comboBox.SelectedIndex = 0;
        }

        private void Button_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            this.RichTextBoxLog.Document.Blocks.Clear();
            this._platformCodition.Or = this.RadioButtonOr.IsChecked.HasValue&& this.RadioButtonOr.IsChecked.Value;
            SetValue(this._platformCodition.Net,this.CheckBoxNet, this.ComboBoxNetMin, this.ComboBoxNetMax);
            SetValue(this._platformCodition.Core, this.CheckBoxCore, this.ComboBoxCoreMin, this.ComboBoxCoreMax);
            SetValue(this._platformCodition.Standard, this.CheckBoxStandard, this.ComboBoxStandardMin, this.ComboBoxStandardMax);
            this._platformCodition.CodeCompile = this.CheckBoxCompile.IsChecked.HasValue && this.CheckBoxCompile.IsChecked.Value;
            this._platformCodition.InputWay = this.CheckBoxInputWay.IsChecked.HasValue && this.CheckBoxInputWay.IsChecked.Value;
            var str = Tool.PlatformHelper.GetCodition(this._platform,this._platformCodition);
            this.RichTextBoxLog.AppendText(str);

        }
        private void SetValue(Value value,CheckBox checkBox,ComboBox min,ComboBox max)
        {
            value.Enable = checkBox.IsChecked.HasValue && checkBox.IsChecked.Value;
            if (value.Enable)
            {
                value.Min = (int)min.SelectedValue;
                value.Max = (int)max.SelectedValue;
            }
        }
        private void SetRadioButton(Grid grid)
        {
            Grid.SetRow(RadioButtonAnd, 9);
            Grid.SetColumn(RadioButtonAnd, 2);
            grid.Children.Add(RadioButtonAnd);

            Grid.SetRow(RadioButtonOr, 9);
            Grid.SetColumn(RadioButtonOr, 3);
            grid.Children.Add(RadioButtonOr);
        }

        private void SetRowCombox(Grid grid,int row,string content,ComboBox min,ComboBox max)
        {
            GridFactory.SetLabel(grid, content, row, 1);
            Grid.SetRow(min, row);
            Grid.SetColumn(min, 2);
            Grid.SetColumnSpan(min, 3);
            grid.Children.Add(min);
            GridFactory.SetLabel(grid, " ~ ", row, 5, System.Windows.HorizontalAlignment.Center);
            Grid.SetRow(max, row);
            Grid.SetColumn(max, 6);
            grid.Children.Add(max);
        }
        private void SetCheck(Grid grid,int row,CheckBox[] checkBoxes)
        {
            for (int i = 0; i < checkBoxes.Length; i++)
            {
                Grid.SetRow(checkBoxes[i], row);
                Grid.SetColumn(checkBoxes[i], 2+i);
                grid.Children.Add(checkBoxes[i]);
            }
        }
    }
}
