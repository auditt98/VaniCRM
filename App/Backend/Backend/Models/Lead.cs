using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Backend.Models
{
    public class Lead
    {
        public int ID { get; set; }

        public string Name { get; set; }

        public string Email { get; set; }

        public int? LeadSource { get; set; }

        public long? AnnualRevenue { get; set; }

        public string CompanyName { get; set; }

        public string Fax { get; set; }

        public string Description { get; set; }

        public int? INDUSTRY_ID { get; set; }

        public bool? NoEmail { get; set; }

        public int? LeadOwner { get; set; }

        public bool? NoCall { get; set; }

        public string Phone { get; set; }

        public string Website { get; set; }

        public string Avatar { get; set; }

        public string Skype { get; set; }

        public DateTime? CreatedAt { get; set; }

        public int? CreatedBy { get; set; }

        public DateTime? ModifiedAt { get; set; }

        public int? ModifiedBy { get; set; }

        public int? PRIORITY_ID { get; set; }
    }
}