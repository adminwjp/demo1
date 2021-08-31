//using MaterialDesignThemes.Wpf;
//using System;
//using System.Collections;
//using System.Collections.Generic;
//using System.Collections.ObjectModel;
//using System.Threading.Tasks;
//using System.Windows;
//using System.Windows.Controls;
//using System.Windows.Controls.Primitives;
//using System.Windows.Data;
//using System.Windows.Media;
//using Wpf.OA.ViewModels;
//using Wpf.Utility.Controls;
//using Wpf.ViewModels;

//namespace Utility.Wpf.Controls
//{
//    /// <summary>
//    /// 列表模板
//    /// </summary>
//    public class MaterialDesignThemesListTemplate : ListTemplate
//    {
//        MaterialDesignThemesDialogTemplate MaterialDesignThemesDialog;
//        ListModel _listModel;
//        DialogHost dialogHost = new DialogHost() {};
//        string _title;
//        Type _type;
//        object _dialogViewModel;
//        public MaterialDesignThemesListTemplate()
//        {

//        }
//        public MaterialDesignThemesListTemplate(ListModel listModel):this(listModel,null)
//        {

//        }
//        public MaterialDesignThemesListTemplate(ListModel listModel, MethodTemplate methodTemplate)
//        {
//            this._title = "{0}"+ listModel.Title;
//            this.DataContext = DataListViewModel;
//            this._listModel = listModel;
//            this.MethodTemplate = methodTemplate;

//            //绑定 一定 要在 DataGird 绑定之前完成绑定 
//            base.Edit -= DataGirdEdit;
//            base.Delete -= DataGirdDelete;
//            base.Preview -= DataGirdPreview;
//            base.Edit += DataGirdEdit;
//            base.Delete += DataGirdDelete;
//            base.Preview += DataGirdPreview;

//            Initial();//DataGird
//            MaterialDesignThemesDialog = new MaterialDesignThemesDialogTemplate(listModel);
//            MaterialDesignThemesDialog.Initial();
//            MaterialDesignThemesDialog.Width = listModel.Width>0 ? listModel.Width:500;
//            MaterialDesignThemesDialog.Height = listModel.Height > 0 ? listModel.Height : 350;
//            //dialogHost.DialogContent = MaterialDesignThemesDialog;//数据 表单
//            MaterialDesignThemesDialog.DataContext = this._dialogViewModel;//双向 绑定
//            MaterialDesignThemesDialog.FormSubmit -= FormOperator;
//            MaterialDesignThemesDialog.FormSubmit += FormOperator;
//            dialogHost.Content = DockPanelList;//数据 表格
//            this.Content = this.dialogHost;
//            GetData();


//        }
//        public async void DataGirdEdit(object id)
//        {
//            FormFlag = Models.OperatorFlag.Modify;
//            if (!GetSelected(_listModel.Id, id))
//            {
//                return;
//            }
//            MaterialDesignThemesDialog.Set(true, false);
//            MaterialDesignThemesDialog.Set(true);
//            this.MaterialDesignThemesDialog.Title.Text = string.Format(this._title, "编辑");
//            var result = await dialogHost.ShowDialog(MaterialDesignThemesDialog);
//        }
//        public async void  DataGirdDelete(object id)
//        {
//            FormFlag = Models.OperatorFlag.Delete;
//            if (!GetSelected(_listModel.Id, id))
//            {
//                return;
//            }
//            MaterialDesignThemesDialog.Set(true, false);
//            MaterialDesignThemesDialog.Set(false);
//            this.MaterialDesignThemesDialog.Title.Text = string.Format(this._title, "删除");
//            var result = await dialogHost.ShowDialog(MaterialDesignThemesDialog);
//        }
//        public async void DataGirdPreview(object id)
//        {
//            FormFlag = Models.OperatorFlag.Query;
//            if (!GetSelected(_listModel.Id, id))
//            {
//                return;
//            }
//            MaterialDesignThemesDialog.Set(true, false);
//            MaterialDesignThemesDialog.Set(false);
//            this.MaterialDesignThemesDialog.Title.Text = string.Format(this._title, "预览");
//            var result = await dialogHost.ShowDialog(MaterialDesignThemesDialog);
//        }
//        public bool FormOperator()
//        {
//            if(FormFlag== Models.OperatorFlag.Add)
//            {
//                object res=MethodTemplate.Add?.Invoke(this._dialogViewModel);
//                if(res!=null)
//                {
//                    GetData();
//                    DialogHost.CloseDialogCommand.Execute(null, null);
//                    return true;
//                }
//            }
//            else if(FormFlag== Models.OperatorFlag.Modify)
//            {
//                 MethodTemplate.Modify?.Invoke(this._dialogViewModel);
//                GetData();
//                DialogHost.CloseDialogCommand.Execute(null, null);
//                return true;
//            }
//            else if (FormFlag == Models.OperatorFlag.Delete)
//            {
//                //MethodTemplate.Delete?.Invoke(this._dialogViewModel);
//                //GetData();
//                //DialogHost.CloseDialogCommand.Execute(null, null);
//                //return true;
//            }
//            return false;
//        }
//        void GetData()
//        {
//            var data = MethodTemplate?.FindList?.Invoke(1, 10);
//            if (data != null)
//            {
//                if (data.GetType().IsGenericType)
//                {
//                    this._type = data.GetType().GenericTypeArguments[0];
//                }
//                List<object> temp = ToListObject(data);
//                DataListViewModel.DataList = new ObservableCollection<object>(temp);
//            }
//        }
//        private bool GetSelected(string idName,object id)
//        {
//            var obj = this.DataListViewModel.GetSelect(idName,id);
//            if (obj == null)
//            {
//                MessageBox.Show("请选中一行数据");
//            }
//            else
//            {
//                this._dialogViewModel = obj;
//                if (obj is IIsSelectedViewModel isSelectedViewModel)
//                {
//                    isSelectedViewModel.CreateByNullInstance();
//                }
//                MaterialDesignThemesDialog.DataContext = obj;
//                return true;
//            }
//            return false;
//        }
//        private bool GetSelected()
//        {
//            if (this.FormFlag != Models.OperatorFlag.Add)
//            {
//                var obj = this.DataListViewModel.GetSelect();
//                if (obj == null)
//                {
//                    MessageBox.Show("请选中一行数据");
//                }
//                else
//                {
//                    this._dialogViewModel = obj;//空引用 尽然 没引用 过来 
//                    if(obj is IIsSelectedViewModel isSelectedViewModel)
//                    {
//                        isSelectedViewModel.CreateByNullInstance();
//                    }
//                    MaterialDesignThemesDialog.DataContext = obj;
//                    return true;
//                }
//            }
//            return false;
//        }
//        public List<object> ToListObject(object data)
//        {
//            List<object> objs = new List<object>();
//            if(data is IEnumerable)
//            {
//                var enumerator = ((IEnumerable)data).GetEnumerator();
//                while (enumerator.MoveNext())
//                {
//                    var da = enumerator.Current;
//                    objs.Add(da);
//                }
//            }
//            return objs;
//        }

//        public void SetMdtDataGridCheckbox(string Id = "Id", string isSelected = "IsSelected", string isAllSelected = "IsAllSelected")
//        {
//            DataGridCheckBoxColumn dataGridCheckBoxColumn = new DataGridCheckBoxColumn() { Width = 50, Binding = new Binding(isSelected) { Mode = BindingMode.TwoWay, UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged } };
//            dataGridCheckBoxColumn.ElementStyle = (Style)Resources["MaterialDesignDataGridCheckBoxColumnStyle"];
//            dataGridCheckBoxColumn.EditingElementStyle = (Style)Resources["MaterialDesignDataGridCheckBoxColumnEditingStyle"];
//            Border border = new Border() { Background = Brushes.Transparent,Padding=new Thickness(6,0,6,0), HorizontalAlignment  = HorizontalAlignment.Center};
//            var check = new CheckBox()
//            {
//                HorizontalAlignment = HorizontalAlignment.Center,
//                DataContext = this.DataContext
//                //无效 
//                //DataContext = new Binding()
//                //{
//                //    RelativeSource = new RelativeSource() { AncestorType = DataGridList.GetType(), Mode = RelativeSourceMode.FindAncestor },
//                //    Path = new PropertyPath("DataContext")
//                //}
//            };
//            BindUtils.SetBind(isAllSelected, check, CheckBox.IsCheckedProperty, BindingMode.TwoWay, UpdateSourceTrigger.PropertyChanged);
//            border.Child = check;
//            dataGridCheckBoxColumn.Header = border;
//            DataGridList.Columns.Add(dataGridCheckBoxColumn);
//        }
//        public virtual void Initial()
//        {
//            LoadSource();
//            InitialDockPanel();
//            BindDataGridItemsSource();
//            SetMaterialDesignThemesDataGrid(this._listModel);
//            //base.AddOperator.Command = DialogHost.OpenDialogCommand;//new CommandImplementation(it=> { Add_Click(null,null); });
//            base.AddOperator.Click -= Add_Click;
//            base.ModifyOperator.Click -= Edit_Click;
//            base.DeleteOperator.Click -= Delete_Click;
//            base.PreviewOperator.Click -= Preview_Click;

//            base.AddOperator.Click += Add_Click;
//            base.ModifyOperator.Click += Edit_Click;
//            base.DeleteOperator.Click += Delete_Click;
//            base.PreviewOperator.Click += Preview_Click;

//            this.DataGridList.Margin = new Thickness(0, 8, 0, 0);
//            this.DataGridList.CanUserSortColumns = true;
//            DataGridAssist.SetCellPadding(DataGridList,new Thickness(13, 8 ,8, 8));
//            DataGridAssist.SetColumnHeaderPadding(DataGridList, new Thickness(8));
//        }

//        private async void Preview_Click(object sender, RoutedEventArgs e)
//        {
//            base.FormFlag = Models.OperatorFlag.Query;
//            if (!GetSelected())
//            {
//                return;
//            }
//            MaterialDesignThemesDialog.Set(true, false);
//            MaterialDesignThemesDialog.Set(false);
//            this.MaterialDesignThemesDialog.Title.Text = string.Format(this._title, "预览");
//            var result = await dialogHost.ShowDialog(MaterialDesignThemesDialog);
//            //var result = await DialogHost.Show(MaterialDesignThemesDialog, "RootDialog", ClosingEventHandler);
//        }
//        private void ClosingEventHandler(object sender, DialogClosingEventArgs eventArgs)
//        {

//        }

//        private async void Delete_Click(object sender, RoutedEventArgs e)
//        {
//            base.FormFlag = Models.OperatorFlag.Delete;
//            if (!GetSelected())
//            {
//                return;
//            }
//            MaterialDesignThemesDialog.Set(true, false);
//            MaterialDesignThemesDialog.Set(false);
//            this.MaterialDesignThemesDialog.Title.Text = string.Format(this._title, "删除");
//            var result = await dialogHost.ShowDialog(MaterialDesignThemesDialog);

//            //var result = await DialogHost.Show(MaterialDesignThemesDialog, "RootDialog", ClosingEventHandler);
//        }

//        private async void Edit_Click(object sender, RoutedEventArgs e)
//        {
//            base.FormFlag = Models.OperatorFlag.Modify;
//            if (!GetSelected())
//            {
//                return;
//            }
//            MaterialDesignThemesDialog.Set(true, false);
//            MaterialDesignThemesDialog.Set(true);
//            this.MaterialDesignThemesDialog.Title.Text = string.Format(this._title, "编辑");
//            var result = await dialogHost.ShowDialog(MaterialDesignThemesDialog);
//            //var result = await DialogHost.Show(MaterialDesignThemesDialog, "RootDialog", ClosingEventHandler);
//        }

//        private async void Add_Click(object sender, RoutedEventArgs e)
//        {
//            if (_type != null)
//            {
//                this._dialogViewModel = Activator.CreateInstance(this._type);
//                if(_dialogViewModel is IIsSelectedViewModel isSelectedViewModel)
//                {
//                    isSelectedViewModel.CreateByNullInstance();//null 不然 双向绑定 失败
//                }
//                MaterialDesignThemesDialog.DataContext = this._dialogViewModel;//双向 绑定
//            }
//            MaterialDesignThemesDialog.Set(false,false);
//            MaterialDesignThemesDialog.Set(true);
//            base.FormFlag = Models.OperatorFlag.Add;
//            this.MaterialDesignThemesDialog.Title.Text = string.Format(this._title,"添加");
//            var result = await dialogHost.ShowDialog(MaterialDesignThemesDialog);
//            //var result = await DialogHost.Show(MaterialDesignThemesDialog, "RootDialog");
//        }

//        public void InitialDockPanel()
//        {
//            base.OperatorShow();
//            base.SetPageShow();
//            DockPanelList.Children.Add(DataGridList);

//            DockPanel.SetDock(DataGridList, Dock.Bottom);
//        }
//        protected virtual void LoadSource()
//        {
//            //添加 静态 资源
//            base.Resources.MergedDictionaries.Add(new ResourceDictionary() { Source=new Uri("pack://application:,,,/MaterialDesignThemes.Wpf;component/Themes/MaterialDesignTheme.DataGrid.xaml") });
//            Resources.MergedDictionaries.Add(new System.Windows.ResourceDictionary() { Source = new Uri("pack://application:,,,/MaterialDesignThemes.Wpf;component/Themes/MaterialDesignTheme.DialogHost.xaml") });

//        }
//        protected virtual void SetMdtDataGridColumnText(ListModel.ColumnModel column)
//        {
//            MaterialDesignThemes.Wpf.DataGridTextColumn dataGridTextColumn = GetMdtDataGridColumnText(column);
//            this.DataGridList.Columns.Add(dataGridTextColumn);
//        }
//        protected virtual MaterialDesignThemes.Wpf.DataGridTextColumn GetMdtDataGridColumnText(ListModel.ColumnModel column)
//        {
//            MaterialDesignThemes.Wpf.DataGridTextColumn dataGridTextColumn = GetDataGridColumnText<MaterialDesignThemes.Wpf.DataGridTextColumn>(column);
//            dataGridTextColumn.MaxLength = column.MaxLength;
//            //dataGridTextColumn.EditingElementStyle = (Style)this.Resources["MaterialDesignDataGridTextColumnEditingStyle"];
//            //dataGridTextColumn.EditingElementStyle = (Style)this.Resources["MaterialDesignDataGridTextColumnPopupEditingStyle"];
//            return dataGridTextColumn;
//        }
//        protected virtual void SetMdtDataGridColumnTextNumber(ListModel.ColumnModel column)
//        {
//            MaterialDesignThemes.Wpf.DataGridTextColumn dataGridTextColumn = GetMdtDataGridColumnText(column);
//            //dataGridTextColumn.HeaderStyle = new Style() {TargetType=typeof(DataGridColumnHeader), /*BasedOn =(Style)this.Resources["MaterialDesignDataGridColumnHeader"]*/ };
//            //dataGridTextColumn.HeaderStyle.Setters.Add(new Setter() { Property = DataGridColumnHeader.HorizontalAlignmentProperty, Value = HorizontalAlignment.Center });
//            //var textBlock = new TextBlock() { TextWrapping = TextWrapping.Wrap, TextAlignment = TextAlignment.Right };
//            //dataGridTextColumn.HeaderStyle.Setters.Add(new Setter() { 
//            //    Property = MaterialDesignThemes.Wpf.DataGridTextColumn.HeaderTemplateProperty, 
//            //    Value =new DataTemplate()
//            //    {
//            //        DataType = textBlock.GetType()

//            //    }
//            //});
//           // dataGridTextColumn.ElementStyle = new Style() {/* TargetType=typeof(TextBlock)*/ };
//            //dataGridTextColumn.ElementStyle.Setters.Add(new Setter() { Property = TextBlock.HorizontalAlignmentProperty, Value = HorizontalAlignment.Right });
//            this.DataGridList.Columns.Add(dataGridTextColumn);
//        }
//        protected virtual void SetMdtDataGridColumnCombox(ListModel.ColumnModel column)
//        {
//            MaterialDesignThemes.Wpf.DataGridComboBoxColumn dataGridComboBoxColumn = GetDataGridColumnCombox<MaterialDesignThemes.Wpf.DataGridComboBoxColumn>(column);
//            dataGridComboBoxColumn.IsEditable = !Disabled && column.Flag == ListModel.ColumnEditFlag.Edit;
//            this.DataGridList.Columns.Add(dataGridComboBoxColumn);
//        }
//        protected virtual void SetMaterialDesignThemesDataGrid(ListModel listModel)
//        {
//            //base.DataGridList.ColumnHeaderStyle = new Style();
//            //SetDataGridColumnHeader(base.DataGridList.ColumnHeaderStyle);
//           // SetMdtDataGridCheckbox(listModel.Id);
//            SetDataGridCheckboxShow(listModel.Id);
//            foreach (var item in listModel.Columns)
//            {
//                switch (item.ColumnType)
//                {
//                    case ListModel.ColumnType.Combox:
//                        SetMdtDataGridColumnCombox(item);
//                        break;
//                    case ListModel.ColumnType.TextBoxNumber:
//                        SetMdtDataGridColumnTextNumber(item);
//                        break;
//                    case ListModel.ColumnType.None:
//                    case ListModel.ColumnType.TextBox:
//                    default:
//                        SetMdtDataGridColumnText(item);
//                        break;
//                }
//            }
//            SetDataGridOperatorShow();
//        }
//    }


//}
