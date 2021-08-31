using Utility.Wpf.Demo.Template.Service;
using Utility.Wpf.Demo.Template.ViewModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Utility.Helpers;
using Utility.Wpf;

namespace Utility.Wpf.Demo.Template
{
    /// <summary>
    /// TemplateDbControl.xaml 的交互逻辑
    /// </summary>
    public partial class TemplateDbControl : UserControl
    {
        public TemplateDbControl()
        {
            InitializeComponent();
            this.DataContext = resultViewModel;
            LoadData();
        }
        OperatorFlag operatorFlag = OperatorFlag.Add;
        readonly DatabaseResultViewModel resultViewModel = new DatabaseResultViewModel();
        int page = 1, size = 10;
        private void LoadData(bool where = false)
        {
            resultViewModel.DataList?.Clear();
            if (resultViewModel.DataList == null)
            {
                resultViewModel.DataList = new ObservableCollection<DatabaseViewModel>();
            }
            var wh = where ? resultViewModel.Query : DatabaseViewModel.Empty;
            var data = TemplateService.Empty.GetDbs(wh, page, size);
            data.Data.ForEach(it =>
            {
                resultViewModel.DataList.Add(it);
            });
        }
        private void Operator(object sender, int flag = 1)
        {
            MessageBox.Show("暂无实现 ui!");
            return;
            if (sender is Button button)
            {
                if (button.Tag != null)
                {
                    //MessageBox.Show(button.Tag.ToString());
                    if (flag == 1||flag==3)
                    {
                        DatabaseViewModel viewModel = resultViewModel.GetSelect("Id", (long)button.Tag);
                        if (viewModel == null)
                        {
                            MessageBox.Show("请选中一行数据编辑!");
                            return;
                        }
                        ReflectHelper.Copy(viewModel, resultViewModel.TableForm);//防止双向绑定 数据 数据脏读
                        operatorFlag =flag==1? OperatorFlag.Modify: OperatorFlag.Query;
                        // Oper(dialogConfigViewModel);
                    }
                    else
                    {
                        TemplateService.Empty.RemoveDb((long)button.Tag);
                        LoadData();
                    }
                }
            }
        }
        private void btnEdit_Click(object sender, RoutedEventArgs e)
        {
            Operator(sender, 1);
        }

        private void btnPreview_Click(object sender, RoutedEventArgs e)
        {
            Operator(sender, 3);
        }

        private void btnDele_Click(object sender, RoutedEventArgs e)
        {
            Operator(sender, 2);
        }

        private void btnQuery_Click(object sender, RoutedEventArgs e)
        {
            LoadData(true);
        }

        private void btnClean_Click(object sender, RoutedEventArgs e)
        {
            resultViewModel.Query.Clean();
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            var res = TemplateService.Empty.AddDb(resultViewModel.Form);
            if (res)
            {
                LoadData();
            }
            else
            {
                MessageBox.Show("add fail");
            }
        }

        private void btnModify_Click(object sender, RoutedEventArgs e)
        {
            var res = TemplateService.Empty.EditDb(resultViewModel.Form);
            if (res)
            {
                LoadData();
            }
            else
            {
                MessageBox.Show("edit fail");
            }
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            var items = resultViewModel.GetSelectItems();
            if (items.Any())
            {
                var res = TemplateService.Empty.RemoveCol(items.Select(it => it.Id).ToArray());
                if (res)
                {
                    LoadData();
                }
                else
                {
                    MessageBox.Show("delete fail");
                }
            }
            else
            {
                MessageBox.Show("unselectd data,not delete");
            }
        }
    }
}
