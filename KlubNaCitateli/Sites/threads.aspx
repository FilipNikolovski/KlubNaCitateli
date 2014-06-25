<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="threads.aspx.cs" Inherits="KlubNaCitateli.Sites.threads" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="../Styles/threads.css" rel="stylesheet" type="text/css" />
    <script>
        $(document).ready(function () {

            $(".threadname").click(function () {
                window.location = "post.aspx?threadid=" + $(this).parent().find(".id").text();

            });
            $(".threadname").hover(function () {
                $(this).addClass("hover");
            }, function () { $(this).removeClass("hover"); });

            $(".topic").hover(function () {
                $(this).addClass("hoverthread");
            }, function () { $(this).removeClass("hoverthread"); });

            $(".userD").click(function () {
                window.location = "profile.aspx?id=" + $(this).parent().find(".iduser").text();

            });
            $(".userD").hover(function () {
                $(this).addClass("hover");
            }, function () { $(this).removeClass("hover"); });

            $("#btnAddThread").on("click", function () { alert("ahhaha"); });


        });
    </script>
    <style>
        .hoverthread
        {
            opacity: 0.8;
            background-color: #e0e0e0;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="mainContent" runat="server">
    <div>
        <div id="progressbar" runat="server">
            hnveibiurebuierbibreeur</div>
            
           
        
        <div id="topics" class="maintopics" runat="server">
        </div>
    </div>
</asp:Content>
