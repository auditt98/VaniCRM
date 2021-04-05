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
                //apiModel.

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
    }
}