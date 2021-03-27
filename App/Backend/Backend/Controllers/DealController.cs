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
using static Backend.Extensions.Enum;

namespace Backend.Controllers
{
    public class DealController : ApiController
    {
        DealService _dealService = new DealService();
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

    }
}
