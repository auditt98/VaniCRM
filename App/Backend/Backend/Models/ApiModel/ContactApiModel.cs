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
        }

    }
}