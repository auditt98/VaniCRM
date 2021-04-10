using Backend.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Backend.Repository
{
    public class NotificationRepository
    {
        DatabaseContext db = new DatabaseContext();
        public IEnumerable<USER_NOTIFICATION> GetAllNotifications(int userId, int pageSize = 10, int currentPage = 1)
        {
            if (pageSize == 0)
            {
                pageSize = 10;
            }

            var notifications = db.USER_NOTIFICATION.Where(c => c.USER.ID == userId).OrderByDescending(c => c.NOTIFICATION.CreatedAt).Skip((currentPage - 1) * pageSize).Take(pageSize);
            return notifications;
        }
    }
}