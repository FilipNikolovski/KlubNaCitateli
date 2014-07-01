using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Activation;
using System.ServiceModel.Web;
using System.Text;
using MySql.Data.MySqlClient;
using System.Configuration;
using System.Diagnostics;

namespace KlubNaCitateli.Services
{
    [ServiceContract(Namespace = "KlubNaCitateli")]
    [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
    public class ForumService
    {
        [OperationContract]
        public string AddThread(string idTopic, string newThreadText, string idUser)
        {
            int userId = Convert.ToInt32(idUser);
            int topicId = Convert.ToInt32(idTopic);

            using (MySqlConnection connection = new MySqlConnection())
            {
                connection.ConnectionString = ConfigurationManager.ConnectionStrings["BooksConn"].ConnectionString.ToString();

                try
                {
                    connection.Open();
                    MySqlCommand command = new MySqlCommand();
                    command.Connection = connection;
                    command.CommandText = "INSERT INTO DISCUSSIONTHREADS (iduser, idtopic, threadname, datecreated) VALUES(?iduser, ?idtopic, ?threadname, ?datecreated)";
                    command.Parameters.AddWithValue("?iduser", userId);
                    command.Parameters.AddWithValue("?idtopic", topicId);
                    command.Parameters.AddWithValue("?threadname", newThreadText.ToString());
                    command.Parameters.AddWithValue("?datecreated", DateTime.Today.ToString("dd/MMMM/yy"));
                    command.ExecuteNonQuery();
                    return "New thread is created";
                }
                catch (Exception ex)
                {
                    Debug.WriteLine(ex.Message);
                    return "Error";
                }
                finally
                {
                    connection.Close();
                }

            }
          

          
        }
        [OperationContract]
        public string AddTopic(string idType, string newTopicText)
        {
            int typeId = Convert.ToInt32(idType);

            using (MySqlConnection connection = new MySqlConnection())
            {
                connection.ConnectionString = ConfigurationManager.ConnectionStrings["BooksConn"].ConnectionString.ToString();

                try
                {
                    connection.Open();
                    MySqlCommand command = new MySqlCommand();
                    command.Connection = connection;
                    command.CommandText = "INSERT INTO FORUMTOPICS (topicname, idtype) VALUES(?topicname, ?idtype)";
                    command.Parameters.AddWithValue("?topicname", newTopicText.ToString());
                    command.Parameters.AddWithValue("?idtype", typeId);
                    command.ExecuteNonQuery();
                    return "New topic is created";
                }
                catch (Exception ex)
                {
                    Debug.WriteLine(ex.Message);
                    return "Error";
                }
                finally
                {
                    connection.Close();
                }

            }



        }


        [OperationContract]
        public string DeleteTopic(string idTopic)
        {
            int topicId = Convert.ToInt32(idTopic);

            using (MySqlConnection connection = new MySqlConnection())
            {
                connection.ConnectionString = ConfigurationManager.ConnectionStrings["BooksConn"].ConnectionString.ToString();

                try
                {
                    connection.Open();
                    MySqlCommand command = new MySqlCommand();
                    command.Connection = connection;
                    command.CommandText = "DELETE FROM FORUMTOPICS WHERE IDTOPIC=?IDTopic";
                    command.Parameters.AddWithValue("?IDTopic", topicId);
                    command.ExecuteNonQuery();
                    return "The selected topic is deleted.";
                }
                catch (Exception ex)
                {
                    Debug.WriteLine(ex.Message);
                    return "Error";
                }
                finally
                {
                    connection.Close();
                }

            }



        }

        [OperationContract]
        public string DeleteThread(string idThread)
        {
            int threadId = Convert.ToInt32(idThread);

            using (MySqlConnection connection = new MySqlConnection())
            {
                connection.ConnectionString = ConfigurationManager.ConnectionStrings["BooksConn"].ConnectionString.ToString();

                try
                {
                    connection.Open();
                    MySqlCommand command = new MySqlCommand();
                    command.Connection = connection;
                    command.CommandText = "DELETE FROM Discussionthreads WHERE IDThread=?IDThread";
                    command.Parameters.AddWithValue("?IDThread", threadId);
                    command.ExecuteNonQuery();
                    return "The selected thread is deleted.";
                }
                catch (Exception ex)
                {
                    Debug.WriteLine(ex.Message);
                    return "Error";
                }
                finally
                {
                    connection.Close();
                }

            }



        }


        [OperationContract]
        public string BannedUser(bool banned, int idUser)
        {


            using (MySqlConnection connection = new MySqlConnection())
            {
                connection.ConnectionString = ConfigurationManager.ConnectionStrings["BooksConn"].ConnectionString.ToString();

                try
                {

                    connection.Open();
                    MySqlCommand command = new MySqlCommand();
                    command.Connection = connection;
                    command.CommandText = "Update users set banned=?banned where iduser=?iduser";
                    command.Parameters.AddWithValue("?banned", banned);
                    command.Parameters.AddWithValue("?iduser", idUser);
                    command.ExecuteNonQuery();
                    if (banned)
                    {
                        return "User is banned!";
                    }
                    else
                    {
                        return "User is unbanned!";
                    }

                }
                catch (Exception ex)
                {
                    Debug.WriteLine(ex.Message);
                    return "Error";
                }
                finally
                {
                    connection.Close();
                }

            }
        }


        [OperationContract]
        public string DeleteComment(int idpost)
        {
            
                    
            using (MySqlConnection connection = new MySqlConnection())
            {
                connection.ConnectionString = ConfigurationManager.ConnectionStrings["BooksConn"].ConnectionString.ToString();

                try
                {
                    
                    connection.Open();
                    MySqlCommand command = new MySqlCommand();
                    command.Connection = connection;
                    command.CommandText = "Update posts set postcomment=?postcomment where idpost=?idpost";
                    command.Parameters.AddWithValue("?postcomment","This post was deleted by a moderator.");
                    command.Parameters.AddWithValue("?idpost", idpost);
                    command.ExecuteNonQuery();
                    return "The post was deleted!";
                }
                catch (Exception ex)
                {
                    Debug.WriteLine(ex.Message);
                    return "Error";
                }
                finally
                {
                    connection.Close();
                }

            }



        }


        [OperationContract]
        public string LockThread(bool locked, int idThread)
        {


            using (MySqlConnection connection = new MySqlConnection())
            {
                connection.ConnectionString = ConfigurationManager.ConnectionStrings["BooksConn"].ConnectionString.ToString();

                try
                {

                    connection.Open();
                    MySqlCommand command = new MySqlCommand();
                    command.Connection = connection;
                    command.CommandText = "Update discussionthreads set locked=?locked where idthread=?idthread";
                    command.Parameters.AddWithValue("?locked", locked);
                    command.Parameters.AddWithValue("?idthread", idThread);
                    command.ExecuteNonQuery();
                    if (locked)
                    {
                        return "Thread is locked!";
                    }
                    else
                    {
                        return "Thread is unlocked!";
                    }

                }
                catch (Exception ex)
                {
                    Debug.WriteLine(ex.Message);
                    return "Error";
                }
                finally
                {
                    connection.Close();
                }

            }
        }


        [OperationContract]
        public string AddComment(int idThread, int idUser, string comment, string username)
        {

            var newComment = comment.Replace("[quote]", " <div class='quote'><img class='leftquote' src='../Images/left-quotes.png' alt='' /> <label class='quoteBorder'> Originally posted by  "+username+"  <label class='quoteuser'></label></label><div>");
            newComment = newComment.Replace("[/quote]", "  <img class='leftquote' src='../Images/right-quotes.png' alt='' /> </div></div><br/>");
            
            using (MySqlConnection connection = new MySqlConnection())
            {
                connection.ConnectionString = ConfigurationManager.ConnectionStrings["BooksConn"].ConnectionString.ToString();

                try
                {

                    connection.Open();
                    MySqlCommand command = new MySqlCommand();
                    command.Connection = connection;
                    command.CommandText = "Insert into posts(iduser, idthread, postcomment, dateposted) values(?iduser, ?idthread, ?postcomment, ?dateposted)";
                    command.Parameters.AddWithValue("?dateposted", DateTime.Today.ToString("dd/MMMM/yy"));
                    command.Parameters.AddWithValue("?iduser", idUser);
                    command.Parameters.AddWithValue("?idthread", idThread);
                    command.Parameters.AddWithValue("?postcomment", newComment);
                    command.ExecuteNonQuery();
                    return "Comment is posted";
                }
                catch (Exception ex)
                {
                    Debug.WriteLine(ex.Message);
                    return "Error";
                }
                finally
                {
                    connection.Close();
                }

            }
        }
    }
}
