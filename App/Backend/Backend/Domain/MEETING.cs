namespace Backend.Domain
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("MEETING")]
    public partial class MEETING
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public MEETING()
        {
            MEETING_PARTICIPANT = new HashSet<MEETING_PARTICIPANT>();
            TAG_ITEM = new HashSet<TAG_ITEM>();
        }

        public int ID { get; set; }

        public int? TASK_TEMPLATE_ID { get; set; }

        public int? Host { get; set; }

        [StringLength(200)]
        public string Location { get; set; }

        public DateTime? FromDate { get; set; }

        public DateTime? ToDate { get; set; }

        public bool? IsAllDay { get; set; }

        public bool? IsRemindParticipant { get; set; }

        public virtual USER USER { get; set; }

        public virtual TASK_TEMPLATE TASK_TEMPLATE { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<MEETING_PARTICIPANT> MEETING_PARTICIPANT { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<TAG_ITEM> TAG_ITEM { get; set; }
    }
}
