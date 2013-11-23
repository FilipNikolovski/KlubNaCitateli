using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MySql.Data.MySqlClient;

namespace KlubNaCitateli
{
    public class Database
    {


        public Database()
        {
            MySqlConnectionStringBuilder conn_string = new MySqlConnectionStringBuilder();
            conn_string.Server = "https://cpanel.students.finki.ukim.mk:2079/index.php?token=a6b521ff97ffbd2421bdba833516bb05";
            conn_string.UserID = "111151";
            conn_string.Password = "0809992450006fnk!";
            conn_string.Database = "111151_books";

            using (MySqlConnection conn = new MySqlConnection(conn_string.ToString()))
            {
                conn.Open();
            }
        }

    }
}