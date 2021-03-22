using Backend.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Backend.Models.ApiModel
{
    public class LeadListApiModel
    {
        public List<LeadInfo> leads { get; set; }
        public Pager pageInfo { get; set; }
        public class LeadInfo
        {
            public int id { get; set; }
            public string companyName { get; set; }
            public string email { get; set; }
            public string phone { get; set; }
            public string leadSource { get; set; }
            public string leadOwner { get; set; }
            public string priority { get; set; }

        }
    }

}