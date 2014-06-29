using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MySql.Data.MySqlClient;
using System.Configuration;
using System.Text;
using System.IO;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace KlubNaCitateli.Sites
{
    public partial class profile : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            using (MySqlConnection connection = new MySqlConnection())
            {
                connection.ConnectionString = ConfigurationManager.ConnectionStrings["BooksConn"].ConnectionString.ToString();

                try
                {
                    connection.Open();
                    int id = Convert.ToInt32(Request.QueryString["id"]);

                    if (id == 0)
                    {
                        Response.Redirect("~/Sites/error.aspx");
                    }

                    if (Session["id"] != null)
                    {
                        int sessionId = Convert.ToInt32(Session["id"].ToString());

                        if (sessionId == id)
                        {
                            changePicBtn.Visible = true;
                            changeUsrBtn.Visible = true;
                            changeEmailBtn.Visible = true;
                            changeAboutBtn.Visible = true;
                        }
                    }

                    MySqlCommand command = new MySqlCommand();
                    command.CommandText = "select iduser, name, surname, username, email, about from users where iduser=?iduser";
                    command.Parameters.AddWithValue("?iduser", id);
                    command.Connection = connection;
                    MySqlDataReader reader = command.ExecuteReader();
                    if (reader.HasRows)
                    {
                        reader.Read();
                        nameLbl.Text = reader["name"].ToString().First().ToString().ToUpper() + String.Join("", reader["name"].ToString().Skip(1)) +
                            " " + reader["surname"].ToString().First().ToString().ToUpper() + String.Join("", reader["surname"].ToString().Skip(1));
                        lblUsername.Text = reader["username"].ToString();
                        lblEmail.Text = reader["email"].ToString();
                        lblAbout.Text = reader["about"].ToString();
                        if (File.Exists(Server.MapPath("~/Images/ProfilePicture/" + reader["iduser"].ToString() + ".jpg")))
                        {
                            profileImg.Src = "~/Images/ProfilePicture/" + reader["iduser"].ToString() + ".jpg/";
                        }
                        else if (File.Exists(Server.MapPath("~/Images/ProfilePicture/" + reader["iduser"].ToString() + ".png")))
                        {
                            profileImg.Src = "~/Images/ProfilePicture/" + reader["iduser"].ToString() + ".png/";
                        }
                        else if (File.Exists(Server.MapPath("~/Images/ProfilePicture/" + reader["iduser"].ToString() + ".JPG")))
                        {
                            profileImg.Src = "~/Images/ProfilePicture/" + reader["iduser"].ToString() + ".JPG/";
                        }
                        else if (File.Exists(Server.MapPath("~/Images/ProfilePicture/" + reader["iduser"].ToString() + ".PNG")))
                        {
                            profileImg.Src = "~/Images/ProfilePicture/" + reader["iduser"].ToString() + ".PNG/";
                        }
                    }
                    else
                    {
                        Response.Redirect("~/Sites/error.aspx");
                    }
                    reader.Close();


                    command.CommandText = "select name from usercategories as uc, categories as c where iduser=?iduser2 and uc.idcategory=c.idcategory";
                    command.Parameters.AddWithValue("?iduser2", id);
                    command.Connection = connection;
                    reader = command.ExecuteReader();
                    if (reader.HasRows)
                    {
                        StringBuilder sb = new StringBuilder();
                        while (reader.Read())
                        {
                            sb.Append(reader["name"].ToString());
                            sb.Append("<br>");
                        }

                        lblCategories.Text = sb.ToString();
                    }
                    else
                    {
                        lblCategories.Text = "You have no categories!!";
                    }
                    reader.Close();

                    command.CommandText = "select distinct thumbnail, name, ub.idbook from userbooks as ub, books as b where iduser=?iduser3 and b.idbook=ub.idbook";
                    command.Parameters.AddWithValue("?iduser3", id);
                    command.Connection = connection;
                    reader = command.ExecuteReader();
                    if (reader.HasRows)
                    {
                        StringBuilder sb2 = new StringBuilder();
                        sb2.Append("<div id='carouselh'>");
                        while (reader.Read())
                        {
                            sb2.Append("<div><img src='" + reader["thumbnail"].ToString() + "' /><span class='thumbnail-text'>"+ reader["name"].ToString() +"</span></div>");                            
                            
                        }
                        sb2.Append("</div>");
                        hWrapper.InnerHtml = sb2.ToString();
                    }
                    reader.Close();
                }
                catch (Exception ex)
                {
                    lblError.Text = ex.Message;
                    lblError.Visible = true;
                }
                finally
                {
                    connection.Close();
                }
            }
        }

        public void ChangeAbout(Object sender, EventArgs e)
        {
            tbAbout.Visible = true;
            tbAbout.Text = lblAbout.Text;
            lblAbout.Visible = false;
            confirmChangeAboutBtn.Visible = true;
            changeAboutBtn.Visible = false;
        }

<<<<<<< HEAD
=======
        public void ChangeUsername(Object sender, EventArgs e)
        {
            tbUsername.Visible = true;
            tbUsername.Text = lblUsername.Text;
            lblUsername.Visible = false;
            confirmChangeUsrBtn.Visible = true;
            changeUsrBtn.Visible = false;
        }

        public void ChangeEmail(Object sender, EventArgs e)
        {
            tbEmail.Visible = true;
            tbEmail.Text = lblEmail.Text;
            lblEmail.Visible = false;
            confirmChangeEmailBtn.Visible = true;
            changeEmailBtn.Visible = false;
        }

        public void ConfirmChangeUsername(Object sender, EventArgs e)
        {
            using(MySqlConnection connection = new MySqlConnection())
            {
                connection.ConnectionString = ConfigurationManager.ConnectionStrings["BooksConn"].ConnectionString.ToString();

                try
                {
                    connection.Open();

                    int id = Convert.ToInt32(Request.QueryString["id"]);
                    
                    MySqlCommand command = new MySqlCommand();
                    command.CommandText = "update users set username=?username where iduser=?iduser";
                    command.Parameters.AddWithValue("?iduser", id);
                    command.Parameters.AddWithValue("?username", tbUsername.Text);
                    command.Connection = connection;
                    MySqlDataReader reader = command.ExecuteReader();
                    command.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    lblError.Text = ex.Message;
                    lblError.Visible = true;
                }
                finally
                {
                    connection.Close();
                }

                confirmChangeUsrBtn.Visible = false;
                lblUsername.Visible = true;
                lblUsername.Text = tbUsername.Text;
                tbUsername.Visible = false;
            }
        }

        public void ConfirmChangeEmail(Object sender, EventArgs e)
        {
            using (MySqlConnection connection = new MySqlConnection())
            {
                connection.ConnectionString = ConfigurationManager.ConnectionStrings["BooksConn"].ConnectionString.ToString();

                try
                {
                    connection.Open();

                    int id = Convert.ToInt32(Request.QueryString["id"]);

                    MySqlCommand command = new MySqlCommand();
                    command.CommandText = "update users set email=?email where iduser=?iduser";
                    command.Parameters.AddWithValue("?iduser", id);
                    command.Parameters.AddWithValue("?email", tbEmail.Text);
                    command.Connection = connection;
                    MySqlDataReader reader = command.ExecuteReader();
                    command.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    lblError.Text = ex.Message;
                    lblError.Visible = true;
                }
                finally
                {
                    connection.Close();
                }
            }

            confirmChangeEmailBtn.Visible = false;
            lblEmail.Visible = true;
            lblEmail.Text = tbEmail.Text;
            tbEmail.Visible = false;
        }

        public void ChangePicture(Object sender, EventArgs e)
        {
            if (changePicBtn.Text == "Change Picture")
            {
                profilePicture.Visible = true;
                changePicBtn.Text = "Confirm Change";
                return;
            }
            if (profilePicture.HasFile)
            {
                if (File.Exists(Server.MapPath("~/Images/ProfilePicture/" + Session["Id"] + ".jpg")))
                {
                    string[] img = Directory.GetFiles(Server.MapPath("~/Images/ProfilePicture/"));
                    File.Delete(img[0]);
                }
                else if (File.Exists(Server.MapPath("~/Images/ProfilePicture/" + Session["Id"] + ".JPG")))
                {
                    string[] img = Directory.GetFiles(Server.MapPath("~/Images/ProfilePicture/"));
                    File.Delete(img[0]);
                }
                else if (File.Exists(Server.MapPath("~/Images/ProfilePicture/" + Session["Id"] + ".png")))
                {
                    string[] img = Directory.GetFiles(Server.MapPath("~/Images/ProfilePicture/"));
                    File.Delete(img[0]);
                }
                else if (File.Exists(Server.MapPath("~/Images/ProfilePicture/" + Session["Id"] + ".PNG")))
                {
                    string[] img = Directory.GetFiles(Server.MapPath("~/Images/ProfilePicture/"));
                    File.Delete(img[0]);
                }

                string ext = Path.GetExtension(this.profilePicture.FileName);
                if (ext == ".jpg" || ext == ".png" || ext == ".JPG" || ext == ".PNG")
                {
                    Bitmap originalBMP = new Bitmap(profilePicture.FileContent);

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
                    newBMP.Save(Server.MapPath("~/Images/ProfilePicture/") + (Session["Id"] + ext));

                    // Once finished with the bitmap objects, we deallocate them.
                    originalBMP.Dispose();
                    newBMP.Dispose();
                    oGraphics.Dispose();
                    profilePicture.Visible = false;
                    changePicBtn.Text = "Change Picture";

                    Response.Redirect("/Sites/profile.aspx?id=" + Session["Id"].ToString());
                }
                else
                {
                    string script = "<script>$(document).ready(function(){alert('The image file is not valid. Valid extensions are .jpg and .png! Try again.');});</script>";
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "openDialog", script);
                }
            }
            else
            {
                return;
            }
        }
>>>>>>> 26982380730902fb765316245d85b5ab719ea8d3
    }
}