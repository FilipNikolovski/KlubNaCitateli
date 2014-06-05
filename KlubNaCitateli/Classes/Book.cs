using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KlubNaCitateli.Classes
{
    public class Book
    {
        public int IDBook { get; set; }
        public string Name { get; set; }
        public string ImageSrc { get; set; }
        public string Description { get; set; }
        public string DateAdded { get; set; }
        public string YearPublished { get; set; }
        public int SumRating { get; set; }
        public int NumVotes { get; set; }
        public List<Author> Authors;


        public Book(int id, string name, List<Author> authors, string imagesrc, string desc, string date, string dateAdded, int sumRating, int numVotes)
        {
            IDBook = id;
            Name = name;
            ImageSrc = imagesrc;
            Description = desc;
            YearPublished = date;
            DateAdded = DateAdded;
            SumRating = sumRating;
            NumVotes = numVotes;

            Authors = new List<Author>();
            for (int i = 0; i < authors.Count; i++)
            {
                Authors.Add(authors[i]);
            }

        }

    }
}