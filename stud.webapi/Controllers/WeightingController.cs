using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using stud.data;
using log4net;
using System.Net.Http.Formatting;
using System.Xml.Linq;
using Newtonsoft.Json;
using stud.webapi.Models;

namespace stud.webapi.Controllers
{
    public class WeightingController : ApiController
    {
        protected JsonMediaTypeFormatter JsonFormatter = JsonHelper.GetJsonformatter();

        public HttpResponseMessage GetWeightings()
        {
            Guid guid = Guid.NewGuid();
            Logger.InitLogger();
            Logger.Log.Info("GetWeightings Request " + guid);
            try
            {
                using (var _db = new StudDBEntities())
                {
                    List<WEIGHTING> WeightingList = _db.WEIGHTING.ToList();
                    Logger.Log.Info("GetWeightings Response " + guid + " " + JsonConvert.SerializeObject(WeightingList));
                    return Request.CreateResponse(HttpStatusCode.OK, WeightingList, JsonFormatter);

                }
            }
            catch (Exception e)
            {
                Logger.Log.Error("Exception GetWeightings " + guid + " " + e.Message);
                return Request.CreateResponse(HttpStatusCode.InternalServerError, e, JsonFormatter);

            }

        }

        public HttpResponseMessage GetWeighting(int? Id)
        {
            Guid guid = Guid.NewGuid();
            Logger.InitLogger();
            if (Id == null || Id <= 0)
            {
                Logger.Log.Error("GetWeighting " + guid + " ErrorCode " + ErrorCodes.InvalidItemId);
                return Request.CreateResponse(HttpStatusCode.BadRequest,
                    new ErrorResponse(ErrorCodes.InvalidItemId, "Provide correct itemId"), JsonFormatter);

            }
            try
            {
                Logger.Log.Info("GetWeighting "  + guid + " Request Id=" + Id);
                using (var _db = new StudDBEntities())
                {
                    List<WEIGHTING> WeightingList = _db.WEIGHTING.Where(i => i.ID == Id).ToList();
                    if (WeightingList.Count > 0)
                    {
                        Logger.Log.Info("GetWeighting " + guid + " Response " + JsonConvert.SerializeObject(WeightingList));
                        return Request.CreateResponse(HttpStatusCode.OK, WeightingList.FirstOrDefault(), JsonFormatter);

                    }
                    else
                    {
                        Logger.Log.Error("GetWeighting " + guid + " ErrorCode " + ErrorCodes.ItemsNotFound);
                        return Request.CreateResponse(HttpStatusCode.NotFound,
                            new ErrorResponse(ErrorCodes.ItemsNotFound, "No item found against itemId"), JsonFormatter);

                    }
                }
            }
            catch (Exception e)
            {
                Logger.Log.Error("Exception GetWeighting " + guid + " " + e.Message);
                return Request.CreateResponse(HttpStatusCode.InternalServerError, e, JsonFormatter);

            }
        }

        public HttpResponseMessage PostWeighting(WEIGHTING weighting)
        {
            Guid guid = Guid.NewGuid();
            Logger.InitLogger();
            if ((weighting == null) || (JsonHelper.IsAnyNull(weighting)))
            {
                Logger.Log.Error("PostWeighting " + guid + " ErrorCode " + ErrorCodes.InvalidItemModel);
                return Request.CreateResponse(HttpStatusCode.BadRequest,
                    new ErrorResponse(ErrorCodes.InvalidItemModel, "Provide correct Item"), JsonFormatter);

            }
            try
            {
                Logger.Log.Info("PostWeighting " + guid + " Request " + JsonConvert.SerializeObject(weighting));
                using (var _db = new StudDBEntities())
                {
                    if (_db.WEIGHTING.Where(i => i.ID == weighting.ID).ToList().Count == 0)
                    {
                        _db.WEIGHTING.Add(weighting);
                        _db.SaveChanges();
                        Logger.Log.Info("PostWeighting  " + guid + " Response " + JsonConvert.SerializeObject(weighting));
                        return Request.CreateResponse(HttpStatusCode.OK, weighting, JsonFormatter);

                    }
                    else
                    {
                        Logger.Log.Error("PostWeighting " + guid + " ErrorCode " + ErrorCodes.ItemAlreadyExists);
                        return Request.CreateResponse(HttpStatusCode.Conflict,
                            new ErrorResponse(ErrorCodes.ItemAlreadyExists, "Item with same ID already exists"),
                            JsonFormatter);

                    }
                }
            }
            catch (Exception e)
            {
                Logger.Log.Error("PostWeighting " + guid + " Exception " + e.Message);
                return Request.CreateResponse(HttpStatusCode.InternalServerError, e, JsonFormatter);
            }

        }

        public HttpResponseMessage PutWeighting(int? Id, WEIGHTING weighting)
        {
            Guid guid = Guid.NewGuid();
            Logger.InitLogger();
            if (Id == null || Id <= 0)
            {
                Logger.Log.Error("PutWeighting " + guid + " ErrorCode " + ErrorCodes.InvalidItemId);
                return Request.CreateResponse(HttpStatusCode.BadRequest,
                    new ErrorResponse(ErrorCodes.InvalidItemId, "Provide correct itemId"), JsonFormatter);
            }
            if ((weighting == null) || (JsonHelper.IsAnyNull(weighting)))
            {
                Logger.Log.Error("PutWeighting " +guid + " ErrorCode " + ErrorCodes.InvalidItemModel);
                return Request.CreateResponse(HttpStatusCode.BadRequest,
                    new ErrorResponse(ErrorCodes.InvalidItemModel, "Provide correct Item"), JsonFormatter);
            }
            try
            {
                Logger.Log.Info("PutWeighting " + guid + "Request " + Id + " " + JsonConvert.SerializeObject(weighting));
                using (var _db = new StudDBEntities())
                {
                    if (_db.WEIGHTING.Where(i => i.ID == weighting.ID).ToList().Count > 0)
                    {
                        _db.WEIGHTING.Remove(_db.WEIGHTING.Where(i => i.ID == Id).FirstOrDefault());
                        _db.WEIGHTING.Add(weighting);
                        _db.SaveChanges();
                        Logger.Log.Info("PutWeighting  " + guid + " Response " + JsonConvert.SerializeObject(weighting));
                        return Request.CreateResponse(HttpStatusCode.OK, weighting, JsonFormatter);
                    }
                    else
                    {
                        Logger.Log.Error("PutWeighting " + guid + " ErrorCode " + ErrorCodes.ItemsNotFound);
                        return Request.CreateResponse(HttpStatusCode.Conflict,
                            new ErrorResponse(ErrorCodes.ItemsNotFound, "Can't find item "),
                            JsonFormatter);
                    }
                }
            }
            catch (Exception e)
            {
                Logger.Log.Error("PutWeighting " + guid + " Exception " + e.Message);
                return Request.CreateResponse(HttpStatusCode.InternalServerError, e, JsonFormatter);
            }


        }
    }
}
