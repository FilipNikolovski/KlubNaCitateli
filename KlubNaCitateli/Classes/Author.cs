using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KlubNaCitateli.Classes
{
    public class Author
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Country { get; set; }

        public Author(string name, string surname, string country)
        {
            Name = name;
            Surname = surname;
            Country = country;
        }
    }
}