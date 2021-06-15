using Backend.Extensions;
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
using System.Web.Http;
using System.Web.Http.Description;

namespace Backend.Controllers
{
    public class TagController : ApiController
    {
        TagService _tagService = new TagService();

        [HttpGet]
        [Route("tags")]
        [ResponseType(typeof(List<TagApiModel>))]
        public HttpResponseMessage Get(string query = "")
        {
            var response = new HttpResponseMessage();
            ResponseFormat responseData = new ResponseFormat();

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
                    response.StatusCode = HttpStatusCode.OK;
                    responseData = ResponseFormat.Success;
                    var tags = _tagService.GetTagList(query);
                    responseData.data = tags;
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
