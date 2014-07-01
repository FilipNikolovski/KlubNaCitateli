<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="book.aspx.cs" Inherits="KlubNaCitateli.Sites.book" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="../Styles/book.css" type="text/css" rel="Stylesheet" />
    <link href="../Styles/jquery.raty.css" type="text/css" rel="Stylesheet" />
    <script src="../Scripts/jquery.raty.js" type="text/javascript"></script>
    <script src="../Scripts/jquery.jcarousel.min.js" type="text/javascript"></script>
    <link href="../Styles/jcarousel.css" rel="stylesheet" type="text/css" />
    <script src="//code.jquery.com/ui/1.11.0/jquery-ui.js"></script>
    <script type="text/javascript">
        $(document).ready(function () {

            $("#stars").raty({
                path: "../Images/RatingImages",

                score: function () {
                    return "<%=StarRating%>";
                },

                readOnly: function () {
                    return ("<%=HasVoted%>" == "False") ? false : true;
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
                            alert("Thanks for rating! Feel free to leave a comment on the book.");
                            $("#stars").raty({ path: "../Images/RatingImages", score: parseFloat(result), readOnly: true });
                        });
                    }
                    else
                        alert("You aren't logged in. Please log in and then rate.");
                }
            });

            $("#mainContent_tags, #mainContent_allTags").sortable({
                connectWith: '.connectedSortable'
            }).disableSelection();

            var jcarousel = $('.jcarousel');

            jcarousel.jcarousel({
                wrap: 'circular',
            });


            $('.jcarousel-control-prev')
            .jcarouselControl({
                target: '-=1'
            });

            $('.jcarousel-control-next')
            .jcarouselControl({
                target: '+=1'
            });

            $('.jcarousel-pagination')
            .on('jcarouselpagination:active', 'a', function () {
                $(this).addClass('active');
            })
            .on('jcarouselpagination:inactive', 'a', function () {
                $(this).removeClass('active');
            })
            .on('click', function (e) {
                e.preventDefault();
            })
            .jcarouselPagination({
                perPage: 1,
                item: function (page) {
                    return '<a href="#' + page + '">' + page + '</a>';
                }
            });

            $("#mainContent_btnSaveTags").on("click", function () {

                var list = $("#mainContent_tags li");
                var tags = new Array();

                list.each(function () {
                    tags.push($(this).text());
                });

                var json = {
                    bookId: '<%=IDBook %>',
                    tags: tags
                };

                var service = new KlubNaCitateli.BookService();
                service.SaveTags(JSON.stringify(json), function (result) {
                    alert(result);
                });
            });

        }); 
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="mainContent" runat="server">
    <asp:ScriptManager runat="server" ID="scriptManager">
        <Services>
            <asp:ServiceReference Path="../Services/BookService.svc" />
        </Services>
    </asp:ScriptManager>
    <asp:Label ID="error" runat="server" Text=""></asp:Label>
    <table id="bookContent">
        <tr>
            <td>
                <table id="div1">
                    <tr>
                        <td>
                            <table id="imageAndInfo">
                                <tr class="tmp2">
                                    <td>
                                        <asp:Image ID="imgBook" runat="server" />
                                    </td>
                                </tr>
                                <tr class="tmp2">
                                    <td>
                                        <div id="stars">
                                        </div>
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
                                <tr class="tmp">
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
                                        <table id="booktags">
                                            <tr>
                                                <td>
                                                    <label style="margin-right: 5%">
                                                        Tags</label>
                                                    <input id="btnSaveTags" type="button" value="Save tags" runat="server" visible="false" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <ul id="tags" class="connectedSortable" runat="server">
                                                    </ul>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <ul id="allTags" class="connectedSortable" runat="server" visible="false">
                                                    </ul>
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
    </table>
    <div class="recommendations">
        <label>
            Recommendations by category:</label>
        <div id="jcarouselWrapper" runat="server">
        </div>
    </div>
    <div class="commentsSection">
        <label>
            Comments:</label>
        <div class="comments">
            <div class="bubble-list">
                <div class="bubble clearfix">
                    <img src="#">
                    <div class="bubble-content">
                        <div class="point">
                        </div>
                        <p>
                            Welcome To ThePCwizard.in - Helping Beginners. Developing Experts.</p>
                    </div>
                </div>
            </div>
            <div class="bubble-list">
                <div class="bubble clearfix">
                    <img src="#">
                    <div class="bubble-content">
                        <div class="point">
                        </div>
                        <p>
                            Welcome To ThePCwizard.in - Helping Beginners. Developing Experts.</p>
                    </div>
                </div>
            </div>
            <div class="bubble-list">
                <div class="bubble clearfix">
                    <img src="#">
                    <div class="bubble-content">
                        <div class="point">
                        </div>
                        <p>
                            Welcome To ThePCwizard.in - Helping Beginners. Developing Experts.</p>
                    </div>
                </div>
            </div>
        </div>
        <div class="commentArea">
            <input id="commentText" type="text" /><br />
            <input id="btnComment" type="button" value="Comment" />
        </div>
        <div id="nodiv"></div>
    </div>

    <asp:Label ID="lblError" runat="server" Visible="False"></asp:Label>
</asp:Content>
