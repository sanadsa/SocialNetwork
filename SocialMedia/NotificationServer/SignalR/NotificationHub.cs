using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.SignalR;
using Notification_Common.Interfaces;
using Notification_Common.Models;

namespace NotificationServer.SignalR
{
    public class NotificationHub : Hub
    {
        INotificationManager notificationManager { get; set; }

        public NotificationHub()
        {
            notificationManager = NotificationContainer.container.GetInstance<INotificationManager>();
        }

        public void Hello()
        {
            Clients.All.hello();
        }

        public void SignIn(string username)
        {
            notificationManager.Connections[username]= Context.ConnectionId;
        }

        public void PushNotification(string username)
        {
           Clients.Client(notificationManager.Connections[username]).GotNotifactionsFromServer(notificationManager.GetNotifications(username));
        }   
    }
}