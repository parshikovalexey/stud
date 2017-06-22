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
using System.Web;
using Newtonsoft.Json.Serialization;

namespace WebApplication2.Controllers
{
    [RoutePrefix("api/dashboard")]
    public class GetDataController : ApiController
    {
        [Route("weightings")]
        [HttpGet]
        public HttpResponseMessage SaveWeightings()
        {
            try
            {
                var response = new WeightModels
                {
                    TitleOfItem = "Test weighting",
                    Date = DateTime.Now,
                    Weight = 100.5,
                    UnitOfWeight = "Undefinied",
                    WeightId = 0
                };
                return Request.CreateResponse(HttpStatusCode.OK, new List<WeightModels>() { response }, JsonFormatter);
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex, JsonFormatter);
            }
        }

        [Route("weightings/{weightId}")]
        [HttpGet]
        public HttpResponseMessage SaveWeightings(int? WeightId)
        {
            if (WeightId == null || WeightId < 0)
                return Request.CreateResponse(HttpStatusCode.BadRequest, new ErrorResponse(ErrorCodes.InvalidWeightId, "Provide correct weightId"), JsonFormatter);
            try
            {
                if (WeightId == 0)
                {
                    var response = new WeightModels
                    {
                        TitleOfItem = "Test weighting",
                        Date = DateTime.Now,
                        Weight = 100.5,
                        UnitOfWeight = "Undefinied",
                        WeightId = 0
                    };
                    return Request.CreateResponse(HttpStatusCode.OK, response, JsonFormatter);
                }
                else
                    return Request.CreateResponse(HttpStatusCode.NotFound, new ErrorResponse(ErrorCodes.WeightNotFound, "No weight found against weightId"), JsonFormatter);
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex, JsonFormatter);
            }
        }

        [Route("weightings")]
        [HttpPost]
        public HttpResponseMessage SaveWeightings(WeightModels request)
        {
            if (request == null || request.Weight == null)
                return Request.CreateResponse(HttpStatusCode.BadRequest, new ErrorResponse(ErrorCodes.InvalidWeightModel, "Provide correct WeightModels"), JsonFormatter);
            try
            {
                if (request.Weight >= 10.0)
                {
                    var response = request;
                    return Request.CreateResponse(HttpStatusCode.OK, new List<WeightModels>() { response }, JsonFormatter);
                }
                else
                    return Request.CreateResponse(HttpStatusCode.NotFound, new ErrorResponse(ErrorCodes.SaveWeightError, "Weight so small"), JsonFormatter);
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex, JsonFormatter);
            }
        }

        [Route("weightings")]
        [HttpPost]
        public HttpResponseMessage SaveWeightings(List<WeightModels> weightings) // поменял название на SaveWeightings
        {
            try // отлавливаем Exeption
            {
                HttpResponseMessage errorResponse = Request.CreateResponse(); // поменял имя переменной на errorResponse
                bool isBadRequest = false; // если входные данные некорректны - изменяем на true
                for (int i = 0; i < weightings.Count; i++)
                {
                    var request = weightings[i];
                    if (request.WeightId == null || request.WeightId < 0) // проверка корректности Id
                    {
                        errorResponse = Request.CreateResponse(HttpStatusCode.BadRequest, new ErrorResponse(ErrorCodes.InvalidWeightId, "Provide correct weightId", i), JsonFormatter);
                        isBadRequest = true;
                        break;
                    }

                    if (request.Weight == null) // проверка корректности взвешивания
                    {
                        errorResponse = Request.CreateResponse(HttpStatusCode.NotFound, new ErrorResponse(ErrorCodes.WeightNotFound, "No weight found against weightId", i), JsonFormatter);
                        isBadRequest = true;
                        break;
                    }

                    if (request.Weight <= 10.0) // проверка корректности взвешивания
                    {
                        errorResponse = Request.CreateResponse(HttpStatusCode.NotFound, new ErrorResponse(ErrorCodes.SaveWeightError, "Weight so small"), JsonFormatter);
                        isBadRequest = true;
                        break;
                    }
                }

                if (!isBadRequest)
                {
                    try
                    {
                        return Request.CreateResponse(HttpStatusCode.OK, weightings, JsonFormatter);
                    }
                    catch (Exception ex)
                    {
                        return Request.CreateResponse(HttpStatusCode.InternalServerError, ex, JsonFormatter);
                    }
                }
                else
                {
                    return errorResponse;
                }
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex, JsonFormatter);
            }
        }

        public XElement GetXml(WeightModels w)
        {
            var res = new XElement
                ("Item",
                    new XElement("TitleOfItem", w.TitleOfItem),
                    new XElement("Date", w.Date),
                    new XElement("Weight", w.Weight),
                    new XElement("UnitOfWeight", w.UnitOfWeight)
                );
            return res;
        }

        public string GetJson(WeightModels w)
        {
            string res = JsonConvert.SerializeObject(w);
            return res;
        }

        protected JsonMediaTypeFormatter JsonFormatter => GetJsonformatter();

        private JsonMediaTypeFormatter GetJsonformatter()
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
