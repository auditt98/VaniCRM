namespace Backend.Domain
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class MEETING_PARTICIPANT
    {
        public int ID { get; set; }

        public int? MEETING_ID { get; set; }

        public int? LEAD_ID { get; set; }

        public int? USER_ID { get; set; }

        public int? CONTACT_ID { get; set; }

        public virtual CONTACT CONTACT { get; set; }

        public virtual LEAD LEAD { get; set; }

        public virtual MEETING MEETING { get; set; }

        public virtual USER USER { get; set; }
    }
}
