<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="WebForm1.aspx.cs" Inherits="Example.Web.WebForm1" %>
<asp:Content ID="cssOrJsSource" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="body" ContentPlaceHolderID="contentPlaceHolder" runat="server">
      <asp:SiteMapPath ID="SiteMapPath1" runat="server"></asp:SiteMapPath>
        <asp:TreeView ID="TreeView1" runat="server"></asp:TreeView>
        <asp:Menu ID="Menu1" runat="server">
            <Items>
                <asp:MenuItem Text="新建项" Value="新建项"></asp:MenuItem>
                <asp:MenuItem Text="新建项" Value="新建项"></asp:MenuItem>
                <asp:MenuItem Text="新建项" Value="新建项"></asp:MenuItem>
            </Items>
        </asp:Menu>
    
        <div>
            <asp:Timer ID="timer" runat="server"></asp:Timer>
           
            <asp:ScriptManager ID="scriptManage" runat="server">
                
            </asp:ScriptManager>
             <asp:UpdatePanel runat="server" >
              
             </asp:UpdatePanel>
            <asp:ScriptManagerProxy ID="scriptManagerProxy" runat="server">

            </asp:ScriptManagerProxy>
            <asp:UpdateProgress ID="updateProgress" runat="server">

            </asp:UpdateProgress>
        </div>
       
        <asp:LoginView ID="loginView" runat="server">
            <AnonymousTemplate>

            </AnonymousTemplate>
            <LoggedInTemplate>

            </LoggedInTemplate>
            <RoleGroups>
              
            </RoleGroups>
        </asp:LoginView>
</asp:Content>

