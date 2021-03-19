using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Backend.Models.ApiModel
{
    public class TaskTemplateApiModel
    {
        public string id { get; set; }
        public string type { get; set; }
        public string status { get; set; }
        public DateTime startDate { get; set; }
        public DateTime endDate { get; set; }
        public string priority { get; set; }
        public int callDuration { get; set; }

    }
}