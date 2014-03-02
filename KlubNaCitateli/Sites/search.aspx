<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="search.aspx.cs" Inherits="KlubNaCitateli.Sites.search" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="../Styles/Search.css" type="text/css" rel="Stylesheet" />
    <script src="../Scripts/jquery-1.10.2.js" type="text/javascript" language="javascript"></script>
    <script src="../Scripts/jquery-ui-1.10.4.custom.min.js" type="text/javascript" language="javascript"></script>
    <script src="../Scripts/Search.js" type="text/javascript" language="javascript"></script>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="mainContent" runat="server">
        <div id="search">
            <asp:TextBox ID="TextBox1" runat="server" placeholder="Search.." CssClass="searchBox" ></asp:TextBox>
        </div>

        <div id="searchContent">
            <ul id="sortList">
                <li><asp:Label ID="Label3" runat="server" Text="Label" CssClass="label">Name</asp:Label></li>
                <li><asp:Label ID="Label4" runat="server" Text="Label" CssClass="label">Author</asp:Label></li>
                <li><asp:Label ID="Label5" runat="server" Text="Label" CssClass="label">Year of publish</asp:Label></li>
            </ul>
            <div id="searchList">
                <div class="searchItem">
                    <div class="span1">
                    </div>
                    <div class="span2">
                        <span>blablabla KUjeKUjeKUje KUjeKUjeKUjeKUje</span>
                    </div>
                    <div class="span3">
                    <span>KUjeKUjeKUjeKUjeKUje KUjeKUje,KUjeKUjeKUjeKUjeKUje KUjeKUje </span>
                    </div>
                    <div class="span4">
                    <span>12/12/1992</span>
                    </div>
                </div>
            </div>


        </div>

        <div id="filterContainer">
            <ul id="filterList">
                <li><asp:Label ID="Label1" runat="server" Text="Label">Category</asp:Label></li>
                <li><asp:CheckBoxList ID="CategoryList" runat="server"></asp:CheckBoxList></li>
                <li><asp:Label ID="Label2" runat="server" Text="Label">Language</asp:Label></li>
                <li><asp:CheckBoxList ID="LanguageList" runat="server"></asp:CheckBoxList></li>
            </ul>
        </div>
        <asp:ScriptManager ID="ScriptManager1" runat="server">
            <Services>
                <asp:ServiceReference Path="~/Services/SearchService.svc" />
            </Services>
        </asp:ScriptManager>

</asp:Content>



