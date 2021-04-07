using Backend.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Backend.Models.ApiModel
{
    public class CompetitorCreateApiModel
    {
        public int id { get; set; }
        public string name { get; set; }
        public string website { get; set; }
        public string threat { get; set; }
        public string strengths { get; set; }
        public string weaknesses { get; set; }
        public string suggestions { get; set; }
    }

    public class CompetitorListApiModel
    {
        public class CompetitorInfo
        {
            public int id { get; set; }
            public string name { get; set; }
            public string website { get; set; }
            public string strengths { get; set; }
            public string weaknesses { get; set; }
            public string threat { get; set; }
            public string suggestions { get; set; }
        }
        public List<CompetitorInfo> competitors { get; set; }
        public Pager pageInfo { get; set; }
    }
}