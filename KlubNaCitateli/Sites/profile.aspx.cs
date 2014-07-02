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
using System.Web.UI.HtmlControls;
using System.Diagnostics;

namespace KlubNaCitateli.Sites
{
    public partial class profile : System.Web.UI.Page
    {
        public int UserId { get; set; }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                FillUserProfile();
                FillCategoriesList();
            }
        }

        public void FillUserProfile()
        {
            using (MySqlConnection connection = new MySqlConnection())
            {
                connection.ConnectionString = ConfigurationManager.ConnectionStrings["BooksConn"].ConnectionString.ToString();

                try
                {
                    connection.Open();
                    UserId = Convert.ToInt32(Request.QueryString["id"]);
                        iduser.Value=UserId.ToString();

                    if (UserId == 0)
                    {
                        Response.Redirect("~/Sites/error.aspx");
                    }

                    if (Session["Id"] != null)
                    {
                        int sessionId = Convert.ToInt32(Session["Id"].ToString());
                        idsession.Value = sessionId.ToString();
                        if (sessionId == UserId)
                        {
                            saveCategories.Visible = true;
                            allCategories.Visible = true;
                            changePicBtn.Visible = true;
                        }
                     }

                    MySqlCommand command = new MySqlCommand();
                    command.CommandText = "select iduser, name, surname, username, email, about, profilepicture from users where iduser=?iduser";
                    command.Parameters.AddWithValue("?iduser", UserId);
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
                        if (File.Exists(Server.MapPath("~/Images/ProfilePicture/" + (reader["profilepicture"].ToString()))))
                        {
                            profileImg.Src = "~/Images/ProfilePicture/" + (reader["profilepicture"].ToString());
                        }
                       
                    }
                    else
                    {
                        Response.Redirect("~/Sites/error.aspx");
                    }
                    reader.Close();

                    //Jcarousel User Books
                    command.CommandText = "select distinct thumbnail, name, ub.idbook from userbooks as ub, books as b where iduser=?iduser and b.idbook=ub.idbook";

                    command.Parameters.Clear();
                    command.Parameters.AddWithValue("?iduser", UserId);
                    command.Connection = connection;
                    reader = command.ExecuteReader();
                    if (reader.HasRows)
                    {
                        StringBuilder sb2 = new StringBuilder();
                        sb2.Append("<div class='jcarousel' data-jcarousel='true'><ul>");
                        while (reader.Read())
                        {
                            sb2.Append("<li><a href='book.aspx?id=" + reader["IDBook"] + "'><img src='" + reader["thumbnail"].ToString() + "' /></a></li>");
                        }
                        sb2.Append("</ul></div>");
                        sb2.Append("<a href='#' class='jcarousel-control-prev'>&lsaquo;</a>");
                        sb2.Append("<a href='#' class='jcarousel-control-next'>&rsaquo;</a>");
                        sb2.Append("<p class='jcarousel-pagination'></p>");

                        jcarouselWrapper.InnerHtml = sb2.ToString();
                    }
                    reader.Close();

                    command.CommandText = "Select idthread, threadname from discussionthreads where iduser=?iduser";
                    reader = command.ExecuteReader();
                    StringBuilder innerHTML = new StringBuilder();
                    if (reader.HasRows)
                    {
                        
                        while (reader.Read())
                        {
                            innerHTML.Append("<div class='example-commentheading'><div style='display:none;' class='idthread'>" + reader["idthread"] + "</div><div class='threadname'>" + reader["threadname"].ToString() + "</div></div>");
                        }
                    }
                    reader.Close();
                    threads.InnerHtml = innerHTML.ToString();
                    innerHTML.Clear();
                    command.CommandText = "Select distinct books.idbook, name, thumbnail from books, bookcomments where iduser=?iduser and books.idbook=bookcomments.idbook";
                    reader = command.ExecuteReader();
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            innerHTML.Append("<div class='books'><div style='display:none;' class='idbook'>" + reader["idbook"] + "</div> <img src='" + reader["thumbnail"] + " alt='' class='bookthumbnail'/><span class='bookname'>" + reader["name"] + "</span></div>");
                        }
                    }
                    reader.Close();
                    comments.InnerHtml = innerHTML.ToString();
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

        public void FillCategoriesList()
        {
            using (MySqlConnection conn = new MySqlConnection())
            {
                conn.ConnectionString = ConfigurationManager.ConnectionStrings["BooksConn"].ConnectionString;

                try
                {
                    conn.Open();

                    string sql = "SELECT * FROM Categories WHERE IDCategory NOT IN (SELECT IDCategory FROM UserCategories WHERE IDUser=?IDuser)";

                    MySqlCommand command = new MySqlCommand(sql, conn);
                    command.Parameters.AddWithValue("?IDUser", UserId);
                    MySqlDataReader reader = command.ExecuteReader();

                    StringBuilder innerHtml = new StringBuilder();
                    while (reader.Read())
                    {
                        innerHtml.Append("<li class='ui-state-default' runat='server'>" + reader["Name"] + "</li>");
                    }
                    reader.Close();
                    allCategories.InnerHtml = innerHtml.ToString();

                    sql = "SELECT c.* FROM Categories as c, UserCategories as uc WHERE uc.IDCategory=c.IDCategory AND uc.IDUser=?IDUser";
                    command.CommandText = sql;
                    command.Parameters.Clear();
                    command.Parameters.AddWithValue("?IDUser", UserId);
                    reader = command.ExecuteReader();

                    innerHtml.Clear();

                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            innerHtml.Append("<li class='ui-state-default' runat='server'>" + reader["Name"] + "</li>");
                        }
                        myCategories.InnerHtml = innerHtml.ToString();
                    }
                    reader.Close();
                }
                catch (Exception ex)
                {
                    lblError.Text = ex.Message;
                }
                finally
                {
                    conn.Close();
                }
            }
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
                    newBMP.Save(Server.MapPath("~/Images/ProfilePicture/") + (Session["Username"] + ext));

                    // Once finished with the bitmap objects, we deallocate them.
                    originalBMP.Dispose();
                    newBMP.Dispose();
                    oGraphics.Dispose();
                    profilePicture.Visible = false;
                    changePicBtn.Text = "Change Picture";
                    using (MySqlConnection connection = new MySqlConnection())
                    {
                        connection.ConnectionString = ConfigurationManager.ConnectionStrings["BooksConn"].ConnectionString;
                        try
                        {
                            connection.Open();
                            MySqlCommand command = new MySqlCommand();
                            command.CommandText = "update users set profilepicture=?profilepicture where iduser=?iduser";
                            command.Connection = connection;
                            command.Parameters.AddWithValue("?profilepicture", Session["Username"] + ext);
                            command.Parameters.AddWithValue("?iduser", iduser);
                            command.ExecuteNonQuery();
                        }
                        catch (Exception ex)
                        { }
                        finally { connection.Close(); }
                    }
                    Response.Redirect("../Sites/profile.aspx?id=" + Session["Id"].ToString());
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

    }
}