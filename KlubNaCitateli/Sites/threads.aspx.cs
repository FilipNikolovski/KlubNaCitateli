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
            idtopic.Value = IdTopic.ToString();
            if (IdTopic == 0)
            {
                Response.Redirect("forum.aspx");
            }

            if (Session["Id"] != null)
            {
                iduser.Value = Session["Id"].ToString();
            }
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
                            topic.InnerText = reader["topicname"].ToString();
                        }
                    }
                    reader.Close();
                }
                catch (Exception ex)
                {
                    Debug.WriteLine(ex.Message);
                }
                finally { connection.Close(); }
            }
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
                            if (iduser.Value != "")
                                innerHTML.Append("<div class='cont'><div class='allblack'><div class='naslov'>" + reader["topicname"] + "</div><div runat='server' id='btnAddThread' class='btnAddThread'>+ New Thread</div><div class='nodiv'></div></div>");
                            else
                                innerHTML.Append("<div class='cont'><div class='allblack'><div class='naslov'>" + reader["topicname"] + "</div><div runat='server' id='btnAddThread' class='btnAddThread' style='display:none;'>+ New Thread</div><div class='nodiv'></div></div>");
                        }
                    }
                    else
                    {
                        topics.Visible = false;
                    }
                    reader.Close();
                   
                    command.CommandText = "select discussionthreads.idthread, threadname, count(idpost) as comments, username, users.iduser, DateCreated, discussionthreads.locked  from users left outer join discussionthreads on discussionthreads.iduser=users.iduser left outer join posts on discussionthreads.idthread=posts.idthread where discussionthreads.idtopic=?IDTopic group by discussionthreads.idthread order by idpost desc";
                    reader = command.ExecuteReader();
                    if (reader.HasRows)
                    {
                        int brojPostovi = 0;
                        int brojStrani = 1;
                        while (reader.Read())
                        {
                            brojPostovi++;
                            if (brojPostovi == 11)
                            {
                                brojStrani++;
                                innerHTML.Append("</div>");
                                brojPostovi = 1;
                            }
                            if (brojPostovi == 1)
                            {
                                innerHTML.Append("<div id='demo" + brojStrani + "' class='demos'>");
                            }

                            innerHTML.Append("<div class='topic'> <div class='thread'> <div class='threadname'>" + reader["threadname"] + "</div><div class='numcomments'><label>Comments:</label> <label>" + reader["comments"] + "</label></div>");
                            innerHTML.Append("<div style='display:none;' class='idthread'>" + reader["idthread"] + "</div></div>");
                            if (Session["Type"] != null)
                            {
                                if (Session["Type"].ToString().Equals("administrator"))
                                {
                                    innerHTML.Append("<div class='delete'></div>");
                                    bool val = Convert.ToBoolean(reader["locked"]);
                                    if (val)
                                    {
                                        innerHTML.Append("<div class='unlock'></div>");
                                    }
                                    else
                                    {
                                        innerHTML.Append("<div class='lock'></div>");
                                    }

                                }
                            }
                            innerHTML.Append("<div class='mostCommCat'><label>Created by:</label> <label class='userD'>" + reader["username"] + "</label> <div class='user'><label>Date created:</label> <label>");
                            if (reader["datecreated"].ToString().Equals(DateTime.Today.ToString("dd-MMMM-yy")))
                            {
                                innerHTML.Append("Today");
                            }
                            else if (reader["datecreated"].ToString().Equals(DateTime.Today.AddDays(-1).ToString("dd-MMMM-yy")))
                            {
                                innerHTML.Append("Yesterday");
                            }
                            else if (reader["datecreated"].ToString().Equals(DateTime.Today.AddDays(-2).ToString("dd-MMMM-yy")))
                            {
                                innerHTML.Append("Two days ago");
                            }
                            else
                            {
                                innerHTML.Append(reader["datecreated"].ToString());
                            }

                            innerHTML.Append("</label></div><div class='iduser' style='display:none;'>" + reader["iduser"] + "</div> <label></label></div> ");
                            innerHTML.Append("<div class='nodiv'></div></div>");
                        }
                        if (brojPostovi > 1 && brojPostovi < 10)
                        {
                            innerHTML.Append("</div>");

                        }
                        numPages.Value = brojStrani.ToString();

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



    }
}