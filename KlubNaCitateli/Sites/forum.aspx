<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="forum.aspx.cs" Inherits="KlubNaCitateli.Sites.forum" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="../Styles/forum.css" rel="stylesheet" type="text/css" />
     <link rel="stylesheet" href="../Styles/BreadCrumb.css" type="text/css"/>
        
       
        <script src="../Scripts/jquery.easing.1.3.js" type="text/javascript" language="JavaScript">
        </script>
        <script src="../Scripts/jquery.jBreadCrumb.1.1.js" type="text/javascript" language="JavaScript"></script>
    <script>
        $(document).ready(function () {
            $("#breadCrumb3").jBreadCrumb();

            $("#forumLink").addClass("active");

            $(".topicLink").click(function () {

                window.location = "threads.aspx?topicid=" + $(this).parent().find(".topicid").text();
            });

            $(".topicLink").hover(function () { $(this).addClass("hover"); }, function () { $(this).removeClass("hover"); });

            $(".border").hover(function () { $(this).addClass("hoverTopic"); }, function () { $(this).removeClass("hoverTopic"); });

            $(".posts").click(function () {

                window.location = "post.aspx?threadid=" + $(this).parent().find(".postid").text();
            });

            $(".addNewTopic, .delete").hover(function () {
                $(this).addClass("hover");
            }, function () { $(this).removeClass("hover"); })


            $(".addNewTopic").on("click", function () {

                var idtopic = $(this).parent().parent().find(".idtopic").text();
                $("#<%=idtopic.ClientID %>").val(idtype);

                $("#dialog-form").dialog("open");
            });


            $(".delete").each(function () {
                $(this).on("click", function () {

                    var idtopic = $(this).siblings().find(".topicid").text();
                    $("#<%=idtopic.ClientID %>").val(idtopic);
                    $("#dialog-form1").dialog("open");
                });
            });


            $("#dialog-form").dialog({
                closeOnEscape: false,
                autoOpen: false,
                minHeight: "auto",
                show: "slide",
                width: 300,
                modal: true
            });
            $("#dialog-form1").dialog({
                closeOnEscape: false,
                autoOpen: false,
                minHeight: "auto",
                show: "slide",
                width: 300,
                modal: true
            });

            function onSuccess(result) {
                alert(result);
            }

            $("#add").click(function () {

                var idType = $("#<%=idtype.ClientID %>").val();
                var newTopicText = $("#<%=newtopictext.ClientID %>").val();

                if (newTopicText == "") {
                    alert("You must enter name.");
                }
                else {
                    var service = new KlubNaCitateli.ForumService();
                    service.AddTopic(idType, newTopicText, onSuccess);
                    $("#<%=newtopictext.ClientID %>").val("");
                    $("#dialog-form").dialog("close");
                }
            });

            $("#cancel").click(function () {
                $("#<%=newtopictext.ClientID %>").val("");
                $("#dialog-form").dialog("close");
            });

            $("#yes").click(function () {

                var idTopic = $("#<%=idtopic.ClientID %>").val();
                var service = new KlubNaCitateli.ForumService();
                service.DeleteTopic(idTopic, onSuccess);
                $("#<%=idtopic.ClientID %>").val("");
                $("#dialog-form1").dialog("close");

            });

            $("#no").click(function () {
                $("#<%=idtopic.ClientID %>").val("");
                $("#dialog-form1").dialog("close");
            });

        });
    
    </script>
    <style type="text/css">
        .hoverTopic
        {
            opacity: 0.8;
            background-color: #e0e0e0;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="mainContent" runat="server">
    <asp:ScriptManager runat="server" ID="scriptManager">
        <Services>
            <asp:ServiceReference Path="../Services/ForumService.svc" />
        </Services>
    </asp:ScriptManager>
    <asp:HiddenField ID="idtype" runat="server" />
    <asp:HiddenField ID="idtopic" runat="server" />
    <div id="dialog-form">
        <fieldset id="fieldSet1">
            <legend>New thread</legend>
            <asp:TextBox runat="server" CssClass="separateLabel" ID="newtopictext"></asp:TextBox>
        </fieldset>
        <br />
        <div id="buttons">
            <input type="button" id="add" value="Add" />
            <input type="button" id="cancel" value="Cancel" />
        </div>
    </div>
    <div id="dialog-form1">
        <fieldset id="fieldSet2">
            <legend>Delete thread</legend>
            <label class="separateLabel">
                Are you sure you want to delete this?</label>
        </fieldset>
        <br />
        <div id="buttons2">
            <input type="button" id="yes" value="Yes" />
            <input type="button" id="no" value="No" />
        </div>
    </div>
    <div class="breadCrumbHolder module">
                <div id="breadCrumb3" class="breadCrumb module">
                    <ul>
                        <li>
                            <a href="index.aspx">Home</a>
                        </li>
                        <li>
                           Forum
                        </li>
                       
                       
                    </ul>
                </div>
            </div>
    <div runat="server" id="topicsdiv">
    </div>
</asp:Content>
