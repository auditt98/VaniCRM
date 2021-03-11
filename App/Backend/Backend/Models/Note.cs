using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Backend.Models
{
    public class Note
    {
        public int ID { get; set; }

        public int CreatedBy { get; set; }

        public DateTime? CreatedAt { get; set; }

        public string NoteBody { get; set; }

        public int? ACCOUNT_ID { get; set; }

        public int? CONTACT_ID { get; set; }

        public int? LEAD_ID { get; set; }

        public int? DEAL_ID { get; set; }

        public int? TASK_TEMPLATE_ID { get; set; }

        public int? CAMPAIGN_ID { get; set; }

        public ICollection<File> Files { get; set; }
    }
}