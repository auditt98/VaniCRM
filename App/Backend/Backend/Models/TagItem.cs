using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Backend.Models
{
    public class TagItem
    {
        public int ID { get; set; }

        public int TAG_ID { get; set; }

        public int? DEAL_ID { get; set; }

        public int? CALL_ID { get; set; }

        public int? TASK_ID { get; set; }

        public int? MEETING_ID { get; set; }

        public int? CAMPAIGN_ID { get; set; }

        public int? LEAD_ID { get; set; }

        public int? ACCOUNT_ID { get; set; }

        public int? CONTACT_ID { get; set; }
    }
}