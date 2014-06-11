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
        int mostWanted = -1, mostViewed = -1, bestThisMonth = -1, category1 = -1, category2 = -1, category3 = -1;
        List<int> categoriesList = new List<int>(2);
        protected void Page_Load(object sender, EventArgs e)
        {
            MySqlConnection connection = new MySqlConnection();
            connection.ConnectionString = ConfigurationManager.ConnectionStrings["BooksConn"].ConnectionString.ToString();

            MySqlCommand command = new MySqlCommand();
            command.CommandText = "select idbook from books where (sumrating/numvotes) in (select max(sumrating/numvotes) from books) group by idbook";
            command.Connection = connection;
            connection.Open();
            MySqlDataReader reader = command.ExecuteReader();
            if (reader.Read())
            {
                mostWanted = Convert.ToInt32(reader.GetValue(0).ToString());
            }
            reader.Close();

            MySqlCommand command1 = new MySqlCommand();
            command1.CommandText = "Select name from Books where idbook=?idbook";
            command1.Parameters.AddWithValue("?idbook", mostWanted);
            command1.Connection = connection;
            MySqlDataReader reader1 = command1.ExecuteReader();
            if (reader1.Read())
            {
                mostWantedBookName.Text = reader1.GetValue(0).ToString();
            }
            reader1.Close();
            command1.CommandText = "select thumbnail from books where idbook=?idbook";
            command1.Parameters.Clear();
            command1.Parameters.AddWithValue("?idbook", mostWanted);
            reader1 = command1.ExecuteReader();
            if (reader1.Read())
            {
                mostWantedPanel.BackImageUrl = reader1.GetValue(0).ToString();
            }
            reader1.Close();


            MySqlCommand command2 = new MySqlCommand();
            command2.CommandText = "select idbook from books where (sumrating) in (select max(sumrating) from books) group by idbook";
            command2.Connection = connection;
            MySqlDataReader reader2 = command2.ExecuteReader();
            if (reader2.Read())
            {
                mostViewed = Convert.ToInt32(reader2.GetValue(0).ToString());
            }
            reader2.Close();

            MySqlCommand command3 = new MySqlCommand();
            command3.CommandText = "Select name from Books where idbook=?idbook";
            command3.Parameters.AddWithValue("?idbook", mostViewed);
            command3.Connection = connection;
            MySqlDataReader reader3 = command3.ExecuteReader();
            if (reader3.Read())
            {
                mostViewedBookName.Text = reader3.GetValue(0).ToString();
            }
            reader3.Close();
            command3.CommandText = "select thumbnail from books where idbook=?idbook";
            command3.Parameters.Clear();
            command3.Parameters.AddWithValue("?idbook", mostViewed);
            reader3 = command3.ExecuteReader();
            if (reader3.Read())
            {
                mostViewedPanel.BackImageUrl = reader3.GetValue(0).ToString();
            }

            reader3.Close();

            MySqlCommand command4 = new MySqlCommand();
            command4.CommandText = "select idbook from books where (numvotes) in (select max(numvotes) from books) group by idbook";
            command4.Connection = connection;

            MySqlDataReader reader4 = command4.ExecuteReader();
            if (reader4.Read())
            {
                bestThisMonth = Convert.ToInt32(reader4.GetValue(0).ToString());
            }
            reader4.Close();

            MySqlCommand command5 = new MySqlCommand();
            command5.CommandText = "Select name from Books where idbook=?idbook";
            command5.Parameters.AddWithValue("?idbook", bestThisMonth);
            command5.Connection = connection;
            MySqlDataReader reader5 = command5.ExecuteReader();
            if (reader5.Read())
            {
                bestThisMonthBookName.Text = reader5.GetValue(0).ToString();
            }
            reader5.Close();
            command5.CommandText = "Select thumbnail from books where idbook=?idbook";
            reader5 = command5.ExecuteReader();
            if (reader5.Read())
            {
                bestThisMonthPanel.BackImageUrl = reader5.GetValue(0).ToString();
            }
            reader5.Close();


            if (Session["Name"] != null)
            {
                /*select categories.name from categories
where idcategory in (select belongsto.idcategory from belongsto, usercategories, users 
where belongsto.idcategory=usercategories.idcategory 
	and usercategories.iduser=users.iduser 
	and users.iduser=56 order by rand()) limit 3*/

                MySqlCommand command7 = new MySqlCommand();
                command7.CommandText = "select books.name from books, belongsto, usercategories, users where books.idbook=belongsto.idbook and belongsto.idcategory=usercategories.idcategory and usercategories.iduser=users.iduser and users.iduser=?iduser limit 1";
                command7.Parameters.AddWithValue("?iduser", Session["id"]);
                command7.Connection = connection;
                MySqlDataReader reader7 = command7.ExecuteReader();
                if (reader7.Read())
                {
                    firstCategoryBookName.Text = reader7.GetValue(0).ToString();
                }
                reader7.Close();
                command7.CommandText = "select books.thumbnail from books, belongsto, usercategories, users where books.idbook=belongsto.idbook and belongsto.idcategory=usercategories.idcategory and usercategories.iduser=users.iduser and users.iduser=?iduser limit 1";
                command7.Connection = connection;
                reader7 = command7.ExecuteReader();
                if (reader7.Read())
                {
                    firstCategoryPanel.BackImageUrl = reader7.GetValue(0).ToString();
                }
                reader7.Close();
                command7.CommandText = "select categories.name from categories, belongsto, books where books.idbook=belongsto.idbook and belongsto.idcategory=categories.idcategory and books.name=?name";
                command7.Connection = connection;
                command7.Parameters.AddWithValue("?name", firstCategoryBookName);
                reader7 = command7.ExecuteReader();
                if (reader7.Read())
                {
                    firstCategoryName.Text = reader7.GetValue(0).ToString();
                }
                reader7.Close();


                command7.CommandText = "select books.name from books, belongsto, usercategories, users where books.idbook=belongsto.idbook and belongsto.idcategory=usercategories.idcategory and usercategories.iduser=users.iduser and users.iduser=?iduser order by books.idbook asc limit 1 ";
                command7.Connection = connection;
                reader7 = command7.ExecuteReader();
                if (reader7.Read())
                {
                    secondCategoryBookName.Text = reader7.GetValue(0).ToString();
                }
                reader7.Close();
                command7.CommandText = "select books.thumbnail from books, belongsto, usercategories, users where books.idbook=belongsto.idbook and belongsto.idcategory=usercategories.idcategory and usercategories.iduser=users.iduser and users.iduser=?iduser order by books.idbook asc limit 1 ";
                command7.Connection = connection;
                reader7 = command7.ExecuteReader();
                if (reader7.Read())
                {
                    secondCategoryPanel.BackImageUrl = reader7.GetValue(0).ToString();
                }
                reader7.Close();
                command7.CommandText = "select categories.name from categories, belongsto, books where books.idbook=belongsto.idbook and belongsto.idcategory=categories.idcategory and books.name=?name";
                command7.Connection = connection;
                command7.Parameters.Clear();
                command7.Parameters.AddWithValue("?name", secondCategoryBookName.Text);
                reader7 = command7.ExecuteReader();
                if (reader7.Read())
                {
                    secondCategoryName.Text = reader7.GetValue(0).ToString();
                }
                reader7.Close();


                command7.CommandText = "select books.name from books, belongsto, usercategories, users where books.idbook=belongsto.idbook and belongsto.idcategory=usercategories.idcategory and usercategories.iduser=users.iduser and users.iduser=?iduser order by books.name desc limit 1";
                command7.Parameters.AddWithValue("?iduser", Session["id"]);

                command7.Connection = connection;
                reader7 = command7.ExecuteReader();
                if (reader7.Read())
                {
                    thirdCategoryBookName.Text = reader7.GetValue(0).ToString();
                }
                reader7.Close();
                command7.CommandText = "select books.thumbnail from books, belongsto, usercategories, users where books.idbook=belongsto.idbook and belongsto.idcategory=usercategories.idcategory and usercategories.iduser=users.iduser and users.iduser=?iduser order by books.name desc limit 1 ";
                command7.Connection = connection;
                reader7 = command7.ExecuteReader();
                if (reader7.Read())
                {
                    thirdCategoryPanel.BackImageUrl = reader7.GetValue(0).ToString();
                }
                reader7.Close();


                command7.CommandText = "select categories.name from categories, belongsto, books where books.idbook=belongsto.idbook and belongsto.idcategory=categories.idcategory and books.name=?name";
                command7.Connection = connection;
                command7.Parameters.Clear();
                command7.Parameters.AddWithValue("?name", thirdCategoryBookName.Text);
                reader7 = command7.ExecuteReader();
                if (reader7.Read())
                {
                    thirdCategoryName.Text = reader7.GetValue(0).ToString();
                }
                reader7.Close();

                connection.Close();
            }
            else
            {
                MySqlCommand command6 = new MySqlCommand();
                command6.CommandText = "select idcategory from belongsto group by idcategory having count(idbook)in (select count(idbook) from belongsto group by idcategory ) order by count(idbook) desc limit 3";
                command6.Connection = connection;
                MySqlDataReader reader6 = command6.ExecuteReader();
               
                if (reader6.HasRows)
                {
                     
                    while(reader6.Read())
                    {
                        categoriesList.Add(Convert.ToInt32(reader6["idcategory"]));  
                       
                    }
                    reader6.Close();
                }
               
                command6.CommandText = "select books.idbook, books.thumbnail, books.name from books, belongsto where books.idbook=belongsto.idbook and belongsto.idcategory=?category order by rand() limit 1";
                command6.Parameters.AddWithValue("?category", categoriesList[0]);
                reader6 = command6.ExecuteReader();
                if (reader6.HasRows)
                {
                    if (reader6.Read())
                    {
                        category1 = Convert.ToInt32(reader6.GetValue(0));
                        firstCategoryBookName.Text = reader6["name"].ToString();
                        firstCategoryPanel.BackImageUrl = reader6["thumbnail"].ToString();
                       
                    }
                    reader6.Close();
                }

                command6.Parameters.Clear();
                command6.Parameters.AddWithValue("?category", categoriesList[1]);
                reader6 = command6.ExecuteReader();
                if (reader6.HasRows)
                {
                    if (reader6.Read())
                    {
                        category2 = Convert.ToInt32(reader6.GetValue(0));
                        secondCategoryBookName.Text = reader6["name"].ToString();
                        secondCategoryPanel.BackImageUrl = reader6["thumbnail"].ToString();
                       
                    }
                    reader6.Close();
                }


                command6.Parameters.Clear();
                command6.Parameters.AddWithValue("?category", categoriesList[2]);
                reader6 = command6.ExecuteReader();
                if (reader6.HasRows)
                {
                    if (reader6.Read())
                    {
                        category3 = Convert.ToInt32(reader6.GetValue(0));
                      
                        thirdCategoryBookName.Text = reader6["name"].ToString();
                        thirdCategoryPanel.BackImageUrl = reader6["thumbnail"].ToString();
                       
                    }
                    reader6.Close();
                }
                List<string> names = new List<string>();
                foreach(int id in categoriesList)
                {
                    command6.CommandText="Select name from categories where idcategory=?idcategory";
                    command6.Parameters.Clear();
                    command6.Parameters.AddWithValue("?idcategory", id);
                    reader6 = command6.ExecuteReader();
                    if(reader6.HasRows)
                    {
                        if(reader6.Read())
                        {
                            names.Add(reader6["name"].ToString());
                        }
                    
                    }
                    reader6.Close();
                }
                firstCategoryName.Text = names[0];
                secondCategoryName.Text = names[1];
                thirdCategoryName.Text = names[2];
                }



            }





        }








    }
