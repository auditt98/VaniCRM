namespace Backend.Domain
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("COMPETITOR")]
    public partial class COMPETITOR
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public COMPETITOR()
        {
            DEAL_COMPETITOR = new HashSet<DEAL_COMPETITOR>();
        }

        public int ID { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        [StringLength(100)]
        public string Website { get; set; }

        [StringLength(2000)]
        public string Strengths { get; set; }

        [StringLength(2000)]
        public string Weaknesses { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<DEAL_COMPETITOR> DEAL_COMPETITOR { get; set; }
    }
}
