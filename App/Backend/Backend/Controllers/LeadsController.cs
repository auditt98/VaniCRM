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
    public class LeadsController : ApiController
    {
        private LeadService _leadService = new LeadService();
        private NoteService _noteService = new NoteService();
        [HttpGet]
        [Route("leads")]
        [ResponseType(typeof(LeadListApiModel))]
        public HttpResponseMessage Get([FromUri] int currentPage = 1, [FromUri] int pageSize = 0, [FromUri] string query = "", [FromUri] string sort = "")
        {
            var response = new HttpResponseMessage();
            ResponseFormat responseData = new ResponseFormat();
            AuthorizationService _authorizationService = new AuthorizationService().SetPerm((int)EnumPermissions.LEAD_VIEW_LIST);

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
                        response.StatusCode = HttpStatusCode.Unauthorized;
                        responseData = ResponseFormat.Fail;
                        responseData.message = ErrorMessages.TOKEN_EXPIRED;
                    }
                    if ((string)payload["error"] == ErrorMessages.TOKEN_INVALID)
                    {
                        response.StatusCode = HttpStatusCode.Unauthorized;
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
                        var sortQ = new List<string>();
                        sortQ = sort.Split(',').ToList();
                        var leads = _leadService.GetLeadList(query, pageSize, currentPage, sortQ);
                        responseData.data = leads;
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
                response.StatusCode = HttpStatusCode.Unauthorized;
                responseData = ResponseFormat.Fail;
                responseData.message = ErrorMessages.UNAUTHORIZED;
            }
            var json = JsonConvert.SerializeObject(responseData);
            response.Content = new StringContent(json, Encoding.UTF8, "application/json");
            return response;
        }


        [HttpGet]
        [Route("leads/prepare")]
        [ResponseType(typeof(LeadBlankApiModel))]
        public HttpResponseMessage PrepareNewLead()
        {
            var response = new HttpResponseMessage();
            ResponseFormat responseData = new ResponseFormat();
            AuthorizationService _authorizationService = new AuthorizationService().SetPerm((int)EnumPermissions.LEAD_CREATE);

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
                        response.StatusCode = HttpStatusCode.Unauthorized;
                        responseData = ResponseFormat.Fail;
                        responseData.message = ErrorMessages.TOKEN_EXPIRED;
                    }
                    if ((string)payload["error"] == ErrorMessages.TOKEN_INVALID)
                    {
                        response.StatusCode = HttpStatusCode.Unauthorized;
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
                        responseData.data = _leadService.PrepareNewLead();
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
                response.StatusCode = HttpStatusCode.Unauthorized;
                responseData = ResponseFormat.Fail;
                responseData.message = ErrorMessages.UNAUTHORIZED;
            }
            var json = JsonConvert.SerializeObject(responseData);
            response.Content = new StringContent(json, Encoding.UTF8, "application/json");
            return response;
        }

        [HttpPost]
        [Route("leads")]
        [ResponseType(typeof(ResponseFormat))]
        public HttpResponseMessage Create(LeadCreateApiModel apiModel)
        {
            var response = new HttpResponseMessage();
            ResponseFormat responseData = new ResponseFormat();
            AuthorizationService _authorizationService = new AuthorizationService().SetPerm((int)EnumPermissions.LEAD_CREATE);
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
                        response.StatusCode = HttpStatusCode.Unauthorized;
                        responseData = ResponseFormat.Fail;
                        responseData.message = ErrorMessages.TOKEN_EXPIRED;
                    }
                    if ((string)payload["error"] == ErrorMessages.TOKEN_INVALID)
                    {
                        response.StatusCode = HttpStatusCode.Unauthorized;
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
                        var isCreated = _leadService.Create(apiModel, Convert.ToInt32(userId));
                        if (isCreated)
                        {
                            response.StatusCode = HttpStatusCode.OK;
                            responseData = ResponseFormat.Success;
                            responseData.message = SuccessMessages.LEAD_CREATED;

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
                response.StatusCode = HttpStatusCode.Unauthorized;
                responseData = ResponseFormat.Fail;
                responseData.message = ErrorMessages.UNAUTHORIZED;
            }
            var json = JsonConvert.SerializeObject(responseData);
            response.Content = new StringContent(json, Encoding.UTF8, "application/json");
            return response;
        }

        [HttpPost]
        [Route("leads/{id}")]
        [ResponseType(typeof(ResponseFormat))]
        public HttpResponseMessage Update([FromUri] int id, [FromBody] LeadCreateApiModel apiModel)
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
                        response.StatusCode = HttpStatusCode.Unauthorized;
                        responseData = ResponseFormat.Fail;
                        responseData.message = ErrorMessages.TOKEN_EXPIRED;
                    }
                    if ((string)payload["error"] == ErrorMessages.TOKEN_INVALID)
                    {
                        response.StatusCode = HttpStatusCode.Unauthorized;
                        responseData = ResponseFormat.Fail;
                        responseData.message = ErrorMessages.TOKEN_INVALID;
                    }
                }
                else
                {
                    var userId = Convert.ToInt32(payload["id"]);
                    var owner = _leadService.FindOwnerId(id);
                    if ((userId == owner) || (new AuthorizationService().SetPerm((int)EnumPermissions.LEAD_DELETE).Authorize(userId)))
                    {
                        var isUpdated = _leadService.Update(id, apiModel, Convert.ToInt32(userId));
                        if (isUpdated)
                        {
                            response.StatusCode = HttpStatusCode.OK;
                            responseData = ResponseFormat.Success;
                            responseData.message = SuccessMessages.LEAD_MODIFIED;
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
                response.StatusCode = HttpStatusCode.Unauthorized;
                responseData = ResponseFormat.Fail;
                responseData.message = ErrorMessages.UNAUTHORIZED;
            }
            var json = JsonConvert.SerializeObject(responseData);
            response.Content = new StringContent(json, Encoding.UTF8, "application/json");
            return response;
        }

        [HttpPost]
        [Route("leads/{id}/change_avatar")]
        [ResponseType(typeof(ResponseFormat))]
        public HttpResponseMessage ChangeAvatar([FromUri] int id)
        {
            var response = new HttpResponseMessage();
            ResponseFormat responseData = new ResponseFormat();
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
                        response.StatusCode = HttpStatusCode.Unauthorized;
                        responseData = ResponseFormat.Fail;
                        responseData.message = ErrorMessages.TOKEN_EXPIRED;
                    }
                    if ((string)payload["error"] == ErrorMessages.TOKEN_INVALID)
                    {
                        response.StatusCode = HttpStatusCode.Unauthorized;
                        responseData = ResponseFormat.Fail;
                        responseData.message = ErrorMessages.TOKEN_INVALID;
                    }
                }
                else
                {
                    var userId = Convert.ToInt32(payload["id"]);
                    //if user is owner
                    var owner = _leadService.FindOwnerId(id);
                    if ((userId == owner) || (new AuthorizationService().SetPerm((int)EnumPermissions.LEAD_DELETE).Authorize(userId)))
                    {
                        if (HttpContext.Current.Request.Files.Count > 0)
                        {
                            var uploadedFile = HttpContext.Current.Request.Files[0];
                            var isChanged = _leadService.ChangeAvatar(id, uploadedFile);
                            if (isChanged)
                            {
                                response.StatusCode = HttpStatusCode.OK;
                                responseData = ResponseFormat.Success;
                                responseData.message = SuccessMessages.AVATAR_CHANGED;
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
                            response.StatusCode = HttpStatusCode.BadRequest;
                            responseData = ResponseFormat.Fail;
                            responseData.message = ErrorMessages.INVALID_BODY;
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
                response.StatusCode = HttpStatusCode.Unauthorized;
                responseData = ResponseFormat.Fail;
                responseData.message = ErrorMessages.UNAUTHORIZED;
            }
            var json = JsonConvert.SerializeObject(responseData);
            response.Content = new StringContent(json, Encoding.UTF8, "application/json");
            return response;
        }

        [HttpGet]
        [Route("leads/{id}")]
        [ResponseType(typeof(LeadDetailApiModel))]
        public HttpResponseMessage Detail([FromUri] int id)
        {
            var response = new HttpResponseMessage();
            ResponseFormat responseData = new ResponseFormat();
            AuthorizationService _authorizationService = new AuthorizationService().SetPerm((int)EnumPermissions.LEAD_VIEW);
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
                        response.StatusCode = HttpStatusCode.Unauthorized;
                        responseData = ResponseFormat.Fail;
                        responseData.message = ErrorMessages.TOKEN_EXPIRED;
                    }
                    if ((string)payload["error"] == ErrorMessages.TOKEN_INVALID)
                    {
                        response.StatusCode = HttpStatusCode.Unauthorized;
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
                        responseData.data = _leadService.GetOne(id);
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
                response.StatusCode = HttpStatusCode.Unauthorized;
                responseData = ResponseFormat.Fail;
                responseData.message = ErrorMessages.UNAUTHORIZED;
            }
            var json = JsonConvert.SerializeObject(responseData);
            response.Content = new StringContent(json, Encoding.UTF8, "application/json");
            return response;

        }

        [HttpPost]
        [Route("leads/{id}/notes")]
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
                        response.StatusCode = HttpStatusCode.Unauthorized;
                        responseData = ResponseFormat.Fail;
                        responseData.message = ErrorMessages.TOKEN_EXPIRED;
                    }
                    if ((string)payload["error"] == ErrorMessages.TOKEN_INVALID)
                    {
                        response.StatusCode = HttpStatusCode.Unauthorized;
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
                            apiModel.lead = id;
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
                response.StatusCode = HttpStatusCode.Unauthorized;
                responseData = ResponseFormat.Fail;
                responseData.message = ErrorMessages.UNAUTHORIZED;
            }
            var json = JsonConvert.SerializeObject(responseData);
            response.Content = new StringContent(json, Encoding.UTF8, "application/json");
            return response;
        }

        [HttpDelete]
        [Route("leads/{id}")]
        [ResponseType(typeof(ResponseFormat))]
        public HttpResponseMessage Delete([FromUri] int id)
        {
            var response = new HttpResponseMessage();
            ResponseFormat responseData = new ResponseFormat();
            AuthorizationService _authorizationService = new AuthorizationService().SetPerm((int)EnumPermissions.LEAD_DELETE);
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
                        response.StatusCode = HttpStatusCode.Unauthorized;
                        responseData = ResponseFormat.Fail;
                        responseData.message = ErrorMessages.TOKEN_EXPIRED;
                    }
                    if ((string)payload["error"] == ErrorMessages.TOKEN_INVALID)
                    {
                        response.StatusCode = HttpStatusCode.Unauthorized;
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
                        var isDeleted = _leadService.Delete(id, Convert.ToInt32(userId));
                        if (isDeleted)
                        {
                            response.StatusCode = HttpStatusCode.OK;
                            responseData = ResponseFormat.Success;
                            responseData.message = SuccessMessages.LEAD_DELETED;
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
                response.StatusCode = HttpStatusCode.Unauthorized;
                responseData = ResponseFormat.Fail;
                responseData.message = ErrorMessages.UNAUTHORIZED;
            }
            var json = JsonConvert.SerializeObject(responseData);
            response.Content = new StringContent(json, Encoding.UTF8, "application/json");
            return response;
        }

        [HttpDelete]
        [Route("leads/{id}/notes/{noteId}")]
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
                        response.StatusCode = HttpStatusCode.Unauthorized;
                        responseData = ResponseFormat.Fail;
                        responseData.message = ErrorMessages.TOKEN_EXPIRED;
                    }
                    if ((string)payload["error"] == ErrorMessages.TOKEN_INVALID)
                    {
                        response.StatusCode = HttpStatusCode.Unauthorized;
                        responseData = ResponseFormat.Fail;
                        responseData.message = ErrorMessages.TOKEN_INVALID;
                    }
                }
                else
                {
                    var userId = Convert.ToInt32(payload["id"]);
                    var leadOwner = _leadService.FindOwnerId(id);
                    var noteOwner = _noteService.FindOwner(noteId);
                    if ((userId == leadOwner) || (userId == noteOwner) || (new AuthorizationService().SetPerm((int)EnumPermissions.NOTE_DELETE_ANY).Authorize(userId)))
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
                response.StatusCode = HttpStatusCode.Unauthorized;
                responseData = ResponseFormat.Fail;
                responseData.message = ErrorMessages.UNAUTHORIZED;
            }
            var json = JsonConvert.SerializeObject(responseData);
            response.Content = new StringContent(json, Encoding.UTF8, "application/json");
            return response;
        }

        [HttpPost]
        [Route("leads/{id}/tags")]
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
                        response.StatusCode = HttpStatusCode.Unauthorized;
                        responseData = ResponseFormat.Fail;
                        responseData.message = ErrorMessages.TOKEN_EXPIRED;
                    }
                    if ((string)payload["error"] == ErrorMessages.TOKEN_INVALID)
                    {
                        response.StatusCode = HttpStatusCode.Unauthorized;
                        responseData = ResponseFormat.Fail;
                        responseData.message = ErrorMessages.TOKEN_INVALID;
                    }
                }
                else
                {
                    var userId = Convert.ToInt32(payload["id"]);
                    var owner = _leadService.FindOwnerId(id);
                    if ((userId == owner) || (new AuthorizationService().SetPerm((int)EnumPermissions.LEAD_DELETE).Authorize(userId)))
                    {
                        //check if a tag exist

                        //if it is, create a tag item with current lead

                        // else create a new tag and a new tag item
                        var isAdded = _leadService.AddTag(id, tag.name);
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
                response.StatusCode = HttpStatusCode.Unauthorized;
                responseData = ResponseFormat.Fail;
                responseData.message = ErrorMessages.UNAUTHORIZED;
            }
            var json = JsonConvert.SerializeObject(responseData);
            response.Content = new StringContent(json, Encoding.UTF8, "application/json");
            return response;
        }
    
        [HttpDelete]
        [Route("leads/{id}/tags/{tagId}")]
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
                        response.StatusCode = HttpStatusCode.Unauthorized;
                        responseData = ResponseFormat.Fail;
                        responseData.message = ErrorMessages.TOKEN_EXPIRED;
                    }
                    if ((string)payload["error"] == ErrorMessages.TOKEN_INVALID)
                    {
                        response.StatusCode = HttpStatusCode.Unauthorized;
                        responseData = ResponseFormat.Fail;
                        responseData.message = ErrorMessages.TOKEN_INVALID;
                    }
                }
                else
                {
                    var userId = Convert.ToInt32(payload["id"]);
                    var owner = _leadService.FindOwnerId(id);
                    if ((userId == owner) || (new AuthorizationService().SetPerm((int)EnumPermissions.LEAD_DELETE).Authorize(userId)))
                    {
                        
                        var isRemoved = _leadService.RemoveTag(id, tagId);
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
                response.StatusCode = HttpStatusCode.Unauthorized;
                responseData = ResponseFormat.Fail;
                responseData.message = ErrorMessages.UNAUTHORIZED;
            }
            var json = JsonConvert.SerializeObject(responseData);
            response.Content = new StringContent(json, Encoding.UTF8, "application/json");
            return response;
        }

        [HttpGet]
        [Route("leads/{id}/convert")]
        [ResponseType(typeof(ResponseFormat))]
        public HttpResponseMessage ConvertLead([FromUri] int id)
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
                        response.StatusCode = HttpStatusCode.Unauthorized;
                        responseData = ResponseFormat.Fail;
                        responseData.message = ErrorMessages.TOKEN_EXPIRED;
                    }
                    if ((string)payload["error"] == ErrorMessages.TOKEN_INVALID)
                    {
                        response.StatusCode = HttpStatusCode.Unauthorized;
                        responseData = ResponseFormat.Fail;
                        responseData.message = ErrorMessages.TOKEN_INVALID;
                    }
                }
                else
                {
                    var userId = Convert.ToInt32(payload["id"]);
                    var owner = _leadService.FindOwnerId(id);
                    if ((userId == owner) || (new AuthorizationService().SetPerm((int)EnumPermissions.LEAD_DELETE).Authorize(userId)))
                    {
                        var result = _leadService.Convert(id, userId);
                        if(result != null)
                        {
                            response.StatusCode = HttpStatusCode.OK;
                            responseData = ResponseFormat.Success;
                            responseData.data = result;
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
                response.StatusCode = HttpStatusCode.Unauthorized;
                responseData = ResponseFormat.Fail;
                responseData.message = ErrorMessages.UNAUTHORIZED;
            }
            var json = JsonConvert.SerializeObject(responseData);
            response.Content = new StringContent(json, Encoding.UTF8, "application/json");
            return response;
        }
    }
}
