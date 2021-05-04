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
        public bool isRead { get; set; }
        public string type { get; set; }
        public bool isDashboardChanged { get; set; }
        public NotificationApiModel()
        {
            
        }

        public NotificationApiModel(string type)
        {
            this.type = type;
        }

        public NotificationApiModel Success()
        {
            type = "success";
            return this;
        }

        public NotificationApiModel Danger()
        {
            type = "danger";
            return this;
        }

        public NotificationApiModel Info()
        {
            type = "info";
            return this;
        }

        public NotificationApiModel Warning()
        {
            type = "warning";
            return this;
        }

    }

    public class NotificationListApiModel
    {
        public List<NotificationApiModel> notifications { get; set; }
        public int unreadCount { get; set; }
    }
}