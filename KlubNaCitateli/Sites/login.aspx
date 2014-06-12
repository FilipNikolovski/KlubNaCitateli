<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="login.aspx.cs" Inherits="KlubNaCitateli.Sites.login" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="../Styles/login.css" rel="stylesheet" type="text/css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="mainContent" runat="server">
    <div id="main">

    <asp:Label ID="Label1" runat="server" Text="Log In" CssClass="login"></asp:Label>

    <div id="logInForm">
        
        <asp:Table ID="Table1" runat="server">
        <asp:TableRow ID="TableRow1" runat="server" CssClass=".TableRow">
        <asp:TableCell ID="TableCell1" runat="server" CssClass="tabCell">
            <asp:Label ID="Label2" runat="server" Text="Enter your username or email:"></asp:Label>
            </asp:TableCell>
        </asp:TableRow>
        <asp:TableRow ID="TableRow2" runat="server" CssClass=".TableRow">
            <asp:TableCell ID="TableCell2" runat="server" CssClass="tabCell">
            <asp:TextBox ID="username" runat="server" CssClass="tabBox">
            </asp:TextBox><br />
            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" 
            ErrorMessage="You must enter username or email!" ControlToValidate="username"></asp:RequiredFieldValidator>
            </asp:TableCell>
        </asp:TableRow>
        <asp:TableRow ID="TableRow3" runat="server" CssClass=".TableRow">
             <asp:TableCell ID="TableCell3" runat="server" CssClass="tabCell">
            <asp:Label ID="Label3" runat="server" Text="Enter your password:"></asp:Label>
            </asp:TableCell>
        </asp:TableRow>
        <asp:TableRow ID="TableRow4" runat="server" CssClass=".TableRow">
        <asp:TableCell ID="TableCell4" runat="server" CssClass="tabCell">
            <asp:TextBox ID="password" runat="server" CssClass="tabBox" TextMode="Password">
            </asp:TextBox><br />
            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" 
            ErrorMessage="You must enter password!" ControlToValidate="password"></asp:RequiredFieldValidator>
            </asp:TableCell>
        </asp:TableRow>
         <asp:TableRow ID="TableRow5" runat="server" CssClass=".TableRow">
        <asp:TableCell ID="TableCell5" runat="server" CssClass="tabCell">
            <asp:Button ID="Button1" runat="server" Text="Log In" CssClass="logInButt" OnClick="logIn_click"/>
            <br />
            <asp:Label ID="loginInfo" runat="server"></asp:Label>
            </asp:TableCell>
        </asp:TableRow>

        </asp:Table>
    </div>




</div>
</asp:Content>
