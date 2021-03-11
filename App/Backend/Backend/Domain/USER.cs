namespace Backend.Domain
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("USER")]
    public partial class USER
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public USER()
        {
            ACCOUNTs = new HashSet<ACCOUNT>();
            ACCOUNTs1 = new HashSet<ACCOUNT>();
            ACCOUNTs2 = new HashSet<ACCOUNT>();
            ACCOUNTs3 = new HashSet<ACCOUNT>();
            CALLs = new HashSet<CALL>();
            CAMPAIGNs = new HashSet<CAMPAIGN>();
            CAMPAIGNs1 = new HashSet<CAMPAIGN>();
            CAMPAIGNs2 = new HashSet<CAMPAIGN>();
            CONTACTs = new HashSet<CONTACT>();
            CONTACTs1 = new HashSet<CONTACT>();
            CONTACTs2 = new HashSet<CONTACT>();
            CONTACTs3 = new HashSet<CONTACT>();
            DEALs = new HashSet<DEAL>();
            DEALs1 = new HashSet<DEAL>();
            DEALs2 = new HashSet<DEAL>();
            LEADs = new HashSet<LEAD>();
            LEADs1 = new HashSet<LEAD>();
            MEETINGs = new HashSet<MEETING>();
            MEETING_PARTICIPANT = new HashSet<MEETING_PARTICIPANT>();
            NOTEs = new HashSet<NOTE>();
            STAGE_HISTORY = new HashSet<STAGE_HISTORY>();
            TASKs = new HashSet<TASK>();
            TASK_TEMPLATE = new HashSet<TASK_TEMPLATE>();
            TASK_TEMPLATE1 = new HashSet<TASK_TEMPLATE>();
            USER1 = new HashSet<USER>();
        }

        public int ID { get; set; }

        [Required]
        [StringLength(50)]
        public string FirstName { get; set; }

        [StringLength(500)]
        public string Avatar { get; set; }

        [Required]
        [StringLength(50)]
        public string LastName { get; set; }

        [Required]
        [StringLength(30)]
        public string Username { get; set; }

        [StringLength(200)]
        public string Hash { get; set; }

        [StringLength(200)]
        public string RememberMeToken { get; set; }

        [Required]
        [StringLength(320)]
        public string Email { get; set; }

        [StringLength(15)]
        public string Phone { get; set; }

        [StringLength(32)]
        public string Skype { get; set; }

        public DateTime? CreatedAt { get; set; }

        public int? CreatedBy { get; set; }

        public int? GROUP_ID { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ACCOUNT> ACCOUNTs { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ACCOUNT> ACCOUNTs1 { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ACCOUNT> ACCOUNTs2 { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ACCOUNT> ACCOUNTs3 { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CALL> CALLs { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CAMPAIGN> CAMPAIGNs { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CAMPAIGN> CAMPAIGNs1 { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CAMPAIGN> CAMPAIGNs2 { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CONTACT> CONTACTs { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CONTACT> CONTACTs1 { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CONTACT> CONTACTs2 { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CONTACT> CONTACTs3 { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<DEAL> DEALs { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<DEAL> DEALs1 { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<DEAL> DEALs2 { get; set; }

        public virtual GROUP GROUP { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<LEAD> LEADs { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<LEAD> LEADs1 { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<MEETING> MEETINGs { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<MEETING_PARTICIPANT> MEETING_PARTICIPANT { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<NOTE> NOTEs { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<STAGE_HISTORY> STAGE_HISTORY { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<TASK> TASKs { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<TASK_TEMPLATE> TASK_TEMPLATE { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<TASK_TEMPLATE> TASK_TEMPLATE1 { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<USER> USER1 { get; set; }

        public virtual USER USER2 { get; set; }
    }
}
