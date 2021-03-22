using Backend.Models.ApiModel;
using Backend.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Backend.Services
{
    public class CampaignService
    {
        CampaignRepository _campaignRepository = new CampaignRepository();
        public List<CampaignListApiModel.CampaignInfo> GetCampaignList(string query = "", int pageSize = 0, int currentPage = 1)
        {
            var dbCampaigns = _campaignRepository.GetAllCampaigns(query, pageSize, currentPage);
            return dbCampaigns.Select(c => new CampaignListApiModel.CampaignInfo() { id = c.ID, name = c.Name, startDate = c.StartDate.GetValueOrDefault(), endDate = c.EndDate.GetValueOrDefault(), status = c.CAMPAIGN_STATUS.Name, owner = c.Owner.FirstName + " " + c.Owner.LastName, type = c.CAMPAIGN_TYPE.Name }).ToList();
        }
    }
}