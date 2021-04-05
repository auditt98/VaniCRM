using Backend.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Backend.Models.ApiModel
{
    public class DealListApiModel
    {
        public List<DealInfo> deals { get; set; }
        public Pager pageInfo { get; set; }
        public class DealInfo
        {
            public int id { get; set; }
            public string name { get; set; }
            public DateTime expectedDate { get; set; }
            public long amount { get; set; }
            public string stage { get; set; }
            public string priority { get; set; }
            public string accountName { get; set; }
            public string owner { get; set; }
        }
    }

    public class DealLinkApiModel
    {
        public int id { get; set; }
        public string name { get; set; }
    }

    public class DealCreateApiModel
    {
        public string name { get; set; }
        public int owner { get; set; }
        public DateTime? closingDate { get; set; }
        public int account { get; set; }
        public int contact { get; set; }
        public int campaign { get; set; }
        public int priority { get; set; }
        public int stage { get; set; }
        public long amount { get; set; }
        public long expectedRevenue { get; set; }
        public int lostReason { get; set; }
        public string description { get; set; }
    }

    public class DealBlankApiModel
    {
        public List<PrioritySelectionApiModel> priorities { get; set; }
        public List<StageLinkApiModel> stages { get; set; }
        //public List<Lost MyProperty { get; set; }
    }

    public class DealDetailApiModel
    {
        public int id { get; set; }
        public string name { get; set; }
        public DateTime closingDate { get; set; }

        public AccountLinkApiModel account { get; set; }
        public ContactLinkApiModel contact { get; set; }
        public CampaignLinkApiModel campaign { get; set; }
        public List<LostReasonLinkApiModel> lostReason { get; set; }
        public List<PrioritySelectionApiModel> priorities { get; set; }
        public long amount { get; set; }
        public int probability { get; set; }
        public long expectedRevenue { get; set; }
        public List<StageLinkApiModel> stages { get; set; }
        public string description { get; set; }

        public List<TagApiModel> tags { get; set; }
        public List<NoteApiModel> notes { get; set; }

        public DateTime CreatedAt { get; set; }
        public DateTime ModifiedAt { get; set; }
        public UserLinkApiModel CreatedBy { get; set; }
        public UserLinkApiModel ModifiedBy { get; set; }
        public UserLinkApiModel owner { get; set; }
    }

    public class StageLinkApiModel
    {
        public int id { get; set; }
        public string name { get; set; }
        public int probability { get; set; }
        public bool selected { get; set; }
        public bool passed { get; set; }
    }

    public class LostReasonLinkApiModel
    {
        public int id { get; set; }
        public string name { get; set; }
        public bool selected { get; set; }
    }
    
    public class StageHistoryListApiModel
    {
        public List<History> histories { get; set; }
        public Pager pageInfo { get; set; }
        public class History
        {
            public string name { get; set; }
            public int probability { get; set; }
            public long expectedRevenue { get; set; }
            public DateTime modifiedAt { get; set; }
            public UserLinkApiModel modifiedBy { get; set; }
        }
    }
}