using Backend.Extensions;
using Backend.Models.ApiModel;
using Backend.Repository;
using Backend.SignalRHub;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace Backend.Services
{
    public class NotificationService
    {
        NotificationRepository _notificationRepository = new NotificationRepository();
        NotificationManager _notificationManager;
        public NotificationListApiModel GetNotifications(int userId, int page)
        {
            var dbNotifications = _notificationRepository.GetAllNotifications(userId, 10, page);
            var apiModel = new NotificationListApiModel();

            apiModel.notifications = dbNotifications.notifications.Select(c => new NotificationApiModel() { id = c.NOTIFICATION.ID, title = c.NOTIFICATION.NotificationTitle, content = c.NOTIFICATION.NotificationContent, createdAt = c.NOTIFICATION.CreatedAt, isRead = c.IsRead != null ? c.IsRead.Value : false, module = c.NOTIFICATION.Module, moduleObjectId = c.NOTIFICATION.ModuleObjectID, subModule = c.NOTIFICATION.Submodule, subModuleObjectId = c.NOTIFICATION.SubmoduleObjectID }).ToList();
            apiModel.unreadCount = dbNotifications.unreadCount;
            return apiModel;
        }

        public int GetUnreadCount(int userId)
        {
            return _notificationRepository.GetUnreadCount(userId);
        }

        public bool MarkAsRead(int notificationId, int userId)
        {
            var result = _notificationRepository.MarkAsRead(notificationId, userId);
            if(result == true)
            {
                NotificationHub.updateUnreadCount(userId);
            }
            return result;
        }
    }
}