﻿using Backend.Extensions;
using Backend.Models.ApiModel;
using Backend.Resources;
using Backend.Services;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web;
using System.Web.Http;
using System.Web.Http.Description;
using static Backend.Extensions.Enum;

namespace Backend.Controllers
{
    public class DealController : ApiController
    {
        DealService _dealService = new DealService();
        NoteService _noteService = new NoteService();

        [HttpGet]
        [Route("deals")]
        [ResponseType(typeof(DealListApiModel))]
        public HttpResponseMessage Get([FromUri] int currentPage = 1, [FromUri] int pageSize = 0, [FromUri] string query = "")
        {
            var response = new HttpResponseMessage();
            ResponseFormat responseData = new ResponseFormat();
            AuthorizationService _authorizationService = new AuthorizationService().SetPerm((int)EnumPermissions.DEAL_VIEW_LIST);

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
                    var isAuthorized = _authorizationService.Authorize(userId);
                    if (isAuthorized)
                    {
                        response.StatusCode = HttpStatusCode.OK;
                        responseData = ResponseFormat.Success;
                        var deals = _dealService.GetDealList(query, pageSize, currentPage);
                        Pager pageInfo;
                        if (deals.Count > 0)
                        {
                            if (pageSize == 0)
                            {
                                pageInfo = new Pager(deals.Count(), currentPage, deals.Count());
                            }
                            else
                            {
                                pageInfo = new Pager(deals.Count(), currentPage, pageSize);
                            }
                            responseData.data = new DealListApiModel()
                            {
                                deals = deals,
                                pageInfo = pageInfo
                            };
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
        [Route("deals")]
        [ResponseType(typeof(ResponseFormat))]
        public HttpResponseMessage Create(DealCreateApiModel apiModel)
        {
            var response = new HttpResponseMessage();
            ResponseFormat responseData = new ResponseFormat();
            AuthorizationService _authorizationService = new AuthorizationService().SetPerm((int)EnumPermissions.DEAL_CREATE);
            //read jwt

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
                    var userId = payload["id"];

                    var isAuthorized = _authorizationService.Authorize(Convert.ToInt32(userId));
                    if (isAuthorized)
                    {
                        var isCreated = _dealService.Create(apiModel, Convert.ToInt32(userId));
                        if (isCreated)
                        {
                            response.StatusCode = HttpStatusCode.OK;
                            responseData = ResponseFormat.Success;
                            responseData.message = SuccessMessages.DEAL_CREATED;
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
        [Route("deals/{id}")]
        [ResponseType(typeof(ResponseFormat))]
        public HttpResponseMessage Update([FromUri] int id, [FromBody] DealCreateApiModel apiModel)
        {
            var response = new HttpResponseMessage();
            ResponseFormat responseData = new ResponseFormat();
            //AuthorizationService _authorizationService = new AuthorizationService().SetPerm((int)EnumPermissions.LEAD_MODIFY);
            //read jwt

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
                    var owner = _dealService.FindOwnerId(id);

                    if ((userId == owner) || (new AuthorizationService().SetPerm((int)EnumPermissions.DEAL_DELETE).Authorize(userId)))
                    {
                        var isUpdated = _dealService.Update(id, apiModel, Convert.ToInt32(userId));
                        if (isUpdated)
                        {
                            response.StatusCode = HttpStatusCode.OK;
                            responseData = ResponseFormat.Success;
                            responseData.message = SuccessMessages.DEAL_MODIFIED;
                        }
                        else
                        {
                            response.StatusCode = HttpStatusCode.InternalServerError;
                            responseData = ResponseFormat.Fail;
                            responseData.message = ErrorMessages.SOMETHING_WRONG;
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

        [HttpDelete]
        [Route("deals/{id}")]
        [ResponseType(typeof(ResponseFormat))]
        public HttpResponseMessage Delete([FromUri] int id)
        {
            var response = new HttpResponseMessage();
            ResponseFormat responseData = new ResponseFormat();
            AuthorizationService _authorizationService = new AuthorizationService().SetPerm((int)EnumPermissions.DEAL_DELETE);
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
                    var userId = payload["id"];

                    var isAuthorized = _authorizationService.Authorize(Convert.ToInt32(userId));
                    if (isAuthorized)
                    {
                        var isDeleted = _dealService.Delete(id);
                        if (isDeleted)
                        {
                            response.StatusCode = HttpStatusCode.OK;
                            responseData = ResponseFormat.Success;
                            responseData.message = SuccessMessages.DEAL_DELETED;
                        }
                        else
                        {
                            response.StatusCode = HttpStatusCode.InternalServerError;
                            responseData = ResponseFormat.Fail;
                            responseData.message = ErrorMessages.SOMETHING_WRONG;
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
        [Route("deals/{id}/notes")]
        [ResponseType(typeof(ResponseFormat))]
        public HttpResponseMessage CreateNote([FromUri] int id)
        {
            var response = new HttpResponseMessage();
            ResponseFormat responseData = new ResponseFormat();
            IEnumerable<string> headerValues;
            if (Request.Headers.TryGetValues("Authorization", out headerValues))
            {
                string jwt = headerValues.FirstOrDefault();
                AuthorizationService _authorizationService = new AuthorizationService().SetPerm((int)EnumPermissions.NOTE_CREATE);
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
                    var isAuthorized = _authorizationService.Authorize(userId);
                    if (isAuthorized)
                    {
                        string noteBody = HttpContext.Current.Request.Form["body"];
                        if (!string.IsNullOrEmpty(noteBody))
                        {
                            //create a note
                            NoteApiModel apiModel = new NoteApiModel();
                            apiModel.body = noteBody;
                            apiModel.createdBy = new UserLinkApiModel() { id = userId };
                            apiModel.deal = id;
                            var createdNote = _noteService.Create(apiModel);

                            //create files and link them to note
                            if (HttpContext.Current.Request.Files.Count > 0)
                            {
                                var allFiles = HttpContext.Current.Request.Files;
                                foreach (string fileName in allFiles)
                                {
                                    HttpPostedFile uploadedFile = allFiles[fileName];
                                    FileManager.File file = new FileManager.File(uploadedFile);
                                    _noteService.AddFile(createdNote, file);

                                }
                            }
                            response.StatusCode = HttpStatusCode.OK;
                            responseData = ResponseFormat.Success;
                            responseData.message = SuccessMessages.NOTE_ADDED;
                        }
                        else
                        {
                            response.StatusCode = HttpStatusCode.BadRequest;
                            responseData = ResponseFormat.Fail;
                            responseData.message = ErrorMessages.NOTE_EMPTY;
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

        [HttpDelete]
        [Route("deals/{id}/notes/{noteId}")]
        [ResponseType(typeof(ResponseFormat))]
        public HttpResponseMessage DeleteNote([FromUri] int id, [FromUri] int noteId)
        {
            var response = new HttpResponseMessage();
            ResponseFormat responseData = new ResponseFormat();
            //AuthorizationService _authorizationService = new AuthorizationService().SetPerm((int)EnumPermissions.LEAD_DELETE);
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
                    var owner = _dealService.FindOwnerId(id);
                    var noteOwner = _noteService.FindOwner(noteId);
                    if ((userId == owner) || (userId == noteOwner) || new AuthorizationService().SetPerm((int)EnumPermissions.NOTE_DELETE_ANY).Authorize(userId))
                    {
                        var isDeleted = _noteService.Delete(noteId);
                        if (isDeleted)
                        {
                            response.StatusCode = HttpStatusCode.OK;
                            responseData = ResponseFormat.Success;
                            responseData.message = SuccessMessages.NOTE_DELETED;
                        }
                        else
                        {
                            response.StatusCode = HttpStatusCode.InternalServerError;
                            responseData = ResponseFormat.Fail;
                            responseData.message = ErrorMessages.SOMETHING_WRONG;
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
        [Route("deals/{id}/tags")]
        [ResponseType(typeof(ResponseFormat))]
        public HttpResponseMessage AddTag([FromUri] int id, [FromBody] TagCreateApiModel tag)
        {
            var response = new HttpResponseMessage();
            ResponseFormat responseData = new ResponseFormat();
            //AuthorizationService _authorizationService = new AuthorizationService().SetPerm((int)EnumPermissions.LEAD_MODIFY);
            //read jwt

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
                    var owner = _dealService.FindOwnerId(id);
                    if ((userId == owner) || (new AuthorizationService().SetPerm((int)EnumPermissions.DEAL_DELETE).Authorize(userId)))
                    {
                        //check if a tag exist

                        //if it is, create a tag item with current lead

                        // else create a new tag and a new tag item
                        var isAdded = _dealService.AddTag(id, tag.name);
                        if (isAdded)
                        {
                            response.StatusCode = HttpStatusCode.OK;
                            responseData = ResponseFormat.Success;
                            responseData.message = SuccessMessages.TAG_ADDED;
                        }
                        else
                        {
                            response.StatusCode = HttpStatusCode.InternalServerError;
                            responseData = ResponseFormat.Fail;
                            responseData.message = ErrorMessages.SOMETHING_WRONG;
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

        [HttpDelete]
        [Route("deals/{id}/tags/{tagId}")]
        [ResponseType(typeof(ResponseFormat))]
        public HttpResponseMessage DeleteTag([FromUri] int id, [FromUri] int tagId)
        {
            var response = new HttpResponseMessage();
            ResponseFormat responseData = new ResponseFormat();
            //AuthorizationService _authorizationService = new AuthorizationService().SetPerm((int)EnumPermissions.LEAD_MODIFY);
            //read jwt

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
                    var owner = _dealService.FindOwnerId(id);
                    if ((userId == owner) || (new AuthorizationService().SetPerm((int)EnumPermissions.DEAL_DELETE).Authorize(userId)))
                    {

                        var isRemoved = _dealService.RemoveTag(id, tagId);
                        if (isRemoved)
                        {
                            response.StatusCode = HttpStatusCode.OK;
                            responseData = ResponseFormat.Success;
                            responseData.message = SuccessMessages.TAG_REMOVED;
                        }
                        else
                        {
                            response.StatusCode = HttpStatusCode.InternalServerError;
                            responseData = ResponseFormat.Fail;
                            responseData.message = ErrorMessages.SOMETHING_WRONG;
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

        [HttpGet]
        [Route("deals/{id}")]
        [ResponseType(typeof(DealDetailApiModel))]
        public HttpResponseMessage Detail([FromUri] int id)
        {
            var response = new HttpResponseMessage();
            ResponseFormat responseData = new ResponseFormat();
            AuthorizationService _authorizationService = new AuthorizationService().SetPerm((int)EnumPermissions.DEAL_VIEW);
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
                    var userId = payload["id"];

                    var isAuthorized = _authorizationService.Authorize(Convert.ToInt32(userId));
                    if (isAuthorized)
                    {
                        response.StatusCode = HttpStatusCode.OK;
                        responseData = ResponseFormat.Success;
                        responseData.data = _dealService.GetOne(id);
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

        [HttpGet]
        [Route("contacts/prepare")]
        [ResponseType(typeof(DealBlankApiModel))]
        public HttpResponseMessage PrepareNewContact()
        {
            var response = new HttpResponseMessage();
            ResponseFormat responseData = new ResponseFormat();
            AuthorizationService _authorizationService = new AuthorizationService().SetPerm((int)EnumPermissions.DEAL_CREATE);

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
                    var isAuthorized = _authorizationService.Authorize(userId);
                    if (isAuthorized)
                    {
                        response.StatusCode = HttpStatusCode.OK;
                        responseData = ResponseFormat.Success;
                        responseData.data = _dealService.PrepareNewDeal();
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
    }
}
