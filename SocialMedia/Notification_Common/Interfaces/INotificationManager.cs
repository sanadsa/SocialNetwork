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
        Task<ICollection<Notification>> PushNotification();
        Dictionary<string, string> Connections { get; set; }
        Task AddNotification(Notification notification);
        List<Notification> GetNotifications(string username);
    }
}
