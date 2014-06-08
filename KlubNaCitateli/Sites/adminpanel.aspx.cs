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
                    catch(Exception exc)
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
                                Book book = new Book();

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

                                //ISBN id na kniga
                                if (volumeInfo.ContainsKey("industryIdentifiers"))
                                {
                                    ArrayList isbn = volumeInfo["industryIdentifiers"] as ArrayList;
                                    Dictionary<string, object> isbn10 = isbn[0] as Dictionary<string, object>;
                                    book.ISBN = isbn10["identifier"].ToString();
                                }
                                else
                                    book.ISBN = "-";

                                //-------------------------------------------------------------------------------

                                bookList.Add(book);
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
                        ClientScript.RegisterStartupScript(this.GetType(), "Alert", "alert('That category already exists');", true);
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

                    string query = "DELETE FROM Categories WHERE IDCategory=?IDCategory";

                    MySqlCommand command = new MySqlCommand(query, connection);
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

        protected void gvBooks_SelectedIndexChanged(object sender, EventArgs e)
        {
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
            
            string script = "<script>$(document).ready(function(){$('#dialog-form').dialog('open');});</script>";
            Page.ClientScript.RegisterStartupScript(this.GetType(), "openDialog", script);
        }

    }
}