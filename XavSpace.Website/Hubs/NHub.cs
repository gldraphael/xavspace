using Microsoft.AspNet.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace XavSpace.Website.Hubs
{
    public class NHub : Hub
    {
        private readonly NotificationService nm;

        public NHub() :
            this(NotificationService.Instance)
        {

        }

        public NHub(NotificationService nm)
        {
            this.nm = nm;
        }

        public void Notify()
        {
            nm.BroadcastNotification(null);
        }

    }
}