using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MySql.Data.MySqlClient;

namespace KlubNaCitateli.Classes
{
    public class Database
    {
        private MySqlConnection connection;
        private string connString;

        public Database()
        {
            connString = "SERVER=localhost;DATABASE=ficodb;UID=root;PASSWORD=;";
            connection = new MySqlConnection(connString);
        }

        public bool OpenConnection()
        {
            try
            {
                connection.Open();
                return true;
            }
            catch (MySqlException ex)
            {
                return false;
            }
                
        }

        public bool CloseConnection()
        {
            try
            {
                connection.Close();
                return true;
            }
            catch (MySqlException ex)
            {
                return false;
            }
        }

        public List<Book> SelectListBooks(string search,string language, string category)
        {
            List<Book> list = new List<Book>();

            if (this.OpenConnection())
            {
                string query = "SELECT IDBook, Name, ImageSrc, Description, Date FROM Books, Categories, Tags, BelongsTo, Tagged";
                MySqlCommand command = new MySqlCommand(query, connection);
                MySqlDataReader dataReader = command.ExecuteReader();

                Dictionary<string, string> dictionary = new Dictionary<string, string>();
                List<Dictionary<string, string>> books = new List<Dictionary<string, string>>();
                List<Author> authors;
                
                while (dataReader.Read())
                {
                    dictionary.Add("IDBook", dataReader["IDBook"].ToString());
                    dictionary.Add("Name", dataReader["Name"].ToString());
                    dictionary.Add("ImageSrc", dataReader["ImageSrc"].ToString());
                    dictionary.Add("Description", dataReader["Description"].ToString());
                    dictionary.Add("Date", dataReader["YearPublished"].ToString());
                    books.Add(dictionary);
                }
                dataReader.Close();

                for (int i = 0; i < books.Count; i++)
                {
                    //Lista na avtori za sekoja kniga
                    authors = new List<Author>();
                    query = "SELECT a.Name, a.Surname,a.Country FROM Authors as a, Books as b, Wrote as w WHERE w.IDAuthor = a.IDAuthor AND w.IDBook =" + books[i]["IDBook"];
                    command.CommandText = query;
                    dataReader = command.ExecuteReader();

                    while (dataReader.Read())
                    {
                        Author a = new Author(dataReader["Name"].ToString(), dataReader["Surname"].ToString(), dataReader["Countrey"].ToString());
                        authors.Add(a);
                    }
                    dataReader.Close();

                    //Dodavanje na knigata vo listata
                    Book b = new Book(books[i]["Name"], authors, books[i]["ImageSrc"], books[i]["Description"], books[i]["Date"]);
                    list.Add(b);
                }

                this.CloseConnection();
                return list;
            }
            else
            {
                return list;
            }

        }


    }
}