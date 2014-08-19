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

        $("#btnComment").on("click", function(){
                var userId = '<%=IDUser %>';

                if(userId != -1) {
                    var comment = $("#commentText").val();

                    if(comment.trim() != '') {
                        var jsonData = {
                            userId: userId,
                            bookId: '<%=IDBook %>',
                            comment: comment
                        };
                        var service = new KlubNaCitateli.BookService();
                        
                        service.AddBookComment(JSON.stringify(jsonData), function(result) {
                            
                            var res = JSON.parse(result);
                            if(res.status == "success") {
                                var content = "<div class='bubble-list'><div class='bubble clearfix'><a class='bubble-username' href='profile.aspx?id=" + res.userid + "'>" + res.username + "</a><div class='bubble-content'><div class='point'></div><p class='bubble-user-comment'>" + res.comment + "</p></div></div></div>";
                                $("#mainContent_comments").append(content);
                            }
                            else {
                                alert(res.message);
                            }
                            
                        });
                    }
                    else {
                        alert("Your comment is empty.");
                    }      
                }
                else {
                    alert("You need to login to comment on a book.");
                }
            });
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

            $("#btnAddToFavorites").on("click", function(){
                
                var userId = '<%=IDUser %>';

                if(userId != -1) {
                    var service = new KlubNaCitateli.ProfileService();
                    var json = {
                        userId: userId,
                        bookId: '<%=IDBook %>'
                    };

                    service.AddToFavorites(JSON.stringify(json), function(result){

                        var res = JSON.parse(result);
                        if(res.status == "success") {
                            alert(res.message);
                        }
                        else {
                            alert(res.message);
                        }
                    });
                }
                else {
                    alert("You must login to add books to favorites.");
                }
            });

            $("[id^='removeComment_']").on("click", function() {
                var list = $(this).attr("id").split("_");
                var obj = {
                    bookId : list[1],
                    userId : list[2],
                    date : list[3]
                };
                var service = new KlubNaCitateli.BookService();

                service.RemoveBookComment(JSON.stringify(obj), function(result){
                    var res = JSON.parse(result);
                    if(res.status == "success") {
                        alert(res.message);
                        alert(res.commentId);
                        $("#comments").remove(res.commentId);
                    }
                    else {
                        alert(res.message);
                    }
                });

            });

        }); 
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="mainContent" runat="server">
    <asp:ScriptManager runat="server" ID="scriptManager">
        <Services>
            <asp:ServiceReference Path="../Services/BookService.svc" />
            <asp:ServiceReference Path="../Services/ProfileService.svc" />
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
                                        <input id="btnAddToFavorites" type="button" value="Add to favorites" />
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
                                                    Download / Buy Links
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:HyperLink ID="hlAmazon" runat="server">www.amazon.com</asp:HyperLink>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:HyperLink ID="hlEbooks" runat="server">www.ebooks.com</asp:HyperLink>
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
        <div id="comments" runat="server">
            <label>
                Comments:</label>
        </div>
        <div class="commentArea">
            <textarea id="commentText" rows="6" cols="50" style="resize: none;">
            </textarea>
            <br />
            <input id="btnComment" type="button" value="Comment" />
        </div>
        <div id="nodiv">
        </div>
    </div>
    <asp:Label ID="lblError" runat="server" Visible="False"></asp:Label>
</asp:Content>
