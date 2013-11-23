using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MySql.Data.MySqlClient;

namespace KlubNaCitateli
{
    public class Database
    {
        public MySqlConnection connection;

        public Database()
        {
            connection = new MySqlConnection("Database=111151_books;Data Source=mysql.students.finki.ukim.mk;User Id=111151;Password=0809992450006fnk!");
            connection.Open();
        }

    }
}