using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Backend.Models
{
    public class Stage
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public int? Probability { get; set; }
    }
}