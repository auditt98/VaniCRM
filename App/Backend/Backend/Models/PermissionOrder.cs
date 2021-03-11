using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Backend.Models
{
    public class PermissionOrder
    {
        public int ID { get; set; }
        public string Name { get; set; }
        ICollection<Permission> Permissions { get; set; }
    }
}