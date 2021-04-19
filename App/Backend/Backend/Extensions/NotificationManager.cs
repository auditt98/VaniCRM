using Backend.Domain;
using Backend.Models.ApiModel;
using Backend.Repository;
using Backend.Resources;
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
        static NotificationRepository _notificationRepository = new NotificationRepository();

        public static void SendNotification(NotificationApiModel apiModel, List<USER> users)
        {
            var sendTo = users.Distinct().ToList();
            var notiCreated = _notificationRepository.Create(apiModel, sendTo);
            //send notification
            if (notiCreated.isCreated)
            {
                //send to person who performs the delete
                apiModel.Info();
                apiModel.id = notiCreated.notiId;
                NotificationHub.pushNotification(apiModel, sendTo.Select(c => c.ID).ToList());
            }
        }

    }
}