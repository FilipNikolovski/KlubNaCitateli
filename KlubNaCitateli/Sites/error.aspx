<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="error.aspx.cs" Inherits="KlubNaCitateli.Sites.error" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .errorContainer 
        {
            font-size:50px;
            color: #1947D1;
            height:400px;
            padding:40px;
        }
        .errorContainer p 
        {
            margin:0px;
            width:50%;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="mainContent" runat="server">
    <div class="errorContainer">
        <p>404 Page not found</p>
        <img src="../Images/book-missing.png" alt="404 page not found" />
    </div>
</asp:Content>
