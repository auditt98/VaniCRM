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
using System.Web;
using System.Web.Http.Cors;
using System.IO;
using Backend.Resources;
using static Backend.Extensions.Enum;

namespace Backend.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
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
        [Route("test/{id}")]
        public HttpResponseMessage Test([FromUri]int id)
        {
            var response = new HttpResponseMessage();
            ResponseFormat responseData = new ResponseFormat();

            string targetFolder = HttpContext.Current.Server.MapPath("~/Uploads");
            var dbUser = db.USERs.Find(id);
            var a = MimeMapping.MimeUtility.GetMimeMapping(dbUser.Avatar);
            //var avatar = System.IO.File.ReadAllBytes(Path.Combine(targetFolder, dbUser.Avatar));
            var img = Convert.ToBase64String(System.IO.File.ReadAllBytes(Path.Combine(targetFolder, dbUser.Avatar)));
            response.StatusCode = HttpStatusCode.OK;
            responseData.data = $"data:{a};base64,{img}";
            var json = JsonConvert.SerializeObject(responseData);
            response.Content = new StringContent(json, Encoding.UTF8, "application/json");
            return response;
        }

        [HttpPost]
        [Route("test/multiple_files_upload")]
        public HttpResponseMessage MultipleFilesUpload()
        {
            var response = new HttpResponseMessage();
            ResponseFormat responseData = new ResponseFormat();

            string targetFolder = HttpContext.Current.Server.MapPath("~/Uploads");
            string content = HttpContext.Current.Request.Form["content"];
            
            if (HttpContext.Current.Request.Files.Count > 0)
            {
                var allFiles = HttpContext.Current.Request.Files;
                responseData = ResponseFormat.Success;
                responseData.message = content;
                foreach(string fileName in allFiles)
                {
                    HttpPostedFile uploadedFile = allFiles[fileName];
                    responseData.message += " " + uploadedFile.FileName;
                    FileManager.File file = new FileManager.File(uploadedFile);
                    file.Rename();
                    file.Save(HttpContext.Current.Server.MapPath("~/Uploads"));
                }
            }
            response.StatusCode = HttpStatusCode.OK;
            //responseData.data = $"data:{a};base64,{img}";
            var json = JsonConvert.SerializeObject(responseData);
            response.Content = new StringContent(json, Encoding.UTF8, "application/json");
            return response;
        }

        [HttpGet]
        [Route("test/file_download")]
        public HttpResponseMessage FileDownload()
        {
            HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.OK);
            string targetFolder = HttpContext.Current.Server.MapPath("~/Uploads");
            byte[] content = null;
            using (FileStream fs = File.Open(Path.Combine(targetFolder, "test.docx"), FileMode.Open))
            {
                content = new byte[fs.Length];
                fs.Read(content, 0, (int)fs.Length);
            }
            response.Content = new ByteArrayContent(content);
            response.Content.Headers.ContentType = new MediaTypeHeaderValue("application/vnd.openxmlformats-officedocument.wordprocessingml.document");
            response.Content.Headers.ContentDisposition = new ContentDispositionHeaderValue("attachment")
            {
                FileName = "test.docx"
            };
            return response;
        }


        [HttpGet]
        [Route("api/demo/success")]
        [ResponseType(typeof(ResponseFormat))]
        public HttpResponseMessage SuccessedResponse(int id)
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
                    if ((id == userId && new AuthorizationService().SetPerm((int)EnumPermissions.USER_MODIFY_SELF).Authorize(userId)) || (id != userId && new AuthorizationService().SetPerm((int)EnumPermissions.USER_VIEW).Authorize(userId)))
                    {
                        var dbUser = db.USERs.Find(id);
                        if (dbUser != null)
                        {

                        }
                        else
                        {
                            response.StatusCode = HttpStatusCode.Gone;
                            responseData = ResponseFormat.Fail;
                            responseData.message = ErrorMessages.USER_NOT_FOUND;
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
