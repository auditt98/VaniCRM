namespace Backend.Domain
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("CONTACT")]
    public partial class CONTACT
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public CONTACT()
        {
            CALLs = new HashSet<CALL>();
            CAMPAIGN_TARGET = new HashSet<CAMPAIGN_TARGET>();
            DEALs = new HashSet<DEAL>();
            MEETING_PARTICIPANT = new HashSet<MEETING_PARTICIPANT>();
            NOTEs = new HashSet<NOTE>();
            TAG_ITEM = new HashSet<TAG_ITEM>();
            TASKs = new HashSet<TASK>();
        }

        public int ID { get; set; }

        public int? ContactOwner { get; set; }

        public int? ContactCollaborator { get; set; }

        [StringLength(100)]
        public string Name { get; set; }

        [StringLength(320)]
        public string Email { get; set; }

        [StringLength(15)]
        public string Phone { get; set; }

        [StringLength(15)]
        public string Mobile { get; set; }

        [StringLength(100)]
        public string DepartmentName { get; set; }

        [Column(TypeName = "date")]
        public DateTime? Birthday { get; set; }

        [StringLength(100)]
        public string AssistantName { get; set; }

        [StringLength(15)]
        public string AssistantPhone { get; set; }

        public bool? NoEmail { get; set; }

        public bool? NoCall { get; set; }

        public DateTime? CreatedAt { get; set; }

        public int? CreatedBy { get; set; }

        public int? ConvertFrom { get; set; }

        public int? ModifiedBy { get; set; }

        public DateTime? ModifiedAt { get; set; }

        public int? PRIORITY_ID { get; set; }

        public int? ACCOUNT_ID { get; set; }

        [StringLength(32)]
        public string Skype { get; set; }

        public virtual ACCOUNT ACCOUNT { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CALL> CALLs { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CAMPAIGN_TARGET> CAMPAIGN_TARGET { get; set; }

        public virtual USER USER { get; set; }

        public virtual USER USER1 { get; set; }

        public virtual USER USER2 { get; set; }

        public virtual USER USER3 { get; set; }

        public virtual PRIORITY PRIORITY { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<DEAL> DEALs { get; set; }

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
