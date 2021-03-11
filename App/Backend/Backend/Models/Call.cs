using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Backend.Models
{
    public class Call
    {
        public int ID { get; set; }

        public int? TASK_TEMPLATE_ID { get; set; }

        public int? CALLREASON_ID { get; set; }

        public int? CALLRESULT_ID { get; set; }

        public int? CALLTYPE_ID { get; set; }

        public int? Length { get; set; }

        public int? CONTACT_ID { get; set; }

        public int? LEAD_ID { get; set; }

        public int? RELATED_DEAL { get; set; }

        public int? RELATED_ACCOUNT { get; set; }

        public int? RELATED_CAMPAIGN { get; set; }

        public DateTime? StartTime { get; set; }

        public int? CallOwner { get; set; }
    }
}