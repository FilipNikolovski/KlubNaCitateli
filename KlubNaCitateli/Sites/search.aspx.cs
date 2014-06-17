using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using KlubNaCitateli.Classes;
using System.Text;
using MySql.Data.MySqlClient;
using System.Configuration;
using System.Data;

namespace KlubNaCitateli.Sites
{
    public partial class search : System.Web.UI.Page
    {

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                FillCategoriesList();
            }
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            FillSearchContent(tbSearch.Text.ToString().Trim());
        }

        private void FillCategoriesList()
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

                    cblCategories.DataSource = ds.Tables["Categories"];
                    cblCategories.DataTextField = "Name";
                    cblCategories.DataValueField = "IDCategory";
                    cblCategories.DataBind();

                    ViewState["Categories"] = ds;
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

        private void FillSearchContent(string search)
        {
            using (MySqlConnection connection = new MySqlConnection())
            {

                connection.ConnectionString = ConfigurationManager.ConnectionStrings["BooksConn"].ConnectionString;
                try
                {
                    connection.Open();

                    List<Book> books = new List<Book>();

                    string query = "Select distinct books.* from books, belongsto where "
                        + "books.idbook=belongsto.idbook and belongsto.idcategory=?IDCategory and "
                        + "(books.idbook in (select tagged.idbook from tagged,tags where tagged.idtag=tags.idtag and LOWER(tags.name) like '%' + @Search + '%') or"
                        + " books.idbook in (select wrote.idbook from authors, wrote where authors.idauthor=wrote.idauthor and LOWER(authors.name) like '%' + @Search + '%')"
                        + " or LOWER(books.name) like '%' + @Search + '%')";

                    MySqlCommand command = new MySqlCommand(query, connection);
                    MySqlDataReader reader;

                    bool categorySelected = false;
                    foreach (ListItem category in cblCategories.Items)
                    {
                        if (category.Selected)
                        {
                            categorySelected = true;
                            command.Parameters.Clear();
                            command.Parameters.AddWithValue("?IDCategory", category.Value);
                            command.Parameters.AddWithValue("@Search", search);
                            reader = command.ExecuteReader();

                            if (reader.HasRows)
                            {
                                Book book = new Book();
                                book.IDBook = Int32.Parse(reader["IDBook"].ToString());
                                book.ISBN = reader["ISBN"].ToString();
                                book.Name = reader["Name"].ToString();
                                book.Description = reader["Description"].ToString();
                                book.Language = reader["Language"].ToString();
                                book.ImageSrc = reader["CoverLink"].ToString();
                                book.ThumbnailSrc = reader["Thumbnail"].ToString();
                                book.YearPublished = reader["YearPublished"].ToString();
                                book.DateAdded = reader["DateAdded"].ToString();
                                book.SumRating = Int32.Parse(reader["Name"].ToString());
                                book.NumVotes = Int32.Parse(reader["NumVotes"].ToString());

                                books.Add(book);
                            }
                            reader.Close();
                        }
                    }

                    if (!categorySelected)
                    {
                        query = "Select distinct books.* from books where "
                        + "books.idbook in (select tagged.idbook from tagged,tags where tagged.idtag=tags.idtag and LOWER(tags.name) like '%'+?Search+'%') or"
                        + " books.idbook in (select wrote.idbook from authors, wrote where authors.idauthor=wrote.idauthor and LOWER(authors.name) like '%'+?Search+'%')"
                        + " or LOWER(books.name) like '%'+?Search+'%'";

                        command.CommandText = query;
                        command.Parameters.Clear();
                        command.Parameters.AddWithValue("?Search", search);
                        reader = command.ExecuteReader();

                        if (reader.HasRows)
                        {
                            Book book = new Book();
                            book.IDBook = Int32.Parse(reader["IDBook"].ToString());
                            book.ISBN = reader["ISBN"].ToString();
                            book.Name = reader["Name"].ToString();
                            book.Description = reader["Description"].ToString();
                            book.Language = reader["Language"].ToString();
                            book.ImageSrc = reader["CoverLink"].ToString();
                            book.ThumbnailSrc = reader["Thumbnail"].ToString();
                            book.YearPublished = reader["YearPublished"].ToString();
                            book.DateAdded = reader["DateAdded"].ToString();
                            book.SumRating = Int32.Parse(reader["Name"].ToString());
                            book.NumVotes = Int32.Parse(reader["NumVotes"].ToString());

                            books.Add(book);
                        }
                        reader.Close();
                    }

                    if (books.Count > 0)
                    {
                        StringBuilder innerHTML = new StringBuilder();
                        foreach (Book book in books)
                        {
                            StringBuilder sb = new StringBuilder();

                            sb.Append("<div class='searchItem'>");

                            sb.Append("<div class='span1'></div>");
                            sb.Append("<div class='span2'><span>");
                            foreach (string author in book.Authors)
                            {
                                sb.Append(author.ToString() + " ");
                            }
                            sb.Append("</span></div>");
                            sb.Append("<div class='span3'><span>" + book.Description + "</span></div>");
                            sb.Append("<div class='span4'><span>" + book.YearPublished + "</span></div>");

                            sb.Append("</div>");
                            innerHTML.Append(sb.ToString());
                        }

                        searchList.InnerHtml = innerHTML.ToString();
                    }

                    else
                    {
                        searchList.InnerHtml = "<div class='searchItem'><span>Search result is empty.</span></div>";
                    }

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
    }


}