using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Web.Http;
using WebApplication2.Models;

namespace WebApplication2.Controllers
{
    public class UsersController : ApiController
    {
        [Route("users")]
        [HttpPost]
        public HttpResponseMessage Register(UserInfoModels user)
        {
            try
            {
                if ((user == null) || (user.ID == null) || (user.ID < 0) || (user.Username == null) || (user.FirstName == null) || (user.LastName == null) || (user.Password == null))
                {
                    return Request.CreateResponse(HttpStatusCode.BadRequest, new ErrorResponse(ErrorCodes.InvalidUserInfoModel, "Provide correct UserInfoModels"), JsonFormatter);
                }
                else
                {
                    return Request.CreateResponse(HttpStatusCode.OK, JsonFormatter);
                }
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex, JsonFormatter);
            }
            

        }

        [Route("users")]
        [HttpPatch]
        public HttpResponseMessage ChangePassword (UserInfoModels user, string oldPassword, string newPassword, string repeatNewPassword)
        {
            try
            {
                if (user.Password != oldPassword)
                {
                    return Request.CreateResponse(HttpStatusCode.BadRequest, new ErrorResponse(ErrorCodes.ChangePasswordError, "The password you entered is incorrect"), JsonFormatter);
                }
                else
                {
                    if (newPassword != repeatNewPassword)
                    {
                        return Request.CreateResponse(HttpStatusCode.BadRequest, new ErrorResponse(ErrorCodes.ChangePasswordError, "Passwords do not match"), JsonFormatter);
                    }
                    else
                    {
                        user.Password = newPassword;
                        return Request.CreateResponse(HttpStatusCode.OK, JsonFormatter);
                    }
                }
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex, JsonFormatter);
            }
        }

        [Route("users")]
        [HttpPatch]
        public HttpResponseMessage Likes (List<LikesModels> likes, LikesModels newLike) // на вход подается список лайков и новый лайк
        {
            try
            {
                for (int i = 0; i < likes.Count; i++) // проверяем корректность списка
                {
                    if ((likes == null) || (likes[i].Time == null))
                    {
                        return Request.CreateResponse(HttpStatusCode.BadRequest, new ErrorResponse(ErrorCodes.InvalidUserID, "Provide correct LikesModel", i), JsonFormatter);
                    }

                    if ((likes[i].ID == null) || (likes[i].ID < 0))
                    {
                        return Request.CreateResponse(HttpStatusCode.BadRequest, new ErrorResponse(ErrorCodes.InvalidUserID, "Provide correct UserID", i), JsonFormatter);
                    }
                }

                if ((newLike == null) || (newLike.Time == null)) // проверяем корректность нового лайка
                {
                    return Request.CreateResponse(HttpStatusCode.BadRequest, new ErrorResponse(ErrorCodes.InvalidUserID, "Provide correct LikesModel"), JsonFormatter);
                }
                if ((newLike.ID == null) || (newLike.ID < 0))
                {
                    return Request.CreateResponse(HttpStatusCode.BadRequest, new ErrorResponse(ErrorCodes.InvalidUserID, "Provide correct UserID"), JsonFormatter);
                }

                likes.Add(newLike); // если ошибок нет, то добавляем новый лайк к списку
                return Request.CreateResponse(HttpStatusCode.OK, likes, JsonFormatter);
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex, JsonFormatter);
            }
            


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
