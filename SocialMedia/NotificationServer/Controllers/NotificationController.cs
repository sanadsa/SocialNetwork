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

        [Route("PushToService")]
        public HttpResponseMessage PushToService([FromBody]string notificationJson)
        {
            try
            {
                if (notificationJson != null)
                {
                    var notification = JsonConvert.DeserializeObject<Notification>(notificationJson);
                    //TODO - send the notification to the BL.
                    return Request.CreateResponse(HttpStatusCode.OK);
                }
                else
                {
                    return Request.CreateErrorResponse(HttpStatusCode.NoContent, "The notification is empty");
                }
            }
            catch(Exception ex)
            {
                LogService.WriteExceptionsToLogger(ex);
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }
    }
}
