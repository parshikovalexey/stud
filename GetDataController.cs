using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Xml.Linq;
using Newtonsoft.Json;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using System.IO;
using WebApplication2.Models;

namespace WebApplication2.Controllers
{
    public class GetDataController : ApiController
    {
        public XElement GetXml(WeightModels w)
        {
            var res = new XElement
                ("Item",
                    new XElement("TitleOfItem", w.titleOfItem),
                    new XElement("Date", w.date),
                    new XElement("Weight", w.weight),
                    new XElement("UnitOfWeight", w.unitOfWeight)
                );
            return res;
        }

        public string GetJson(WeightModels w)
        {
            string res = JsonConvert.SerializeObject(w);
            return res;
        }
    }
}


