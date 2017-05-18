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
using System.Net.Http.Formatting;

namespace WebApplication2.Controllers
{
    [RoutePrefix("api/dashboard")]
    public class GetDataController : ApiController
    {
        [Route("weightings")]
        [HttpGet]
        public HttpResponseMessage GetWeightings() {
            try {
                var response = new WeightModels {
                    TitleOfItem = "Test weighting",
                    Date = DateTime.Now,
                    Weight = 100.5,
                    UnitOfWeight = "Undefinied"
                };
                return Request.CreateResponse(HttpStatusCode.OK, new List<WeightModels>() { response }, JsonFormatter);
            } catch (Exception ex) {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex, JsonFormatter);
            }
        }

        [Route("weightings/{weightId}")]
        [HttpGet]
        public HttpResponseMessage GetWeightings(int? weightId) {
            if (weightId == null || weightId <= 0)
                return Request.CreateResponse(HttpStatusCode.BadRequest, new ErrorResponse(ErrorCodes.BadWeightId, "Provide correct weightId"), JsonFormatter);
            try {
                if (weightId == 1) {
                    var response = new WeightModels {
                        TitleOfItem = "Test weighting",
                        Date = DateTime.Now,
                        Weight = 100.5,
                        UnitOfWeight = "Undefinied"
                    };
                    return Request.CreateResponse(HttpStatusCode.OK, response, JsonFormatter);
                } else
                    return Request.CreateResponse(HttpStatusCode.BadRequest, new ErrorResponse(ErrorCodes.WeightNotFound, "No weight found against weightId"), JsonFormatter);
            } catch (Exception ex) {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex, JsonFormatter);
            }
        }


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


        protected JsonMediaTypeFormatter JsonFormatter => GetJsonformatter();

        private JsonMediaTypeFormatter GetJsonformatter() {
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


