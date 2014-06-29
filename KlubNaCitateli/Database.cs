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

            string fic = "kreten si";
            bool flag = true;
            if (flag)
                fic = "blabla";
            using (MySqlConnection conn = new MySqlConnection("Server=mysql.students.finki.ukim.mk;Database=111151_books;Uid=111151_Boki;Password=boki123_"))
            {
                conn.Open();
            }
        }

    }
}