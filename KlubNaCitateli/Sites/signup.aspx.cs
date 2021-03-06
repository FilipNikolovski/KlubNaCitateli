﻿using System;
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
using System.Diagnostics;

namespace KlubNaCitateli.Sites
{
    public partial class signup : System.Web.UI.Page
    {
        User user;
        int iduser = -1;
        string profile = "";
        protected void Page_Load(object sender, EventArgs e)
        {

            if (Session["Id"] != null)
            {
                Response.Redirect("~/Sites/index.aspx");
            }
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

        public void addUser(string profile)
        {
            user = new User(name.Text, surname.Text, email.Text, username.Text, password.Text, TextBox2.Text);
            bool checkUsername = true;
            bool checkEmail = true;
            user.CheckIfUserExists(out checkEmail, out checkUsername);

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

                using (MySqlConnection conn = new MySqlConnection())
                {
                    conn.ConnectionString = ConfigurationManager.ConnectionStrings["BooksConn"].ConnectionString.ToString();

                    try
                    {
                        conn.Open();
                        MySqlCommand comm = new MySqlCommand();
                        comm.CommandText = "INSERT into users (name, banned, surname, email, username, password, about, type, numComments, profilepicture) VALUES(?name, ?banned, ?surname, ?email, ?username, ?password, ?about, ?type, ?numComments, ?profile)";
                        comm.Connection = conn;
                        comm.Parameters.AddWithValue("?name", user.name);
                        comm.Parameters.AddWithValue("?surname", user.surname);
                        comm.Parameters.AddWithValue("?email", user.email);
                        comm.Parameters.AddWithValue("?username", user.username);
                        comm.Parameters.AddWithValue("?password", user.password);
                        comm.Parameters.AddWithValue("?type", "user");
                        comm.Parameters.AddWithValue("?numComments", 0);
                        comm.Parameters.AddWithValue("?banned", 0);
                        comm.Parameters.AddWithValue("?about", user.aboutUser);
                        comm.Parameters.AddWithValue("?profile", profile);

                        comm.ExecuteNonQuery();


                        comm.CommandText = "select iduser from users where username=?username";
                        comm.Parameters.Clear();
                        comm.Parameters.AddWithValue("?username", user.username);
                        MySqlDataReader reader = comm.ExecuteReader();
                        if (reader.Read())
                        {
                            iduser = Convert.ToInt32(reader["iduser"].ToString());

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
                        Debug.WriteLine(err.Message);
                    }
                    finally
                    {
                        conn.Close();
                    }

                }
                Response.Redirect("login.aspx");
            }

        }

        public void finishButton_click(object sender, EventArgs e)
        {
            if (profileImage.HasFile)
            {
                string ext = Path.GetExtension(this.profileImage.FileName);
                if (ext == ".jpg" || ext == ".png" || ext == ".JPG" || ext == ".PNG")
                {

                    Bitmap originalBMP = new Bitmap(profileImage.FileContent);

                    // Calculate the new image dimensions
                    float origWidth = originalBMP.Width;
                    float origHeight = originalBMP.Height;
                    float sngRatio = origWidth / origHeight;
                    float newWidth = 200;
                    float newHeight = newWidth / sngRatio;

                    // Create a new bitmap which will hold the previous resized bitmap
                    Bitmap newBMP = new Bitmap(originalBMP, (int)newWidth, (int)newHeight);

                    // Create a graphic based on the new bitmap
                    Graphics oGraphics = Graphics.FromImage(newBMP);
                    // Set the properties for the new graphic file
                    oGraphics.SmoothingMode = SmoothingMode.AntiAlias;
                    oGraphics.InterpolationMode = InterpolationMode.HighQualityBicubic;

                    // Draw the new graphic based on the resized bitmap
                    oGraphics.DrawImage(originalBMP, 0, 0, newWidth, newHeight);
                    // Save the new graphic file to the server
                    profile = username.Text + ext;
                    newBMP.Save(Server.MapPath("~/Images/ProfilePicture/") + (username.Text + ext));

                    // Once finished with the bitmap objects, we deallocate them.
                    originalBMP.Dispose();
                    newBMP.Dispose();
                    oGraphics.Dispose();


                    addUser(profile);

                }
                else
                {
                    string script = "<script>$(document).ready(function(){alert('The image file is not valid. Valid extensions are .jpg and .png! Try again.');});</script>";
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "openDialog", script);
                }
            }
            else
            {
                addUser("");
            }


        }

    }

}
