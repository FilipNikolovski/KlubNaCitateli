using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MySql.Data.MySqlClient;
using System.Configuration;

namespace KlubNaCitateli.Sites
{
    public partial class index : System.Web.UI.Page
    {
        int mostWanted = -1, mostViewed = -1, bestThisMonth = -1, category1 = -1, category2 = -1, category3 = -1, category4 = -1, category5 = -1, category6 = -1;
        List<int> categoriesList = new List<int>(2);

        MySqlConnection connection;
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                connection = new MySqlConnection();
                connection.ConnectionString = ConfigurationManager.ConnectionStrings["BooksConn"].ConnectionString.ToString();
                connection.Open();
                MySqlCommand command = new MySqlCommand();
                command.CommandText = "select idbook, name, thumbnail from books where (sumrating/numvotes) in (select max(sumrating/numvotes) from books) group by idbook";
                command.Connection = connection;

                MySqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    mostWanted = Convert.ToInt32(reader["idbook"]);
                    mostWantedBookName.Text = reader["name"].ToString();
                    mostWantedPanel.ImageUrl = reader["thumbnail"].ToString();
                    mostWantedPanel.PostBackUrl = "~/Sites/book.aspx?id=" + mostWanted;
                    mostWantedBookName.PostBackUrl = "~/Sites/book.aspx?id=" + mostWanted;

                }
                reader.Close();

                command.CommandText = "select idbook, name, thumbnail from books where (sumrating) in (select max(sumrating) from books) group by idbook";
                command.Connection = connection;
                reader = command.ExecuteReader();
                if (reader.Read())
                {
                    mostViewed = Convert.ToInt32(reader["idbook"]);
                    mostViewedBookName.Text = reader["name"].ToString();
                    mostViewedPanel.ImageUrl = reader["thumbnail"].ToString();
                    mostViewedPanel.PostBackUrl = "~/Sites/book.aspx?id=" + mostViewed;
                    mostViewedBookName.PostBackUrl = "~/Sites/book.aspx?id=" + mostViewed;
                }
                reader.Close();

                command.CommandText = "select idbook, name, thumbnail from books where (numvotes) in (select max(numvotes) from books) group by idbook";
                command.Connection = connection;
                reader = command.ExecuteReader();
                if (reader.Read())
                {
                    bestThisMonth = Convert.ToInt32(reader["idbook"]);
                    bestThisMonthBookName.Text = reader["name"].ToString();
                    bestThisMonthPanel.ImageUrl = reader["thumbnail"].ToString();
                    bestThisMonthPanel.PostBackUrl = "~/Sites/book.aspx?id=" + bestThisMonth;
                    bestThisMonthBookName.PostBackUrl = "~/Sites/book.aspx?id=" + bestThisMonth;
                }
                reader.Close();

                if (Session["Name"] != null)
                {
                    List<string> namesCategories = new List<string>();

                    command.CommandText = "select categories.idcategory, name from usercategories, categories where iduser=?iduser and usercategories.idcategory=categories.idcategory order by rand() limit 6";
                    command.Parameters.AddWithValue("?iduser", Session["id"].ToString());
                    command.Connection = connection;
                    reader = command.ExecuteReader();

                    if (reader.HasRows)
                    {

                        while (reader.Read())
                        {
                            categoriesList.Add(Convert.ToInt32(reader["idcategory"]));
                            namesCategories.Add(reader["name"].ToString());

                        }
                        reader.Close();
                    }

                    while (categoriesList.Count < 6)
                    {
                        command.CommandText = "select categories.idcategory, name from categories, belongsto where belongsto.idcategory=categories.idcategory group by idcategory having count(idbook) > 0 order by rand() limit 1";
                        command.Connection = connection;
                        reader = command.ExecuteReader();

                        if (reader.HasRows)
                        {

                            if (reader.Read())
                            {
                                categoriesList.Add(Convert.ToInt32(reader["idcategory"]));
                                namesCategories.Add(reader["name"].ToString());

                            }

                        }
                        reader.Close();

                    }

                    firstCategoryName.Text = namesCategories[0];
                    secondCategoryName.Text = namesCategories[1];
                    thirdCategoryName.Text = namesCategories[2];
                    fourthCategoryName.Text = namesCategories[3];
                    fifthCategoryName.Text = namesCategories[4];
                    sixthCategoryName.Text = namesCategories[5];

                    command.CommandText = "select books.idbook, books.thumbnail, books.name from books, belongsto where books.idbook=belongsto.idbook and belongsto.idcategory=?category order by rand() limit 1";
                    command.Parameters.AddWithValue("?category", categoriesList[0]);
                    reader = command.ExecuteReader();
                    if (reader.HasRows)
                    {
                        if (reader.Read())
                        {
                            category1 = Convert.ToInt32(reader.GetValue(0));
                            firstCategoryBookName.Text = reader["name"].ToString();
                            firstCategoryPanel.ImageUrl = reader["thumbnail"].ToString();
                            firstCategoryPanel.PostBackUrl = "~/Sites/book.aspx?id=" + category1;
                            firstCategoryBookName.PostBackUrl = "~/Sites/book.aspx?id=" + category1;

                        }

                    }
                    reader.Close();

                    command.Parameters.Clear();
                    command.Parameters.AddWithValue("?category", categoriesList[1]);
                    reader = command.ExecuteReader();
                    if (reader.HasRows)
                    {
                        if (reader.Read())
                        {
                            category2 = Convert.ToInt32(reader.GetValue(0));
                            secondCategoryBookName.Text = reader["name"].ToString();
                            secondCategoryPanel.ImageUrl = reader["thumbnail"].ToString();
                            secondCategoryPanel.PostBackUrl = "~/Sites/book.aspx?id=" + category2;
                            secondCategoryBookName.PostBackUrl = "~/Sites/book.aspx?id=" + category2;
                        }

                    }
                    reader.Close();

                    command.Parameters.Clear();
                    command.Parameters.AddWithValue("?category", categoriesList[2]);
                    reader = command.ExecuteReader();
                    if (reader.HasRows)
                    {
                        if (reader.Read())
                        {
                            category3 = Convert.ToInt32(reader.GetValue(0));

                            thirdCategoryBookName.Text = reader["name"].ToString();
                            thirdCategoryPanel.ImageUrl = reader["thumbnail"].ToString();
                            thirdCategoryPanel.PostBackUrl = "~/Sites/book.aspx?id=" + category3;
                            thirdCategoryBookName.PostBackUrl = "~/Sites/book.aspx?id=" + category3;


                        }

                    }
                    reader.Close();
                    command.Parameters.Clear();
                    command.Parameters.AddWithValue("?category", categoriesList[3]);
                    reader = command.ExecuteReader();
                    if (reader.HasRows)
                    {
                        if (reader.Read())
                        {
                            category4 = Convert.ToInt32(reader.GetValue(0));

                            fourthCategoryBookName.Text = reader["name"].ToString();
                            fourthCategoryPanel.ImageUrl = reader["thumbnail"].ToString();
                            fourthCategoryPanel.PostBackUrl = "~/Sites/book.aspx?id=" + category4;
                            fourthCategoryBookName.PostBackUrl = "~/Sites/book.aspx?id=" + category4;


                        }

                    }
                    reader.Close();

                    command.Parameters.Clear();
                    command.Parameters.AddWithValue("?category", categoriesList[4]);
                    reader = command.ExecuteReader();
                    if (reader.HasRows)
                    {
                        if (reader.Read())
                        {
                            category5 = Convert.ToInt32(reader.GetValue(0));

                            fifthCategoryBookName.Text = reader["name"].ToString();
                            fifthCategoryPanel.ImageUrl = reader["thumbnail"].ToString();
                            fifthCategoryPanel.PostBackUrl = "~/Sites/book.aspx?id=" + category5;
                            fifthCategoryBookName.PostBackUrl = "~/Sites/book.aspx?id=" + category5;

                        }

                    }
                    reader.Close();

                    command.Parameters.Clear();
                    command.Parameters.AddWithValue("?category", categoriesList[5]);
                    reader = command.ExecuteReader();
                    if (reader.HasRows)
                    {
                        if (reader.Read())
                        {
                            category6 = Convert.ToInt32(reader.GetValue(0));

                            sixthCategoryBookName.Text = reader["name"].ToString();
                            sixthCategoryPanel.ImageUrl = reader["thumbnail"].ToString();
                            sixthCategoryPanel.PostBackUrl = "~/Sites/book.aspx?id=" + category6;
                            sixthCategoryBookName.PostBackUrl = "~/Sites/book.aspx?id=" + category6;
                        }

                    }
                    reader.Close();

                }
                else
                {
                    List<string> namesCategories = new List<string>();

                    command.CommandText = "select categories.idcategory, name from categories, belongsto where belongsto.idcategory=categories.idcategory group by idcategory having count(idbook) > 0 order by rand() limit 6";
                    command.Connection = connection;
                    reader = command.ExecuteReader();

                    if (reader.HasRows)
                    {

                        while (reader.Read())
                        {
                            categoriesList.Add(Convert.ToInt32(reader["idcategory"]));
                            namesCategories.Add(reader["name"].ToString());
                        }

                    }

                    reader.Close();
                    while (categoriesList.Count < 6)
                    {
                        command.CommandText = "select categories.idcategory, name from categories, belongsto where belongsto.idcategory=categories.idcategory group by idcategory having count(idbook) > 0 order by rand() limit 1";
                        command.Connection = connection;
                        reader = command.ExecuteReader();

                        if (reader.HasRows)
                        {

                            if (reader.Read())
                            {
                                categoriesList.Add(Convert.ToInt32(reader["idcategory"]));
                                namesCategories.Add(reader["name"].ToString());

                            }

                        }
                        reader.Close();

                    }


                    firstCategoryName.Text = namesCategories[0];
                    secondCategoryName.Text = namesCategories[1];
                    thirdCategoryName.Text = namesCategories[2];
                    fourthCategoryName.Text = namesCategories[3];
                    fifthCategoryName.Text = namesCategories[4];
                    sixthCategoryName.Text = namesCategories[5];

                    command.CommandText = "select books.idbook, books.thumbnail, books.name from books, belongsto where books.idbook=belongsto.idbook and belongsto.idcategory=?category order by rand() limit 1";
                    command.Parameters.AddWithValue("?category", categoriesList[0]);
                    reader = command.ExecuteReader();
                    if (reader.HasRows)
                    {
                        if (reader.Read())
                        {
                            category1 = Convert.ToInt32(reader.GetValue(0));
                            firstCategoryBookName.Text = reader["name"].ToString();
                            firstCategoryPanel.ImageUrl = reader["thumbnail"].ToString();
                            firstCategoryPanel.PostBackUrl = "~/Sites/book.aspx?id=" + category1;
                            firstCategoryBookName.PostBackUrl = "~/Sites/book.aspx?id=" + category1;
                        }

                    }
                    reader.Close();

                    command.Parameters.Clear();
                    command.Parameters.AddWithValue("?category", categoriesList[1]);
                    reader = command.ExecuteReader();
                    if (reader.HasRows)
                    {
                        if (reader.Read())
                        {
                            category2 = Convert.ToInt32(reader.GetValue(0));
                            secondCategoryBookName.Text = reader["name"].ToString();
                            secondCategoryPanel.ImageUrl = reader["thumbnail"].ToString();
                            secondCategoryPanel.PostBackUrl = "~/Sites/book.aspx?id=" + category2;
                            secondCategoryBookName.PostBackUrl = "~/Sites/book.aspx?id=" + category2;

                        }

                    }
                    reader.Close();

                    command.Parameters.Clear();
                    command.Parameters.AddWithValue("?category", categoriesList[2]);
                    reader = command.ExecuteReader();
                    if (reader.HasRows)
                    {
                        if (reader.Read())
                        {
                            category3 = Convert.ToInt32(reader.GetValue(0));

                            thirdCategoryBookName.Text = reader["name"].ToString();
                            thirdCategoryPanel.ImageUrl = reader["thumbnail"].ToString();
                            thirdCategoryPanel.PostBackUrl = "~/Sites/book.aspx?id=" + category3;
                            thirdCategoryBookName.PostBackUrl = "~/Sites/book.aspx?id=" + category3;
                        }
                        reader.Close();
                    }

                    command.Parameters.Clear();
                    command.Parameters.AddWithValue("?category", categoriesList[3]);
                    reader = command.ExecuteReader();
                    if (reader.HasRows)
                    {
                        if (reader.Read())
                        {
                            category4 = Convert.ToInt32(reader.GetValue(0));

                            fourthCategoryBookName.Text = reader["name"].ToString();
                            fourthCategoryPanel.ImageUrl = reader["thumbnail"].ToString();
                            fourthCategoryPanel.PostBackUrl = "~/Sites/book.aspx?id=" + category4;
                            fourthCategoryBookName.PostBackUrl = "~/Sites/book.aspx?id=" + category4;
                        }
                        reader.Close();
                    }

                    command.Parameters.Clear();
                    command.Parameters.AddWithValue("?category", categoriesList[4]);
                    reader = command.ExecuteReader();
                    if (reader.HasRows)
                    {
                        if (reader.Read())
                        {
                            category5 = Convert.ToInt32(reader.GetValue(0));

                            fifthCategoryBookName.Text = reader["name"].ToString();
                            fifthCategoryPanel.ImageUrl = reader["thumbnail"].ToString();
                            fifthCategoryPanel.PostBackUrl = "~/Sites/book.aspx?id=" + category5;
                            fifthCategoryBookName.PostBackUrl = "~/Sites/book.aspx?id=" + category5;
                        }
                        reader.Close();
                    }

                    command.Parameters.Clear();
                    command.Parameters.AddWithValue("?category", categoriesList[5]);
                    reader = command.ExecuteReader();
                    if (reader.HasRows)
                    {
                        if (reader.Read())
                        {
                            category6 = Convert.ToInt32(reader.GetValue(0));

                            sixthCategoryBookName.Text = reader["name"].ToString();
                            sixthCategoryPanel.ImageUrl = reader["thumbnail"].ToString();
                            sixthCategoryPanel.PostBackUrl = "~/Sites/book.aspx?id=" + category6;
                            sixthCategoryBookName.PostBackUrl = "~/Sites/book.aspx?id=" + category6;
                        }
                        reader.Close();
                    }

                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                connection.Close();
            }
        }

    }

}
