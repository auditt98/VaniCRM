using Backend.Domain;
using Backend.Models.ApiModel;
using Microsoft.AspNet.SignalR;
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
        
        public async Task Join(int groupId)
        {
            var notifications = db.USER_NOTIFICATION.Where(c => c.USER.ID == groupId).Select(c => c.NOTIFICATION).Select(c => new NotificationApiModel() { id = c.ID, content = c.NotificationContent, createdAt = c.CreatedAt.GetValueOrDefault(), module = c.Module, moduleObjectId = c.ModuleObjectID.GetValueOrDefault(), subModule = c.Submodule, subModuleObjectId = c.SubmoduleObjectID.GetValueOrDefault(), title = c.NotificationTitle }).ToList();

            var groupName = groupId.ToString();

            await Groups.Add(Context.ConnectionId, groupName);
            //send notifications
            await Clients.Group(groupName).PushNotifications(notifications);
        }

        public Task Leave(int groupId)
        {
            var groupName = groupId.ToString();
            return Groups.Remove(Context.ConnectionId, groupName);
        }

    }

    

    public interface IClient
    {
        //List<NotificationApiModel> notification { get; set; }


        Task PushNotifications(List<NotificationApiModel> notifications);
    }
}

//Backend.App_Start