using Backend.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Backend.Models.ApiModel
{
    public class LeadStatus
    {
        public int id { get; set; }
        public string name { get; set; }
        public bool selected { get; set; }
    }

    public class LeadSource
    {
        public int id { get; set; }
        public string name { get; set; }
        public bool selected { get; set; }
    }

    public class LeadListApiModel
    {
        public List<LeadInfo> leads { get; set; }
        public Pager pageInfo { get; set; }
        public class LeadInfo
        {
            public int id { get; set; }
            public string name { get; set; }
            public string companyName { get; set; }
            public string email { get; set; }
            public string phone { get; set; }
            public string leadSource { get; set; }
            public string leadOwner { get; set; }
            public string priority { get; set; }

        }
    }

    public class LeadLinkApiModel
    {
        public int id { get; set; }
        public string name { get; set; }
    }

    public class LeadDetailApiModel
    {
        public int id { get; set; }
        public string avatar { get; set; }
        public string name { get; set; }
        public string email { get; set; }
        public string phone { get; set; }
        public string fax { get; set; }
        public string website { get; set; }
        public long annualRevenue { get; set; }
        public string skype { get; set; }
        public string companyName { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime ModifiedAt { get; set; }
        public UserLinkApiModel CreatedBy { get; set; }
        public UserLinkApiModel ModifiedBy { get; set; }
        public UserLinkApiModel owner { get; set; }
        public List<PrioritySelectionApiModel> priority { get; set; }
        public List<LeadStatus> status { get; set; }
        public List<IndustrySelectionApiModel> industry { get; set; }
        public List<LeadSource> leadSource { get; set; }
        public string description { get; set; }
        public bool noEmail { get; set; }
        public bool noCall { get; set; }
        public string country { get; set; }
        public string city { get; set; }
        public string addressDetail { get; set; }
        public List<TagApiModel> tags { get; set; }
        public List<NoteApiModel> notes { get; set; }
    }

    public class LeadBlankApiModel
    {
        public List<IndustrySelectionApiModel> industry { get; set; }
        public List<LeadStatus> status { get; set; }
        public List<PrioritySelectionApiModel> priority { get; set; }
        public List<LeadSource> leadSource { get; set; }
    }

    public class LeadCreateApiModel
    {
        public string name { get; set; }
        public string email { get; set; }
        public string phone { get; set; }
        public string fax { get; set; }
        public int industry { get; set; }
        public string website { get; set; }
        public long annualRevenue { get; set; }
        public int priority { get; set; }
        public bool noEmail { get; set; }
        public bool noCall { get; set; }
        public int leadSource { get; set; }
        public string companyName { get; set; }
        public int numberOfEmployees { get; set; }
        public int leadStatus { get; set; }
        public string skype { get; set; }
        public string country { get; set; }
        public string city { get; set; }
        public string addressDetail { get; set; }
        public string description { get; set; }
        public int owner { get; set; }
    }
}