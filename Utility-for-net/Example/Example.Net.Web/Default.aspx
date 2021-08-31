<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Site.Master" CodeBehind="Default.aspx.cs" Inherits="Web.Admin.Default" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
   
    <h3>配置信息(ListView示例)</h3>
    <asp:ListView runat="server" ID="ConfigListView" OnItemDeleting="ConfigListView_ItemDeleting" OnItemEditing="ConfigListView_ItemEditing" DataKeyNames="Id"
         OnItemUpdating="ConfigListView_ItemUpdating" OnItemCanceling="ConfigListView_ItemCanceling" >
        <LayoutTemplate>
            <table border="1" cellspacing="2">
                <thead>
                    <th><asp:CheckBox ID="AllCheck" runat="server" Checked="false" /></th>
                    <th width="100" align="center">编号</th>
                    <th width="100" align="center">名称</th>
                    <th width="100" align="center">地址</th>
                    <th width="100" align="center">标识</th>
                    <th width="100" align="center">账户</th>
                    <th width="100" align="center">密码</th>
                    <th width="300" align="center">创建时间</th>
                    <th width="300" align="center">修改时间</th>
                    <th width="200" align="center">操作</th>
                </thead>
                <tbody>
                    <asp:PlaceHolder ID="itemPlaceholder" runat="server" />  
                </tbody>
                <tfoot>
                     <asp:DataPager ID="DataPager1" runat="server">
                        <Fields>
                          <asp:NumericPagerField />
                        </Fields>
                     </asp:DataPager>
                </tfoot>
            </table>

        </LayoutTemplate>
        <ItemTemplate>
             <tr>
                  <td><input type="checkbox"  /></td>
                    <td width="100" align="center"><%# DataBinder.Eval(Container.DataItem, "Id") %></td>
                    <td width="100" align="center"><%# Eval("Name") %></td>
                    <td width="100" align="center"><%# DataBinder.Eval(Container.DataItem, "Address") %></td>
                    <td width="100" align="center"><%# Eval("Flag") %></td>
                    <td width="100" align="center"><%# Eval("User") %></td>
                    <td width="100" align="center"><%# Eval("Pwd") %></td>
                    <td width="300" align="center"><%# Eval("CreateDate") %></td>
                    <td width="300" align="center"><%# Eval("LastDate") %></td>
                    <td width="200" align="center">
                        <asp:Button runat="server" ID="Delete" CommandName="Delete" Text="删除" OnClientClick="return confirm('确认删除?')" />
                        <asp:Button runat="server" ID="Edit" CommandName="Edit" Text="编辑"  />
                    </td>
             </tr>

        </ItemTemplate>
        <EditItemTemplate >
           <tr>
                <td><input type="checkbox"  /></td>
                    <td width="100" align="center"><%#("Id") %></td>
                    <td width="100" align="center"><asp:TextBox runat="server" ID="Name" Text='<%#Eval("Name")%>'></asp:TextBox></td>
                    <td width="100" align="center"><asp:TextBox runat="server" ID="Address" Text='<%#Eval("Address")%>'></asp:TextBox></td>
                    <td width="100" align="center"><asp:TextBox runat="server" ID="Flag" Text='<%#Eval("Flag")%>'></asp:TextBox></td>
                    <td width="100" align="center"><asp:TextBox runat="server" ID="User" Text='<%#Eval("User")%>'></asp:TextBox></td>
                    <td width="100" align="center"><asp:TextBox runat="server" ID="Pwd" Text='<%#Eval("Pwd")%>'></asp:TextBox></td>
                    <td width="300" align="center"><%# Eval("CreateDate") %></td>
                    <td width="300" align="center"><%# Eval("LastDate") %></td>
                     <td width="200" align="center">
                        <asp:Button runat="server" ID="Update" CommandName="Update" Text="修改" />
                        <asp:Button runat="server" ID="Cancel" CommandName="Cancel"  Text="取消" />
                    </td>
           </tr>
        </EditItemTemplate>
    </asp:ListView>
</asp:Content>
