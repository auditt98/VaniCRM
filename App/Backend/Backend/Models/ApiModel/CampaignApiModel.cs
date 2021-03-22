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
}