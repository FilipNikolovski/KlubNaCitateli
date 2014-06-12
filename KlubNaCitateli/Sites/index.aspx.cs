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
                    MySqlCommand command6 = new MySqlCommand();
                    command6.CommandText = "select categories.idcategory, name from usercategories, categories where iduser=?iduser and usercategories.idcategory=categories.idcategory order by rand() limit 6";
                    command6.Parameters.AddWithValue("?iduser", Session["id"].ToString());
                    command6.Connection = connection;
                    MySqlDataReader reader6 = command6.ExecuteReader();

                    if (reader6.HasRows)
                    {

                        while (reader6.Read())
                        {
                            categoriesList.Add(Convert.ToInt32(reader6["idcategory"]));
                            namesCategories.Add(reader6["name"].ToString());

                        }
                        reader6.Close();
                    }

                    while (categoriesList.Count < 6)
                    {
                        command6.CommandText = "select categories.idcategory, name from categories, belongsto where belongsto.idcategory=categories.idcategory group by idcategory having count(idbook) > 0 order by rand() limit 1";
                        command6.Connection = connection;
                        reader6 = command6.ExecuteReader();

                        if (reader6.HasRows)
                        {

                            if (reader6.Read())
                            {
                                categoriesList.Add(Convert.ToInt32(reader6["idcategory"]));
                                namesCategories.Add(reader6["name"].ToString());

                            }

                        }
                        reader6.Close();

                    }

                    firstCategoryName.Text = namesCategories[0];
                    secondCategoryName.Text = namesCategories[1];
                    thirdCategoryName.Text = namesCategories[2];
                    fourthCategoryName.Text = namesCategories[3];
                    fifthCategoryName.Text = namesCategories[4];
                    sixthCategoryName.Text = namesCategories[5];

                    command6.CommandText = "select books.idbook, books.thumbnail, books.name from books, belongsto where books.idbook=belongsto.idbook and belongsto.idcategory=?category order by rand() limit 1";
                    command6.Parameters.AddWithValue("?category", categoriesList[0]);
                    reader6 = command6.ExecuteReader();
                    if (reader6.HasRows)
                    {
                        if (reader6.Read())
                        {
                            category1 = Convert.ToInt32(reader6.GetValue(0));
                            firstCategoryBookName.Text = reader6["name"].ToString();
                            firstCategoryPanel.ImageUrl = reader6["thumbnail"].ToString();
                            firstCategoryPanel.PostBackUrl = "~/Sites/book.aspx?id=" + category1;
                            firstCategoryBookName.PostBackUrl = "~/Sites/book.aspx?id=" + category1;

                        }

                    }
                    reader6.Close();

                    command6.Parameters.Clear();
                    command6.Parameters.AddWithValue("?category", categoriesList[1]);
                    reader6 = command6.ExecuteReader();
                    if (reader6.HasRows)
                    {
                        if (reader6.Read())
                        {
                            category2 = Convert.ToInt32(reader6.GetValue(0));
                            secondCategoryBookName.Text = reader6["name"].ToString();
                            secondCategoryPanel.ImageUrl = reader6["thumbnail"].ToString();
                            secondCategoryPanel.PostBackUrl = "~/Sites/book.aspx?id=" + category2;
                            secondCategoryBookName.PostBackUrl = "~/Sites/book.aspx?id=" + category2;
                        }

                    }
                    reader6.Close();

                    command6.Parameters.Clear();
                    command6.Parameters.AddWithValue("?category", categoriesList[2]);
                    reader6 = command6.ExecuteReader();
                    if (reader6.HasRows)
                    {
                        if (reader6.Read())
                        {
                            category3 = Convert.ToInt32(reader6.GetValue(0));

                            thirdCategoryBookName.Text = reader6["name"].ToString();
                            thirdCategoryPanel.ImageUrl = reader6["thumbnail"].ToString();
                            thirdCategoryPanel.PostBackUrl = "~/Sites/book.aspx?id=" + category3;
                            thirdCategoryBookName.PostBackUrl = "~/Sites/book.aspx?id=" + category3;


                        }

                    }
                    reader6.Close();
                    command6.Parameters.Clear();
                    command6.Parameters.AddWithValue("?category", categoriesList[3]);
                    reader6 = command6.ExecuteReader();
                    if (reader6.HasRows)
                    {
                        if (reader6.Read())
                        {
                            category4 = Convert.ToInt32(reader6.GetValue(0));

                            fourthCategoryBookName.Text = reader6["name"].ToString();
                            fourthCategoryPanel.ImageUrl = reader6["thumbnail"].ToString();
                            fourthCategoryPanel.PostBackUrl = "~/Sites/book.aspx?id=" + category4;
                            fourthCategoryBookName.PostBackUrl = "~/Sites/book.aspx?id=" + category4;


                        }

                    }
                    reader6.Close();

                    command6.Parameters.Clear();
                    command6.Parameters.AddWithValue("?category", categoriesList[4]);
                    reader6 = command6.ExecuteReader();
                    if (reader6.HasRows)
                    {
                        if (reader6.Read())
                        {
                            category5 = Convert.ToInt32(reader6.GetValue(0));

                            fifthCategoryBookName.Text = reader6["name"].ToString();
                            fifthCategoryPanel.ImageUrl = reader6["thumbnail"].ToString();
                            fifthCategoryPanel.PostBackUrl = "~/Sites/book.aspx?id=" + category5;
                            fifthCategoryBookName.PostBackUrl = "~/Sites/book.aspx?id=" + category5;

                        }

                    }
                    reader6.Close();

                    command6.Parameters.Clear();
                    command6.Parameters.AddWithValue("?category", categoriesList[5]);
                    reader6 = command6.ExecuteReader();
                    if (reader6.HasRows)
                    {
                        if (reader6.Read())
                        {
                            category6 = Convert.ToInt32(reader6.GetValue(0));

                            sixthCategoryBookName.Text = reader6["name"].ToString();
                            sixthCategoryPanel.ImageUrl = reader6["thumbnail"].ToString();
                            sixthCategoryPanel.PostBackUrl = "~/Sites/book.aspx?id=" + category6;
                            sixthCategoryBookName.PostBackUrl = "~/Sites/book.aspx?id=" + category6;
                        }

                    }
                    reader6.Close();

                }
                else
                {
                    List<string> namesCategories = new List<string>();
                    MySqlCommand command6 = new MySqlCommand();
                    command6.CommandText = "select categories.idcategory, name from categories, belongsto where belongsto.idcategory=categories.idcategory group by idcategory having count(idbook) > 0 order by rand() limit 6";
                    command6.Connection = connection;
                    MySqlDataReader reader6 = command6.ExecuteReader();

                    if (reader6.HasRows)
                    {

                        while (reader6.Read())
                        {
                            categoriesList.Add(Convert.ToInt32(reader6["idcategory"]));
                            namesCategories.Add(reader6["name"].ToString());
                        }

                    }

                    reader6.Close();
                    while (categoriesList.Count < 6)
                    {
                        command6.CommandText = "select categories.idcategory, name from categories, belongsto where belongsto.idcategory=categories.idcategory group by idcategory having count(idbook) > 0 order by rand() limit 1";
                        command6.Connection = connection;
                        reader6 = command6.ExecuteReader();

                        if (reader6.HasRows)
                        {

                            if (reader6.Read())
                            {
                                categoriesList.Add(Convert.ToInt32(reader6["idcategory"]));
                                namesCategories.Add(reader6["name"].ToString());

                            }

                        }
                        reader6.Close();

                    }


                    firstCategoryName.Text = namesCategories[0];
                    secondCategoryName.Text = namesCategories[1];
                    thirdCategoryName.Text = namesCategories[2];
                    fourthCategoryName.Text = namesCategories[3];
                    fifthCategoryName.Text = namesCategories[4];
                    sixthCategoryName.Text = namesCategories[5];

                    command6.CommandText = "select books.idbook, books.thumbnail, books.name from books, belongsto where books.idbook=belongsto.idbook and belongsto.idcategory=?category order by rand() limit 1";
                    command6.Parameters.AddWithValue("?category", categoriesList[0]);
                    reader6 = command6.ExecuteReader();
                    if (reader6.HasRows)
                    {
                        if (reader6.Read())
                        {
                            category1 = Convert.ToInt32(reader6.GetValue(0));
                            firstCategoryBookName.Text = reader6["name"].ToString();
                            firstCategoryPanel.ImageUrl = reader6["thumbnail"].ToString();
                            firstCategoryPanel.PostBackUrl = "~/Sites/book.aspx?id=" + category1;
                            firstCategoryBookName.PostBackUrl = "~/Sites/book.aspx?id=" + category1;
                        }

                    }
                    reader6.Close();

                    command6.Parameters.Clear();
                    command6.Parameters.AddWithValue("?category", categoriesList[1]);
                    reader6 = command6.ExecuteReader();
                    if (reader6.HasRows)
                    {
                        if (reader6.Read())
                        {
                            category2 = Convert.ToInt32(reader6.GetValue(0));
                            secondCategoryBookName.Text = reader6["name"].ToString();
                            secondCategoryPanel.ImageUrl = reader6["thumbnail"].ToString();
                            secondCategoryPanel.PostBackUrl = "~/Sites/book.aspx?id=" + category2;
                            secondCategoryBookName.PostBackUrl = "~/Sites/book.aspx?id=" + category2;

                        }

                    }
                    reader6.Close();

                    command6.Parameters.Clear();
                    command6.Parameters.AddWithValue("?category", categoriesList[2]);
                    reader6 = command6.ExecuteReader();
                    if (reader6.HasRows)
                    {
                        if (reader6.Read())
                        {
                            category3 = Convert.ToInt32(reader6.GetValue(0));

                            thirdCategoryBookName.Text = reader6["name"].ToString();
                            thirdCategoryPanel.ImageUrl = reader6["thumbnail"].ToString();
                            thirdCategoryPanel.PostBackUrl = "~/Sites/book.aspx?id=" + category3;
                            thirdCategoryBookName.PostBackUrl = "~/Sites/book.aspx?id=" + category3;
                        }
                        reader6.Close();
                    }

                    command6.Parameters.Clear();
                    command6.Parameters.AddWithValue("?category", categoriesList[3]);
                    reader6 = command6.ExecuteReader();
                    if (reader6.HasRows)
                    {
                        if (reader6.Read())
                        {
                            category4 = Convert.ToInt32(reader6.GetValue(0));

                            fourthCategoryBookName.Text = reader6["name"].ToString();
                            fourthCategoryPanel.ImageUrl = reader6["thumbnail"].ToString();
                            fourthCategoryPanel.PostBackUrl = "~/Sites/book.aspx?id=" + category4;
                            fourthCategoryBookName.PostBackUrl = "~/Sites/book.aspx?id=" + category4;
                        }
                        reader6.Close();
                    }

                    command6.Parameters.Clear();
                    command6.Parameters.AddWithValue("?category", categoriesList[4]);
                    reader6 = command6.ExecuteReader();
                    if (reader6.HasRows)
                    {
                        if (reader6.Read())
                        {
                            category5 = Convert.ToInt32(reader6.GetValue(0));

                            fifthCategoryBookName.Text = reader6["name"].ToString();
                            fifthCategoryPanel.ImageUrl = reader6["thumbnail"].ToString();
                            fifthCategoryPanel.PostBackUrl = "~/Sites/book.aspx?id=" + category5;
                            fifthCategoryBookName.PostBackUrl = "~/Sites/book.aspx?id=" + category5;
                        }
                        reader6.Close();
                    }

                    command6.Parameters.Clear();
                    command6.Parameters.AddWithValue("?category", categoriesList[5]);
                    reader6 = command6.ExecuteReader();
                    if (reader6.HasRows)
                    {
                        if (reader6.Read())
                        {
                            category6 = Convert.ToInt32(reader6.GetValue(0));

                            sixthCategoryBookName.Text = reader6["name"].ToString();
                            sixthCategoryPanel.ImageUrl = reader6["thumbnail"].ToString();
                            sixthCategoryPanel.PostBackUrl = "~/Sites/book.aspx?id=" + category6;
                            sixthCategoryBookName.PostBackUrl = "~/Sites/book.aspx?id=" + category6;
                        }
                        reader6.Close();
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
