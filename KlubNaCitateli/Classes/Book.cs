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
        public List<string> Categories;
        public List<string> Tags;

        public Book()
        {
            Authors = new List<string>();
            Categories = new List<string>();
            Tags = new List<string>();
        }

        public Book(int id, string isbn, string name, List<string> authors, List<string> categories, List<string> tags, string imagesrc, string desc, string date, string dateAdded, int sumRating, int numVotes, string language)
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
            Language = language;

            Authors = new List<string>();
            for (int i = 0; i < authors.Count; i++)
                Authors.Add(authors[i]);

            Categories = new List<string>();
            for(int i=0; i<categories.Count; i++)
                Categories.Add(categories[i]);

            Tags = new List<string>();
            for (int i = 0; i < tags.Count; i++)
                Tags.Add(tags[i]);
        }
    }
}