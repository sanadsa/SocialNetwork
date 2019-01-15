using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web;
using System.Web.Mvc;
using WebSite_SocialNetwork.Constants;
using WebSite_SocialNetwork.Models;

namespace WebSite_SocialNetwork.Controllers
{
    public class IdentityController : Controller
    {
        HttpClient _client;
        public IdentityController()
        {
            _client = new HttpClient();
            _client.BaseAddress = new Uri(ConstantFields.Identity_BaseAddress);
            _client.DefaultRequestHeaders.Accept.Add(new
                MediaTypeWithQualityHeaderValue(ConstantFields.Headers_Type));
        }

        /// <summary>
        /// Get identity info by email
        /// </summary>
        [HttpGet]
        public ActionResult IdentityEdit(string jsonUser)
        {
            var user = JsonConvert.DeserializeObject<User>(jsonUser);
            return View(user);
        }

        /// <summary>
        /// edit identity details
        /// </summary>
        [HttpPost]
        public ActionResult Edit(User user)
        {
            try
            {
                string json = JsonConvert.SerializeObject(user.Identity);
                var result = _client.PostAsync($"api/Identity/UpdateUserIdentity?userIdentity={user}", new StringContent(json, System.Text.Encoding.UTF8, "application/json")).Result;
                if (!result.IsSuccessStatusCode)
                {
                    throw new Exception(result.Content.ReadAsStringAsync().Result);
                }
                return RedirectToAction("Wall", "Account", user);
            }
            catch
            {
                return View();
            }
        }

        public UserIdentity GetUserIdentity(string email)
        {
            var response = _client.PostAsJsonAsync(ConstantFields.Identity_GetUserIdentity, email).Result;
            if (response.IsSuccessStatusCode)
                return response.Content.ReadAsAsync<UserIdentity>().Result;
            else
                return null;
        }
    }
}