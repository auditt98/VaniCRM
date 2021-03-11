using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Backend.Models
{
    public class Meeting
    {
        public int ID { get; set; }

        public int? TASK_TEMPLATE_ID { get; set; }

        public int? Host { get; set; }

        public string Location { get; set; }

        public DateTime? FromDate { get; set; }

        public DateTime? ToDate { get; set; }

        public bool? IsAllDay { get; set; }

        public bool? IsRemindParticipant { get; set; }
    }
}