using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Backend.Models
{
    public class StageHistory
    {
        public int STAGE_ID { get; set; }

        public int DEAL_ID { get; set; }

        public int? ModifiedBy { get; set; }

        public DateTime? ModifiedAt { get; set; }
    }
}