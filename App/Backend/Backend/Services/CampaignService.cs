﻿using Backend.Extensions;
using Backend.Models.ApiModel;
using Backend.Repository;
using Backend.Resources;
using Backend.Validators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Backend.Services
{
    public class CampaignService
    {
        CampaignRepository _campaignRepository = new CampaignRepository();
        CampaignValidator _campaignValidator = new CampaignValidator();

        public CampaignListApiModel GetCampaignList(string query = "", int pageSize = 0, int currentPage = 1, List<string> sort = null)
        {
            var dbCampaigns = _campaignRepository.GetAllCampaigns(query, pageSize, currentPage, sort);
            var apiModel = new CampaignListApiModel();

            apiModel.campaigns = dbCampaigns.campaigns.Select(c => new CampaignListApiModel.CampaignInfo() { id = c.ID, name = c.Name, type = c.CAMPAIGN_TYPE != null ? c.CAMPAIGN_TYPE.Name : "", endDate = c.EndDate.GetValueOrDefault(), startDate = c.StartDate.GetValueOrDefault(), status = c.CAMPAIGN_STATUS != null ? c.CAMPAIGN_STATUS.Name : "", owner = c.Owner.Username }).ToList();
            apiModel.pageInfo = dbCampaigns.p;
            return apiModel;
        }

        public CampaignBlankApiModel PrepareNewCampaign()
        {
            var apiModel = new CampaignBlankApiModel();
          
            apiModel.statuses = _campaignRepository.GetAllCampaignStatuses().Select(c => new CampaignStatus() { id = c.ID, name = c.Name, selected = false }).ToList();
            apiModel.types = _campaignRepository.GetAllCampaignTypes().Select(c => new CampaignType() { id = c.ID, name = c.Name, selected = false }).ToList();

            return apiModel;
        }

        public bool Create(CampaignCreateApiModel apiModel, int createdUser)
        {
            var validator = _campaignValidator.Validate(apiModel);
            if (validator.IsValid)
            {
                return _campaignRepository.Create(apiModel, createdUser);
            }
            return false;
        }

        public int FindOwnerId(int campaignId)
        {
            var dbCampaign= _campaignRepository.GetOne(campaignId);
            if (dbCampaign != null)
            {
                return dbCampaign.Owner.ID;
            }
            else { return 0; }
        }

        public bool Update(int id, CampaignCreateApiModel apiModel, int modifiedUser)
        {
            var validator = _campaignValidator.Validate(apiModel);

            if (validator.IsValid)
            {
                return _campaignRepository.Update(id, apiModel, modifiedUser);
            }
            return false;
        }

        public bool Delete(int id)
        {
            return _campaignRepository.Delete(id);
        }

        public bool AddTag(int id, string tagName)
        {
            return _campaignRepository.AddTag(id, tagName);
        }

        public bool RemoveTag(int id, int tagId)
        {
            return _campaignRepository.RemoveTag(id, tagId);
        }

        public (bool isAdded, string message) AddContact(int id, int contactId, int modifiedUser)
        {
            return _campaignRepository.AddContact(id, contactId, modifiedUser);
        }

        public (bool isAdded, string message) AddLead(int id, int leadId, int modifiedUser)
        {
            return _campaignRepository.AddLead(id, leadId, modifiedUser);
        }

        public bool DeleteLead(int id, int leadId, int modifiedUser)
        {
            return _campaignRepository.DeleteLead(id, leadId, modifiedUser);
        }

        public bool DeleteContact(int id, int contactId, int modifiedUser)
        {
            return _campaignRepository.DeleteContact(id, contactId, modifiedUser);
        }

        public CampaignDetailApiModel GetOne(int id)
        {
            var dbCampaign = _campaignRepository.GetOne(id);
            if (dbCampaign != null)
            {
                var apiModel = new CampaignDetailApiModel();
                apiModel.actualCost = dbCampaign.ActualCost.GetValueOrDefault();
                apiModel.budgetedCost = dbCampaign.BudgetedCost.GetValueOrDefault();
                apiModel.campaignName = dbCampaign.Name;
                apiModel.createdAt = dbCampaign.CreatedAt.GetValueOrDefault();
                apiModel.modifiedAt = dbCampaign.ModifiedAt.GetValueOrDefault();
                if(dbCampaign.CreatedUser != null)
                {
                    apiModel.createdBy = new UserLinkApiModel() { id = dbCampaign.CreatedUser.ID, username = dbCampaign.CreatedUser.Username, email = dbCampaign.CreatedUser.Email };
                }
                if(dbCampaign.ModifiedUser != null)
                {

                    apiModel.modifiedBy = new UserLinkApiModel() { id = dbCampaign.ModifiedUser.ID, username = dbCampaign.ModifiedUser.Username, email = dbCampaign.ModifiedUser.Email };
                }
                apiModel.id = dbCampaign.ID;
                apiModel.description = dbCampaign.Description;
                apiModel.emailTitle = dbCampaign.EmailTitle;
                apiModel.startDate = dbCampaign.StartDate.GetValueOrDefault();
                apiModel.endDate = dbCampaign.EndDate.GetValueOrDefault();
                apiModel.expectedResponse = dbCampaign.ExpectedResponse.GetValueOrDefault();
                apiModel.numberSent = dbCampaign.NumberSent.GetValueOrDefault();

                apiModel.types = _campaignRepository.GetAllCampaignTypes().Select(c => new CampaignType() { id = c.ID, name = c.Name, selected = dbCampaign.CAMPAIGN_TYPE != null ? c.ID == dbCampaign.CAMPAIGN_TYPE.ID : false }).ToList();

                apiModel.statuses = _campaignRepository.GetAllCampaignStatuses().Select(c => new CampaignStatus() { id = c.ID, name = c.Name, selected = dbCampaign.CAMPAIGN_STATUS != null ? c.ID == dbCampaign.CAMPAIGN_STATUS.ID : false }).ToList();
                if(dbCampaign.Owner != null)
                {
                    apiModel.owner = new UserLinkApiModel() { id = dbCampaign.Owner.ID, username = dbCampaign.Owner.Username, email = dbCampaign.Owner.Email };
                }
                apiModel.tags = dbCampaign.TAG_ITEM.Select(c => new TagApiModel() { id = c.TAG.ID, name = c.TAG.Name }).ToList();
                apiModel.notes = dbCampaign.NOTEs.Select(c => new NoteApiModel() { id = c.ID, body = c.NoteBody, createdAt = c.CreatedAt.GetValueOrDefault(), createdBy = new UserLinkApiModel() { id = c.USER.ID, username = c.USER.Username, email = c.USER.Email }, files = c.FILEs.Select(f => new FileApiModel() { id = f.ID, fileName = f.FileName, size = f.FileSize.Value.ToString() + " KB", url = StaticStrings.ServerHost + "files/" + f.ID }).ToList() }).ToList();

                return apiModel;
            }
            else
            {
                return null;
            }
        }
    
        public ContactListApiModel GetContacts(int id, string query = "", int pageSize = 0, int currentPage = 1)
        {
            var dbContacts = _campaignRepository.GetContacts(id, query, pageSize, currentPage);
            var contacts = new ContactListApiModel();
            contacts.contacts = dbContacts.contacts.Select(c => new ContactListApiModel.ContactInfo() {id = c.ID, accountName = c.ACCOUNT?.Name, contactName = c.Name, email = c.Email, owner = c.Owner?.Username, phone = c.Phone }).ToList();
            contacts.pageInfo = dbContacts.p;
            return contacts;
        }

        public LeadListApiModel GetLeads(int id, string query = "", int pageSize = 0, int currentPage = 1)
        {
            var dbLeads = _campaignRepository.GetLeads(id, query, pageSize, currentPage);
            var leads = new LeadListApiModel();
            leads.leads = dbLeads.leads.Select(c => new LeadListApiModel.LeadInfo() { id = c.ID, companyName = c.CompanyName, email = c.Email, leadOwner = c.Owner?.Username, leadSource = c.LEAD_SOURCE?.Name, name = c.Name, phone = c.Phone, priority = c.PRIORITY?.Name}).ToList();
            leads.pageInfo = dbLeads.p;
            return leads;
        }

        public (bool isSent, string error) SendEmail(int id)
        {
            var campaign = _campaignRepository.GetOne(id);
            
            var emailManager = new EmailManager();
            emailManager.Title = campaign.EmailTitle;
            emailManager.Content = campaign.Description;
            emailManager.Recipients = new List<string>();
            foreach (var target in campaign.CAMPAIGN_TARGET)
            {
                if (target.LEAD_ID != null)
                {
                    if(target.LEAD_ID != 0)
                    {
                        if (!string.IsNullOrEmpty(target.LEAD.Email))
                        {
                            emailManager.Recipients.Add(target.LEAD.Email);
                        }
                    }
                    
                }
                else if (target.CONTACT_ID != null)
                {
                    if(target.CONTACT_ID != 0)
                    {
                        if (!string.IsNullOrEmpty(target.CONTACT.Email))
                        {
                            emailManager.Recipients.Add(target.CONTACT.Email);
                        }
                    }
                    
                }
            }

            var result = emailManager.SendBatch();
            return (result.isSent, result.error);
        }
    }
}