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
using System.Web.Http.Description;
using System.Xml.Linq;
using Newtonsoft.Json;
using stud.webapi.Models;
using Swashbuckle.Swagger;
using Swashbuckle.Swagger.Annotations;

namespace stud.webapi.Controllers
{

    public class WeightingController : ApiController
    {
        protected JsonMediaTypeFormatter JsonFormatter = JsonHelper.GetJsonformatter();

        /// <summary>
        /// Метод возвращает все взвешивания.
        /// </summary>
        /// <returns>Возвращает либо все взвешивания с кодом 200, либо внутренную ошибку сервера с кодом 500</returns>
        /// <response code="200">Объект найден</response>
        /// <response code="500">Внуренняя ошибка сервера</response>
        [ResponseType(typeof(List<WEIGHTING>))]
        [Route("weightings")]
        [HttpGet]
        public HttpResponseMessage GetWeightings()
        {
            Guid guid = Guid.NewGuid();
            Logger.InitLogger();
            Logger.Log.Info("GetWeightings Request " + guid);
            try
            {
                using (var db = new StudDBEntities())
                {
                    List<WEIGHTING> weightingList = db.WEIGHTING.ToList();
                    Logger.Log.Info("GetWeightings Response " + guid + " " + JsonConvert.SerializeObject(weightingList));
                    return Request.CreateResponse(HttpStatusCode.OK, weightingList, JsonFormatter);
                }
            }
            catch (Exception e)
            {
                Logger.Log.Error("Exception GetWeightings " + guid + " " + e.Message);
                return Request.CreateResponse(HttpStatusCode.InternalServerError, e, JsonFormatter);
            }
        }

        /// <summary>
        /// Метод возвращает взвешивание по его ID.
        /// </summary>
        /// <param name="id">Идентификатор взвешиваниz</param>
        /// <returns></returns>
        /// <response code="200">Объект найден</response>
        /// <response code="400">Некоректный ID</response>
        /// <response code="404">Объекта с такам ID нет</response>
        /// <response code="500">Внуренняя ошибка сервера</response>
        [Route("weightings/{id}")]
        [HttpGet]
        public HttpResponseMessage GetWeighting(int? id)
        {
            Guid guid = Guid.NewGuid();
            Logger.InitLogger();
            if (id == null || id <= 0)
            {
                Logger.Log.Error("GetWeighting " + guid + " ErrorCode " + ErrorCodes.InvalidItemId);
                return Request.CreateResponse(HttpStatusCode.BadRequest,
                    new ErrorResponse(ErrorCodes.InvalidItemId, "Provide correct itemId"), JsonFormatter);
            }
            try
            {
                Logger.Log.Info("GetWeighting "  + guid + " Request Id=" + id);
                using (var db = new StudDBEntities())
                {
                    List<WEIGHTING> weightingList = db.WEIGHTING.Where(i => i.ID == id).ToList();
                    if (weightingList.Count > 0)
                    {
                        Logger.Log.Info("GetWeighting " + guid + " Response " + JsonConvert.SerializeObject(weightingList));
                        return Request.CreateResponse(HttpStatusCode.OK, weightingList.FirstOrDefault(), JsonFormatter);
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

        /// <summary>
        /// Метод для вставки нового взвешивания
        /// </summary>
        /// <param name="weighting">Объект нового взвешивания</param>
        /// <returns></returns>
        /// <response code="200">Объект добавлен успешно</response>
        /// <response code="400">Передан некорекный объект взвешивания</response>
        /// <response code="409">Объекта с такам ID  уже есть</response>
        /// <response code="500">Внуренняя ошибка сервера</response>
        [Route("weightings")]
        [HttpPost]
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
                using (var db = new StudDBEntities())
                {
                    if (db.WEIGHTING.Where(i => i.ID == weighting.ID).ToList().Count == 0)
                    {
                        db.WEIGHTING.Add(weighting);
                        db.SaveChanges();
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

        /// <summary>
        /// Метод для обновления информации в уже произведенном взвешивании
        /// </summary>
        /// <param name="id">Идентификатор взвешивания, которое нужно обновить</param>
        /// <param name="weighting">Объект с обновлением о взвешивании</param>
        /// <returns></returns>
        /// <response code="200">Объект обновлен успешно</response>
        /// <response code="400">переданный id некоректен либо переданный объект для взвешивания некоректен</response>
        /// <response code="409">Объекта с такам ID  для обновления не найден</response>
        /// <response code="500">Внуренняя ошибка сервера</response>
        [Route("weightings/{id}")]
        [HttpPut]
        public HttpResponseMessage PutWeighting(int? id, WEIGHTING weighting)
        {
            Guid guid = Guid.NewGuid();
            Logger.InitLogger();
            if (id == null || id <= 0)
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
                Logger.Log.Info("PutWeighting " + guid + "Request " + id + " " + JsonConvert.SerializeObject(weighting));
                using (var db = new StudDBEntities())
                {
                    if (db.WEIGHTING.Where(i => i.ID == weighting.ID).ToList().Count > 0)
                    {
                        db.WEIGHTING.Remove(db.WEIGHTING.Where(i => i.ID == id).FirstOrDefault());
                        db.WEIGHTING.Add(weighting);
                        db.SaveChanges();
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
