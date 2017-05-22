using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Formatting;
using System.Reflection;
using System.Web;

namespace stud.webapi
{
    public static class JsonHelper
    {

        public static bool IsAnyNull(object obj)
        {
             foreach (PropertyInfo pi in obj.GetType().GetProperties()) {
                 var value = pi.GetValue(obj);
                 if (value == null) return true;
             }
             return false;
         }
        public static JsonMediaTypeFormatter GetJsonformatter()
        {
            var formatter = new JsonMediaTypeFormatter();
            var json = formatter.SerializerSettings;
            
            json.DateFormatHandling = Newtonsoft.Json.DateFormatHandling.IsoDateFormat;
            json.DateTimeZoneHandling = Newtonsoft.Json.DateTimeZoneHandling.Utc;
            json.NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore;
            json.Formatting = Newtonsoft.Json.Formatting.Indented;
            json.ContractResolver = new CamelCasePropertyNamesContractResolver();
            json.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
            
            //json.Culture = new CultureInfo("it-IT");
            
           return formatter;
            
        }
    }
}