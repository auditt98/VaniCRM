using Backend.Domain;
using Backend.Extensions;
using Backend.Models.ApiModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using static Backend.Extensions.Enum;
using static Backend.Extensions.DbDateHelper;

namespace Backend.Repository
{
    public class DealRepository
    {
        DatabaseContext db = new DatabaseContext();
        TagRepository _tagRepository = new TagRepository();
        
        public (IEnumerable<DEAL> deals, Pager p) GetAllDeals(string query = "", int pageSize = 0, int currentPage = 1)
        {
            var q = query.ToLower();
            if (pageSize == 0)
            {
                pageSize = 10;
            }
            if (String.IsNullOrEmpty(q))
            {
                Pager pager = new Pager(db.DEALs.Count(), currentPage, pageSize, 9999);
                return (db.DEALs.OrderByDescending(c => c.ID).Skip((currentPage - 1) * pageSize).Take(pageSize), pager);
            }
            var deals = db.DEALs.Where(c => c.Name.ToLower().Contains(q) || c.PRIORITY.Name.ToLower().Contains(q) || c.ACCOUNT.Name.ToLower().Contains(q)).OrderByDescending(c => c.ID);
            if (deals.Count() > 0)
            {
                Pager p = new Pager(deals.Count(), currentPage, pageSize, 9999);

                return (deals.Skip((currentPage - 1) * pageSize).Take(pageSize), p);
            }
            else
            {
                return (deals, null);
            }

        }

        public bool Create(DealCreateApiModel apiModel, int createdUser)
        {
            var newDeal = new DEAL();
            if(apiModel.account != 0)
            {
                newDeal.ACCOUNT_ID = apiModel.account;
            }
            newDeal.Amount = apiModel.amount;
            //newDeal.ClosingDate = apiModel.closingDate.GetValueOrDefault();
            if (apiModel.contact != 0)
            {
                newDeal.Contact_ID = apiModel.contact;
            }
            newDeal.DealOwner = apiModel.owner != 0 ? apiModel.owner : createdUser;
            newDeal.Description = apiModel.description;
            newDeal.ExpectedRevenue = apiModel.expectedRevenue;
            if(apiModel.stage != 0)
            {
                var newStageHistory = new STAGE_HISTORY();
                newStageHistory.ModifiedBy = createdUser;
                newStageHistory.ModifiedAt = DateTime.Now;
                newStageHistory.STAGE_ID = apiModel.stage;
                if(apiModel.stage == (int)EnumStage.LOST)
                {
                    newDeal.isLost = true;
                }
                else
                {
                    newDeal.isLost = false;
                }
                newDeal.STAGE_HISTORY.Add(newStageHistory);
                if (apiModel.lostReason != 0)
                {
                    newDeal.LOST_REASON_ID = apiModel.lostReason;
                }
            }
            newDeal.Name = apiModel.name;
            if(apiModel.priority != 0)
            {
                newDeal.PRIORITY_ID = apiModel.priority;
            }
            if(apiModel.closingDate != null)
            {
                newDeal.ClosingDate = DbDateHelper.ToNullIfTooEarlyForDb(apiModel.closingDate.Value);
            } 
            newDeal.CreatedAt = DateTime.Now;
            newDeal.CreatedBy = createdUser;
            newDeal.ModifiedAt = DateTime.Now;
            if(apiModel.campaign != 0)
            {
                newDeal.CAMPAIGN_ID = apiModel.campaign;
            }
            if(apiModel.contact != 0)
            {
                newDeal.Contact_ID = apiModel.contact;
            }

            try
            {
                db.DEALs.Add(newDeal);
                db.SaveChanges();

                var owner = db.USERs.Find(newDeal.DealOwner);
                var creator = db.USERs.Find(createdUser);

                var notifyModel = new NotificationApiModel();
                notifyModel.title = "Deal created";
                notifyModel.content = $"Deal {newDeal.Name} has been created and assigned to you by {creator?.Username}.";
                notifyModel.createdAt = DateTime.Now;
                notifyModel.module = "deals";
                notifyModel.moduleObjectId = newDeal.ID;
                //notifyModel.
                NotificationManager.SendNotification(notifyModel, new List<USER> { owner });
                NotificationManager.ReloadDashboardSale();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public int GetOwner(int id)
        {
            var dbDeal = db.DEALs.Find(id);
            if (dbDeal != null)
            {
                if (dbDeal.Owner != null)
                {
                    return dbDeal.Owner.ID;
                }
                else
                {
                    return 0;
                }
            }
            else
            {
                return 0;
            }
        }

        public bool Update(int dealId, DealCreateApiModel apiModel, int modifiedUser)
        {
            var dbDeal = db.DEALs.Find(dealId);
            if (dbDeal != null)
            {
                if (apiModel.account != 0)
                {
                    dbDeal.ACCOUNT_ID = apiModel.account;
                }
                dbDeal.Amount = apiModel.amount;
                if(apiModel.closingDate != null)
                {

                    dbDeal.ClosingDate = DbDateHelper.ToNullIfTooEarlyForDb(apiModel.closingDate.Value);
                }
                if (apiModel.contact != 0)
                {
                    dbDeal.Contact_ID = apiModel.contact;
                }
                if(apiModel.owner != 0)
                {
                    dbDeal.DealOwner = apiModel.owner;
                }
                dbDeal.Description = apiModel.description;
                dbDeal.ExpectedRevenue = apiModel.expectedRevenue;
                if (apiModel.stage != 0)
                {
                    var newStageHistory = new STAGE_HISTORY();
                    newStageHistory.ModifiedBy = modifiedUser;
                    newStageHistory.ModifiedAt = DateTime.Now;
                    newStageHistory.STAGE_ID = apiModel.stage;
                    if (apiModel.stage == (int)EnumStage.LOST)
                    {
                        dbDeal.isLost = true;
                    }
                    else
                    {
                        dbDeal.isLost = false;
                    }
                    dbDeal.STAGE_HISTORY.Add(newStageHistory);
                    if (apiModel.lostReason != 0)
                    {
                        dbDeal.LOST_REASON_ID = apiModel.lostReason;
                    }
                }
                dbDeal.Name = apiModel.name;
                if (apiModel.priority != 0)
                {
                    dbDeal.PRIORITY_ID = apiModel.priority;
                }
                dbDeal.ModifiedAt = DateTime.Now;
                dbDeal.ModifiedBy = modifiedUser;
                db.SaveChanges();

                var owner = db.USERs.Find(dbDeal.DealOwner);
                var modifyUser = db.USERs.Find(modifiedUser);
                var creator = db.USERs.Find(dbDeal.CreatedBy);

                var notifyModel = new NotificationApiModel();
                notifyModel.title = "Deal updated";
                notifyModel.content = $"Deal {dbDeal.Name} has been updated by {modifyUser?.Username}.";
                notifyModel.createdAt = DateTime.Now;
                notifyModel.module = "deals";
                notifyModel.moduleObjectId = dbDeal.ID;
                NotificationManager.SendNotification(notifyModel, new List<USER> { owner, creator });
                NotificationManager.ReloadDashboardSale();

                return true;
            }
            else
            {
                return false;
            }
        }

        public bool Delete(int id)
        {
            var dbDeal = db.DEALs.Find(id);
            if (dbDeal != null)
            {
                db.DEALs.Remove(dbDeal);
                db.SaveChanges();
                NotificationManager.ReloadDashboardSale();

                return true;
            }
            else
            {
                return false;
            }
        }

        public bool AddTag(int id, string tagName)
        {
            var dbDeal = db.DEALs.Find(id);
            var dbTag = _tagRepository.GetOneByName(tagName);
            if (dbDeal != null)
            {
                if (dbTag != null)
                {
                    var tagItem = dbDeal.TAG_ITEM.Where(c => c.TAG_ID == dbTag.ID).FirstOrDefault();
                    if (tagItem == null)
                    {
                        var newTagItem = new TAG_ITEM();
                        newTagItem.TAG_ID = dbTag.ID;
                        newTagItem.DEAL_ID = dbDeal.ID;
                        db.TAG_ITEM.Add(newTagItem);
                        db.SaveChanges();

                        var owner = db.USERs.Find(dbDeal.DealOwner);
                        var creator = db.USERs.Find(dbDeal.CreatedBy);

                        var notifyModel = new NotificationApiModel();
                        notifyModel.title = "Tag added";
                        notifyModel.content = $"Tag {tagName} has been added to deal {dbDeal.Name}.";
                        notifyModel.module = "deals";
                        notifyModel.moduleObjectId = dbDeal.ID;
                        notifyModel.createdAt = DateTime.Now;
                        NotificationManager.SendNotification(notifyModel, new List<USER> { owner, creator });
                        NotificationManager.ReloadDashboardSale();

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
                    tagItem.DEAL_ID = dbDeal.ID;
                    db.TAG_ITEM.Add(tagItem);
                    db.SaveChanges();
                    var owner = db.USERs.Find(dbDeal.DealOwner);
                    var creator = db.USERs.Find(dbDeal.CreatedBy);

                    var notifyModel = new NotificationApiModel();
                    notifyModel.title = "Tag added";
                    notifyModel.content = $"Tag {tagName} has been added to deal {dbDeal.Name}.";
                    notifyModel.module = "deals";
                    notifyModel.moduleObjectId = dbDeal.ID;
                    notifyModel.createdAt = DateTime.Now;
                    NotificationManager.SendNotification(notifyModel, new List<USER> { owner, creator });
                    NotificationManager.ReloadDashboardSale();

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
            var dbDeal = db.DEALs.Find(id);
            if (dbDeal != null)
            {
                var tagItem = dbDeal.TAG_ITEM.Where(c => c.TAG.ID == tagId).FirstOrDefault();
                if (tagItem != null)
                {
                    var tagName = tagItem.TAG.Name;
                    db.TAG_ITEM.Remove(tagItem);
                    db.SaveChanges();
                    var owner = db.USERs.Find(dbDeal.DealOwner);
                    var creator = db.USERs.Find(dbDeal.CreatedBy);
                    var notifyModel = new NotificationApiModel();
                    notifyModel.title = "Tag removed";
                    notifyModel.content = $"Tag {tagName} has been removed from deal {dbDeal.Name}.";
                    notifyModel.module = "deals";
                    notifyModel.moduleObjectId = dbDeal.ID;
                    notifyModel.createdAt = DateTime.Now;
                    NotificationManager.SendNotification(notifyModel, new List<USER> { owner, creator });
                    NotificationManager.ReloadDashboardSale();

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

        public IEnumerable<STAGE> GetAllStages()
        {
            return db.STAGEs;
        }
        
        public DEAL GetOne(int id)
        {
            return db.DEALs.Find(id);
        }

        public IEnumerable<LOST_REASON> GetAllLostReason()
        {
            return db.LOST_REASON;
        }

        public (IEnumerable<TASK_TEMPLATE> tasks, Pager p) GetTasks(int dealId, int currentPage, int pageSize, string query)
        {
            var dbDeal = db.DEALs.Find(dealId);
            if (dbDeal != null)
            {
                var q = query.ToLower();
                var callList = db.CALLs.Where(c => c.DEAL.ID == dealId).Select(c => c.TASK_TEMPLATE).ToList();
                var taskList = db.TASKs.Where(c => c.DEAL.ID == dealId).Select(c => c.TASK_TEMPLATE).ToList();

                var allTasks = new List<TASK_TEMPLATE>();
                allTasks.AddRange(callList);
                allTasks.AddRange(taskList);

                if (pageSize == 0)
                {
                    pageSize = 10;
                }

                if (String.IsNullOrEmpty(q))
                {
                    Pager pager = new Pager(allTasks.Count(), currentPage, pageSize, 9999);
                    return (allTasks.OrderByDescending(c => c.ID).Skip((currentPage - 1) * pageSize).Take(pageSize), pager);
                }
                var filtered = allTasks.Where(c => c.Title.Contains(q)).OrderByDescending(c => c.ID);
                if (filtered.Count() > 0)
                {
                    Pager p = new Pager(filtered.Count(), currentPage, pageSize, 9999);

                    return (filtered.Skip((currentPage - 1) * pageSize).Take(pageSize), p);
                }
                else
                {
                    return (filtered, null);
                }
            }
            else
            {
                return (null, null);
            }
        }

        public int GetCreator(int id)
        {
            var dbDeal = db.DEALs.Find(id);
            if (dbDeal != null)
            {
                if (dbDeal.CreatedUser != null)
                {
                    return dbDeal.CreatedUser.ID;
                }
                else
                {
                    return 0;
                }
            }
            else
            {
                return 0;
            }
        }

        public bool ChangeStage(int id, int stageId, int modifiedUser)
        {
            var dbDeal = db.DEALs.Find(id);
            var dbStage = db.STAGEs.Find(stageId);
            if (dbDeal != null && dbStage != null)
            {
                var histories = dbDeal.STAGE_HISTORY.OrderByDescending(sh => sh.ModifiedAt);
                if(histories.Count() > 0)
                {
                    var current = histories.FirstOrDefault();
                    if(current.STAGE.ID == stageId)
                    {
                        return false;
                    }
                }
                var newStageHistory = new STAGE_HISTORY();
                newStageHistory.ModifiedBy = modifiedUser;
                newStageHistory.ModifiedAt = DateTime.Now;
                newStageHistory.STAGE_ID = stageId;
                newStageHistory.DEAL_ID = dbDeal.ID;
                if (stageId == (int)EnumStage.LOST)
                {
                    dbDeal.isLost = true;
                }
                else
                {
                    dbDeal.isLost = false;
                }
                db.STAGE_HISTORY.Add(newStageHistory);
                db.SaveChanges();

                var owner = db.USERs.Find(dbDeal.DealOwner);
                var modifyUser = db.USERs.Find(modifiedUser);
                var creator = db.USERs.Find(dbDeal.CreatedBy);

                var notifyModel = new NotificationApiModel();
                notifyModel.title = "Stage updated";
                notifyModel.content = $"Deal {dbDeal.Name}'s stage has been updated by {modifyUser?.Username}.";
                notifyModel.createdAt = DateTime.Now;
                notifyModel.module = "deals";
                notifyModel.moduleObjectId = dbDeal.ID;
                NotificationManager.SendNotification(notifyModel, new List<USER> { owner, creator });
                NotificationManager.ReloadDashboardSale();

                return true;
            }
            else
            {
                return false;
            }
        }
    
        public bool AddCompetitor(int id, CompetitorCreateApiModel apiModel)
        {
            var dbDeal = db.DEALs.Find(id);
            if (dbDeal != null && apiModel != null)
            {
                var dbCompetitor = db.COMPETITORs.Find(apiModel.id);
                if(dbCompetitor != null)
                {
                    var dealCompetitor = dbDeal.DEAL_COMPETITOR.Where(c => c.COMPETITOR_ID == dbCompetitor.ID).FirstOrDefault();
                    dbCompetitor.Website = apiModel.website;
                    dbCompetitor.Strengths = apiModel.strengths;
                    dbCompetitor.Weaknesses = apiModel.weaknesses;

                    if (dealCompetitor != null)
                    {
                        dealCompetitor.ThreatLevel = apiModel.threat;
                        dealCompetitor.Suggestions = apiModel.suggestions;
                    }
                    else
                    {
                        var newDealCompetitor = new DEAL_COMPETITOR();
                        newDealCompetitor.DEAL = dbDeal;
                        newDealCompetitor.COMPETITOR = dbCompetitor;
                        newDealCompetitor.Suggestions = apiModel.suggestions;
                        newDealCompetitor.ThreatLevel = apiModel.threat;
                        db.DEAL_COMPETITOR.Add(newDealCompetitor);
                    }
                    db.SaveChanges();

                    var owner = db.USERs.Find(dbDeal.DealOwner);
                    var creator = db.USERs.Find(dbDeal.CreatedBy);

                    var notifyModel = new NotificationApiModel();
                    notifyModel.title = "Competitor added";
                    notifyModel.content = $"Competitor {dbCompetitor.Name} has been added to deal {dbDeal.Name}.";
                    notifyModel.createdAt = DateTime.Now;
                    notifyModel.module = "deals";
                    notifyModel.moduleObjectId = dbDeal.ID;
                    NotificationManager.SendNotification(notifyModel, new List<USER> { owner, creator });
                    return true;
                }
                else
                {
                    var newCompetitor = new COMPETITOR();
                    newCompetitor.Name = apiModel.name;
                    newCompetitor.Strengths = apiModel.strengths;
                    newCompetitor.Weaknesses = apiModel.weaknesses;
                    newCompetitor.Website = apiModel.website;
                    db.COMPETITORs.Add(newCompetitor);
                    var newDealCompetitor = new DEAL_COMPETITOR();
                    newDealCompetitor.DEAL_ID = dbDeal.ID;
                    newDealCompetitor.COMPETITOR = newCompetitor;
                    newDealCompetitor.Suggestions = apiModel.suggestions;
                    newDealCompetitor.ThreatLevel = apiModel.threat;
                    db.DEAL_COMPETITOR.Add(newDealCompetitor);
                    db.SaveChanges();

                    var owner = db.USERs.Find(dbDeal.DealOwner);
                    var creator = db.USERs.Find(dbDeal.CreatedBy);

                    var notifyModel = new NotificationApiModel();
                    notifyModel.title = "Competitor added";
                    notifyModel.content = $"Competitor {newCompetitor.Name} has been added to deal {dbDeal.Name}.";
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

        public (IEnumerable<DEAL_COMPETITOR> competitors, Pager p) GetCompetitors(int dealId, int currentPage, int pageSize, string query)
        {
            var dbDeal = db.DEALs.Find(dealId);
            if (dbDeal != null)
            {
                var q = query.ToLower();
                var competitorList = dbDeal.DEAL_COMPETITOR;

                if (pageSize == 0)
                {
                    pageSize = 10;
                }

                if (String.IsNullOrEmpty(q))
                {
                    Pager pager = new Pager(competitorList.Count(), currentPage, pageSize, 9999);
                    return (competitorList.OrderByDescending(c => c.COMPETITOR.ID).Skip((currentPage - 1) * pageSize).Take(pageSize), pager);
                }
                var filtered = competitorList.Where(c => c.COMPETITOR.Name.ToLower().Contains(q)).OrderByDescending(c => c.COMPETITOR.ID);
                if (filtered.Count() > 0)
                {
                    Pager p = new Pager(filtered.Count(), currentPage, pageSize, 9999);

                    return (filtered.Skip((currentPage - 1) * pageSize).Take(pageSize), p);
                }
                else
                {
                    return (filtered, null);
                }
            }
            else
            {
                return (null, null);
            }
        }
    }
}