<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="index.aspx.cs" Inherits="KlubNaCitateli.Sites.index" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="../Styles/index.css" rel="stylesheet" type="text/css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="mainContent" runat="server">
    <asp:Panel ID="Panel1" runat="server" CssClass="searchPanel">
        <asp:Label ID="Label1" runat="server" Text="Book club is more than reviews" CssClass="bookPanel"></asp:Label>
        <br />
        <asp:TextBox ID="searchWord" runat="server" CssClass="searchBox"></asp:TextBox>
    </asp:Panel>
    <asp:Panel ID="Panel2" runat="server" CssClass="panel2">
        <asp:Table runat="server" CssClass="tableRow">
            <asp:TableRow>
                <asp:TableCell CssClass="tablename">
                    <asp:Label ID="Label2" runat="server" Text="Most Wanted"></asp:Label>
                </asp:TableCell>
                <asp:TableCell> </asp:TableCell>
                <asp:TableCell CssClass="tablename">
                    <asp:Label ID="Label5" runat="server" Text="Most Viewed"></asp:Label></asp:TableCell>
                <asp:TableCell></asp:TableCell>
                <asp:TableCell CssClass="tablename">
                    <asp:Label ID="Label3" runat="server" Text="Best this month"></asp:Label></asp:TableCell>
                <asp:TableCell></asp:TableCell>
            </asp:TableRow>
            <asp:TableRow>
                <asp:TableCell CssClass="table">
                   
                    <asp:Label ID="mostWantedBookName" runat="server" Text="" CssClass="bookNameLabel"
                        Multiline="True"></asp:Label>
                </asp:TableCell>
                <asp:TableCell>
                    <asp:Panel ColumnSpan="2" ID="mostWantedPanel" runat="server" CssClass="bookPlacePanel">
                    </asp:Panel>
                </asp:TableCell>
                <asp:TableCell CssClass="table">
                    
                    <asp:Label ID="mostViewedBookName" runat="server" Text="" CssClass="bookNameLabel"
                        Multiline="True"></asp:Label>
                </asp:TableCell>
                <asp:TableCell>
                    <asp:Panel ColumnSpan="2" ID="mostViewedPanel" runat="server" CssClass="bookPlacePanel">
                    </asp:Panel>
                </asp:TableCell>
                <asp:TableCell CssClass="table">
                    
                    <asp:Label ID="bestThisMonthBookName" runat="server" Text="" CssClass="bookNameLabel"
                        Multiline="True"></asp:Label></asp:TableCell>
                <asp:TableCell>
                    <asp:Panel ColumnSpan="2" ID="bestThisMonthPanel" runat="server" CssClass="bookPlacePanel">
                    </asp:Panel>
                </asp:TableCell>
            </asp:TableRow>
        </asp:Table>
        <asp:Table runat="server" CssClass="tableRow">
            <asp:TableRow>
                <asp:TableCell CssClass="tablename">
                    <asp:Label ID="firstCategoryName" runat="server" Text=""></asp:Label></asp:TableCell>
                <asp:TableCell></asp:TableCell>
                <asp:TableCell CssClass="tablename">
                    <asp:Label ID="secondCategoryName" runat="server" Text=""></asp:Label></asp:TableCell>
                <asp:TableCell></asp:TableCell>
                <asp:TableCell CssClass="tablename">
                    <asp:Label ID="thirdCategoryName" runat="server" Text=""></asp:Label></asp:TableCell>
                <asp:TableCell></asp:TableCell>
            </asp:TableRow>
            <asp:TableRow>
                <asp:TableCell CssClass="table">
                    
                    <asp:Label ID="firstCategoryBookName" runat="server" Text="" CssClass="bookNameLabel"></asp:Label></asp:TableCell>
                <asp:TableCell>
                    <asp:Panel ID="firstCategoryPanel" CollumnSpan="2" runat="server" CssClass="bookPlacePanel">
                    </asp:Panel>
                </asp:TableCell>
                <asp:TableCell CssClass="table">
                    
                    <asp:Label ID="secondCategoryBookName" runat="server" Text="" CssClass="bookNameLabel"></asp:Label></asp:TableCell>
                <asp:TableCell>
                    <asp:Panel ID="secondCategoryPanel" CollumnSpan="2" runat="server" CssClass="bookPlacePanel">
                    </asp:Panel>
                </asp:TableCell>
                <asp:TableCell CssClass="table">
                    
                    <asp:Label ID="thirdCategoryBookName" runat="server" Text="" CssClass="bookNameLabel"></asp:Label></asp:TableCell>
                <asp:TableCell>
                    <asp:Panel ID="thirdCategoryPanel" CollumnSpan="2" runat="server" CssClass="bookPlacePanel">
                    </asp:Panel>
                </asp:TableCell>
            </asp:TableRow>
        </asp:Table>
    </asp:Panel>
</asp:Content>
