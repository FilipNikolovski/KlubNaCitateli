<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="book.aspx.cs" Inherits="KlubNaCitateli.Sites.book" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="../Styles/book.css" type="text/css" rel="Stylesheet" />
    <style type="text/css">
        #name
        {
            height: 57px;
        }
        #list
        {
            margin-bottom: 115px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="mainContent" runat="server">
<div id="content">
    <div id="div1">
        <div id="info">
            <div id="image">
                <asp:Image ID="imgBook" runat="server" Height="159px" Width="129px" />
                <ul>
                    <li></li>
                    <li></li>
                    <li></li>
                    <li></li>
                    <li></li>
                </ul>
                <asp:Button ID="btnAddFavourites" runat="server" Text="Button" />
            </div>
            <div id="about">
                <div id="name">
                    <asp:Label ID="lblNameBook" runat="server" Text="Ime Kniga"></asp:Label>
                    <br /> by <br />
                    <asp:Label ID="lblNameAuthor" runat="server" Text="Ime Aftor"></asp:Label>
                </div>
                <div id="description">
                    <label>Description</label>
                    <br />
                    <asp:Panel ID="pnlDescription" runat="server" Height="115px">
                        j;asnasjdasdnaiodason</asp:Panel>
                </div>
            </div>
            <div id="links">
                <label>Download/By Links</label>
                <div id="list">
                    <asp:ListBox ID="lbBookLinks" runat="server"></asp:ListBox>
                </div>
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
