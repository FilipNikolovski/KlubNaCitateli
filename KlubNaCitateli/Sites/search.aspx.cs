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

                if (Request.QueryString["search"] != null)
                {
                    tbSearch.Text = Request.QueryString["search"].ToString();
                    FillSearchContent(Request.QueryString["search"].ToString());
                }
              
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

                    string query = "";

                    MySqlCommand command = new MySqlCommand(query, connection);
                    MySqlDataReader reader;

                    bool categorySelected = false;
                    foreach (ListItem category in cblCategories.Items)
                    {
                        if (category.Selected)
                        {
                            query = "Select books.* from books, belongsto where "
                                + "books.idbook=belongsto.idbook and belongsto.idcategory=?IDCategory and "
                                + "(books.idbook in (select tagged.idbook from tagged,tags where tagged.idtag=tags.idtag and LOWER(tags.name) like ?Search) or"
                                + " books.idbook in (select wrote.idbook from authors, wrote where authors.idauthor=wrote.idauthor and LOWER(authors.name) like ?Search)"
                                + " or LOWER(books.name) like ?Search)";

                            categorySelected = true;
                            command.CommandText = query;
                            command.Parameters.Clear();
                            command.Parameters.AddWithValue("?IDCategory", category.Value);
                            command.Parameters.AddWithValue("?Search",'%' + search.ToLower() + '%');
                            reader = command.ExecuteReader();

                            if (reader.HasRows)
                            {
                                while (reader.Read())
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
                                    book.SumRating = Int32.Parse(reader["SumRating"].ToString());
                                    book.NumVotes = Int32.Parse(reader["NumVotes"].ToString());

                                    bool flag = true;
                                    foreach (Book b in books)
                                    {
                                        if (b.IDBook == book.IDBook)
                                        {
                                            flag = false;
                                            break;
                                        }
                                    }
                                    if (flag)
                                        books.Add(book);
                                }
                                reader.Close();

                                foreach (Book book in books)
                                {
                                    query = "SELECT Name FROM Authors, Wrote WHERE Authors.IDAuthor=Wrote.IDAuthor AND Wrote.IDBook=?IDBook";

                                    command.CommandText = query;
                                    command.Parameters.Clear();
                                    command.Parameters.AddWithValue("?IDBook", book.IDBook);
                                    reader = command.ExecuteReader();

                                    if (reader.HasRows)
                                    {
                                        while (reader.Read())
                                        {
                                            book.Authors.Add(reader["Name"].ToString());
                                        }
                                    }
                                    reader.Close();
                                }
                                
                            }
                            reader.Close();
                        }
                    }

                    if ( ! categorySelected)
                    {
                        query = "Select distinct books.* from books where "
                        + "books.idbook in (select tagged.idbook from tagged,tags where tagged.idtag=tags.idtag and LOWER(tags.name) like ?Search) or"
                        + " books.idbook in (select wrote.idbook from authors, wrote where authors.idauthor=wrote.idauthor and LOWER(authors.name) like ?Search)"
                        + " or LOWER(books.name) like ?Search";

                        command.CommandText = query;
                        command.Parameters.Clear();
                        command.Parameters.AddWithValue("?Search",'%' +  search.ToLower() + '%');
                        reader = command.ExecuteReader();

                        if (reader.HasRows)
                        {
                            while (reader.Read())
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
                                book.SumRating = Int32.Parse(reader["SumRating"].ToString());
                                book.NumVotes = Int32.Parse(reader["NumVotes"].ToString());

                                bool flag = true;
                                foreach (Book b in books)
                                {
                                    if (b.IDBook == book.IDBook)
                                    {
                                        flag = false;
                                        break;
                                    }
                                }
                                if (flag)
                                    books.Add(book);
                            }
                            reader.Close();

                            foreach (Book book  in books)
                            {
                                query = "SELECT Name FROM Authors, Wrote WHERE Authors.IDAuthor=Wrote.IDAuthor AND Wrote.IDBook=?IDBook";

                                command.CommandText = query;
                                command.Parameters.Clear();
                                command.Parameters.AddWithValue("?IDBook", book.IDBook);
                                reader = command.ExecuteReader();

                                if (reader.HasRows)
                                {
                                    while (reader.Read())
                                    {
                                        book.Authors.Add(reader["Name"].ToString());
                                    }
                                }
                                reader.Close();
                            }
                            
                        }
                        reader.Close();
                    }

                    if (books.Count > 0)
                    {
                       
                        StringBuilder innerHTML = new StringBuilder();

                        innerHTML.Append("<table class='tableSearch'>");
                        foreach (Book book in books)
                        {
                            innerHTML.Append("<tr>");
                            innerHTML.Append("<td><img src=" + book.ThumbnailSrc + " /></td>");
                            innerHTML.Append("<td>" + book.Name + "</td>");
                            innerHTML.Append("<td>" + book.YearPublished + "</td>");
                            if (book.NumVotes == 0)
                                innerHTML.Append("<td>0</td>");
                            else
                                innerHTML.Append("<td>" + (float)(book.SumRating / (book.NumVotes * 1.0)) + "</td>");
                            innerHTML.Append("<td style='display:none;' class='bookId'>" + book.IDBook + "</td>");
<<<<<<< HEAD

=======
                            StringBuilder sb = new StringBuilder();
                            foreach (string author in book.Authors)
                            {
                                sb.Append(author.ToString() + "<br/>");
                            }
                            innerHTML.Append("<td>" + sb.ToString() + "</td>");
                           
>>>>>>> 95814633f269bff814387770879a0e5d59a02939
                            innerHTML.Append("</tr>");

                        }
                        innerHTML.Append("</table>");
                        
                        searchList.InnerHtml = innerHTML.ToString();
                    }

                    else
                    {
                        searchList.InnerHtml = "<div class='searchItem'><span>Search result is empty. There are no matching books with '"+search.ToString()+"' in selected categories.</span></div>";
                    }

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