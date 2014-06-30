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

namespace KlubNaCitateli.Sites
{
    public partial class post : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            int idThread = Convert.ToInt32(Request.QueryString["threadid"]);

          //  showPosts(idThread);


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
                    command.CommandText = "select Posts.*, Users.username, Users.Banned from Posts, Users where Users.iduser=Posts.iduser and idthread=?idthread";
                    command.Parameters.AddWithValue("?idthread", idThread);
                    MySqlDataReader reader = command.ExecuteReader();
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            innerHTML.Append("<div class='comm'><div class='posthead'>  <span class='postdateold'><span class='date'>");
                            innerHTML.Append(reader["DatePosted"].ToString() + "</span></span>");
                            if (Session["Id"] != null && Session["Type"].ToString().Equals("administrator"))
                            {
                                    innerHTML.Append("<img src='../Images/deletePost.png' alt='' class='deletePost' />");
                               
                            }
                            innerHTML.Append("<div class='noDiv'></div></div>");
                            innerHTML.Append("<div class='postdetails'><div class='userinfo'>");
                            if (Session["Id"] != null && Session["Type"].ToString().Equals("administrator"))
                            {
                                bool x = (bool)reader["Banned"];
                                    if (!x)
                                    {
                                        innerHTML.Append("<div class='banusr'><div class='banUser' >[ Ban user ]</div></div>");
                                    }
                                    else
                                    {
                                        innerHTML.Append("<div class='banusr'><div class='banUser' >[ Unban user ]</div></div>");
                                    }
                                
                            }

                            innerHTML.Append("<div class='username'><div class='usernameLink'><strong>" + reader["username"].ToString() + "</strong></div></div>");
                            innerHTML.Append("<div class='picture'><img class='userpicture' alt='' src='../Images/user-icon.png' /></div>");
                            innerHTML.Append("<div class='posts'><label>Posts:</label><strong>120</strong></div>");
                            innerHTML.Append("</div><div class='posttext'>");
                            innerHTML.Append("<div class='maintext'>");
                            innerHTML.Append("<div class='comments'>"+reader["PostComment"].ToString()+"</div></div>");
                            if (Session["Id"] != null)
                            {
                                innerHTML.Append("</div><div class='btnreply'><asp:LinkButton Text='Reply with quote' runat='server' ID='replyWithText'></asp:LinkButton></div></div>");
                            }
                            innerHTML.Append("</div><div class='noDiv'></div></div>");
                         
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