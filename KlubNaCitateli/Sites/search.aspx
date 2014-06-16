<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="search.aspx.cs" Inherits="KlubNaCitateli.Sites.search" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="../Styles/Search.css" type="text/css" rel="Stylesheet" />
    <script src="../Scripts/jquery-1.10.2.js" type="text/javascript" language="javascript"></script>
    <script src="../Scripts/jquery-ui-1.10.4.custom.min.js" type="text/javascript" language="javascript"></script>
    <script src="../Scripts/Search.js" type="text/javascript" language="javascript"></script>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="mainContent" runat="server">
        <div id="search">
            <asp:TextBox ID="tbSearch" runat="server" placeholder="Search.." 
                CssClass="searchBox" Width="540px" ></asp:TextBox>
            <asp:Button ID="btnSearch" runat="server" Text="Search" 
                onclick="btnSearch_Click" /> 
                   
            <asp:Label ID="lblError" runat="server" Text="Error" CssClass="label"></asp:Label>

        </div>

        <div id="searchContent">
            <ul id="sortList">
                <li><asp:Label ID="Label3" runat="server" Text="Name" CssClass="label"></asp:Label></li>
                <li><asp:Label ID="Label4" runat="server" Text="Author" CssClass="label"></asp:Label></li>
                <li><asp:Label ID="Label5" runat="server" Text="Year of publish" CssClass="label"></asp:Label></li>
            </ul>
            <div id="searchList" runat="server">
                
            </div>


        </div>

        <div id="filterContainer">
            <ul id="filterList">
                <li><asp:Label ID="Label1" runat="server" Text="Category"></asp:Label></li>
                <li><asp:CheckBoxList ID="CategoryList" runat="server"></asp:CheckBoxList></li>
                <li><asp:Label ID="Label2" runat="server" Text="Language"></asp:Label></li>
                <li><asp:CheckBoxList ID="LanguageList" runat="server"></asp:CheckBoxList></li>
            </ul>
        </div>

</asp:Content>



