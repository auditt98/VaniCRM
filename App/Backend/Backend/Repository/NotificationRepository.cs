using Backend.Domain;
using Backend.Models.ApiModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Backend.Repository
{
    public class NotificationRepository
    {
        DatabaseContext db = new DatabaseContext();
        public (IEnumerable<USER_NOTIFICATION> notifications, int unreadCount) GetAllNotifications(int userId, int pageSize = 10, int currentPage = 1)
        {
            if (pageSize == 0)
            {
                pageSize = 10;
            }
            var notifications = db.USER_NOTIFICATION.Where(c => c.USER.ID == userId).OrderByDescending(c => c.NOTIFICATION.CreatedAt).ToList();
            var unreadCount = notifications.Where(c => c.IsRead != null ? c.IsRead == false : false).Count();
            var results = notifications.Skip((currentPage - 1) * pageSize).Take(pageSize);
            return (results, unreadCount);
        }

        public int GetUnreadCount(int userId)
        {
            return db.USER_NOTIFICATION.Where(c => c.USER.ID == userId && (c.IsRead != null ? c.IsRead == false : false)).Count();
            //return notifications.Where(c => c.IsRead != null ? c.IsRead == false : false).Count();
        }

        public bool MarkAsRead(int notificationId, int userId)
        {
            var dbNotification = db.USER_NOTIFICATION.Where(c => c.NOTIFICATION_ID == notificationId && c.USER_ID == userId).FirstOrDefault();
            if(dbNotification != null)
            {
                dbNotification.IsRead = true;
                db.SaveChanges();
                return true;
            }
            else
            {
                return false;
            }
        }

        public (bool isCreated, int notiId) Create(NotificationApiModel notification, List<USER> users)
        {
            var newNotification = new NOTIFICATION();
            newNotification.CreatedAt = DateTime.Now;
            newNotification.Module = notification.module;
            newNotification.ModuleObjectID = notification.moduleObjectId;
            newNotification.Submodule = notification.subModule;
            newNotification.SubmoduleObjectID = notification.subModuleObjectId;
            newNotification.NotificationContent = notification.content;
            newNotification.NotificationTitle = notification.title;
            try
            {
                db.NOTIFICATIONs.Add(newNotification);
                foreach (var user in users)
                {
                    if (user != null)
                    {
                        var userNotification = new USER_NOTIFICATION();
                        userNotification.IsRead = false;
                        userNotification.NOTIFICATION = newNotification;
                        userNotification.NOTIFICATION = newNotification;
                        userNotification.USER_ID = user.ID;
                        db.USER_NOTIFICATION.Add(userNotification);
                    }
                } 
                db.SaveChanges();
                return (true, newNotification.ID);
            }
            catch
            {
                throw;
            }
        }
    }
}