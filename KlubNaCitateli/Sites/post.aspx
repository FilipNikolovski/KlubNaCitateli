<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="post.aspx.cs" Inherits="KlubNaCitateli.Sites.post" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="../Styles/post.css" rel="stylesheet" type="text/css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="mainContent" runat="server">
    <div id="progress-bar">urhouerh
    wo;ji
    </div>
    <div class="maincontent">
        <div class="postHeader">
        Sto e javascript?
        </div>
        <div class="commentsarea">
            <table id="tablecomments">
                <tr>
                <td id="Td1">
                        <img id="slika" runat="server" class="userpicture"/><br />
                        <label id="Label4" runat="server">
                            biurguir</label>
                    </td>
                    <td class="commentuser">
                        <div class="comment"></div>
                    </td>
                    
                </tr>
                <tr>
                <td id="Td2">
                        <img id="Img1" runat="server" class="userpicture"/><br />
                        <label id="Label1" runat="server">
                            biurguir</label>
                    </td>
                    <td class="commentuser">
                        <div class="comment"></div>
                    </td>
                    
                </tr>
                
                 
            </table>
        </div>
    </div>
</asp:Content>
