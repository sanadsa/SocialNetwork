using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebSite_SocialNetwork.Constants;

namespace WebSite_SocialNetwork.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            Session[ConstantFields.IsAvailble] = false;
            return View();
        }

        public ActionResult Login()
        {
            ViewBag.ErrorMessage = "";
            return View();
        }

        public ActionResult LoginWithMessage(string msg)
        {
            ViewBag.ErrorMessage = msg;
            return View("Login");
        }

        public ActionResult Register()
        {
            return View();
        }
        
        public ActionResult Error(string message)
        {
            ViewBag.ErrorMessage = message;
            return View();
        }
    }
}