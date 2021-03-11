namespace Backend.Domain
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("STAGE")]
    public partial class STAGE
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public STAGE()
        {
            STAGE_HISTORY = new HashSet<STAGE_HISTORY>();
        }

        public int ID { get; set; }

        [Required]
        [StringLength(30)]
        public string Name { get; set; }

        public int? Probability { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<STAGE_HISTORY> STAGE_HISTORY { get; set; }
    }
}
