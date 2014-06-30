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
        public bool HasVoted { get; set; }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string bookID = Request.QueryString["id"];

                if (IsContainingID(bookID))
                {
                    SetImgAndInfo(bookID);
                    IDBook = Int32.Parse(bookID);
                    if (Session["Id"] != null)
                        HasVoted = HasUserVoted(bookID, Session["Id"].ToString());
                    else
                        HasVoted = false;
                }
                //else 
                    // Eror 404
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

                    //popolnuvanje na stars za rejting na knigata
                    if (book.NumVotes > 0)
                        StarRating = (float) book.SumRating / (book.NumVotes * 1.0F);
                    else
                        StarRating = 0.0F;

                    //popolnuvanje na komponentite
                    if (book.ImageSrc == "defaultImage.png")
                        imgBook.ImageUrl = "~/Images/defaultImage.png"; 
                    else
                        imgBook.ImageUrl = book.ImageSrc;

                    lblDescription.Text = book.Description;

                    StringBuilder sb = new StringBuilder();
                    sb.Append(book.Name + "<br/>" + "By" + "<br/>");
                    sb.Append(string.Join(", ",book.Authors.ToArray()));

                    lblAbout.Text = sb.ToString();

                    sb.Clear();
                    sb.Append("Year:   " + book.YearPublished + "<br/>" + "Rating:   " + StarRating + "<br/>" + "Language:   " + book.Language);
                    sb.Append("<br/>" + "Categories:" + "<br/>");
                    foreach (string categorie in book.Categories)
                        sb.Append(categorie + "<br/>");

                    lblInfo.Text = sb.ToString();

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
    }
}