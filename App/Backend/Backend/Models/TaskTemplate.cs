using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Backend.Models
{
    public class TaskTemplate
    {
        public int ID { get; set; }

        public string Title { get; set; }

        public bool? IsRepeat { get; set; }

        public string RRule { get; set; }

        public int? TASK_STATUS_ID { get; set; }

        public string Description { get; set; }

        public int? CreatedBy { get; set; }

        public DateTime? CreatedAt { get; set; }

        public int? ModifiedBy { get; set; }

        public DateTime? ModifiedAt { get; set; }

        public int? PRIORITY_ID { get; set; }

        public bool? IsCompleted { get; set; }

        public DateTime? DueDate { get; set; }
    }
}