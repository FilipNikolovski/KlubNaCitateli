using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MySql.Data.MySqlClient;

namespace KlubNaCitateli.Sites
{
    public partial class login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        public void logIn_click(object sender, EventArgs e)
        {
            MySqlConnection conn = new MySqlConnection();
            conn.ConnectionString = @"Data source=localhost;Database=books;User=root;Password=''";


            MySqlCommand command = new MySqlCommand();
            command.CommandText = "SELECT password from users where username=@username OR Email=@email";
            command.Parameters.AddWithValue("@username", username.Text.ToString());
            command.Parameters.AddWithValue("@email", username.Text.ToString());
            command.Connection = conn;
            conn.Open();
            MySqlDataReader reader = command.ExecuteReader();
            if (reader.Read())
            {
                if (reader.GetValue(0).ToString() == password.Text)
                {
                    Response.Redirect("index.aspx");
                }
                else
                {
                    loginInfo.Text = "Password is incorrect!";
                }
            }
            else
            {
                loginInfo.Text = "Username or email doesn't exist!";
            }
            conn.Close();



        }
    }
}