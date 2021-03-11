using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Backend.Models
{
    public class Task
    {
        public int ID { get; set; }

        public int? TASK_TEMPLATE_ID { get; set; }

        public int? TaskOwner { get; set; }

        public int? CONTACT_ID { get; set; }

        public int? LEAD_ID { get; set; }

        public int? RELATED_CAMPAIGN { get; set; }

        public int? RELATED_DEAL { get; set; }

        public int? RELATED_ACCOUNT { get; set; }

        public DateTime? EndOn { get; set; }
    }
}