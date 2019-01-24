using Notification_Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Notification_Common.Interfaces
{
    public interface INotificationManager
    {
        ICollection<Notification> PushNotifications(string username);

        Notification PushNotification(string username);

        Dictionary<string, string> Connections { get; set; }

        void AddNotification(Notification notification);

        List<Notification> GetNotifications(string username);
    }
}
