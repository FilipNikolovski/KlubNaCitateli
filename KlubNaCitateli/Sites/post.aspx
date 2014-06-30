<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="post.aspx.cs" Inherits="KlubNaCitateli.Sites.post" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="../Styles/post.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript">
        $(document).ready(function () {
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

            $(".deletePost").click(function () {

                var idcomment = $(this).parent().parent().find(".idcomment").text();


            })



        });  
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="mainContent" runat="server">
<asp:ScriptManager runat="server" ID="scriptManager">
        <Services>
            <asp:ServiceReference Path="../Services/ForumService.svc" />
        </Services>
    </asp:ScriptManager>
    <div id="progress-bar">
        urhouerh wo;ji
    </div>
    <div class="maincontent">
        <div id="commentsarea" class="commentsarea" runat="server">
        </div>
    </div>
</asp:Content>
