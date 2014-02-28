using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Activation;
using System.ServiceModel.Web;
using System.Text;
using KlubNaCitateli.Services;

namespace KlubNaCitateli.Services
{
    [ServiceContract(Namespace = "")]
    [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
    public class SearchService
    {

        public class Book
        {
            public string ImageSrc;
            public string BookName;
            public string Author;
            public string Date;
        }
        
        [OperationContract]
        public string GetBooks(string search)
        {
            return (new Book()
                        {
                            ImageSrc = "",
                            BookName = "",
                            Author = "",
                            Date = "",
                        }).ToJSON();
        }

       
    }
}
