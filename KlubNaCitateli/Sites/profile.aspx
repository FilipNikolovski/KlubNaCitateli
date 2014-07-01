<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="profile.aspx.cs" Inherits="KlubNaCitateli.Sites.profile" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script src="../Scripts/jquery-1.10.2.js" type="text/javascript"></script>
    <script src="../Scripts/jquery.jcarousel.min.js" type="text/javascript"></script>
    <link href="../Styles/jcarousel.css" rel="stylesheet" type="text/css" />
    <link rel="stylesheet" href="//code.jquery.com/ui/1.11.0/themes/smoothness/jquery-ui.css">
    <script src="//code.jquery.com/ui/1.11.0/jquery-ui.js"></script>
    <script type="text/javascript">
        $(document).ready(function () {

            $("#myProfileLink").addClass("active");

            $("#mainContent_saveCategories").on("click", function() {
                
                var list = $("#mainContent_myCategories li");
                var myCategories = new Array();

                list.each(function() {
                    myCategories.push($(this).text());
                });
                
                var json = {
                    userId: '<%=UserId %>',
                    categories: myCategories
                }

                var service = new KlubNaCitateli.BookService();
                service.SaveCategories(JSON.stringify(json), function(result) {
                    alert(result);
                });
            });

            $("#mainContent_myCategories, #mainContent_allCategories").sortable({
                connectWith: '.connectedSortable'
            }).disableSelection();

            var jcarousel = $('.jcarousel');

            jcarousel.jcarousel({
                wrap: 'circular',
            });


            $('.jcarousel-control-prev')
            .jcarouselControl({
                target: '-=1'
            });

            $('.jcarousel-control-next')
            .jcarouselControl({
                target: '+=1'
            });

            $('.jcarousel-pagination')
            .on('jcarouselpagination:active', 'a', function () {
                $(this).addClass('active');
            })
            .on('jcarouselpagination:inactive', 'a', function () {
                $(this).removeClass('active');
            })
            .on('click', function (e) {
                e.preventDefault();
            })
            .jcarouselPagination({
                perPage: 1,
                item: function (page) {
                    return '<a href="#' + page + '">' + page + '</a>';
                }
            });
            
        });       
         
    </script>
    <style type="text/css">
        #mainContent_profileImg
        {
            width: 200px;
            max-height: 200px;
            background-position: center;
            background-size: 90%;
        }
        #mainContent_lblAbout
        {
            -webkit-box-shadow: 8px 8px 6px -6px black;
            -moz-box-shadow: 8px 8px 6px -6px black;
            box-shadow: 0 0 10px #000000;
        }
        #mainContent_myCategories, #mainContent_allCategories
        {
            border: 1px solid #eee;
            width: 142px;
            min-height: 20px;
            list-style-type: none;
            margin: 0;
            padding: 5px 0 0 0;
            float: left;
            margin-right: 10px;
        }
        #mainContent_myCategories li, #mainContent_allCategories li
        {
            margin: 0 5px 5px 5px;
            padding: 5px;
            font-size: 1.2em;
            width: 120px;
        }
        #mainContent_btnAddCategories
        {
            float: left;
        }
        #mainContent_profileCategories
        {
            clear: both;
            padding: 20px;
        }
        .nodiv
        {
            clear: both;
        }
        .saveCategories
        {
            float: left;
            clear: both;
            margin-right: 10px;
        }
        .fieldsetContainer 
        {
            clear:both;
            margin-top:50px;
        }
        #profileCategories
        {
            width: 41%;
            float:left;
            margin:0px;
            margin-right:20px;
            margin-left:20px;
            padding-bottom: 15px;
        }
        #profileThreads
        {
            float: left;
            width: 50%;
            margin-left: 30px;
            margin:0px !important;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="mainContent" runat="server">
    <asp:ScriptManager runat="server" ID="scriptManager">
        <Services>
            <asp:ServiceReference Path="../Services/BookService.svc" />
        </Services>
    </asp:ScriptManager>
    <asp:Table ID="Table1" runat="server" Width="100%">
        <asp:TableRow Style="margin-bottom: 20px;">
            <asp:TableCell Width="200px" VerticalAlign="Top">
                <asp:Table ID="Table2" runat="server">
                    <asp:TableRow>
                        <asp:TableCell>
                            <img runat="server" id="profileImg" src="/Images/user-icon.png" alt="" />
                        </asp:TableCell>
                    </asp:TableRow>
                    <asp:TableRow>
                        <asp:TableCell>
                            <asp:FileUpload ID="profilePicture" runat="server" Visible="false" />
                            <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Always">
                                <Triggers>
                                    <asp:PostBackTrigger ControlID="changePicBtn" />
                                </Triggers>
                                <ContentTemplate>
                                    <asp:Button ID="changePicBtn" Text="Change Picture" runat="server" Visible="false"
                                        OnClick="ChangePicture" />
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
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="You must enter a value!"
                                            ControlToValidate="tbUsername" ForeColor="Red" ValidationGroup="Group1">
                                        </asp:RequiredFieldValidator>
                                    </asp:TableCell>
                                    <asp:TableCell>
                                        <asp:Button runat="server" ID="changeUsrBtn" Text="Edit username" Visible="false"
                                            OnClick="ChangeUsername" CausesValidation="false" />
                                        <asp:Button runat="server" ID="confirmChangeUsrBtn" Text="Confirm edit" Visible="false"
                                            OnClick="ConfirmChangeUsername" />
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
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="You must enter a value!"
                                            ControlToValidate="tbEmail" ForeColor="Red" ValidationGroup="Group2">
                                        </asp:RequiredFieldValidator>
                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="tbEmail"
                                            ErrorMessage="Incorrect format!" ValidationExpression="^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$"
                                            ForeColor="Red" ValidationGroup="Group2"></asp:RegularExpressionValidator>
                                    </asp:TableCell>
                                    <asp:TableCell>
                                        <asp:Button ID="changeEmailBtn" runat="server" Text="Edit email" Visible="false"
                                            OnClick="ChangeEmail" CausesValidation="false" />
                                        <asp:Button ID="confirmChangeEmailBtn" runat="server" Text="Confirm edit" Visible="false"
                                            OnClick="ConfirmChangeEmail" />
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
                            <asp:Label ID="lblAbout" runat="server" Text="About" Width="100%" Height="200px"></asp:Label>
                            <asp:TextBox ID="tbAbout" runat="server" Text="About marko" Width="100%" Height="200px"
                                TextMode="MultiLine" Visible="false"></asp:TextBox>
                            <asp:Button ID="changeAboutBtn" runat="server" Text="Edit about" OnClick="ChangeAbout"
                                Visible="false" />
                            <asp:Button ID="confirmChangeAboutBtn" runat="server" Text="Confirm edit" Visible="false" />
                        </asp:TableCell>
                    </asp:TableRow>
                </asp:Table>
            </asp:TableCell>
        </asp:TableRow>
    </asp:Table>
    <asp:Table ID="Table5" runat="server" Width="98%" Style="margin: 10px 10px 10px 10px;">
        <asp:TableRow>
            <asp:TableCell>
                <asp:Label ID="Label6" runat="server" Text="My books:"></asp:Label>
            </asp:TableCell>
        </asp:TableRow>
    </asp:Table>
    <!-- JCAROUSEL -->
    <div id="jcarouselWrapper" runat="server">
    </div>
    <div class="fieldsetContainer">
        <fieldset id="profileCategories">
            <legend>Categories</legend>
            <div class="saveCategories">
                <input id="saveCategories" type="button" value="Save Categories" runat="server" visible="false" />
                <div class='nodiv'>
                </div>
            </div>
            <ul id="myCategories" class="connectedSortable" runat="server">
            </ul>
            <ul id="allCategories" class="connectedSortable" runat="server" visible="false">
            </ul>
            <div class='nodiv'>
            </div>
        </fieldset>
        <fieldset id="profileThreads">
            <legend>Profile threads:</legend>
        </fieldset>
        <div class='nodiv'></div>
    </div>
    <asp:Label runat="server" ID="lblError" Text="" Visible="true"></asp:Label>
</asp:Content>
