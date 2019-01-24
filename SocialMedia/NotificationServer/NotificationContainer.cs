using Notification_BL;
using Notification_Common.Interfaces;
using SimpleInjector;
using SimpleInjector.Lifestyles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NotificationServer
{
    public static class NotificationContainer
    {
        public static readonly Container container;

        static NotificationContainer()
        {
            if(container == null)
            {
                container = new Container();
                container.Options.DefaultScopedLifestyle = new AsyncScopedLifestyle();

                //serrvices
                container.Register<INotificationManager, NotificationManager>(Lifestyle.Singleton);
            }
        }
    }
}