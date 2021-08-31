
using System;
using System.Collections;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using Utility.Wpf.Entries;
using Utility.Wpf.Validations;



namespace Utility.Wpf.Components
{
   

    /// <summary>
    /// 默认 dialog 组件 组装
    /// </summary>
    public class DialogTemplateComponent
    {
        /// <summary>
        /// 资源文件
        /// </summary>
        public ResourceDictionary Resources { get; set; }
        /// <summary>
        /// dialog 数据源 双向绑定
        /// </summary>
        internal ListEntry _listEntry;
        /// <summary>
        /// ComboBox 
        /// </summary>
        protected readonly List<ComboBox> comboBoxes = new List<ComboBox>();
        /// <summary>
        /// TextBox
        /// </summary>
        protected readonly List<TextBox> textBoxes = new List<TextBox>();

        /// <summary>
        /// ComboBox column None
        /// </summary>
        protected readonly List<ComboBox> otherComboBoxes = new List<ComboBox>();
        /// <summary>
        /// TextBox column None
        /// </summary>
        protected readonly List<TextBox> otherTextBoxes = new List<TextBox>();
        /// <summary>
        /// TextBlock 
        /// </summary>
        protected readonly List<TextBlock> textBlocks = new List<TextBlock>();
        /// <summary>
        /// 是否基于 MaterialDesignTheme 实现
        /// </summary>
        protected bool IsMaterialDesignTheme { get; set; } = false;
        /// <summary>
        /// dialog 表单 布局 容器
        /// </summary>
        protected Grid GridForm = new Grid();
        /// <summary>
        /// 提交按钮
        /// </summary>
        public Button Ok;
        /// <summary>
        /// 标题 
        /// </summary>
        public TextBlock Title;
        /// <summary>
        /// 是否禁用表单
        /// </summary>
        protected bool Disabled { get; set; } = true;
        /// <summary>
        /// 是否需要标题
        /// </summary>
        public bool NeedTitle { get; set; } = true;
        /// <summary>
        /// 可用 提交 事件
        /// </summary>
        public Func<bool> FormSubmit { get; set; }
        /// <summary>
        ///可用 取消  事件
        /// </summary>
        public Func<bool> FormCancel { get; set; }
        /// <summary>
        /// dialog 布局 容器(表单 布局 和 标题布局)
        /// </summary>

        public readonly DockPanel DockPanelDialog = new DockPanel();
        //DialogHost _dialogHost = new DialogHost() { Identifier= "RootDialog" };

        /// <summary>
        /// dialog 组件
        /// </summary>
        /// <param name="listEntry">数据格式</param>
        public DialogTemplateComponent(ListEntry listEntry)
        {
            this._listEntry = listEntry;
        }
        /// <summary>
        /// 无
        /// </summary>
        public DialogTemplateComponent()
        {
           
        }
        /// <summary>
        /// 设置文本框 是否可用
        /// </summary>
        /// <param name="enable"></param>
        public void Set(bool enable)
        {
            foreach (var item in otherComboBoxes)
            {
                if (enable)
                {
                    if (item.IsEnabled == false)
                    {
                        item.IsEnabled = enable;
                    }
                }
                else
                {
                    if (item.IsEnabled)
                    {
                        item.IsEnabled = enable;
                    }
                }
            }
            foreach (var item in otherTextBoxes)
            {
                if (enable)
                {
                    if (item.IsEnabled == false)
                    {
                        item.IsEnabled = enable;
                    }
                }
                else
                {
                    if (item.IsEnabled)
                    {
                        item.IsEnabled = enable;
                    }
                }
            }
        }
        /// <summary>
        /// 设置文本框 是否隐藏 是否可用
        /// </summary>
        /// <param name="visiable"></param>
        /// <param name="enable"></param>
        public void Set(bool visiable,bool enable)
        {
            foreach (var item in comboBoxes)
            {
                if(visiable)
                {
                    if(item.Visibility != Visibility.Visible)
                    {
                        item.Visibility = Visibility.Visible;
                    }
                }else
                {
                    if (item.Visibility != Visibility.Collapsed)
                    {
                        item.Visibility = Visibility.Collapsed;
                    }
                }
                if (enable)
                {
                    if (item.IsEnabled==false)
                    {
                        item.IsEnabled = enable;
                    }
                }
                else
                {
                    if (item.IsEnabled)
                    {
                        item.IsEnabled = enable;
                    }
                }
            }
            foreach (var item in textBoxes)
            {
                if (visiable)
                {
                    if (item.Visibility != Visibility.Visible)
                    {
                        item.Visibility = Visibility.Visible;
                    }
                }
                else
                {
                    if (item.Visibility != Visibility.Collapsed)
                    {
                        item.Visibility = Visibility.Collapsed;
                    }
                }
                if (enable)
                {
                    if (item.IsEnabled == false)
                    {
                        item.IsEnabled = enable;
                    }
                }
                else
                {
                    if (item.IsEnabled)
                    {
                        item.IsEnabled = enable;
                    }
                }
            }
            foreach (var item in textBlocks)
            {
                if (visiable)
                {
                    if (item.Visibility != Visibility.Visible)
                    {
                        item.Visibility = Visibility.Visible;
                    }
                }
                else
                {
                    if (item.Visibility != Visibility.Collapsed)
                    {
                        item.Visibility = Visibility.Collapsed;
                    }
                }
                if (enable)
                {
                    if (item.IsEnabled == false)
                    {
                        item.IsEnabled = enable;
                    }
                }
                else
                {
                    if (item.IsEnabled)
                    {
                        item.IsEnabled = enable;
                    }
                }
            }
        }
        /// <summary>
        /// 初始化 布局 以及 事件 绑定
        /// </summary>
        public void Initial()
        {
            LoadSource();
            DialogTitle();
            for (int j = 0; j < 4; j++)
            {
                if (j == 1)
                {
                    GridForm.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(2, GridUnitType.Star) });
                }
                else if (j == 2)
                {
                    GridForm.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(6, GridUnitType.Star) });
                }
                else
                {
                    GridForm.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(1, GridUnitType.Star) });
                }
            }
            for (int j = 0; j < this._listEntry.Columns.Count+3; j++)
            {
                GridForm.RowDefinitions.Add(new RowDefinition() );
            }
            var i = 1;
            for (; i <= this._listEntry.Columns.Count; i++)
            {
                Set(this._listEntry.Columns[i-1], i, 1);
            }
            StackPanel stackPanel = new StackPanel() {Orientation= Orientation.Horizontal, VerticalAlignment = VerticalAlignment.Center, HorizontalAlignment = HorizontalAlignment.Right,Margin=new Thickness(0, 16, 0, 0) };
            Ok = new Button() { IsDefault=true, Height = 30, Width = 50/*, Command = DialogHost.CloseDialogCommand ,CommandParameter=true*/,Content= "确定" };
            Ok.Click -= Ok_Click;
            Ok.Click += Ok_Click;
            Button cancel = new Button() { IsDefault = true, Height = 30, Width=50,/*Command = DialogHost.CloseDialogCommand, */CommandParameter = false,Margin=new  Thickness(10,0,0,0), Content = "取消" };
            cancel.Click-= Cancel_Click;
            cancel.Click += Cancel_Click;
            stackPanel.Children.Add(Ok);
            stackPanel.Children.Add(cancel);
            GridForm.Children.Add(stackPanel);
            Grid.SetRow(stackPanel, i);
            Grid.SetColumn(stackPanel, 1);
            Grid.SetColumnSpan(stackPanel, 2);
            DockPanelDialog.Children.Add(GridForm);
            DockPanel.SetDock(GridForm, Dock.Bottom);
            //_dialogHost.Content = DockPanelDialog;
            //this.Content = _dialogHost;
        }
        /// <summary>
        /// 取消事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            bool? res = FormCancel?.Invoke();
            if (res.HasValue && res.Value)
            {

            }
        }
        /// <summary>
        /// 提交事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Ok_Click(object sender, RoutedEventArgs e)
        {
           bool? res= FormSubmit?.Invoke();
            if (res.HasValue&&res.Value)
            {

            }
        }

        /// <summary>
        /// MaterialDesignThemes 资源
        /// </summary>
        public virtual void LoadSource()
        {
            if (IsMaterialDesignTheme)
            {
                Resources.MergedDictionaries.Add(new System.Windows.ResourceDictionary() { Source = new Uri("pack://application:,,,/MaterialDesignThemes.Wpf;component/Themes/MaterialDesignTheme.ComboBox.xaml") });
                Resources.MergedDictionaries.Add(new System.Windows.ResourceDictionary() { Source = new Uri("pack://application:,,,/MaterialDesignThemes.Wpf;component/Themes/MaterialDesignTheme.Slider.xaml") });
                Resources.MergedDictionaries.Add(new System.Windows.ResourceDictionary() { Source = new Uri("pack://application:,,,/MaterialDesignThemes.Wpf;component/Themes/MaterialDesignTheme.TextBox.xaml") });
                Resources.MergedDictionaries.Add(new System.Windows.ResourceDictionary() { Source = new Uri("pack://application:,,,/MaterialDesignThemes.Wpf;component/Themes/MaterialDesignTheme.DialogHost.xaml") });
            }
        }
        /// <summary>
        /// ComboBox 提示
        /// </summary>
        /// <param name="comboBox"></param>
        /// <param name="tip"></param>
        protected virtual void ComboxHit(ComboBox comboBox, string tip)
        {
            //HintAssist.SetHint(comboBox, tip);
        }
        /// <summary>
        /// ComboBox 错误提示
        /// </summary>
        /// <param name="comboBox"></param>
        /// <param name="name"></param>
        protected virtual void ComboxErrorTip(ComboBox comboBox, string name)
        {
            //HintAssist.SetHelperText(comboBox, $"{name} 不能为空 !");
        }
        /// <summary>
        /// TextBox 提示
        /// </summary>
        /// <param name="textBox"></param>
        /// <param name="tip"></param>
        protected virtual void TextboxHit(TextBox textBox, string tip)
        {
            //HintAssist.SetHint(textBox, tip);
        }
        /// <summary>
        ///  TextBox 错误提示
        /// </summary>
        /// <param name="textBox"></param>
        /// <param name="name"></param>
        protected virtual void TextboxErrorTip(TextBox textBox, string name)
        {
            //HintAssist.SetHelperText(textBox, $"{name} 不能为空 !");
        }
     
        /// <summary>
        /// 文本框 布局
        /// </summary>
        /// <param name="column"></param>
        /// <param name="row"></param>
        /// <param name="col"></param>
        public void Set(ColumnEntry column,int row,int col)
        {
            TextBlock textBlock = new TextBlock() { Text=column.Header, VerticalAlignment = VerticalAlignment.Center,  HorizontalAlignment = HorizontalAlignment.Right,Margin=new Thickness(0,0,10,0) };
            GridForm.Children.Add(textBlock);
            Grid.SetRow(textBlock, row);
            Grid.SetColumn(textBlock, col);
            switch (column.ColumnType)
            {
                case ColumnType.Combox:
                    {
                        ComboBox comboBox = new ComboBox() { Height=35,VerticalContentAlignment= VerticalAlignment.Center,HorizontalContentAlignment= HorizontalAlignment.Left };
                        GridForm.Children.Add(comboBox);
                        Grid.SetRow(comboBox, row);
                        Grid.SetColumn(comboBox, col+1);
                        ComboxHit(comboBox, column.Header);
                        comboBox.SelectedValuePath = column.SelectedValuePath;
                        comboBox.DisplayMemberPath = column.DisplayMemberPath;
                        if (column.Items!=null&& column.Items.Count>0)
                        {
                            comboBox.ItemsSource = column.Items;
                        }
                        else
                        {
                            if (!string.IsNullOrEmpty(column.Key))
                            {
                                var obj=CacheListModelManager.GetData(column.Key)?.Invoke(false);
                                comboBox.ItemsSource = (IEnumerable)obj;
                            }
                        }
                        //BindUtils.SetBind("Items", comboBox, ComboBox.ItemsSourceProperty, column.StringFormat);
                        switch (column.Flag)
                        {
                            case ColumnEditFlag.Hiddern:
                                BindHelper.SetBind(column.Name, comboBox, ComboBox.TextProperty, column.StringFormat);
                                comboBox.IsEnabled = false;
                                comboBox.Visibility = Visibility.Collapsed;//折叠 位置改变, 隐藏 话  该位置 还存在 影响 布局(空的)
                                comboBoxes.Add(comboBox);
                                textBlocks.Add(textBlock);
                                break;
                            case ColumnEditFlag.Disabled:
                                BindHelper.SetBind(column.Name, comboBox, ComboBox.TextProperty, column.StringFormat);
                                comboBox.IsEnabled = false;
                                comboBoxes.Add(comboBox);
                                textBlocks.Add(textBlock);
                                break;
                            case ColumnEditFlag.Edit:
                            default:
                                {
                                    List<ValidationRule> validationRules = null;
                                    if (!Disabled && column.Required)
                                    {
                                        ComboxErrorTip(comboBox, column.Name);
                                        validationRules = new List<ValidationRule>() { new NotEmptyValidationRule() { ValidatesOnTargetUpdated = true } };
                                    }
                                    if (column.SingleItems)
                                    {
                                        BindHelper.SetBind(column.Name, comboBox, ComboBox.TextProperty, column.StringFormat, BindingMode.TwoWay, UpdateSourceTrigger.PropertyChanged, validationRules);
                                    }
                                    else
                                    {
                                        BindHelper.SetBind(column.Name, comboBox, ComboBox.SelectedValueProperty, column.StringFormat, BindingMode.TwoWay, UpdateSourceTrigger.PropertyChanged, validationRules);
                                    }
                                    otherComboBoxes.Add(comboBox);
                                }
                                break;
                        }
                    }
                    break;
                case ColumnType.TextBoxNumber:
                case ColumnType.None:
                case ColumnType.TextBox:
                default:
                    {
                        TextBox textBox = new TextBox() {  Height = 35, VerticalContentAlignment = VerticalAlignment.Center, HorizontalContentAlignment = HorizontalAlignment.Left };
                        GridForm.Children.Add(textBox);
                        Grid.SetRow(textBox, row);
                        Grid.SetColumn(textBox, col + 1);
                        TextboxHit(textBox, column.Header);
                        if (column.ColumnType==ColumnType.TextBoxNumber)
                        {
                            if (IsMaterialDesignTheme)
                            {
                                textBox.Style = (Style)Resources["MaterialDesignFloatingHintTextBox"];
                            }
                        }
                        switch (column.Flag)
                        {
                            case ColumnEditFlag.Hiddern:
                                BindHelper.SetBind(column.Name, textBox, TextBox.TextProperty, column.StringFormat);
                                textBox.IsEnabled = false;
                                textBox.Visibility = Visibility.Collapsed;
                                textBoxes.Add(textBox);
                                textBlocks.Add(textBlock);
                                break;
                            case ColumnEditFlag.Disabled:
                                BindHelper.SetBind( column.Name, textBox, TextBox.TextProperty,column.StringFormat);
                                textBox.IsEnabled = false;
                                textBoxes.Add(textBox);
                                textBlocks.Add(textBlock);
                                break;
                            case ColumnEditFlag.Edit:
                            default:
                                {
                                    List<ValidationRule> validationRules = null; 
                                    if (!Disabled && column.Required)
                                    {
                                        TextboxErrorTip(textBox, column.Name);
                                        validationRules = new List<ValidationRule>() { new NotEmptyValidationRule() { ValidatesOnTargetUpdated = true } };
                                    }
                                    BindHelper.SetBind(column.Name, textBox, TextBox.TextProperty,column.StringFormat,BindingMode.TwoWay, UpdateSourceTrigger.PropertyChanged, validationRules);
                                    otherTextBoxes.Add(textBox);
                                }
                                break;
                        }
                    }
                    break;
            }

        }
        /// <summary>
        /// dialog 标题
        /// </summary>
        public virtual void DialogTitle()
        {
            if (!IsMaterialDesignTheme)
            {
                return;
            }
            Grid grid = new Grid();
            grid.ColumnDefinitions.Add(new ColumnDefinition());
            grid.ColumnDefinitions.Add(new ColumnDefinition());
            if (NeedTitle)
            {
                Title = new TextBlock() { VerticalAlignment = VerticalAlignment.Center, Text = this._listEntry.Title, HorizontalAlignment = HorizontalAlignment.Center };
                if (IsMaterialDesignTheme)
                {
                    Title.Style = (Style)Resources["MaterialDesignHeadline6TextBlock"];
                }
                //BindUtils.SetBind("Title", textBlock, TextBlock.TextProperty, BindingMode.TwoWay, UpdateSourceTrigger.PropertyChanged);
                grid.Children.Add(Title);
                Grid.SetColumn(Title, 0);
                Grid.SetColumnSpan(Title, 2);
                Button button = new Button()
                {
                    ToolTip = "关闭",
                    VerticalAlignment = VerticalAlignment.Center,
                    HorizontalAlignment = HorizontalAlignment.Right,
                    IsCancel = true
                };
                if (IsMaterialDesignTheme)
                {
                    //button.Style = (Style)Resources["MaterialDesignOutlinedButton"];
                    CloseButton(button);
                }
                grid.Children.Add(button);
                Grid.SetColumn(button, 1);
                StackPanel stackPanel = new StackPanel() { Orientation = Orientation.Horizontal };
                TextBlock closeTextBlock = new TextBlock() { Text = "关闭" };
                stackPanel.Children.Add(closeTextBlock);
                CloseIcon(stackPanel);
                button.Content = stackPanel;
            }
            DockPanelDialog.Children.Add(grid);
            DockPanel.SetDock(grid,Dock.Top);
        }
        /// <summary>
        /// 关闭按钮
        /// </summary>
        /// <param name="stackPanel"></param>
        protected virtual void CloseIcon(StackPanel stackPanel)
        {
            //PackIcon packIcon = new PackIcon() { Margin = new Thickness(10, 0, 10, 0), Kind = PackIconKind.WindowClose };
            //stackPanel.Children.Add(closeTextBlock);
            //stackPanel.Children.Add(packIcon);
        }

        /// <summary>
        /// 关闭按钮
        /// </summary>
        /// <param name="button"></param>
        protected virtual void CloseButton(Button button)
        {
            //button.Command = DialogHost.CloseDialogCommand;
        }
    }
}
