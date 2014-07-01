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
                    command.Parameters.AddWithValue("?postcomment","");
                    command.Parameters.AddWithValue("?idpost", idpost);
                    command.ExecuteNonQuery();
                   
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

            return "Success";

        }
        
    }
}
