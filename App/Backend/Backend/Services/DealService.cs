using Backend.Extensions;
using Backend.Models.ApiModel;
using Backend.Repository;
using Backend.Resources;
using Backend.Validators;
using MoreLinq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Backend.Services
{
    public class DealService
    {
        DealRepository _dealRepository = new DealRepository();
        DealValidator _dealValidator = new DealValidator();
        PriorityRepository _priorityRepository = new PriorityRepository();

        public List<DealListApiModel.DealInfo> GetDealList(string query = "", int pageSize = 0, int currentPage = 1)
        {
            var dbDeals = _dealRepository.GetAllDeals(query, pageSize, currentPage);
            var temp = dbDeals.Select(c => new DealListApiModel.DealInfo() { id = c.ID, name = c.Name, expectedDate = c.ClosingDate.GetValueOrDefault(), amount = c.Amount.GetValueOrDefault(), accountName = c.ACCOUNT.Name, priority = c.PRIORITY.Name});
            var results = new List<DealListApiModel.DealInfo>();
            foreach(var deal in dbDeals)
            {
                var dealInfo = new DealListApiModel.DealInfo();
                dealInfo.id = deal.ID;
                dealInfo.name = deal.Name;
                dealInfo.expectedDate = deal.ClosingDate.GetValueOrDefault();
                dealInfo.amount = deal.Amount.GetValueOrDefault();
                dealInfo.accountName = deal.ACCOUNT.Name;
                dealInfo.priority = deal.PRIORITY.Name;
                dealInfo.owner = deal.Owner.FirstName + " " + deal.Owner.LastName;
                if(deal.STAGE_HISTORY.Count > 0)
                {
                    var stageHistory = deal.STAGE_HISTORY.MaxBy(c => c.ModifiedAt).FirstOrDefault();
                    if (stageHistory != null)
                    {
                        dealInfo.stage = stageHistory.STAGE.Name;
                    }
                }
                results.Add(dealInfo);
            }
            return results;
        }

        public bool Create(DealCreateApiModel apiModel, int createdUser)
        {
            var validator = _dealValidator.Validate(apiModel);
            if (validator.IsValid)
            {
                return _dealRepository.Create(apiModel, createdUser);
            }
            return false;
        }

        public int FindOwnerId(int dealId)
        {
            return _dealRepository.GetOwner(dealId);
        }

        public bool Update(int id, DealCreateApiModel apiModel, int modifiedUser)
        {
            var validator = _dealValidator.Validate(apiModel);
            if (validator.IsValid)
            {
                return _dealRepository.Update(id, apiModel, modifiedUser);
            }
            return false;
        }

        public bool Delete(int id)
        {
            return _dealRepository.Delete(id);
        }

        public bool AddTag(int id, string tagName)
        {
            return _dealRepository.AddTag(id, tagName);
        }

        public bool RemoveTag(int id, int tagId)
        {
            return _dealRepository.RemoveTag(id, tagId);
        }

        public DealBlankApiModel PrepareNewDeal()
        {
            var apiModel = new DealBlankApiModel();
            apiModel.priorities = _priorityRepository.GetAllPriorities().Select(c => new PrioritySelectionApiModel() { id = c.ID, name = c.Name, selected = false }).ToList();
            apiModel.stages = _dealRepository.GetAllStages().Select(c => new StageLinkApiModel() { id = c.ID, name = c.Name, probability = c.Probability.GetValueOrDefault(), selected = false, passed = false }).ToList();
            return apiModel;
        }

        public DealDetailApiModel GetOne(int id)
        {
            var dbDeal = _dealRepository.GetOne(id);
            if (dbDeal != null)
            {
                var apiModel = new DealDetailApiModel();

                apiModel.id = dbDeal.ID;
                apiModel.amount = dbDeal.Amount.GetValueOrDefault();
                apiModel.closingDate = dbDeal.ClosingDate.GetValueOrDefault();
                apiModel.CreatedAt = dbDeal.CreatedAt.GetValueOrDefault();
                if(dbDeal.CreatedUser != null)
                {
                    apiModel.CreatedBy = new UserLinkApiModel() { id = dbDeal.CreatedUser.ID, username = dbDeal.CreatedUser.Username, email = dbDeal.CreatedUser.Email };
                }
                if(dbDeal.Owner != null)
                {
                    apiModel.owner = new UserLinkApiModel() { id = dbDeal.Owner.ID, username = dbDeal.Owner.Username, email = dbDeal.Owner.Email };
                }

                if(dbDeal.ACCOUNT != null)
                {
                    apiModel.account = new AccountLinkApiModel() { id = dbDeal.ACCOUNT.ID, name = dbDeal.ACCOUNT.Name, email = dbDeal.ACCOUNT.Email };
                }
                
                if(dbDeal.PRIORITY != null)
                {
                    apiModel.priorities = _priorityRepository.GetAllPriorities().Select(c => new PrioritySelectionApiModel() { id = c.ID, name = c.Name, selected = c.ID == dbDeal.PRIORITY.ID }).ToList();
                }
                if(dbDeal.LOST_REASON != null)
                {
                    apiModel.lostReason = _dealRepository.GetAllLostReason().Select(c => new LostReasonLinkApiModel() { id = c.ID, name = c.Reason, selected = c.ID == dbDeal.LOST_REASON.ID }).ToList();
                }
                apiModel.expectedRevenue = dbDeal.ExpectedRevenue.GetValueOrDefault();
                apiModel.name = dbDeal.Name;
                //apiModel.
                if(dbDeal.STAGE_HISTORY.Count > 0)
                {
                    var histories = dbDeal.STAGE_HISTORY.OrderByDescending(sh => sh.ModifiedAt);
                    var current = histories.First();
                    apiModel.probability = current.STAGE.Probability.GetValueOrDefault();
                    apiModel.stages = _dealRepository.GetAllStages().OrderBy(c => c.ID).Select(c => new StageLinkApiModel() { id = c.ID, name = c.Name, probability = c.Probability.GetValueOrDefault(), selected = c.ID == current.STAGE.ID, passed = c.ID < current.STAGE.ID }).ToList();

                }
                if(dbDeal.CAMPAIGN != null)
                {
                    apiModel.campaign = new CampaignLinkApiModel() { id = dbDeal.CAMPAIGN.ID, name = dbDeal.CAMPAIGN.Name };

                }
                apiModel.CreatedAt = dbDeal.CreatedAt.GetValueOrDefault();
                apiModel.CreatedBy = new UserLinkApiModel() { id = dbDeal.CreatedUser.ID, username = dbDeal.CreatedUser.Username, email = dbDeal.CreatedUser.Email };
                apiModel.ModifiedAt = dbDeal.ModifiedAt.GetValueOrDefault();
                apiModel.ModifiedBy = new UserLinkApiModel() { id = dbDeal.ModifiedUser?.ID, username = dbDeal.ModifiedUser?.Username, email = dbDeal.ModifiedUser?.Email };


                //notes
                apiModel.notes = dbDeal.NOTEs.Select(c => new NoteApiModel() { id = c.ID, avatar = $"{StaticStrings.ServerHost}avatar?fileName={c.USER?.Avatar}", body = c.NoteBody, createdAt = c.CreatedAt.GetValueOrDefault(), createdBy = new UserLinkApiModel() { id = c.USER.ID, username = c.USER.Username, email = c.USER.Email }, files = c.FILEs.Select(f => new FileApiModel() { id = f.ID, fileName = f.FileName, size = f.FileSize.Value.ToString() + " KB", url = StaticStrings.ServerHost + "files/" + f.ID }).ToList() }).ToList();

                //tags
                apiModel.tags = dbDeal.TAG_ITEM.Select(c => new TagApiModel() { id = c.TAG.ID, name = c.TAG.Name }).ToList();
                return apiModel;
            }
            else
            {
                return null;
            }
        }

        public StageHistoryListApiModel GetHistories(int id, int currentPage, int pageSize, string query)
        {
            var dbDeal = _dealRepository.GetOne(id);
            if (dbDeal != null)
            {
                var apiModel = new StageHistoryListApiModel();
                var q = query.ToLower();
                if (dbDeal.STAGE_HISTORY.Count > 0)
                {
                    var histories = dbDeal.STAGE_HISTORY.OrderByDescending(sh => sh.ModifiedAt);
                    if(pageSize == 0)
                    {
                        pageSize = histories.Count();
                    }
                    if (string.IsNullOrEmpty(query))
                    {
                        //return accounts.OrderBy(c => c.ID).Skip((currentPage - 1) * pageSize).Take(pageSize);
                        Pager pager = new Pager(dbDeal.STAGE_HISTORY.Count(), currentPage, pageSize, 9999);
                        apiModel.histories = histories.Skip((currentPage - 1) * pageSize).Take(pageSize).Select(c => new StageHistoryListApiModel.History() { name = c.STAGE.Name, probability = c.STAGE.Probability.GetValueOrDefault(), expectedRevenue = (c.STAGE.Probability.GetValueOrDefault() * c.DEAL.Amount / 100).GetValueOrDefault(), modifiedAt = c.ModifiedAt.GetValueOrDefault(), modifiedBy = new UserLinkApiModel() { id = c.USER.ID, email = c.USER.Email, username = c.USER.Username }}).ToList();
                        apiModel.pageInfo = pager;
                    }
                    else
                    {
                        var historyList = histories.Where(c => c.STAGE.Name.ToLower().Contains(q)).Select(c => new StageHistoryListApiModel.History() { name = c.STAGE.Name, probability = c.STAGE.Probability.GetValueOrDefault(), expectedRevenue = (c.STAGE.Probability.GetValueOrDefault() * c.DEAL.Amount / 100).GetValueOrDefault(), modifiedAt = c.ModifiedAt.GetValueOrDefault(), modifiedBy = new UserLinkApiModel() { id = c.USER.ID, email = c.USER.Email, username = c.USER.Username } }).ToList();
                        if(historyList.Count > 0)
                        {
                            Pager p = new Pager(historyList.Count(), currentPage, pageSize, 9999);
                            apiModel.histories = historyList;
                            apiModel.pageInfo = p;

                        }
                    }

                    return apiModel;
                }
                else
                {
                    return null;
                }
            }
            else
            {
                return null;
            }
        }

        public TaskListApiModel GetTasks(int contactId, int currentPage, int pageSize, string query)
        {
            var dbTasks = _dealRepository.GetTasks(contactId, currentPage, pageSize, query);
            var apiModel = new TaskListApiModel();
            apiModel.tasks = new List<TaskListApiModel.TaskInfo>();
            foreach (var t in dbTasks.tasks)
            {
                var taskInfo = new TaskListApiModel.TaskInfo();
                if (t.MEETINGs.Count > 0)
                {
                    var meeting = t.MEETINGs.FirstOrDefault();
                    if (meeting != null)
                    {
                        taskInfo.id = meeting.ID;
                        taskInfo.startDate = meeting.FromDate.GetValueOrDefault();
                        taskInfo.owner = new UserLinkApiModel() { id = meeting.HostUser.ID, email = meeting.HostUser.Email, username = meeting.HostUser.Username };
                        taskInfo.type = "meetings";
                        if (t.PRIORITY != null)
                        {
                            taskInfo.priority = t.PRIORITY.Name;
                        }
                        taskInfo.endDate = meeting.ToDate.GetValueOrDefault();
                        if (t.TASK_STATUS != null)
                        {
                            taskInfo.status = t.TASK_STATUS.Name;

                        }
                        apiModel.tasks.Add(taskInfo);

                    }
                    else
                    {
                        return null;
                    }
                }
                else if (t.CALLs.Count > 0)
                {
                    var call = t.CALLs.FirstOrDefault();
                    if (call != null)
                    {
                        taskInfo.id = call.ID;
                        taskInfo.startDate = t.CreatedAt.GetValueOrDefault();
                        taskInfo.owner = new UserLinkApiModel() { id = call.Owner.ID, email = call.Owner.Email, username = call.Owner.Username };
                        taskInfo.type = "calls";
                        if (t.PRIORITY != null)
                        {
                            taskInfo.priority = t.PRIORITY.Name;
                        }
                        taskInfo.endDate = t.DueDate.GetValueOrDefault();
                        if (t.TASK_STATUS != null)
                        {
                            taskInfo.status = t.TASK_STATUS.Name;

                        }
                        apiModel.tasks.Add(taskInfo);

                    }
                    else
                    {
                        return null;
                    }
                }
                else if (t.TASKs.Count > 0)
                {
                    var task = t.TASKs.FirstOrDefault();
                    if (task != null)
                    {
                        taskInfo.id = task.ID;
                        taskInfo.startDate = t.CreatedAt.GetValueOrDefault();
                        taskInfo.owner = new UserLinkApiModel() { id = task.USER.ID, email = task.USER.Email, username = task.USER.Username };
                        taskInfo.type = "tasks";
                        if (t.PRIORITY != null)
                        {
                            taskInfo.priority = t.PRIORITY.Name;
                        }
                        taskInfo.endDate = t.DueDate.GetValueOrDefault();
                        if (t.TASK_STATUS != null)
                        {
                            taskInfo.status = t.TASK_STATUS.Name;

                        }
                        apiModel.tasks.Add(taskInfo);
                    }
                    else
                    {
                        return null;
                    }
                }
                //taskInfo.id = t.ID
            }
            //apiModel.tasks = dbTasks.tasks.Select(c => new TaskListApiModel.TaskInfo() { id = c.ID, type = c.}).ToList();
            apiModel.pageInfo = dbTasks.p;
            return apiModel;
        }

        public bool ChangeStage(int dealId, int stageId, int modifiedUser)
        {
            return _dealRepository.ChangeStage(dealId, stageId, modifiedUser);
        }

        public int FindCreatorId(int dealId)
        {
            return _dealRepository.GetCreator(dealId);
        }
    }
}