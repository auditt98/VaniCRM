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
    public class ApiResponseDemoController : ApiController
    {
        DatabaseContext db = new DatabaseContext();
        UserService _userService = new UserService();
        /// <summary>
        /// Format response trả về cho các request được thực hiện thành công, data chứa trong key [data].
        /// </summary>
        /// <returns>ResponseFormat</returns>
        /// 

        [HttpGet]
        [Route("api/demo/success")]
        [ResponseType(typeof(ResponseFormat))]
        public HttpResponseMessage SuccessedResponse()
        {
            HttpResponseMessage response = new HttpResponseMessage();
            ResponseFormat responseData;
            responseData = ResponseFormat.Success;
            responseData.data = new { a = 1 };

            var json = JsonConvert.SerializeObject(responseData);
            response.Content = new StringContent(json, Encoding.UTF8, "application/json");
            return response;
        }

        /// <summary>
        /// Format response trả về cho các request thực hiện không thành công (do lỗi dữ liệu không chính xác, không đủ..)
        /// Thông tin lỗi sẽ được hiển thị ở key [message]
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("api/demo/fail")]
        [ResponseType(typeof(ResponseFormat))]
        public HttpResponseMessage FailedResponse()
        {
            HttpResponseMessage response = new HttpResponseMessage();
            ResponseFormat responseData;
            responseData = ResponseFormat.Fail;
            responseData.message = "No user with this email address is found";

            var json = JsonConvert.SerializeObject(responseData);
            response.Content = new StringContent(json, Encoding.UTF8, "application/json");
            return response;
        }

        /// <summary>
        /// Format response trả về cho các request thực hiện bị lỗi do exception
        /// Thông tin lỗi sẽ được hiển thị ở key [message]
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("api/demo/error")]
        [ResponseType(typeof(ResponseFormat))]
        public HttpResponseMessage ErrorResponse()
        {
            HttpResponseMessage response = new HttpResponseMessage();
            ResponseFormat responseData;
            responseData = ResponseFormat.Error;
            responseData.message = "Unhandled Exception: .....";

            var json = JsonConvert.SerializeObject(responseData);
            response.Content = new StringContent(json, Encoding.UTF8, "application/json");
            return response;
        }


    }
}
