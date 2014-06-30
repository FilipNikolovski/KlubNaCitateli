<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="post.aspx.cs" Inherits="KlubNaCitateli.Sites.post" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="../Styles/post.css" rel="stylesheet" type="text/css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="mainContent" runat="server">
    <div id="progress-bar">
        urhouerh wo;ji
    </div>
    <div class="maincontent">
        <div id="commentsarea" class="commentsarea" runat="server">
            <div class="comm">
                <div class="posthead">
                    <span class="postdateold"><span class="date"></span></span>
                    <img src="../Images/deletePost.png" alt="" class="deletePost" />
                    <div class="noDiv">
                    </div>
                </div>
                <div class="postdetails">
                    <div class="userinfo">
                        <div class="banusr"><asp:LinkButton runat="server" CssClass="banUser" Text="[ Ban user ]"></asp:LinkButton></div>
                        <div class="username">
                            <a class=""><strong></strong></a>
                        </div>
                        <div class="picture">
                            <img class="userpicture" alt="" src="../Images/user-icon.png" /></div>
                            <div class="posts">
                            <label>Posts:</label><strong>120</strong></div>
                    </div>
                    <div class="posttext">
                        <div class="maintext">
                            <div class="quote">
                                <img class="leftquote" src="../Images/left-quotes.png" alt="" /> <label class="quoteBorder"> Originally posted by <label class="quoteuser"></label></label><div>
                                        <span></span> <img class="leftquote" src="../Images/right-quotes.png" alt="" /> </div>
                            </div>
                            <div class="comments">
                          
                            </div>
                        </div>
                        <div class="btnreply">
                            <div  runat="server" id="replyWithText">Reply with quote</div></div>
                    </div>
                </div>
                <div class="noDiv">
                </div>
            </div>
            
        </div>
    </div>
</asp:Content>
