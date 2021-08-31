
using Config.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Utility.Domain.Repositories;

namespace Example.Web
{
    public partial class Default : System.Web.UI.Page
    {
        private IRepository<ConfigEntity,string> configDAL { 
            get {
                if (configDAL1 == null)
                {
                    configDAL1 = MvcApplication.IocManager.Get<IRepository<ConfigEntity, string>>();
                }
                return configDAL1;
            }
            set => configDAL1 = value;
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!base.IsPostBack)
            {
                configDAL = MvcApplication.IocManager.Get<IRepository<ConfigEntity, string>>();
                BindConfigDatasource();
            }
        }
        static readonly ConfigEntity EmptyConfigModel = new ConfigEntity();
        private IRepository<ConfigEntity,string> configDAL1;

        private void BindConfigDatasource()
        {
            ConfigListView.DataSource = configDAL.FindListByEntity(EmptyConfigModel);
            ConfigListView.DataBind();
        }

        protected void ConfigListView_ItemDeleting(object sender, ListViewDeleteEventArgs e)
        {
            string id = ConfigListView.DataKeys[e.ItemIndex].Value.ToString();
            if (configDAL.Delete(id) > 0)
            {
                Response.Write("<script>alert('删除成功');</script>");
                BindConfigDatasource();
            }
            else
                Response.Write("<script>alert('删除失败');</script>");
        }

        protected void ConfigListView_ItemEditing(object sender, ListViewEditEventArgs e)
        {
            ConfigListView.EditIndex = e.NewEditIndex;

        }

        protected void ConfigListView_ItemUpdating(object sender, ListViewUpdateEventArgs e)
        {
            string id = ConfigListView.DataKeys[e.ItemIndex].Value.ToString();
            TextBox name = (TextBox)ConfigListView.Items[e.ItemIndex].FindControl("Name");
            TextBox address = (TextBox)ConfigListView.Items[e.ItemIndex].FindControl("Address");
            TextBox flag = (TextBox)ConfigListView.Items[e.ItemIndex].FindControl("Flag");
            TextBox user = (TextBox)ConfigListView.Items[e.ItemIndex].FindControl("User");
            TextBox pwd = (TextBox)ConfigListView.Items[e.ItemIndex].FindControl("Pwd");
            ConfigEntity configModel = new ConfigEntity() { Id = id, Name = name.Text, Flag = flag.Text, AddressTemplate = address.Text, User = user.Text, Pwd = pwd.Text, LastDate = DateTime.Now };
            if (configDAL.Update(configModel) > 0)
            {
                ConfigListView.EditIndex = -1;
                BindConfigDatasource();
            }
            else
                Response.Write("<script>alert('更新失败')</script>");
        }

        protected void ConfigListView_ItemCanceling(object sender, ListViewCancelEventArgs e)
        {
            if (e.CancelMode == ListViewCancelMode.CancelingEdit)
            {
                ConfigListView.EditIndex = -1;

            }
        }
    }
}