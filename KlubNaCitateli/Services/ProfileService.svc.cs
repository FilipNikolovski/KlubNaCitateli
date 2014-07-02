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
using System.Web.Script.Serialization;

namespace KlubNaCitateli.Services
{
    [ServiceContract(Namespace = "KlubNaCitateli")]
    [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
    public class ProfileService
    {

        [OperationContract]
        public string UpdateUsername(string username, int iduser)
        {

            using (MySqlConnection connection = new MySqlConnection())
            {
                connection.ConnectionString = ConfigurationManager.ConnectionStrings["BooksConn"].ConnectionString.ToString();

                try
                {
                    connection.Open();

                    int id = Convert.ToInt32(iduser);

                    MySqlCommand command = new MySqlCommand();
                    command.Connection = connection;
                    command.CommandText = "select iduser from users where username=?username";
                    command.Parameters.AddWithValue("?username", username);
                    MySqlDataReader reader = command.ExecuteReader();
                    if (!reader.HasRows)
                    {
                        reader.Close();
                        command.CommandText = "update users set username=?username where iduser=?iduser";
                        command.Parameters.Clear();
                        command.Parameters.AddWithValue("?iduser", id);
                        command.Parameters.AddWithValue("?username", username);
                        command.ExecuteNonQuery();
                        return "Username is updated";

                    }
                    else
                    {
                        reader.Close();
                        return "Username is already in use. Please choose another one!";
                    }


                }
                catch (Exception ex)
                {
                }
                finally
                {
                    connection.Close();
                }



                return "Error";


               
            }


        }


        [OperationContract]
        public string UpdateEmail(string email, int iduser)
        {

            using (MySqlConnection connection = new MySqlConnection())
            {
                connection.ConnectionString = ConfigurationManager.ConnectionStrings["BooksConn"].ConnectionString.ToString();

                try
                {
                    connection.Open();

                    int id = Convert.ToInt32(iduser);

                    MySqlCommand command = new MySqlCommand();
                    command.Connection = connection;
                    command.CommandText = "select iduser from users where email=?email";
                    command.Parameters.AddWithValue("?email", email);
                    MySqlDataReader reader = command.ExecuteReader();
                    if (!reader.HasRows)
                    {
                        reader.Close();
                        command.CommandText = "update users set email=?email where iduser=?iduser";
                        command.Parameters.Clear();
                        command.Parameters.AddWithValue("?iduser", id);
                        command.Parameters.AddWithValue("?email", email);
                        command.ExecuteNonQuery();
                        return "Email is updated";

                    }
                    else
                    {
                        reader.Close();
                        return "Email is already in use. Please choose another one!";
                    }

                }
                catch (Exception ex)
                {
                }
                finally
                {
                    connection.Close();
                }

                return "Error";
            }
        }

        [OperationContract]
        public string UpdateAbout(string about, int iduser)
        {

            using (MySqlConnection connection = new MySqlConnection())
            {
                connection.ConnectionString = ConfigurationManager.ConnectionStrings["BooksConn"].ConnectionString.ToString();

                try
                {
                    connection.Open();

                    int id = Convert.ToInt32(iduser);

                    MySqlCommand command = new MySqlCommand();
                    command.Connection = connection;
                        command.CommandText = "update users set about=?about where iduser=?iduser";
                        command.Parameters.Clear();
                        command.Parameters.AddWithValue("?iduser", id);
                        command.Parameters.AddWithValue("?about", about);
                        command.ExecuteNonQuery();
                        return "About is updated";
 


                }
                catch (Exception ex)
                {
                }
                finally
                {
                    connection.Close();
                }

                return "Error";
            }
        }

        [OperationContract]
        public string AddToFavorites(string jsonData)
        {
            Dictionary<string, string> json = new JavaScriptSerializer().Deserialize<Dictionary<string, string>>(jsonData);
            Dictionary<string, string> result = new Dictionary<string, string>();
            JavaScriptSerializer serializer = new JavaScriptSerializer();

            using (MySqlConnection conn = new MySqlConnection())
            {
                conn.ConnectionString = ConfigurationManager.ConnectionStrings["BooksConn"].ConnectionString;
                try
                {
                    conn.Open();
                    
                    string sql = "SELECT * FROM UserBooks WHERE IDUser=?IDUser AND IDBook=?IDBook";
                    MySqlCommand command = new MySqlCommand(sql, conn);
                    command.Parameters.AddWithValue("?IDUser", json["userId"]);
                    command.Parameters.AddWithValue("?IDBook", json["bookId"]);

                    MySqlDataReader reader = command.ExecuteReader();

                    if (reader.HasRows)
                    {
                        reader.Close();
                        result["status"] = "error";
                        result["message"] = "You already have this book in your favorites.";

                        return serializer.Serialize((object)result);
                    }
                    else
                    {
                        reader.Close();

                        command.CommandText = "INSERT INTO UserBooks (IDUser, IDBook) VALUES (?IDUser, ?IDBook)";
                        command.ExecuteNonQuery();

                        result["status"] = "success";
                        result["message"] = "The book has been added to your favorites.";

                        return serializer.Serialize((object)result);
                    }
                }
                catch (Exception ex)
                {
                    result["status"] = "error";
                    result["message"] = ex.Message;

                    return serializer.Serialize((object)result);
                }
                finally
                {
                    conn.Close();
                }
            }
        }
    }
}