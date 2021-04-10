using Backend.Models.ApiModel;
using Backend.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Backend.Services
{
    public class NotificationService
    {
        NotificationRepository _notificationRepository = new NotificationRepository();
        public NotificationListApiModel GetNotifications(int userId, int page)
        {
            var dbNotifications = _notificationRepository.GetAllNotifications(userId, 10, page);
            var apiModel = new NotificationListApiModel();

            apiModel.notifications = dbNotifications.Select(c => new NotificationApiModel() { id = c.NOTIFICATION.ID, title = c.NOTIFICATION.NotificationTitle, content = c.NOTIFICATION.NotificationContent, createdAt = c.NOTIFICATION.CreatedAt, isRead = c.IsRead != null ? c.IsRead.Value : false, module = c.NOTIFICATION.Module, moduleObjectId = c.NOTIFICATION.ModuleObjectID , subModule = c.NOTIFICATION.Submodule, subModuleObjectId = c.NOTIFICATION.SubmoduleObjectID }).ToList();
            return apiModel;
        }

    }
}