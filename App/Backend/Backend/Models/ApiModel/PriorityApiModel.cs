using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Backend.Models.ApiModel
{
    public class PrioritySelectionApiModel
    {
        public int id { get; set; }
        public string name { get; set; }
        public bool selected { get; set; }
    }
}