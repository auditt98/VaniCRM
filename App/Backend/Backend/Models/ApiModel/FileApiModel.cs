using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Backend.Models.ApiModel
{
    public class FileApiModel
    {
        public int id { get; set; }
        public string fileName { get; set; }
        public string url { get; set; }
        public string size { get; set; }
    }
}