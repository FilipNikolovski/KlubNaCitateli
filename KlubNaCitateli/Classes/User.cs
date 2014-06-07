using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MySql.Data.MySqlClient;
using System.Text.RegularExpressions;
using System.Configuration;

namespace KlubNaCitateli.Classes
{
    public class User
    {
        public string name { get; set; }
        public string surname { get; set; }
        public string email { get; set; }
        public string username { get; set; }
        public string password { get; set; }
        public string type { get; set; }
        public int numComments { get; set; }
        public String aboutUser { get; set; }
       

        public User(string name, string surname, string email, string username, string password, String aboutUser)
        {
            this.name = name;
            this.surname = surname;
            this.email = email;
            this.username = username;
            this.password = password;
            this.type = "user";
            this.numComments = 0;
            this.aboutUser = aboutUser;
        }


        public void CheckIfUserExists(bool checkEmail, bool checkUsername)
        {
            
                    MySqlConnection connection = new MySqlConnection();
                    connection.ConnectionString = ConfigurationManager.ConnectionStrings["BooksConn"].ConnectionString.ToString();
                    MySqlCommand command = new MySqlCommand();
                    command.CommandText = "SELECT IDUser from users where email=@email";
                    command.Parameters.AddWithValue("@email", email.ToString());
                    command.Connection = connection;
                    connection.Open();
                    MySqlDataReader dataReader = command.ExecuteReader();
                    if (dataReader.HasRows)
                    {
                        checkEmail = false;
                    }
                    dataReader.Close();
            
                    MySqlCommand command1 = new MySqlCommand();
                    command1.Connection = connection;
                    command1.CommandText = "SELECT IDUser from users where username=@username";
                    command1.Parameters.AddWithValue("@username", username.ToString());
                   MySqlDataReader dataReader1 = command1.ExecuteReader();
                    if (dataReader1.HasRows)
                    {
                        checkUsername = false;
                    }
                    dataReader1.Close();
                   
                    connection.Close();
                

               
            }
        }





    }
