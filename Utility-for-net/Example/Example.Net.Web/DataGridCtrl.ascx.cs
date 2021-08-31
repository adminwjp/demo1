using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using Utility.Model;

namespace Example.Web
{
    /// <summary>自动生成的 .ascx.cs 文件 类库没有 需要复制过来  
    /// <%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ServiceCtrl.ascx.cs" Inherits="Service.Web.ServiceCtrl" %> 
    /// 通用表格  默认 DataGrid 已实现 
    /// </summary>
    public partial class DataGridCtrl : System.Web.UI.UserControl
    {
        private DataGrid dataGrid = new DataGrid();//一般用于表格
        //private DataList dataList = new DataList();//一般用于表格
        // private Repeater repeater = new Repeater();//一般用于布局的 比如菜单布局
        //private GridView gridView = new GridView();//一般用于表格
        public ListModel ListTable { get; set; }
        public DataListDataSource DataListDataSource { get; set; }
        protected CheckBox IsAllSelectedCheckBox = new CheckBox() { ID = "IsAllSelected", Checked = false, AutoPostBack = true };
        public string Title { get; set; }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
               
                this.Controls.Add(dataGrid);
                DataListDataSource.IsAllSelectedEvent -= DataListDataSource_IsAllSelectedEvent;
                DataListDataSource.IsAllSelectedEvent += DataListDataSource_IsAllSelectedEvent;
                dataGrid.DeleteCommand -= DataGrid_DeleteCommand;
                dataGrid.DeleteCommand += DataGrid_DeleteCommand;
                dataGrid.EditCommand -= DataGrid_EditCommand;
                dataGrid.EditCommand += DataGrid_EditCommand;
                dataGrid.CancelCommand -= DataGrid_CancelCommand;
                dataGrid.CancelCommand += DataGrid_CancelCommand;
                // dataGrid.AllowCustomPaging = true;//
                dataGrid.AllowPaging = true;
                dataGrid.PageIndexChanged -= DataGrid_PageIndexChanged;
                dataGrid.PageIndexChanged += DataGrid_PageIndexChanged;
                //这表达式怎么绑定 页面上可以硬代码怎么搞 难道 初始化时手动绑定?
                TemplateColumn templateColumn = new TemplateColumn();
                dataGrid.Columns.Add(templateColumn);
                TemplateBuilder builder=new TemplateBuilder();
                builder.InstantiateIn(IsAllSelectedCheckBox);
                templateColumn.HeaderTemplate =builder;//尼玛怎么没反应显示不出来
               builder = new TemplateBuilder();
                builder.InstantiateIn(new CheckBox() { ID = "IsSelected", Checked = false});
                templateColumn.EditItemTemplate = builder;
                templateColumn.ItemTemplate = templateColumn.EditItemTemplate;

                foreach (var item in ListTable.Columns)
                {
                    if (item.Name.Equals("IsSelected")) continue;
                    templateColumn = new TemplateColumn() { };
                    dataGrid.Columns.Add(templateColumn);
                    templateColumn.HeaderText = item.Header;
                    switch (item.ColumnType)
                    {
                        case ListModel.ColumnType.TextBox:
                            templateColumn.EditItemTemplate = new TemplateBuilder() { ID = item.Name };
                            templateColumn.EditItemTemplate.InstantiateIn(new TextBox() { ID=item.Name});
                            templateColumn.ItemTemplate = templateColumn.EditItemTemplate;
                            break;
                        case ListModel.ColumnType.Combox:
                            //templateColumn.EditItemTemplate = new TemplateBuilder();
                           // templateColumn.EditItemTemplate.InstantiateIn(new ListBox() { });//数据源绑定
                            break;
                        case ListModel.ColumnType.TextBoxNumber:
                            templateColumn.EditItemTemplate = new TemplateBuilder() { ID= item.Name };
                            templateColumn.EditItemTemplate.InstantiateIn(new TextBox() { });
                            templateColumn.ItemTemplate = templateColumn.EditItemTemplate;
                            break;
                        case ListModel.ColumnType.None:
                        default:
                            templateColumn.EditItemTemplate = new TemplateBuilder() { ID = item.Name };
                            templateColumn.EditItemTemplate.InstantiateIn(new Label() { ID = item.Name });
                            templateColumn.ItemTemplate = templateColumn.EditItemTemplate;
                            break;
                    }
                }
                dataGrid.ItemDataBound += DataGrid_ItemDataBound;
                //dataGrid.ItemCreated += DataGrid_ItemCreated;
                dataGrid.DataSource = DataListDataSource.DataList;
                dataGrid.DataBind();

         
            }
        }

        private void DataGrid_CancelCommand(object source, DataGridCommandEventArgs e)
        {
            dataGrid.EditItemIndex = -1;
        }

        private void DataGrid_EditCommand(object source, DataGridCommandEventArgs e)
        {
            dataGrid.EditItemIndex = (int)e.Item.ItemIndex;
        }

        private void DataGrid_DeleteCommand(object source, DataGridCommandEventArgs e)
        {

        }

        private void DataGrid_ItemCreated(object sender, DataGridItemEventArgs e)
        {
            DataGrid_ItemDataBound(sender,e);
        }

        private void DataGrid_PageIndexChanged(object source, DataGridPageChangedEventArgs e)
        {
            if (e.NewPageIndex>0)
            {
                //do nothing
                dataGrid.CurrentPageIndex = e.NewPageIndex;
            }
        }

        private void DataListDataSource_IsAllSelectedEvent(bool obj)
        {
            //一般都会执行 if 没用
            if(IsAllSelectedCheckBox.Checked!=obj)
            {
                IsAllSelectedCheckBox.Checked = obj;
                DataListDataSource.IsAllSelected = obj;
            }
        }

        /// <summary> 自己触发? </summary>
        private bool _allCustom=false;
        /// <summary>这里应该是多线程环境 没调试过  </summary>
        protected bool _allEventTrigger = false;
        private void CheckBox_CheckedChanged(object sender, EventArgs e)
        {
            //IsAllSelectedCheckBox
            if (sender is CheckBox checkBox)
            {
                _allEventTrigger = true;
                //IsAllSelectedCheckBox.Checked = checkBox.Checked;//自更新 自己没用
                if (checkBox.Checked && _allCustom)
                {
                    return;
                }
                if (dataGrid.Items.Count > 0)
                {
                    foreach (DataGridItem item in dataGrid.Items)
                    {
                        CheckBox check=(CheckBox)item.FindControl("IsSelected");
                        check.Checked = checkBox.Checked;
                    }
                }
                _allEventTrigger = false;
            }
        }

        private void DataGrid_ItemDataBound(object sender, DataGridItemEventArgs e)
        {
            //绑定数据源不然 数据源咋显示 表达式不知道怎么用
           

            if (e.Item.ItemType ==  ListItemType.Header)
            {


            }

            if (e.Item.ItemType == ListItemType.EditItem || e.Item.ItemType == ListItemType.Item)
            {
              
                if(e.Item.Controls[0] is TableCell cell)
                {
                    int i = 0;
                    cell.Text = e.Item.DataItem.GetType().GetProperty("IsSelected").GetValue(e.Item.DataItem)?.ToString();
                    foreach (var item in ListTable.Columns)
                    {
                        i++;
                        if (item.Name == "IsSelected")
                        {
                            continue;
                        }
                        if (e.Item.Controls[i] is TableCell tableCell)
                        {
                            tableCell.Text = e.Item.DataItem.GetType().GetProperty(item.Name).GetValue(e.Item.DataItem)?.ToString();
                        }
                    }
                }
                else
                {
                    CheckBox check = (CheckBox)e.Item.FindControl("IsSelected");
                    check.Checked = (bool)e.Item.DataItem.GetType().GetProperty("IsSelected").GetValue(e.Item.DataItem);
                    check.CheckedChanged += Check_CheckedChanged;
                    foreach (var item in ListTable.Columns)
                    {
                        if (item.Name == "IsSelected")
                        {
                            continue;
                        }
                        Control control = e.Item.FindControl(item.Name);
                        if (control is Label label)
                        {
                            label.Text = e.Item.DataItem.GetType().GetProperty(item.Name).GetValue(e.Item.DataItem)?.ToString();
                        }
                        else if (control is TextBox textBox)
                        {
                            textBox.Text = e.Item.DataItem.GetType().GetProperty(item.Name).GetValue(e.Item.DataItem)?.ToString();
                        }
                        else if (control is ListBox listBox)
                        {
                            //绑定数据源
                            listBox.DataMember = item.DisplayMemberPath;
                            listBox.DataTextField = item.SelectedValuePath;
                            listBox.DataSource = item.Items;
                            listBox.DataBind();
                            listBox.SelectedValue = e.Item.DataItem.GetType().GetProperty(item.Name).GetValue(e.Item.DataItem)?.ToString();
                        }
                        else
                        {
                            //do nothing 
                        }
                    }
                }
            }

        }

        private void Check_CheckedChanged(object sender, EventArgs e)
        {
            //全选 事件   单选事件不触发 会不会有影响了(值是否更新)
            if (!_allEventTrigger&&sender is CheckBox checkBox)
            {
                DataGridItem item = dataGrid.SelectedItem;
                //要么 更新属性 里值 要么这里做逻辑 
                //先不管了
                Label label = (Label)item.FindControl("Id");
                // 单选事件 是否触发 全选事件 逻辑已在里面
                _allCustom = false;
                DataListDataSource.SetSelected(Convert.ChangeType(label.ID, ListTable.IdType), checkBox.Checked);
                _allCustom = true;
            }

        }
    }
}
