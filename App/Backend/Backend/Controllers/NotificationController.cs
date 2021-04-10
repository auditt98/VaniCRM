using Backend.Extensions;
using Backend.Models.ApiModel;
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
    public class NotificationController : ApiController
    {
        NotificationService _notificationService = new NotificationService();

        [HttpGet]
        [Route("notification")]
        [ResponseType(typeof(NotificationListApiModel))]
        public HttpResponseMessage Get([FromUri] int userId, [FromUri]int page = 1)
        {
            var response = new HttpResponseMessage();
            ResponseFormat responseData = new ResponseFormat();
            var notifications = _notificationService.GetNotifications(userId, page);

            response.StatusCode = HttpStatusCode.OK;
            responseData = ResponseFormat.Success;
            responseData.data = _notificationService.GetNotifications(userId, page);
            
            var json = JsonConvert.SerializeObject(responseData);
            response.Content = new StringContent(json, Encoding.UTF8, "application/json");
            return response;
        }

    }
}
