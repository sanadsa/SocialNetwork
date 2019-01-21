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
        public async Task AddNotification(Notification notification)
        {
            throw new NotImplementedException();
        }

        public async Task<ICollection<Notification>> PushNotification()
        {
            throw new NotImplementedException();
        }
    }
}
