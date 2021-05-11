using Backend.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Backend.Models.ApiModel
{

    public class ChangePasswordApiModel
    {
        public string oldPassword { get; set; }
        public string newPassword { get; set; }
    }

    public class UserContactApiModel
    {
        public List<C> contacts { get; set; }
        public Pager pageInfo { get; set; }
        public class C
        {
            public string name { get; set; }
            public int id { get; set; }
            public string email { get; set; }
            public string phone { get; set; }
            public string mobile { get; set; }
            public string skype { get; set; }
            public bool isOwner { get; set; }
            public bool isCollaborator { get; set; }

            public C() { }
        }
    }

    public class UserAccountApiModel
    {
        public List<A> accounts { get; set; }
        public Pager pageInfo { get; set; }

        public class A
        {
            public string name { get; set; }
            public int id { get; set; }
            public string phone { get; set; }
            public string email { get; set; }
            public string website { get; set; }
            public string taxCode { get; set; }
            public bool isOwner { get; set; }
            public bool isCollaborator { get; set; }

            public A() { }
        }
    }

    public class UserLeadApiModel
    {
        public List<L> leads { get; set; }
        public Pager pageInfo { get; set; }
        public class L
        {
            public int id { get; set; }
            public string name { get; set; }
            public string email { get; set; }
            public string phone { get; set; }
            public string website { get; set; }
            public string companyName { get; set; }
            public string skype { get; set; }
        }
    }

    public class UserTaskApiModel
    {
        public List<T> tasks { get; set; }
        public Pager pageInfo { get; set; }
        public class T
        {
            public int id { get; set; }
            public string title { get; set; }
            public string type { get; set; }
            public string status { get; set; }
            public DateTime startDate { get; set; }
            public DateTime endDate { get; set; }
            public string priotity { get; set; }
            public string rrule { get; set; }
        }
    }


    public class UserDetailApiModel
    {
        public int id { get; set; }
        public string username { get; set; }
        public string avatar { get; set; }
        public string firstName { get; set; }
        public string lastName { get; set; }
        public string email { get; set; }
        public string skype { get; set; }
        public string phone { get; set; }
        public DateTime createdAt { get; set; }
        public string createdByName { get; set; }
        public int createdById { get; set; }
        public List<G> groups { get; set; }
        //public List<A> accounts

        public class G
        {
            public int id { get; set; }
            public string name { get; set; }
            public bool selected { get; set; }

            public G() { }

            public G(int id, string name, bool selected)
            {
                this.id = id;
                this.name = name;
                this.selected = selected;
            }
        }

        public class C
        {
            public string name { get; set; }
            public int id { get; set; }
            public string email { get; set; }
            public string phone { get; set; }
            public string mobile { get; set; }
            public string skype { get; set; }
            public bool isOwner { get; set; }
            public bool isCollaborator { get; set; }

            public C() { }
        }

        public class A
        {
            public string name { get; set; }
            public int id { get; set; }
            public string phone { get; set; }
            public string email { get; set; }
            public string website { get; set; }
            public string taxCode { get; set; }
            public bool isOwner { get; set; }
            public bool isCollaborator { get; set; }

            public A() { }
        }

        public class L
        {
            public int id { get; set; }
            public string name { get; set; }
            public string email { get; set; }
            public string phone { get; set; }
            public string website { get; set; }
            public string companyName { get; set; }
            public string skype { get; set; }
        }

        public class T
        {
            public int id { get; set; }
            public string title { get; set; }
            public string type { get; set; }
            public string status { get; set; }
            public DateTime startDate { get; set; }
            public DateTime endDate { get; set; }
            public string priority { get; set; }
            public string rrule { get; set; }
            public LeadLinkApiModel lead { get; set; }
            public ContactLinkApiModel contact { get; set; }
            public AccountLinkApiModel relatedAccount { get; set; }
            public CampaignLinkApiModel relatedCampaign { get; set; }
            public DealLinkApiModel relatedDeal { get; set; }
        }

    }

    public class UserLinkApiModel
    {
        public int? id { get; set; }
        public string username { get; set; }
        public string email { get; set; }
    }

    public class UserSelectionApiModel
    {
        public int id { get; set; }
        public string username { get; set; }
        public bool selected { get; set; }
    }

    public class UserListApiModel
    {
        public List<UserInfo> users { get; set; }
        public Pager pageInfo { get; set; }

        public class UserInfo
        {
            public int id { get; set; }
            public string username { get; set; }
            public string firstName { get; set; }
            public string lastName { get; set; }
            public string phone { get; set; }
            public string email { get; set; }
            public string skype { get; set; }
            public string avatar { get; set; }
        }
    }
}