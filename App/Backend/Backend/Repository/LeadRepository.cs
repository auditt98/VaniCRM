using Backend.Domain;
using Backend.Extensions;
using Backend.Models.ApiModel;
using Backend.Resources;
using Backend.SignalRHub;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using static Backend.Extensions.Enum;

namespace Backend.Repository
{
    public class LeadRepository
    {
        DatabaseContext db = new DatabaseContext();
        TagRepository _tagRepository = new TagRepository();
        NotificationRepository _notificationRepository = new NotificationRepository();

        public LEAD GetOne(int id)
        {
            return db.LEADs.Find(id);
        }

        public IEnumerable<LEAD> GetUserLeads(int userID, string q = "", int currentPage = 1, int pageSize = 0)
        {
            var dbUser = db.USERs.Find(userID);
            var leads = dbUser.LeadsAsOwner.ToList();
            if (pageSize == 0)
            {
                pageSize = leads.Count();
            }

            if (String.IsNullOrEmpty(q))
            {
                return leads.OrderBy(c=>c.ID).Skip((currentPage - 1) * pageSize).Take(pageSize).ToList();
            }
            var result = leads.Where(c => c.Name.ToLower().Contains(q.ToLower()) || c.Phone.Contains(q) || c.Email.ToLower().Contains(q.ToLower()) || c.CompanyName.ToLower().Contains(q.ToLower())).OrderBy(c => c.ID).Skip((currentPage - 1)*pageSize).Take(pageSize).ToList();
            return result;
        }

        public (IEnumerable<LEAD> leads, Pager p) GetAllLeads(string query = "", int pageSize = 0, int currentPage = 1)
        {
            var q = query.ToLower();
            
            if (pageSize == 0)
            {
                pageSize = 10;
            }
            
            if (String.IsNullOrEmpty(q))
            {
                Pager pager = new Pager(db.LEADs.Count(), currentPage, pageSize, 9999);
                return (db.LEADs.OrderBy(c => c.ID).Skip((currentPage - 1) * pageSize).Take(pageSize), pager);
            }
            var leads = db.LEADs.Where(c => c.Name.ToLower().Contains(q) || c.CompanyName.ToLower().Contains(q) || c.Email.ToLower().Contains(q) || c.Phone.Contains(q) || c.LEAD_SOURCE.Name.ToLower().Contains(q) || c.PRIORITY.Name.ToLower().Contains(q)).OrderBy(c => c.ID);
            if(leads.Count() > 0)
            {
                Pager p = new Pager(leads.Count(), currentPage, pageSize, 9999);
                
                return (leads.Skip((currentPage - 1) * pageSize).Take(pageSize), p);
            }
            else
            {
                return (leads, null);
            }
        }

        public IEnumerable<LEAD_SOURCE> GetAllLeadSources()
        {
            return db.LEAD_SOURCE;
        }

        public IEnumerable<LEAD_STATUS> GetAllLeadStatuses()
        {
            return db.LEAD_STATUS;
        }

        public bool Delete(int id, int userId)
        {
            var dbLead = db.LEADs.Find(id);
            var dbUser = db.USERs.Find(userId);
            if(dbLead != null)
            {
                var dbAccount = db.ACCOUNTs.Where(c => c.ConvertFrom == dbLead.ID).ToList();
                foreach(var account in dbAccount)
                {
                    account.ConvertFrom = null;
                }
                var owner = dbLead.Owner;
                var creator = dbLead.CreatedUser;
                var deletedLead = db.LEADs.Remove(dbLead);
                db.SaveChanges();

                //create a notification
                var apiModel = new NotificationApiModel();
                apiModel.title = "Lead removed";
                apiModel.content = $"Lead {deletedLead.Name} removed by user {dbUser.Username}.";
                apiModel.createdAt = DateTime.Now;
                var notiCreated = _notificationRepository.Create(apiModel, new List<USER> { dbUser, owner, creator });
                //send notification
                if (notiCreated.isCreated)
                {
                    //send to person who performs the delete
                    apiModel.Info();
                    apiModel.id = notiCreated.notiId;
                    NotificationHub.pushNotification(apiModel, new List<int> { userId, owner.ID, creator.ID });
                }
                else
                {
                    apiModel.Danger();
                    apiModel.title = ErrorMessages.SOMETHING_WRONG;
                    apiModel.content = ErrorMessages.CANT_PERFORM_ACTION;
                    NotificationHub.pushNotification(apiModel, new List<int> { userId });

                }

                return true;
            }
            else
            {
                return false;
            }
        }

        public bool Create(LeadCreateApiModel apiModel, int createdUser)
        {
            var newLead = new LEAD();
            newLead.AddressDetail = apiModel.addressDetail;
            newLead.AnnualRevenue = apiModel.annualRevenue;
            newLead.City = apiModel.city;
            newLead.CompanyName = apiModel.companyName;
            newLead.Country = apiModel.country;
            newLead.CreatedAt = DateTime.Now;
            newLead.CreatedBy = createdUser;
            newLead.Description = apiModel.description;
            newLead.Email = apiModel.email;
            newLead.Fax = apiModel.fax;
            newLead.INDUSTRY_ID = apiModel.industry;
            newLead.LeadOwner = apiModel.owner;
            newLead.LeadSource = apiModel.leadSource;
            newLead.LeadStatus = apiModel.leadStatus;
            newLead.Name = apiModel.name;
            newLead.NoCall = apiModel.noCall;
            newLead.NoEmail = apiModel.noEmail;
            newLead.Phone = apiModel.phone;
            newLead.PRIORITY_ID = apiModel.priority;
            newLead.Skype = apiModel.skype;
            newLead.Website = apiModel.website;
            try
            {
                db.LEADs.Add(newLead);
                db.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool Update(int leadId, LeadCreateApiModel apiModel, int modifiedUser)
        {
            var dbLead = db.LEADs.Find(leadId);
            if(dbLead != null)
            {
                dbLead.AddressDetail = apiModel.addressDetail;
                dbLead.AnnualRevenue = apiModel.annualRevenue;
                dbLead.City = apiModel.city;
                dbLead.CompanyName = apiModel.companyName;
                dbLead.Country = apiModel.country;
                dbLead.ModifiedBy = modifiedUser;
                dbLead.ModifiedAt = DateTime.Now;
                dbLead.Description = apiModel.description;
                dbLead.Email = apiModel.email;
                dbLead.Fax = apiModel.fax;
                dbLead.INDUSTRY_ID = apiModel.industry;
                dbLead.LeadSource = apiModel.leadSource;
                dbLead.Name = apiModel.name;
                dbLead.NoCall = apiModel.noCall;
                dbLead.NoEmail = apiModel.noEmail;
                dbLead.Phone = apiModel.phone;
                dbLead.PRIORITY_ID = apiModel.priority;
                dbLead.Skype = apiModel.skype;
                dbLead.LeadStatus = apiModel.leadStatus;
                dbLead.Website = apiModel.website;
                dbLead.LeadOwner = apiModel.owner;
                db.SaveChanges();
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool ChangeAvatar(int id, string fileName)
        {
            var dbLead = db.LEADs.Find(id);
            if (dbLead != null)
            {
                dbLead.Avatar = fileName;
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
            var dbLead = db.LEADs.Find(id);
            var dbTag = _tagRepository.GetOneByName(tagName);
            if(dbLead != null)
            {
                if (dbTag != null)
                {
                    var tagItem = dbLead.TAG_ITEM.Where(c => c.TAG_ID == dbTag.ID).FirstOrDefault();
                    if(tagItem != null)
                    {
                        var newTagItem = new TAG_ITEM();
                        newTagItem.TAG_ID = dbTag.ID;
                        newTagItem.LEAD_ID = dbLead.ID;
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
                    tagItem.LEAD_ID = dbLead.ID;
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
            var dbLead = db.LEADs.Find(id);
            if(dbLead != null)
            {
                var tagItem = dbLead.TAG_ITEM.Where(c => c.TAG.ID == tagId).FirstOrDefault();
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
    
        public LeadConvertApiModel Convert(int id, int convertUser)
        {
            var apiModel = new LeadConvertApiModel();
            var dbLead = db.LEADs.Find(id);
            if (dbLead != null)
            {
                if (dbLead.Status != null)
                {
                    if (dbLead.Status.ID != (int)EnumLeadStatus.CONVERTED)
                    {
                        var newAccount = new ACCOUNT();
                        var newContact = new CONTACT();
                        newAccount.Name = dbLead.CompanyName;
                        newAccount.Website = dbLead.Website;
                        newAccount.Email = dbLead.Email;
                        newAccount.AnnualRevenue = dbLead.AnnualRevenue;
                        newAccount.Country = dbLead.Country;
                        newAccount.City = dbLead.City;
                        newAccount.AddressDetail = dbLead.AddressDetail;
                        newAccount.ConvertFrom = dbLead.ID;
                        newAccount.Avatar = dbLead.Avatar;
                        newAccount.Owner = dbLead.Owner;
                        newAccount.CreatedAt = DateTime.Now;
                        if (convertUser != 0)
                        {
                            newAccount.CreatedBy = convertUser;
                        }
                        newAccount.INDUSTRY = dbLead.INDUSTRY;
                        newAccount.Phone = dbLead.Phone;
                        newAccount.Fax = dbLead.Fax;
                        dbLead.LeadStatus = (int)EnumLeadStatus.CONVERTED;
                        newAccount.NOTEs = dbLead.NOTEs;
                        db.ACCOUNTs.Add(newAccount);

                        newContact.Name = dbLead.Name;
                        newContact.NoCall = dbLead.NoCall;
                        newContact.NoEmail = dbLead.NoEmail;
                        newContact.NOTEs = dbLead.NOTEs;
                        newContact.Phone = dbLead.Phone;
                        newContact.Skype = dbLead.Skype;
                        newContact.City = dbLead.City;
                        newContact.Country = dbLead.Country;
                        newContact.AddressDetail = dbLead.AddressDetail;
                        if (dbLead.Owner != null)
                        {
                            newContact.Owner = dbLead.Owner;
                        }
                        newContact.CreatedAt = DateTime.Now;
                        if (convertUser != 0)
                        {
                            newContact.CreatedBy = convertUser;
                        }
                        newContact.ACCOUNT = newAccount;
                        newContact.Avatar = dbLead.Avatar;
                        db.CONTACTs.Add(newContact);
                        db.SaveChanges();
                        apiModel.newAccountId = newAccount.ID;
                        return apiModel;
                    }
                    else
                    {
                        return null;
                    }
                }
                else
                {
                    var newAccount = new ACCOUNT();
                    var newContact = new CONTACT();
                    newAccount.Name = dbLead.CompanyName;
                    newAccount.Website = dbLead.Website;
                    newAccount.Email = dbLead.Email;
                    newAccount.AnnualRevenue = dbLead.AnnualRevenue;
                    newAccount.Country = dbLead.Country;
                    newAccount.City = dbLead.City;
                    newAccount.AddressDetail = dbLead.AddressDetail;
                    newAccount.ConvertFrom = dbLead.ID;
                    newAccount.Avatar = dbLead.Avatar;
                    newAccount.Owner = dbLead.Owner;
                    newAccount.CreatedAt = DateTime.Now;
                    if (convertUser != 0)
                    {
                        newAccount.CreatedBy = convertUser;
                    }
                    newAccount.INDUSTRY = dbLead.INDUSTRY;
                    newAccount.Phone = dbLead.Phone;
                    newAccount.Fax = dbLead.Fax;
                    dbLead.LeadStatus = (int)EnumLeadStatus.CONVERTED;
                    newAccount.NOTEs = dbLead.NOTEs;
                    db.ACCOUNTs.Add(newAccount);

                    newContact.Name = dbLead.Name;
                    newContact.NoCall = dbLead.NoCall;
                    newContact.NoEmail = dbLead.NoEmail;
                    newContact.NOTEs = dbLead.NOTEs;
                    newContact.Phone = dbLead.Phone;
                    newContact.Skype = dbLead.Skype;
                    newContact.City = dbLead.City;
                    newContact.Country = dbLead.Country;
                    newContact.AddressDetail = dbLead.AddressDetail;
                    if (dbLead.Owner != null)
                    {
                        newContact.Owner = dbLead.Owner;
                    }
                    newContact.CreatedAt = DateTime.Now;
                    if (convertUser != 0)
                    {
                        newContact.CreatedBy = convertUser;
                    }
                    newContact.ACCOUNT = newAccount;
                    newContact.Avatar = dbLead.Avatar;
                    db.CONTACTs.Add(newContact);
                    db.SaveChanges();
                    apiModel.newAccountId = newAccount.ID;
                    return apiModel;
                }
                
                
            }
            else
            {
                return null;
            }
        }
    }
}