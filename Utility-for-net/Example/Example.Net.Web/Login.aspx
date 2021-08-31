<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="Example.Web.Login" %>
<asp:Content ID="cssOrJsSource" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="body" ContentPlaceHolderID="contentPlaceHolder" runat="server">
     
        <div class="login"> 
            <asp:LoginStatus ID="loginStatus" runat="server" OnLoggedOut="loginStatus_LoggedOut" OnLoggingOut="loginStatus_LoggingOut" />
            <asp:Login ID="login1" runat="server" OnAuthenticate="login1_Authenticate" OnLoggedIn="login1_LoggedIn" OnLoggingIn="login1_LoggingIn" OnLoginError="login1_LoginError"  ></asp:Login>
           

        </div>

        <div class="reg">

             <asp:CreateUserWizard runat="server">
                <wizardsteps> <asp:CreateUserWizardStep runat="server"/> <asp:CompleteWizardStep runat="server"/> </wizardsteps>
            </asp:CreateUserWizard>
        </div>

        <div class="forget">
            <asp:PasswordRecovery ID="passwordRecovery" runat="server"></asp:PasswordRecovery>
        </div>

         <div class="changePassword">
          <asp:LoginName ID="loginName" runat="server" />
          <asp:ChangePassword ID="changePassword" runat="server">

           </asp:ChangePassword>
        </div>
</asp:Content>



