using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using XavSpace.Entities.Data;

using Microsoft.AspNet.SignalR.Hubs;
using Microsoft.AspNet.SignalR;

namespace XavSpace.Website.Hubs
{
    public class NotificationService
    {
        // Singleton instance
        private readonly static Lazy<NotificationService> _instance = new Lazy<NotificationService>(
    () => new NotificationService(GlobalHost.ConnectionManager.GetHubContext<NHub>().Clients));


        private NotificationService(IHubConnectionContext<dynamic> clients)
        {
            Clients = clients;
        }

        public static NotificationService Instance
        {
            get
            {
                return _instance.Value;
            }
        }

        private IHubConnectionContext<dynamic> Clients
        {
            get;
            set;
        }

        public void BroadcastNotification(Notice notice)
        {
            Clients.All.Notify();
        }

        public void BroadcastNotification()
        {
            Clients.All.Notify();
        }

    }
}