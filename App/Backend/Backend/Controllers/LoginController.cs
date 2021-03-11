using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Newtonsoft.Json;
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

namespace Backend.Controllers
{

    public class LoginController : ApiController
    {
        DatabaseContext db = new DatabaseContext();
        private UserRepository _userRepository = new UserRepository();
        private UserService _userService = new UserService();
        /// <summary>
        /// Login người dùng vào hệ thống, param bắt buộc: email, password
        /// Trả về 
        /// </summary>
        /// <param name="email">Email người dùng</param>
        /// <param name="password">Mật khẩu người dùng</param>
        /// <returns></returns>
        [HttpPost]
        [Route("login")]
        [ResponseType(typeof(ResponseFormat))]
        public HttpResponseMessage Login([FromBody]LoginApiModel apiModel)
        {
            HttpResponseMessage response = new HttpResponseMessage();
            ResponseFormat responseData;
            var validate = _userService.ValidatePassword(apiModel.email, apiModel.password);
            if(validate.Item1 == true)
            {
                //generate jwt token
                var JwtToken = JwtTokenManager.GenerateJwtToken(validate.Item3);
                //generate refresh token
                var RefreshToken = JwtTokenManager.GenerateRefreshToken();
                //save refresh token to user record
                //var dbUser = _userRepository.GetByEmail(validate.Item3.Email);
                var dbUser = db.USERs.Where(c => c.Email == validate.Item3.Email).FirstOrDefault();
                dbUser.RememberMeToken = RefreshToken;
                db.SaveChanges();
                //set refresh token to httponly and add it to cookies
                var nv = new NameValueCollection();
                nv["refreshToken"] = RefreshToken;
                nv["seriesIdentifier"] = dbUser.ID.ToString();

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
                        jwt = JwtToken
                    }
                };

            }
            else
            {
                responseData = ResponseFormat.Fail;
                responseData.message = validate.Item2;
            }
            var json = JsonConvert.SerializeObject(responseData);
            response.Content = new StringContent(json, Encoding.UTF8, "application/json");
            return response;
        }
        /// <summary>
        /// Refresh jwt key và refresh_key
        /// </summary>
        /// <param name="apiModel"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("refresh_token")]
        [ResponseType(typeof(ResponseFormat))]
        public HttpResponseMessage Refresh([FromBody] LoginApiModel apiModel)
        {
            string c_refreshToken = "";
            string c_series = "";
            HttpResponseMessage response = new HttpResponseMessage();
            ResponseFormat responseData;

            CookieHeaderValue cookie = Request.Headers.GetCookies("refreshTokenData").FirstOrDefault();
            if(cookie != null)
            {
                CookieState cookieState = cookie["refreshTokenData"];
                c_refreshToken = cookieState["refreshToken"];
                c_series = cookieState["seriesIdentifier"];
                var dbUser = db.USERs.Find(int.Parse(c_series));
                if(dbUser != null)
                {
                    if(c_refreshToken == dbUser.RememberMeToken)
                    {
                        var user = AutoMapper.Mapper.Map<User>(dbUser);
                        //grab a new jwt token
                        var JwtToken = JwtTokenManager.GenerateJwtToken(user);
                        //grab a new refresh token
                        var RefreshToken = JwtTokenManager.GenerateRefreshToken();
                        dbUser.RememberMeToken = RefreshToken;
                        db.SaveChanges();
                        //set new cookie
                        var nv = new NameValueCollection();
                        nv["refreshToken"] = RefreshToken;
                        nv["seriesIdentifier"] = dbUser.ID.ToString();
                        var respCookie = new CookieHeaderValue("refreshTokenData", nv);
                        respCookie.HttpOnly = true;
                        respCookie.Secure = true;
                        respCookie.Domain = Request.RequestUri.Host;
                        //build response data
                        responseData = ResponseFormat.Success;
                        responseData.data = new
                        {
                            user = new
                            {
                                username = user.Username,
                                firstName = user.FirstName,
                                lastName = user.LastName,
                                jwt = JwtToken
                            }
                        };
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
                    responseData.message = "No valid user found";
                }
            }
            else
            {
                responseData = ResponseFormat.Fail;
                responseData.message = "No cookie found";
            }
            var json = JsonConvert.SerializeObject(responseData);
            response.Content = new StringContent(json, Encoding.UTF8, "application/json");
            return response;
        }
    }
}
