<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="adminpanel.aspx.cs" Inherits="KlubNaCitateli.Sites.adminpanel" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="mainContent" runat="server">
    
    <asp:TextBox ID="tbSearchBooks" runat="server"></asp:TextBox>
    <asp:Button ID="btnSearch" runat="server" Text="Search" 
        onclick="btnSearch_Click" />
    
    <asp:Label ID="lblError" runat="server" Text="lblError" Visible="False"></asp:Label>
    <asp:GridView ID="gvBooks" runat="server" AutoGenerateColumns="False" 
        CellPadding="4" DataKeyNames="ISBN" ForeColor="#333333" GridLines="None">
        <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
        <Columns>
            <asp:BoundField DataField="ISBN" HeaderText="ISBN" />
            <asp:BoundField DataField="Name" HeaderText="Name" />
            <asp:BoundField DataField="Language" HeaderText="Language" />
            <asp:BoundField DataField="YearPublished" HeaderText="Year Published" />
            <asp:ImageField DataImageUrlField="ThumbnailSrc" HeaderText="Thumbnail">
            </asp:ImageField>
            <asp:CommandField HeaderText="Add to database" SelectText="add" 
                ShowSelectButton="True" />
        </Columns>
        <EditRowStyle BackColor="#999999" />
        <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
        <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
        <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
        <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
        <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
        <SortedAscendingCellStyle BackColor="#E9E7E2" />
        <SortedAscendingHeaderStyle BackColor="#506C8C" />
        <SortedDescendingCellStyle BackColor="#FFFDF8" />
        <SortedDescendingHeaderStyle BackColor="#6F8DAE" />
    </asp:GridView>
</asp:Content>
