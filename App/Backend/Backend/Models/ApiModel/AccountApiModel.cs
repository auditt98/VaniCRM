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
        }

        
    }

    public class AccountLinkApiModel
    {
        public int id { get; set; }
        public string name { get; set; }
    }
}