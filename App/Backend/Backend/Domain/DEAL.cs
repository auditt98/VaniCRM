namespace Backend.Domain
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("DEAL")]
    public partial class DEAL
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public DEAL()
        {
            CALLs = new HashSet<CALL>();
            DEAL_COMPETITOR = new HashSet<DEAL_COMPETITOR>();
            NOTEs = new HashSet<NOTE>();
            STAGE_HISTORY = new HashSet<STAGE_HISTORY>();
            TAG_ITEM = new HashSet<TAG_ITEM>();
            TASKs = new HashSet<TASK>();
        }

        public int ID { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        public DateTime? ClosingDate { get; set; }

        public int? DealOwner { get; set; }

        public long? Amount { get; set; }

        public long? ExpectedRevenue { get; set; }

        public int? Contact_ID { get; set; }

        public int? ACCOUNT_ID { get; set; }

        public int? CAMPAIGN_ID { get; set; }

        public bool? isLost { get; set; }

        public int? LOST_REASON_ID { get; set; }

        [StringLength(2000)]
        public string Description { get; set; }

        public int? ModifiedBy { get; set; }

        public DateTime? ModifiedAt { get; set; }

        public int? CreatedBy { get; set; }

        public DateTime? CreatedAt { get; set; }

        public int? PRIORITY_ID { get; set; }

        public virtual ACCOUNT ACCOUNT { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CALL> CALLs { get; set; }

        public virtual CAMPAIGN CAMPAIGN { get; set; }

        public virtual CONTACT CONTACT { get; set; }

        public virtual USER CreatedUser { get; set; }

        public virtual USER Owner { get; set; }

        public virtual LOST_REASON LOST_REASON { get; set; }

        public virtual USER ModifiedUser { get; set; }

        public virtual PRIORITY PRIORITY { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<DEAL_COMPETITOR> DEAL_COMPETITOR { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<NOTE> NOTEs { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<STAGE_HISTORY> STAGE_HISTORY { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<TAG_ITEM> TAG_ITEM { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<TASK> TASKs { get; set; }
    }
}
