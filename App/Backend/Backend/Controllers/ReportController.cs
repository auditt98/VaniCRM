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
using System.Net.Http.Headers;
using System.Text;
using System.Web.Http;
using System.Web.Http.Description;

namespace Backend.Controllers
{
    public class ReportController : ApiController
    {
        public ReportService _reportService = new ReportService();
        public FileService _fileService = new FileService();

        [HttpGet]
        [Route("reports/amount_by_stage")]
        [ResponseType(typeof(ChartReportApiModel))]
        public HttpResponseMessage GetAmountByStageReport()
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
                    response.StatusCode = HttpStatusCode.OK;
                    responseData = ResponseFormat.Success;
                    responseData.data = _reportService.GetAmountByStageReport();
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

        [HttpGet]
        [Route("reports/top_sales")]
        [ResponseType(typeof(ChartReportApiModel))]
        public HttpResponseMessage GetTopSalesReport()
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
                    response.StatusCode = HttpStatusCode.OK;
                    responseData = ResponseFormat.Success;
                    responseData.data = _reportService.GetTopSalesReport();
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

        [HttpGet]
        [Route("reports/top_marketings")]
        [ResponseType(typeof(ChartReportApiModel))]
        public HttpResponseMessage GetTopMarketingsReport()
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
                    response.StatusCode = HttpStatusCode.OK;
                    responseData = ResponseFormat.Success;
                    responseData.data = _reportService.GetTopMarketingsReport();
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

        [HttpGet]
        [Route("reports/key_accounts")]
        [ResponseType(typeof(ChartReportApiModel))]
        public HttpResponseMessage GetKeyAccountsReport()
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
                    response.StatusCode = HttpStatusCode.OK;
                    responseData = ResponseFormat.Success;
                    responseData.data = _reportService.GetKeyAccountsReport();
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

        [HttpGet]
        [Route("reports/accounts_by_industry")]
        [ResponseType(typeof(ChartReportApiModel))]
        public HttpResponseMessage GetAccountsByIndustryReport()
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
                    response.StatusCode = HttpStatusCode.OK;
                    responseData = ResponseFormat.Success;
                    responseData.data = _reportService.GetAccountsByIndustryReport();
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

        [HttpGet]
        [Route("reports/revenue_comparison")]
        [ResponseType(typeof(ChartReportApiModel))]
        public HttpResponseMessage GetRevenueComparisonReport()
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
                    response.StatusCode = HttpStatusCode.OK;
                    responseData = ResponseFormat.Success;
                    responseData.data = _reportService.GetRevenueComparisonReport();
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

        [HttpGet]
        [Route("reports/exportables")]
        [ResponseType(typeof(ExportablesApiModel))]
        public HttpResponseMessage GetExportables([FromUri] int currentPage = 1, [FromUri] int pageSize = 0)
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
                    response.StatusCode = HttpStatusCode.OK;
                    responseData = ResponseFormat.Success;
                    responseData.data = _reportService.GetExportables(currentPage, pageSize);
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

        [HttpGet]
        [Route("reports/exportables/{name}")]
        public HttpResponseMessage GetPdf([FromUri] string name, [FromUri] DateTime? fromDate = null, [FromUri] DateTime? toDate = null)
        {
            HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.OK);

            if (!String.IsNullOrEmpty(name))
            {

                switch (name)
                {
                    case "leads":
                        {
                            //responseData.data = _reportService.Get
                            var reportName = "PotentialCustomerReport_" + DateTime.Now.ToString("dd'-'MM'-'yyyy") + ".pdf";
                            var pdfManager = new PdfManager(reportName);
                            var fileName = pdfManager.GenerateLeadsReport().fileName;
                            var results = _fileService.GetFile(fileName);
                            if (results.file != null)
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
                            break;
                        }
                    case "accounts":
                        {
                            //responseData.data = _reportService.Get
                            var reportName = "CustomerReport_" + DateTime.Now.ToString("dd'-'MM'-'yyyy") + ".pdf";
                            var pdfManager = new PdfManager(reportName);
                            var fileName = pdfManager.GenerateAccountsReport().fileName;
                            var results = _fileService.GetFile(fileName);
                            if (results.file != null)
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
                            break;
                        }
                    case "deals":
                        {
                            var reportName = "DealsReport_" + DateTime.Now.ToString("dd'-'MM'-'yyyy") + ".pdf";
                            var pdfManager = new PdfManager(reportName);
                            var fileName = pdfManager.GenerateDealsReport(fromDate, toDate).fileName;
                            var results = _fileService.GetFile(fileName);
                            if (results.file != null)
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
                            break;
                        }
                    case "revenue":
                        {
                            var reportName = "RevenueReport_" + DateTime.Now.ToString("dd'-'MM'-'yyyy") + ".pdf";
                            var pdfManager = new PdfManager(reportName);
                            var fileName = pdfManager.GenerateRevenueReport(fromDate, toDate).fileName;
                            var results = _fileService.GetFile(fileName);
                            if (results.file != null)
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
                            break;
                        }

                    case "campaigns":
                        {
                            //responseData.data = _reportService.Get
                            var reportName = "CampaignReport_" + DateTime.Now.ToString("dd'-'MM'-'yyyy") + ".pdf";
                            var pdfManager = new PdfManager(reportName);
                            var fileName = pdfManager.GenerateCampaignsReport().fileName;
                            var results = _fileService.GetFile(fileName);
                            if (results.file != null)
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
                            break;
                        }

                    default:
                        break;
                }

            }
            else
            {
                response.StatusCode = HttpStatusCode.BadRequest;
                ResponseFormat responseData = ResponseFormat.Fail;
                responseData.message = ErrorMessages.INVALID_BODY;
                var json = JsonConvert.SerializeObject(responseData);
                response.Content = new StringContent(json, Encoding.UTF8, "application/json");
            }
            return response;
        }
    }
}
