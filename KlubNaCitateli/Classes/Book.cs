using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MySql.Data.MySqlClient;
using System.Configuration;

namespace KlubNaCitateli.Classes
{
    [Serializable]
    public class Book
    {
        public int IDBook { get; set; }
        public string ISBN { get; set; }
        public string Name { get; set; }
        public string ImageSrc { get; set; }
        public string ThumbnailSrc { get; set; }
        public string Description { get; set; }
        public string DateAdded { get; set; }
        public string YearPublished { get; set; }
        public string Language { get; set; }
        public int SumRating { get; set; }
        public int NumVotes { get; set; }
        public List<string> Authors;

        public Book()
        {

        }

        public Book(int id, string isbn, string name, List<string> authors, string imagesrc, string desc, string date, string dateAdded, int sumRating, int numVotes)
        {
            IDBook = id;
            ISBN = isbn;
            Name = name;
            ImageSrc = imagesrc;
            Description = desc;
            YearPublished = date;
            DateAdded = DateAdded;
            SumRating = sumRating;
            NumVotes = numVotes;

            Authors = new List<string>();
            for (int i = 0; i < authors.Count; i++)
            {
                Authors.Add(authors[i]);
            }

        }

        //Vrakja lista na knigi
        public static List<Book> SelectListBooks(string search, string language, string category)
        {
            List<Book> list = new List<Book>();

            using (MySqlConnection connection = new MySqlConnection())
            {
                connection.ConnectionString = ConfigurationManager.ConnectionStrings["BooksConn"].ConnectionString;
                connection.Open();

                string query;

                if (category == "Any" && language == "Any")
                    query = "SELECT Books.* FROM Books, Tags, Tagged WHERE Books.Name like @Search";
                else if (language == "Any")
                    query = "SELECT Books.* FROM Books, Categories, Tags, BelongsTo, Tagged WHERE Books.Name like @Search AND Categories.Name=@Category AND BelongsTo.IDBook=Book.IDBook AND BelongsTo.IDCategory=Categories.IDCategory";
                else
                    query = "SELECT Books.* FROM Books, Tags, Tagged WHERE Books.Name like @Search AND Books.Language=@Language";

                MySqlCommand command = new MySqlCommand(query, connection);

                command.Parameters.AddWithValue("@Search", "%" + search + "%");
                if (category != "Any")
                    command.Parameters.AddWithValue("@Category", category);
                if (language != "Any")
                    command.Parameters.AddWithValue("@Language", language);

                MySqlDataReader dataReader = command.ExecuteReader();

                Dictionary<string, string> dictionary = new Dictionary<string, string>();
                List<Dictionary<string, string>> books = new List<Dictionary<string, string>>();
                List<string> authors;

                while (dataReader.Read())
                {
                    dictionary.Add("IDBook", dataReader["IDBook"].ToString());
                    dictionary.Add("ISBN", dataReader["ISBN"].ToString());
                    dictionary.Add("Name", dataReader["Name"].ToString());
                    dictionary.Add("ImageSrc", dataReader["ImageSrc"].ToString());
                    dictionary.Add("Description", dataReader["Description"].ToString());
                    dictionary.Add("YearPublished", dataReader["YearPublished"].ToString());
                    dictionary.Add("DateAdded", dataReader["DateAdded"].ToString());
                    dictionary.Add("SumRating", dataReader["SumRating"].ToString());
                    dictionary.Add("NumVotes", dataReader["NumVotes"].ToString());
                    books.Add(dictionary);
                }
                dataReader.Close();

                for (int i = 0; i < books.Count; i++)
                {
                    //Lista na avtori za sekoja kniga
                    authors = new List<string>();
                    query = "SELECT a.Name FROM Authors as a, Books as b, Wrote as w WHERE w.IDAuthor = a.IDAuthor AND w.IDBook =" + books[i]["IDBook"];
                    command.CommandText = query;
                    dataReader = command.ExecuteReader();

                    while (dataReader.Read())
                    {
                        authors.Add(dataReader["Name"].ToString());
                    }
                    dataReader.Close();

                    //Dodavanje na knigata vo listata
                    Book b = new Book(Int32.Parse(books[i]["IDBook"]), books[i]["ISBN"], books[i]["Name"], authors, books[i]["ImageSrc"], books[i]["Description"], books[i]["YearPublished"], books[i]["DateAdded"], Int32.Parse(books[i]["SumRating"]), Int32.Parse(books[i]["NumVotes"]));
                    list.Add(b);
                }

                connection.Close();
                return list;
            }
        }

    }
}