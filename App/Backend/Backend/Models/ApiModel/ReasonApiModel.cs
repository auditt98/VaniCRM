using Backend.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Backend.Models.ApiModel
{
    public class ReasonListApiModel
    {
        public List<ReasonInfo> reasons { get; set; }
        public Pager pageInfo { get; set; }
        public class ReasonInfo
        {
            public int id { get; set; }
            public string reason { get; set; }
        }
    }
}