<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="book.aspx.cs" Inherits="KlubNaCitateli.Sites.book" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
<link href="../Styles/book.css" type="text/css" rel="Stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="mainContent" runat="server">

    <form id="form1" runat="server">
    <div id="bookInfo">
        <div id="image">
            <asp:Image ID="Image1" runat="server" Height="200px" Width="163px" />
            <br />
            <asp:BulletedList ID="BulletedList1" CssClass="ul" runat="server">
                <asp:ListItem></asp:ListItem>
            </asp:BulletedList>
            <asp:BulletedList ID="BulletedList2" CssClass="ul" runat="server">
                <asp:ListItem></asp:ListItem>
            </asp:BulletedList>
            <asp:BulletedList ID="BulletedList3" CssClass="ul" runat="server">
                <asp:ListItem></asp:ListItem>
            </asp:BulletedList>
            <asp:BulletedList ID="BulletedList4" CssClass="ul" runat="server">
                <asp:ListItem></asp:ListItem>
            </asp:BulletedList>
            <asp:BulletedList ID="BulletedList5" CssClass="ul" runat="server">
                <asp:ListItem></asp:ListItem>
            </asp:BulletedList>
            <asp:Button ID="Button1" runat="server" Text="Button" />
        </div>
        <div id="info">
        
        </div>
        <div id="links">
        
        </div>
    </div>
    <div id="recomendation">

    </div>
    <div id="comments">
    
    </div>
    </form>
</asp:Content>
