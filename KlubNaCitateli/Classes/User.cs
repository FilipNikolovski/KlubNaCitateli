using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MySql.Data.MySqlClient;
using System.Text.RegularExpressions;

namespace KlubNaCitateli.Classes
{
    public class User
    {
        public string name;
        public string surname;
        public string email;
        public string username;
        public string password;
        public string type;
        public int numComments;
        public String aboutUser;
        private string connString = "SERVER=localhost;DATABASE=ficodb;UID=root;PASSWORD=;";


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


        public void ValidateEmail(bool checkEmail, bool checkUsername)
        {
            
            using (MySqlConnection connection = new MySqlConnection())
            {
                connection.ConnectionString = connString;
                connection.Open();
                String query = "SELECT IDUser from users where email=" + email + ";";
                MySqlCommand command = new MySqlCommand(query, connection);
                MySqlDataReader dataReader = command.ExecuteReader();
                if (dataReader.HasRows)
                {
                    checkEmail = false;
                }

                command.CommandText = "SELECT IDUser from users where username=" + username + ";";
                dataReader = command.ExecuteReader();
                if (dataReader.HasRows)
                {
                    checkUsername = false;
                }
                connection.Close();

                if (!Regex.IsMatch(email, @"^(?("")("".+?""@)|(([0-9a-zA-Z]((\.(?!\.))|[-!#\$%&'\*\+/=\?\^`\{\}\|~\w])*)(?<=[0-9a-zA-Z])@))(?(\[)(\[(\d{1,3}\.){3}\d{1,3}\])|(([0-9a-zA-Z][-\w]*[0-9a-zA-Z]\.)+[a-zA-Z]{2,6}))$"))
                {
                    checkEmail = false;
                }  
            }
        }





    }
}