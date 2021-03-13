using Backend.Extensions;
using Backend.Resources;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web.Http;
using static Backend.Extensions.Enum;

namespace Backend.Controllers
{
    public class UsersController : ApiController
    {
        [HttpGet]
        [Route("users")]
        public HttpResponseMessage GetAll()
        {
            //read jwt
            var response = new HttpResponseMessage();
            ResponseFormat responseData = new ResponseFormat();


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


            //ResponseFormat responseData = ResponseFormat.Success;
            Console.WriteLine(payload["id"]);

           
            responseData.data = new
            {
                jwtToken = jwt
            };
            var json = JsonConvert.SerializeObject(responseData);
            response.Content = new StringContent(json, Encoding.UTF8, "application/json");
            return response;
        }

    }
}
