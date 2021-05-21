namespace Backend.Domain
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class TASK_TEMPLATE
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public TASK_TEMPLATE()
        {
            CALLs = new HashSet<CALL>();
            MEETINGs = new HashSet<MEETING>();
            NOTEs = new HashSet<NOTE>();
            TASKs = new HashSet<TASK>();
        }

        public int ID { get; set; }

        [StringLength(100)]
        public string Title { get; set; }

        public bool? IsRepeat { get; set; }

        [StringLength(100)]
        public string RRule { get; set; }

        public int? TASK_STATUS_ID { get; set; }

        [StringLength(2000)]
        public string Description { get; set; }

        [StringLength(2000)]
        public string EventId { get; set; }

        public int? CreatedBy { get; set; }

        public DateTime? CreatedAt { get; set; }

        public int? ModifiedBy { get; set; }

        public DateTime? ModifiedAt { get; set; }

        public int? PRIORITY_ID { get; set; }

        public bool? IsCompleted { get; set; }

        public DateTime? DueDate { get; set; }

        public DateTime? StartDate { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CALL> CALLs { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<MEETING> MEETINGs { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<NOTE> NOTEs { get; set; }

        public virtual PRIORITY PRIORITY { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<TASK> TASKs { get; set; }

        public virtual TASK_STATUS TASK_STATUS { get; set; }

        public virtual USER CreatedUser { get; set; }

        public virtual USER ModifiedUSer { get; set; }
    }
}
