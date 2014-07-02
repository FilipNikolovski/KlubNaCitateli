using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MySql.Data.MySqlClient;
using System.Configuration;
using KlubNaCitateli.Classes;
using System.Text;

namespace KlubNaCitateli.Sites
{
    public partial class book : System.Web.UI.Page
    {
        public float StarRating { get; set; }
        public int IDBook { get; set; }
        public int IDUser { get; set; }
        public bool HasVoted { get; set; }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

                if (Session["Id"] != null)
                {
                    IDUser = Int32.Parse(Session["Id"].ToString());
                }
                else
                {
                    IDUser = -1;
                }

                string bookID = Request.QueryString["id"];

                if (IsContainingID(bookID))
                {
                    SetImgAndInfo(bookID);
                    SetBookComments(bookID);
                    IDBook = Int32.Parse(bookID);
                    if (Session["Id"] != null)
                        HasVoted = HasUserVoted(bookID, Session["Id"].ToString());
                    else
                        HasVoted = false;
                }
                else
                {
                    Response.Redirect("~/Sites/error.aspx");
                }

                if (Session["Id"] != null && Session["Type"].ToString().Equals("administrator"))
                {
                    allTags.Visible = true;
                    btnSaveTags.Visible = true;
                }
            }
        }

        public bool HasUserVoted(string bookID, string userID)
        {
            using (MySqlConnection conn = new MySqlConnection())
            {
                conn.ConnectionString = ConfigurationManager.ConnectionStrings["BooksConn"].ConnectionString;

                try
                {
                    conn.Open();

                    string sql = "SELECT * FROM rates WHERE IDUser = ?IDUser AND IDBook = ?IDBook";

                    MySqlCommand command = new MySqlCommand(sql, conn);

                    command.Parameters.AddWithValue("?IDUser", userID);
                    command.Parameters.AddWithValue("?IDBook", bookID);

                    MySqlDataReader reader = command.ExecuteReader();

                    return reader.HasRows;
                }
                catch (Exception e)
                {
                    error.Text = e.Message;
                }
                finally
                {
                    conn.Close();
                }
            }

            return false;
        }

        public bool IsContainingID(string bookID)
        {
            using (MySqlConnection connection = new MySqlConnection())
            {
                connection.ConnectionString = ConfigurationManager.ConnectionStrings["BooksConn"].ConnectionString;

                try
                {
                    connection.Open();

                    string sql = "SELECT IDBook FROM books WHERE IDBook = ?IDBook";

                    MySqlCommand command = new MySqlCommand(sql, connection);
                    command.Parameters.AddWithValue("?IDBook", bookID);
                    MySqlDataReader reader = command.ExecuteReader();

                    if (reader.HasRows)
                        return true;
                }
                catch (Exception ex)
                {

                }
                finally
                {
                    connection.Close();
                }
            }
            return false;
        }

        public void SetImgAndInfo(string bookID)
        {
            using (MySqlConnection connection = new MySqlConnection())
            {
                connection.ConnectionString = ConfigurationManager.ConnectionStrings["BooksConn"].ConnectionString;

                try
                {
                    connection.Open();

                    //kreiranje na kniga
                    string sql = "SELECT * FROM books WHERE IDBook = ?IDBook";

                    MySqlCommand command = new MySqlCommand(sql, connection);
                    command.Parameters.AddWithValue("?IDBook", bookID);

                    MySqlDataReader reader = command.ExecuteReader();

                    Book book = new Book();
                    if (reader.Read())
                    {
                        book.IDBook = Int32.Parse(reader["IDBook"].ToString());
                        book.ISBN = reader["ISBN"].ToString();
                        book.Name = reader["Name"].ToString();
                        book.Description = reader["Description"].ToString();
                        book.Language = reader["Language"].ToString();
                        book.ImageSrc = reader["CoverLink"].ToString();
                        book.ThumbnailSrc = reader["Thumbnail"].ToString();
                        book.YearPublished = reader["YearPublished"].ToString();
                        book.DateAdded = reader["DateAdded"].ToString();
                        book.SumRating = Int32.Parse(reader["SumRating"].ToString());
                        book.NumVotes = Int32.Parse(reader["NumVotes"].ToString());
                    }

                    reader.Close();

                    //dodavanje na avtorite vo kreiranata kniga
                    sql = "SELECT Name FROM authors, wrote WHERE authors.IDAuthor=wrote.IDAuthor AND wrote.IDBook=?IDBook";

                    command.CommandText = sql;
                    command.Parameters.Clear();
                    command.Parameters.AddWithValue("?IDBook", bookID);
                    reader = command.ExecuteReader();

                    if (reader.HasRows)
                        while (reader.Read())
                            book.Authors.Add(reader["Name"].ToString());

                    reader.Close();

                    //dodavanje na kategoriite vo kreiranata kniga
                    sql = "SELECT Name FROM categories, belongsto WHERE categories.IDCategory=belongsto.IDCategory AND belongsto.IDBook=?IDBook";

                    command.CommandText = sql;
                    command.Parameters.Clear();
                    command.Parameters.AddWithValue("?IDBook", bookID);
                    reader = command.ExecuteReader();

                    if (reader.HasRows)
                        while (reader.Read())
                            book.Categories.Add(reader["Name"].ToString());

                    reader.Close();

                    //dodavanje na tagovite vo kreiranata kniga
                    sql = "SELECT Name FROM tags, tagged WHERE tags.IDTag=tagged.IDTag AND tagged.IDBook=?IDBook";

                    command.CommandText = sql;
                    command.Parameters.Clear();
                    command.Parameters.AddWithValue("?IDBook", bookID);
                    reader = command.ExecuteReader();

                    if (reader.HasRows)
                        while (reader.Read())
                            book.Tags.Add(reader["Name"].ToString());

                    reader.Close();

                    //zemanje na site tagovi koi se koristat od strana na adminot
                    sql = "SELECT * FROM Tags WHERE IDTag NOT IN (SELECT IDTag FROM tagged WHERE IDBook=?IDBook)";

                    command.CommandText = sql;
                    command.Parameters.Clear();
                    command.Parameters.AddWithValue("?IDBook", bookID);
                    reader = command.ExecuteReader();

                    StringBuilder sb = new StringBuilder();
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            sb.Append("<li><a href='' style='text-decoration: none; color: red; margin-right: 15px;'>" + "#" + reader["Name"] + "</a></li>");
                        }
                    }
                    allTags.InnerHtml = sb.ToString();
                    reader.Close();

                    //Jcarousel Recommendation Books
                    command.CommandText = "SELECT b.IDBook, b.Thumbnail FROM Books as b, BelongsTo as bt, Categories as c WHERE b.IDBook = bt.IDBook AND bt.IDCategory = c.IDCategory AND c.Name = ?CategoryName ORDER BY rand() LIMIT 10";
                    command.Connection = connection;
                    foreach (string category in book.Categories)
                    {
                        command.Parameters.Clear();
                        command.Parameters.AddWithValue("?IDBook", IDBook);
                        command.Parameters.AddWithValue("?CategoryName", category);
                        
                        reader = command.ExecuteReader();
                        if (reader.HasRows)
                        {
                            StringBuilder sb2 = new StringBuilder();
                            sb2.Append("<div class='jcarousel' data-jcarousel='true'><ul>");
                            while (reader.Read())
                            {
                                sb2.Append("<li><a href='book.aspx?id=" + reader["IDBook"] + "'><img src='" + reader["thumbnail"].ToString() + "' /></a></li>");
                            }
                            sb2.Append("</ul></div>");
                            sb2.Append("<a href='#' class='jcarousel-control-prev'>&lsaquo;</a>");
                            sb2.Append("<a href='#' class='jcarousel-control-next'>&rsaquo;</a>");
                            sb2.Append("<p class='jcarousel-pagination'></p>");

                            jcarouselWrapper.InnerHtml = sb2.ToString();
                        }
                        reader.Close();
                    }


                    //setiranje na StarRating
                    if (book.NumVotes > 0)
                        StarRating = (float)book.SumRating / (book.NumVotes * 1.0F);
                    else
                        StarRating = 0.0F;

                    //popolnuvanje na komponentite
                    if (book.ImageSrc == "defaultImage.png")
                        imgBook.ImageUrl = "~/Images/defaultImage.png";
                    else
                        imgBook.ImageUrl = book.ImageSrc;

                    lblDescription.Text = book.Description;

                    sb.Clear();
                    sb.Append(book.Name + "<br/>" + "By" + "<br/>");
                    sb.Append(string.Join(", ", book.Authors.ToArray()));

                    lblAbout.Text = sb.ToString();

                    sb.Clear();
                    sb.Append("Year:" + "<label style='margin-left: 18%;'>" + book.YearPublished + "</label>" + "<br/>" + "Rating:" + "<label style='margin-left: 14%;'>" + StarRating + "</label>" + "<br/>" + "Language:" + "<label style='margin-left: 5%;'>" + book.Language + "</label>");
                    sb.Append("<br/>" + "Categories: ");
                    sb.Append(string.Join(", ", book.Categories.ToArray()));

                    lblInfo.Text = sb.ToString();

                    sb.Clear();
                    foreach (string tag in book.Tags)
                        sb.Append("<li><a href='' style='text-decoration: none; color: red; margin-right: 15px;'>" + "#" + tag + "</a></li>");

                    tags.InnerHtml = sb.ToString();

                    //Dodavanje buy/download linkovi
                    hlAmazon.NavigateUrl = "http://www.amazon.com/gp/product/" + book.ISBN;
                    hlEbooks.NavigateUrl = "http://www.ebooks.com/searchapp/searchresults.net?term=" + book.ISBN;


                    ViewState["Book"] = book;
                }
                catch (Exception e)
                {
                    lblError.Text = e.Message + " " + e.InnerException;
                    lblError.Visible = true;
                }
                finally
                {
                    connection.Close();
                }
            }
        }

        public void SetBookComments(string IDBook)
        {
            using (MySqlConnection conn = new MySqlConnection())
            {
                conn.ConnectionString = ConfigurationManager.ConnectionStrings["BooksConn"].ConnectionString;

                try
                {
                    conn.Open();

                    string sql = "SELECT bc.*, u.Username FROM BookComments as bc, users as u WHERE bc.IDBook = ?IDBook AND u.IDUser = bc.IDUser";
                    MySqlCommand command = new MySqlCommand(sql, conn);

                    command.Parameters.AddWithValue("?IDBook", IDBook);
                    MySqlDataReader reader = command.ExecuteReader();

                    if (reader.HasRows)
                    {
                        StringBuilder sb = new StringBuilder();
                        while (reader.Read())
                        {
                            sb.Append("<div class='bubble-list'><div class='bubble clearfix'>");
                            sb.Append("<a class='bubble-username' href='profile.aspx?id=" + reader["IDUser"].ToString() + "'>" + reader["Username"] + "</a>");
                            sb.Append("<div class='bubble-content'><div class='point'></div><p class='bubble-user-comment'>" + reader["Comment"] + "</p></div></div></div>");
                        }
                        comments.InnerHtml = sb.ToString();
                    }
                    reader.Close();
                }
                catch (Exception e)
                {
                    lblError.Text = e.Message + " " + e.InnerException;
                    lblError.Visible = true;
                }
                finally
                {
                    conn.Close();
                }

            }
        }
    }
}