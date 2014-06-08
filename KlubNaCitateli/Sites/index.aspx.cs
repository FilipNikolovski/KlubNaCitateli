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
        int mostWanted=-1, mostViewed=-1, bestThisMonth=-1, category1=-1, category2=-1, category3=-1;
        protected void Page_Load(object sender, EventArgs e)
        {
            MySqlConnection connection = new MySqlConnection();
            connection.ConnectionString = ConfigurationManager.ConnectionStrings["BooksConn"].ConnectionString.ToString();

            MySqlCommand command = new MySqlCommand();
            command.CommandText = "Select idbook from books having sumrating/numvotes = (select max(sumrating/numvotes) from books) group by idbook";
            command.Connection = connection;
            connection.Open();
            MySqlDataReader reader = command.ExecuteReader();
            if (reader.Read())
            {
                mostWanted = Convert.ToInt32(reader.GetValue(0).ToString());
            }
            reader.Close();

            MySqlCommand command1 = new MySqlCommand();
            command1.CommandText = "Select name from Books where idbook=@idbook";
            command1.Parameters.AddWithValue("@idbook", mostWanted);
            command1.Connection = connection;
            MySqlDataReader reader1 = command1.ExecuteReader();
            if (reader1.Read())
            {
                mostWantedBookName.Text = reader1.GetValue(0).ToString();
            }
            reader1.Close();
            command1.CommandText = "select thumbnail from books where idbook=@idbook";
            command1.Parameters.AddWithValue("@idbook", mostWanted);
            reader1 = command1.ExecuteReader();
            if (reader1.Read())
            {
                mostWantedPanel.BackImageUrl = reader1.GetValue(0).ToString();
            }
            reader1.Close();


            MySqlCommand command2 = new MySqlCommand();
            command2.CommandText = "Select idbook from books having numvotes = (select max(numvotes) from books) group by idbook";
            command2.Connection = connection;
            MySqlDataReader reader2 = command2.ExecuteReader();
            if (reader2.Read())
            {
                mostViewed = Convert.ToInt32(reader2.GetValue(0).ToString());
            }
            reader2.Close();

            MySqlCommand command3 = new MySqlCommand();
            command3.CommandText = "Select name from Books where idbook=@idbook";
            command3.Parameters.AddWithValue("@idbook", mostViewed);
            command3.Connection = connection;
            MySqlDataReader reader3 = command3.ExecuteReader();
            if (reader3.Read())
            {
                mostViewedBookName.Text = reader3.GetValue(0).ToString();
            }
            reader3.Close();
            command3.CommandText = "select thumbnail from books where idbook=@idbook";
            command3.Parameters.AddWithValue("@idbook", mostViewed);
            reader3 = command3.ExecuteReader();
            if (reader3.Read())
            {
                mostViewedPanel.BackImageUrl = reader3.GetValue(0).ToString();
            }

            reader3.Close();

            MySqlCommand command4 = new MySqlCommand();
            command4.CommandText = "Select idbook from books having sumrating = (select max(sumrating) from books) group by idbook";
            command4.Connection = connection;
            
            MySqlDataReader reader4 = command4.ExecuteReader();
            if (reader4.Read())
            {
               bestThisMonth= Convert.ToInt32(reader4.GetValue(0).ToString());
            }
            reader4.Close();

            MySqlCommand command5 = new MySqlCommand();
            command5.CommandText = "Select name from Books where idbook=@idbook";
            command5.Parameters.AddWithValue("@idbook", bestThisMonth);
            command5.Connection = connection;
            MySqlDataReader reader5 = command5.ExecuteReader();
            if (reader5.Read())
            {
                bestThisMonthBookName.Text = reader5.GetValue(0).ToString();
            }
            reader5.Close();
            command5.CommandText = "Select thumbnail from books where idbook=@idbook";
            reader5 = command5.ExecuteReader();
            if (reader5.Read())
            {
                bestThisMonthPanel.BackImageUrl = reader5.GetValue(0).ToString();
            }
            reader5.Close();


            if (Session["Name"] != null)
            {

                MySqlCommand command6 = new MySqlCommand();
                command6.CommandText = "select categoryname from categories limit 3";
                command6.Connection = connection;
                MySqlDataReader reader6 = command6.ExecuteReader();
                List<string> categories = new List<string>();
                int i = 0;
                while (reader6.Read())
                {
                    categories.Add(reader6.GetValue(i).ToString());
                    i++;
                }
                reader6.Close();
                firstCategoryName.Text = categories[0];
                secondCategoryName.Text = categories[1];
                thirdCategoryName.Text = categories[2];

                MySqlCommand command7 = new MySqlCommand();
                command7.CommandText = "Select name from books, belongsto where books.idbook=belongsto.idbook having sumrating/numvotes=(select max(sumrating/numvotes) from books) group by idbook";
                command7.Connection = connection;
                MySqlDataReader reader7 = command7.ExecuteReader();
                if (reader7.Read())
                {
                    firstCategoryBookName.Text = reader7.GetValue(0).ToString();
                }
                reader7.Close();
                MySqlCommand command8 = new MySqlCommand();
                command8.CommandText="Select name from books, belongsto where books.idbook=belongsto.idbook having sumrating/numvotes=(select max(sumrating/numvotes) from books) group by idbook";
                command8.Connection = connection;
                MySqlDataReader reader8 = command8.ExecuteReader();
                if (reader8.Read())
                {
                    secondCategoryBookName.Text = reader8.GetValue(0).ToString();
                }
                reader8.Close();

                MySqlCommand command9 = new MySqlCommand();
                command9.CommandText = "Select name from books, belongsto where books.idbook=belongsto.idbook having sumrating/numvotes=(select max(sumrating/numvotes) from books) group by idbook";
                command9.Connection = connection;
                MySqlDataReader reader9 = command9.ExecuteReader();
                if (reader9.Read())
                {
                    thirdCategoryBookName.Text = reader9.GetValue(0).ToString();
                }
                reader9.Close();

                MySqlCommand command10 = new MySqlCommand();
                command10.CommandText = "Select thumbnail from books, belongsto where books.idbook=belongsto.idbook having sumrating/numvotes=(select max(sumrating/numvotes) from books) group by idbook";
                command10.Connection = connection;
                MySqlDataReader reader10 = command10.ExecuteReader();
                if(reader10.Read())
                {
                    firstCategoryPanel.BackImageUrl=reader10.GetValue(0).ToString();
                }
                reader10.Close();
                MySqlCommand command11 = new MySqlCommand();
                command11.CommandText = "Select thumbnail from books, belongsto where books.idbook=belongsto.idbook having sumrating/numvotes=(select max(sumrating/numvotes) from books) group by idbook";
                command11.Connection = connection;
                MySqlDataReader reader11 = command11.ExecuteReader();
                if (reader11.Read())
                {
                    secondCategoryPanel.BackImageUrl = reader11.GetValue(0).ToString();
                }
                reader11.Close();

                MySqlCommand command12 = new MySqlCommand();
                command12.CommandText = "Select thumbnail from books, belongsto where books.idbook=belongsto.idbook having sumrating/numvotes=(select max(sumrating/numvotes) from books) group by idbook";
                command12.Connection = connection;
                MySqlDataReader reader12=command12.ExecuteReader();
                if (reader12.Read())
                {
                    thirdCategoryPanel.BackImageUrl = reader12.GetValue(0).ToString();
                }
                reader.Close();










            }





        }





       
        

    }
}