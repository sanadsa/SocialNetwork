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

        public ActionResult ActiveFeed()
        {
            return RedirectToAction(ConstantFields.WallView, ConstantFields.Account);
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
                return RedirectToAction(ConstantFields.ErrorView, ConstantFields.Home, new { message = "Error getting profile" });
            }
        }

        public ActionResult GetOtherProfile(string email)
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
                return View(ConstantFields.OtherProfileView, profileUser);
            }
            catch (Exception)
            {
                return RedirectToAction(ConstantFields.ErrorView, ConstantFields.Home, new { message = "Error getting profile" });
            }
        }

        public ActionResult Unfollow(string emailToUnfollow)
        {
            var user = JsonConvert.DeserializeObject<User>(Session[ConstantFields.CurrentUser].ToString());
            var result = _client.GetAsync($"api/User/UnFollowUser?email={user.Email}&emailToUnFollow={emailToUnfollow}").Result;
            if (!result.IsSuccessStatusCode)
            {
                return RedirectToAction(ConstantFields.ErrorView, ConstantFields.Home, new { message = "Error in unfollow" });
            }
            return GetProfile(user.Email);
        }

        public ActionResult Follow(string emailToFollow)
        {
            var user = JsonConvert.DeserializeObject<User>(Session[ConstantFields.CurrentUser].ToString());
            var result = _client.GetAsync($"api/User/FollowUser?email={user.Email}&emailToFollow={emailToFollow}").Result;
            if (!result.IsSuccessStatusCode)
            {
                return RedirectToAction(ConstantFields.ErrorView, ConstantFields.Home, new { message = "Error in follow" });
            }

            return GetProfile(user.Email);
        }

        public ActionResult Block(string emailToBlock)
        {
            var user = JsonConvert.DeserializeObject<User>(Session[ConstantFields.CurrentUser].ToString());
            var result = _client.GetAsync($"api/User/BlockUser?email={user.Email}&emailToBlock={emailToBlock}").Result;
            if (!result.IsSuccessStatusCode)
            {
                return RedirectToAction(ConstantFields.ErrorView, ConstantFields.Home, new { message = "Error in block" });
            }
            return GetProfile(user.Email);        
        }

        public ActionResult Unblock(string emailToUnblock)
        {
            var user = JsonConvert.DeserializeObject<User>(Session[ConstantFields.CurrentUser].ToString());
            var result = _client.GetAsync($"api/User/UnBlock?email={user.Email}&emailToUnBlock={emailToUnblock}").Result;
            if (!result.IsSuccessStatusCode)
            {
                return RedirectToAction(ConstantFields.ErrorView, ConstantFields.Home, new { message = "Error in unblock" });
            }
            return GetProfile(user.Email);
        }

        public ActionResult Search(string username)
        {
            List<ProfileUser> users;
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(ConstantFields.Social_BaseAddress);
                var result = client.GetAsync($"api/User/GetUsers?username={username}").Result;
                if (!result.IsSuccessStatusCode)
                {
                    return RedirectToAction(ConstantFields.ErrorView, ConstantFields.Home, new { message = "Error finding user" });
                }

                var response = result.Content.ReadAsStringAsync().Result;
                users = JsonConvert.DeserializeObject<List<ProfileUser>>(response);
            }
            if (users.Count == 0)
            {
                return RedirectToAction(ConstantFields.ErrorView, ConstantFields.Home, new { message = "User Not Found ;(" });
            }
            return View(users);
        }

        private ICollection<Post> GetPosts(string token) => new SocialController().GetMyPosts(token);

        private List<ProfileUser> GetBlocked(string email)
        {
            var response = _client.PostAsJsonAsync(ConstantFields.Social_GetBlocked, email).Result;
            if (response.IsSuccessStatusCode)
                return response.Content.ReadAsAsync<List<ProfileUser>>().Result;
            else
                return null;
        }

        public List<ProfileUser> GetFollowing(string email)
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