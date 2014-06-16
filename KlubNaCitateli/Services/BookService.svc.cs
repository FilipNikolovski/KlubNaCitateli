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

                    string query = "select idbook from books where name=?name AND isbn=?isbn";
                    MySqlCommand command = new MySqlCommand(query, connection);
                    command.Parameters.AddWithValue("?name", bookStrings[1]);
                    command.Parameters.AddWithValue("?isbn", bookStrings[0]);

                    MySqlDataReader reader = command.ExecuteReader();
                    if (!reader.HasRows)
                    {
                        reader.Close();
                        //Vnesuvanje na KNIGA vo baza---------------------------------------------------------------
                        query = "INSERT INTO books (Name, Description, CoverLink, Thumbnail, YearPublished, Language, SumRating, NumVotes, DateAdded, ISBN) VALUES (?Name, ?Description, ?CoverLink, ?Thumbnail, ?YearPublished, ?Language, ?SumRating, ?NumVotes, ?DateAdded, ?ISBN)";
                        command.CommandText = query;
                        command.Parameters.Clear();
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
                    }
                    else
                        return "Книгата веќе постои!";
                    
                    //-------------------------------------------------------------------------------------------------

                    //Vnesuvanje na AVTORI vo baza---------------------------------------------------------------------
                    query = "INSERT INTO authors (Name) VALUES (?Name)";
                    command.CommandText = query;
                    foreach (string author in authors)
                    {
                        if (author.Trim() != "")
                        {
                            MySqlCommand cm = new MySqlCommand();
                            cm.CommandText = "select IDAuthor from authors where name=?name";
                            cm.Connection = connection;
                            cm.Parameters.AddWithValue("?name", author);
                            MySqlDataReader rd=cm.ExecuteReader();
                            if (!rd.HasRows)
                            {
                                rd.Close();
                                command.Parameters.Clear();
                                command.Parameters.AddWithValue("?Name", author);
                                command.ExecuteNonQuery();
                            }
                            else
                            {
                                rd.Close();
                            }
                        }
                    }
                    //-------------------------------------------------------------------------------------------------

                    //Vnesuvanje na ID na avtori i knigi vo WROTE tabelata---------------------------------------------
                    query = "SELECT IDBook FROM books WHERE Name=?Name AND isbn=?isbn";
                    command.CommandText = query;
                    command.Parameters.Clear();
                    command.Parameters.AddWithValue("?Name", bookStrings[1]);
                    command.Parameters.AddWithValue("?isbn", bookStrings[0]);

                    reader = command.ExecuteReader();

                    int idBook = -1;
                    if (reader.Read())
                    {
                        idBook = Int32.Parse(reader["IDBook"].ToString());

                        reader.Close();
                        foreach (string author in authors)
                        {
                            if (author.Trim() != "")
                            {
                                query = "SELECT IDAuthor FROM authors WHERE Name=?Name";
                                command.CommandText = query;
                                command.Parameters.Clear();
                                command.Parameters.AddWithValue("?Name", author);

                                reader = command.ExecuteReader();
                                int idAuthor = -1;

                                if (reader.Read())
                                {
                                    idAuthor = Int32.Parse(reader["IDAuthor"].ToString());
                                    reader.Close();
                                    query = "INSERT INTO wrote (IDAuthor, IDBook) VALUES (?IDAuthor, ?IDBook)";
                                    command.CommandText = query;
                                    command.Parameters.Clear();
                                    command.Parameters.AddWithValue("?IDAuthor", idAuthor);
                                    command.Parameters.AddWithValue("?IDBook", idBook);
                                    command.ExecuteNonQuery();
                                }


                            }
                        }
                    }
                    //-------------------------------------------------------------------------------------------------

                    if (categoriesList.Length == 0)
                        return "Категориите и таговите не се успешно внесени!";

                    //Vnesuvanje na ID na kategorii i knigi vo BELONGSTO tabelata--------------------------------------
                    foreach (string category in categoriesList)
                    {
                        if (category.Trim() != "")
                        {
                            query = "SELECT IDCategory FROM categories WHERE Name=?CategoryName";
                            command.CommandText = query;
                            command.Parameters.Clear();
                            command.Parameters.AddWithValue("?CategoryName", category);

                            reader = command.ExecuteReader();
                            int idCategory = -1;

                            if (reader.Read())
                            {
                                idCategory = Int32.Parse(reader["IDCategory"].ToString());
                                reader.Close();
                                query = "INSERT INTO belongsto (IDCategory, IDBook) VALUES (?IDCategory, ?IDBook)";
                                command.CommandText = query;
                                command.Parameters.Clear();
                                command.Parameters.AddWithValue("?IDCategory", idCategory);
                                command.Parameters.AddWithValue("?IDBook", idBook);
                                command.ExecuteNonQuery();
                            }
                        }
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
                            MySqlCommand cm = new MySqlCommand();
                            cm.CommandText = "select IDTag from tags where name=?name";
                            cm.Connection = connection;
                            cm.Parameters.AddWithValue("?name", tag);
                            MySqlDataReader rd = cm.ExecuteReader();
                            if (!rd.HasRows)
                            {
                                rd.Close();
                                command.Parameters.Clear();
                                command.Parameters.AddWithValue("?Name", tag);
                                command.ExecuteNonQuery();
                            }
                            else
                            {
                                rd.Close();
                            }
                           
                        }
                    }
                    //-------------------------------------------------------------------------------------------------

                    //Vnesuvanje na ID na tagovi i kniga vo TAGGED tabela----------------------------------------------
                    foreach (string tag in tagsList)
                    {
                        if (tag.Trim() != "")
                        {
                            query = "SELECT IDTag FROM tags WHERE Name=?TagName";
                            command.CommandText = query;
                            command.Parameters.Clear();
                            command.Parameters.AddWithValue("?TagName", tag);

                            reader = command.ExecuteReader();
                            int idTag = -1;

                            if (reader.Read())
                            {
                                idTag = Int32.Parse(reader["IDTag"].ToString());
                                reader.Close();
                                query = "INSERT INTO tagged (IDTag, IDBook) VALUES (?IDTag, ?IDBook)";
                                command.CommandText = query;
                                command.Parameters.Clear();
                                command.Parameters.AddWithValue("?IDTag", idTag);
                                command.Parameters.AddWithValue("?IDBook", idBook);
                                command.ExecuteNonQuery();
                            }
                        }       
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
        [OperationContract]
        public string AddAllBooks(string tags, string categories, string bookIds)
        {
            using (MySqlConnection connection = new MySqlConnection())
            {
                connection.ConnectionString = ConfigurationManager.ConnectionStrings["BooksConn"].ConnectionString;
                try
                {
                    connection.Open();

                    string[] tagsList = tags.Split(new char[] { ',' });
                    string[] categoriesList = categories.Split(new char[] { ',' });
                    string[] bookIdsList = bookIds.Split(new char[] { ',' });

                    string query = "";
                    MySqlCommand command = new MySqlCommand();
                    command.Connection = connection;

                    //Vnesuvanje na TAGOVI vo tabela-------------------------------------------------------------------
                    query = "INSERT INTO tags (Name) VALUES (?Name)";
                    command.CommandText = query;
                    foreach (string tag in tagsList)
                    {
                        if (tag.Trim() != "")
                        {
                            MySqlCommand cm = new MySqlCommand();
                            cm.CommandText = "select IDTag from tags where name=?name";
                            cm.Connection = connection;
                            cm.Parameters.AddWithValue("?name", tag);
                            MySqlDataReader rd = cm.ExecuteReader();
                            if (!rd.HasRows)
                            {
                                rd.Close();
                                command.Parameters.Clear();
                                command.Parameters.AddWithValue("?Name", tag);
                                command.ExecuteNonQuery();
                            }
                            else
                                rd.Close();
                        }
                    }
                    //-------------------------------------------------------------------------------------------------

                    MySqlDataReader reader;
                    foreach (string idBook in bookIdsList)
                    {
                        if (idBook.Trim() != "")
                        {
                            //Vnesuvanje na ID na tagovi i kniga vo TAGGED tabela----------------------------------------------
                            foreach (string tag in tagsList)
                            {
                                if (tag.Trim() != "")
                                {
                                    query = "SELECT IDTag FROM tags WHERE Name=?TagName";
                                    command.CommandText = query;
                                    command.Parameters.Clear();
                                    command.Parameters.AddWithValue("?TagName", tag);

                                    reader = command.ExecuteReader();

                                    if (reader.Read())
                                    {
                                        int idTag = Int32.Parse(reader["IDTag"].ToString());
                                        reader.Close();
                                        query = "INSERT INTO tagged (IDTag, IDBook) VALUES (?IDTag, ?IDBook)";
                                        command.CommandText = query;
                                        command.Parameters.Clear();
                                        command.Parameters.AddWithValue("?IDTag", idTag);
                                        command.Parameters.AddWithValue("?IDBook", Int32.Parse(idBook));
                                        command.ExecuteNonQuery();
                                    }
                                    else
                                        reader.Close();
                                }
                            }
                            //--------------------------------------------------------------------------------------------------

                            //Vnesuvanje na ID na kategorii i knigi vo BELONGSTO tabelata--------------------------------------
                            foreach (string category in categoriesList)
                            {
                                if (category.Trim() != "")
                                {
                                    query = "SELECT IDCategory FROM categories WHERE Name=?CategoryName";
                                    command.CommandText = query;
                                    command.Parameters.Clear();
                                    command.Parameters.AddWithValue("?CategoryName", category);

                                    reader = command.ExecuteReader();
                                    int idCategory = -1;

                                    if (reader.Read())
                                    {
                                        idCategory = Int32.Parse(reader["IDCategory"].ToString());
                                        reader.Close();
                                        query = "INSERT INTO belongsto (IDCategory, IDBook) VALUES (?IDCategory, ?IDBook)";
                                        command.CommandText = query;
                                        command.Parameters.Clear();
                                        command.Parameters.AddWithValue("?IDCategory", idCategory);
                                        command.Parameters.AddWithValue("?IDBook", Int32.Parse(idBook));
                                        command.ExecuteNonQuery();
                                    }
                                    else
                                        reader.Close();
                                }
                            }
                            //-------------------------------------------------------------------------------------------------
                        }
                    }
                }
                catch(Exception e)
                {
                    return "Таговите и категориите не се успешно внесени.\n Error: " + e.Message;
                }
                finally
                {
                    connection.Close();
                }
            }

            return "Книгите се успешно внесени.";
        }
        
    }
}
