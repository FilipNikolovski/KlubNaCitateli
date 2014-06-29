<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="profile.aspx.cs" 

Inherits="KlubNaCitateli.Sites.profile" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script src="/Scripts/jquery-1.4.4.min.js" type="text/javascript"></script>
    <script src="/Scripts/jsCarousel-2.0.0.js" type="text/javascript"></script>
    <link href="/Styles/jsCarousel-2.0.0.css" rel="stylesheet" type="text/css" />
 
    <script type="text/javascript">
        $(document).ready(function () {

            $("#myProfileLink").addClass("active");

            $('#carouselh').jsCarousel({ onthumbnailclick: function (src) { alert(src); }, autoscroll: false, masked: false, itemstodisplay: 5, orientation: 'h' });
            
        });       
         
    </script>
    <style type="text/css">
        

#mainContent_profileImg {
	width: 200px;
	max-height: 200px;
	background-position: center;
	background-size: 90%;
}

    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="mainContent" runat="server">
    <asp:Table ID="Table1" runat="server" Width="100%">
        <asp:TableRow style="margin-bottom: 20px;">
            <asp:TableCell Width="200px" VerticalAlign="Top">
                <asp:Table ID="Table2" runat="server">
                    <asp:TableRow>
                        <asp:TableCell>
                            <img runat="server" id="profileImg" src="/Images/user-icon.png" alt="" />
                        </asp:TableCell>
                    </asp:TableRow>
                    <asp:TableRow>
                        <asp:TableCell>
                            <asp:ScriptManager ID="ScriptManager1" runat="server">
                            </asp:ScriptManager>
                            <asp:FileUpload ID="profilePicture" runat="server" Visible="false" />
                            <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Always">
                                <Triggers>
                                    <asp:PostBackTrigger ControlID="changePicBtn" />
                                </Triggers>
                                <ContentTemplate>
                                    <asp:Button ID="changePicBtn" Text="Change Picture" runat="server" Visible="false" OnClick="ChangePicture" />
                                </ContentTemplate>
                            </asp:UpdatePanel>
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
                                        <asp:ValidationSummary runat="server" ValidationGroup="Group1" />
                                        <asp:Label ID="Label1" runat="server" Text="Username:"></asp:Label>
                                    </asp:TableCell>
                                    <asp:TableCell Width="300px">
                                        <asp:Label ID="lblUsername" runat="server" Text="MarkoMarkovski1"></asp:Label>
                                        <asp:TextBox ID="tbUsername" runat="server" Visible="false" ValidationGroup="Group1"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="You must enter a value!" ControlToValidate="tbUsername" ForeColor="Red" ValidationGroup="Group1">
                                        </asp:RequiredFieldValidator>
                                    </asp:TableCell>
                                    <asp:TableCell>
                                        <asp:Button runat="server" ID="changeUsrBtn" Text="Edit username" Visible="false" OnClick="ChangeUsername" CausesValidation="false" />
                                        <asp:Button runat="server" ID="confirmChangeUsrBtn" Text="Confirm edit" Visible="false" OnClick="ConfirmChangeUsername" />
                                    </asp:TableCell>
                                </asp:TableRow>
                                <asp:TableRow>
                                    <asp:TableCell>
                                        <asp:ValidationSummary runat="server" ValidationGroup="Group2" />
                                        <asp:Label ID="Label3" runat="server" Text="Email:"></asp:Label>
                                    </asp:TableCell>
                                    <asp:TableCell>
                                        <asp:Label ID="lblEmail" runat="server" Text="markoMarkovski@email.com"></asp:Label>
                                        <asp:TextBox ID="tbEmail" runat="server" Visible="false" ValidationGroup="Group2"></asp:TextBox>
                                         <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="You must enter a value!" ControlToValidate="tbEmail" ForeColor="Red" ValidationGroup="Group2">
                                         </asp:RequiredFieldValidator>
                                         <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="tbEmail"
                                         ErrorMessage="Incorrect format!" ValidationExpression="^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$" ForeColor="Red" ValidationGroup="Group2"></asp:RegularExpressionValidator>
                          
                                    </asp:TableCell>
                                    <asp:TableCell>
                                        <asp:Button ID="changeEmailBtn" runat="server" Text="Edit email" Visible="false" OnClick="ChangeEmail" CausesValidation="false"/>
                                        <asp:Button ID="confirmChangeEmailBtn" runat="server" Text="Confirm edit" Visible="false" OnClick="ConfirmChangeEmail" />
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
    <asp:Table ID="Table5" runat="server" Width="98%" style="margin: 10px 10px 10px 10px;">
        <asp:TableRow>
            <asp:TableCell>
                <asp:Label ID="Label6" runat="server" Text="My books:"></asp:Label>
            </asp:TableCell>
        </asp:TableRow>
        <asp:TableRow>
            <asp:TableCell>
                <div id="carousel" style="width: 100%; height: 350px;">
                    <div id="hWrapper" runat="server">
                    
                    </div>
                </div>
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