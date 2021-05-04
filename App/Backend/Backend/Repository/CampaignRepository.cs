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


        public (IEnumerable<CAMPAIGN> campaigns, Pager p) GetAllCampaigns(string query = "", int pageSize = 0, int currentPage = 1)
        {
            var q = query.ToLower();

            if (pageSize == 0)
            {
                pageSize = 10;
            }

            if (String.IsNullOrEmpty(q))
            {
                Pager pager = new Pager(db.CAMPAIGNs.Count(), currentPage, pageSize, 9999);
                return (db.CAMPAIGNs.OrderByDescending(c => c.ID).Skip((currentPage - 1) * pageSize).Take(pageSize), pager);
            }
            var campaigns = db.CAMPAIGNs.Where(c => c.Name.ToLower().Contains(q) || c.CAMPAIGN_TYPE.Name.ToLower().Contains(q)).OrderByDescending(c => c.ID);
            if (campaigns.Count() > 0)
            {
                Pager p = new Pager(campaigns.Count(), currentPage, pageSize, 9999);

                return (campaigns.Skip((currentPage - 1) * pageSize).Take(pageSize), p);
            }
            else
            {
                return (campaigns, null);
            }
        }

        //public IEnumerable<CAMPAIGN> GetAllCampaigns(string query = "", int pageSize = 0, int currentPage = 1)
        //{
        //    var q = query.ToLower();
        //    if (pageSize == 0)
        //    {
        //        pageSize = db.CAMPAIGNs.Count();
        //    }
        //    if (String.IsNullOrEmpty(q))
        //    {
        //        return db.CAMPAIGNs.OrderBy(c => c.ID).Skip((currentPage - 1) * pageSize).Take(pageSize);
        //    }
        //    var campaigns = db.CAMPAIGNs.Where(c => c.Name.ToLower().Contains(q) || c.CAMPAIGN_TYPE.Name.ToLower().Contains(q)).OrderBy(c => c.ID);
        //    return campaigns;
        //}

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
            newCampaign.CampaignOwner = apiModel.owner != 0 ? apiModel.owner : createdUser;
            var dbStatus = db.CAMPAIGN_STATUS.Find(apiModel.status);
            if(dbStatus != null)
            {
                newCampaign.CAMPAIGN_STATUS_ID = apiModel.status;
            }
            if(db.CAMPAIGN_TYPE.Find(apiModel.type) != null)
            {
                newCampaign.CAMPAIGN_TYPE_ID = apiModel.type;
            }
            newCampaign.CreatedAt = DateTime.Now;
            newCampaign.CreatedBy = createdUser;
            newCampaign.Description = apiModel.description;
            newCampaign.EndDate = DbDateHelper.ToNullIfTooEarlyForDb(apiModel.endDate);
            newCampaign.ExpectedResponse = apiModel.expectedResponse;
            newCampaign.ExpectedRevenue = apiModel.expectedRevenue;
            newCampaign.Name = apiModel.campaignName;
            newCampaign.NumberSent = apiModel.numberSent;
            newCampaign.StartDate = DbDateHelper.ToNullIfTooEarlyForDb(apiModel.startDate);
            try
            {
                db.CAMPAIGNs.Add(newCampaign);
                db.SaveChanges();

                var owner = db.USERs.Find(newCampaign.CampaignOwner);
                var creator = db.USERs.Find(createdUser);

                var notifyModel = new NotificationApiModel();
                notifyModel.title = "Campaign created";
                notifyModel.content = $"Campaign {newCampaign.Name} has been created by {creator?.Username}.";
                notifyModel.createdAt = DateTime.Now;
                notifyModel.module = "campaigns";
                notifyModel.moduleObjectId = newCampaign.ID;
                NotificationManager.SendNotification(notifyModel, new List<USER> { owner, creator });
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
                if(apiModel.owner != 0)
                {
                    dbCampaign.CampaignOwner = apiModel.owner;
                }
                if (apiModel.status != 0)
                {
                    dbCampaign.CAMPAIGN_STATUS_ID = apiModel.status;

                }


                if(apiModel.type != 0)
                {
                    dbCampaign.CAMPAIGN_TYPE_ID = apiModel.type;
                }
                dbCampaign.Description = apiModel.description;
                dbCampaign.EndDate = DbDateHelper.ToNullIfTooEarlyForDb(apiModel.endDate);
                dbCampaign.ExpectedResponse = apiModel.expectedResponse;
                dbCampaign.ExpectedRevenue = apiModel.expectedRevenue;
                dbCampaign.Name = apiModel.campaignName;
                dbCampaign.NumberSent = apiModel.numberSent;
                dbCampaign.StartDate = DbDateHelper.ToNullIfTooEarlyForDb(apiModel.startDate);
                dbCampaign.ModifiedAt = DateTime.Now;
                dbCampaign.ModifiedBy = modifiedUser;
                db.SaveChanges();

                var owner = db.USERs.Find(dbCampaign.CampaignOwner);
                var creator = db.USERs.Find(dbCampaign.CreatedBy);
                var modifyUser = db.USERs.Find(modifiedUser);

                var notifyModel = new NotificationApiModel();
                notifyModel.title = "Campaign updated";
                notifyModel.content = $"Campaign {dbCampaign.Name} has been updated by {modifyUser?.Username}.";
                notifyModel.createdAt = DateTime.Now;
                notifyModel.module = "campaigns";
                notifyModel.moduleObjectId = dbCampaign.ID;
                NotificationManager.SendNotification(notifyModel, new List<USER> { owner, creator });
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
                var campaignName = dbCampaign.Name;
                var owner = db.USERs.Find(dbCampaign.CampaignOwner);
                var creator = db.USERs.Find(dbCampaign.CreatedBy);

                db.CAMPAIGNs.Remove(dbCampaign);
                db.SaveChanges();

                var notifyModel = new NotificationApiModel();
                notifyModel.title = "Campaign deleted";
                notifyModel.content = $"Campaign {campaignName} has been deleted.";
                notifyModel.createdAt = DateTime.Now;
                NotificationManager.SendNotification(notifyModel, new List<USER> { owner, creator });
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

                        var owner = db.USERs.Find(dbCampaign.CampaignOwner);
                        var creator = db.USERs.Find(dbCampaign.CreatedBy);

                        var notifyModel = new NotificationApiModel();
                        notifyModel.title = "Tag added";
                        notifyModel.content = $"Tag  {tagName} has been added to campaign {dbCampaign.Name}.";
                        notifyModel.createdAt = DateTime.Now;
                        NotificationManager.SendNotification(notifyModel, new List<USER> { owner, creator });

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

                    var owner = db.USERs.Find(dbCampaign.CampaignOwner);
                    var creator = db.USERs.Find(dbCampaign.CreatedBy);

                    var notifyModel = new NotificationApiModel();
                    notifyModel.title = "Tag added";
                    notifyModel.content = $"Tag {tagName} has been added to campaign {dbCampaign.Name}.";
                    notifyModel.createdAt = DateTime.Now;
                    NotificationManager.SendNotification(notifyModel, new List<USER> { owner, creator });
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
                    var tagName = tagItem.TAG.Name;
                    db.TAG_ITEM.Remove(tagItem);
                    db.SaveChanges();

                    var owner = db.USERs.Find(dbCampaign.CampaignOwner);
                    var creator = db.USERs.Find(dbCampaign.CreatedBy);

                    var notifyModel = new NotificationApiModel();
                    notifyModel.title = "Tag removed";
                    notifyModel.content = $"Tag {tagName} has been removed from campaign {dbCampaign.Name}.";
                    notifyModel.createdAt = DateTime.Now;
                    NotificationManager.SendNotification(notifyModel, new List<USER> { owner, creator });
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

                    var owner = db.USERs.Find(dbCampaign.CampaignOwner);
                    var creator = db.USERs.Find(dbCampaign.CreatedBy);
                    var modifyUser = db.USERs.Find(modifiedUser);
                    var contactAdded = db.CONTACTs.Find(apiModel.id);

                    var notifyModel = new NotificationApiModel();
                    notifyModel.title = "Campaign target added";
                    notifyModel.content = $"Contact {contactAdded.Name} has been added to campaign {dbCampaign.Name} by {modifyUser.Username}.";
                    notifyModel.createdAt = DateTime.Now;
                    NotificationManager.SendNotification(notifyModel, new List<USER> { owner, creator });
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

                    var owner = db.USERs.Find(dbCampaign.CampaignOwner);
                    var creator = db.USERs.Find(dbCampaign.CreatedBy);
                    var modifyUser = db.USERs.Find(modifiedUser);
                    var leadAdded = db.LEADs.Find(apiModel.id);

                    var notifyModel = new NotificationApiModel();
                    notifyModel.title = "Campaign target added";
                    notifyModel.content = $"Lead {leadAdded.Name} has been added to campaign {dbCampaign.Name} by {modifyUser.Username}.";
                    notifyModel.createdAt = DateTime.Now;
                    NotificationManager.SendNotification(notifyModel, new List<USER> { owner, creator });
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
                    var leadName = campaignLead.LEAD.Name;
                    db.CAMPAIGN_TARGET.Remove(campaignLead);
                    dbCampaign.ModifiedAt = DateTime.Now;
                    dbCampaign.ModifiedBy = modifiedUser;
                    db.SaveChanges();

                    var owner = db.USERs.Find(dbCampaign.CampaignOwner);
                    var creator = db.USERs.Find(dbCampaign.CreatedBy);
                    var modifyUser = db.USERs.Find(modifiedUser);

                    var notifyModel = new NotificationApiModel();
                    notifyModel.title = "Campaign target removed";
                    notifyModel.content = $"Lead {leadName} has been removed from campaign {dbCampaign.Name} by {modifyUser.Username}.";
                    notifyModel.createdAt = DateTime.Now;
                    NotificationManager.SendNotification(notifyModel, new List<USER> { owner, creator });
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
                    var contactName = campaignContact.CONTACT.Name;
                    db.CAMPAIGN_TARGET.Remove(campaignContact);
                    dbCampaign.ModifiedAt = DateTime.Now;
                    dbCampaign.ModifiedBy = modifiedUser;
                    db.SaveChanges();

                    var owner = db.USERs.Find(dbCampaign.CampaignOwner);
                    var creator = db.USERs.Find(dbCampaign.CreatedBy);
                    var modifyUser = db.USERs.Find(modifiedUser);

                    var notifyModel = new NotificationApiModel();
                    notifyModel.title = "Campaign target removed";
                    notifyModel.content = $"Contact {contactName} has been removed from campaign {dbCampaign.Name} by {modifyUser.Username}.";
                    notifyModel.createdAt = DateTime.Now;
                    NotificationManager.SendNotification(notifyModel, new List<USER> { owner, creator });
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
                    return (contactList.OrderByDescending(c => c.ID).Skip((currentPage - 1) * pageSize).Take(pageSize), pager);
                }
                else
                {
                    var result = contactList.Where(c => c.Name.ToLower().Contains(q.ToLower()) || c.Phone.Contains(q) || c.Email.ToLower().Contains(q.ToLower()) || c.Mobile.Contains(q)).OrderByDescending(c => c.ID);
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
                    return (leadList.OrderByDescending(c => c.ID).Skip((currentPage - 1) * pageSize).Take(pageSize), pager);
                }
                else
                {
                    var result = leadList.Where(c => c.Name.ToLower().Contains(q) || c.CompanyName.ToLower().Contains(q) || c.Email.ToLower().Contains(q) || c.Phone.Contains(q) || c.LEAD_SOURCE != null ? c.LEAD_SOURCE.Name.ToLower().Contains(q) : false || c.PRIORITY != null ? c.LEAD_SOURCE.Name.ToLower().Contains(q) : false).OrderByDescending(c => c.ID);
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