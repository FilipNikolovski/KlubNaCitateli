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

            var iduser = $("#<%= iduser.ClientID %>").val();
            var idsession = $("#<%= idsession.ClientID %>").val();

            if (parseInt(iduser) == parseInt(idsession)) {
                $("#changeUsrBtn").show();
                $("#confirmChangeUsrBtn").hide();
                $("#lblUsername").show();
                $("#tbUsername").hide();
                $("#changeEmailBtn").show();
                $("#confirmChangeEmailBtn").hide();
                $("#lblEmail").show();
                $("#tbEmail").hide();
                $("#cancelEdit").hide();
                $("#cancelEditE").hide();
                $("#changeAboutBtn").show();
                $("#confirmChangeAboutBtn").hide();
                $("#lblAbout").show();
                $("#tbAbout").hide();
                $("#cancelEditA").hide();

            }
            else {
                $("#changeUsrBtn").hide();
                $("#confirmChangeUsrBtn").hide();
                $("#lblUsername").hide();
                $("#tbUsername").hide();
                $("#changeEmailBtn").hide();
                $("#confirmChangeEmailBtn").hide();
                $("#lblEmail").hide();
                $("#tbEmail").hide();
                $("#cancelEdit").hide();
                $("#cancelEditE").hide();
                $("#changeAboutBtn").hide();
                $("#confirmChangeAboutBtn").hide();
                $("#lblAbout").hide();
                $("#tbAbout").hide();
                $("#cancelEditA").hide();

            }

            $(".bookname").click(function () {
                var id = $(this).parent().find(".idbook").html();
                window.location = "book.aspx?id=" + id;
            })
            $("#myProfileLink").addClass("active");

            $("#mainContent_saveCategories").on("click", function () {

                var list = $("#mainContent_myCategories li");
                var myCategories = new Array();

                list.each(function () {
                    myCategories.push($(this).text());
                });

                var json = {
                    userId: '<%=UserId %>',
                    categories: myCategories
                }

                var service = new KlubNaCitateli.BookService();
                service.SaveCategories(JSON.stringify(json), function (result) {
                    alert(result);
                });
            });

            $("#mainContent_myCategories, #mainContent_allCategories").sortable({
                connectWith: '.connectedSortable'
            }).disableSelection();

            var jcarousel = $('.jcarousel');

            jcarousel.jcarousel({
                wrap: 'circular'
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
            $(".threadname").click(function () {
                var id = $(this).parent().find(".idthread").html();
                window.location = "post.aspx?threadid=" + id;

            })

            $("#changeUsrBtn").click(function () {
                var username = $("#<%= lblUsername.ClientID %>").text();
                $("#tbUsername").val(username);
                $("#tbUsername").show();
                $("#<%= lblUsername.ClientID %>").hide();
                $("#confirmChangeUsrBtn").show();
                $("#changeUsrBtn").hide();
                $("#cancelEdit").show();

            });

            $("#cancelEdit").click(function () {
                $("#tbUsername").hide();
                $("#confirmChangeUsrBtn").hide();
                $("#changeUsrBtn").show();
                $("#cancelEdit").hide();
                $("#<%= lblUsername.ClientID %>").show();
            })

            $("#confirmChangeUsrBtn").click(function () {
                var username = $("#tbUsername").val();
                if (username == "") {
                    alert("You must enter username!");
                }
                else {
                    var iduser = $("#<%= iduser.ClientID %>").val();
                    var service = new KlubNaCitateli.ProfileService();
                    service.UpdateUsername(username, parseInt(iduser), onSuccessUsername);
                }
            });

            function onSuccessUsername(result) {

                if (result == "Username is updated") {
                    var username = $("#tbUsername").val();
                    $("#<%= lblUsername.ClientID %>").text(username);
                    $("#<%= lblUsername.ClientID %>").show();
                    $("#tbUsername").hide();
                    $("#confirmChangeUsrBtn").hide();
                    $("#changeUsrBtn").show();
                    $("#cancelEdit").hide();
                }
                alert(result);
            }

            function isValidEmailAddress(emailAddress) {
                var pattern = new RegExp(/^((([a-z]|\d|[!#\$%&'\*\+\-\/=\?\^_`{\|}~]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])+(\.([a-z]|\d|[!#\$%&'\*\+\-\/=\?\^_`{\|}~]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])+)*)|((\x22)((((\x20|\x09)*(\x0d\x0a))?(\x20|\x09)+)?(([\x01-\x08\x0b\x0c\x0e-\x1f\x7f]|\x21|[\x23-\x5b]|[\x5d-\x7e]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])|(\\([\x01-\x09\x0b\x0c\x0d-\x7f]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF]))))*(((\x20|\x09)*(\x0d\x0a))?(\x20|\x09)+)?(\x22)))@((([a-z]|\d|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])|(([a-z]|\d|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])([a-z]|\d|-|\.|_|~|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])*([a-z]|\d|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])))\.)+(([a-z]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])|(([a-z]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])([a-z]|\d|-|\.|_|~|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])*([a-z]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])))\.?$/i);
                return pattern.test(emailAddress);
            };
            $("#changeEmailBtn").click(function () {
                var email = $("#<%= lblEmail.ClientID %>").text();
                $("#tbEmail").val(email);
                $("#tbEmail").show();
                $("#<%= lblEmail.ClientID %>").hide();
                $("#confirmChangeEmailBtn").show();
                $("#changeEmailBtn").hide();
                $("#cancelEditE").show();

            });

            $("#cancelEditE").click(function () {
                $("#tbEmail").hide();
                $("#confirmChangeEmailBtn").hide();
                $("#changeEmailBtn").show();
                $("#cancelEditE").hide();
                $("#<%= lblEmail.ClientID %>").show();
            })

            $("#confirmChangeEmailBtn").click(function () {
                var email = $("#tbEmail").val();
                if (email == "") {
                    alert("You must enter email!");
                }
                else if (!isValidEmailAddress(email)) {
                    alert("You must enter valid email format!");
                }
                else {
                    var iduser = $("#<%= iduser.ClientID %>").val();
                    var service = new KlubNaCitateli.ProfileService();
                    service.UpdateEmail(email, parseInt(iduser), onSuccessEmail);
                }
            });

            function onSuccessEmail(result) {

                if (result == "Email is updated") {
                    var email = $("#tbEmail").val();
                    $("#<%= lblEmail.ClientID %>").text(email);
                    $("#<%= lblEmail.ClientID %>").show();
                    $("#tbEmail").hide();
                    $("#confirmChangeEmailBtn").hide();
                    $("#changeEmailBtn").show();
                    $("#cancelEditE").hide();
                }
                alert(result);
            }

            $("#changeAboutBtn").click(function () {
                var username = $("#<%= lblAbout.ClientID %>").text();
                $("#tbAbout").val(username);
                $("#tbAbout").show();
                $("#<%= lblAbout.ClientID %>").hide();
                $("#confirmChangeAboutBtn").show();
                $("#changeAboutBtn").hide();
                $("#cancelEditA").show();

            });

            $("#cancelEditA").click(function () {
                $("#tbAbout").hide();
                $("#confirmChangeAboutBtn").hide();
                $("#changeAboutBtn").show();
                $("#cancelEditA").hide();
                $("#<%= lblAbout.ClientID %>").show();
            })

            $("#confirmChangeAboutBtn").click(function () {
                var username = $("#tbAbout").val();
                var iduser = $("#<%= iduser.ClientID %>").val();
                var service = new KlubNaCitateli.ProfileService();
                service.UpdateAbout(username, parseInt(iduser), onSuccessAbout);

            });

            function onSuccessAbout(result) {

                if (result == "About is updated") {
                    var username = $("#tbAbout").val();
                    $("#<%= lblAbout.ClientID %>").text(username);
                    $("#<%= lblAbout.ClientID %>").show();
                    $("#tbAbout").hide();
                    $("#confirmChangeAboutBtn").hide();
                    $("#changeAboutBtn").show();
                    $("#cancelEditA").hide();
                }
                alert(result);
            }




        });       
         
    </script>
    <style type="text/css">
       
        #mainContent_lblAbout
        {
            -webkit-box-shadow: 8px 8px 6px -6px black;
            -moz-box-shadow: 8px 8px 6px -6px black;
            box-shadow: 0 0 10px #000000;
        }
        #tbAbout
        {
            -webkit-box-shadow: 8px 8px 6px -6px black;
            -moz-box-shadow: 8px 8px 6px -6px black;
            box-shadow: 0 0 10px #000000;
        }
        .example-commentheading
        {
            position: relative;
            padding: 0;
            color: #b513af;
            min-height: 20px;
            height: auto;
            width: 90%;
            margin-top: 15px;
        }
        
        /* creates the rectangle */
        .example-commentheading:before
        {
            zoom: 2;
            content: "";
            position: absolute;
            top: 0px;
            left: -15px;
            width: 15px;
            height: 10px;
            background: #4169E1; /* css3 */
            -webkit-border-radius: 3px;
            -moz-border-radius: 3px;
            border-radius: 3px;
        }
        
        /* creates the triangle */
        .example-commentheading:after
        {
            zoom: 2;
            content: "";
            position: absolute;
            top: 6px;
            left: -9px;
            border: 4px solid transparent;
            border-left-color: #4169E1; /* reduce the damage in FF3.0 */
            display: block;
            width: 0;
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
            clear: both;
            margin-top: 50px;
        }
        #profileCategories
        {
            width: 41%;
            float: left;
            margin: 0px;
            margin-right: 20px;
            margin-left: 20px;
            padding-bottom: 15px;
        }
        #profileThreads, #userbookcomments
        {
            float: left;
            width: 23%;
            margin-top:0px !important;
            margin-right: 10px;
        }
        .profile
        {
            max-height: 200px;
            overflow: hidden;
        }
        #mainContent_profileImg
        {
            display: block;
            max-width: 100%; /* just in case, to force correct aspect ratio */
            height: auto !important;
            width: auto\9; /* ie8+9 */ /* lt ie8 */
            -ms-interpolation-mode: bicubic;
            margin-left: auto;
        }
        #mainContent_threads
        {
            height: 100px;
            width: 100%;
            color: Black;
            margin-left: 30px;
        }
        .threadname
        {
            margin-left:10px;
            color:#4169E1;
            font-weight:bold;
        }
        .threadname:hover
        {
            cursor:pointer;
        }
        .bookthumbnail
        {
            width:50px;
            height:70px;
        }
        .bookname
        {
            height:100%;
            margin-top:-100px;
            font-weight:bold;
        }
        #mainContent_comments
        {
            
            height:300px;
            overflow-y:auto;
            
        }
        .books
        {
            min-height:80px;
            height:auto;
            margin-bottom:5px;
        } 
        .bookname:hover
        {
            cursor:pointer;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="mainContent" runat="server">
    <asp:ScriptManager runat="server" ID="scriptManager">
        <Services>
            <asp:ServiceReference Path="../Services/BookService.svc" />
            <asp:ServiceReference Path="../Services/ProfileService.svc" />
        </Services>
    </asp:ScriptManager>
    <asp:HiddenField ID="iduser" runat="server" />
    <asp:HiddenField ID="idsession" runat="server" />
    <asp:Table ID="Table1" runat="server" Width="100%">
        <asp:TableRow Style="margin-bottom: 20px;">
            <asp:TableCell VerticalAlign="Top">
                <asp:Table ID="Table2" runat="server">
                    <asp:TableRow Width="200px">
                        <asp:TableCell >
                        <div class="profile">
                            <img runat="server" id="profileImg" src="/Images/user-icon.png" alt="" /></div>
                        </asp:TableCell>
                    </asp:TableRow>
                    <asp:TableRow>
                        <asp:TableCell >
                            <asp:FileUpload ID="profilePicture" CssClass="profilepic" runat="server" Visible="false" />
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
                            <asp:Label runat="server" ID="nameLbl" Font-Size="20"></asp:Label>
                        </asp:TableCell>
                    </asp:TableRow>
                    <asp:TableRow Width="100%">
                        <asp:TableCell >
                            <asp:Table ID="Table4" runat="server">
                                <asp:TableRow Width="319px">
                                    <asp:TableCell Width="150px">
                                        <asp:Label ID="Label1" runat="server" Text="Username:"></asp:Label>
                                    </asp:TableCell>
                                    <asp:TableCell Width="300px">
                                        <asp:Label ID="lblUsername" runat="server"></asp:Label>
                                        <input type="text" id="tbUsername" />
                                        <br />
                                    </asp:TableCell>
                                    <asp:TableCell>
                                        <input type="button" id="changeUsrBtn" value="Edit username"/>
                                        <input type="button" id="confirmChangeUsrBtn" value="Confirm edit"/>
                                        <input type="button" id="cancelEdit" value="Cancel"/>
                                    </asp:TableCell>
                                </asp:TableRow>
                                <asp:TableRow Width="319px">
                                    <asp:TableCell>
                                        <asp:Label ID="Label3" runat="server" Text="Email:"></asp:Label>
                                    </asp:TableCell>
                                    <asp:TableCell>
                                        <asp:Label ID="lblEmail" runat="server"></asp:Label>
                                        <input type="text" id="tbEmail" />
                                    </asp:TableCell>
                                    <asp:TableCell>
                                        <input type="button" id="changeEmailBtn" value="Edit email"/>
                                        <input type="button" id="confirmChangeEmailBtn" value="Confirm edit" />
                                         <input type="button" id="cancelEditE" value="Cancel"/>
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
                    <asp:TableRow Width="319px">
                        <asp:TableCell>
                            <asp:Label ID="lblAbout" runat="server" Text="About" Width="568px" Height="200px"></asp:Label>
                            <textarea id="tbAbout" rows="12" cols="70" style="resize:none;"></textarea><br />
                            <input type="button" id="changeAboutBtn" value="Edit about" />
                            <input type="button" id="confirmChangeAboutBtn" value="Confirm edit" />
                            <input type="button" id="cancelEditA" value="Cancel" />
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
            <legend>User threads:</legend>
            <div id="threads" runat="server">
            </div>
        </fieldset>
        <fieldset id="userbookcomments">
            <legend>Books user commented on:</legend>
            <div id="comments" runat="server">
            </div>
        </fieldset>
        <div class='nodiv'>
        </div>
    </div>
    <asp:Label runat="server" ID="lblError" Text="" Visible="true"></asp:Label>
</asp:Content>
