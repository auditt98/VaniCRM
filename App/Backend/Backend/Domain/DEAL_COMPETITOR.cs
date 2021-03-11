namespace Backend.Domain
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class DEAL_COMPETITOR
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int COMPETITOR_ID { get; set; }

        [Key]
        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int DEAL_ID { get; set; }

        [StringLength(2000)]
        public string Suggestions { get; set; }

        [StringLength(20)]
        public string ThreatLevel { get; set; }

        public virtual COMPETITOR COMPETITOR { get; set; }

        public virtual DEAL DEAL { get; set; }
    }
}
