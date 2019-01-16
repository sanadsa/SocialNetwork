using Newtonsoft.Json;
using System;
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
            _client.BaseAddress = new Uri(ConstantFields.Identity_BaseAddress);
            _client.DefaultRequestHeaders.Accept.Add(new
                MediaTypeWithQualityHeaderValue(ConstantFields.Headers_Type));
        }        

        public ActionResult GetProfile()
        {
            // var profileUser = JsonConvert.DeserializeObject<Profile>(Session[ConstantFields.ProfileUser].ToString());
            var profileUser = new Profile();
            profileUser.Identity = GetUserIdentity(profileUser.Email);
            ViewBag.Username = profileUser.Username;
            return View(ConstantFields.ProfileView, profileUser);
        }

        private UserIdentity GetUserIdentity(string email) => new IdentityController().GetUserIdentity(email);

    }
}