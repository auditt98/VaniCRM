using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Backend.Models
{
    public class Deal
    {
        public int ID { get; set; }

        public string Name { get; set; }

        public DateTime? ClosingDate { get; set; }

        public int? DealOwner { get; set; }

        public long? Amount { get; set; }

        public long? ExpectedRevenue { get; set; }

        public int? Contact_ID { get; set; }

        public int? ACCOUNT_ID { get; set; }

        public int? CAMPAIGN_ID { get; set; }

        public bool? isLost { get; set; }

        public int? LOST_REASON_ID { get; set; }

        public string Description { get; set; }

        public int? ModifiedBy { get; set; }

        public DateTime? ModifiedAt { get; set; }

        public int? CreatedBy { get; set; }

        public DateTime? CreatedAt { get; set; }

        public int? PRIORITY_ID { get; set; }
    }
}