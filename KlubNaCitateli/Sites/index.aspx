<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="index.aspx.cs" Inherits="KlubNaCitateli.Sites.index" %>
  
  <asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
  <link href="../Styles/index.css" rel="stylesheet" type="text/css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="mainContent" runat="server">
    <asp:Panel ID="Panel1" runat="server" CssClass="searchPanel">

        <asp:Label ID="Label1" runat="server" Text="Book club is more than reviews" CssClass="bookPanel"></asp:Label>
       <br /> <asp:TextBox ID="searchWord" runat="server" CssClass="searchBox"></asp:TextBox>

    </asp:Panel>

    <asp:Panel ID="Panel2" runat="server" CssClass="panel2">
    <div id="gore">
        <asp:Panel ID="mostWantedPanel" runat="server" CssClass="bookPlacePanel">
            <asp:Label ID="Label2" runat="server" Text="Most Wanted"></asp:Label>
            <asp:Label ID="mostWantedBookName" runat="server" Text="" CssClass="bookNameLabel"></asp:Label>
        </asp:Panel>
        <asp:Panel ID="mostViewedPanel" runat="server" CssClass="bookPlacePanel1">
            <asp:Label ID="Label3" runat="server" Text="Most Viewed"></asp:Label>
            <asp:Label ID="mostViewedBookName" runat="server" Text="" CssClass="bookNameLabel"></asp:Label>
        </asp:Panel>

        <asp:Panel ID="bestThisMonthPanel" runat="server" CssClass="bookPlacePanel2">
            <asp:Label ID="Label4" runat="server" Text="Best this month"></asp:Label>
            <asp:Label ID="bestThisMonthBookName" runat="server" Text="" CssClass="bookNameLabel"></asp:Label>
        </asp:Panel>

        </div>
        <div id="dole">
        
        <asp:Panel ID="firstCategoryPanel" runat="server" CssClass="bookPlacePanel">
            <asp:Label ID="firstCategoryName" runat="server" Text=""></asp:Label>
            <asp:Label ID="firstCategoryBookName" runat="server" Text="" CssClass="bookNameLabel"></asp:Label>
        </asp:Panel>


        <asp:Panel ID="secondCategoryPanel" runat="server" CssClass="bookPlacePanel1">
            <asp:Label ID="secondCategoryName" runat="server" Text=""></asp:Label>
            <asp:Label ID="secondCategoryBookName" runat="server" Text="" CssClass="bookNameLabel"></asp:Label>
        </asp:Panel>
        <asp:Panel ID="thirdCategoryPanel" runat="server" CssClass="bookPlacePanel2">
            <asp:Label ID="thirdCategoryName" runat="server" Text=""></asp:Label>
            <asp:Label ID="thirdCategoryBookName" runat="server" Text="" CssClass="bookNameLabel"></asp:Label>
            </asp:Panel>
            </div>
            </asp:Panel>

            
</asp:Content>
