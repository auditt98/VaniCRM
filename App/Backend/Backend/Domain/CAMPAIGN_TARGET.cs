namespace Backend.Domain
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class CAMPAIGN_TARGET
    {
        public int ID { get; set; }

        public int? CAMPAIGN_ID { get; set; }

        public int? CONTACT_ID { get; set; }

        public int? LEAD_ID { get; set; }

        public virtual CAMPAIGN CAMPAIGN { get; set; }

        public virtual CONTACT CONTACT { get; set; }

        public virtual LEAD LEAD { get; set; }
    }
}
