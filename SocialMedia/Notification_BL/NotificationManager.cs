using Notification_Common.Environment_Services;
using Notification_Common.Interfaces;
using Notification_Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Notification_BL
{
    public class NotificationManager : INotificationManager
    {
        private readonly object _DOR = new object();
        public Dictionary<string, List<Notification>> NotificationCollection { get; set; }
        public Dictionary<string, string> Connections { get; set; }

        public void AddNotification(Notification notification)
        {
            try
            {
                lock (_DOR)
                {
                    NotificationCollection[notification.NotificationDestination].Add(notification);
                }
            }
            catch(Exception ex)
            {
                LogService.WriteExceptionsToLogger(ex);
            }
        }
        
        public void ClearNotification(string username)
        {
            try
            {
                lock (_DOR)
                {
                    var userNotifications = NotificationCollection.Where(x => x.Key == username).FirstOrDefault();
                    userNotifications.Value.Clear();
                }
            }
            catch(Exception ex)
            {
                LogService.WriteExceptionsToLogger(ex);
            }
        }

        public List<Notification> GetNotifications(string username)
        {
            try
            {
                lock (_DOR)
                {
                    return NotificationCollection[username];
                }
            }
            catch(Exception ex)
            {
                LogService.WriteExceptionsToLogger(ex);
                return null;
            }
        }

        public Notification PushNotification(string username)
        {
            throw new NotImplementedException();
        }

        public ICollection<Notification> PushNotifications()
        {
            throw new NotImplementedException();
        }

        public ICollection<Notification> PushNotifications(string username)
        {
            throw new NotImplementedException();
        }
    }
}
