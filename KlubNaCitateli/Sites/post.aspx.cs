using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MySql.Data.MySqlClient;
using System.Text;
using System.Configuration;
using System.Diagnostics;
using System.IO;

namespace KlubNaCitateli.Sites
{
    public partial class post : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            int idThread = Convert.ToInt32(Request.QueryString["threadid"]);

           showPosts(idThread);


        }
        protected void showPosts(int idThread)
        {
            using (MySqlConnection connection = new MySqlConnection())
            {
                StringBuilder innerHTML = new StringBuilder();

                connection.ConnectionString = ConfigurationManager.ConnectionStrings["BooksConn"].ConnectionString.ToString();
                try
                {
                    connection.Open();
                    MySqlCommand command = new MySqlCommand();
                    command.Connection = connection;
                    command.CommandText = "select Posts.*, Users.username, Users.Banned,(select count(idpost) from Posts where posts.iduser=users.iduser group by Posts.iduser) as numberPosts from Posts, Users where Users.iduser=Posts.iduser and idthread=?idthread";
                    command.Parameters.AddWithValue("?idthread", idThread);
                    MySqlDataReader reader = command.ExecuteReader();
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            innerHTML.Append("<div class='comm'><div class='idcomment' style='display:none;'>" + reader["idpost"].ToString() + "</div><div class='posthead'>  <span class='postdateold'><span class='date'>");
                            innerHTML.Append(reader["DatePosted"].ToString() + "</span></span>");
                            if (Session["Id"] != null && Session["Type"].ToString().Equals("administrator"))
                            {
                                innerHTML.Append("<img src='../Images/deletePost.png' alt='' class='deletePost' />");
                               
                            }
                            innerHTML.Append("<div class='noDiv'></div></div>");
                            innerHTML.Append("<div class='postdetails'><div class='userinfo'>");
                            innerHTML.Append("<div class='iduser' style='display:none;'>" + reader["iduser"].ToString() + "</div>");
                            if (Session["Id"] != null)
                            {
                                if (Session["Type"].ToString().Equals("administrator"))
                                {
                                    bool x = (bool)reader["Banned"];
                                    if (!x)
                                    {
                                        innerHTML.Append("<div class='banusr'><div class='banUser' >[ Ban user ]</div></div>");
                                    }
                                    else
                                    {
                                        innerHTML.Append("<div class='banusr'><div class='unBanUser' >[ Unban user ]</div></div>");
                                    }

                                }
                            }

                            innerHTML.Append("<div class='username'><div class='usernameLink'><strong>" + reader["username"].ToString() + "</strong></div></div>");
                            innerHTML.Append("<div class='picture'><img class='userpicture' alt=''");
                            if (File.Exists(Server.MapPath("~/Images/ProfilePicture/" + reader["iduser"].ToString() + ".jpg")))
                            {
                                innerHTML.Append("src='../Images/ProfilePicture/"+reader["iduser"].ToString()+".jpg' /></div>");
                            }
                            else if (File.Exists(Server.MapPath("~/Images/ProfilePicture/" + reader["iduser"].ToString() + ".png")))
                            {
                                innerHTML.Append("src='../Images/ProfilePicture/" + reader["iduser"].ToString() + ".png' /></div>");
                            }
                            else if (File.Exists(Server.MapPath("~/Images/ProfilePicture/" + reader["iduser"].ToString() + ".JPG")))
                            {
                                innerHTML.Append("src='../Images/ProfilePicture/" + reader["iduser"].ToString() + ".JPG' /></div>");
                            }
                            else if (File.Exists(Server.MapPath("~/Images/ProfilePicture/" + reader["iduser"].ToString() + ".PNG")))
                            {
                                innerHTML.Append("src='../Images/ProfilePicture/" + reader["iduser"].ToString() + ".jpg' /></div>");
                            }
                            else
                            {
                                innerHTML.Append("src='../Images/forum-user.png' /></div>");
                            }
                           
                            innerHTML.Append("<div class='posts'><label>Posts: </label><strong>"+reader["numberposts"].ToString()+"</strong></div>");
                            innerHTML.Append("</div><div class='posttext'>");
                            innerHTML.Append("<div class='maintext'>");
                            innerHTML.Append("<div class='comments'>" + reader["PostComment"].ToString() + "</div></div>");
                            if (Session["Id"] != null)
                            {
                                innerHTML.Append("<div class='btnreply'><div class='replyQuote' id='replyWithText'>Reply with quote</div></div>");
                            }
                            innerHTML.Append("</div><div class='noDiv'></div></div></div>");
                         
                        }
                    }
                    reader.Close();

                    commentsarea.InnerHtml = innerHTML.ToString();
                    
                }
                catch (Exception ex)
                {
                    Debug.WriteLine(ex.Message);
                }
                finally
                {
                    connection.Close();
                }
            }



        }
    }
}