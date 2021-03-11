namespace Backend.Domain
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("NOTE")]
    public partial class NOTE
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public NOTE()
        {
            FILEs = new HashSet<FILE>();
        }

        public int ID { get; set; }

        public int CreatedBy { get; set; }

        public DateTime? CreatedAt { get; set; }

        [StringLength(2000)]
        public string NoteBody { get; set; }

        public int? ACCOUNT_ID { get; set; }

        public int? CONTACT_ID { get; set; }

        public int? LEAD_ID { get; set; }

        public int? DEAL_ID { get; set; }

        public int? TASK_TEMPLATE_ID { get; set; }

        public int? CAMPAIGN_ID { get; set; }

        public virtual ACCOUNT ACCOUNT { get; set; }

        public virtual CAMPAIGN CAMPAIGN { get; set; }

        public virtual CONTACT CONTACT { get; set; }

        public virtual DEAL DEAL { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<FILE> FILEs { get; set; }

        public virtual LEAD LEAD { get; set; }

        public virtual USER USER { get; set; }

        public virtual TASK_TEMPLATE TASK_TEMPLATE { get; set; }
    }
}
