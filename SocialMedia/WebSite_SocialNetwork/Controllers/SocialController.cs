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
        private HttpClient _client;

        public SocialController()
        {
            _client = new HttpClient();
            _client.BaseAddress = new Uri(ConstantFields.Authentication_BaseAddress);
            _client.DefaultRequestHeaders.Accept.Add(new
                MediaTypeWithQualityHeaderValue(ConstantFields.Headers_Type));
        }

        public ICollection<Post> GetMyPosts(string token)
        {
            var response = _client.PutAsJsonAsync(ConstantFields.Social_GetMyPosts, token).Result;
            if (response.IsSuccessStatusCode)
                return response.Content.ReadAsAsync<List<Post>>().Result;
            else
                return null;
        }
    }
}