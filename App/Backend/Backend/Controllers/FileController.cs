using Backend.Extensions;
using Backend.Resources;
using Backend.Services;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Web.Http;

namespace Backend.Controllers
{
    public class FileController : ApiController
    {
        FileService _fileService = new FileService();

        /// <summary>
        /// Download a file
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("files/{id}")]
        public HttpResponseMessage GetFile(int id)
        {
            HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.OK);
            var results = _fileService.Download(id);
            if(results.file != null)
            {
                response.StatusCode = HttpStatusCode.OK;
                response.Content = results.file;
                response.Content.Headers.ContentType = new MediaTypeHeaderValue(results.mimeType);
                response.Content.Headers.ContentDisposition = new ContentDispositionHeaderValue("attachment")
                {
                    FileName = results.fileName
                };

            }
            else
            {
                response.StatusCode = HttpStatusCode.Gone;
                ResponseFormat responseData = ResponseFormat.Fail;
                responseData.message = ErrorMessages.SOMETHING_WRONG;
                var json = JsonConvert.SerializeObject(responseData);
                response.Content = new StringContent(json, Encoding.UTF8, "application/json");
            }
            return response;
        }
    }
}
