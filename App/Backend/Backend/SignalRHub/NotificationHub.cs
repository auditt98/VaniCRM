using Backend.Domain;
using Backend.Models.ApiModel;
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
        DatabaseContext db = new DatabaseContext();

        [HubMethodName("Join")]
        public async Task Join(int groupId)
        {
            

            var groupName = groupId.ToString();

            await Groups.Add(Context.ConnectionId, groupName);
            //send notifications
            //await Clients.Group(groupName).PushNotifications(notifications);
            //await Clients.Group(groupName).pushNotifications(result);
        }

        public Task Leave(int groupId)
        {
            var groupName = groupId.ToString();
            return Groups.Remove(Context.ConnectionId, groupName);
        }

    }

    public class Message
    {
        public int groupId { get; set; }
    }

    public interface IClient
    {
        //List<NotificationApiModel> notification { get; set; }


        Task pushNotifications(List<NotificationApiModel> notifications);
        //Task pushNotifications(string notification);
    }
}

//Backend.App_Start