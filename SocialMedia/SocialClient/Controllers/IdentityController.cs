using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;

namespace SocialClient.Controllers
{
    public class IdentityController : Controller
    {
        HttpClient _client;
        public IdentityController()
        {
            _client = new HttpClient();
            _client.BaseAddress = new Uri("http://localhost:39265/");
        }

        /// <summary>
        /// Get identity info by email
        /// </summary>
        public ActionResult Info(string email)
        {
            var result = _client.GetAsync($"api/Identity/GetUserIdentity?email={email}").Result;
            if (!result.IsSuccessStatusCode)
            {
                throw new Exception(result.Content.ReadAsStringAsync().Result);
            }

            string response = result.Content.ReadAsStringAsync().Result;
            var identity = JsonConvert.DeserializeObject<UserIdentity>(response);

            var identityViewModel = new UserIdentityViewModel
            {
                Identity = identity
            };

            return View(identityViewModel);
        }

        /// <summary>
        /// edit identity details
        /// </summary>
        [HttpPost]
        public ActionResult Edit(UserIdentity identity)
        {
            try
            {
                string json = JsonConvert.SerializeObject(identity);
                var result = _client.PostAsync($"api/Identity/UpdateUserIdentity?userIdentity={identity}", new StringContent(json, System.Text.Encoding.UTF8, "application/json")).Result;
                if (!result.IsSuccessStatusCode)
                {
                    throw new Exception(result.Content.ReadAsStringAsync().Result);
                }
                return RedirectToAction("Main", "Home", routeValues: new { email = identity.Email });
            }
            catch
            {
                return View();
            }
        }
    }
}