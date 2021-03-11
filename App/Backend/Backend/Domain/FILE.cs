namespace Backend.Domain
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("FILE")]
    public partial class FILE
    {
        public int ID { get; set; }

        [StringLength(200)]
        public string FileName { get; set; }

        public int? FileSize { get; set; }

        public int? NOTE_ID { get; set; }

        public byte[] Data { get; set; }

        [StringLength(500)]
        public string FileStorePath { get; set; }

        public virtual NOTE NOTE { get; set; }
    }
}
