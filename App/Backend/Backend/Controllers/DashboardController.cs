using Backend.Domain;
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
using System.Web.Http.Cors;
using System.Web.Http.Description;
using static Backend.Extensions.Enum;

namespace Backend.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class DashboardController : ApiController
    {
        public DatabaseContext db = new DatabaseContext();
        public UserService _userService = new UserService();

        [HttpGet]
        [Route("sale_dashboard")]
        [ResponseType(typeof(DashboardApiModel))]
        public HttpResponseMessage ViewSaleDashboard()
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
                    var userId = payload["id"];

                    var isAuthorized = _authorizationService.Authorize(Convert.ToInt32(userId));
                    if (isAuthorized)
                    {
                        response.StatusCode = HttpStatusCode.OK;
                        responseData = ResponseFormat.Success;
                        var deals = db.DEALs.ToList();

                        DashboardApiModel apiModel = new DashboardApiModel();
                        apiModel.stages = new List<DashboardApiModel.S>();

                        var qualified = new DashboardApiModel.S();
                        var valueProposition = new DashboardApiModel.S();
                        var findKeyContacts = new DashboardApiModel.S();
                        var sendProposal = new DashboardApiModel.S();
                        var review = new DashboardApiModel.S();
                        var negotiate = new DashboardApiModel.S();
                        var won = new DashboardApiModel.S();
                        var lost = new DashboardApiModel.S();


                        var qualifiedStage = db.STAGEs.Find((int)EnumStage.QUALIFIED);
                        var valuePropositionStage = db.STAGEs.Find((int)EnumStage.VALUE_PROPOSITION);
                        var findKeyContactsStage = db.STAGEs.Find((int)EnumStage.FIND_KEY_CONTACTS);
                        var sendProposalStage = db.STAGEs.Find((int)EnumStage.SEND_PROPOSAL);
                        var reviewStage = db.STAGEs.Find((int)EnumStage.REVIEW);
                        var negotiateStage = db.STAGEs.Find((int)EnumStage.NEGOTIATE);
                        var wonStage = db.STAGEs.Find((int)EnumStage.WON);
                        var lostStage = db.STAGEs.Find((int)EnumStage.LOST);
                        #region stages
                        //qualified
                        qualified.stageID = qualifiedStage.ID;
                        qualified.stageName = qualifiedStage.Name;
                        qualified.probability = qualifiedStage.Probability.Value;
                        //value proposition
                        valueProposition.stageID = valuePropositionStage.ID;
                        valueProposition.stageName = valuePropositionStage.Name;
                        valueProposition.probability = valuePropositionStage.Probability.Value;
                        //find key contacts
                        findKeyContacts.stageID = findKeyContactsStage.ID;
                        findKeyContacts.stageName = findKeyContactsStage.Name;
                        findKeyContacts.probability = findKeyContactsStage.Probability.Value;
                        //send proposal
                        sendProposal.stageID = sendProposalStage.ID;
                        sendProposal.stageName = sendProposalStage.Name;
                        sendProposal.probability = sendProposalStage.Probability.Value;
                        //review 
                        review.stageID = reviewStage.ID;
                        review.stageName = reviewStage.Name;
                        review.probability = reviewStage.Probability.Value;
                        //negotiate
                        negotiate.stageID = negotiateStage.ID;
                        negotiate.stageName = negotiateStage.Name;
                        negotiate.probability = negotiateStage.Probability.Value;
                        //won
                        won.stageID = wonStage.ID;
                        won.stageName = wonStage.Name;
                        won.probability = wonStage.Probability.Value;
                        //lost
                        lost.stageID = lostStage.ID;
                        lost.stageName = lostStage.Name;
                        lost.probability = lostStage.Probability.Value;
                        #endregion

                        foreach (var deal in deals)
                        {
                            var d = new DashboardApiModel.D();
                            d.dealID = deal.ID;
                            d.dealName = deal.Name;
                            d.ownerID = deal.Owner.ID;
                            d.ownerUsername = deal.Owner.Username;
                            d.accountID = deal.ACCOUNT != null ? deal.ACCOUNT.ID : 0;
                            d.accountName = deal.ACCOUNT != null ? deal.ACCOUNT.Name : "";
                            d.expectedRevenue = deal.ExpectedRevenue.HasValue ? deal.ExpectedRevenue.Value : 0 ;
                            d.priority = deal.PRIORITY != null ? deal.PRIORITY.Name : "";

                            foreach (var tag in deal.TAG_ITEM)
                            {
                                var t = new DashboardApiModel.T();
                                t.tagID = tag.TAG.ID;
                                t.tagName = tag.TAG.Name;
                                d.tags.Add(t);
                            }
                            var history = deal.STAGE_HISTORY.OrderByDescending(sh => sh.ModifiedAt).Take(1);
                            if (history.Count() != 0)
                            {
                                var stage = history.Select(c => c.STAGE_ID).First();
                                if (stage == (int)EnumStage.QUALIFIED)
                                {
                                    qualified.deals.Add(d);
                                }
                                if (stage == (int)EnumStage.VALUE_PROPOSITION)
                                {
                                    valueProposition.deals.Add(d);
                                }
                                if (stage == (int)EnumStage.FIND_KEY_CONTACTS)
                                {
                                    findKeyContacts.deals.Add(d);
                                }
                                if (stage == (int)EnumStage.SEND_PROPOSAL)
                                {
                                    sendProposal.deals.Add(d);
                                }
                                if (stage == (int)EnumStage.REVIEW)
                                {
                                    review.deals.Add(d);
                                }
                                if (stage == (int)EnumStage.NEGOTIATE)
                                {
                                    negotiate.deals.Add(d);
                                }
                                if (stage == (int)EnumStage.WON)
                                {
                                    won.deals.Add(d);
                                }
                                if (stage == (int)EnumStage.LOST)
                                {
                                    lost.deals.Add(d);
                                }
                            }

                        }

                        apiModel.stages.AddRange(new List<DashboardApiModel.S>() { qualified, valueProposition, findKeyContacts, sendProposal, review, negotiate, won, lost });
                        responseData.data = apiModel;
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