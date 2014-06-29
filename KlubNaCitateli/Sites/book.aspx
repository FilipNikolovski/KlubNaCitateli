<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="book.aspx.cs" Inherits="KlubNaCitateli.Sites.book" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="../Styles/book.css" type="text/css" rel="Stylesheet" />
    <link href="../Styles/jquery.raty.css" type="text/css" rel="Stylesheet" />
    <script src="../Scripts/jquery.raty.js" type="text/javascript"></script>
    <script type="text/javascript">
        $(document).ready(function () {

            $("#stars").raty({
                path: "../Images/RatingImages",
                readOnly: function () {
                    return "<%=HasVoted %>";
                },
                score: function () {
                    return "<%=StarRating%>";
                },
                click: function (score, evt) {

                    var userId = '<%=Session["Id"]%>';
                    if (userId != null && userId !== "") {
                        var service = new KlubNaCitateli.BookService();
                        var jsonObj = {
                            id: userId,
                            rating: score.toString(),
                            bookId: '<%=IDBook%>'
                        }

                        service.RateBook(JSON.stringify(jsonObj), function (result) {
                            $(this).raty({ score: result.toFloat() });
                        });
                    }
                    else {
                        alert("You aren't logged in. Please log in and then rate.");
                    }

                }
            });

            alert(rating);
        }); 
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="mainContent" runat="server">
    <asp:ScriptManager runat="server" ID="scriptManager">
        <Services>
            <asp:ServiceReference Path="../Services/BookService.svc" />
        </Services>
    </asp:ScriptManager>

    <table id="content">
        <tr>
            <td>
                <table id="div1">
                    <tr>
                        <td>
                            <table id="imageAndInfo">
                                <tr>
                                    <td>
                                        <asp:Image ID="imgBook" runat="server" />
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <div id="stars"></div>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Button ID="btnAddFavourites" runat="server" Text="Add" />
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="lblInfo" runat="server" BackColor="#F0F0F0" BorderWidth="0px" Width="100%"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <table id="links">
                                            <tr>
                                                <td>
                                                    <label>
                                                        Download/Buy Links</label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:Label ID="lblLinks" runat="server"></asp:Label>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td>
                            <table id="about">
                                <tr id="tmp">
                                    <td>
                                        <asp:Label ID="lblAbout" runat="server"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <label>
                                            Description</label>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="lblDescription" runat="server" BackColor="#F0F0F0" BorderWidth="0px"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <table id="tags">
                                            <tr>
                                                <td>
                                                    <label>
                                                        Tags</label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:Label ID="lblTags" runat="server"></asp:Label>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table> 
            </td>
        </tr>
        <tr>
            <td>
                <table id="recomendation">
                    <tr>
                        <td>
                            <label>
                                Recomendation</label>
                            <asp:Label ID="lblError" runat="server" Visible="False"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td>
                <table id="comments">
                </table>
            </td>
        </tr>
    </table>
</asp:Content>
