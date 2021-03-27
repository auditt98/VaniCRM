using Backend.Models.ApiModel;
using Backend.Repository;
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
    }
}