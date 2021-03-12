namespace Backend.Domain
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("ACCOUNT")]
    public partial class ACCOUNT
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public ACCOUNT()
        {
            CALLs = new HashSet<CALL>();
            CONTACTs = new HashSet<CONTACT>();
            DEALs = new HashSet<DEAL>();
            NOTEs = new HashSet<NOTE>();
            TAG_ITEM = new HashSet<TAG_ITEM>();
            TASKs = new HashSet<TASK>();
        }

        public int ID { get; set; }

        public int? AccountOwner { get; set; }

        public int? AccountCollaborator { get; set; }

        [StringLength(100)]
        public string Name { get; set; }

        [StringLength(15)]
        public string Phone { get; set; }

        [StringLength(15)]
        public string Fax { get; set; }

        [StringLength(100)]
        public string Website { get; set; }

        public int? ACCOUNT_TYPE_ID { get; set; }

        public int? INDUSTRY_ID { get; set; }

        [StringLength(30)]
        public string TaxCode { get; set; }

        public int? NoEmployees { get; set; }

        public long? AnnualRevenue { get; set; }

        [StringLength(100)]
        public string BankName { get; set; }

        [StringLength(100)]
        public string BankAccountName { get; set; }

        [StringLength(100)]
        public string BankAccount { get; set; }

        [StringLength(500)]
        public string Avatar { get; set; }

        [StringLength(320)]
        public string Email { get; set; }

        public DateTime? CreatedAt { get; set; }

        public int? CreatedBy { get; set; }

        public int? ConvertFrom { get; set; }

        public int? ModifiedBy { get; set; }

        public DateTime? ModifiedAt { get; set; }

        [StringLength(100)]
        public string Country { get; set; }

        [StringLength(100)]
        public string City { get; set; }

        [StringLength(200)]
        public string AddressDetail { get; set; }

        public virtual USER Owner { get; set; }

        public virtual USER Collaborator { get; set; }

        public virtual ACCOUNT_TYPE ACCOUNT_TYPE { get; set; }

        public virtual LEAD LEAD { get; set; }

        public virtual USER CreatedUser { get; set; }

        public virtual INDUSTRY INDUSTRY { get; set; }

        public virtual USER ModifiedUser { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CALL> CALLs { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CONTACT> CONTACTs { get; set; }

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
