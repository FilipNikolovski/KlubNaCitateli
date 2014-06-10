<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="book.aspx.cs" Inherits="KlubNaCitateli.Sites.book" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
<script type="text/javascript" src="<%= Page.ResolveClientUrl("../Scripts/jquery.tokeninput.js")%>"></script>
<link href="../Styles/token-input.css" rel="Stylesheet" type="text/css" />
<link href="../Styles/token-input-facebook.css" rel="Stylesheet" type="text/css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="mainContent" runat="server">

<div>
        <input type="text" id="demo-input" name="blah" />
        <input type="button" value="Submit" />
        <script type="text/javascript">
            $(document).ready(function () {
                $("#demo-input").tokenInput("http://shell.loopj.com/tokeninput/tvshows.php");
            });
        </script>
    </div>
</asp:Content>
