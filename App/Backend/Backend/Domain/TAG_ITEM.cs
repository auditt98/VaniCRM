namespace Backend.Domain
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class TAG_ITEM
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

        public virtual ACCOUNT ACCOUNT { get; set; }

        public virtual CALL CALL { get; set; }

        public virtual CAMPAIGN CAMPAIGN { get; set; }

        public virtual CONTACT CONTACT { get; set; }

        public virtual DEAL DEAL { get; set; }

        public virtual LEAD LEAD { get; set; }

        public virtual MEETING MEETING { get; set; }

        public virtual TAG TAG { get; set; }

        public virtual TASK TASK { get; set; }
    }
}
