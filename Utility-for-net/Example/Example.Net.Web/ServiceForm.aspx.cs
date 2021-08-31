using System;
using System.Collections.Generic;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Web.UI;
using Utility.Model;
using Config.Domain.Repositories;
using Config.Domain.Entities;
using Config.Nhibernate.Repositories;

namespace Example.Web
{
    /**
     <%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WebForm1.aspx.cs" Inherits="Example.Web.WebForm1" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
        </div>
    </form>
</body>
</html>
     */
    /// <summary>自动生成的 .ashx.cs 文件 类库没有 需要复制过来 编辑时文本框值都没了 如果需要 需要手动赋值(自己去实现) 大致处理过程已完成 业务逻辑以及判断自己去实现   </summary>
    public partial class ServiceForm : System.Web.UI.Page
    {
        private IConfigManager configManager;//写死
        public List<ServiceEntity> serviceModels;

        public ServiceForm(ConfigManager configNhibernateManager)
        {
            configManager = configNhibernateManager;
        }
        public ServiceForm()
        {
            configManager = MvcApplication.IocManager.Get<IConfigManager>();
        }
        static readonly ServiceEntity Empty = new ServiceEntity();
        public ListModel ListModel { get; set; }
        public void LoadData(bool refersh = false)
        {
            if (refersh)
            {
                serviceModels = configManager.Service.FindListByEntity(Empty);
                Cache["Service"] = serviceModels;
            }
            if (serviceModels == null)
            {
                var obj=Cache["Service"];
                if (obj == null)
                {
                    serviceModels = configManager.Service.FindListByEntity(Empty);
                    Cache["Service"] = serviceModels;
                }
                else
                {
                    serviceModels = obj as List<ServiceEntity>;
                }
            }
   
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                GridViewDataBind();

                dg.DataSource = serviceModels;
                dg.DataBind();

                DataListDataBind();
            }
        }
        protected void Page_Load1(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
               // ListModel = ServiceConfig.ListModels[0];
                //怎么换行了 
                //查询表单
                foreach (var item in ListModel.Columns)
                {
                    switch (item.ColumnType)
                    {
                        case ListModel.ColumnType.Combox:
                            break;
                        case ListModel.ColumnType.TextBoxNumber:
                        case ListModel.ColumnType.TextBox:
                        case ListModel.ColumnType.None:
                        default:
                            Label label = new Label() { ID = item.Name, Text = item.Header };
                            this.Form.Controls.Add(label);
                            label.Style.Add("margin", "0px,0px,10px,0px");
                           
                            TextBox textBox = new TextBox() { ID=item.Name,Text=item.Header };
                            textBox.Style.Add("margin","0px,0px,10px,0px");
                            this.Form.Controls.Add(textBox);
                            break;
                    }

                }
                var signatureDiv = new HtmlGenericControl()
                {
                    ID = "signature",
                    TagName = "div",
                    
                };
                signatureDiv.Style.Add("margin", "50px 100px;;");
                // DivControl divControl = new DivControl();
                //真麻烦
                var data = configManager.Service.FindListByEntity(Empty);
                List<IIsSelected> selecteds = new List<IIsSelected>();
                foreach (var item in data)
                {
                  //  selecteds.Add(item);
                }
                DataListDataSource dataListDataSource = new DataListDataSource() { DataList= selecteds };
                signatureDiv.Controls.Add(new DataGridCtrl() { ListTable= ListModel,DataListDataSource= dataListDataSource });

                this.Form.Controls.Add(signatureDiv);//只能继承了？
            }
        }

        private void GridViewDataBind()
        {
            LoadData();
            gv.DataSource = serviceModels;
            gv.DataBind();
        }
        private void DataListDataBind()
        {
            LoadData();
            dl.DataSource = serviceModels;
            dl.DataBind();
        }
        #region GridView
        protected void gv_RowEditing(object sender, GridViewEditEventArgs e)
        {
            gv.EditIndex = e.NewEditIndex;
            //文本框需要点击2次才能进入
            DataGridSet(e.NewEditIndex);
        }
        /// <summary>删除按钮触发该事件 </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void gv_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            if (gv.EditIndex > -1)
            {

            }
        }

        /// <summary>取消按钮触发该事件 </summary>
        protected void gv_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            DataGridSet(e.RowIndex, false);
            gv.EditIndex = -1;
            e.Cancel = true;
        }
        /// <summary>保存按钮触发该事件 </summary>
        protected void gv_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {

        }

        private void DataGridSet(int index,bool isEdit=true)
        {
            if (index > -1)
            {
                //固定死的位置
                bool ed =false;
                if (isEdit)
                {
                    Control control = gv.Rows[gv.EditIndex].Controls[1];
                    Control text = control.FindControl("Name");
                    if (text != null && text is TextBox textBox)
                    {//文本框需要点击2次才能进入 强制出现才进入
                        textBox.Enabled = true;
                        ed = true;
                    }
                }
                else
                {
                    Control control = gv.Rows[index].Controls[1];
                    Control text = control.FindControl("Name");
                    if (text != null && text is TextBox textBox)
                    {
                        //文本框需要点击2次才能消失 强制消失才进入

                    }
                    else
                    {
                        ed = true;// 为何文本框还在
                    }
                }
                if (ed)
                {
                    Control control1 = gv.Rows[index].Controls[gv.Columns.Count - 1];
                    //怎么没反应  Visible
                    Control edit = control1.FindControl("edit");
                    if (edit != null && edit is Button edit1)
                    {
                        edit1.Visible = !isEdit;
                    }
                    Control save = control1.FindControl("save");
                    if (save != null && save is Button svae1)
                    {
                        svae1.Visible = isEdit;
                    }
                    Control cacel = control1.FindControl("cancel");
                    if (cacel != null && cacel is Button cacel1)
                    {
                        cacel1.Visible = isEdit;
                    }
                    // GridViewDataBind(); //重新加载没效果了
                    //文本框没有值了 怎么赋值
                    if (!isEdit)
                    {
                        //文本框消失值不见了
                        GridViewDataBind();
                    }
                }
            }
        }

        #endregion GridView

        #region DataGrid 有问题 触发2次才出现文本框 无论哪个事件

        protected void dg_DeleteCommand(object source, DataGridCommandEventArgs e)
        {
            if (e.Item.ItemIndex > -1)
            {

            }
        }

        protected void dg_CancelCommand(object source, DataGridCommandEventArgs e)
        {
            DataGridSet(e,false);
          
        }

        protected void dg_EditCommand(object source, DataGridCommandEventArgs e)
        {
            DataGridSet(e);
        }
        protected void dg_UpdateCommand(object source, DataGridCommandEventArgs e)
        {
            if (e.Item.ItemIndex > -1)
            {

            }
        }

        private void DataGridSet(DataGridCommandEventArgs e,bool isEdit=true)
        {
            dg.EditItemIndex = isEdit?e.Item.ItemIndex:-1;
            if (e.Item.ItemIndex > -1)
            {
                bool ed = false;
                if (isEdit)
                {
                    //文本框需要点击2次才能进入 强制出现才进入
                    Control control = e.Item.Controls[2];
                    if(control.Controls.Count>0)
                    {
                        Control text = control.Controls[0];
                        if (text != null && text is TextBox textBox)
                        {
                            ed = true;
                        }
                    }
                }
                else
                {  //文本框需要点击2次才能取消 强制消失才进入
                    Control control = e.Item.Controls[2];
                    if (control.Controls.Count > 0)
                    {
                        Control text = control.Controls[0];
                        if (text != null && text is TextBox textBox)
                        {
                            ed = false;//文本框需要点击2次才能取消 强制消失才进入
                        }
                        else
                        {
                            ed = true;
                        }
                    }
                    else
                    {
                        ed = true;
                    }
                }
                if (ed)
                {
                    //固定死的位置
                    Control control1 = e.Item.Controls[dg.Columns.Count - 1];
                    //需要点击1次才能进入
                    Control edit = control1.FindControl("edit1");
                    if (edit != null && edit is Button edit1)
                    {
                        edit1.Visible = !isEdit;
                    }
                    Control save = control1.FindControl("save1");
                    if (save != null && save is Button svae1)
                    {
                        svae1.Visible = isEdit;
                    }
                    Control cacel = control1.FindControl("cancel1");
                    if (cacel != null && cacel is Button cacel1)
                    {
                        cacel1.Visible = isEdit;
                    }
                    // GridViewDataBind(); //重新加载没效果了
                    //文本框没有值了 怎么赋值
                }
            }
        }
       
        #endregion DataGrid

        #region DataList
        protected void dl_CancelCommand(object source, DataListCommandEventArgs e)
        {
     
            DataListSet(e,false);//取消后数据没有了需要重新加载
        }

        protected void dl_DeleteCommand(object source, DataListCommandEventArgs e)
        {
            if (e.Item.ItemIndex > -1)
            {

            }
        }

        protected void dl_UpdateCommand(object source, DataListCommandEventArgs e)
        {
            if (e.Item.ItemIndex > -1)
            {
            }
        }

        protected void dl_EditCommand(object source, DataListCommandEventArgs e)
        {
            if (e.Item.ItemIndex > -1)
            {
                DataListSet(e, true);
            }
        }
        private void DataListSet(DataListCommandEventArgs e,bool isEdit=true)
        {
            dl.EditItemIndex = isEdit? e.Item.ItemIndex:- 1;
            if (e.Item.ItemIndex > -1)
            {
                bool ed =false;
                //触发2次才出现文本框  触发2次才消失文本框
                if (isEdit)
                {
                    if (e.Item.Controls.Count >= 24)//26
                    {
                        ed = true;//第2次编辑才有文本框 请求再次触发事件
                    }
                }
                else
                {
                    if (e.Item.Controls.Count >= 24)//26
                    {
                       //第一次取消还有文本框 请求再次触发事件
                    }
                    else
                    {
                        ed = true;
                    }
                }
                if (ed )
                {
                    //需要点击1次才能进入
                    Control edit = e.Item.FindControl("edit2");
                    if (edit != null && edit is Button edit1)
                    {
                        edit1.Visible = !isEdit;
                    }
                    Control save = e.Item.FindControl("save2");
                    if (save != null && save is Button svae1)
                    {
                        svae1.Visible = isEdit;
                    }
                    Control cacel = e.Item.FindControl("cancel2");
                    if (cacel != null && cacel is Button cacel1)
                    {
                        cacel1.Visible = isEdit;
                    }
                    // GridViewDataBind(); //重新加载没效果了
                    //文本框没有值了 怎么赋值
                    if (!isEdit)
                    {
                        DataListDataBind();//取消后数据没有了需要重新加载
                    }
                }

            }
        }
        #endregion DataList
    }
    public class DivControl: HtmlControl
    {
        public DivControl():base("div")
        {

        }
        protected override void CreateChildControls()
        {
            base.CreateChildControls();
        }
        public override bool HasControls()
        {
            return base.HasControls();
        }
        protected override ControlCollection CreateControlCollection()
        {
            return base.CreateControlCollection();
        }
    }
}
