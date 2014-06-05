using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using KlubNaCitateli.Classes;
using MySql.Data.MySqlClient;
using System.Configuration;

namespace KlubNaCitateli.Sites
{
    public partial class signup : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            
        }

        public void finishButton_click(object sender, EventArgs e)
        {
            User user = new User(name.Text, surname.Text, email.Text, username.Text, password.Text, TextBox2.Text);
            bool checkUsername = true;
            bool checkEmail = true;
            user.CheckIfUserExists(checkEmail, checkUsername);

            if (!checkEmail && !checkUsername)
            {
                finishLabel.Text = "Email and username are already in use.";
            }
            else if(!checkEmail)
            {
                finishLabel.Text="Email is already in use.";
            }
            else if (!checkUsername)
            {
                finishLabel.Text = "Username is already in use.";
            }
            else
            {
                
                MySqlConnection conn = new MySqlConnection();
                conn.ConnectionString = ConfigurationManager.ConnectionStrings["BooksConn"].ConnectionString.ToString();

                try
                {
                    conn.Open();
                    MySqlCommand comm = new MySqlCommand();
                    comm.CommandText = "INSERT into users (name, surname, email, username, password, type, numComments) VALUES(@name, @surname, @email, @username, @password, @type, @numComments)";
                    comm.Connection = conn;
                    comm.Parameters.AddWithValue("@name", user.name);
                    comm.Parameters.AddWithValue("@surname", user.surname);
                    comm.Parameters.AddWithValue("@email", user.email);
                    comm.Parameters.AddWithValue("@username", user.username);
                    comm.Parameters.AddWithValue("@password", user.password);
                    comm.Parameters.AddWithValue("@type", "user");
                    comm.Parameters.AddWithValue("@numComments", 0);

                    comm.ExecuteNonQuery();
                }
                catch (Exception err)
                {
                    Console.WriteLine(err.Message);
                }
                finally
                {
                    conn.Close();
                }
                Response.Redirect("login.aspx");
            }
           
            

        }
    }
}