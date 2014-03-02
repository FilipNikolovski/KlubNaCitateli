using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KlubNaCitateli.Classes
{
    public class Book
    {
        public string Name { get; set; }
        public string ImageSrc { get; set; }
        public string Description { get; set; }
        public string Date { get; set; }
        public List<Author> Authors;


        public Book(string name,List<Author> authors, string imagesrc,string desc, string date)
        {
            Name = name;
            ImageSrc = imagesrc;
            Description = desc;
            Date = date;

            Authors = new List<Author>();
            for (int i = 0; i < authors.Count; i++)
            {
                Authors.Add(authors[i]);
            }

        }

    }
}