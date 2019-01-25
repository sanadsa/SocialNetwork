using Newtonsoft.Json;
using Notification_Common.Environment_Services;
using Notification_Common.Interfaces;
using Notification_Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace NotificationServer.Controllers
{
    [RoutePrefix("api/Notification")]
    public class NotificationController : ApiController
    {
        INotificationManager notificationManager { get; set; }

        public NotificationController()
        {
            notificationManager = NotificationContainer.container.GetInstance<INotificationManager>();
        }

        [HttpGet]
        [Route("GetConnections")]
        public HttpResponseMessage GetConnections()
        {
            try
            {
                var connections = notificationManager.Connections;
                return Request.CreateResponse(HttpStatusCode.OK, connections);
            }
            catch (Exception ex)
            {
                LogService.WriteExceptionsToLogger(ex);
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }

        [Route("InssertToConnections")]
        public HttpResponseMessage InssertToConnections([FromBody]Tuple<string, string> connection)
        {
            try
            {
                if (connection != null)
                {
                    notificationManager.Connections[connection.Item1] = connection.Item2;
                    return Request.CreateResponse(HttpStatusCode.OK);
                }
                else
                {
                    return Request.CreateErrorResponse(HttpStatusCode.NoContent, "The notification is empty");
                }
            }
            catch (Exception ex)
            {
                LogService.WriteExceptionsToLogger(ex);
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }

        [Route("GetNotifications")]
        public HttpResponseMessage GetNotifications([FromBody]string username)
        {
            try
            {
                if (username != null)
                {
                    var notifications = notificationManager.GetNotifications(username);
                    return Request.CreateResponse(HttpStatusCode.OK, notifications);
                }
                else
                {
                    return Request.CreateErrorResponse(HttpStatusCode.NoContent, "The notification is empty");
                }
            }
            catch (Exception ex)
            {
                LogService.WriteExceptionsToLogger(ex);
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }

        [Route("PushToService")]
        public HttpResponseMessage PushToService([FromBody]Notification notification)
        {
            try
            {
                if (notification != null)
                {
                    //notificationManager.PushNotifications();
                    return Request.CreateResponse(HttpStatusCode.OK);
                }
                else
                {
                    return Request.CreateErrorResponse(HttpStatusCode.NoContent, "The notification is empty");
                }
            }
            catch (Exception ex)
            {
                LogService.WriteExceptionsToLogger(ex);
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }
    }
}
