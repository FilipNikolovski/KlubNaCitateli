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

        private Database db;
        private Book book;
        

        public SearchService()
        {
            db = new Database();
            db.OpenConnection();
        }

        [OperationContract]
        public string GetBooks(string search)
        {
            /*
            return (new Book()
            {
                ImageSrc = "",
                BookName = "",
                Author = "",
                Date = "",
            }).ToJSON();
            */
        }


    }
}
