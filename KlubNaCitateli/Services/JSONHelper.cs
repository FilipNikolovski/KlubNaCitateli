using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;

namespace KlubNaCitateli.Services
{
    public static class JSONHelper
    {
        public static string ToJSON(this object obj)
        {
            JavaScriptSerializer serializer = new JavaScriptSerializer();

            return serializer.Serialize(obj);
        }

        public static string ToJSON(this object obj, int recursion)
        {
            JavaScriptSerializer serializer = new JavaScriptSerializer();

            serializer.RecursionLimit = recursion;

            return serializer.Serialize(obj);
        }

    }
}