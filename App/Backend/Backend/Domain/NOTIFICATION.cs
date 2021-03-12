namespace Backend.Domain
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("NOTIFICATION")]
    public partial class NOTIFICATION
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public NOTIFICATION()
        {
            USER_NOTIFICATION = new HashSet<USER_NOTIFICATION>();
        }

        public int ID { get; set; }

        [StringLength(50)]
        public string NotificationTitle { get; set; }

        [StringLength(100)]
        public string NotificationContent { get; set; }

        public DateTime? CreatedAt { get; set; }

        [StringLength(20)]
        public string Module { get; set; }

        public int? ModuleObjectID { get; set; }

        [StringLength(20)]
        public string Submodule { get; set; }

        public int? SubmoduleObjectID { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<USER_NOTIFICATION> USER_NOTIFICATION { get; set; }
    }
}
