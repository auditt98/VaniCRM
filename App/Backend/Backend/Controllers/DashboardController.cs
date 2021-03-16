﻿using Backend.Domain;
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
using static Backend.Extensions.Enum;

namespace Backend.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class DashboardController : ApiController
    {
        public DatabaseContext db = new DatabaseContext();
        public UserService _userService = new UserService();

        [HttpGet]
        [Route("dashboard")]
        public HttpResponseMessage ViewSaleDashboard()
        {
            var response = new HttpResponseMessage();
            ResponseFormat responseData = new ResponseFormat();
            AuthorizationService _authorizationService = new AuthorizationService().SetPerm((int)EnumPermissions.DEAL_VIEW_LIST);
            IEnumerable<string> headerValues = Request.Headers.GetValues("Authorization");
            if (headerValues == null)
            {
                response.StatusCode = HttpStatusCode.Forbidden;
                responseData = ResponseFormat.Fail;
                responseData.message = ErrorMessages.UNAUTHORIZED;
            }
            else
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
                    var userId = payload["id"];

                    var isAuthorized = _authorizationService.Authorize(Convert.ToInt32(userId));
                    if (isAuthorized)
                    {
                        response.StatusCode = HttpStatusCode.OK;
                        responseData = ResponseFormat.Success;
                        var deals = db.DEALs.ToList();
                        
                        DashboardApiModel apiModel = new DashboardApiModel();
                        apiModel.qualified = new DashboardApiModel.S();

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
                        apiModel.qualified.stageID = qualifiedStage.ID;
                        apiModel.qualified.stageName = qualifiedStage.Name;
                        apiModel.qualified.probability = qualifiedStage.Probability.Value;
                        //value proposition
                        apiModel.valueProposition.stageID = valuePropositionStage.ID;
                        apiModel.valueProposition.stageName = valuePropositionStage.Name;
                        apiModel.valueProposition.probability = valuePropositionStage.Probability.Value;
                        //find key contacts
                        apiModel.findKeyContacts.stageID = findKeyContactsStage.ID;
                        apiModel.findKeyContacts.stageName = findKeyContactsStage.Name;
                        apiModel.findKeyContacts.probability = findKeyContactsStage.Probability.Value;
                        //send proposal
                        apiModel.sendProposal.stageID = sendProposalStage.ID;
                        apiModel.sendProposal.stageName = sendProposalStage.Name;
                        apiModel.sendProposal.probability = sendProposalStage.Probability.Value;
                        //review 
                        apiModel.review.stageID = reviewStage.ID;
                        apiModel.review.stageName = reviewStage.Name;
                        apiModel.review.probability = reviewStage.Probability.Value;
                        //negotiate
                        apiModel.negotiate.stageID = negotiateStage.ID;
                        apiModel.negotiate.stageName = negotiateStage.Name;
                        apiModel.negotiate.probability = negotiateStage.Probability.Value;
                        //won
                        apiModel.won.stageID = wonStage.ID;
                        apiModel.won.stageName = wonStage.Name;
                        apiModel.won.probability = wonStage.Probability.Value;
                        //lost
                        apiModel.lost.stageID = lostStage.ID;
                        apiModel.lost.stageName = lostStage.Name;
                        apiModel.lost.probability = lostStage.Probability.Value;
                        #endregion

                        foreach(var deal in deals)
                        {
                            var d = new DashboardApiModel.D();
                            d.dealID = deal.ID;
                            d.dealName = deal.Name;
                            d.ownerID = deal.Owner.ID;
                            d.ownerUsername = deal.Owner.Username;
                            d.accountID = deal.ACCOUNT.ID;
                            d.accountName = deal.ACCOUNT.Name;
                            foreach(var tag in deal.TAG_ITEM)
                            {
                                var t = new DashboardApiModel.T();
                                t.tagID = tag.TAG.ID;
                                t.tagName = tag.TAG.Name;
                                d.tags.Add(t);
                            }
                            var history = deal.STAGE_HISTORY.OrderByDescending(sh => sh.ModifiedAt).Take(1);
                            if(history.Count() != 0)
                            {
                                var stage = history.Select(c => c.STAGE_ID).First();
                                if(stage == (int)EnumStage.QUALIFIED)
                                {
                                    apiModel.qualified.deals.Add(d);
                                }
                                if (stage == (int)EnumStage.VALUE_PROPOSITION)
                                {
                                    apiModel.valueProposition.deals.Add(d);
                                }
                                if (stage == (int)EnumStage.FIND_KEY_CONTACTS)
                                {
                                    apiModel.findKeyContacts.deals.Add(d);
                                }
                                if (stage == (int)EnumStage.SEND_PROPOSAL)
                                {
                                    apiModel.sendProposal.deals.Add(d);
                                }
                                if (stage == (int)EnumStage.REVIEW)
                                {
                                    apiModel.review.deals.Add(d);
                                }
                                if (stage == (int)EnumStage.NEGOTIATE)
                                {
                                    apiModel.negotiate.deals.Add(d);
                                }
                                if (stage == (int)EnumStage.WON)
                                {
                                    apiModel.won.deals.Add(d);
                                }
                                if (stage == (int)EnumStage.LOST)
                                {
                                    apiModel.lost.deals.Add(d);
                                }
                            }

                        }

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

            var json = JsonConvert.SerializeObject(responseData);
            response.Content = new StringContent(json, Encoding.UTF8, "application/json");
            return response;

        }

    }
}