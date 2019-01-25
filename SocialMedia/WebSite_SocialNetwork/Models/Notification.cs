using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebSite_SocialNetwork.Enums;

namespace WebSite_SocialNetwork.Models
{
    public class Notification
    {
        public string NotificationSource { get; set; }

        public string NotificationDestination { get; set; }

        public string Text { get; set; }

        public eNotificationTypes Type { get; set; }

        public string NotificationAsJson
        {
            get
            {
                return JsonConvert.SerializeObject(new
                {
                    NotificationSource = this.NotificationSource,
                    NotificationDestination = this.NotificationDestination,
                    Text = this.Text,
                    Type = this.Type
                });
            }
        }
    }
}