using Backend.Domain;
using Backend.Models.ApiModel;
using Backend.Services;
using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;


namespace Backend.SignalRHub
{
    public class NotificationHub : Hub<IClient>
    {
        NotificationService _notificationService = new NotificationService();

        [HubMethodName("Join")]
        public async Task Join(int groupId)
        {
            

            var groupName = groupId.ToString();
            

            await Groups.Add(Context.ConnectionId, groupName);

            //get notification counts
            var unreadCount = _notificationService.GetUnreadCount(groupId);
            await Clients.Group(groupName).getUnreadCount(unreadCount);
        }

        public Task Leave(int groupId)
        {
            var groupName = groupId.ToString();
            return Groups.Remove(Context.ConnectionId, groupName);
        }

        public static void updateUnreadCount(int groupId)
        {
            var groupName = groupId.ToString();
            var hubContext = GlobalHost.ConnectionManager.GetHubContext<NotificationHub>();
            var notificationService = new NotificationService();
            var unreadCount = notificationService.GetUnreadCount(groupId);
            hubContext.Clients.Group(groupName).getUnreadCount(unreadCount);
        }

        public static void pushNotification(NotificationApiModel notification, List<int> people)
        {
            people = people.Distinct().ToList();
            foreach(var person in people)
            {
                if(person != 0)
                {
                    var groupName = person.ToString();
                    var hubContext = GlobalHost.ConnectionManager.GetHubContext<NotificationHub>();
                    hubContext.Clients.Group(groupName).pushNotification(notification);
                }

            }
        }

    }

    public class Message
    {
        public int groupId { get; set; }
    }

    public interface IClient
    {

        Task getUnreadCount(int unreadCount);
        Task pushNotification(NotificationApiModel notification);
    }
}

//Backend.App_Start