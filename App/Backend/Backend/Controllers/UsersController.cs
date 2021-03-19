using Backend.Domain;
using Backend.Extensions;
using Backend.Models;
using Backend.Models.ApiModel;
using Backend.Models.SwaggerModel;
using Backend.Models.SwaggerModel.Users;
using Backend.Resources;
using Backend.Services;
using Backend.Validators;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web;
using System.Web.Http;
using System.Web.Http.Cors;
using System.Web.Http.Description;
using static Backend.Extensions.Enum;

namespace Backend.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class UsersController : ApiController
    {
        public DatabaseContext db = new DatabaseContext();
        public UserService _userService = new UserService();
        public UserValidator _userValidator = new UserValidator();
        public HashManager _hashManager = new HashManager();

        [HttpGet]
        [Route("users")]
        [ResponseType(typeof(Swagger_User_List))]
        public HttpResponseMessage Get([FromUri] int currentPage = 0, [FromUri] int pageSize = 0)
        {
            var response = new HttpResponseMessage();
            ResponseFormat responseData = new ResponseFormat();
            AuthorizationService _authorizationService = new AuthorizationService().SetPerm((int)EnumPermissions.USER_VIEW_LIST);
            //read jwt
            IEnumerable<string> headerValues = Request.Headers.GetValues("Authorization");
            if (headerValues == null)
            {
                response.StatusCode = HttpStatusCode.Forbidden;
                responseData = ResponseFormat.Fail;
                responseData.message = ErrorMessages.UNAUTHORIZED;
            }
            else
            {
                string jwt = headerValues.FirstOrDefault();
                //validate jwt
                var payload = JwtTokenManager.ValidateJwtToken(jwt);

                if (payload.ContainsKey("error"))
                {
                    if ((string)payload["error"] == ErrorMessages.TOKEN_EXPIRED)
                    {
                        response.StatusCode = HttpStatusCode.Forbidden;
                        responseData = ResponseFormat.Fail;
                        responseData.message = ErrorMessages.TOKEN_EXPIRED;
                    }
                    if ((string)payload["error"] == ErrorMessages.TOKEN_INVALID)
                    {
                        response.StatusCode = HttpStatusCode.Forbidden;
                        responseData = ResponseFormat.Fail;
                        responseData.message = ErrorMessages.TOKEN_INVALID;
                    }
                }
                else
                {
                    var userId = payload["id"];

                    var isAuthorized = _authorizationService.Authorize(Convert.ToInt32(userId));
                    if (isAuthorized)
                    {
                        response.StatusCode = HttpStatusCode.OK;
                        responseData = ResponseFormat.Success;
                        (List<USER> userList, Pager p) = _userService.GetAll(currentPage, pageSize);

                        responseData.data = new
                        {
                            users = userList.Select(c => new { id = c.ID, firstName = c.FirstName, lastName = c.LastName, phone = c.Phone, email = c.Email, skype = c.Skype }),
                            pageInfo = new { totalPage = p.TotalPages, pageSize = p.PageSize, startIndex = p.StartIndex, endIndex = p.EndIndex, currentPage = p.CurrentPage }
                        };
                    }
                    else
                    {
                        response.StatusCode = HttpStatusCode.Forbidden;
                        responseData = ResponseFormat.Fail;
                        responseData.message = ErrorMessages.UNAUTHORIZED;
                    }
                }
            }

            var json = JsonConvert.SerializeObject(responseData);
            response.Content = new StringContent(json, Encoding.UTF8, "application/json");
            return response;
        }

        [HttpDelete]
        [Route("users/{id}")]
        [ResponseType(typeof(ResponseModel))]
        public HttpResponseMessage Delete([FromUri] int id)
        {
            var response = new HttpResponseMessage();
            ResponseFormat responseData = new ResponseFormat();
            AuthorizationService _authorizationService = new AuthorizationService().SetPerm((int)EnumPermissions.USER_DELETE);
            //read jwt
            IEnumerable<string> headerValues = Request.Headers.GetValues("Authorization");
            if (headerValues == null)
            {
                response.StatusCode = HttpStatusCode.Forbidden;
                responseData = ResponseFormat.Fail;
                responseData.message = ErrorMessages.UNAUTHORIZED;
            }
            else
            {
                string jwt = headerValues.FirstOrDefault();
                //validate jwt
                var payload = JwtTokenManager.ValidateJwtToken(jwt);

                if (payload.ContainsKey("error"))
                {
                    if ((string)payload["error"] == ErrorMessages.TOKEN_EXPIRED)
                    {
                        response.StatusCode = HttpStatusCode.Forbidden;
                        responseData = ResponseFormat.Fail;
                        responseData.message = ErrorMessages.TOKEN_EXPIRED;
                    }
                    if ((string)payload["error"] == ErrorMessages.TOKEN_INVALID)
                    {
                        response.StatusCode = HttpStatusCode.Forbidden;
                        responseData = ResponseFormat.Fail;
                        responseData.message = ErrorMessages.TOKEN_INVALID;
                    }
                }
                else
                {
                    var userId = payload["id"];

                    var isAuthorized = _authorizationService.Authorize(Convert.ToInt32(userId));
                    if (isAuthorized)
                    {
                        var dbUser = db.USERs.Find(id);
                        if (dbUser != null)
                        {
                            db.USERs.Remove(dbUser);
                            db.SaveChanges();
                            response.StatusCode = HttpStatusCode.OK;
                            responseData = ResponseFormat.Success;
                            responseData.message = SuccessMessages.USER_DELETED;
                        }
                        else
                        {
                            response.StatusCode = HttpStatusCode.Gone;
                            responseData = ResponseFormat.Fail;
                            responseData.message = ErrorMessages.USER_NOT_FOUND;
                        }
                    }
                    else
                    {
                        response.StatusCode = HttpStatusCode.Forbidden;
                        responseData = ResponseFormat.Fail;
                        responseData.message = ErrorMessages.UNAUTHORIZED;
                    }
                }
            }

            var json = JsonConvert.SerializeObject(responseData);
            response.Content = new StringContent(json, Encoding.UTF8, "application/json");
            return response;
        }

        [HttpPost]
        [Route("users")]
        [ResponseType(typeof(ResponseModel))]
        public HttpResponseMessage Create([FromBody] User user)
        {
            //validate
            var response = new HttpResponseMessage();
            ResponseFormat responseData = new ResponseFormat();
            AuthorizationService _authorizationService = new AuthorizationService().SetPerm((int)EnumPermissions.USER_CREATE);

            IEnumerable<string> headerValues = Request.Headers.GetValues("Authorization");
            if (headerValues == null)
            {
                response.StatusCode = HttpStatusCode.Forbidden;
                responseData = ResponseFormat.Fail;
                responseData.message = ErrorMessages.UNAUTHORIZED;
            }
            else
            {
                string jwt = headerValues.FirstOrDefault();
                //validate jwt
                var payload = JwtTokenManager.ValidateJwtToken(jwt);
                if (payload.ContainsKey("error"))
                {
                    if ((string)payload["error"] == ErrorMessages.TOKEN_EXPIRED)
                    {
                        response.StatusCode = HttpStatusCode.Forbidden;
                        responseData = ResponseFormat.Fail;
                        responseData.message = ErrorMessages.TOKEN_EXPIRED;
                    }
                    if ((string)payload["error"] == ErrorMessages.TOKEN_INVALID)
                    {
                        response.StatusCode = HttpStatusCode.Forbidden;
                        responseData = ResponseFormat.Fail;
                        responseData.message = ErrorMessages.TOKEN_INVALID;
                    }
                }
                else
                {
                    var userId = payload["id"];

                    var isAuthorized = _authorizationService.Authorize(Convert.ToInt32(userId));
                    if (isAuthorized)
                    {
                        var validator = _userValidator.Validate(user);
                        if (validator.IsValid)
                        {
                            var dbUser = _userService.Create(user, Convert.ToInt32(userId));
                            response.StatusCode = HttpStatusCode.OK;
                            responseData = ResponseFormat.Success;
                            responseData.message = SuccessMessages.USER_CREATED;
                        }
                        else
                        {
                            response.StatusCode = HttpStatusCode.BadRequest;
                            responseData = ResponseFormat.Fail;
                            var errorString = "Details";
                            foreach (var error in validator.Errors)
                            {
                                errorString += " - " + error.ErrorMessage;
                            }
                            responseData.message = errorString;
                        }
                    }
                    else
                    {
                        response.StatusCode = HttpStatusCode.Forbidden;
                        responseData = ResponseFormat.Fail;
                        responseData.message = ErrorMessages.UNAUTHORIZED;
                    }
                }
            }

            var json = JsonConvert.SerializeObject(responseData);
            response.Content = new StringContent(json, Encoding.UTF8, "application/json");
            return response;
        }

        [HttpPost]
        [Route("users/{id}/change_password")]
        [ResponseType(typeof(ResponseModel))]
        public HttpResponseMessage ChangePassword([FromUri] int id, [FromBody] ChangePasswordApiModel changePasswordModel)
        {
            var response = new HttpResponseMessage();
            ResponseFormat responseData = new ResponseFormat();
            //AuthorizationService _authorizationService = new AuthorizationService().SetPerm((int)EnumPermissions.USER_MODIFY_SELF);
            //read jwt
            IEnumerable<string> headerValues = Request.Headers.GetValues("Authorization");
            if (headerValues == null)
            {
                response.StatusCode = HttpStatusCode.Forbidden;
                responseData = ResponseFormat.Fail;
                responseData.message = ErrorMessages.UNAUTHORIZED;
            }
            else
            {
                string jwt = headerValues.FirstOrDefault();
                //validate jwt
                var payload = JwtTokenManager.ValidateJwtToken(jwt);

                if (payload.ContainsKey("error"))
                {
                    if ((string)payload["error"] == ErrorMessages.TOKEN_EXPIRED)
                    {
                        response.StatusCode = HttpStatusCode.Forbidden;
                        responseData = ResponseFormat.Fail;
                        responseData.message = ErrorMessages.TOKEN_EXPIRED;
                    }
                    if ((string)payload["error"] == ErrorMessages.TOKEN_INVALID)
                    {
                        response.StatusCode = HttpStatusCode.Forbidden;
                        responseData = ResponseFormat.Fail;
                        responseData.message = ErrorMessages.TOKEN_INVALID;
                    }
                }
                else
                {
                    var userId = Convert.ToInt32(payload["id"]);
                    if (userId == id)
                    {
                        var isAuthorized = new AuthorizationService().SetPerm((int)EnumPermissions.USER_MODIFY_SELF).Authorize(userId);
                        if (isAuthorized)
                        {
                            //require old and new password
                            //verify oldPassword agaisnt the database
                            var dbUser = db.USERs.Find(id);
                            if (dbUser != null)
                            {
                                var validate = _userService.ValidatePassword(dbUser.Email, changePasswordModel.oldPassword);
                                if (validate.Item1 == true)
                                {
                                    //set new password
                                    dbUser.Hash = _hashManager.Hash(changePasswordModel.newPassword);
                                    db.SaveChanges();
                                    response.StatusCode = HttpStatusCode.OK;
                                    responseData = ResponseFormat.Success;
                                    responseData.message = SuccessMessages.PASSWORD_CHANGED;
                                }
                                else
                                {
                                    response.StatusCode = HttpStatusCode.Unauthorized;
                                    responseData = ResponseFormat.Fail;
                                    responseData.message = validate.Item2;
                                }
                            }
                            else
                            {
                                response.StatusCode = HttpStatusCode.Gone;
                                responseData = ResponseFormat.Fail;
                                responseData.message = ErrorMessages.USER_NOT_FOUND;
                            }
                        }
                        else
                        {
                            response.StatusCode = HttpStatusCode.Forbidden;
                            responseData = ResponseFormat.Fail;
                            responseData.message = ErrorMessages.UNAUTHORIZED;
                        }
                    }
                    else
                    {
                        var isAuthorized = new AuthorizationService().SetPerm((int)EnumPermissions.USER_MODIFY).Authorize(userId);
                        if (isAuthorized)
                        {
                            //require new password
                            var dbUser = db.USERs.Find(id);
                            if (dbUser != null)
                            {
                                dbUser.Hash = _hashManager.Hash(changePasswordModel.newPassword);
                                db.SaveChanges();
                                response.StatusCode = HttpStatusCode.OK;
                                responseData = ResponseFormat.Success;
                                responseData.message = SuccessMessages.PASSWORD_CHANGED;
                            }
                            else
                            {
                                response.StatusCode = HttpStatusCode.Gone;
                                responseData = ResponseFormat.Fail;
                                responseData.message = ErrorMessages.USER_NOT_FOUND;
                            }
                        }
                        else
                        {
                            response.StatusCode = HttpStatusCode.Forbidden;
                            responseData = ResponseFormat.Fail;
                            responseData.message = ErrorMessages.UNAUTHORIZED;
                        }
                    }
                }
            }

            var json = JsonConvert.SerializeObject(responseData);
            response.Content = new StringContent(json, Encoding.UTF8, "application/json");
            return response;
        }

        [HttpGet]
        [Route("users/{id}")]
        [ResponseType(typeof(UserApiModel))]
        public HttpResponseMessage Detail([FromUri] int id)
        {
            var response = new HttpResponseMessage();
            ResponseFormat responseData = new ResponseFormat();
            //2 routes for permissions
            //AuthorizationService _authorizationService = new AuthorizationService().SetPerm((int)EnumPermissions.USER_VIEW);

            IEnumerable<string> headerValues;
            if (Request.Headers.TryGetValues("Authorization", out headerValues))
            {
                string jwt = headerValues.FirstOrDefault();
                //validate jwt
                var payload = JwtTokenManager.ValidateJwtToken(jwt);

                if (payload.ContainsKey("error"))
                {
                    if ((string)payload["error"] == ErrorMessages.TOKEN_EXPIRED)
                    {
                        response.StatusCode = HttpStatusCode.Forbidden;
                        responseData = ResponseFormat.Fail;
                        responseData.message = ErrorMessages.TOKEN_EXPIRED;
                    }
                    if ((string)payload["error"] == ErrorMessages.TOKEN_INVALID)
                    {
                        response.StatusCode = HttpStatusCode.Forbidden;
                        responseData = ResponseFormat.Fail;
                        responseData.message = ErrorMessages.TOKEN_INVALID;
                    }
                }
                else
                {
                    var userId = Convert.ToInt32(payload["id"]);
                    if ((id == userId && new AuthorizationService().SetPerm((int)EnumPermissions.USER_MODIFY_SELF).Authorize(userId)) || (id != userId && new AuthorizationService().SetPerm((int)EnumPermissions.USER_VIEW).Authorize(userId)))
                    {
                        var dbUser = db.USERs.Find(id);
                        //get avatar
                        string targetFolder = HttpContext.Current.Server.MapPath("~/Uploads");
                        var mime = MimeMapping.MimeUtility.GetMimeMapping(dbUser.Avatar);
                        var img = Convert.ToBase64String(System.IO.File.ReadAllBytes(Path.Combine(targetFolder, dbUser.Avatar)));

                        UserDetailApiModel apiModel = new UserDetailApiModel();
                        apiModel.avatar = $"data:{mime};base64,{img}";
                        apiModel.username = dbUser.Username;
                        apiModel.lastName = dbUser.LastName;
                        apiModel.firstName = dbUser.FirstName;
                        apiModel.createdAt = dbUser.CreatedAt.GetValueOrDefault();
                        apiModel.createdById = dbUser.CreatedBy.GetValueOrDefault();
                        apiModel.createdByName = dbUser.CreatedUser.Username;
                        apiModel.email = dbUser.Email;
                        apiModel.skype = dbUser.Skype;
                        apiModel.phone = dbUser.Phone;
                        apiModel.groups = db.GROUPs.Select(c => new UserDetailApiModel.G { groupId = c.ID, groupName = c.Name, selected = dbUser.GROUP_ID == c.ID }).ToList();
                        response.StatusCode = HttpStatusCode.OK;
                        responseData = ResponseFormat.Success;
                        responseData.data = apiModel;
                    }
                    else
                    {
                        response.StatusCode = HttpStatusCode.Forbidden;
                        responseData = ResponseFormat.Fail;
                        responseData.message = ErrorMessages.UNAUTHORIZED;
                    }
                }
            }
            else
            {
                response.StatusCode = HttpStatusCode.Forbidden;
                responseData = ResponseFormat.Fail;
                responseData.message = ErrorMessages.UNAUTHORIZED;
            }
            var json = JsonConvert.SerializeObject(responseData);
            response.Content = new StringContent(json, Encoding.UTF8, "application/json");
            return response;
        }

        [HttpPost]
        [Route("users/{id}")]
        public HttpResponseMessage Update([FromUri] int id, [FromBody] User user)
        {
            var response = new HttpResponseMessage();
            ResponseFormat responseData = new ResponseFormat();
            IEnumerable<string> headerValues = Request.Headers.GetValues("Authorization");

            if (headerValues == null)
            {
                response.StatusCode = HttpStatusCode.Forbidden;
                responseData = ResponseFormat.Fail;
                responseData.message = ErrorMessages.UNAUTHORIZED;
            }
            else
            {
                string jwt = headerValues.FirstOrDefault();
                //validate jwt
                var payload = JwtTokenManager.ValidateJwtToken(jwt);

                if (payload.ContainsKey("error"))
                {
                    if ((string)payload["error"] == ErrorMessages.TOKEN_EXPIRED)
                    {
                        response.StatusCode = HttpStatusCode.Forbidden;
                        responseData = ResponseFormat.Fail;
                        responseData.message = ErrorMessages.TOKEN_EXPIRED;
                    }
                    if ((string)payload["error"] == ErrorMessages.TOKEN_INVALID)
                    {
                        response.StatusCode = HttpStatusCode.Forbidden;
                        responseData = ResponseFormat.Fail;
                        responseData.message = ErrorMessages.TOKEN_INVALID;
                    }
                }
                else
                {
                    var userId = Convert.ToInt32(payload["id"]);
                    if (userId == id)
                    {
                        var isAuthorized = new AuthorizationService().SetPerm((int)EnumPermissions.USER_MODIFY_SELF).Authorize(userId);
                        if (isAuthorized)
                        {
                            var dbUser = db.USERs.Find(id);
                            dbUser.FirstName = user.FirstName;
                            dbUser.LastName = user.LastName;
                            dbUser.Username = user.Username;
                            dbUser.Phone = user.Phone;
                            dbUser.Skype = user.Skype;
                            db.SaveChanges();
                            response.StatusCode = HttpStatusCode.OK;
                            responseData = ResponseFormat.Success;
                            responseData.message = SuccessMessages.USER_MODIFIED;
                        }
                        else
                        {
                            response.StatusCode = HttpStatusCode.Forbidden;
                            responseData = ResponseFormat.Fail;
                            responseData.message = ErrorMessages.UNAUTHORIZED;
                        }
                    }
                    else
                    {
                        var isAuthorized = new AuthorizationService().SetPerm((int)EnumPermissions.USER_MODIFY).Authorize(userId);
                        if (isAuthorized)
                        {
                            var dbUser = db.USERs.Find(id);
                            dbUser.FirstName = user.FirstName;
                            dbUser.LastName = user.LastName;
                            dbUser.Username = user.Username;
                            dbUser.Phone = user.Phone;
                            dbUser.Skype = user.Skype;
                            dbUser.GROUP_ID = user.GROUP_ID;
                            db.SaveChanges();
                            response.StatusCode = HttpStatusCode.OK;
                            responseData = ResponseFormat.Success;
                            responseData.message = SuccessMessages.USER_MODIFIED;
                        }
                        else
                        {
                            response.StatusCode = HttpStatusCode.Forbidden;
                            responseData = ResponseFormat.Fail;
                            responseData.message = ErrorMessages.UNAUTHORIZED;
                        }
                    }
                }
            }
            var json = JsonConvert.SerializeObject(responseData);
            response.Content = new StringContent(json, Encoding.UTF8, "application/json");
            return response;
        }

        [HttpPost]
        [Route("users/{id}/change_avatar")]
        [ResponseType(typeof(ResponseModel))]
        public HttpResponseMessage ChangeAvatar([FromUri] int id)
        {
            var response = new HttpResponseMessage();
            ResponseFormat responseData = new ResponseFormat();
            //AuthorizationService _authorizationService = new AuthorizationService().SetPerm((int)EnumPermissions.USER_MODIFY_SELF);
            //read jwt
            IEnumerable<string> headerValues = Request.Headers.GetValues("Authorization");
            if (headerValues == null)
            {
                response.StatusCode = HttpStatusCode.Forbidden;
                responseData = ResponseFormat.Fail;
                responseData.message = ErrorMessages.UNAUTHORIZED;
            }
            else
            {
                string jwt = headerValues.FirstOrDefault();
                //validate jwt
                var payload = JwtTokenManager.ValidateJwtToken(jwt);

                if (payload.ContainsKey("error"))
                {
                    if ((string)payload["error"] == ErrorMessages.TOKEN_EXPIRED)
                    {
                        response.StatusCode = HttpStatusCode.Forbidden;
                        responseData = ResponseFormat.Fail;
                        responseData.message = ErrorMessages.TOKEN_EXPIRED;
                    }
                    if ((string)payload["error"] == ErrorMessages.TOKEN_INVALID)
                    {
                        response.StatusCode = HttpStatusCode.Forbidden;
                        responseData = ResponseFormat.Fail;
                        responseData.message = ErrorMessages.TOKEN_INVALID;
                    }
                }
                else
                {
                    var userId = Convert.ToInt32(payload["id"]);
                    if (userId == id)
                    {
                        var isAuthorized = new AuthorizationService().SetPerm((int)EnumPermissions.USER_MODIFY_SELF).Authorize(userId);
                        if (isAuthorized)
                        {
                            var dbUser = db.USERs.Find(id);
                            if (dbUser != null)
                            {
                                if (HttpContext.Current.Request.Files.Count > 0)
                                {
                                    var uploadedFile = HttpContext.Current.Request.Files[0];
                                    //file.
                                    FileManager.File file = new FileManager.File(uploadedFile);
                                    var isImage = file.FilterExtension(new List<string>() { ".jpeg", ".jpg", "png", ".tif", ".tiff" });
                                    if (isImage)
                                    {
                                        file.Rename();
                                        file.Save(HttpContext.Current.Server.MapPath("~/Uploads"));
                                        dbUser.Avatar = file.FullName;
                                        db.SaveChanges();
                                        response.StatusCode = HttpStatusCode.OK;
                                        responseData = ResponseFormat.Success;
                                        responseData.message = SuccessMessages.AVATAR_CHANGED;
                                    }
                                    else
                                    {
                                        response.StatusCode = HttpStatusCode.BadRequest;
                                        responseData = ResponseFormat.Fail;
                                        responseData.message = ErrorMessages.NOT_IMAGE;
                                    }
                                }
                            }
                            else
                            {
                                response.StatusCode = HttpStatusCode.Gone;
                                responseData = ResponseFormat.Fail;
                                responseData.message = ErrorMessages.USER_NOT_FOUND;
                            }
                        }
                        else
                        {
                            response.StatusCode = HttpStatusCode.Forbidden;
                            responseData = ResponseFormat.Fail;
                            responseData.message = ErrorMessages.UNAUTHORIZED;
                        }
                    }
                    else
                    {
                        var isAuthorized = new AuthorizationService().SetPerm((int)EnumPermissions.USER_MODIFY).Authorize(userId);
                        if (isAuthorized)
                        {
                            
                            var dbUser = db.USERs.Find(id);
                            if (dbUser != null)
                            {
                                if (HttpContext.Current.Request.Files.Count > 0)
                                {
                                    var uploadedFile = HttpContext.Current.Request.Files[0];
                                    //file.
                                    FileManager.File file = new FileManager.File(uploadedFile);
                                    var isImage = file.FilterExtension(new List<string>() { ".jpeg", ".jpg", "png", ".tif", ".tiff" });
                                    if (isImage)
                                    {
                                        file.Rename()
                                            .ReadPosted();
                                        file.Save(HttpContext.Current.Server.MapPath("~/Uploads"));
                                        dbUser.Avatar = file.FullName;
                                        db.SaveChanges();
                                        response.StatusCode = HttpStatusCode.OK;
                                        responseData = ResponseFormat.Success;
                                        responseData.message = SuccessMessages.AVATAR_CHANGED;
                                    }
                                    else
                                    {
                                        response.StatusCode = HttpStatusCode.BadRequest;
                                        responseData = ResponseFormat.Fail;
                                        responseData.message = ErrorMessages.NOT_IMAGE;
                                    }
                                }
                            }
                            else
                            {
                                response.StatusCode = HttpStatusCode.Gone;
                                responseData = ResponseFormat.Fail;
                                responseData.message = ErrorMessages.USER_NOT_FOUND;
                            }
                        }
                        else
                        {
                            response.StatusCode = HttpStatusCode.Forbidden;
                            responseData = ResponseFormat.Fail;
                            responseData.message = ErrorMessages.UNAUTHORIZED;
                        }
                    }
                }
            }

            var json = JsonConvert.SerializeObject(responseData);
            response.Content = new StringContent(json, Encoding.UTF8, "application/json");
            return response;
        }

    }
}
