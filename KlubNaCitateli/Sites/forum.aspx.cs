using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MySql.Data.MySqlClient;
using System.Configuration;
using System.Text;
using System.Diagnostics;

namespace KlubNaCitateli.Sites
{
    public partial class forum : System.Web.UI.Page
    {
        Dictionary<int, string> topicIds = new Dictionary<int, string>();
        Dictionary<int, List<Thread>> topicsInfo = new Dictionary<int, List<Thread>>();
        protected void Page_Load(object sender, EventArgs e)
        {
            LoadForum();
        }

        protected void LoadForum()
        {

            using (MySqlConnection connection = new MySqlConnection())
            {

                connection.ConnectionString = ConfigurationManager.ConnectionStrings["BooksConn"].ConnectionString.ToString();

                try
                {
                    connection.Open();
                    MySqlCommand command = new MySqlCommand();
                    command.Connection = connection;
                    command.CommandText = "SELECT * from TopicTypes";
                    MySqlDataReader reader = command.ExecuteReader();
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            topicIds.Add(Convert.ToInt32(reader["IDType"]), reader["Name"].ToString());
                        }
                    }
                    reader.Close();
                    command.CommandText = "Select forumtopics.IDTopic, forumtopics.TopicName, count(distinct DiscussionThreads.IDThread) as Threads, count(distinct Posts.IDPost) as Posts from forumtopics left outer join DiscussionThreads on forumtopics.idtopic=DiscussionThreads.idtopic left outer join Posts on Discussionthreads.IdThread=posts.idthread where forumtopics.idtype=?IDType group by forumtopics.Idtopic";

              
                    foreach (KeyValuePair<int, string> current in topicIds)
                    {
                         List<Thread> list = new List<Thread>();
                        command.Parameters.Clear();
                        command.Parameters.AddWithValue("?IDType", current.Key);
                        reader = command.ExecuteReader();
                        if (reader.HasRows)
                        {
                           
                            while (reader.Read())
                            {
                                Thread thread = new Thread(Convert.ToInt32(reader["IDTopic"]), reader["TopicName"].ToString(), Convert.ToInt32(reader["Threads"]), Convert.ToInt32(reader["Posts"]));
                                list.Add(thread);
                            }
                        }
                        reader.Close();
                        topicsInfo.Add(current.Key, list);
                       
                    }

                    foreach (KeyValuePair<int, List<Thread>> current in topicsInfo)
                    {

                        command.CommandText = "select discussionthreads.idthread, discussionthreads.ThreadName, numPosts  from discussionthreads, posts, (select count(posts.idpost) as numPosts, idthread from posts group by idthread) as a where discussionthreads.idthread=a.idthread and posts.idthread=discussionthreads.idthread and idtopic=?IDTopic group by discussionthreads.idthread order by numPosts desc limit 1";
                        foreach (Thread thread in current.Value)
                        {
                            command.Parameters.Clear();
                            command.Parameters.AddWithValue("?IDTopic", thread.IdForumTopic);
                            reader = command.ExecuteReader();
                            if (reader.HasRows)
                            {
                                if (reader.Read())
                                {
                                    thread.IdThread = Convert.ToInt32(reader["idthread"].ToString());
                                    thread.ThreadName = reader["ThreadName"].ToString();
                                    thread.ThreadNumPosts = Convert.ToInt32(reader["numPosts"].ToString());
                                }
                            }
                            reader.Close();
                        }

                    }


                    StringBuilder innerHTML = new StringBuilder();
                    foreach(KeyValuePair<int, List<Thread>> current in topicsInfo)
                    {
                        innerHTML.Append("<div class='maintopics'> <div class='naslov'>" + topicIds[current.Key] + "</div>");
                        innerHTML.Append("<div class='topic'>");
                        foreach (Thread thread in current.Value)
                        {
                                innerHTML.Append(" <div class='border'><div class='thread'> <div class='topicLink'>"+thread.TopicName+"</div>");
                                innerHTML.Append("<div class='class1'><label>Threads:</label> <label>" + thread.NumThreads + "</label>  <label>Posts:</label> <label>" + thread.NumPosts + "</label></div>");
                                innerHTML.Append("<div style='display:none;' class='id'>"+thread.IdForumTopic+"</div></div>");
                                innerHTML.Append("<div class='mostCommCat'> <div class='posts'>"+thread.ThreadName+"</div><div class='class2'><label>Posts:</label> <label>"+thread.ThreadNumPosts+"</label></div><div style='display:none;' class='id'>"+thread.IdThread+"</div></div>");
                                innerHTML.Append("<div class='nodiv'></div></div>");
                        }

                        innerHTML.Append("</div></div>"); 

                    }
                    topicsdiv.InnerHtml = innerHTML.ToString();


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