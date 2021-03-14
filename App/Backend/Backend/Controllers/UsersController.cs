﻿using Backend.Domain;
using Backend.Extensions;
using Backend.Models;
using Backend.Models.SwaggerModel;
using Backend.Models.SwaggerModel.Users;
using Backend.Resources;
using Backend.Services;
using Backend.Validators;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web.Http;
using System.Web.Http.Description;
using static Backend.Extensions.Enum;

namespace Backend.Controllers
{
    public class UsersController : ApiController
    {
        public DatabaseContext db = new DatabaseContext();
        public UserService _userService = new UserService();
        public UserValidator _userValidator = new UserValidator();

        [HttpGet]
        [Route("users")]
        [ResponseType(typeof(Swagger_User_List))]
        public HttpResponseMessage Get([FromUri] int currentPage = 0, [FromUri] int pageSize = 0)
        {
            var response = new HttpResponseMessage();
            ResponseFormat responseData = new ResponseFormat();
            AuthorizationService _authorizationService = new AuthorizationService().SetPerm((int)EnumPermissions.USER_VIEW);
            //read jwt
            IEnumerable<string> headerValues = Request.Headers.GetValues("Authorization");
            string jwt = headerValues.FirstOrDefault();
            //validate jwt
            var payload = JwtTokenManager.ValidateJwtToken(jwt);

            if (payload.ContainsKey("error"))
            {
                if((string)payload["error"] == ErrorMessages.TOKEN_EXPIRED)
                {
                    response.StatusCode = HttpStatusCode.Forbidden;
                    responseData = ResponseFormat.Fail;
                    responseData.message = ErrorMessages.TOKEN_EXPIRED;
                }
                if((string)payload["error"] == ErrorMessages.TOKEN_INVALID)
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
                        pageInfo = new { totalPage = p.TotalPages, pageSize = p.PageSize, startIndex = p.StartIndex, endIndex = p.EndIndex, currentPage = p.CurrentPage}
                    };
                }
                else
                {
                    response.StatusCode = HttpStatusCode.Forbidden;
                    responseData = ResponseFormat.Fail;
                    responseData.message = ErrorMessages.UNAUTHORIZED;
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
            if(headerValues == null)
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
                        }
                        else
                        {
                            response.StatusCode = HttpStatusCode.NotFound;
                            responseData = ResponseFormat.Fail;
                            responseData.message = "No valid user found.";
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
            string jwt = headerValues.FirstOrDefault();

            if (user != null)
            {

            }
            return Request.CreateResponse(HttpStatusCode.OK);
        }

    }
}
