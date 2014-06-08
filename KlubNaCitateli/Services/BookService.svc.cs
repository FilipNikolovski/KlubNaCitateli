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
using KlubNaCitateli.Classes;

namespace KlubNaCitateli.Services
{
    [ServiceContract(Namespace = "KlubNaCitateli")]
    [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
    public class BookService
    {
        
        [OperationContract]
        public string DoWork(string tags, string categories, string bookString)
        {
            using (MySqlConnection connection = new MySqlConnection())
            {
                connection.ConnectionString = ConfigurationManager.ConnectionStrings["BooksConn"].ConnectionString;

                try
                {
                    connection.Open();

                    string[] tagsList = tags.Split(new char[] { ',' });
                    string[] categoriesList = categories.Split(new char[] { ',' });
                    string[] bookStrings = bookString.Split(new string[] { "[split]" }, StringSplitOptions.None);

                    if (bookStrings.Length == 0)
                        return "Книгата не е успешно внесена!";

                    string[] authors = bookStrings[8].Split(new char[] { ',' });

                    //Vnesuvanje na KNIGA vo baza---------------------------------------------------------------
                    string query = "INSERT INTO books (Name, Description, CoverLink, Thumbnail, YearPublished, Language, SumRating, NumVotes, DateAdded, ISBN) VALUES (?Name, ?Description, ?CoverLink, ?Thumbnail, ?YearPublished, ?Language, ?SumRating, ?NumVotes, ?DateAdded, ?ISBN)";

                    MySqlCommand command = new MySqlCommand(query, connection);
                    command.Parameters.AddWithValue("?ISBN", bookStrings[0]);
                    command.Parameters.AddWithValue("?Name", bookStrings[1]);
                    command.Parameters.AddWithValue("?Description", bookStrings[2]);
                    command.Parameters.AddWithValue("?CoverLink", bookStrings[3]);
                    command.Parameters.AddWithValue("?Thumbnail", bookStrings[4]);
                    command.Parameters.AddWithValue("?DateAdded", bookStrings[5]);
                    command.Parameters.AddWithValue("?YearPublished", bookStrings[6]);
                    command.Parameters.AddWithValue("?Language", bookStrings[7]);
                    command.Parameters.AddWithValue("?SumRating", 0);
                    command.Parameters.AddWithValue("?NumVotes", 0);

                    command.ExecuteNonQuery();
                    //-------------------------------------------------------------------------------------------------

                    //Vnesuvanje na AVTORI vo baza---------------------------------------------------------------------
                    query = "INSERT INTO authors (Name) VALUES (?Name)";
                    command.CommandText = query;
                    foreach (string author in authors)
                    {
                        if (author.Trim() != "")
                        {
                            command.Parameters.Clear();
                            command.Parameters.AddWithValue("?Name", author);
                            command.ExecuteNonQuery();
                        }
                    }
                    //-------------------------------------------------------------------------------------------------

                    //Vnesuvanje na ID na avtori i knigi vo WROTE tabelata---------------------------------------------
                    query = "SELECT IDBook FROM books WHERE Name=?Name";
                    command.CommandText = query;
                    command.Parameters.Clear();
                    command.Parameters.AddWithValue("?Name", bookStrings[1]);

                    MySqlDataReader reader = command.ExecuteReader();

                    int idBook = -1;
                    if (reader.Read())
                    {
                        idBook = Int32.Parse(reader["IDBook"].ToString());

                        foreach (string author in authors)
                        {
                            query = "SELECT IDAuthor FROM authors WHERE Name=?Name";
                            command.CommandText = query;
                            command.Parameters.Clear();
                            command.Parameters.AddWithValue("?Name", author);

                            reader = command.ExecuteReader();
                            if (reader.Read())
                            {
                                int idAuthor = Int32.Parse(reader["IDAuthor"].ToString());

                                query = "INSERT INTO wrote (IDAuthor, IDBook) VALUES (?IDAuthor, ?IDBook)";
                                command.CommandText = query;
                                command.Parameters.Clear();
                                command.Parameters.AddWithValue("?IDAuthor", idAuthor);
                                command.Parameters.AddWithValue("?IDBook", idBook);

                                command.ExecuteNonQuery();
                            }
                            reader.Close();
                        }
                    }
                    reader.Close();
                    //-------------------------------------------------------------------------------------------------

                    if (categoriesList.Length == 0)
                        return "Категориите и таговите не се успешно внесени!";

                    //Vnesuvanje na ID na kategorii i knigi vo BELONGSTO tabelata--------------------------------------
                    
                    foreach (string category in categoriesList)
                    {
                        query = "SELECT IDCategory FROM categories WHERE Name=?CategoryName";
                        command.CommandText = query;
                        command.Parameters.Clear();
                        command.Parameters.AddWithValue("?CategoryName", category);

                        reader = command.ExecuteReader();
                        if (reader.Read())
                        {
                            int idCategory = Int32.Parse(reader["IDCategory"].ToString());

                            query = "INSERT INTO belongsto (IDCategory, IDBook) VALUES (?IDCategory, ?IDBook)";
                            command.CommandText = query;
                            command.Parameters.Clear();
                            command.Parameters.AddWithValue("?IDCategory", idCategory);
                            command.Parameters.AddWithValue("?IDBook", idBook);
                            command.ExecuteNonQuery();
                        }
                        reader.Close();
                    }
                    //-------------------------------------------------------------------------------------------------

                    if (tagsList.Length == 0)
                        return "Таговите не се успешно внесени!";

                    //Vnesuvanje na TAGOVI vo tabela-------------------------------------------------------------------
                    query = "INSERT INTO tags (Name) VALUES (?Name)";
                    command.CommandText = query;
                    foreach (string tag in tagsList)
                    {
                        if (tag.Trim() != "")
                        {
                            command.Parameters.Clear();
                            command.Parameters.AddWithValue("?Name", tag);
                            command.ExecuteNonQuery();
                        }
                    }
                    //-------------------------------------------------------------------------------------------------

                    //Vnesuvanje na ID na tagovi i kniga vo TAGGED tabela----------------------------------------------
                    foreach (string tag in tagsList)
                    {
                        query = "SELECT IDTag FROM tags WHERE Name=?TagName";
                        command.CommandText = query;
                        command.Parameters.Clear();
                        command.Parameters.AddWithValue("?TagName", tag);

                        reader = command.ExecuteReader();
                        if (reader.Read())
                        {
                            int idTag = Int32.Parse(reader["IDTag"].ToString());
                            
                            query = "INSERT INTO tagged (IDTag, IDBook) VALUES (?IDTag, ?IDBook)";
                            command.CommandText = query;
                            command.Parameters.Clear();
                            command.Parameters.AddWithValue("?IDTag", idTag);
                            command.Parameters.AddWithValue("?IDBook", idBook);
                            command.ExecuteNonQuery();
                        }
                        reader.Close();
                    }
                    //--------------------------------------------------------------------------------------------------

                }
                finally
                {
                    connection.Close();
                }
            }

            return "Книгата е успешно додадена.";
        }

        
    }
}
