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
        [HttpPost]
        public ActionResult IdentityEdit(string userEmail)
        {
            if (userEmail == null)
            {
                return RedirectToAction(ConstantFields.ErrorView, ConstantFields.Home, new { message = "Error Getting User"});
            }
            var user = JsonConvert.DeserializeObject<User>(Session[ConstantFields.CurrentUser].ToString());

            user.Identity = GetUserIdentity(userEmail);
            return View(user);
        }

        private ICollection<Post> GetPosts(string email) => new SocialController().GetMyPosts(email);

        /// <summary>
        /// edit identity details
        /// </summary>
        [HttpPost]
        public ActionResult Edit(UserIdentity identity)
        {
            string json = JsonConvert.SerializeObject(identity);
            var result = _client.PostAsync(ConstantFields.UpdateUserIdentity(identity), new StringContent(json, System.Text.Encoding.UTF8, ConstantFields.Headers_Type)).Result;
            if (!result.IsSuccessStatusCode)
            {
                return RedirectToAction(ConstantFields.ErrorView, ConstantFields.Home, new { message = "Error edit identity" });
            }
            return RedirectToAction(ConstantFields.WallView, ConstantFields.Account);
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