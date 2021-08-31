using System;
using System.Collections;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Media;
using Utility.Wpf.Entries;
using Utility.Wpf.ViewModels;
using System.Collections.ObjectModel;

namespace Utility.Wpf.Components
{

    public enum ComponentFlag
    {
        Control=1,
        Page=-1,
        MaterialDesignThemesControl=0,
        MaterialDesignThemesPage=2
    }
    /// <summary>
    ///尼玛 整理 东西 真 累 有空再  整理 
    /// </summary>
    public class ListTemplateComponent
    {
        /// <summary>
        /// 资源文件
        /// </summary>
        public ResourceDictionary Resources { get; set; }
        /// <summary>
        /// 布局 容器
        /// </summary>
        public readonly DockPanel DockPanelList = new DockPanel();
        /// <summary>
        /// 数据 表格
        /// </summary>
        protected readonly DataGrid DataGridList = new DataGrid();
        private readonly Thickness _operatorMargin = new Thickness(20, 10, 0, 10);//外 间距
        /// <summary>
        /// 添加
        /// </summary>
        protected Button AddOperator;
        /// <summary>
        /// 编辑
        /// </summary>
        protected Button ModifyOperator;
        /// <summary>
        /// 删除
        /// </summary>
        protected Button DeleteOperator;
        /// <summary>
        /// 预览
        /// </summary>
        protected Button PreviewOperator;
        /// <summary>
        /// 模板方法
        /// </summary>
        internal MethodTemplateEntry MethodTemplateEntry { get; set; }
        /// <summary>
        /// 是否基于 MaterialDesignTheme 实现
        /// </summary>
        public bool IsMaterialDesignTheme { get; set; } = false;
        /// <summary>
        /// 多个 表格 组合
        /// </summary>
        internal MuilDataEntry MuilDataEntry { get; set; }
        /// <summary>
        /// 表单 组装 默认 增删改查 或 添加
        /// </summary>
        public virtual DialogTemplateComponent DialogTemplateComponent { get; set; }
        /// <summary>
        /// 表单 组装 默认 编辑
        /// </summary>
        public virtual DialogTemplateComponent EditDialogTemplateComponent { get; set; }
        /// <summary>
        /// 表单 组装 默认 预览 或 删除
        /// </summary>
        public virtual DialogTemplateComponent PreviewDialogTemplateComponent { get; set; }
        /// <summary>
        ///默认 表单 窗口 重写 window 关闭 ,实际隐藏  默认 增删改查 或 添加
        /// </summary>
        public WpfWindow FormDialog { get; set; }

        //================
        //打开 太多 窗口 浪费资源 
        /// <summary>
        ///默认 表单 窗口 重写 window 关闭 ,实际隐藏  编辑
        /// </summary>
        public WpfWindow EditFormDialog { get; set; }

        /// <summary>
        ///默认 表单 窗口 重写 window 关闭 ,实际隐藏   预览 或 删除
        /// </summary>
        public WpfWindow PreViewFormDialog { get; set; }
        //================


        /// <summary>
        /// 表单 数据 双向绑定 增删改查 或 添加
        /// </summary>
        public object _dialogViewModel;
        /// <summary>
        /// 表单 数据 双向绑定 编辑
        /// </summary>
        public object _editDialogViewModel;
        /// <summary>
        /// 表单 数据 双向绑定 预览 或 删除
        /// </summary>
        public object _previewDialogViewModel;

        /// <summary>
        /// 查询 表单 数据双向 绑定 (查询表单有则组装 没有放弃.简单布局 用到。 复杂布局 重写)
        /// </summary>
        public object _queryViewModel;
        /// <summary>
        ///datagrid 双向绑定 原生数据
        /// </summary>
        public readonly DataListViewModel DataListViewModel = new DataListViewModel();
        /// <summary>
        /// 增删改查 窗口是否 拆开
        /// </summary>
        public bool SplitForm = false;
        /// <summary>
        /// 默认 表单 打开 新窗口 false 则直接更新 该ui 
        /// </summary>
        public bool IsFormShowWindow = true;
        /// <summary>
        /// 是否禁用
        /// </summary>
        protected bool Disabled { get; set; } = false;
        /// <summary>
        /// 按钮 操作标识
        /// </summary>
        protected OperatorFlag FormFlag { get; set; }
        /// <summary>
        /// 自定义多个表单(窗口)
        /// </summary>
        public List<ListEntry> ListEntries { get; set; }
        /// <summary>
        ///form  或 datagrid 绑定数据 增删改查 或 添加
        /// </summary>
        protected ListEntry ListEntry;
        /// <summary>
        ///form  绑定数据 编辑
        /// </summary>
        protected ListEntry EditListEntry;

        /// <summary>
        ///form   绑定数据 预览 或 删除
        /// </summary>
        protected ListEntry PreviewListEntry;
        /// <summary>
        /// 标题
        /// </summary>
        string _title;

        /// <summary>
        ///双向绑定数据 原生 viewmodel 类型
        /// </summary>
        Type _type;

        /// <summary>
        /// 默认  1 control -1 page 0 切记 不要用常量不然 默认调用基类
        /// </summary>
        /// <param name="falg">1 control -1 page 0:mdt</param>
        /// <param name="splitForm">增删改查 窗口是否 拆开</param>
        public ListTemplateComponent(ComponentFlag falg,bool splitForm)
        {
            if (falg == ComponentFlag .Control|| falg == ComponentFlag.Page)
            {
                DialogTemplateComponent = new DialogTemplateComponent();
                
            }
            if (falg == ComponentFlag.Control)
            {
                SplitForm = splitForm;
                if (splitForm)
                {
                    FormDialog = new WpfWindow() { WindowStartupLocation = WindowStartupLocation.CenterScreen };
                    EditFormDialog = new WpfWindow() { WindowStartupLocation = WindowStartupLocation.CenterScreen };
                    PreViewFormDialog = new WpfWindow() { WindowStartupLocation = WindowStartupLocation.CenterScreen };
                }
                else
                {
                    FormDialog = new WpfWindow() { WindowStartupLocation = WindowStartupLocation.CenterScreen };
                }
            }
        }

        /// <summary>
        /// 默认初始化
        /// </summary>
        /// <param name="listModel"></param>
        /// <param name="methodTemplateEntry"></param>
        /// <param name="falg">1 control -1 page 0:mdt</param>
        /// <param name="splitForm">增删改查 窗口是否 拆开</param>

        public ListTemplateComponent(ListEntry listModel, MethodTemplateEntry methodTemplateEntry, ComponentFlag falg, bool splitForm) : this(falg,splitForm)
        {
            //if (initial)
            //{
            //    Initial(listModel, methodTemplateEntry);
            //}
        }
        /// <summary>
        /// 默认初始化 
        /// </summary>
        /// <param name="muilDataEntry"></param>
        /// <param name="methodTemplateEntry"></param>
        /// <param name="flag">1 control -1 page 0:mdt</param>
        /// <param name="splitForm">增删改查 窗口是否 拆开</param>
        public ListTemplateComponent(MuilDataEntry muilDataEntry, MethodTemplateEntry methodTemplateEntry, ComponentFlag flag, bool splitForm) : this(muilDataEntry.Data[0].Lists[0], methodTemplateEntry, flag,splitForm)
        {

        }
        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="listModel"></param>
        /// <param name="methodTemplateEntry"></param>
        public virtual void Initial(ListEntry listModel, MethodTemplateEntry methodTemplateEntry)
        {
            this._title = "{0}" + listModel.Title;
            //this.DataContext = DataListViewModel;
            //先 这样 改动. 太大 我不知道咋改了
            this.ListEntry = listModel;
            this.MethodTemplateEntry = methodTemplateEntry;
            //绑定 一定 要在 DataGird 绑定之前完成绑定 
            this.Edit -= DataGridEdit;
            this.Delete -= DataGridDelete;
            this.Preview -= DataGridPreview;
            this.Edit += DataGridEdit;
            this.Delete += DataGridDelete;
            this.Preview += DataGridPreview;

            InitialLayout();//DataGird
            DialogTemplateComponent._listEntry = ListEntry;
            //DialogTemplateComponent.Initial();
            InitForm();
            DialogTemplateComponent.FormSubmit -= FormOperator;
            DialogTemplateComponent.FormSubmit += FormOperator;
            //this.Content = this.dialogHost;
            GetData();
        }
        /// <summary>
        /// 初始化 表单 窗口 
        /// </summary>
        public virtual void InitForm()
        {
            InitForm(FormDialog, ListEntry, _dialogViewModel, DialogTemplateComponent);
        }
        /// <summary>
        /// 初始化 表单 窗口 
        /// </summary>
        /// <param name="formDialog"></param>
        /// <param name="listEntry"></param>
        /// <param name="dialogViewModel"></param>
        /// <param name="dialogTemplateComponent"></param>
        public virtual void InitForm(WpfWindow formDialog,ListEntry listEntry,object dialogViewModel,DialogTemplateComponent dialogTemplateComponent)
        {
            if (formDialog != null)
            {
                formDialog.Width = listEntry.Width > 0 ? listEntry.Width : 500;
                formDialog.Height = listEntry.Height > 0 ? listEntry.Height : 350;
                formDialog.Height = formDialog.Height + 40;
                //dialogHost.DialogContent = MaterialDesignThemesDialog;//数据 表单
                formDialog.DataContext = dialogViewModel;//双向 绑定
                formDialog.Content = dialogTemplateComponent.DockPanelDialog;//数据 表单
            }
        }
        /// <summary>
        /// 加载 资源
        /// </summary>

        protected virtual void LoadSource()
        {
            //添加 静态 资源
            if (IsMaterialDesignTheme)
            {
                Resources.MergedDictionaries.Add(new ResourceDictionary() { Source = new Uri("pack://application:,,,/MaterialDesignThemes.Wpf;component/Themes/MaterialDesignTheme.DataGrid.xaml") });
                Resources.MergedDictionaries.Add(new System.Windows.ResourceDictionary() { Source = new Uri("pack://application:,,,/MaterialDesignThemes.Wpf;component/Themes/MaterialDesignTheme.DialogHost.xaml") });
            }
        }
        /// <summary>
        /// 初始化 资源 布局 双向 绑定数据 事件绑定  
        /// </summary>
        public virtual void InitialLayout()
        {
            LoadSource();
            InitialDockPanel();
            BindDataGridItemsSource();
            DataGridShow(ListEntry, DataListViewModel);
            this.AddOperator.Click -= Add_Click;
            this.ModifyOperator.Click -= Edit_Click;
            this.DeleteOperator.Click -= Delete_Click;
            this.PreviewOperator.Click -= Preview_Click;

            this.AddOperator.Click += Add_Click;
            this.ModifyOperator.Click += Edit_Click;
            this.DeleteOperator.Click += Delete_Click;
            this.PreviewOperator.Click += Preview_Click;

            this.DataGridList.Margin = new Thickness(0, 8, 0, 0);
            this.DataGridList.CanUserSortColumns = true;
            //DataGridAssist.SetCellPadding(DataGridList, new Thickness(13, 8, 8, 8));
            //DataGridAssist.SetColumnHeaderPadding(DataGridList, new Thickness(8));
        }
        /// <summary>
        /// 初始化 布局
        /// </summary>
        public void InitialDockPanel()
        {
            this.OperatorShow();
            this.SetPageShow();
            DockPanelList.Children.Add(DataGridList);

            DockPanel.SetDock(DataGridList, Dock.Bottom);
        }
        #region  查询 增 删 该 查
        private void Preview_Click(object sender, RoutedEventArgs e)
        {
            this.FormFlag = OperatorFlag.Query;
            if (!GetSelected())
            {
                return;
            }
            var obj = this.DataListViewModel.GetSelect();
            //重用代码
            DataGridPreview(obj.GetType().GetProperty(ListEntry.Id).GetValue(obj));
        }

        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            this.FormFlag = OperatorFlag.Delete;
            if (!GetSelected())
            {
                return;
            }
            var obj = this.DataListViewModel.GetSelect();
            //重用代码
            DataGridDelete(obj.GetType().GetProperty(ListEntry.Id).GetValue(obj));
        }

        private void Edit_Click(object sender, RoutedEventArgs e)
        {
            this.FormFlag = OperatorFlag.Modify;
            if (!GetSelected())
            {
                return;
            }
            var obj = this.DataListViewModel.GetSelect();
            //重用代码
            DataGridEdit(obj.GetType().GetProperty(ListEntry.Id).GetValue(obj));
        }

        private void Add_Click(object sender, RoutedEventArgs e)
        {
            if (_type != null)
            {
               //重新创建 清空 编辑 或 预览 残留的双向绑定数据
                if (_dialogViewModel is ICleanViewModel cleanViewModel)
                {
                    cleanViewModel.Clean();
                }
                else if (_dialogViewModel is IIsSelectedViewModel isSelectedViewModel)
                {
                   // this._dialogViewModel = Activator.CreateInstance(this._type);
                }
                //FormDialog.DataContext = this._dialogViewModel;//双向 绑定
                DialogBind?.Invoke(this._dialogViewModel);
            }
            DialogTemplateComponent.Set(false, false);
            DialogTemplateComponent.Set(true);
            this.FormFlag = OperatorFlag.Add;
            if(IsMaterialDesignTheme)
            {
                this.DialogTemplateComponent.Title.Text = string.Format(this._title, "添加");
            }
            else if(FormDialog!=null)
            {
                this.FormDialog.Title = string.Format(this._title, "添加");
            }
            FormShow();
        }
        #endregion 查询 增 删 该 查

        #region datagird 操作
        /// <summary>
        /// 编辑
        /// </summary>
        /// <param name="id"></param>
        public virtual void DataGridEdit(object id)
        {
            FormFlag = OperatorFlag.Modify;
            if (!GetSelected(ListEntry.Id, id))
            {
                return;
            }
            DialogTemplateComponent.Set(true, false);
            DialogTemplateComponent.Set(true);
            if (IsMaterialDesignTheme)
            {
                this.DialogTemplateComponent.Title.Text = string.Format(this._title, "编辑");
            }
            else if (FormDialog != null)
            {
                this.FormDialog.Title = string.Format(this._title, "编辑");
            }
            FormShow();

        }
        /// <summary>
        /// 表单 dialog 打开
        /// </summary>
        public Action FormDialogOpen { get; set; }
        /// <summary>
        /// dialog 显示
        /// </summary>
        protected virtual void FormShow()
        {
            if (FormDialogOpen != null)
            {
                FormDialogOpen.Invoke();
                return;
            }
            FormDialog.ShowDialog();
            //var result = await dialogHost.ShowDialog(MaterialDesignThemesDialog);
            //var result = await DialogHost.Show(MaterialDesignThemesDialog, "RootDialog");
        }
        /// <summary>
        /// dialog 显示
        /// </summary>
        protected virtual void FormClose()
        {
            FormDialog.Close();
            // DialogHost.CloseDialogCommand.Execute(null, null);
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="id"></param>
        public void DataGridDelete(object id)
        {
            FormFlag = OperatorFlag.Delete;
            if (!GetSelected(ListEntry.Id, id))
            {
                return;
            }
            DialogTemplateComponent.Set(true, false);
            DialogTemplateComponent.Set(false);
            if (IsMaterialDesignTheme)
            {
                this.DialogTemplateComponent.Title.Text = string.Format(this._title, "删除");
            }
            else if (FormDialog != null)
            {
                this.FormDialog.Title = string.Format(this._title, "删除");
            }
            FormShow();
        }

        /// <summary>
        /// 预览
        /// </summary>
        /// <param name="id"></param>
        public void DataGridPreview(object id)
        {
            FormFlag = OperatorFlag.Query;
            if (!GetSelected(ListEntry.Id, id))
            {
                return;
            }
            DialogTemplateComponent.Set(true, false);
            DialogTemplateComponent.Set(false);
            if (IsMaterialDesignTheme)
            {
                this.DialogTemplateComponent.Title.Text = string.Format(this._title, "预览");
            }
            else if (FormDialog != null)
            {
                this.FormDialog.Title = string.Format(this._title, "预览");
            }
            FormShow();
        }
        #endregion   datagird 操作
        /// <summary>
        /// 表单提交
        /// </summary>
        /// <returns></returns>
        public bool FormOperator()
        {
            if (FormFlag == OperatorFlag.Add)
            {
                object res = MethodTemplateEntry.Add?.Invoke(this._dialogViewModel);
                if (res != null)
                {
                    GetData();
                    FormClose();
                    return true;
                }
            }
            else if (FormFlag == OperatorFlag.Modify)
            {
                MethodTemplateEntry.Modify?.Invoke(this._dialogViewModel);
                GetData();
                FormClose();
                return true;
            }
            else if (FormFlag == OperatorFlag.Delete)
            {
                MethodTemplateEntry.Delete?.Invoke(this._dialogViewModel);
                GetData();
                FormClose();
                return true;
            }
            return false;
        }
        void GetData()
        {
            var data = MethodTemplateEntry?.FindList?.Invoke(1, 10);
            if (data != null)
            {
                if (data.GetType().IsGenericType)
                {
                    this._type = data.GetType().GenericTypeArguments[0];
                    if (this._dialogViewModel == null|| this._dialogViewModel.GetType() != this._type)
                    {
                        this._dialogViewModel = Activator.CreateInstance(this._type);
                        var method = _dialogViewModel.GetType().GetMethod("CreateByNullInstance", System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.Public);
                        if (method != null)
                        {
                            method.Invoke(_dialogViewModel, null);//初始化数据
                        }
                    }
                }
                List<object> temp = ToListObject(data);//统一格式数据 不然报错 list<a> - list<object>
                DataListViewModel.DataList = new ObservableCollection<object>(temp);
            }
        }
        private bool GetSelected(string idName, object id)
        {
            var obj = this.DataListViewModel.GetSelect(idName, id);
            if (obj == null)
            {
                MessageBox.Show("请选中一行数据");
            }
            else
            {
                //编辑时:这里 必须 创建 一个 新对象 不然影响 表格 数据(双向绑定)
                this._dialogViewModel = obj;//双向绑定 失效 重新赋值 没触发
                if (obj is IIsSelectedViewModel isSelectedViewModel)
                {
                    //isSelectedViewModel.CreateByNullInstance();
                }
                DialogBind.Invoke(obj);//双向绑定 有效
                return true;
            }
            return false;
        }
        private bool GetSelected()
        {
            if (this.FormFlag != OperatorFlag.Add)
            {
                var obj = this.DataListViewModel.GetSelect();
                if (obj == null)
                {
                    MessageBox.Show("请选中一行数据");
                }
                else
                {
                    this._dialogViewModel = obj;//空引用 尽然 没引用 过来 
                    if (obj is IIsSelectedViewModel isSelectedViewModel)
                    {
                        // isSelectedViewModel.CreateByNullInstance();
                    }
                    DialogBind?.Invoke(obj); //双向绑定 有效
                    return true;
                }
            }
            return false;
        }
        /// <summary>
        /// 绑定数据
        /// </summary>
        public virtual Action<object> DialogBind{get;set;}
        /// <summary>
        ///统一 datagrid 数据
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public List<object> ToListObject(object data)
        {
            List<object> objs = new List<object>();
            if (data is IEnumerable)
            {
                var enumerator = ((IEnumerable)data).GetEnumerator();
                while (enumerator.MoveNext())
                {
                    var da = enumerator.Current;
                    if(da is IIsSelectedViewModel selectedViewModel)
                    {
                        selectedViewModel.AllSelectEvent += (it) => {
                            //多线程 下 需要 用锁 (wpf 是单线程)
                            if(DataListViewModel.Flag== CheckFlag.None)
                            {
                                DataListViewModel.Flag = CheckFlag.Check;
                                DataListViewModel.IsAllSelected = it.IsSelected;
                                DataListViewModel.Flag = CheckFlag.None;
                            }
                        };
                    }
                    objs.Add(da);
                }
            }
            return objs;
        }


        /// <summary> 按钮  </summary>
        internal  virtual void OperatorShow()
        {
            WrapPanel wrapPanel = GetOperatorShow();
            DockPanelList.Children.Add(wrapPanel);
            DockPanel.SetDock(wrapPanel,Dock.Top);
          
        }


        /// <summary> 按钮  </summary>
        public virtual WrapPanel GetOperatorShow()
        {
            WrapPanel wrapPanel = new WrapPanel();
            this.AddOperator = new Button() { Content = "添加", HorizontalAlignment = HorizontalAlignment.Center, Width = 80, Height = 30, Margin = this._operatorMargin };
            this.ModifyOperator = new Button() { Content = "编辑", HorizontalAlignment = HorizontalAlignment.Center, Width = 80, Height = 30, Margin = this._operatorMargin };
            this.DeleteOperator = new Button() { Content = "删除", HorizontalAlignment = HorizontalAlignment.Center, Width = 80, Height = 30, Margin = this._operatorMargin };
            this.PreviewOperator = new Button() { Content = "预览", HorizontalAlignment = HorizontalAlignment.Center, Width = 80, Height = 30, Margin = this._operatorMargin };
            wrapPanel.Children.Add(this.AddOperator);
            wrapPanel.Children.Add(this.ModifyOperator);
            wrapPanel.Children.Add(this.DeleteOperator);
            wrapPanel.Children.Add(this.PreviewOperator);
            return wrapPanel;
        }

        /// <summary>
        /// 增 删 该 查 按钮 是否显示
        /// </summary>
        /// <param name="visiable"></param>
        public void SetOperatorShow(bool visiable)
        {
            var res = visiable ? Visibility.Collapsed : Visibility.Visible;
            this.AddOperator.Visibility = res;
            this.ModifyOperator.Visibility = res;
            this.DeleteOperator.Visibility = res;
            this.PreviewOperator.Visibility = res;
        }

        /// <summary>
        /// 绑定 dagrid 数据源
        /// </summary>
        public void BindDataGridItemsSource()
        {
            BindHelper.SetBind("DataList", DataGridList, DataGrid.ItemsSourceProperty, BindingMode.TwoWay, UpdateSourceTrigger.PropertyChanged);
            //this.DataGridList.SetBinding(DataGrid.ItemsSourceProperty, new Binding("DataList")
            //{
            //    Mode = BindingMode.TwoWay,
            //    UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged
            //});
            this.DataGridList.CellEditEnding += GridList_CellEditEnding;
            this.DataGridList.AutoGenerateColumns = false;
            this.DataGridList.SelectionUnit = DataGridSelectionUnit.Cell;
            this.DataGridList.CanUserAddRows = false;
            this.DataGridList.SelectionMode = DataGridSelectionMode.Extended;
        }

        /// <summary>
        /// datagird 编辑
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected virtual void GridList_CellEditEnding(object sender, DataGridCellEditEndingEventArgs e)
        {

        }
        /// <summary>
        /// datagrid 双向绑定数据
        /// </summary>
        /// <param name="listentry"></param>
        /// <param name="dataContext"></param>
        public virtual void DataGridShow(ListEntry listentry,object dataContext)
        {
            Expander expander = new Expander();
            expander.Header = listentry.Title;
            expander.Content = this.DataGridList;
            DockPanel.SetDock(expander, System.Windows.Controls.Dock.Top);
            this.SetDataGridCheckboxShow(dataContext);
            foreach (var item in listentry.Columns)
            {
                if (item.ColumnType == ColumnType.TextBox|| item.ColumnType == ColumnType.TextBoxNumber)
                {
                    SeDataGridColumnText(item);
                }
                else if (item.ColumnType == ColumnType.Combox)
                {
                    SetDataGridColumnCombox(item);
                }
            }
            this.SetDataGridOperatorShow();
        }
        /// <summary>
        /// DataGridCheckBoxColumn 绑定
        /// </summary>
        /// <param name="dataContext"></param>
        /// <param name="Id"></param>
        /// <param name="isSelected"></param>
        /// <param name="isAllSelected"></param>
        protected virtual void SetDataGridCheckboxShow(object dataContext, string Id = "Id",string isSelected= "IsSelected", string isAllSelected = "IsAllSelected")
        {
            DataGridCheckBoxColumn dataGridCheckBoxColumn = new DataGridCheckBoxColumn() { Width = 50 ,IsReadOnly=false,Binding=new Binding(isSelected) { Mode= BindingMode.TwoWay ,UpdateSourceTrigger= UpdateSourceTrigger.PropertyChanged } };
           if (IsMaterialDesignTheme)
            {
                dataGridCheckBoxColumn.ElementStyle = (Style)Resources["MaterialDesignDataGridCheckBoxColumnStyle"];
                dataGridCheckBoxColumn.EditingElementStyle = (Style)Resources["MaterialDesignDataGridCheckBoxColumnEditingStyle"];
            }
            BindHelper.SetBind(Id, dataGridCheckBoxColumn, CheckBox.TagProperty, BindingMode.TwoWay, UpdateSourceTrigger.PropertyChanged);
            var check = new CheckBox()
            {
                HorizontalAlignment = HorizontalAlignment.Center,
                DataContext = dataContext
                //无效
                //new Binding()
                //{
                //    RelativeSource = new RelativeSource() { AncestorType = DataGridList.GetType(), Mode = RelativeSourceMode.FindAncestor },
                //    Path = new PropertyPath("DataContext")
                //    //Path = new PropertyPath("DataContext." + isAllSelected)
                //}
            };
            // BindHelper.SetBind("DataContext", check, CheckBox.DataContextProperty, BindingMode.TwoWay, UpdateSourceTrigger.PropertyChanged);//无效
            dataGridCheckBoxColumn.Header = check;
            BindHelper.SetBind(isAllSelected, check, CheckBox.IsCheckedProperty, BindingMode.TwoWay, UpdateSourceTrigger.PropertyChanged);
            dataGridCheckBoxColumn.ElementStyle = new Style();
            dataGridCheckBoxColumn.ElementStyle.Setters.Add(new Setter() { Property = CheckBox.VerticalAlignmentProperty, Value = VerticalAlignment.Center });
            dataGridCheckBoxColumn.ElementStyle.Setters.Add(new Setter() { Property = CheckBox.HorizontalAlignmentProperty, Value = HorizontalAlignment.Center });
            DataGridList.Columns.Add(dataGridCheckBoxColumn);
        }
        /// <summary>
        /// 绑定 样式
        /// </summary>
        /// <param name="style"></param>
        protected void SetDataGridColumnHeader(Style style)
        {
            //style.Setters.Add(new Setter() { Property = DataGridColumnHeader.VerticalAlignmentProperty, Value = VerticalAlignment.Center });
            style.Setters.Add(new Setter() { Property = DataGridColumnHeader.HorizontalAlignmentProperty, Value = HorizontalAlignment.Center });
        }

        /// <summary>
        /// 表格 内 编辑 删除 预览 按钮 操作
        /// </summary>
        /// <param name="id"></param>
        protected virtual void SetDataGridOperatorShow(string id = "Id")
        {
            DataGridTemplateColumn dataGridTemplateColumn = new DataGridTemplateColumn() { Header = "操作" };
            //dataGridTemplateColumn.HeaderStyle = new Style();
            //SetDataGridColumnHeader(dataGridTemplateColumn.HeaderStyle);
            //ContentPresenter contentPresenter = new ContentPresenter();
            //contentPresenter.Content = new DataGridOperatorTemplate();
            //dataGridTemplateColumn.CellTemplate = contentPresenter.ContentTemplate;

            DataTemplate dt = new DataTemplate();

            FrameworkElementFactory fef = new FrameworkElementFactory(typeof(DataGridOperatorTemplate));
            Binding binding = new Binding("MarketIndicator");
            fef.SetBinding(DataGridOperatorTemplate.ContentProperty, binding);

            fef.SetBinding(DataGridOperatorTemplate.EditDependencyProperty,new Binding() { Source= Edit });
            fef.SetBinding(DataGridOperatorTemplate.DeleteDependencyProperty,  new Binding() { Source = Delete });
            fef.SetBinding(DataGridOperatorTemplate.PreviewDependencyProperty,  new Binding() { Source = Preview });
            fef.SetBinding(DataGridOperatorTemplate.IdDependencyProperty, new Binding() { Source = id });

            fef.SetValue(DataGridOperatorTemplate.ForegroundProperty, Brushes.White);
            dt.VisualTree = fef;

            dataGridTemplateColumn.CellTemplate = dt;


            DataGridList.Columns.Add(dataGridTemplateColumn);
            //VisualTreeHelper
           //var edit = dt.FindName("edit",null);
        }
        /// <summary>
        ///  表格 内 编辑 按钮 操作
        /// </summary>
        public Action<object> Edit { get; set; }
        /// <summary>
        ///  表格 内  删除  按钮 操作
        /// </summary>
        public Action<object> Delete { get; set; }
        /// <summary>
        ///  表格 内  预览 按钮 操作
        /// </summary>
        public Action<object> Preview { get; set; }
        /// <summary>
        ///  表格 内 编辑 删除 预览 按钮 操作
        /// </summary>
        private class DataGridOperatorTemplate : UserControl
        {
            public static readonly DependencyProperty EditDependencyProperty = DependencyProperty.Register("Edit", typeof(Action<object>),
                typeof(DataGridOperatorTemplate),new PropertyMetadata((de, e) =>{
                    DataGridOperatorTemplate dataGridOperator = (DataGridOperatorTemplate)de;
                    dataGridOperator.Edit -= (Action<object>)e.NewValue;
                    dataGridOperator.Edit += (Action<object>)e.NewValue;
                }), (obj) =>{ 
                    return true;
                });
            public static readonly DependencyProperty DeleteDependencyProperty = DependencyProperty.Register("Delete", typeof(Action<object>), typeof(DataGridOperatorTemplate),
                new PropertyMetadata((de, e) => {
                DataGridOperatorTemplate dataGridOperator = (DataGridOperatorTemplate)de;
                dataGridOperator.Delete -= (Action<object>)e.NewValue;
                    dataGridOperator.Delete += (Action<object>)e.NewValue;
                }), (obj) => {
                return true;
            });
            public static readonly DependencyProperty PreviewDependencyProperty = DependencyProperty.Register("Preview", typeof(Action<object>), typeof(DataGridOperatorTemplate),
                new PropertyMetadata((de, e) => {
                DataGridOperatorTemplate dataGridOperator = (DataGridOperatorTemplate)de;
                dataGridOperator.Preview -= (Action<object>)e.NewValue;
                dataGridOperator.Preview += (Action<object>)e.NewValue;
                }), (obj) => {
                return true;
            });
            public static readonly DependencyProperty IdDependencyProperty = DependencyProperty.Register("Id", typeof(string), typeof(DataGridOperatorTemplate),
             new PropertyMetadata((de, e) => {
                 DataGridOperatorTemplate dataGridOperator = (DataGridOperatorTemplate)de;
                 dataGridOperator.SetTag((string)e.NewValue);
             }), (obj) => {
                 return true;
             });
            Button _modify;
            Button _delete;
            Button _preview;
            public Action<object> Edit { get; set; }
            public Action<object> Delete { get; set; }
            public Action<object> Preview { get; set; }

            public DataGridOperatorTemplate()
            {
                Initial();
            }
            public new void SetValue(DependencyProperty dp, object value)
            {
                if (EditDependencyProperty == dp)
                {

                }
                base.SetValue(dp,value);
            }
            public void SetTag(string id)
            {
                BindHelper.SetBind(id, _modify, Button.TagProperty);
                BindHelper.SetBind(id, _delete, Button.TagProperty);
                BindHelper.SetBind(id, _preview, Button.TagProperty);
            }
            public void Initial(string id = "Id")
            {
                WrapPanel wrapPanel = new WrapPanel() { HorizontalAlignment = HorizontalAlignment.Center, VerticalAlignment = VerticalAlignment.Center };
                _modify = new Button() { Content = "编辑", Name="edit",Width = 60, Height = 30, Margin = new Thickness(10, 0, 10, 0) };
                _delete = new Button() { Content = "删除", Name = "delete", Width = 60, Height = 30 };
                _preview = new Button() { Content = "预览", Name = "preview", Width = 60, Height = 30, Margin = new Thickness(10, 0, 10, 0) };
                SetTag(id);
                _modify.Click -= _modify_Click;
                _delete.Click -= _delete_Click;
                _preview.Click -= _preview_Click;
                _modify.Click += _modify_Click;
                _delete.Click += _delete_Click; 
                _preview.Click += _preview_Click; 
                wrapPanel.Children.Add(_modify);
                wrapPanel.Children.Add(_delete);
                wrapPanel.Children.Add(_preview);
                this.Content = wrapPanel;
            }

            private void _preview_Click(object sender, RoutedEventArgs e)
            {
               if(sender is Button btn)
                {
                    Preview?.Invoke(btn.Tag);
                }
            }

            private void _delete_Click(object sender, RoutedEventArgs e)
            {
                if (sender is Button btn)
                {
                    Delete?.Invoke(btn.Tag);
                }
            }

            private void _modify_Click(object sender, RoutedEventArgs e)
            {
                if (sender is Button btn)
                {
                    Edit?.Invoke(btn.Tag);
                }
            }
        }
        /// <summary>
        /// 表格 内 DataGridTextColumn
        /// </summary>
        /// <param name="column"></param>
        protected virtual void SeDataGridColumnText(ColumnEntry column)
        {
            DataGridTextColumn dataGridTextColumn=GetDataGridColumnText<DataGridTextColumn>(column);
            this.DataGridList.Columns.Add(dataGridTextColumn);
        }
        /// <summary>
        /// 表格 内 DataGridTextColumn
        /// </summary>
        /// <param name="column"></param>
        protected virtual T GetDataGridColumnText<T>(ColumnEntry column) where T : DataGridTextColumn, new()
        {
            T dataGridTextColumn = new T()
            {
                Header = column.Header,
                IsReadOnly =Disabled|| column.Flag != ColumnEditFlag.Edit,
                Binding = new Binding(column.Name) { StringFormat = !string.IsNullOrEmpty(column.StringFormat) ? column.StringFormat : null },
            };
            dataGridTextColumn.ElementStyle = new Style();
            SetDataGridCellCenter(dataGridTextColumn.ElementStyle);
            return dataGridTextColumn;
        }
        /// <summary>
        /// 表格 内 DataGridComboBoxColumn
        /// </summary>
        /// <param name="column"></param>
        protected virtual void SetDataGridColumnCombox(ColumnEntry column)
        {
            DataGridComboBoxColumn dataGridComboBoxColumn = GetDataGridColumnCombox<DataGridComboBoxColumn>(column);
            dataGridComboBoxColumn.ElementStyle = new Style();
            SetDataGridCellCenter(dataGridComboBoxColumn.ElementStyle);
            this.DataGridList.Columns.Add(dataGridComboBoxColumn);
        }
        /// <summary>
        /// 表格 内 居中
        /// </summary>
        /// <param name="style"></param>
        protected void SetDataGridCellCenter(Style style)
        {
            style.Setters.Add(new Setter() { Property = DataGridCell.VerticalAlignmentProperty, Value = VerticalAlignment.Center });
            style.Setters.Add(new Setter() { Property = DataGridCell.HorizontalAlignmentProperty, Value = HorizontalAlignment.Center });
        }
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="column"></param>
        /// <returns></returns>
        protected virtual T GetDataGridColumnCombox<T>(ColumnEntry column) where T : DataGridComboBoxColumn, new()
        {
            T dataGridComboBoxColumn = new T()
            {
                Header = column.Header,
                //[1,2,3] 不能要 不然显示 不出文本
                DisplayMemberPath = column.DisplayMemberPath,
                SelectedValuePath = column.SelectedValuePath
            };
            dataGridComboBoxColumn.IsReadOnly = Disabled || column.Flag != ColumnEditFlag.Edit;
            if (column.Items != null && column.Items.Count > 0)
            {
                dataGridComboBoxColumn.ItemsSource = column.Items;
            }
            else
            {
                if (!string.IsNullOrEmpty(column.Key))
                {
                    var obj = CacheListModelManager.GetData(column.Key)?.Invoke(false);
                    dataGridComboBoxColumn.ItemsSource = (IEnumerable)obj;
                }
            }
            //[1,2,3]
            if (column.SingleItems)
            {
                dataGridComboBoxColumn.DisplayMemberPath = string.Empty;
                dataGridComboBoxColumn.SelectedValuePath = string.Empty;
                dataGridComboBoxColumn.TextBinding = new Binding(column.Name) { Mode = BindingMode.TwoWay, UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged };
            }
            else
            {
                dataGridComboBoxColumn.SelectedValueBinding = new Binding(column.Name) { Mode = BindingMode.TwoWay, UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged };
            }
            //dataGridComboBoxColumn.SelectedItemBinding = new Binding(column.Name) { Mode = BindingMode.TwoWay, UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged };
            dataGridComboBoxColumn.ElementStyle = new Style();
            SetDataGridCellCenter(dataGridComboBoxColumn.ElementStyle);
            return dataGridComboBoxColumn;
        }
        /// <summary>
        /// 显示 分页
        /// </summary>
        protected virtual void SetPageShow()
        {
            Grid grid = new PageCommpenont().GetPageShow();
            DockPanelList.Children.Add(grid);
            DockPanel.SetDock(grid, Dock.Bottom);
        }
    }
  

}
