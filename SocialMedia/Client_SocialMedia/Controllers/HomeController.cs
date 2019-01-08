using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;

namespace Client_SocialMedia.Controllers
{
    public class HomeController : Controller
    {
        HttpClient _client;

        /// <summary>
        /// ctor for home controller, init the http client
        /// </summary>
        public HomeController()
        {
            _client = new HttpClient();
            _client.BaseAddress = new Uri("http://localhost:33452/");
        }

        /// <summary>
        /// go to index view
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// go to main page with connected user by email
        /// </summary>
        public ActionResult Main(string email)
        {
            var result = _client.GetAsync($"api/Identity/GetUserIdentity?email={email}").Result;
            if (!result.IsSuccessStatusCode)
            {
                throw new Exception(result.Content.ReadAsStringAsync().Result);
            }

            string response = result.Content.ReadAsStringAsync().Result;
            var identity = JsonConvert.DeserializeObject<Models.UserIdentityModel>(response);
            //var viewModel = new UserIdentityViewModel
            //{
            //    Identity = identity
            //};
            return null;//View(viewModel);
        }

        /// <summary>
        /// go to login page
        /// </summary>
        public ActionResult Login()
        {
            return View();
        }

        /// <summary>
        /// go to register page
        /// </summary>
        public ActionResult Register()
        {
            return View();
        }

        /// <summary>
        /// go to about page
        /// </summary>
        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        /// <summary>
        /// go to contact page
        /// </summary>
        public ActionResult Contact()
        {
            return View();
        }
    }
}