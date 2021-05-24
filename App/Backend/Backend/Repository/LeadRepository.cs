using Backend.Domain;
using Backend.Extensions;
using Backend.Models.ApiModel;
using Backend.Resources;
using Backend.SignalRHub;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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
                return leads.OrderByDescending(c=>c.ID).Skip((currentPage - 1) * pageSize).Take(pageSize).ToList();
            }
            var result = leads.Where(c => c.Name.ToLower().Contains(q.ToLower()) || c.Phone.Contains(q) || c.Email.ToLower().Contains(q.ToLower()) || c.CompanyName.ToLower().Contains(q.ToLower())).OrderByDescending(c => c.ID).Skip((currentPage - 1)*pageSize).Take(pageSize).ToList();
            return result;
        }

        public (IEnumerable<LEAD> leads, Pager p) GetAllLeads(string query = "", int pageSize = 0, int currentPage = 1, List<string> sort = null)
        {
            var q = query.ToLower();
            
            if (pageSize == 0)
            {
                pageSize = 10;
            }
            
            if (String.IsNullOrEmpty(q))
            {
                Pager pager = new Pager(db.LEADs.Count(), currentPage, pageSize, 9999);
                var results1 = new List<LEAD>();
                if (sort != null)
                {
                    foreach (var s in sort)
                    {
                        if (s.Contains("desc."))
                        {
                            if (s.Contains("name"))
                            {
                                results1 = db.LEADs.OrderByDescending(c => c.Name).ToList();
                            }
                            else if (s.Contains("companyName"))
                            {
                                results1 = db.LEADs.OrderByDescending(c => c.CompanyName).ToList();
                            }
                            else if (s.Contains("email"))
                            {
                                results1 = db.LEADs.OrderByDescending(c => c.Email).ToList();

                            }
                            else if (s.Contains("phone"))
                            {
                                results1 = db.LEADs.OrderByDescending(c => c.Phone).ToList();

                            }
                            else if (s.Contains("leadSource"))
                            {
                                results1 = db.LEADs.OrderByDescending(c => c.LEAD_SOURCE.Name).ToList();
                            }
                            else if (s.Contains("leadOwner"))
                            {
                                results1 = db.LEADs.OrderByDescending(c => c.Owner.Username).ToList();

                            }
                            else if (s.Contains("priority"))
                            {
                                results1 = db.LEADs.OrderByDescending(c => c.PRIORITY.Name).ToList();
                            }
                        }
                        else if(s.Contains("asc."))
                        {
                            if (s.Contains("name"))
                            {
                                results1 = db.LEADs.OrderBy(c => c.Name).ToList();
                            }
                            else if (s.Contains("companyName"))
                            {
                                results1 = db.LEADs.OrderBy(c => c.CompanyName).ToList();
                            }
                            else if (s.Contains("email"))
                            {
                                results1 = db.LEADs.OrderBy(c => c.Email).ToList();

                            }
                            else if (s.Contains("phone"))
                            {
                                results1 = db.LEADs.OrderBy(c => c.Phone).ToList();

                            }
                            else if (s.Contains("leadSource"))
                            {
                                results1 = db.LEADs.OrderBy(c => c.LEAD_SOURCE.Name).ToList();
                            }
                            else if (s.Contains("leadOwner"))
                            {
                                results1 = db.LEADs.OrderBy(c => c.Owner.Username).ToList();

                            }
                            else if (s.Contains("priority"))
                            {
                                results1 = db.LEADs.OrderBy(c => c.PRIORITY.Name).ToList();
                            }
                        }
                    }
                    return (results1.Skip((currentPage - 1) * pageSize).Take(pageSize), pager);
                }

                return (db.LEADs.OrderByDescending(c => c.ID).Skip((currentPage - 1) * pageSize).Take(pageSize), pager);
            }

            var leads = db.LEADs.Where(c => c.Name.ToLower().Contains(q) || c.CompanyName.ToLower().Contains(q) || c.Email.ToLower().Contains(q) || c.Phone.Contains(q) || c.LEAD_SOURCE.Name.ToLower().Contains(q) || c.PRIORITY.Name.ToLower().Contains(q)).OrderByDescending(c => c.ID);


            Pager p = null;
            if(leads.Count() > 0)
            {
                p = new Pager(leads.Count(), currentPage, pageSize, 9999);
            }

            var results2 = new List<LEAD>();
            if (sort != null)
            {
                foreach (var s in sort)
                {
                    if (s.Contains("desc."))
                    {
                        if (s.Contains("name"))
                        {
                            results2 = leads.OrderByDescending(c => c.Name).ToList();
                        }
                        else if (s.Contains("companyName"))
                        {
                            results2 = leads.OrderByDescending(c => c.CompanyName).ToList();
                        }
                        else if (s.Contains("email"))
                        {
                            results2 = leads.OrderByDescending(c => c.Email).ToList();

                        }
                        else if (s.Contains("phone"))
                        {
                            results2 = leads.OrderByDescending(c => c.Phone).ToList();

                        }
                        else if (s.Contains("leadSource"))
                        {
                            results2 = leads.OrderByDescending(c => c.LEAD_SOURCE.Name).ToList();
                        }
                        else if (s.Contains("leadOwner"))
                        {
                            results2 = leads.OrderByDescending(c => c.Owner.Username).ToList();

                        }
                        else if (s.Contains("priority"))
                        {
                            results2 = leads.OrderByDescending(c => c.PRIORITY.Name).ToList();
                        }
                    }
                    else if (s.Contains("asc."))
                    {
                        if (s.Contains("name"))
                        {
                            results2 = leads.OrderBy(c => c.Name).ToList();
                        }
                        else if (s.Contains("companyName"))
                        {
                            results2 = leads.OrderBy(c => c.CompanyName).ToList();
                        }
                        else if (s.Contains("email"))
                        {
                            results2 = leads.OrderBy(c => c.Email).ToList();

                        }
                        else if (s.Contains("phone"))
                        {
                            results2 = leads.OrderBy(c => c.Phone).ToList();

                        }
                        else if (s.Contains("leadSource"))
                        {
                            results2 = leads.OrderBy(c => c.LEAD_SOURCE.Name).ToList();
                        }
                        else if (s.Contains("leadOwner"))
                        {
                            results2 = leads.OrderBy(c => c.Owner.Username).ToList();

                        }
                        else if (s.Contains("priority"))
                        {
                            results2 = leads.OrderBy(c => c.PRIORITY.Name).ToList();
                        }
                    }
                }

                return (results2.Skip((currentPage - 1) * pageSize).Take(pageSize).ToList(), p);
                
            }
            else
            {
                results2 = leads.Skip((currentPage - 1) * pageSize).Take(pageSize).ToList();
                return (results2, p);
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
                NotificationManager.SendNotification(apiModel, new List<USER> { owner, creator, dbUser });

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
            newLead.ModifiedAt = DateTime.Now;
            newLead.Description = apiModel.description;
            newLead.Email = apiModel.email;
            newLead.Fax = apiModel.fax;
            var dbIndustry = db.INDUSTRies.Find(apiModel.industry);
            if(dbIndustry != null)
            {
                newLead.INDUSTRY = dbIndustry;
            }
            newLead.LeadOwner = apiModel.owner != 0 ? apiModel.owner : createdUser;
            var dbLeadSource = db.LEAD_SOURCE.Find(apiModel.leadSource);
            if(dbLeadSource != null)
            {
                newLead.LEAD_SOURCE = dbLeadSource;

            }
            var dbLeadStatus = db.LEAD_STATUS.Find(apiModel.leadStatus);
            if(dbLeadStatus != null)
            {
                newLead.Status = dbLeadStatus;

            }
            newLead.Name = apiModel.name;
            newLead.NoCall = apiModel.noCall;
            newLead.NoEmail = apiModel.noEmail;
            newLead.Phone = apiModel.phone;

            var dbPriority = db.PRIORITies.Find(apiModel.priority);
            if(dbPriority != null)
            {
                newLead.PRIORITY_ID = apiModel.priority;
            }
            newLead.Skype = apiModel.skype;
            newLead.Website = apiModel.website;
            try
            {
                db.LEADs.Add(newLead);
                db.SaveChanges();
                var owner = db.USERs.Find(newLead.LeadOwner);
                var notifyModel = new NotificationApiModel();
                notifyModel.title = "Lead assigned";
                notifyModel.content = $"Lead {newLead.Name} has been created and assigned to you.";
                notifyModel.module = "leads";
                notifyModel.moduleObjectId = newLead.ID;
                notifyModel.createdAt = DateTime.Now;
                NotificationManager.SendNotification(notifyModel, new List<USER> { owner });
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
                if (apiModel.industry != 0)
                {
                    dbLead.INDUSTRY_ID = apiModel.industry;
                }
                if(apiModel.leadSource != 0)
                {
                    dbLead.LeadSource = apiModel.leadSource;

                }
                if(apiModel.priority != 0)
                {
                    dbLead.PRIORITY_ID = apiModel.priority;
                }

                dbLead.Name = apiModel.name;
                dbLead.NoCall = apiModel.noCall;
                dbLead.NoEmail = apiModel.noEmail;
                dbLead.Phone = apiModel.phone;
                dbLead.Skype = apiModel.skype;
                if(apiModel.leadStatus != 0)
                {
                    dbLead.LeadStatus = apiModel.leadStatus;
                }
                dbLead.Website = apiModel.website;
                if(apiModel.owner != 0)
                {
                    dbLead.LeadOwner = apiModel.owner;
                }
                db.SaveChanges();

                var owner = db.USERs.Find(dbLead.LeadOwner);
                var modifyUser = db.USERs.Find(modifiedUser);

                var notifyModel = new NotificationApiModel();
                notifyModel.title = "Lead modified";
                notifyModel.content = $"Lead {dbLead.Name} has been modified by {modifyUser.Username}.";
                notifyModel.module = "leads";
                notifyModel.moduleObjectId = dbLead.ID;
                notifyModel.createdAt = DateTime.Now;
                NotificationManager.SendNotification(notifyModel, new List<USER> { owner, modifyUser });
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
                    if(tagItem == null)
                    {
                        var newTagItem = new TAG_ITEM();
                        newTagItem.TAG_ID = dbTag.ID;
                        newTagItem.LEAD_ID = dbLead.ID;
                        db.TAG_ITEM.Add(newTagItem);
                        db.SaveChanges();

                        var notifyModel = new NotificationApiModel();
                        notifyModel.title = "Tag added to lead";
                        notifyModel.content = $"Tag '{tagName}' has been added to lead {dbLead.Name}.";
                        notifyModel.module = "leads";
                        notifyModel.moduleObjectId = dbLead.ID;
                        notifyModel.createdAt = DateTime.Now;
                        NotificationManager.SendNotification(notifyModel, new List<USER> { dbLead.Owner });
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

                    var notifyModel = new NotificationApiModel();
                    notifyModel.title = "Tag added to lead";
                    notifyModel.content = $"Tag '{tagName}' has been added to lead {dbLead.Name}.";
                    notifyModel.module = "leads";
                    notifyModel.moduleObjectId = dbLead.ID;
                    notifyModel.createdAt = DateTime.Now;
                    NotificationManager.SendNotification(notifyModel, new List<USER> { dbLead.Owner });
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
                    var tagName = tagItem.TAG.Name;
                    db.TAG_ITEM.Remove(tagItem);
                    db.SaveChanges();

                    var notifyModel = new NotificationApiModel();
                    notifyModel.title = "Tag removed";
                    notifyModel.content = $"Tag '{tagName}' has been removed from lead {dbLead.Name}.";
                    notifyModel.module = "leads";
                    notifyModel.moduleObjectId = dbLead.ID;
                    notifyModel.createdAt = DateTime.Now;
                    NotificationManager.SendNotification(notifyModel, new List<USER> { dbLead.Owner });
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

                        var notifyModel = new NotificationApiModel();
                        notifyModel.title = "Lead converted";
                        notifyModel.content = $"Lead {dbLead.Name} has been converted to account {newAccount.Name}.";
                        notifyModel.module = "accounts";
                        notifyModel.moduleObjectId = newAccount.ID;
                        notifyModel.createdAt = DateTime.Now;
                        NotificationManager.SendNotification(notifyModel, new List<USER> { dbLead.Owner });

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

                    var notifyModel = new NotificationApiModel();
                    notifyModel.title = "Lead converted";
                    notifyModel.content = $"Lead {dbLead.Name} has been converted to account {newAccount.Name}.";
                    notifyModel.module = "accounts";
                    notifyModel.moduleObjectId = newAccount.ID;
                    notifyModel.createdAt = DateTime.Now;
                    NotificationManager.SendNotification(notifyModel, new List<USER> { dbLead.Owner });
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