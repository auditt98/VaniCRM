namespace Backend.Domain
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("CAMPAIGN")]
    public partial class CAMPAIGN
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public CAMPAIGN()
        {
            CALLs = new HashSet<CALL>();
            CAMPAIGN_TARGET = new HashSet<CAMPAIGN_TARGET>();
            DEALs = new HashSet<DEAL>();
            NOTEs = new HashSet<NOTE>();
            TAG_ITEM = new HashSet<TAG_ITEM>();
            TASKs = new HashSet<TASK>();
        }

        public int ID { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        public string Description { get; set; }

        [StringLength(2000)]
        public string EmailTitle { get; set; }

        public int? CampaignOwner { get; set; }

        public DateTime? StartDate { get; set; }

        public DateTime? EndDate { get; set; }

        public long? ActualCost { get; set; }

        public long? BudgetedCost { get; set; }

        public int? ExpectedResponse { get; set; }

        public long? ExpectedRevenue { get; set; }

        public int? NumberSent { get; set; }

        public DateTime? CreatedAt { get; set; }

        public int? CreatedBy { get; set; }

        public int? ModifiedBy { get; set; }

        public DateTime? ModifiedAt { get; set; }

        public int? CAMPAIGN_TYPE_ID { get; set; }

        public int? CAMPAIGN_STATUS_ID { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CALL> CALLs { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CAMPAIGN_TARGET> CAMPAIGN_TARGET { get; set; }

        public virtual USER Owner { get; set; }

        public virtual CAMPAIGN_TYPE CAMPAIGN_TYPE { get; set; }

        public virtual CAMPAIGN_STATUS CAMPAIGN_STATUS { get; set; }

        public virtual USER CreatedUser { get; set; }

        public virtual USER ModifiedUser { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<DEAL> DEALs { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<NOTE> NOTEs { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<TAG_ITEM> TAG_ITEM { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<TASK> TASKs { get; set; }
    }
}
