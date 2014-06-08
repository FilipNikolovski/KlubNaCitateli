<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="adminpanel.aspx.cs" Inherits="KlubNaCitateli.Sites.adminpanel" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    
    <script src="<%= Page.ResolveClientUrl("../Scripts/AdminPanel.js") %>" type="text/javascript">
    </script>
    <style>
        fieldset { padding:0; border:1; margin-top:25px; }
        input.text { margin-bottom:12px; width:95%; padding: .4em; }
    </style>
    
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="mainContent" runat="server">
    
    <asp:Label ID="lblError" runat="server" Text="lblError" Visible="False"></asp:Label>

    <div id="dialog-form" title="Create new user">
      <p class="validateTips">All form fields are required.</p>
 
      <form action="adminpanel.aspx">
          <fieldset>
            <label for="name">Name</label>
            <input type="text" name="name" id="name" class="text ui-widget-content ui-corner-all" />
            <label for="email">Email</label>
            <input type="text" name="email" id="email" value="" class="text ui-widget-content ui-corner-all" />
            <label for="password">Password</label>
            <input type="password" name="password" id="password" value="" class="text ui-widget-content ui-corner-all" />
          </fieldset>
      </form>
    </div>

    <fieldset>
        <legend>Manage Categories</legend>
        <asp:TextBox ID="tbCategory" runat="server"></asp:TextBox>
        <asp:Button ID="btnAddCategory" runat="server" Text="Add category" 
            onclick="btnAddCategory_Click" />

        <asp:RegularExpressionValidator ID="revCategory" runat="server" 
            ErrorMessage="The category name must only consist of characters." 
            ControlToValidate="tbCategory" ForeColor="Red" 
            ValidationExpression="^[A-z]+$" ValidationGroup="vGroup1" Display="None"></asp:RegularExpressionValidator>
        <asp:RequiredFieldValidator ID="rfvCategory" runat="server" 
            ErrorMessage="The input must not be empty." ControlToValidate="tbCategory" 
            ForeColor="Red" ValidationGroup="vGroup1" Display="None"></asp:RequiredFieldValidator>
        
        <asp:ValidationSummary ID="vSummary1" runat="server" ValidationGroup="vGroup1" 
            DisplayMode="List" ForeColor="Red" />
        
        <asp:GridView ID="gvCategories" runat="server" CellPadding="4" 
            ForeColor="#333333" GridLines="None" DataKeyNames="IDCategory" 
            onrowcancelingedit="gvCategories_RowCancelingEdit" 
            onrowdeleting="gvCategories_RowDeleting" onrowediting="gvCategories_RowEditing" 
            onrowupdating="gvCategories_RowUpdating">
            <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
            <Columns>
                <asp:CommandField HeaderText="Manage" ShowEditButton="True" />
                <asp:CommandField HeaderText="Delete" ShowDeleteButton="True" />
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
        </fieldset>

        <fieldset>
        <legend>Manage Books</legend>
        <asp:TextBox ID="tbSearchBooks" runat="server"></asp:TextBox>
        <asp:Button ID="btnSearch" runat="server" Text="Search" 
            onclick="btnSearch_Click" />

        <asp:RequiredFieldValidator ID="rfvSearchBooks" runat="server" 
            ErrorMessage="The input must not be empty." ControlToValidate="tbSearchBooks" 
            ForeColor="Red" Display="None" ValidationGroup="vGroup2"></asp:RequiredFieldValidator>
        <asp:ValidationSummary ID="vSummary2" runat="server" ValidationGroup="vGroup2" 
                DisplayMode="List" ForeColor="Red" />

        <asp:GridView ID="gvBooks" runat="server" AutoGenerateColumns="False" 
            CellPadding="4" DataKeyNames="ISBN" ForeColor="#333333" GridLines="None" 
                onselectedindexchanged="gvBooks_SelectedIndexChanged">
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
    </fieldset>
</asp:Content>
