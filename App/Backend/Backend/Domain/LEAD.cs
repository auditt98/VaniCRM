namespace Backend.Domain
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("LEAD")]
    public partial class LEAD
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public LEAD()
        {
            ACCOUNTs = new HashSet<ACCOUNT>();
            CALLs = new HashSet<CALL>();
            CAMPAIGN_TARGET = new HashSet<CAMPAIGN_TARGET>();
            MEETING_PARTICIPANT = new HashSet<MEETING_PARTICIPANT>();
            NOTEs = new HashSet<NOTE>();
            TAG_ITEM = new HashSet<TAG_ITEM>();
            TASKs = new HashSet<TASK>();
        }

        public int ID { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        [Required]
        [StringLength(320)]
        public string Email { get; set; }

        public int? LeadSource { get; set; }

        public long? AnnualRevenue { get; set; }

        [StringLength(150)]
        public string CompanyName { get; set; }

        [StringLength(15)]
        public string Fax { get; set; }

        [StringLength(2000)]
        public string Description { get; set; }

        public int? INDUSTRY_ID { get; set; }

        public bool? NoEmail { get; set; }

        public int? LeadOwner { get; set; }

        public bool? NoCall { get; set; }

        [StringLength(15)]
        public string Phone { get; set; }

        [StringLength(100)]
        public string Website { get; set; }

        [StringLength(500)]
        public string Avatar { get; set; }

        [StringLength(32)]
        public string Skype { get; set; }

        public DateTime? CreatedAt { get; set; }

        public int? CreatedBy { get; set; }

        public DateTime? ModifiedAt { get; set; }

        public int? ModifiedBy { get; set; }

        public int? PRIORITY_ID { get; set; }

        [StringLength(100)]
        public string Country { get; set; }

        [StringLength(100)]
        public string City { get; set; }

        [StringLength(200)]
        public string AddressDetail { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ACCOUNT> ACCOUNTs { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CALL> CALLs { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CAMPAIGN_TARGET> CAMPAIGN_TARGET { get; set; }

        public virtual INDUSTRY INDUSTRY { get; set; }

        public virtual USER CreatedUser { get; set; }

        public virtual USER Owner { get; set; }

        public virtual LEAD_SOURCE LEAD_SOURCE { get; set; }

        public virtual USER ModifiedUser { get; set; }

        public virtual PRIORITY PRIORITY { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<MEETING_PARTICIPANT> MEETING_PARTICIPANT { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<NOTE> NOTEs { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<TAG_ITEM> TAG_ITEM { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<TASK> TASKs { get; set; }
    }
}
