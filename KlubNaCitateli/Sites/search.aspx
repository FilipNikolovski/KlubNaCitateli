<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="search.aspx.cs" Inherits="KlubNaCitateli.Sites.search" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="../Styles/search.css" type="text/css" rel="Stylesheet" />
    <script src="../Scripts/jquery-1.10.2.js" type="text/javascript" language="javascript"></script>
    <script src="../Scripts/jquery-ui-1.10.4.custom.min.js" type="text/javascript" language="javascript"></script>
    
    <script type="text/javascript">
        $(document).ready(function () {

            $("#searchLink").addClass("active");

            $(".tableSearch tr").each(function () {

                $(this).hover(function () {
                    $(this).addClass("hovertd");
                }, function () {
                    $(this).removeClass("hovertd");
                });
            });

            $(".tableSearch tr").on("click", function () {
                window.location = "book.aspx?id=" + $(this).find(".bookId").html();
                return false;
            });
        });
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="mainContent" runat="server">
    <asp:Panel runat="server" DefaultButton="btnSearch">
        <div id="search">
            <asp:TextBox ID="tbSearch" runat="server" placeholder="Search.." CssClass="searchBox"></asp:TextBox>
            <asp:Button ID="btnSearch" runat="server" Text="" OnClick="btnSearch_Click" CssClass="submitButton" />
            <asp:Label ID="lblError" runat="server" Text="Error" CssClass="label" Visible="false"></asp:Label>
        </div>
    </asp:Panel>
    <div id="searchContent">
        <table id="sortList">
            <tr>
                <th>
                    <asp:Label ID="Label3" runat="server" Text="Thumbnail" CssClass="label"></asp:Label>
                </th>
                <th>
                    <asp:Label ID="Label4" runat="server" Text="Name" CssClass="label"></asp:Label>
                </th>
                <th>
                    <asp:Label ID="Label5" runat="server" Text="Year of publish" CssClass="label"></asp:Label>
                </th>
                <th>
                    <asp:Label ID="Label6" runat="server" Text="Rating" CssClass="label"></asp:Label>
                </th>
                <th>
                    <asp:Label ID="Label2" runat="server" Text="Authors" CssClass="label"></asp:Label>
                </th>
            </tr>
        </table>
        <div id="searchList" class="searchList" runat="server">
        </div>
    </div>
    <div id="filterContainer">
        <ul id="filterList">
            <li>
                <asp:Label ID="Label1" runat="server" Text="Category" CssClass="categoryLabel"></asp:Label></li>
            <li>
                <asp:CheckBoxList ID="cblCategories" runat="server">
                </asp:CheckBoxList>
            </li>
        </ul>
    </div>
</asp:Content>
