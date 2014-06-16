using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using KlubNaCitateli.Classes;
using MySql.Data.MySqlClient;
using System.Configuration;
using System.IO;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace KlubNaCitateli.Sites
{
    public partial class signup : System.Web.UI.Page
    {
        User user;
        protected void Page_Load(object sender, EventArgs e)
        {
            this.Page.Form.Enctype = "multipart/form-data";
            if (!this.IsPostBack)
            {
                using (MySqlConnection connection = new MySqlConnection())
                {
                    connection.ConnectionString = ConfigurationManager.ConnectionStrings["BooksConn"].ConnectionString;

                    try
                    {
                        connection.Open();

                        string query = "SELECT * FROM Categories";

                        MySqlCommand command = new MySqlCommand(query, connection);
                        MySqlDataReader reader = command.ExecuteReader();

                        while (reader.Read())
                        {
                            categoriesId.Value += reader["IDCategory"].ToString() + ",";
                            categories.Value += reader["Name"].ToString() + ",";
                        }

                        reader.Close();


                    }
                    finally
                    {
                        connection.Close();
                    }

                }
            }
        }

        public void addUser()
        {
            user = new User(name.Text, surname.Text, email.Text, username.Text, password.Text, TextBox2.Text);
            bool checkUsername = true;
            bool checkEmail = true;
            user.CheckIfUserExists(checkEmail, checkUsername);

            if (!checkEmail && !checkUsername)
            {
                finishLabel.Text = "Email and username are already in use.";
            }
            else if (!checkEmail)
            {
                finishLabel.Text = "Email is already in use.";
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
                    comm.CommandText = "INSERT into users (name, banned, surname, email, username, password, type, numComments) VALUES(?name, ?banned, ?surname, ?email, ?username, ?password, ?type, ?numComments)";
                    comm.Connection = conn;
                    comm.Parameters.AddWithValue("?name", user.name);
                    comm.Parameters.AddWithValue("?surname", user.surname);
                    comm.Parameters.AddWithValue("?email", user.email);
                    comm.Parameters.AddWithValue("?username", user.username);
                    comm.Parameters.AddWithValue("?password", user.password);
                    comm.Parameters.AddWithValue("?type", "user");
                    comm.Parameters.AddWithValue("?numComments", 0);
                    comm.Parameters.AddWithValue("?banned", 0);

                    comm.ExecuteNonQuery();
                    string iduser = "";
                    comm.CommandText = "select iduser from users where username=?username";
                    MySqlDataReader reader = comm.ExecuteReader();
                    if (reader.Read())
                    {
                        iduser = reader.GetValue(0).ToString();

                    }
                    reader.Close();



                    string categories = demo.Text;
                    string[] cat = categories.Split(new char[] { ',' });

                    comm.CommandText = "Insert into usercategories (IDUser, IDCategory) values (?IDUser, ?IDCategory)";
                    foreach (string category in cat)
                    {
                        if (category.Trim() != "")
                        {
                            comm.Parameters.Clear();
                            comm.Parameters.AddWithValue("?IDUser", iduser);
                            comm.Parameters.AddWithValue("?IDCategory", category);
                            comm.ExecuteNonQuery();

                        }
                    }

                }
                catch (Exception err)
                {
                    Console.WriteLine(err.Message);
                }
                finally
                {
                    conn.Close();
                }

            }
            Response.Redirect("login.aspx");
        }

        public void finishButton_click(object sender, EventArgs e)
        {
            if (profileImage.HasFile)
            {
                string ext = Path.GetExtension(this.profileImage.FileName);
                if (ext == ".jpg" || ext == ".png")
                {
                    Bitmap originalBMP = new Bitmap(profileImage.FileContent);

                    // Calculate the new image dimensions
                    int origWidth = originalBMP.Width;
                    int origHeight = originalBMP.Height;
                    int sngRatio = origWidth / origHeight;
                    int newWidth = 200;
                    int newHeight = newWidth / sngRatio;

                    // Create a new bitmap which will hold the previous resized bitmap
                    Bitmap newBMP = new Bitmap(originalBMP, newWidth, newHeight);

                    // Create a graphic based on the new bitmap
                    Graphics oGraphics = Graphics.FromImage(newBMP);
                    // Set the properties for the new graphic file
                    oGraphics.SmoothingMode = SmoothingMode.AntiAlias;
                    oGraphics.InterpolationMode = InterpolationMode.HighQualityBicubic;

                    // Draw the new graphic based on the resized bitmap
                    oGraphics.DrawImage(originalBMP, 0, 0, newWidth, newHeight);
                    // Save the new graphic file to the server
                    newBMP.Save(Server.MapPath("~/Images/ProfilePicture/") + (user.username + ext));

                    // Once finished with the bitmap objects, we deallocate them.
                    originalBMP.Dispose();
                    newBMP.Dispose();
                    oGraphics.Dispose();
                    addUser();
                }
                else
                {
                    string script = "<script>alert('The image file is not valid. Valid extensions are .jpg and .png! Try again.');</script>";
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "openDialog", script);
                }
            }
            else
            {
                addUser();
            }

            
        }


    }



}
