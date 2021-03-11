using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Backend.Models
{
    public class File
    {
        public int ID { get; set; }

        public string FileName { get; set; }

        public int? FileSize { get; set; }

        public int? NOTE_ID { get; set; }

        public byte[] Data { get; set; }

        public string FileStorePath { get; set; }
    }
}