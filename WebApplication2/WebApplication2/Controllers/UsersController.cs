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

        [Route("users/{id}/password")]
        [HttpPut]
        public HttpResponseMessage ChangePassword (ChangePasswordModels changePassword)
        {
            try
            {
                UserInfoModels user = GetDataController.GetUser(changePassword.id); // метод, который ищет пользователя по id
                if (user.Password != changePassword.oldPassword)
                {
                    return Request.CreateResponse(HttpStatusCode.BadRequest, new ErrorResponse(ErrorCodes.ChangePasswordError, "The password you entered is incorrect"), JsonFormatter);
                }
                else
                {
                    if (changePassword.newPassword != changePassword.repeatNewPassword)
                    {
                        return Request.CreateResponse(HttpStatusCode.BadRequest, new ErrorResponse(ErrorCodes.ChangePasswordError, "Passwords do not match"), JsonFormatter);
                    }
                    else
                    {
                        user.Password = changePassword.newPassword;
                        return Request.CreateResponse(HttpStatusCode.OK, changePassword.newPassword, JsonFormatter);
                    }
                }
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex, JsonFormatter);
            }
        }

        [Route("posts/{id}/likes")]
        [HttpPost]
        public HttpResponseMessage AddLike (LikesModels like) 
        {
            try
            {
                PostsModels post = GetDataController.GetPost(like.PostID); // метод, который ищет пост по id
                if (post == null)
                {
                    return Request.CreateResponse(HttpStatusCode.BadRequest, new ErrorResponse(ErrorCodes.InvalidPost, "Post is invalid"), JsonFormatter);
                }
                if ((like.UserID == null) || (like.UserID < 0))
                {
                    return Request.CreateResponse(HttpStatusCode.BadRequest, new ErrorResponse(ErrorCodes.InvalidUserID, "UserID is invald"), JsonFormatter);
                }
                if ((like.PostID == null) || (like.PostID < 0))
                {
                    return Request.CreateResponse(HttpStatusCode.BadRequest, new ErrorResponse(ErrorCodes.InvalidPostID, "PostID is invald"), JsonFormatter);
                }
                post.Likes.Add(like);
                return Request.CreateResponse(HttpStatusCode.OK, post, JsonFormatter);
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
