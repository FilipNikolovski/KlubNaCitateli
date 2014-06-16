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
                    command.CommandText = "select name, surname, username, email, about from users where iduser=?iduser";
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
                        if (File.Exists(Server.MapPath("~/Images/ProfilePicture/" + reader["username"].ToString() + ".jpg")))
                        {
                            Image1.ImageUrl = "~/Images/ProfilePicture/" + reader["username"].ToString() + ".jpg/";
                        }
                        else if (File.Exists(Server.MapPath("~/Images/ProfilePicture/" + reader["username"].ToString() + ".png")))
                        {
                            Image1.ImageUrl = "~/Images/ProfilePicture/" + reader["username"].ToString() + ".png/";
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

                    command.CommandText = "select thumbnail, b.idbook from userbooks as ub, books as b where iduser=?iduser3 and b.idbook=ub.idbook";
                    command.Parameters.AddWithValue("?iduser3", id);
                    command.Connection = connection;
                    reader = command.ExecuteReader();
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            carousel.InnerHtml += "<a href='book.aspx?id="+ reader["idbook"].ToString() + "'><img src=" + reader["thumbnail"] + "/></a>";
                        }
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
    }
}