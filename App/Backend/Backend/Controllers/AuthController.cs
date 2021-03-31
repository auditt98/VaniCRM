using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Newtonsoft.Json;
using Backend.Models.SwaggerModel;
using Backend.Models;
using BCrypt.Net;
using Backend.Domain;
using Backend.Repository;
using Backend.Extensions;
using Backend.Services;
using Backend.Models.ApiModel;
using System.Net.Http.Headers;
using System.Text;
using System.Collections.Specialized;
using System.Web.Http.Description;
using Backend.Resources;
using System.Web.Http.Cors;

namespace Backend.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class AuthController : ApiController
    {
        DatabaseContext db = new DatabaseContext();
        private UserRepository _userRepository = new UserRepository();
        private UserService _userService = new UserService();
        private EmailManager _emailManager = new EmailManager();
        private HashManager _hashManager = new HashManager();
        /// <summary>
        /// Login an user
        /// 
        /// </summary>
        /// <param name="email"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("login")]
        [ResponseType(typeof(SwaggerLoginReponse))]
        //[EnableCors(origins: "*", headers: "*", methods: "*")]
        public HttpResponseMessage Login([FromBody]LoginApiModel apiModel)
        {
            HttpResponseMessage response = new HttpResponseMessage();

            ResponseFormat responseData;
            if(apiModel == null)
            {
                response.StatusCode = HttpStatusCode.BadRequest;
                responseData = ResponseFormat.Fail;
                responseData.message = ErrorMessages.INVALID_BODY;
            }
            else
            {
                var validate = _userService.ValidatePassword(apiModel.email, apiModel.password);
                if (validate.Item1 == true)
                {
                    var dbUser = _userRepository.GetByEmail(validate.Item3.Email);
                    //generate jwt token
                    var JwtToken = JwtTokenManager.GenerateJwtToken(validate.Item3);
                    //generate refresh token
                    var RefreshToken = JwtTokenManager.GenerateRefreshToken();
                    REFRESH_TOKEN newRefreshToken = new REFRESH_TOKEN();
                    newRefreshToken.USER_ID = dbUser.ID;
                    newRefreshToken.Token = RefreshToken;
                    db.REFRESH_TOKEN.Add(newRefreshToken);
                    db.SaveChanges();

                    //set refresh token to httponly and add it to cookies
                    var nv = new NameValueCollection();
                    nv["refreshToken"] = RefreshToken;
                    nv["seriesIdentifier"] = dbUser.ID.ToString();
                    nv["tokenIdentifier"] = newRefreshToken.ID.ToString();

                    var cookie = new CookieHeaderValue("refreshTokenData", nv);
                    cookie.HttpOnly = true;
                    cookie.Secure = true;
                    cookie.Domain = Request.RequestUri.Host;
                    response.Headers.AddCookies(new CookieHeaderValue[] { cookie });

                    //create response data
                    responseData = ResponseFormat.Success;
                    responseData.data = new
                    {
                        user = new
                        {
                            username = validate.Item3.Username,
                            firstName = validate.Item3.FirstName,
                            lastName = validate.Item3.LastName,
                            jwt = JwtToken,
                            group = dbUser.GROUP.ID
                        }
                    };
                    response.StatusCode = HttpStatusCode.OK;
                }
                else
                {
                    response.StatusCode = HttpStatusCode.Unauthorized;
                    responseData = ResponseFormat.Fail;
                    responseData.message = validate.Item2;
                }
            }
            var json = JsonConvert.SerializeObject(responseData);
            response.Content = new StringContent(json, Encoding.UTF8, "application/json");
            return response;
        }
        /// <summary>
        /// Refresh jwt and refresh_key
        /// </summary>
        /// <param name="apiModel"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("refresh_token")]
        [ResponseType(typeof(SwaggerLoginReponse))]
        public HttpResponseMessage Refresh()
        {
            string c_refreshToken = "";
            string c_series = "";
            string t_series = "";
            HttpResponseMessage response = new HttpResponseMessage();
            ResponseFormat responseData;

            CookieHeaderValue cookie = Request.Headers.GetCookies("refreshTokenData").FirstOrDefault();
            if(cookie != null)
            {
                cookie.Expires = DateTimeOffset.Now.AddMonths(1);
                CookieState cookieState = cookie["refreshTokenData"];
                c_refreshToken = cookieState["refreshToken"];
                c_series = cookieState["seriesIdentifier"];
                t_series = cookieState["tokenIdentifier"];
                var dbUser = db.USERs.Find(int.Parse(c_series));

                if (dbUser != null)
                {
                    //look for token
                    var temp = int.Parse(t_series);
                    var dbToken = dbUser.REFRESH_TOKEN.Where(c => c.ID == temp).FirstOrDefault();
                    if (dbToken != null)
                    {
                        if(c_refreshToken == dbToken.Token)
                        {
                            var user = AutoMapper.Mapper.Map<User>(dbUser);
                            //grab a new jwt token
                            var JwtToken = JwtTokenManager.GenerateJwtToken(user);
                            //grab a new refresh token
                            var RefreshToken = JwtTokenManager.GenerateRefreshToken();
                            //store new value for token
                            dbToken.Token = RefreshToken;
                            db.SaveChanges();
                            //make a cookie
                            var nv = new NameValueCollection();
                            nv["refreshToken"] = RefreshToken;
                            nv["seriesIdentifier"] = dbUser.ID.ToString();
                            nv["tokenIdentifier"] = dbToken.ID.ToString();
                            var respCookie = new CookieHeaderValue("refreshTokenData", nv);
                            respCookie.HttpOnly = true;
                            respCookie.Secure = true;
                            respCookie.Domain = Request.RequestUri.Host;
                            response.Headers.AddCookies(new CookieHeaderValue[] { respCookie });
                            //build response data
                            responseData = ResponseFormat.Success;
                            responseData.data = new
                            {
                                user = new
                                {
                                    id = user.ID,
                                    username = user.Username,
                                    firstName = user.FirstName,
                                    lastName = user.LastName,
                                    jwt = JwtToken,
                                    group = dbUser.GROUP.ID
                                }
                            };
                            response.StatusCode = HttpStatusCode.OK;

                        }
                        else
                        {
                            responseData = ResponseFormat.Fail;
                            responseData.message = "Cookie Invalid";
                        }
                    }
                    else
                    {
                        responseData = ResponseFormat.Fail;
                        responseData.message = "Cookie Invalid";
                    }
                }
                else
                {
                    response.StatusCode = HttpStatusCode.NotFound;
                    responseData = ResponseFormat.Fail;
                    responseData.message = "Not a valid user";
                }
            }
            else
            {
                response.StatusCode = HttpStatusCode.Forbidden;
                responseData = ResponseFormat.Fail;
                responseData.message = "No cookie found";
            }
            var json = JsonConvert.SerializeObject(responseData);
            response.Content = new StringContent(json, Encoding.UTF8, "application/json");
            return response;
        }
    
        [HttpGet]
        [Route("logout")]
        [ResponseType(typeof(ResponseFormat))]
        public HttpResponseMessage Logout()
        {
            CookieHeaderValue cookie = Request.Headers.GetCookies("refreshTokenData").FirstOrDefault();
            HttpResponseMessage response = new HttpResponseMessage();
            ResponseFormat responseData;
            string c_series = "";
            string t_series = "";

            if (cookie != null)
            {
                CookieState cookieState = cookie["refreshTokenData"];
                t_series = cookieState["tokenIdentifier"];
                var temp = int.Parse(t_series);
                var token = db.REFRESH_TOKEN.Find(temp);
                db.REFRESH_TOKEN.Remove(token);
                db.SaveChanges();
                cookie.Expires = DateTime.Now.AddDays(-10);
                response.Headers.AddCookies(new CookieHeaderValue[] { cookie });
                response.StatusCode = HttpStatusCode.OK;
                responseData = ResponseFormat.Success;
                
            }
            else
            {
                response.StatusCode = HttpStatusCode.Forbidden;
                responseData = ResponseFormat.Fail;
                responseData.message = "No cookie found";
            }
            var json = JsonConvert.SerializeObject(responseData);
            response.Content = new StringContent(json, Encoding.UTF8, "application/json");
            return response;
        }

        [HttpGet]
        [Route("forgot_password")]
        public HttpResponseMessage ResetPassword([FromUri] string email)
        {
            //_emailManager = new EmailManager()
            HttpResponseMessage response = new HttpResponseMessage();

            ResponseFormat responseData;

            var dbUser = db.USERs.Where(c => c.Email == email).FirstOrDefault();
            if (dbUser != null)
            {
                Random r = new Random();
                string validationCode = r.Next(10000, 1000000).ToString("D6");
                var jwtManager = JwtTokenManager.GenerateJwtForPasswordReset(validationCode, email);

                _emailManager = new EmailManager();
                _emailManager.Title = StaticStrings.RESET_PASSWORD_TITLE;
                _emailManager.Content = $"<a style='-webkit-appearance:button; -moz-appearance:button; appearance:button; text-decoration:none; background-color: #D93915; color: white; padding: 1em 1.5em; text-transform: uppercase;' href='{StaticStrings.ClientHost}reset_password?key={jwtManager}'>Reset password</a><p style='font-style: italic;'>This link will expire after 30 minutes.</p>";
                _emailManager.Recipients = new List<string>() { email };
                _emailManager.SendEmail();
                if (_emailManager.isSent)
                {
                    dbUser.RememberMeToken = validationCode;
                    db.SaveChanges();
                    response.StatusCode = HttpStatusCode.OK;
                    responseData = ResponseFormat.Success;
                }
                else
                {
                    response.StatusCode = HttpStatusCode.InternalServerError;
                    responseData = ResponseFormat.Fail;
                    responseData.message = ErrorMessages.EMAIL_SEND_FAILED;

                }
            }
            else
            {
                response.StatusCode = HttpStatusCode.NotFound;
                responseData = ResponseFormat.Fail;
                responseData.message = ErrorMessages.USER_NOT_FOUND;

            }
            var json = JsonConvert.SerializeObject(responseData);
            response.Content = new StringContent(json, Encoding.UTF8, "application/json");
            return response;
        }

        [HttpPost]
        [Route("reset_password")]
        public HttpResponseMessage ValidateCode([FromBody] ResetPasswordApiModel apiModel)
        {
            var response = new HttpResponseMessage();
            ResponseFormat responseData = new ResponseFormat();

            if (apiModel == null)
            {
                response.StatusCode = HttpStatusCode.BadRequest;
                responseData = ResponseFormat.Fail;
                responseData.message = ErrorMessages.INVALID_KEY;
            }
            else
            {
                //validate the key sent
                if (string.IsNullOrEmpty(apiModel.key) || string.IsNullOrEmpty(apiModel.newPassword))
                {
                    response.StatusCode = HttpStatusCode.BadRequest;
                    responseData = ResponseFormat.Fail;
                    responseData.message = ErrorMessages.INVALID_KEY;
                }
                else
                {
                    var payload = JwtTokenManager.ValidateJwtToken(apiModel.key);
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
                        //decode key for field "validationCode" and "email"
                        var userEmail = Convert.ToString(payload["email"]);
                        var userCode = Convert.ToString(payload["validationCode"]);
                        //find user with email, if validation code is the same, hash password and save it to db
                        var dbUser = db.USERs.Where(c => c.Email == userEmail).FirstOrDefault();
                        if (dbUser != null)
                        {
                            if (dbUser.RememberMeToken == userCode)
                            {
                                //hash user password
                                dbUser.Hash = _hashManager.Hash(apiModel.newPassword);
                                db.SaveChanges();
                                response.StatusCode = HttpStatusCode.OK;
                                responseData = ResponseFormat.Success;
                                responseData.message = SuccessMessages.PASSWORD_RESET;
                            }
                            else
                            {
                                response.StatusCode = HttpStatusCode.Forbidden;
                                responseData = ResponseFormat.Fail;
                                responseData.message = ErrorMessages.INVALID_KEY;
                            }
                        }
                        else
                        {
                            response.StatusCode = HttpStatusCode.NotFound;
                            responseData = ResponseFormat.Fail;
                            responseData.message = ErrorMessages.USER_NOT_FOUND;
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
