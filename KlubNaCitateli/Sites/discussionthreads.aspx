<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="discussionthreads.aspx.cs" Inherits="KlubNaCitateli.Sites.discussionthreads" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="../Styles/threads.css" rel="stylesheet" type="text/css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="mainContent" runat="server">
    <div id="header">
        <asp:Label ID="Label1" runat="server" CssClass="headerLabel" Text="Readers club forum">
        </asp:Label>
    </div>
    <div id="topics1" class="maintopics">
        <div class="naslov">
            General</div>
        <div id="topic1" class="topic">
            <label id="nametopic" runat="server">
                General</label><br />
            <label id="Label100" runat="server">
                Threads:</label>
            <label id="Label2" runat="server">
                50</label>
            <label runat="server" id="posts">
                Posts</label>
            <label id="Label3" runat="server">
                1200</label>
        </div>
       
    </div>
    <div id="topics2" class="maintopics">
        <div class="naslov">
            Categories</div>
        <div id="Div4" class="topic">
            <label id="Label17" runat="server">
                General</label><br />
            <label id="Label18" runat="server">
                Threads:</label>
            <label id="Label19" runat="server">
                50</label>
            <label runat="server" id="Label20">
                Posts</label>
            <label id="Label21" runat="server">
                1200</label>
        </div>
        
    </div>
    <div id="topics3" class="maintopics">
        <div class="naslov">
            Off topic</div>
        <div id="Div8" class="topic">
            <div>
                <label id="Label37" runat="server">
                    General</label><br />
                <label id="Label38" runat="server">
                    Threads:</label>
                <label id="Label39" runat="server">
                    50</label>
                <label runat="server" id="Label40">
                    Posts</label>
                <label id="Label41" runat="server">
                    1200</label>
                    <div style="display:none;" class="id"></div>
            </div>
            <div class="mostCommCat">
                <a href="#">Bla bla</a><br />
                <label>
                    Posts:</label><label>1368</label><div style="display: none;" class="id">
                        id</div>
            </div>
        </div>
       
    </div>
</asp:Content>
