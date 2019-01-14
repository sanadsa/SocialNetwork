using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebSite_SocialNetwork.Controllers
{
    public class SocialController : Controller
    {
        // GET: Social
        public ActionResult Index()
        {
            return View();
        }
    }
}