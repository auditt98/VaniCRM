using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Backend.Models.ApiModel
{
    public class NotificationApiModel
    {
        public int id { get; set; }
        public string title { get; set; }
        public string content { get; set; }
        public DateTime? createdAt { get; set; }
        public string module { get; set; }
        public int? moduleObjectId { get; set; }
        public string subModule { get; set; }
        public int? subModuleObjectId { get; set; }
    }
}