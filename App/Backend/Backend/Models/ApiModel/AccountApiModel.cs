using Backend.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Backend.Models.ApiModel
{
    public class AccountListApiModel
    {
        public List<AccountInfo> accounts { get; set; }
        public Pager pageInfo { get; set; }
        public class AccountInfo
        {
            public int id { get; set; }
            public string name { get; set; }
            public string phone { get; set; }
            public string website { get; set; }
            public string owner { get; set; }
            public string avatar { get; set; }
        }
    }

    public class AccountLinkApiModel
    {
        public int id { get; set; }
        public string name { get; set; }
        public string email { get; set; }
    }

    public class AccountCreateApiModel
    {
        public int owner { get; set; }
        public int collaborator { get; set; }
        public string name { get; set; }
        public string email { get; set; }
        public string phone { get; set; }
        public string fax { get; set; }
        public string taxCode { get; set; }
        public int numberOfEmployees { get; set; }
        public long annualRevenue { get; set; }
        public string website { get; set; }
        public string bankName { get; set; }
        public string bankAccountName { get; set; }
        public string bankAccount { get; set; }
        public string country { get; set; }
        public string city { get; set; }
        public string addressDetail { get; set; }
    }

    public class AccountAddContactApiModel
    {
        public int id { get; set; }
    }

    public class AccountDetailApiModel
    {
        public int id { get; set; }
        public string avatar { get; set; }
        public string name { get; set; }
        public string email { get; set; }
        public string phone { get; set; }
        public string fax { get; set; }
        public string taxCode { get; set; }
        public int numberOfEmployees { get; set; }
        public long annualRevenue { get; set; }
        public string website { get; set; }
        public string bankName { get; set; }
        public string bankAccountName { get; set; }
        public string bankAccount { get; set; }
        public string country { get; set; }
        public string city { get; set; }
        public string addressDetail { get; set; }

        public List<TagApiModel> tags { get; set; }
        public List<NoteApiModel> notes { get; set; }

        public DateTime CreatedAt { get; set; }
        public DateTime ModifiedAt { get; set; }
        public UserLinkApiModel CreatedBy { get; set; }
        public UserLinkApiModel ModifiedBy { get; set; }
        public UserLinkApiModel owner { get; set; }
        public UserLinkApiModel collaborator { get; set; }
        public LeadLinkApiModel convertedFrom { get; set; }

    }

}