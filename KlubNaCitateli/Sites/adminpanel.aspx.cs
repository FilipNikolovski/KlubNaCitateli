using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.UI;
using System.Web.UI.WebControls;
using KlubNaCitateli.Classes;
using MySql.Data.MySqlClient;
using System.Configuration;
using System.Data;
using System.Web.Services;
using System.Drawing;
using System.IO;
using System.Drawing.Drawing2D;

namespace KlubNaCitateli.Sites
{
    public partial class adminpanel : System.Web.UI.Page
    {
        private static string api_key = "AIzaSyDQNdTLOCVjieDzeY9IZoyaMvpDy4ApRec&maxResults";

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                FillCategoriesGrid();
                FillTagsGrid();
                FillUsersGrid();

                if (Session["Type"] == null || Session["Type"].ToString() != "administrator")
                {
                    Response.Redirect("~/Sites/login.aspx");
                }
            }

        }

        private void FillCategoriesGrid()
        {
            using (MySqlConnection connection = new MySqlConnection())
            {
                connection.ConnectionString = ConfigurationManager.ConnectionStrings["BooksConn"].ConnectionString;

                try
                {
                    connection.Open();

                    string query = "SELECT * FROM Categories";

                    MySqlCommand command = new MySqlCommand(query, connection);

                    MySqlDataAdapter dataAdapter = new MySqlDataAdapter();
                    dataAdapter.SelectCommand = command;

                    DataSet ds = new DataSet();

                    dataAdapter.Fill(ds, "Categories");

                    gvCategories.DataSource = ds.Tables["Categories"];
                    gvCategories.DataBind();

                    cblCategories.DataSource = ds.Tables["Categories"];
                    cblCategories.DataTextField = "Name";
                    cblCategories.DataBind();

                    MySqlDataReader reader = command.ExecuteReader();

                    if (reader.HasRows)
                    {
                        reader.Read();
                        categories.Value += reader["Name"].ToString();

                        while (reader.Read())
                        {
                            categories.Value += "," + reader["Name"].ToString();
                        }
                        reader.Close();
                    }

                    ViewState["Categories"] = ds;
                }
                catch (Exception ex)
                {
                    lblError.Text = ex.Message;
                    lblError.Visible = true;
                }
                finally
                {
                    connection.Close();
                }
            }
        }

        private void FillUsersGrid()
        {
            using (MySqlConnection connection = new MySqlConnection())
            {
                connection.ConnectionString = ConfigurationManager.ConnectionStrings["BooksConn"].ConnectionString;

                try
                {
                    connection.Open();

                    string query = "SELECT * FROM users";

                    MySqlCommand command = new MySqlCommand(query, connection);
                    MySqlDataAdapter adapter = new MySqlDataAdapter(command);

                    DataSet ds = new DataSet();

                    adapter.Fill(ds, "Users");

                    gvUsers.DataSource = ds.Tables["Users"];
                    gvUsers.DataBind();

                    ViewState["Users"] = ds;
                }
                catch (Exception e)
                {
                    lblError.Text = e.Message;
                    lblError.Visible = true;
                }
                finally
                {
                    connection.Close();
                }
            }
        }

        private void FillTagsGrid()
        {
            using (MySqlConnection connection = new MySqlConnection())
            {
                connection.ConnectionString = ConfigurationManager.ConnectionStrings["BooksConn"].ConnectionString;

                try
                {
                    connection.Open();

                    string query = "SELECT * FROM Tags";

                    MySqlCommand command = new MySqlCommand(query, connection);

                    MySqlDataAdapter dataAdapter = new MySqlDataAdapter();
                    dataAdapter.SelectCommand = command;

                    DataSet ds = new DataSet();

                    dataAdapter.Fill(ds, "Tags");

                    gvTags.DataSource = ds.Tables["Tags"];
                    gvTags.DataBind();

                    ViewState["Tags"] = ds;
                }
                catch (Exception ex)
                {
                    lblError.Text = ex.Message;
                    lblError.Visible = true;
                }
                finally
                {
                    connection.Close();
                }
            }
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {

            using (WebClient webClient = new WebClient())
            {
                string url = "";
                string urlContents = "";
                url = "https://www.googleapis.com/books/v1/volumes?key=AIzaSyDQNdTLOCVjieDzeY9IZoyaMvpDy4ApRec&maxResults=40&q=";
                url += tbSearchBooks.Text;

                try
                {
                    urlContents = webClient.DownloadString(url);
                }
                catch (Exception exc)
                {
                    lblError.Text = exc.Message;
                    lblError.Visible = true;
                }

                if (urlContents.ToString().Trim() != "")
                {
                    Dictionary<string, object> jsonData = new Dictionary<string, object>();

                    jsonData = new JavaScriptSerializer().Deserialize<Dictionary<string, object>>(urlContents);

                    ArrayList items = jsonData["items"] as ArrayList;

                    List<string> idList = new List<string>();
                    for (int i = 0; i < items.Count; i++)
                    {
                        Dictionary<string, object> item = items[i] as Dictionary<string, object>;
                        idList.Add(item["id"].ToString());
                    }

                    //Prevzemanje lista na knigi od google.books
                    List<Book> bookList = new List<Book>();

                    foreach (string id in idList)
                    {
                        url = "https://www.googleapis.com/books/v1/volumes/";
                        url += (id + "?key=" + api_key);

                        try
                        {
                            urlContents = webClient.DownloadString(url);
                            jsonData = new JavaScriptSerializer().Deserialize<Dictionary<string, object>>(urlContents);

                            Dictionary<string, object> volumeInfo = jsonData["volumeInfo"] as Dictionary<string, object>;

                            //Kreiranje i dodavanje na kniga vo lista----------------------------------------

                            //ISBN id na kniga
                            if (volumeInfo.ContainsKey("industryIdentifiers"))
                            {
                                Book book = new Book();

                                ArrayList isbn = volumeInfo["industryIdentifiers"] as ArrayList;
                                Dictionary<string, object> isbn10 = isbn[0] as Dictionary<string, object>;
                                book.ISBN = isbn10["identifier"].ToString();

                                if (volumeInfo.ContainsKey("title"))
                                    book.Name = volumeInfo["title"].ToString();
                                else
                                    book.Name = "No title available";

                                if (volumeInfo.ContainsKey("description"))
                                    book.Description = volumeInfo["description"].ToString();
                                else
                                    book.Description = "No description available.";

                                if (volumeInfo.ContainsKey("publishedDate"))
                                    book.YearPublished = volumeInfo["publishedDate"].ToString();
                                else
                                    book.YearPublished = "-";
                                if (volumeInfo.ContainsKey("language"))
                                    book.Language = volumeInfo["language"].ToString();
                                else
                                    book.Language = "No language";

                                book.DateAdded = DateTime.Now.Year.ToString();
                                book.SumRating = 0;
                                book.NumVotes = 0;

                                //Avtori
                                if (volumeInfo.ContainsKey("authors"))
                                {
                                    ArrayList authors = volumeInfo["authors"] as ArrayList;
                                    List<string> authorList = new List<string>();
                                    for (int i = 0; i < authors.Count; i++)
                                    {
                                        authorList.Add(authors[i].ToString());
                                    }
                                    book.Authors = authorList;
                                }
                                else
                                {
                                    book.Authors = new List<string>();
                                    book.Authors.Add("-");
                                }


                                //Link kon sliki
                                Dictionary<string, object> imageLinks = volumeInfo["imageLinks"] as Dictionary<string, object>;
                                if (imageLinks.ContainsKey("small"))
                                    book.ImageSrc = imageLinks["small"].ToString();
                                else if (imageLinks.ContainsKey("medium"))
                                    book.ImageSrc = imageLinks["medium"].ToString();
                                else if (imageLinks.ContainsKey("large"))
                                    book.ImageSrc = imageLinks["large"].ToString();
                                else
                                    book.ImageSrc = "defaultImage.png";
                                if (imageLinks.ContainsKey("thumbnail"))
                                    book.ThumbnailSrc = imageLinks["thumbnail"].ToString();
                                else if (imageLinks.ContainsKey("smallThumbnail"))
                                    book.ThumbnailSrc = imageLinks["smallThumbnail"].ToString();
                                else
                                    book.ThumbnailSrc = "defaultThumb.png";

                                //-------------------------------------------------------------------------------
                                bookList.Add(book);
                            }

                        }
                        catch (Exception ex)
                        {
                            lblError.Text = ex.Message;
                            lblError.Visible = true;
                        }
                    }

                    if (bookList.Count > 0)
                    {
                        gvBooks.DataSource = bookList;
                        gvBooks.DataBind();

                        ViewState["BookList"] = bookList;

                        addAllBooks.Visible = true;
                        gvBooks.Visible = true;

                        bookField.Value = "";
                        bookIdsField.Value = "";
                    }

                }
            }
        }

        protected void btnAddCategory_Click(object sender, EventArgs e)
        {
            using (MySqlConnection connection = new MySqlConnection())
            {
                connection.ConnectionString = ConfigurationManager.ConnectionStrings["BooksConn"].ConnectionString;

                try
                {
                    connection.Open();

                    string query = "SELECT * FROM Categories WHERE Name=?Name";

                    MySqlCommand command = new MySqlCommand(query, connection);
                    command.Parameters.AddWithValue("?Name", tbCategory.Text);

                    MySqlDataReader reader = command.ExecuteReader();

                    if (!reader.HasRows)
                    {
                        reader.Close();
                        query = "INSERT INTO Categories (Name) VALUES (?Name)";
                        command.CommandText = query;
                        command.ExecuteNonQuery();
                    }
                    else
                    {
                        ClientScript.RegisterStartupScript(this.GetType(), "Alert", "$(document).ready(function(){alert('That category already exists');});", true);
                    }
                }
                catch (Exception ex)
                {
                    lblError.Text = ex.Message;
                    lblError.Visible = true;
                }
                finally
                {
                    connection.Close();
                }
            }

            FillCategoriesGrid();
        }

        protected void gvCategories_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            gvCategories.EditIndex = -1;

            DataSet ds = (DataSet)ViewState["Categories"];

            gvCategories.DataSource = ds.Tables["Categories"];
            gvCategories.DataBind();
        }

        protected void gvCategories_RowEditing(object sender, GridViewEditEventArgs e)
        {
            gvCategories.EditIndex = e.NewEditIndex;

            DataSet ds = (DataSet)ViewState["Categories"];

            gvCategories.DataSource = ds.Tables["Categories"];
            gvCategories.DataBind();
        }

        protected void gvCategories_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            using (MySqlConnection connection = new MySqlConnection())
            {
                connection.ConnectionString = ConfigurationManager.ConnectionStrings["BooksConn"].ConnectionString;

                try
                {
                    connection.Open();

                    string query = "DELETE FROM BelongsTo WHERE IDCategory=?IDCategory";

                    MySqlCommand command = new MySqlCommand(query, connection);
                    command.Parameters.AddWithValue("?IDCategory", gvCategories.DataKeys[e.RowIndex].Value);
                    command.ExecuteNonQuery();

                    query = "DELETE FROM Categories WHERE IDCategory=?IDCategory";
                    command.CommandText = query;
                    command.ExecuteNonQuery();

                }
                catch (Exception ex)
                {
                    lblError.Text = ex.Message;
                    lblError.Visible = true;
                }
                finally
                {
                    connection.Close();
                }
            }

            FillCategoriesGrid();
        }

        protected void gvCategories_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            using (MySqlConnection connection = new MySqlConnection())
            {
                connection.ConnectionString = ConfigurationManager.ConnectionStrings["BooksConn"].ConnectionString;

                try
                {
                    connection.Open();

                    GridViewRow row = (GridViewRow)gvCategories.Rows[e.RowIndex];
                    TextBox tbName = (TextBox)row.Cells[3].Controls[0];

                    string query = "UPDATE Categories SET Name=?Name WHERE IDCategory=?IDCategory";

                    MySqlCommand command = new MySqlCommand(query, connection);
                    command.Parameters.AddWithValue("?Name", tbName.Text);
                    command.Parameters.AddWithValue("?IDCategory", gvCategories.DataKeys[e.RowIndex].Value);
                    command.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    lblError.Text = ex.Message;
                    lblError.Visible = true;
                }
                finally
                {
                    connection.Close();
                }

                gvCategories.EditIndex = -1;
                FillCategoriesGrid();
            }
        }

        protected void gvTags_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            gvTags.EditIndex = -1;

            DataSet ds = (DataSet)ViewState["Tags"];

            gvTags.DataSource = ds.Tables["Tags"];
            gvTags.DataBind();
        }

        protected void gvTags_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            using (MySqlConnection connection = new MySqlConnection())
            {
                connection.ConnectionString = ConfigurationManager.ConnectionStrings["BooksConn"].ConnectionString;

                try
                {
                    connection.Open();

                    string query = "DELETE FROM Tagged WHERE IDTag=?IDTag";

                    MySqlCommand command = new MySqlCommand(query, connection);
                    command.Parameters.AddWithValue("?IDTag", gvTags.DataKeys[e.RowIndex].Value);
                    command.ExecuteNonQuery();

                    query = "DELETE FROM Tags WHERE IDTag=?IDTag";
                    command.CommandText = query;
                    command.ExecuteNonQuery();

                }
                catch (Exception ex)
                {
                    lblError.Text = ex.Message;
                    lblError.Visible = true;
                }
                finally
                {
                    connection.Close();
                }
            }

            FillTagsGrid();
        }

        protected void gvTags_RowEditing(object sender, GridViewEditEventArgs e)
        {
            gvTags.EditIndex = e.NewEditIndex;

            DataSet ds = (DataSet)ViewState["Tags"];

            gvTags.DataSource = ds.Tables["Tags"];
            gvTags.DataBind();
        }

        protected void gvTags_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            using (MySqlConnection connection = new MySqlConnection())
            {
                connection.ConnectionString = ConfigurationManager.ConnectionStrings["BooksConn"].ConnectionString;

                try
                {
                    connection.Open();

                    GridViewRow row = (GridViewRow)gvTags.Rows[e.RowIndex];
                    TextBox tbName = (TextBox)row.Cells[3].Controls[0];

                    string query = "UPDATE Tags SET Name=?Name WHERE IDTag=?IDTag";

                    MySqlCommand command = new MySqlCommand(query, connection);
                    command.Parameters.AddWithValue("?Name", tbName.Text);
                    command.Parameters.AddWithValue("?IDTag", gvTags.DataKeys[e.RowIndex].Value);
                    command.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    lblError.Text = ex.Message;
                    lblError.Visible = true;
                }
                finally
                {
                    connection.Close();
                }

                gvTags.EditIndex = -1;
                FillTagsGrid();
            }
        }

        protected void gvTags_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvTags.PageIndex = e.NewPageIndex;

            DataSet ds = (DataSet)ViewState["Tags"];

            gvTags.DataSource = ds.Tables["Tags"];
            gvTags.DataBind();
        }

        protected void btnAddTag_Click(object sender, EventArgs e)
        {
            using (MySqlConnection connection = new MySqlConnection())
            {
                connection.ConnectionString = ConfigurationManager.ConnectionStrings["BooksConn"].ConnectionString;

                try
                {
                    connection.Open();

                    string query = "SELECT * FROM Tags WHERE Name=?Name";

                    MySqlCommand command = new MySqlCommand(query, connection);
                    command.Parameters.AddWithValue("?Name", tbCategory.Text);

                    MySqlDataReader reader = command.ExecuteReader();

                    if (!reader.HasRows)
                    {
                        reader.Close();
                        query = "INSERT INTO Tags (Name) VALUES (?Name)";
                        command.CommandText = query;
                        command.ExecuteNonQuery();
                    }
                    else
                    {
                        ClientScript.RegisterStartupScript(this.GetType(), "Alert", "$(document).ready(function(){alert('That tag already exists');});", true);
                    }
                }
                catch (Exception ex)
                {
                    lblError.Text = ex.Message;
                    lblError.Visible = true;
                }
                finally
                {
                    connection.Close();
                }
            }

            FillCategoriesGrid();
        }

        protected void gvBooks_SelectedIndexChanged(object sender, EventArgs e)
        {
            bookField.Value = "";
            bookIdsField.Value = "";

            List<Book> bookList = (List<Book>)ViewState["BookList"];
            Book book = bookList[gvBooks.SelectedIndex];

            bookField.Value += book.ISBN + "[split]";
            bookField.Value += book.Name + "[split]";
            bookField.Value += book.Description + "[split]";
            bookField.Value += book.ImageSrc + "[split]";
            bookField.Value += book.ThumbnailSrc + "[split]";
            bookField.Value += book.DateAdded + "[split]";
            bookField.Value += book.YearPublished + "[split]";
            bookField.Value += book.Language + "[split]";
            foreach (string author in book.Authors)
            {
                bookField.Value += (author + ",");
            }

            addBooksFinished.Value = "true";
        }

        protected void addAllBooks_Click(object sender, EventArgs e)
        {
            bookField.Value = "";
            bookIdsField.Value = "";

            using (MySqlConnection connection = new MySqlConnection())
            {
                connection.ConnectionString = ConfigurationManager.ConnectionStrings["BooksConn"].ConnectionString;

                try
                {
                    connection.Open();

                    List<Book> bookList = ViewState["BookList"] as List<Book>;

                    if (bookList != null)
                    {
                        string query = "";
                        MySqlCommand command = new MySqlCommand();
                        MySqlDataReader reader;
                        command.Connection = connection;

                        foreach (Book book in bookList)
                        {
                            query = "select IDBook from books where Name=?Name AND ISBN=?ISBN";
                            command.CommandText = query;
                            command.Parameters.Clear();
                            command.Parameters.AddWithValue("?Name", book.Name);
                            command.Parameters.AddWithValue("?ISBN", book.ISBN);

                            reader = command.ExecuteReader();

                            if (!reader.HasRows) //Dali knigata vekje postoi
                            {
                                reader.Close();
                                //Vnesuvanje na KNIGA vo baza---------------------------------------------------------------
                                query = "INSERT INTO books (Name, Description, CoverLink, Thumbnail, YearPublished, Language, SumRating, NumVotes, DateAdded, ISBN) VALUES (?Name, ?Description, ?CoverLink, ?Thumbnail, ?YearPublished, ?Language, ?SumRating, ?NumVotes, ?DateAdded, ?ISBN)";
                                command.CommandText = query;
                                command.Parameters.Clear();
                                command.Parameters.AddWithValue("?ISBN", book.ISBN);
                                command.Parameters.AddWithValue("?Name", book.Name);
                                command.Parameters.AddWithValue("?Description", book.Description);
                                command.Parameters.AddWithValue("?CoverLink", book.ImageSrc);
                                command.Parameters.AddWithValue("?Thumbnail", book.ThumbnailSrc);
                                command.Parameters.AddWithValue("?DateAdded", book.DateAdded);
                                command.Parameters.AddWithValue("?YearPublished", book.YearPublished);
                                command.Parameters.AddWithValue("?Language", book.Language);
                                command.Parameters.AddWithValue("?SumRating", 0);
                                command.Parameters.AddWithValue("?NumVotes", 0);

                                command.ExecuteNonQuery();
                                //-----------------------------------------------------------------------------------------

                                //Vnesuvanje na AVTORI vo baza-------------------------------------------------------------
                                query = "INSERT INTO authors (Name) VALUES (?Name)";
                                command.CommandText = query;
                                foreach (string author in book.Authors)
                                {
                                    if (author.Trim() != "")
                                    {
                                        MySqlCommand cm = new MySqlCommand();
                                        cm.CommandText = "select IDAuthor from authors where name=?name";
                                        cm.Connection = connection;
                                        cm.Parameters.AddWithValue("?name", author);
                                        MySqlDataReader rd = cm.ExecuteReader();
                                        if (!rd.HasRows)
                                        {
                                            rd.Close();
                                            command.Parameters.Clear();
                                            command.Parameters.AddWithValue("?Name", author);
                                            command.ExecuteNonQuery();
                                        }
                                        else
                                        {
                                            rd.Close();
                                        }
                                    }
                                }
                                //-----------------------------------------------------------------------------------------

                                //Vnesuvanje na ID na avtori i knigi vo WROTE tabelata---------------------------------------------
                                query = "SELECT IDBook FROM books WHERE Name=?Name AND ISBN=?ISBN";
                                command.CommandText = query;
                                command.Parameters.Clear();
                                command.Parameters.AddWithValue("?Name", book.Name);
                                command.Parameters.AddWithValue("?ISBN", book.ISBN);

                                reader = command.ExecuteReader();

                                if (reader.Read())
                                {
                                    int idBook = Int32.Parse(reader["IDBook"].ToString());
                                    reader.Close();

                                    foreach (string author in book.Authors)
                                    {
                                        if (author.Trim() != "")
                                        {
                                            query = "SELECT IDAuthor FROM authors WHERE Name=?Name";
                                            command.CommandText = query;
                                            command.Parameters.Clear();
                                            command.Parameters.AddWithValue("?Name", author);

                                            reader = command.ExecuteReader();

                                            if (reader.Read())
                                            {
                                                int idAuthor = Int32.Parse(reader["IDAuthor"].ToString());
                                                reader.Close();
                                                query = "INSERT INTO wrote (IDAuthor, IDBook) VALUES (?IDAuthor, ?IDBook)";
                                                command.CommandText = query;
                                                command.Parameters.Clear();
                                                command.Parameters.AddWithValue("?IDAuthor", idAuthor);
                                                command.Parameters.AddWithValue("?IDBook", idBook);
                                                command.ExecuteNonQuery();
                                            }
                                            else
                                                reader.Close();
                                        }
                                    }
                                }
                                //-------------------------------------------------------------------------------------------------

                                query = "SELECT IDBook FROM books WHERE Name=?Name AND ISBN=?ISBN";
                                command.CommandText = query;
                                command.Parameters.Clear();
                                command.Parameters.AddWithValue("?Name", book.Name);
                                command.Parameters.AddWithValue("?ISBN", book.ISBN);

                                reader = command.ExecuteReader();
                                if (reader.HasRows)
                                {
                                    reader.Read();
                                    bookIdsField.Value += (reader["IDBook"].ToString() + ",");
                                }
                                reader.Close();
                            }
                            else
                                reader.Close();

                        }
                    }

                }
                catch (Exception ex)
                {
                    lblError.Text = ex.Message;
                    lblError.Visible = true;
                }
                finally
                {
                    connection.Close();
                }
            }

            addAllBooks.Visible = false;
            gvBooks.Visible = false;
            ViewState["BookList"] = null;

            if (bookIdsField.Value != "")
            {
                addBooksFinished.Value = "true";
            }
            else
                addBooksFinished.Value = "false";
        }

        protected void gvBooks_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvBooks.PageIndex = e.NewPageIndex;

            List<Book> bookList = ViewState["BookList"] as List<Book>;
            gvBooks.DataSource = bookList;
            gvBooks.DataBind();
        }

        protected void gvUsers_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            using (MySqlConnection connection = new MySqlConnection())
            {
                connection.ConnectionString = ConfigurationManager.ConnectionStrings["BooksConn"].ConnectionString;

                try
                {
                    connection.Open();

                    GridViewRow row = (GridViewRow)gvUsers.Rows[e.RowIndex];
                    TextBox tbType = (TextBox)row.Cells[5].Controls[0];
                    TextBox tbBanned = (TextBox)row.Cells[6].Controls[0];

                    if (tbType.Text != "administrator" && tbType.Text != "user" && tbType.Text != "moderator")
                    {
                        ClientScript.RegisterStartupScript(this.GetType(), "Alert", "$(document).ready(function(){alert('The type must be administrator, user or moderator.');});", true);
                    }
                    else if (tbBanned.Text != "0" && tbBanned.Text != "1")
                    {
                        ClientScript.RegisterStartupScript(this.GetType(), "Alert", "$(document).ready(function(){alert('You can use only 1 or 0 in the banned column.');});", true);
                    }
                    else
                    {
                        string sql = "UPDATE Users SET Type=?Type, Banned=?Banned WHERE IDUser=?IDUser";

                        MySqlCommand command = new MySqlCommand(sql, connection);
                        command.Parameters.AddWithValue("?Type", tbType.Text);
                        command.Parameters.AddWithValue("?Banned", tbBanned.Text);
                        command.Parameters.AddWithValue("?IDUser", gvUsers.DataKeys[e.RowIndex].Value);

                        command.ExecuteNonQuery();

                        gvUsers.EditIndex = -1;
                        FillUsersGrid();
                    }

                }
                catch (Exception ex)
                {
                    lblError.Text = ex.Message;
                    lblError.Visible = true;
                }
                finally
                {
                    connection.Close();
                }


            }
        }

        protected void gvUsers_RowEditing(object sender, GridViewEditEventArgs e)
        {
            gvUsers.EditIndex = e.NewEditIndex;

            DataSet ds = (DataSet)ViewState["Users"];

            gvUsers.DataSource = ds.Tables["Users"];
            gvUsers.DataBind();
        }

        protected void gvUsers_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            gvUsers.EditIndex = -1;

            DataSet ds = (DataSet)ViewState["Users"];

            gvUsers.DataSource = ds.Tables["Users"];
            gvUsers.DataBind();
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            string ISBN = tbISBN.Text;
            string BookName = tbBookName.Text;
            string Language = tbLanguage.Text;
            string YearPublished = tbYearPublished.Text;
            string Authors = tbAuthors.Text;
            string Tags = tbAddTags.Text;
            string Categories = tbAddCategories.Text;
            string Description = tbDescription.Text;

            using (MySqlConnection conn = new MySqlConnection())
            {
                conn.ConnectionString = ConfigurationManager.ConnectionStrings["BooksConn"].ConnectionString;
                try
                {
                    conn.Open();
                    string query = "SELECT * FROM Books WHERE ISBN = ?ISBN";
                    MySqlCommand command = new MySqlCommand(query, conn);

                    command.Parameters.AddWithValue("?ISBN", ISBN);
                    MySqlDataReader reader = command.ExecuteReader();

                    if (reader.HasRows)
                    {
                        string script = "<script>$(document).ready(function(){alert('The book already exists.');});</script>";
                        Page.ClientScript.RegisterStartupScript(this.GetType(), "openDialog", script);
                        return;
                    }
                    else if (imageUpload.HasFile) //Add the book to the database and upload the image
                    {
                        reader.Close();
                        string ext = Path.GetExtension(this.imageUpload.FileName);
                        if (ext == ".jpg" || ext == ".png" || ext == ".JPG" || ext == ".PNG")
                        {
                            Bitmap originalBMP = new Bitmap(imageUpload.FileContent);

                            // Calculate the new image dimensions
                            float origWidth = originalBMP.Width;
                            float origHeight = originalBMP.Height;
                            float sngRatio = origWidth / origHeight;
                            float newWidth = 300;
                            float newHeight = newWidth / sngRatio;

                            Bitmap newBMP = new Bitmap(originalBMP, (int)newWidth, (int)newHeight);

                            Graphics oGraphics = Graphics.FromImage(newBMP);
                            oGraphics.SmoothingMode = SmoothingMode.AntiAlias;
                            oGraphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
                            oGraphics.DrawImage(originalBMP, 0, 0, newWidth, newHeight);

                            newBMP.Save(Server.MapPath("~/Images/BookPicture/") + (ISBN + ext));

                            newWidth = 128;
                            newHeight = newWidth / sngRatio;

                            newBMP = new Bitmap(originalBMP, (int)newWidth, (int)newHeight);

                            oGraphics = Graphics.FromImage(newBMP);
                            oGraphics.SmoothingMode = SmoothingMode.AntiAlias;
                            oGraphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
                            oGraphics.DrawImage(originalBMP, 0, 0, newWidth, newHeight);

                            newBMP.Save(Server.MapPath("~/Images/BookPicture/") + (ISBN + "-thumbnail" + ext));

                            originalBMP.Dispose();
                            newBMP.Dispose();
                            oGraphics.Dispose();

                            //Add the book in the db
                            query = "INSERT INTO books (Name, Description, CoverLink, Thumbnail, YearPublished, Language, SumRating, NumVotes, DateAdded, ISBN) VALUES (?Name, ?Description, ?CoverLink, ?Thumbnail, ?YearPublished, ?Language, ?SumRating, ?NumVotes, ?DateAdded, ?ISBN)";
                            command.CommandText = query;
                            command.Parameters.Clear();
                            command.Parameters.AddWithValue("?ISBN", ISBN);
                            command.Parameters.AddWithValue("?Name", BookName);
                            command.Parameters.AddWithValue("?Description", Description);
                            command.Parameters.AddWithValue("?CoverLink", ISBN + ext);
                            command.Parameters.AddWithValue("?Thumbnail", ISBN + "-thumbnail" + ext);
                            command.Parameters.AddWithValue("?DateAdded", new DateTime().ToShortDateString());
                            command.Parameters.AddWithValue("?YearPublished", YearPublished);
                            command.Parameters.AddWithValue("?Language", Language);
                            command.Parameters.AddWithValue("?SumRating", 0);
                            command.Parameters.AddWithValue("?NumVotes", 0);

                            command.ExecuteNonQuery();

                            query = "INSERT INTO authors (Name) VALUES (?Name)";
                            command.CommandText = query;
                            string[] authorsList = Authors.Split(new char[] { ',' });
                            foreach (string author in authorsList)
                            {
                                if (author.Trim() != "")
                                {
                                    command.CommandText = "select IDAuthor from authors where name=?name";
                                    command.Parameters.Clear();
                                    command.Parameters.AddWithValue("?name", author);
                                    
                                    reader = command.ExecuteReader();
                                    if (!reader.HasRows)
                                    {
                                        command.Parameters.Clear();
                                        command.Parameters.AddWithValue("?Name", author);
                                        command.ExecuteNonQuery();
                                    }
                                    reader.Close();
                                }
                            }

                            query = "SELECT IDBook FROM books WHERE Name=?Name AND isbn=?isbn";
                            command.CommandText = query;
                            command.Parameters.Clear();
                            command.Parameters.AddWithValue("?Name", BookName);
                            command.Parameters.AddWithValue("?isbn", ISBN);

                            reader = command.ExecuteReader();

                            int idBook = -1;
                            if (reader.Read())
                            {
                                idBook = Int32.Parse(reader["IDBook"].ToString());

                                reader.Close();
                                foreach (string author in authorsList)
                                {
                                    if (author.Trim() != "")
                                    {
                                        query = "SELECT IDAuthor FROM authors WHERE Name=?Name";
                                        command.CommandText = query;
                                        command.Parameters.Clear();
                                        command.Parameters.AddWithValue("?Name", author);

                                        reader = command.ExecuteReader();
                                        int idAuthor = -1;

                                        if (reader.Read())
                                        {
                                            idAuthor = Int32.Parse(reader["IDAuthor"].ToString());
                                            reader.Close();
                                            query = "INSERT INTO wrote (IDAuthor, IDBook) VALUES (?IDAuthor, ?IDBook)";
                                            command.CommandText = query;
                                            command.Parameters.Clear();
                                            command.Parameters.AddWithValue("?IDAuthor", idAuthor);
                                            command.Parameters.AddWithValue("?IDBook", idBook);
                                            command.ExecuteNonQuery();
                                        }
                                    }
                                }
                            }

                            string[] categoriesList = Categories.Split(new char[] { ',' });
                            foreach (string category in categoriesList)
                            {
                                if (category.Trim() != "")
                                {
                                    query = "SELECT IDCategory FROM categories WHERE Name=?CategoryName";
                                    command.CommandText = query;
                                    command.Parameters.Clear();
                                    command.Parameters.AddWithValue("?CategoryName", category);

                                    reader = command.ExecuteReader();
                                    
                                    if (!reader.HasRows)
                                    {
                                        reader.Close();
                                        query = "INSERT INTO Categories (Name) VALUES (?Name)";
                                        command.CommandText = query;
                                        command.Parameters.Clear();
                                        command.Parameters.AddWithValue("?Name", category);
                                        command.ExecuteNonQuery();
                                    }
                                    else reader.Close();
                                }
                            }

                            foreach (string category in categoriesList)
                            {
                                if (category.Trim() != "")
                                {
                                    query = "SELECT IDCategory FROM categories WHERE Name=?CategoryName";
                                    command.CommandText = query;
                                    command.Parameters.Clear();
                                    command.Parameters.AddWithValue("?CategoryName", category);

                                    reader = command.ExecuteReader();
                                    int idCategory = -1;

                                    if (reader.Read())
                                    {
                                        idCategory = Int32.Parse(reader["IDCategory"].ToString());
                                        reader.Close();
                                        query = "INSERT INTO belongsto (IDCategory, IDBook) VALUES (?IDCategory, ?IDBook)";
                                        command.CommandText = query;
                                        command.Parameters.Clear();
                                        command.Parameters.AddWithValue("?IDCategory", idCategory);
                                        command.Parameters.AddWithValue("?IDBook", idBook);
                                        command.ExecuteNonQuery();
                                    }
                                }
                            }

                            string[] tagsList = Tags.Split(new char[] { ',' });
                            foreach (string tag in tagsList)
                            {
                                if (tag.Trim() != "")
                                {
                                    query = "SELECT IDTag FROM tags WHERE Name=?TagName";
                                    command.CommandText = query;
                                    command.Parameters.Clear();
                                    command.Parameters.AddWithValue("?TagName", tag);

                                    reader = command.ExecuteReader();

                                    if (!reader.HasRows)
                                    {
                                        reader.Close();
                                        query = "INSERT INTO tags (Name) VALUES (?Name)";
                                        command.CommandText = query;
                                        command.Parameters.Clear();
                                        command.Parameters.AddWithValue("?Name", tag);
                                        command.ExecuteNonQuery();
                                    }
                                    else reader.Close();
                                }
                            }

                            foreach (string tag in tagsList)
                            {
                                if (tag.Trim() != "")
                                {
                                    query = "SELECT IDTag FROM tags WHERE Name=?TagName";
                                    command.CommandText = query;
                                    command.Parameters.Clear();
                                    command.Parameters.AddWithValue("?TagName", tag);

                                    reader = command.ExecuteReader();
                                    int idTag = -1;

                                    if (reader.Read())
                                    {
                                        idTag = Int32.Parse(reader["IDTag"].ToString());
                                        reader.Close();
                                        query = "INSERT INTO tagged (IDTag, IDBook) VALUES (?IDTag, ?IDBook)";
                                        command.CommandText = query;
                                        command.Parameters.Clear();
                                        command.Parameters.AddWithValue("?IDTag", idTag);
                                        command.Parameters.AddWithValue("?IDBook", idBook);
                                        command.ExecuteNonQuery();
                                    }
                                }
                            }
                        }
                        else
                        {
                            string script = "<script>$(document).ready(function(){alert('The image file is not valid. Valid extensions are .jpg and .png! Try again.');});</script>";
                            Page.ClientScript.RegisterStartupScript(this.GetType(), "openDialog", script);
                            return;
                        }
                    }
                    else
                    {
                        string script = "<script>$(document).ready(function(){alert('Please choose a cover for the book.');});</script>";
                        Page.ClientScript.RegisterStartupScript(this.GetType(), "openDialog", script);
                        return;
                    }
                }
                catch (Exception ex)
                {
                }
                finally
                {
                    conn.Close();
                }
            }

        }


    }
}