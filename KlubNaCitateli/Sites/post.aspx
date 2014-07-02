<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="post.aspx.cs" Inherits="KlubNaCitateli.Sites.post" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="../Styles/post.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="../Scripts/jquery.paginate.js"></script>
    <link href="../Styles/style.css" rel="stylesheet" type="text/css" />
    <link rel="stylesheet" href="../Styles/BreadCrumb.css" type="text/css"/>
        
       
        <script src="../Scripts/jquery.easing.1.3.js" type="text/javascript" language="JavaScript">
        </script>
        <script src="../Scripts/jquery.jBreadCrumb.1.1.js" type="text/javascript" language="JavaScript"></script>
    <script type="text/javascript">
        $(document).ready(function () {

            $(".submitPost").click(function () {
                var userId = $("#<%=userId.ClientID %>").val();
                var postcomment = $(this).parent().parent().find(".newPostText").val();
                var threadId = $("#<%=threadId.ClientID %>").val();
                var service = new KlubNaCitateli.ForumService();
                service.AddComment(threadId, userId, postcomment, quoteUser, onSuccess);
            });
            var quoteUser;
            $(".replyQuote").click(function () {
                var stringQuote = "[quote]";
                var text = $(this).parent().parent().parent().find('.comments').html();
                quoteUser = $(this).parent().parent().parent().find('.usernameLink').html();
                stringQuote += text;
                stringQuote += "[/quote]";
                $(".newPostText").val(stringQuote);
                $(".newPostText").focus();
            });


            var numPages = parseInt($("#<%=numPages.ClientID %>").val());
            $("#demo").hide();

            if (numPages > 0) {
                $("#demo").show();
            }

            $(".demos").hide();
            $("#demo1").show();

            $(".usernameLink").click(function () {
                var id = $(this).parent().parent().find(".iduser").html();
                window.location = "profile.aspx?id=" + id;
            });

            var id;
            function onSuccess(result) {

                alert(result);

            }

            $(".comm").delegate(".banUser", "click", function () {
                id = $(this).parent().parent().find(".iduser").html();
                var service = new KlubNaCitateli.ForumService();
                service.BannedUser(true, parseInt(id), onSuccess);
                $(".iduser:contains(" + id + ")").parent().parent().parent().find(".banUser").removeClass("banUser").addClass("unBanUser").html("[ Unban user ]");
            });



            $(".comm").delegate(".unBanUser", "click", function () {
                id = $(this).parent().parent().find(".iduser").html();
                var service = new KlubNaCitateli.ForumService();
                service.BannedUser(false, parseInt(id), onSuccess);
                $(".iduser:contains(" + id + ")").parent().parent().parent().find(".unBanUser").removeClass("unBanUser").addClass("banUser").html("[ Ban user ]");
            });

            $(".comments").each(function () {
                if ($(this).text() == "This post was deleted by a moderator.") {
                    $(this).css("font-style", "italic");
                }
            });

            $(".deletePost").click(function () {

                var idcomment = $(this).parent().parent().find(".idcomment").text();
                var service = new KlubNaCitateli.ForumService();
                service.DeleteComment(parseInt(idcomment), onSuccess);
                $(".idcomment:contains(" + idcomment + ")").parent().find(".comments").html("<div style='font-style:italic;'>This post was deleted by a moderator.</div>")
            });

            var pages;
            $("#demo").paginate({
                count: numPages,
                start: 1,
                display: 10,
                text_color: '#888',
                background_color: '#EEE',
                text_hover_color: 'black',
                background_hover_color: '#CFCFCF',
                images: false,
                mouse: 'press',
                onChange: function (page) {
                    pages = page;
                    $(".demos").hide();
                    $("#demo" + page).show();

                }
            });


            
           
            $("#breadCrumb3").jBreadCrumb();

        });  
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="mainContent" runat="server">
    <asp:ScriptManager runat="server" ID="scriptManager">
        <Services>
            <asp:ServiceReference Path="../Services/ForumService.svc" />
        </Services>
    </asp:ScriptManager>
    <asp:HiddenField ID="numPages" runat="server" />
    <asp:HiddenField ID="userId" runat="server" />
    <asp:HiddenField ID="threadId" runat="server" />
    <div id="progress-bar">
     <div class="breadCrumbHolder module">
                <div id="breadCrumb3" class="breadCrumb module">
                    <ul>
                        <li>
                            <a href="index.aspx">Home</a>
                        </li>
                        <li>
                            <a href="forum.aspx">Forum</a>
                        </li>
                        <li>
                            <a runat="server" id="topic"></a>
                        </li>
                        <li runat="server" id="thread">
                            
                        </li>
                    </ul>
                </div>
            </div>

   
       
    </div>
    <div class="maincontent">
        <div id="commentsarea" class="commentsarea" runat="server">
        </div>
         
        <div class="help">
         <div id='demo'></div>
        <div class="help2">
        <div id="newpost" class="newpostarea" runat="server">
            <div class="text">
                <textarea rows="10" cols="100" style="resize:none;" class="newPostText"></textarea>
            </div>
            <div class="btnDiv">
                <input type="button" value="Post comment" class="submitPost" /></div>
            <div class="noDiv">
            </div>
        </div>
        </div>
       
        </div>
        </div>
   
</asp:Content>
