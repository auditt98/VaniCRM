using Backend.Domain;
using Backend.Extensions;
using Backend.Models.ApiModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Backend.Repository
{
    public class CampaignRepository
    {
        DatabaseContext db = new DatabaseContext();
        TagRepository _tagRepository = new TagRepository();

        public IEnumerable<CAMPAIGN> GetAllCampaigns(string query = "", int pageSize = 0, int currentPage = 1)
        {
            var q = query.ToLower();
            if (pageSize == 0)
            {
                pageSize = db.CAMPAIGNs.Count();
            }
            if (String.IsNullOrEmpty(q))
            {
                return db.CAMPAIGNs.OrderBy(c => c.ID).Skip((currentPage - 1) * pageSize).Take(pageSize);
            }
            var campaigns = db.CAMPAIGNs.Where(c => c.Name.ToLower().Contains(q) || c.CAMPAIGN_TYPE.Name.ToLower().Contains(q)).OrderBy(c => c.ID);
            return campaigns;
        }

        public IEnumerable<CAMPAIGN_STATUS> GetAllCampaignStatuses()
        {
            return db.CAMPAIGN_STATUS;
        }

        public IEnumerable<CAMPAIGN_TYPE> GetAllCampaignTypes()
        {
            return db.CAMPAIGN_TYPE;
        }

        public CAMPAIGN GetOne(int id)
        {
            return db.CAMPAIGNs.Find(id);
        }


        public bool Create(CampaignCreateApiModel apiModel, int createdUser)
        {
            var newCampaign = new CAMPAIGN();
            newCampaign.ActualCost = apiModel.actualCost;
            newCampaign.BudgetedCost = apiModel.budgetedCost;
            newCampaign.CampaignOwner = apiModel.owner;
            newCampaign.CAMPAIGN_STATUS_ID = apiModel.status;
            newCampaign.CAMPAIGN_TYPE_ID = apiModel.type;
            newCampaign.CreatedAt = DateTime.Now;
            newCampaign.CreatedBy = createdUser;
            newCampaign.Description = apiModel.description;
            newCampaign.EndDate = apiModel.endDate;
            newCampaign.ExpectedResponse = apiModel.expectedResponse;
            newCampaign.ExpectedRevenue = apiModel.expectedRevenue;
            newCampaign.Name = apiModel.campaignName;
            newCampaign.NumberSent = apiModel.numberSent;
            newCampaign.StartDate = apiModel.startDate;
            try
            {
                db.CAMPAIGNs.Add(newCampaign);
                db.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool Update(int campaignId, CampaignCreateApiModel apiModel, int modifiedUser)
        {
            var dbCampaign = db.CAMPAIGNs.Find(campaignId);
            if (dbCampaign != null)
            {
                dbCampaign.ActualCost = apiModel.actualCost;
                dbCampaign.BudgetedCost = apiModel.budgetedCost;
                dbCampaign.CampaignOwner = apiModel.owner;
                dbCampaign.CAMPAIGN_STATUS_ID = apiModel.status;
                dbCampaign.CAMPAIGN_TYPE_ID = apiModel.type;
                dbCampaign.Description = apiModel.description;
                dbCampaign.EndDate = apiModel.endDate;
                dbCampaign.ExpectedResponse = apiModel.expectedResponse;
                dbCampaign.ExpectedRevenue = apiModel.expectedRevenue;
                dbCampaign.Name = apiModel.campaignName;
                dbCampaign.NumberSent = apiModel.numberSent;
                dbCampaign.StartDate = apiModel.startDate;
                dbCampaign.ModifiedAt = DateTime.Now;
                dbCampaign.ModifiedBy = modifiedUser;
                db.SaveChanges();
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool Delete(int id)
        {
            var dbCampaign = db.CAMPAIGNs.Find(id);
            if (dbCampaign != null)
            {
                db.CAMPAIGNs.Remove(dbCampaign);
                db.SaveChanges();
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool AddTag(int id, string tagName)
        {
            var dbCampaign = db.CAMPAIGNs.Find(id);
            var dbTag = _tagRepository.GetOneByName(tagName);
            if (dbCampaign != null)
            {
                if (dbTag != null)
                {
                    var tagItem = dbCampaign.TAG_ITEM.Where(c => c.TAG_ID == dbTag.ID).FirstOrDefault();
                    if (tagItem != null)
                    {
                        var newTagItem = new TAG_ITEM();
                        newTagItem.TAG_ID = dbTag.ID;
                        newTagItem.CAMPAIGN_ID = dbCampaign.ID;
                        db.TAG_ITEM.Add(newTagItem);
                        db.SaveChanges();
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                else
                {
                    var newTag = _tagRepository.Create(tagName);
                    var tagItem = new TAG_ITEM();
                    tagItem.TAG_ID = newTag.ID;
                    tagItem.CAMPAIGN_ID = dbCampaign.ID;
                    db.TAG_ITEM.Add(tagItem);
                    db.SaveChanges();
                    return true;
                }
            }
            else
            {
                return false;
            }
        }

        public bool RemoveTag(int id, int tagId)
        {
            var dbCampaign = db.CAMPAIGNs.Find(id);
            if (dbCampaign != null)
            {
                var tagItem = dbCampaign.TAG_ITEM.Where(c => c.TAG.ID == tagId).FirstOrDefault();
                if (tagItem != null)
                {
                    db.TAG_ITEM.Remove(tagItem);
                    db.SaveChanges();
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }
        
        public bool AddContact(int id, CampaignUpdateContactApiModel apiModel, int modifiedUser)
        {
            var dbCampaign = db.CAMPAIGNs.Find(id);
            if (dbCampaign != null)
            {
                var contact = dbCampaign.CAMPAIGN_TARGET.Where(c => c.CONTACT.ID == apiModel.id).FirstOrDefault();
                if(contact != null)
                {
                    return false;
                }
                else
                {
                    var newTarget = new CAMPAIGN_TARGET();
                    newTarget.CAMPAIGN_ID = dbCampaign.ID;
                    newTarget.CONTACT_ID = apiModel.id;
                    db.CAMPAIGN_TARGET.Add(newTarget);
                    dbCampaign.ModifiedAt = DateTime.Now;
                    dbCampaign.ModifiedBy = modifiedUser;
                    db.SaveChanges();
                    return true;
                }

            }
            else
            {
                return false;
            }
        }

        public bool AddLead(int id, CampaignUpdateLeadApiModel apiModel, int modifiedUser)
        {
            var dbCampaign = db.CAMPAIGNs.Find(id);
            if (dbCampaign != null)
            {
                var lead = dbCampaign.CAMPAIGN_TARGET.Where(c => c.LEAD.ID == apiModel.id).FirstOrDefault();
                if (lead != null)
                {
                    return false;
                }
                else
                {
                    var newTarget = new CAMPAIGN_TARGET();
                    newTarget.CAMPAIGN_ID = dbCampaign.ID;
                    newTarget.LEAD_ID = apiModel.id;
                    db.CAMPAIGN_TARGET.Add(newTarget);
                    dbCampaign.ModifiedAt = DateTime.Now;
                    dbCampaign.ModifiedBy = modifiedUser;
                    db.SaveChanges();
                    return true;
                }

            }
            else
            {
                return false;
            }
        }

        public bool DeleteLead(int id, int leadId, int modifiedUser)
        {
            var dbCampaign = db.CAMPAIGNs.Find(id);
            if (dbCampaign != null)
            {
                var campaignLead = dbCampaign.CAMPAIGN_TARGET.Where(c => c.LEAD.ID == leadId).FirstOrDefault();
                if (campaignLead != null)
                {
                    db.CAMPAIGN_TARGET.Remove(campaignLead);
                    dbCampaign.ModifiedAt = DateTime.Now;
                    dbCampaign.ModifiedBy = modifiedUser;
                    db.SaveChanges();
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }

        public bool DeleteContact(int id, int contactId, int modifiedUser)
        {
            var dbCampaign = db.CAMPAIGNs.Find(id);
            if (dbCampaign != null)
            {
                var campaignContact = dbCampaign.CAMPAIGN_TARGET.Where(c => c.CONTACT.ID == contactId).FirstOrDefault();
                if (campaignContact != null)
                {
                    db.CAMPAIGN_TARGET.Remove(campaignContact);
                    dbCampaign.ModifiedAt = DateTime.Now;
                    dbCampaign.ModifiedBy = modifiedUser;
                    db.SaveChanges();
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }
    
        public (IEnumerable<CONTACT> contacts, Pager p) GetContacts(int id, string query = "", int pageSize = 0, int currentPage = 1)
        {
            var dbCampaign = db.CAMPAIGNs.Find(id);
            var contactList = dbCampaign.CAMPAIGN_TARGET.Where(c => c.CONTACT != null).Select(c => c.CONTACT).ToList();
            if (dbCampaign != null)
            {
                var q = query.ToLower();

                if (pageSize == 0)
                {
                    pageSize = 10;
                }
                if (String.IsNullOrEmpty(q))
                {
                    Pager pager = new Pager(contactList.Count(), currentPage, pageSize, 9999);
                    return (contactList.OrderBy(c => c.ID).Skip((currentPage - 1) * pageSize).Take(pageSize), pager);
                }
                else
                {
                    var result = contactList.Where(c => c.Name.ToLower().Contains(q.ToLower()) || c.Phone.Contains(q) || c.Email.ToLower().Contains(q.ToLower()) || c.Mobile.Contains(q)).OrderBy(c => c.ID);
                    if (result.Count() > 0)
                    {
                        Pager p = new Pager(result.Count(), currentPage, pageSize, 9999);

                        return (result.Skip((currentPage - 1) * pageSize).Take(pageSize), p);
                    }
                    else
                    {
                        return (result, null);
                    }
                }
            }
            else
            {
                return (null, null);
            }
        }

        public (IEnumerable<LEAD> leads, Pager p) GetLeads(int id, string query = "", int pageSize = 0, int currentPage = 1)
        {
            var dbCampaign = db.CAMPAIGNs.Find(id);
            var leadList = dbCampaign.CAMPAIGN_TARGET.Where(c => c.LEAD != null).Select(c => c.LEAD).ToList();
            if (dbCampaign != null)
            {
                var q = query.ToLower();

                if (pageSize == 0)
                {
                    pageSize = 10;
                }
                if (String.IsNullOrEmpty(q))
                {
                    Pager pager = new Pager(leadList.Count(), currentPage, pageSize, 9999);
                    return (leadList.OrderBy(c => c.ID).Skip((currentPage - 1) * pageSize).Take(pageSize), pager);
                }
                else
                {
                    var result = leadList.Where(c => c.Name.ToLower().Contains(q) || c.CompanyName.ToLower().Contains(q) || c.Email.ToLower().Contains(q) || c.Phone.Contains(q) || c.LEAD_SOURCE.Name.ToLower().Contains(q) || c.PRIORITY.Name.ToLower().Contains(q)).OrderBy(c => c.ID);
                    if (result.Count() > 0)
                    {
                        Pager p = new Pager(result.Count(), currentPage, pageSize, 9999);

                        return (result.Skip((currentPage - 1) * pageSize).Take(pageSize), p);
                    }
                    else
                    {
                        return (result, null);
                    }
                }
            }
            else
            {
                return (null, null);
            }
        }

    }
}