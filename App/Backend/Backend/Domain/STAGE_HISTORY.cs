namespace Backend.Domain
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class STAGE_HISTORY
    {
        [Key]
        [Column(Order = 4)]
        public int ID { get; set; }
        
        [Column(Order = 0)]
        public int STAGE_ID { get; set; }
        [Column(Order = 1)]
        public int DEAL_ID { get; set; }

        public int? ModifiedBy { get; set; }

        public DateTime? ModifiedAt { get; set; }

        public virtual DEAL DEAL { get; set; }

        public virtual STAGE STAGE { get; set; }

        public virtual USER USER { get; set; }
    }
}
