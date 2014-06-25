<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="forum.aspx.cs" Inherits="KlubNaCitateli.Sites.forum" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="../Styles/forum.css" rel="stylesheet" type="text/css" />
    <script>
        $(document).ready(function () {
            $(".topicLink").click(function () {

                window.location = "threads.aspx?topicid=" + $(this).parent().find(".id").text();
            });

            $(".topicLink").hover(function () { $(this).addClass("hover"); }, function () { $(this).removeClass("hover"); });

            $(".border").hover(function () { $(this).addClass("hoverTopic"); }, function () { $(this).removeClass("hoverTopic"); });

            $(".posts").click(function () {

                window.location = "post.aspx?threadid=" + $(this).parent().find(".id").text();
            });

        });
    
    </script>
    <style type="text/css">
    .hoverTopic
        {
            opacity:0.8;
            background-color:#e0e0e0;
    
        }
</style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="mainContent" runat="server">
    <div id="header">
        <asp:Label ID="Label1" runat="server" CssClass="headerLabel" Text="Readers club forum">
        </asp:Label>
    </div>
    <div runat="server" id="topicsdiv">
    
    </div>
</asp:Content>
