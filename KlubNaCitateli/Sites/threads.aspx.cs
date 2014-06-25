using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MySql.Data.MySqlClient;
using System.Diagnostics;
using System.Configuration;
using System.Text;

namespace KlubNaCitateli.Sites
{
    public partial class threads : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            int IdTopic = Convert.ToInt32(Request.QueryString["topicid"]);
            LoadThreads(IdTopic);
        }

        protected void LoadThreads(int IdTopic)
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
                    command.CommandText = "select topicname from forumtopics where idtopic=?IDTopic";
                    command.Parameters.AddWithValue("?IDTopic", IdTopic);
                    MySqlDataReader reader = command.ExecuteReader();
                    if (reader.HasRows)
                    {
                        if (reader.Read())
                        {
                            innerHTML.Append("<div class='cont'><div class='allblack'><div class='naslov'>" + reader["topicname"] + "</div><button runat='server' id='btnAddThread' class='btnAddThread'>+ Add new thread</button><div class='nodiv'></div></div>");
                        }
                    }
                    else
                    {
                        topics.Visible = false;
                    }
                    reader.Close();

                   
                    command.CommandText = "select discussionthreads.idthread, threadname, count(idpost) as comments, username, users.iduser, DateCreated  from discussionthreads, posts, users where discussionthreads.idthread=posts.idthread and discussionthreads.idtopic=?IDTopic and discussionthreads.iduser=users.iduser group by discussionthreads.idthread";
                    reader = command.ExecuteReader();
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            innerHTML.Append("<div class='topic'> <div class='thread'> <div class='threadname'>" + reader["threadname"] + "</div><div class='numcomments'><label>Comments:</label> <label>" + reader["comments"] + "</label></div>");
                            innerHTML.Append("<div style='display:none;' class='id'>" + reader["idthread"]);
                            innerHTML.Append("</div></div><div class='mostCommCat'><label>Created by:</label> <label class='userD'>" + reader["username"] + "</label> <div class='user'><label>Date created:</label> <label>" + reader["datecreated"] + "</label></div><div class='iduser' style='display:none;'>" + reader["iduser"] + "</div> <label></label> ");
                            innerHTML.Append("</div> <div class='nodiv'></div></div>");
                        }
                        
                    }
                    topics.InnerHtml = innerHTML.ToString();
                    
                    reader.Close();
                    

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


        protected void addNewThread_click(object sender, EventArgs e)
        {
            progressbar.InnerText = "hahahahha";
        }
    }
}