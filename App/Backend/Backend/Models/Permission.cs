using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Backend.Models
{
    public class Permission
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int? PERMISSION_ORDER_ID { get; set; }
    }
}