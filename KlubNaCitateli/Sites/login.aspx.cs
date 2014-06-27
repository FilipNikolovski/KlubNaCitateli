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
    public partial class login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["Id"] != null)
                Response.Redirect("~/Sites/index.aspx");
        }
        public void logIn_click(object sender, EventArgs e)
        {
            using (MySqlConnection conn = new MySqlConnection())
            {

                conn.ConnectionString = ConfigurationManager.ConnectionStrings["BooksConn"].ConnectionString.ToString();


                MySqlCommand command = new MySqlCommand();
                command.CommandText = "SELECT password from users where username=?username OR Email=?email";
                command.Parameters.AddWithValue("?username", username.Text.ToString());
                command.Parameters.AddWithValue("?email", username.Text.ToString());
                command.Connection = conn;
                try
                {
                    conn.Open();
                    MySqlDataReader reader = command.ExecuteReader();
                    if (reader.Read())
                    {
                        if (reader.GetValue(0).ToString() == password.Text)
                        {
                            reader.Close();
                            MySqlCommand command1 = new MySqlCommand();
                            command1.CommandText = "SELECT name from users where username=?username OR Email=?email";
                            command1.Parameters.AddWithValue("?username", username.Text.ToString());
                            command1.Parameters.AddWithValue("?email", username.Text.ToString());
                            command1.Connection = conn;
                            MySqlDataReader read = command1.ExecuteReader();
                            if (read.Read())
                            {
                                Session["Name"] = read.GetValue(0).ToString();
                            }
                            read.Close();
                            MySqlCommand command2 = new MySqlCommand();
                            command2.CommandText = "SELECT surname from users where username=?username OR Email=?email";
                            command2.Parameters.AddWithValue("?username", username.Text.ToString());
                            command2.Parameters.AddWithValue("?email", username.Text.ToString());
                            command2.Connection = conn;
                            MySqlDataReader read1 = command2.ExecuteReader();
                            if (read1.Read())
                            {
                                Session["Surname"] = read1.GetValue(0).ToString();
                            }

                            read1.Close();

                            command2.CommandText = "SELECT type from users where username=?username OR Email=?email";
                            read1 = command2.ExecuteReader();
                            if (read1.Read())
                            {
                                Session["Type"] = read1.GetValue(0).ToString();
                            }
                            read1.Close();


                            Session["UsernameOrEmail"] = username.ToString();
                            command2.CommandText = "SELECT iduser from users where username=?username OR Email=?email";
                            read1 = command2.ExecuteReader();
                            if (read1.Read())
                            {
                                Session["Id"] = read1.GetValue(0).ToString();
                            }
                            read1.Close();

                            command2.CommandText = "SELECT banned from users where username=?username OR Email=?email";
                            read1 = command2.ExecuteReader();
                            if (read1.Read())
                            {
                                Session["Banned"] = read1.GetValue(0).ToString();
                            }
                            read1.Close();

                            if (Session["Type"].ToString().Equals("administrator"))
                            {
                                Response.Redirect("adminpanel.aspx");
                            }
                            else
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
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
                finally
                {
                    conn.Close();
                }

            }

        }
    }
}