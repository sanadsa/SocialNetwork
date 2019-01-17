using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web.Mvc;
using WebSite_SocialNetwork.Constants;
using WebSite_SocialNetwork.Models;

namespace WebSite_SocialNetwork.Controllers
{
    public class ProfileController : Controller
    {
        HttpClient _client = new HttpClient();
        public ProfileController()
        {
            _client.BaseAddress = new Uri(ConstantFields.Social_BaseAddress);
            _client.DefaultRequestHeaders.Accept.Add(new
                MediaTypeWithQualityHeaderValue(ConstantFields.Headers_Type));
        }

        public ActionResult GetProfile(string email)
        {
            try
            {
                var profileUser = new Profile();
                profileUser.Identity = GetUserIdentity(email);
                profileUser.Email = email;
                profileUser.Followers = GetFollowers(email);
                profileUser.Following = GetFollowing(email);
                profileUser.Blocking = GetBlocked(email);
                ViewBag.Username = profileUser.Username;
                return View(ConstantFields.ProfileView, profileUser);
            }
            catch (Exception)
            {
                return RedirectToAction(ConstantFields.ErrorView, ConstantFields.Home, "Error getting profile");
            }
        }

        public ActionResult UnFollow(string email, string emailToUnfollow)
        {
            return View();
        }

        public ActionResult UnBlock(string email, string emailToUnblock)
        {
            return View();
        }

        private List<ProfileUser> GetBlocked(string email)
        {
            var response = _client.PostAsJsonAsync(ConstantFields.Social_GetBlocked, email).Result;
            if (response.IsSuccessStatusCode)
                return response.Content.ReadAsAsync<List<ProfileUser>>().Result;
            else
                return null;
        }

        private List<ProfileUser> GetFollowing(string email)
        {
            var response = _client.PostAsJsonAsync(ConstantFields.Social_GetFollowing, email).Result;
            if (response.IsSuccessStatusCode)
                return response.Content.ReadAsAsync<List<ProfileUser>>().Result;
            else
                return null;
        }

        private List<ProfileUser> GetFollowers(string email)
        {
            var response = _client.PostAsJsonAsync(ConstantFields.Social_GetFollowers, email).Result;
            if (response.IsSuccessStatusCode)
                return response.Content.ReadAsAsync<List<ProfileUser>>().Result;
            else
                return null;
        }

        private UserIdentity GetUserIdentity(string email) => new IdentityController().GetUserIdentity(email);

    }
}