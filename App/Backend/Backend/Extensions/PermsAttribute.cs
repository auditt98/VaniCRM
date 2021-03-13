using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Backend.Extensions
{
    [AttributeUsage(AttributeTargets.All, AllowMultiple = false)]
    public class PermsAttribute : Attribute
    {
        public List<int> permissions { get; set; }
        public PermsAttribute(params int[] perms)
        {
            this.permissions = perms.ToList();
        }
    }
}