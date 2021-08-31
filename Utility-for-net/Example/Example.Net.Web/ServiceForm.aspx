<%@ Page Language="C#" AutoEventWireup="true" Debug="true" CodeBehind="ServiceForm.aspx.cs" Inherits="Config.Web.ServiceForm,NetframeworkWeb.Admin" %>
<%@ Import Namespace="System.Runtime" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <p>GridView操作示例</p>
            <asp:GridView runat="server" ID="gv" AutoGenerateColumns="False" Caption="服务信息" 
                 CellPadding="4" ForeColor="#333333" OnRowEditing="gv_RowEditing" OnRowDeleting="gv_RowDeleting" 
                OnRowCancelingEdit="gv_RowCancelingEdit" OnRowUpdating="gv_RowUpdating"
                GridLines="None" >
                <FooterStyle BackColor="#990000" Font-Bold="True" ForeColor="White" />
                <HeaderStyle BackColor="#990000" Font-Bold="True" ForeColor="White" />
                <PagerStyle BackColor="#FFCC66" ForeColor="#333333" HorizontalAlign="Center" />
                <RowStyle BackColor="#FFFBD6" ForeColor="#333333" />
                <SelectedRowStyle BackColor="#FFCC66" Font-Bold="True" ForeColor="Navy" />
                <SortedAscendingCellStyle BackColor="#FDF5AC" />
                <SortedAscendingHeaderStyle BackColor="#4D0000" />
                <SortedDescendingCellStyle BackColor="#FCF6C0" />
                <SortedDescendingHeaderStyle BackColor="#820000" />
                <Columns>
                    <asp:TemplateField>
                        <ItemTemplate>
                            <asp:CheckBox runat="server" />
                        </ItemTemplate>
                        <HeaderTemplate>
                            <asp:CheckBox runat="server" />
                        </HeaderTemplate>
                    </asp:TemplateField>
                    
                    <asp:TemplateField HeaderText="名称">
                        <ItemTemplate><%#  Eval("Name") %></ItemTemplate>
                        <EditItemTemplate>
                            <asp:TextBox runat="server" Text='<%#  Eval("Name") %>' Enabled="false" id="Name" ></asp:TextBox>
                        </EditItemTemplate>
                    </asp:TemplateField>
                     <asp:TemplateField HeaderText="编号">
                        <ItemTemplate><%#  Eval("Id") %></ItemTemplate>
                    </asp:TemplateField>
                     <asp:TemplateField HeaderText="ip地址">
                        <ItemTemplate><%#  Eval("Ip") %></ItemTemplate>
                    </asp:TemplateField>
                     <asp:TemplateField HeaderText="端口">
                        <ItemTemplate><%#  Eval("Port") %></ItemTemplate>
                    </asp:TemplateField>
                     <asp:TemplateField HeaderText="状态">
                        <ItemTemplate><%#  Eval("Status") %></ItemTemplate>
                    </asp:TemplateField>
                      <asp:TemplateField HeaderText="创建时间">
                        <ItemTemplate><%#  Eval("CreateDate") %></ItemTemplate>
                    </asp:TemplateField>
                     <asp:TemplateField HeaderText="修改时间">
                        <ItemTemplate><%#  Eval("LastDate") %></ItemTemplate>
                    </asp:TemplateField>
                <%--    <asp:BoundField DataField="Id" HeaderText="编号" HtmlEncode="False" SortExpression="Id" />--%>
                   <%-- <asp:BoundField DataField="Ip" HeaderText="ip地址" HtmlEncode="False" SortExpression="Ip" />
                    <asp:BoundField DataField="Port" HeaderText="端口" HtmlEncode="False" SortExpression="Port" />--%>
                   <%-- <asp:BoundField DataField="Status" HeaderText="状态" HtmlEncode="False" SortExpression="Status" />
                    <asp:BoundField DataField="CreateDate" HeaderText="创建时间" HtmlEncode="False" SortExpression="CreateDate" />
                    <asp:BoundField DataField="LastDate" HeaderText="修改时间" HtmlEncode="False" SortExpression="LastDate" />--%>
                    <asp:TemplateField HeaderText="操作">
                        <ItemTemplate>
                            <asp:Button runat="server" ID="edit"  Text="编辑" CommandName="edit"  />
                            <asp:Button runat="server" ID="save" Text="保存" Visible="false" CommandName="update"  />
                            <asp:Button runat="server" ID="cancel" Text="取消" Visible="false" CommandName="cancel"  />
                            <asp:Button runat="server" ID="delete" Text="删除" CommandName="delete"  />
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
        </div>

        <div style="margin-top: 50px">
            <p>DataGrid操作示例</p>
            <asp:DataGrid runat="server" ID="dg" OnDeleteCommand="dg_DeleteCommand" 
                OnCancelCommand="dg_CancelCommand" OnEditCommand="dg_EditCommand"
                OnUpdateCommand="dg_UpdateCommand"
                AutoGenerateColumns="false">
                <Columns>
                    <asp:TemplateColumn>
                        <ItemTemplate>
                            <asp:CheckBox runat="server" />
                        </ItemTemplate>
                        <HeaderTemplate>
                             <asp:CheckBox runat="server" />
                        </HeaderTemplate>
                    </asp:TemplateColumn>
                   <asp:TemplateColumn HeaderText="名称">
           <%--             <ItemTemplate><%#  Eval("Name") %></ItemTemplate>--%>
                      <%-- <EditItemTemplate></EditItemTemplate>--%>
                       <%-- <ItemTemplate>
                            <asp:TextBox runat="server" Text='<%#  Eval("Name") %>' Enabled="false" id="Name" ></asp:TextBox>
                        </ItemTemplate>--%>
                   </asp:TemplateColumn>
                    <%--                 <%# Container.DataItem("Name") %>--%>
                     <asp:BoundColumn DataField="Name" SortExpression="Name" HeaderText="名称"></asp:BoundColumn>
                    <asp:BoundColumn DataField="Id" SortExpression="Id" HeaderText="编号"></asp:BoundColumn>
                    <asp:BoundColumn DataField="Ip" SortExpression="Ip" HeaderText="ip地址"></asp:BoundColumn>
                    <asp:BoundColumn DataField="Port" SortExpression="Port" HeaderText="端口"></asp:BoundColumn>
                    <asp:BoundColumn DataField="Status" SortExpression="Status" HeaderText="状态"></asp:BoundColumn>
                    <asp:BoundColumn DataField="CreateDate" SortExpression="CreateDate" HeaderText="创建时间"></asp:BoundColumn>
                    <asp:BoundColumn DataField="LastDate" SortExpression="LastDate" HeaderText="修改时间"></asp:BoundColumn>
                    <asp:TemplateColumn HeaderText="操作">
                        <ItemTemplate>
                            <asp:Button runat="server" Text="编辑" ID="edit1" CommandName="edit" />
                            <asp:Button runat="server" Text="保存" ID="save1" Visible="false" CommandName="update" />
                            <asp:Button runat="server" Text="取消" ID="cancel1" Visible="false" CommandName="cancel" />
                            <asp:Button runat="server" Text="删除" ID="delete1" CommandName="delete" />
                        </ItemTemplate>
                    </asp:TemplateColumn>
                </Columns>
                <FooterStyle BackColor="#990000" Font-Bold="True" ForeColor="White" />
                <HeaderStyle BackColor="#990000" Font-Bold="True" ForeColor="White" />
                <ItemStyle BackColor="#FFFBD6" ForeColor="#333333" />
                <PagerStyle BackColor="#FFCC66" ForeColor="#333333" HorizontalAlign="Center" />
                <SelectedItemStyle BackColor="#FFCC66" Font-Bold="True" ForeColor="Navy" />
            </asp:DataGrid>
        </div>

        <div style="margin-top: 50px">
            <p>DataList操作示例</p>
           
                <asp:DataList runat="server" ID="dl" OnCancelCommand="dl_CancelCommand"  
                    OnDeleteCommand="dl_DeleteCommand" OnUpdateCommand="dl_UpdateCommand"
                    OnEditCommand="dl_EditCommand"       >
                    <HeaderTemplate>
                        <table>
                            <tr>
                                <th>
                                    <asp:CheckBox runat="server" ID="dl_AllSelected" /></th>
                                <td>名称</td>
                                <td>编号</td>
                                <td>ip地址</td>
                                <td>端口</td>
                                <td>状态</td>
                                <td>创建时间</td>
                                <td>修改时间</td>
                                <th>操作</th>
                            </tr>
                    </HeaderTemplate>
                    <EditItemTemplate>
                         <tr>   
                             <td> <asp:CheckBox runat="server" ID="dl_Selected" /></td>
                                <td> <asp:TextBox runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Name") %>'></asp:TextBox></td>
                             <!-- 该数据不会出现 但其他组合又出现 奇怪 -->
               <%--                 <td><%#  DataBinder.Eval(Container.DataItem,"Id")%></td>--%>
                                <td> <asp:TextBox runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Id") %>'></asp:TextBox></td>
                                <td> <asp:TextBox runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Ip") %>'></asp:TextBox></td>
                                <td> <asp:TextBox runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Port") %>'></asp:TextBox></td>
                                <td> <asp:TextBox runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Status") %>'></asp:TextBox></td>
                                <td> <asp:TextBox runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "CreateDate") %>'></asp:TextBox></td>
                                <td> <asp:TextBox runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "LastDate") %>'></asp:TextBox></td>
                            <td>
                                <asp:Button runat="server" Text="编辑" ID="edit2" CommandName="edit" />
                                <asp:Button runat="server" Text="保存" ID="save2" Visible="false" CommandName="update" />
                                <asp:Button runat="server" Text="取消" ID="cancel2" Visible="false" CommandName="cancel" />
                                <asp:Button runat="server" Text="删除" ID="delete2" CommandName="delete" />
                            </td>
                        </tr>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <tr>
                            <td> <asp:CheckBox runat="server" ID="dl_Selected" /></td>
                            <td> <%# DataBinder.Eval(Container.DataItem, "Name") %></td>
                            <td><%#  Eval("Id")%></td>
                            <td><%#  Eval("Ip")%></td>
                            <td><%#  Eval("Port")%></td>
                            <td><%#  Eval("Status")%></td>
                            <td><%#  Eval("CreateDate")%></td>
                            <td><%#  Eval("LastDate")%></td>
                            <td>
                                <asp:Button runat="server" Text="编辑" ID="edit2" CommandName="edit" />
                                <asp:Button runat="server" Text="保存" ID="save2" Visible="false" CommandName="update" />
                                <asp:Button runat="server" Text="取消" ID="cancel2" Visible="false" CommandName="cancel" />
                                <asp:Button runat="server" Text="删除" ID="delete2" CommandName="delete" />
                            </td>
                        </tr>
                    </ItemTemplate>
                    <FooterTemplate>
                        </table>
                    </FooterTemplate>
                </asp:DataList>

        </div>

        <div>
            <p>自定义表格 </p>
            <table>
                <thead>
                    <tr>
                        <td>名称</td>
                        <td>编号</td>
                        <td>ip地址</td>
                        <td>端口</td>
                        <td>状态</td>
                        <td>创建时间</td>
                        <td>修改时间</td>
                    </tr>
                </thead>
                <tbody>
                    <!-- 其他操作时数据没有了 除非重新加载 没啥用 -->
                    <%
                        if(serviceModels!=null)
                        {
                              //自定义代码
                            foreach (var item in serviceModels)
                            { %>
                                <tr>
                                    <td><% =item.Name %></td>
                                    <td><% =item.Id %></td>
                                    <td><%= item.Ip %></td>
                                    <!-- 提示错误不用管 -->
                                    <td><% = item.Port %></td>
                                    <td><%=  item.Status %></td>
                                    <td><% = item.CreateDate %></td>
                                    <td><% = item.LastDate  %></td>
                                </tr>
                        <%
                            }
                        }
                    %>
                </tbody>
            </table>

        </div>

    </form>
</body>
</html>
