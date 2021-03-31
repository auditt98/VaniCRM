using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Backend.Models.ApiModel
{
    public class TaskTemplateApiModel
    {
        public string id { get; set; }
        public string type { get; set; }
        public string status { get; set; }
        public DateTime startDate { get; set; }
        public DateTime endDate { get; set; }
        public string priority { get; set; }
        public int callDuration { get; set; }

    }

    public class TaskStatus
    {
        public int id { get; set; }
        public string name { get; set; }
        public bool selected { get; set; }

    }

    public class CallPurpose
    {
        public int id { get; set; }
        public string name { get; set; }
        public bool selected { get; set; }
    }

    public class CallType
    {
        public int id { get; set; }
        public string name { get; set; }
        public bool selected { get; set; }
    }

    public class CallResult
    {
        public int id { get; set; }
        public string name { get; set; }
        public bool selected { get; set; }
    }

    public class CallCreateApiModel
    {
        public int owner { get; set; }

        public int contact { get; set; }
        public int lead { get; set; }

        public int purpose { get; set; }
        public int status { get; set; }
        public int result { get; set; }
        public int type { get; set; }

        public int relatedDeal { get; set; }
        public int relatedAccount { get; set; }
        public int relatedCampaign { get; set; }

        public string title { get; set; }
        public int duration { get; set; }
        public DateTime startTime { get; set; }
        public bool isReminder { get; set; }
        public string rrule { get; set; }
        public string description { get; set; }
    }

    public class CallDetailApiModel
    {
        public List<TagApiModel> tags { get; set; }
        public List<NoteApiModel> notes { get; set; }
        public DateTime createdAt { get; set; }
        public DateTime modifiedAt { get; set; }
        public UserLinkApiModel createdBy { get; set; }
        public UserLinkApiModel modifiedBy { get; set; }
        public UserLinkApiModel owner { get; set; }

        public ContactLinkApiModel contact { get; set; }
        public LeadLinkApiModel lead { get; set; }
        public DealLinkApiModel relatedDeal { get; set; }
        public AccountLinkApiModel relatedAccount { get; set; }
        public CampaignLinkApiModel relatedCampaign { get; set; }

        public List<CallPurpose> purposes { get; set; }
        public List<CallType> types { get; set; }
        public List<TaskStatus> statuses { get; set; }
        public List<CallResult> results { get; set; }

        public string title { get; set; }
        public int duration { get; set; }
        public DateTime startTime { get; set; }
        public bool isRepeat { get; set; }
        public string rrule { get; set; }
        public string description { get; set; }
    }

    public class CallBlankApiModel
    {
        public List<CallPurpose> purposes { get; set; }
        public List<CallType> types { get; set; }
        public List<TaskStatus> statuses { get; set; }
        public List<CallResult> results { get; set; }
    }

    public class MeetingCreateApiModel
    {
        public int host { get; set; }
        public string title { get; set; }
        public string location { get; set; }
        public DateTime from { get; set; }
        public DateTime to { get; set; }
        public bool isAllDay { get; set; }
        public bool isRepeat { get; set; }
        public string rrule { get; set; }
        public string description { get; set; }

    }

}