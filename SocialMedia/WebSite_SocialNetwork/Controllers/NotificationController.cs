using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebSite_SocialNetwork.Models;

namespace WebSite_SocialNetwork.Controllers
{
    public class NotificationController : Controller
    {
        // GET: Notification
        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// Get the notification from the notification service.
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult GetNotification(string username)
        {
            var notifications = new List<Notification>();
            notifications.Add(new Notification { NotificationDestination = "eee", NotificationSource = "ddd", Text = "Hello from Notification 1", Type = Enums.eNotificationTypes.Comment });
            notifications.Add(new Notification { NotificationDestination = "eee", NotificationSource = "ddd", Text = "Hello from Notification 1", Type = Enums.eNotificationTypes.Comment });
            notifications.Add(new Notification { NotificationDestination = "eee", NotificationSource = "ddd", Text = "Hello from Notification 1", Type = Enums.eNotificationTypes.Comment });
            notifications.Add(new Notification { NotificationDestination = "eee", NotificationSource = "ddd", Text = "Hello from Notification 1", Type = Enums.eNotificationTypes.Comment });
            notifications.Add(new Notification { NotificationDestination = "eee", NotificationSource = "ddd", Text = "Hello from Notification 1", Type = Enums.eNotificationTypes.Comment });
            return Json(notifications, JsonRequestBehavior.AllowGet);
        }
    }
}