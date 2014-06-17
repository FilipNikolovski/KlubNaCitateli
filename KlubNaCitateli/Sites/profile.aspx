<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="profile.aspx.cs" 

Inherits="KlubNaCitateli.Sites.profile" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
<style>
.profilePicture
{
    background-position:center center;
    width:200px;
    max-height:400px;
    height:auto;
}
</style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="mainContent" runat="server">
    <asp:Table ID="Table1" runat="server" Width="100%">
        <asp:TableRow style="margin-bottom: 20px;">
            <asp:TableCell Width="20%" VerticalAlign="Top">
                <asp:Table ID="Table2" runat="server">
                    <asp:TableRow>
                        <asp:TableCell>
                            <asp:Image ID="Image1" runat="server" ImageUrl="/Images/user-icon.png" CssClass="profilePicture" />
                        </asp:TableCell>
                    </asp:TableRow>
                    <asp:TableRow>
                        <asp:TableCell>
                           <asp:Button id="changePicBtn" Text="Change Picture" runat="server" Visible="false" />
                        </asp:TableCell>
                    </asp:TableRow>
                </asp:Table>
            </asp:TableCell>
            <asp:TableCell>
                <asp:Table ID="Table3" runat="server">
                    <asp:TableRow>
                        <asp:TableCell>
                            <asp:Label runat="server" ID="nameLbl" Text="Marko Markovski" Font-Size="20"></asp:Label>
                        </asp:TableCell>
                    </asp:TableRow>
                    <asp:TableRow>
                        <asp:TableCell>
                            <asp:Table ID="Table4" runat="server">
                                <asp:TableRow>
                                    <asp:TableCell Width="150px">
                                        <asp:Label ID="Label1" runat="server" Text="Username:"></asp:Label>
                                    </asp:TableCell>
                                    <asp:TableCell Width="300px">
                                        <asp:Label ID="lblUsername" runat="server" Text="MarkoMarkovski1"></asp:Label>
                                    </asp:TableCell>
                                    <asp:TableCell>
                                        <asp:Button runat="server" ID="changeUsrBtn" Text="Edit username" Visible="false" />
                                    </asp:TableCell>
                                </asp:TableRow>
                                <asp:TableRow>
                                    <asp:TableCell>
                                        <asp:Label ID="Label3" runat="server" Text="Email:"></asp:Label>
                                    </asp:TableCell>
                                    <asp:TableCell>
                                        <asp:Label ID="lblEmail" runat="server" Text="markoMarkovski@email.com"></asp:Label>
                                    </asp:TableCell>
                                    <asp:TableCell>
                                        <asp:Button ID="changeEmailBtn" runat="server" Text="Edit email" Visible="false" />
                                    </asp:TableCell>
                                </asp:TableRow>
                            </asp:Table>                            
                        </asp:TableCell>
                    </asp:TableRow>
                    <asp:TableRow>
                        <asp:TableCell>
                            <asp:Label ID="Label5" runat="server" Text="About"></asp:Label>
                        </asp:TableCell>
                    </asp:TableRow>
                    <asp:TableRow>
                        <asp:TableCell>
                            <asp:Label ID="lblAbout" runat="server" Text="About" Width="100%" Height="200px" BorderStyle="Solid" BorderColor="Gray" BorderWidth="1px"></asp:Label>
                            <asp:TextBox ID="tbAbout" runat="server" Text="About marko" Width="100%" Height="200px" TextMode="MultiLine" Visible="false"></asp:TextBox>
                            <asp:Button ID="changeAboutBtn" runat="server" Text="Edit about" OnClick="ChangeAbout" Visible="false" />
                            <asp:Button ID="confirmChangeAboutBtn" runat="server" Text="Confirm edit" Visible="false" />
                        </asp:TableCell>
                    </asp:TableRow>
                </asp:Table>
            </asp:TableCell>
        </asp:TableRow>
    </asp:Table>
    <asp:Table ID="Table5" runat="server" Width="100%" style="margin: 10px 10px 10px 10px;">
        <asp:TableRow>
            <asp:TableCell>
                <asp:Label ID="Label6" runat="server" Text="My books:"></asp:Label>
            </asp:TableCell>
        </asp:TableRow>
        <asp:TableRow>
            <asp:TableCell>
                <div id="carousel" runat="server" style="width: 100%; height: 200px; border: 1px solid gray;"></div>
            </asp:TableCell>
        </asp:TableRow>
        <asp:TableRow>
            <asp:TableCell>
                <asp:Label ID="Label7" runat="server" Text="Preferirani kategorii:"></asp:Label>
            </asp:TableCell>
        </asp:TableRow>
        <asp:TableRow>
            <asp:TableCell>
                <asp:Label ID="lblCategories" runat="server" Text="asdasd" Height="200px" Width="100%" BorderStyle="Solid" BorderColor="Gray" BorderWidth="1px"></asp:Label>
            </asp:TableCell>
        </asp:TableRow>
        <asp:TableRow>
            <asp:TableCell>
                <asp:Label ID="Label8" runat="server" Text="Otvoreni Temi:"></asp:Label>
            </asp:TableCell>
        </asp:TableRow>
        <asp:TableRow>
            <asp:TableCell> 
                <asp:TextBox ID="TextBox3" runat="server" Text="asdasd" Height="200px" Width="100%" TextMode="MultiLine" 

ReadOnly="true"></asp:TextBox>
            </asp:TableCell>
        </asp:TableRow>
    </asp:Table>

    <asp:Label runat="server" ID="lblError" Text="" Visible="false"></asp:Label>

</asp:Content>