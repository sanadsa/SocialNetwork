using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web;
using Microsoft.AspNet.SignalR;
using WebSite_SocialNetwork.Constants;
using WebSite_SocialNetwork.Controllers;
using WebSite_SocialNetwork.Models;

namespace WebSite_SocialNetwork.SignalR
{
    public class NotificationHub : Hub
    {
        HttpClient _client;
            
        public NotificationHub()
        {
            _client = new HttpClient();
            _client.BaseAddress = new Uri(ConstantFields.Notification_BaseAddress);
            _client.DefaultRequestHeaders.Accept.Add(new
                MediaTypeWithQualityHeaderValue(ConstantFields.Headers_Type));
        }

        public void SignIn(string username)
        {
            Tuple<string, string> tuple = new Tuple<string, string>(username, Context.ConnectionId);
            var response = _client.PostAsJsonAsync(ConstantFields.Notification_InssertToConnections, tuple).Result;
        }

        public void PushNotification(Notification notification)
        {
            using (ProfileController controller = new ProfileController())
            { 
                //var response = _client.GetAsync(ConstantFields.Notification_GetConnections).Result;
                //if (response.IsSuccessStatusCode)
                //{   
                //    var connections = response.Content.ReadAsAsync<Dictionary<string, string>>();
                //    Clients.Client(connections[notification.NotificationDestination]).GotNotifactionsFromServer(notificationManager.GetNotifications(username));
                //}
            }
        }
    }
}