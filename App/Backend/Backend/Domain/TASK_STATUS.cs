namespace Backend.Domain
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class TASK_STATUS
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public TASK_STATUS()
        {
            TASK_TEMPLATE = new HashSet<TASK_TEMPLATE>();
        }

        public int ID { get; set; }

        [Required]
        [StringLength(20)]
        public string Name { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<TASK_TEMPLATE> TASK_TEMPLATE { get; set; }
    }
}
