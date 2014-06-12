<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="index.aspx.cs" Inherits="KlubNaCitateli.Sites.index" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="../Styles/index.css" rel="stylesheet" type="text/css" />
    <script>
        $(document).ready(function () {

           

            $("#<%= searchWord.ClientID %>").keyup(function (e) {
                if (e.keyCode == 13) {
                    window.location.replace("~/Sites/search.aspx?search=" + $("#<%=searchWord.ClientID %>").val());
                }
            });




        });
    </script>
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
                    <asp:LinkButton ID="mostWantedBookName" runat="server" Text="" CssClass="bookNameLabel"
                        Multiline="True"></asp:LinkButton>
                </asp:TableCell>
                <asp:TableCell>
                    <asp:ImageButton ColumnSpan="2" ID="mostWantedPanel" runat="server" CssClass="bookPlacePanel">
                    </asp:ImageButton>
                </asp:TableCell>
                <asp:TableCell CssClass="table">
                    <asp:LinkButton ID="mostViewedBookName" runat="server" Text="" CssClass="bookNameLabel"
                        Multiline="True"></asp:LinkButton>
                </asp:TableCell>
                <asp:TableCell>
                    <asp:ImageButton ColumnSpan="2" ID="mostViewedPanel" runat="server" CssClass="bookPlacePanel">
                    </asp:ImageButton>
                </asp:TableCell>
                <asp:TableCell CssClass="table">
                    <asp:LinkButton ID="bestThisMonthBookName" runat="server" Text="" CssClass="bookNameLabel"
                        Multiline="True"></asp:LinkButton></asp:TableCell>
                <asp:TableCell>
                    <asp:ImageButton ColumnSpan="2" ID="bestThisMonthPanel" runat="server" CssClass="bookPlacePanel">
                    </asp:ImageButton>
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
                    <asp:LinkButton ID="firstCategoryBookName" runat="server" Text="" CssClass="bookNameLabel"></asp:LinkButton></asp:TableCell>
                <asp:TableCell>
                    <asp:ImageButton ID="firstCategoryPanel" CollumnSpan="2" runat="server" CssClass="bookPlacePanel">
                    </asp:ImageButton>
                </asp:TableCell>
                <asp:TableCell CssClass="table">
                    <asp:LinkButton ID="secondCategoryBookName" runat="server" Text="" CssClass="bookNameLabel"></asp:LinkButton></asp:TableCell>
                <asp:TableCell>
                    <asp:ImageButton ID="secondCategoryPanel" CollumnSpan="2" runat="server" CssClass="bookPlacePanel">
                    </asp:ImageButton>
                </asp:TableCell>
                <asp:TableCell CssClass="table">
                    <asp:LinkButton ID="thirdCategoryBookName" runat="server" Text="" CssClass="bookNameLabel"></asp:LinkButton></asp:TableCell>
                <asp:TableCell>
                    <asp:ImageButton ID="thirdCategoryPanel" CollumnSpan="2" runat="server" CssClass="bookPlacePanel">
                    </asp:ImageButton>
                </asp:TableCell>
            </asp:TableRow>
        </asp:Table>
        <asp:Table ID="Table1" runat="server" CssClass="tableRow">
            <asp:TableRow>
                <asp:TableCell CssClass="tablename">
                    <asp:Label ID="fourthCategoryName" runat="server" Text=""></asp:Label></asp:TableCell>
                <asp:TableCell></asp:TableCell>
                <asp:TableCell CssClass="tablename">
                    <asp:Label ID="fifthCategoryName" runat="server" Text=""></asp:Label></asp:TableCell>
                <asp:TableCell></asp:TableCell>
                <asp:TableCell CssClass="tablename">
                    <asp:Label ID="sixthCategoryName" runat="server" Text=""></asp:Label></asp:TableCell>
                <asp:TableCell></asp:TableCell>
            </asp:TableRow>
            <asp:TableRow>
                <asp:TableCell CssClass="table">
                    <asp:LinkButton ID="fourthCategoryBookName" runat="server" Text="" CssClass="bookNameLabel"></asp:LinkButton></asp:TableCell>
                <asp:TableCell>
                    <asp:ImageButton ID="fourthCategoryPanel" CollumnSpan="2" runat="server" CssClass="bookPlacePanel">
                    </asp:ImageButton>
                </asp:TableCell>
                <asp:TableCell CssClass="table">
                    <asp:LinkButton ID="fifthCategoryBookName" runat="server" Text="" CssClass="bookNameLabel"></asp:LinkButton></asp:TableCell>
                <asp:TableCell>
                    <asp:ImageButton ID="fifthCategoryPanel" CollumnSpan="2" runat="server" CssClass="bookPlacePanel">
                    </asp:ImageButton>
                </asp:TableCell>
                <asp:TableCell CssClass="table">
                    <asp:LinkButton ID="sixthCategoryBookName" runat="server" Text="" CssClass="bookNameLabel"></asp:LinkButton></asp:TableCell>
                <asp:TableCell>
                    <asp:ImageButton ID="sixthCategoryPanel" CollumnSpan="2" runat="server" CssClass="bookPlacePanel">
                    </asp:ImageButton>
                </asp:TableCell>
            </asp:TableRow>
        </asp:Table>
    </asp:Panel>
</asp:Content>
