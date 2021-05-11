using Backend.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Backend.Models.ApiModel
{
    public class ContactListApiModel
    {
        public List<ContactInfo> contacts { get; set; }
        public Pager pageInfo { get; set; }
        public class ContactInfo
        {
            public int id { get; set; }
            public string contactName { get; set; }
            public string accountName { get; set; }
            public string email { get; set; }
            public string phone { get; set; }
            public string owner { get; set; }
            public string avatar { get; set; }
        }
    }

    public class ContactLinkApiModel
    {
        public int id { get; set; }
        public string name { get; set; }
        public string email { get; set; }
    }

    public class ContactCreateApiModel
    {
        public int owner { get; set; }
        public int account { get; set; }
        public int collaborator { get; set; }
        public string name { get; set; }
        public string email { get; set; }
        public string phone { get; set; }
        public string mobile { get; set; }
        public string departmentName { get; set; }
        public DateTime birthday { get; set; }
        public int priority { get; set; }
        public bool noEmail { get; set; }
        public bool noCall { get; set; }
        public string skype { get; set; }
        public string assistantName { get; set; }
        public string assistantPhone { get; set; }
        public string country { get; set; }
        public string city { get; set; }
        public string addressDetail { get; set; }
    }

    public class ContactDetailApiModel
    {
        public int id { get; set; }
        public string avatar { get; set; }

        public string name { get; set; }
        public string email { get; set; }
        public string phone { get; set; }
        public string mobile { get; set; }
        public string departmentName { get; set; }
        public DateTime birthday { get; set; }
        public bool noCall { get; set; }
        public bool noEmail { get; set; }
        public string skype { get; set; }
        public string assistantName { get; set; }
        public string assistantPhone { get; set; }
        public AccountLinkApiModel account { get; set; }

        public List<PrioritySelectionApiModel> priorities { get; set; }

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
    }

    public class ContactBlankApiModel
    {

        public List<PrioritySelectionApiModel> priority { get; set; }

    }
}