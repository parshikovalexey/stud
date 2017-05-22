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
            Logger.InitLogger();
            Logger.Log.Info("GetWeightings Request");
            try
            {
                using (var _db = new StudDBEntities())
                {
                    List<WEIGHTING> WeightingList = _db.WEIGHTING.ToList();
                    Logger.Log.Info("GetWeightings Response " + JsonConvert.SerializeObject(WeightingList));
                    return Request.CreateResponse(HttpStatusCode.OK, WeightingList, JsonFormatter);

                }
            }
            catch (Exception e)
            {
                Logger.Log.Error("Exception GetWeightings" + e.Message);
                return Request.CreateResponse(HttpStatusCode.InternalServerError, e, JsonFormatter);

            }

        }

        public HttpResponseMessage GetWeighting(int? Id)
        {
            Logger.InitLogger();
            if (Id == null || Id <= 0)
            {
                Logger.Log.Error("GetWeighting ErrorCode " + ErrorCodes.InvalidItemId);
                return Request.CreateResponse(HttpStatusCode.BadRequest,
                    new ErrorResponse(ErrorCodes.InvalidItemId, "Provide correct itemId"), JsonFormatter);

            }
            try
            {
                Logger.Log.Info("GetWeighting Request Id=" + Id);
                using (var _db = new StudDBEntities())
                {
                    List<WEIGHTING> WeightingList = _db.WEIGHTING.Where(i => i.ID == Id).ToList();
                    if (WeightingList.Count > 0)
                    {
                        Logger.Log.Info("GetWeighting Response " + JsonConvert.SerializeObject(WeightingList));
                        return Request.CreateResponse(HttpStatusCode.OK, WeightingList.FirstOrDefault(), JsonFormatter);

                    }
                    else
                    {
                        Logger.Log.Error("GetWeighting ErrorCode " + ErrorCodes.ItemsNotFound);
                        return Request.CreateResponse(HttpStatusCode.NotFound,
                            new ErrorResponse(ErrorCodes.ItemsNotFound, "No item found against itemId"), JsonFormatter);

                    }
                }
            }
            catch (Exception e)
            {
                Logger.Log.Error("Exception GetWeighting" + e.Message);
                return Request.CreateResponse(HttpStatusCode.InternalServerError, e, JsonFormatter);

            }
        }

        public HttpResponseMessage PostWeighting(WEIGHTING weighting)
        {
            Logger.InitLogger();
            if ((weighting == null) || (JsonHelper.IsAnyNull(weighting)))
            {
                Logger.Log.Error("PostWeighting ErrorCode " + ErrorCodes.InvalidItemModel);
                return Request.CreateResponse(HttpStatusCode.BadRequest,
                    new ErrorResponse(ErrorCodes.InvalidItemModel, "Provide correct Item"), JsonFormatter);

            }
            try
            {
                Logger.Log.Info("PostWeighting Request " + JsonConvert.SerializeObject(weighting));
                using (var _db = new StudDBEntities())
                {
                    if (_db.WEIGHTING.Where(i => i.ID == weighting.ID).ToList().Count == 0)
                    {
                        _db.WEIGHTING.Add(weighting);
                        _db.SaveChanges();
                        Logger.Log.Info("PostWeighting Response " + JsonConvert.SerializeObject(weighting));
                        return Request.CreateResponse(HttpStatusCode.OK, weighting, JsonFormatter);

                    }
                    else
                    {
                        Logger.Log.Error("PostWeighting ErrorCode " + ErrorCodes.ItemAlreadyExists);
                        return Request.CreateResponse(HttpStatusCode.Conflict,
                            new ErrorResponse(ErrorCodes.ItemAlreadyExists, "Item with same ID already exists"),
                            JsonFormatter);

                    }
                }
            }
            catch (Exception e)
            {
                Logger.Log.Error("PostWeighting Exception " + e.Message);
                return Request.CreateResponse(HttpStatusCode.InternalServerError, e, JsonFormatter);
            }

        }

        public HttpResponseMessage PutWeighting(int? Id, WEIGHTING weighting)
        {
            Logger.InitLogger();
            if (Id == null || Id <= 0)
            {
                Logger.Log.Error("PutWeighting ErrorCode " + ErrorCodes.InvalidItemId);
                return Request.CreateResponse(HttpStatusCode.BadRequest,
                    new ErrorResponse(ErrorCodes.InvalidItemId, "Provide correct itemId"), JsonFormatter);
            }
            if ((weighting == null) || (JsonHelper.IsAnyNull(weighting)))
            {
                Logger.Log.Error("PutWeighting ErrorCode " + ErrorCodes.InvalidItemModel);
                return Request.CreateResponse(HttpStatusCode.BadRequest,
                    new ErrorResponse(ErrorCodes.InvalidItemModel, "Provide correct Item"), JsonFormatter);
            }
            try
            {
                Logger.Log.Info("PutWeighting Request " + Id + " " + JsonConvert.SerializeObject(weighting));
                using (var _db = new StudDBEntities())
                {
                    if (_db.WEIGHTING.Where(i => i.ID == weighting.ID).ToList().Count > 0)
                    {
                        _db.WEIGHTING.Remove(_db.WEIGHTING.Where(i => i.ID == Id).FirstOrDefault());
                        _db.WEIGHTING.Add(weighting);
                        _db.SaveChanges();
                        Logger.Log.Info("PutWeighting Response " + JsonConvert.SerializeObject(weighting));
                        return Request.CreateResponse(HttpStatusCode.OK, weighting, JsonFormatter);
                    }
                    else
                    {
                        Logger.Log.Error("PutWeighting ErrorCode " + ErrorCodes.ItemsNotFound);
                        return Request.CreateResponse(HttpStatusCode.Conflict,
                            new ErrorResponse(ErrorCodes.ItemsNotFound, "Can't find item "),
                            JsonFormatter);
                    }
                }
            }
            catch (Exception e)
            {
                Logger.Log.Error("PutWeighting Exception " + e.Message);
                return Request.CreateResponse(HttpStatusCode.InternalServerError, e, JsonFormatter);
            }


        }
    }
}
