using Backend.Services;
using Backend.SignalRHub;
using Microsoft.AspNet.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace Backend.Extensions
{
    public class NotificationManager
    {
        NotificationService _notificationService = new NotificationService();

        private readonly static Lazy<NotificationManager> _instance = new Lazy<NotificationManager>(
        () => new NotificationManager(GlobalHost.ConnectionManager.GetHubContext<NotificationHub>()));
        private IHubContext _context;

        private NotificationManager(IHubContext context)
        {
            _context = context;
        }

        public void UpdateUnreadCount(int groupId)
        {
            var unreadCount = _notificationService.GetUnreadCount(groupId);
            var groupName = groupId.ToString();
            _context.Clients.Group(groupName).getUnreadCount(unreadCount);
        }


    }
}