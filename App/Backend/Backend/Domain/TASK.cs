namespace Backend.Domain
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("TASK")]
    public partial class TASK
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public TASK()
        {
            TAG_ITEM = new HashSet<TAG_ITEM>();
        }

        public int ID { get; set; }

        public int? TASK_TEMPLATE_ID { get; set; }

        public int? TaskOwner { get; set; }

        public int? CONTACT_ID { get; set; }

        public int? LEAD_ID { get; set; }

        public int? RELATED_CAMPAIGN { get; set; }

        public int? RELATED_DEAL { get; set; }

        public int? RELATED_ACCOUNT { get; set; }

        public DateTime? EndOn { get; set; }

        public virtual ACCOUNT ACCOUNT { get; set; }

        public virtual CAMPAIGN CAMPAIGN { get; set; }

        public virtual CONTACT CONTACT { get; set; }

        public virtual DEAL DEAL { get; set; }

        public virtual LEAD LEAD { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<TAG_ITEM> TAG_ITEM { get; set; }

        public virtual TASK_TEMPLATE TASK_TEMPLATE { get; set; }

        public virtual USER USER { get; set; }
    }
}
