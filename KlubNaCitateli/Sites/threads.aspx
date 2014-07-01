<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="threads.aspx.cs" Inherits="KlubNaCitateli.Sites.threads" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="../Styles/threads.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="../Scripts/jquery.paginate.js"></script>
    <link rel="stylesheet" href="../Styles/BreadCrumb.css" type="text/css"/>
        
       
        <script src="../Scripts/jquery.easing.1.3.js" type="text/javascript" language="JavaScript">
        </script>
        <script src="../Scripts/jquery.jBreadCrumb.1.1.js" type="text/javascript" language="JavaScript"></script>
    <link href="../Styles/style.css" rel="stylesheet" type="text/css" />
    <script>
        $(document).ready(function () {

<<<<<<< HEAD
            $("#forumLink").addClass("active");

=======
            $("#breadCrumb3").jBreadCrumb();
>>>>>>> 9f993181614445844dd7fdc064244b1acc165379
            var numPages = parseInt($("#<%=numPages.ClientID %>").val());
            $("#demo").hide();
            if (numPages > 0) {
                $("#demo").show();
            }


            $(".threadname").click(function () {
                window.location = "post.aspx?threadid=" + $(this).parent().find(".idthread").text();

            });
            $(".demos").hide();
            $("#demo1").show();

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

            $("#btnAddThread, .delete").hover(function () {
                $(this).addClass("hover");
            }, function () { $(this).removeClass("hover"); })

            $("#btnAddThread").on("click", function () { $("#dialog-form").dialog("open"); });

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

                var idTopic = $("#<%=idtopic.ClientID %>").val();
                var newThreadText = $("#<%=newthreadtext.ClientID %>").val();
                var iduser = $("#<%=iduser.ClientID %>").val()

                if (newThreadText == "") {
                    alert("You must enter name.");
                }
                else {
                    var service = new KlubNaCitateli.ForumService();
                    service.AddThread(idTopic, newThreadText, iduser, onSuccess);
                    $("#<%=newthreadtext.ClientID %>").val("");
                    $("#dialog-form").dialog("close");
                }
            });

            $("#cancel").click(function () {
                $("#<%=newthreadtext.ClientID %>").val("");
                $("#dialog-form").dialog("close");
            });

            $("#yes").click(function () {

                var idThread = $("#<%=idthread.ClientID %>").val();
                var service = new KlubNaCitateli.ForumService();
                service.DeleteThread(idThread, onSuccess);
                $("#<%=idthread.ClientID %>").val("");
                $("#dialog-form1").dialog("close");

            });

            $("#no").click(function () {
                $("#<%=idthread.ClientID %>").val("");
                $("#dialog-form1").dialog("close");
            });

            $(".delete").each(function () {
                $(this).on("click", function () {

                    var idthread = $(this).parent().find(".idthread").text();
                    $("#<%=idthread.ClientID %>").val(idthread);
                    $("#dialog-form1").dialog("open");
                });
            });

            $("#demo").paginate({
                count: numPages,
                start: 1,
                display: 8,
                text_color: '#888',
                background_color: '#EEE',
                text_hover_color: 'black',
                background_hover_color: '#CFCFCF',
                images: false,
                mouse: 'press',
                onChange: function (page) {
                    $(".demos").hide();
                    $("#demo" + page).show();
                }


            });

            $(".topic").delegate(".lock", "click", function () {
                var idthread = $(this).parent().find(".idthread").text();
                var service = new KlubNaCitateli.ForumService();
                service.LockThread(true, parseInt(idthread), onSuccess);
                $(".idthread:contains(" + idthread + ")").parent().parent().find(".lock").removeClass("lock").addClass("unlock");
            });

            $(".topic").delegate(".unlock", "click", function () {
                var idthread = $(this).parent().find(".idthread").text();
                var service = new KlubNaCitateli.ForumService();
                service.LockThread(false, parseInt(idthread), onSuccess);
                $(".idthread:contains(" + idthread + ")").parent().parent().find(".unlock").removeClass("unlock").addClass("lock");
            });
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
    <asp:ScriptManager runat="server" ID="scriptManager">
        <Services>
            <asp:ServiceReference Path="../Services/ForumService.svc" />
        </Services>
    </asp:ScriptManager>
    
    <asp:HiddenField ID="idtopic" runat="server" />
    <asp:HiddenField ID="iduser" runat="server" />
    
    <asp:HiddenField ID="idthread" runat="server" />
     <asp:HiddenField ID="numPages" runat="server" />
    
<div id="dialog-form">
       
        <fieldset id="fieldSet1">
            <legend>New Thread</legend>
           
           <asp:TextBox runat="server" CssClass="separateLabel" ID="newthreadtext"></asp:TextBox>
        </fieldset>

        <br />
        <div id="buttons">
            <input type="button" id="add" value="Add" />
             <input type="button" id="cancel" value="Cancel" />
        </div>
        </div>
        <div id="dialog-form1">
        <fieldset id="fieldSet2">
            <legend>Delete topic</legend>
            <label class="separateLabel">
                Are you sure you want to delete this?</label>
        </fieldset>
        <br />
        <div id="buttons2">
            <input type="button" id="yes" value="Yes" />
            <input type="button" id="no" value="No" />
        </div>
    </div>
    <div>
    <div class="breadCrumbHolder module">
                <div id="breadCrumb3" class="breadCrumb module">
                    <ul>
                        <li>
                            <a href="index.aspx">Home</a>
                        </li>
                        <li>
                            <a href="forum.aspx">Forum</a>
                        </li>
                        <li runat="server" id="topic">
                           
                        </li>
                       
                    </ul>
                </div>
            </div>
    </div>
    <div id="contenta" runat="server">
        
        <div id="topics" class="maintopics" runat="server">
        </div>
        <div class="help">
         <div id="demo"></div>
         <div class="nodiv"></div>
         </div>
    </div>
   
</asp:Content>
