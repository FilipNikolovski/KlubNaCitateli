<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="book.aspx.cs" Inherits="KlubNaCitateli.Sites.book" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="../Styles/book.css" type="text/css" rel="Stylesheet" />
    <style type="text/css">
        #list
        {
            width: 167px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="mainContent" runat="server">
<div id="content">
    <div id="div1">
        <div id="info">
            <div id="image_stars">
                <div id="image">
                    <asp:Image ID="imgBook" runat="server" Height="159px" Width="129px" />
                </div>
                <div id="stars"></div>
                <ul>
                    <li></li>
                    <li></li>
                    <li></li>
                    <li></li>
                    <li></li>
                </ul>
                <div id="button">
                    <asp:Button ID="btnAddFavourites" runat="server" Text="Button" />
                </div>
            </div>
            <div id="about">
                <div id="name">
                <span>
                    <asp:Label ID="lblNameBook" runat="server" Text="Ime Kniga"></asp:Label>
                </span>
                    <br /> 
                        <label>by</label>
                    <br />
                    <span>
                        <asp:Label ID="lblNameAuthor" runat="server" Text="Ime Aftor"></asp:Label>
                    </span>
                </div>
                <div id="description">
                    <label>Description</label>
                    <br />
                    <div id="text_description">
                        <asp:Panel ID="pnlDescription" runat="server" Height="115px">
                        j;asnasjdasdnaiodason</asp:Panel>
                    </div>
                </div>
            </div>
                <div id="list">
                </div>
            <div id="links">
                <label>Download/By Links</label>
                </div>
        </div>
        <div id="infoTags">
            <div id="info2">
            </div>
            <div id="tags">
                <label>Tags:</label>
                <asp:BulletedList ID="BulletedList1" runat="server">
                <asp:ListItem Text="Tag1"></asp:ListItem>
                <asp:ListItem Text="Tag2"></asp:ListItem>
                <asp:ListItem Text="Tag3"></asp:ListItem>
                </asp:BulletedList>
            </div>
        </div>
    </div>
    <div id="div2">
    
    </div>
    <div id="div3">
    
    </div>
</div>
</asp:Content>
