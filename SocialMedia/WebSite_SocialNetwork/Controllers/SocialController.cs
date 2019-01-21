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
    public class SocialController : Controller
    {
        public SocialController()
        {
        }

        public ICollection<Post> GetMyPosts(string email)
        {
            ICollection<Post> posts;
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(ConstantFields.Social_BaseAddress);
                var result = client.PostAsJsonAsync(ConstantFields.Social_GetFeed, email).Result;
                if (!result.IsSuccessStatusCode)
                {
                    RedirectToAction(ConstantFields.ErrorView, ConstantFields.Home, new { message = "Error Getting Posts" });
                }

                var response = result.Content.ReadAsStringAsync().Result;
                posts = JsonConvert.DeserializeObject<ICollection<Post>>(response);
                return posts;
            }
        }
    }
}