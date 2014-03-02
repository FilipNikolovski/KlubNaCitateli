using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Activation;
using System.ServiceModel.Web;
using System.Text;
using KlubNaCitateli.Services;
using KlubNaCitateli.Classes;

namespace KlubNaCitateli.Services
{
    [ServiceContract(Namespace = "")]
    [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
    public class SearchService
    {

        private class BooksObj
        {
            public List<Book> Books;
        }

        private Database db;
        public SearchService()
        {
            db = new Database();
        }

        [OperationContract]
        public string GetBooks(string search, string language, string category)
        {
            List<Book> list = new List<Book>();
            list = db.SelectListBooks(search, language, category);

            return  (new BooksObj() { Books = list }).ToJSON();
           
        }


    }
}
