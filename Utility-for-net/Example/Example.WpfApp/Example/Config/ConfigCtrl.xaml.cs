using Config.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using Utility.Domain.Repositories;
using Utility.Ioc;
using Utility.Mappers;
using Utility.Wpf;
using Utility.Wpf.Components;
using Utility.Wpf.Ctrls;

namespace Example.WpfApp
{
    /// <summary>
    /// ConfigCtr.xaml 的交互逻辑
    /// </summary>
    public partial class ConfigCtrl : UserControl
    {
        /// <summary>
        /// 查询 条件 双向 绑定
        /// </summary>
        ConfigViewModel selectConfigViewModel = new ConfigViewModel();
        /// <summary>
        /// 表单双向绑定
        /// </summary>
        ConfigViewModel dialogConfigViewModel;
        IRepository<ConfigEntity, string> repository;
        OperatorFlag operatorFlag = OperatorFlag.Add;
        PageCommpenont pageCommpenont = new PageCommpenont();
        /// <summary>
        /// ioc 依赖注入 获取 需要的对象
        /// </summary>
        public IIocManager IocManager { get; set; }
        WpfWindow window = new WpfWindow()
        {
            Width = ConfigResultViewModel.Table.Width + 30,
            Height = ConfigResultViewModel.Table.Height + 30,
            WindowStartupLocation = WindowStartupLocation.CenterScreen,
            ResizeMode = ResizeMode.NoResize,
            Title = "添加配置信息"
        };
        DialogTemplateCtrl _dialog = new DialogTemplateCtrl(ConfigResultViewModel.Table) { Width = ConfigResultViewModel.Table.Width, Height = ConfigResultViewModel.Table.Height };
        public ConfigCtrl()
        {
            _dialog.DialogTemplateComponent.NeedTitle = false;
            IocManager = AppStart.IocManager;
            repository = IocManager.Get<IRepository<ConfigEntity, string>>();
            InitializeComponent();
            _dialog.DialogTemplateComponent.Initial();
            window.Content = _dialog;
            LoadData();
            this.DataContext = ConfigResultViewModel.Instance;
            _dialog.DialogTemplateComponent.FormSubmit = () =>
            {
                if (operatorFlag == OperatorFlag.Add)
                {
                    dialogConfigViewModel.Id = Guid.NewGuid().ToString("N").Substring(0, 20);
                    repository.Insert(dialogConfigViewModel.Parse());
                    GetData();
                    selectConfigViewModel.Clean();
                    window.Close();

                }
                else if (operatorFlag == OperatorFlag.Modify)
                {
                    repository.Update(dialogConfigViewModel.Parse());
                    GetData();
                    window.Close();
                }
                return true;
            };
            _dialog.DialogTemplateComponent.FormCancel = () =>
            {
                window.Close();
                return true;
            };

            Grid grid = pageCommpenont.GetPageShow();
            dockPanel.Children.Insert(1, grid);
            DockPanel.SetDock(grid, Dock.Bottom);

            this.Loaded -= ConfigCtr_Loaded;
            this.Loaded += ConfigCtr_Loaded;
        }

        private void ConfigCtr_Loaded(object sender, RoutedEventArgs e)
        {
            dataGrid.LoadingRow -= DataGrid_LoadingRow;
            dataGrid.LoadingRow += DataGrid_LoadingRow;   //自动添加序号的事件  调用下面的dataGrid_LoadingRow  
        }

        private void DataGrid_LoadingRow(object sender, DataGridRowEventArgs e)
        {
            e.Row.Header = e.Row.GetIndex() + 1;    //设置行表头的内容值  
        }

        private void LoadData()
        {
            var datas = repository.FindListByEntityAndPage(selectConfigViewModel.Parse(), 1, 10);
            var objectMapper = IocManager.Get<IMapper>();
            var results = objectMapper.Map<List<ConfigViewModel>>(datas).ToList();
            ConfigResultViewModel.Instance.DataList = new System.Collections.ObjectModel.ObservableCollection<ConfigViewModel>(results);
            ConfigViewModel.ConfigResult = ConfigResultViewModel.Instance;
        }

        private void btnQuery_Click(object sender, RoutedEventArgs e)
        {
            GetData();
        }

        private void GetData()
        {
            //bug 数据 怎么 都是 空的  nhibernate 没有 transtion
            var datas = repository.FindListByEntityAndPage(ConfigResultViewModel.Instance.Config.Parse(), 1, 10);
            var objectMapper = IocManager.Get<IMapper>();
            var results = objectMapper.Map<List<ConfigViewModel>>(datas).ToList();
            ConfigResultViewModel.Instance.DataList = new System.Collections.ObjectModel.ObservableCollection<ConfigViewModel>(results);
            ConfigViewModel.ConfigResult = ConfigResultViewModel.Instance;
            ConfigResultViewModel.Instance.IsAllSelected = false;
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            operatorFlag = OperatorFlag.Add;
            Oper(selectConfigViewModel);
        }
        private void Oper(ConfigViewModel addressView)
        {

            string title = string.Empty;

            switch (operatorFlag)
            {
                case OperatorFlag.Add:
                    title = "添加" + ConfigResultViewModel.Table.Title;
                    _dialog.DialogTemplateComponent.Set(false, false);
                    _dialog.DialogTemplateComponent.Set(true);
                    _dialog.DialogTemplateComponent.Ok.IsEnabled = true;
                    break;
                case OperatorFlag.Modify:
                    _dialog.DialogTemplateComponent.Set(true, false);
                    _dialog.DialogTemplateComponent.Set(true);
                    title = "编辑" + ConfigResultViewModel.Table.Title;
                    _dialog.DialogTemplateComponent.Ok.IsEnabled = true;
                    break;
                case OperatorFlag.Delete:
                    break;
                case OperatorFlag.Query:
                    _dialog.DialogTemplateComponent.Set(true, false);
                    _dialog.DialogTemplateComponent.Set(false);
                    title = "预览" + ConfigResultViewModel.Table.Title;
                    _dialog.DialogTemplateComponent.Ok.IsEnabled = false;
                    break;
                default:
                    break;
            }
            dialogConfigViewModel = addressView;
            window.DataContext = dialogConfigViewModel;
            window.Title = title;
            Dispatcher.Invoke(() => { if (_dialog.DialogTemplateComponent.Title != null) _dialog.DialogTemplateComponent.Title.Text = title; });
            window.ShowDialog();
        }

        private void btnModify_Click(object sender, RoutedEventArgs e)
        {
            ConfigViewModel addressView = ConfigResultViewModel.Instance.GetSelect();
            if (addressView == null)
            {
                MessageBox.Show("请选中一行数据!");
                return;
            }
            dialogConfigViewModel = addressView.Copy();//没有确定修改不需要双向绑定
            operatorFlag = OperatorFlag.Modify;
            Oper(dialogConfigViewModel);
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            List<string> ids = ConfigResultViewModel.Instance.GetMulSelectId().ToList();
            if (ids == null || ids.Count == 0)
            {
                MessageBox.Show("请选中一行数据!");
                return;
            }
            repository.DeleteList(ids.ToArray());
            GetData();
        }

        private void btnEdit_Click(object sender, RoutedEventArgs e)
        {
            Operator(sender);
        }
        private void Operator(object sender, int flag = 1)
        {
            if (sender is Button button)
            {
                if (button.Tag != null)
                {
                    //MessageBox.Show(button.Tag.ToString());
                    if (flag == 1)
                    {
                        ConfigViewModel addressView = ConfigResultViewModel.Instance.GetSelect((string)button.Tag);
                        if (addressView == null)
                        {
                            MessageBox.Show("请选中一行数据!");
                            return;
                        }
                        dialogConfigViewModel = addressView.Copy();
                        operatorFlag = OperatorFlag.Modify;
                        Oper(dialogConfigViewModel);
                    }
                    else if (flag == 3)
                    {
                        ConfigViewModel addressView = ConfigResultViewModel.Instance.GetSelect((string)button.Tag);
                        if (addressView == null)
                        {
                            MessageBox.Show("请选中一行数据!");
                            return;
                        }
                        dialogConfigViewModel = addressView;
                        operatorFlag = OperatorFlag.Query;
                        Oper(dialogConfigViewModel);

                    }
                    else
                    {
                        repository.Delete((string)button.Tag);
                        GetData();
                    }
                }
            }
        }

        private void btnDele_Click(object sender, RoutedEventArgs e)
        {
            Operator(sender, 2);
        }

        private void btnClean_Click(object sender, RoutedEventArgs e)
        {
            ConfigResultViewModel.Instance.Config?.Clean();
        }

        private void btnPreview_Click(object sender, RoutedEventArgs e)
        {
            Operator(sender, 3);
        }
    }
}
