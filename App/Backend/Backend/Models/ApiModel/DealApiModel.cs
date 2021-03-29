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
}