namespace Backend.Domain
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class REFRESH_TOKEN
    {
        public int ID { get; set; }

        [StringLength(100)]
        public string Token { get; set; }

        public int? USER_ID { get; set; }

        public virtual USER USER { get; set; }
    }
}
