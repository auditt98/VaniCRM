using Backend.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Backend.Models.ApiModel
{
    public class CampaignListApiModel
    {
        public List<CampaignInfo> campaigns { get; set; }
        public Pager pageInfo { get; set; }

        public class CampaignInfo
        {
            public int id { get; set; }
            public string name { get; set; }
            public string type { get; set; }
            public string status { get; set; }
            public DateTime startDate { get; set; }
            public DateTime endDate { get; set; }
            public string owner { get; set; }
        }

    }

    public class CampaignStatus
    {
        public int id { get; set; }
        public string name { get; set; }
        public bool selected { get; set; }
    }

    public class CampaignType
    {
        public int id { get; set; }
        public string name { get; set; }
        public bool selected { get; set; }
    }

    public class CampaignBlankApiModel
    {
        public List<CampaignStatus> statuses { get; set; }
        public List<CampaignType> types { get; set; }
    }

    public class CampaignCreateApiModel
    {
        public int owner { get; set; }
        public string campaignName { get; set; }
        public DateTime startDate { get; set; }
        public DateTime endDate { get; set; }
        public long actualCost { get; set; }
        public long budgetedCost { get; set; }
        public int expectedResponse { get; set; }
        public long expectedRevenue { get; set; }
        public int numberSent { get; set; }
        public int type { get; set; }
        public int status { get; set; }
        public string description { get; set; }
        public string emailTitle { get; set; }
    }

    public class CampaignDetailApiModel
    {
        public List<TagApiModel> tags { get; set; }
        public List<NoteApiModel> notes { get; set; }
        public DateTime createdAt { get; set; }
        public DateTime modifiedAt { get; set; }
        public UserLinkApiModel createdBy { get; set; }
        public UserLinkApiModel modifiedBy { get; set; }
        public UserLinkApiModel owner { get; set; }
        //public ContactListApiModel contacts { get; set; }
        //public LeadListApiModel leads { get; set; }

        public int id { get; set; }
        public List<CampaignStatus> statuses { get; set; }
        public List<CampaignType> types { get; set; }
        public long actualCost { get; set; }
        public string campaignName { get; set; }
        public DateTime startDate { get; set; }
        public DateTime endDate { get; set; }
        public long budgetedCost { get; set; }
        public int expectedResponse { get; set; }
        public int numberSent { get; set; }
        public string description { get; set; }
        public string emailTitle { get; set; }

    }

    public class CampaignUpdateContactApiModel
    {
        public int id { get; set; }
    }


    public class CampaignUpdateLeadApiModel
    {
        public int id { get; set; }
    }

    public class CampaignLinkApiModel
    {
        public int id { get; set; }
        public string name { get; set; }
    }
}