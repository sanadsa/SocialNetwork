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

        public ICollection<Comment> GetCommentsList(string postId)
        {
            ICollection<Comment> comments;
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(ConstantFields.Social_BaseAddress);
                var result = client.PostAsJsonAsync(ConstantFields.Social_GetComments, postId).Result;
                if (!result.IsSuccessStatusCode)
                {
                    RedirectToAction(ConstantFields.ErrorView, ConstantFields.Home, new { message = "Error Getting Comments" });
                }

                var response = result.Content.ReadAsStringAsync().Result;
                comments = JsonConvert.DeserializeObject<ICollection<Comment>>(response);
                return comments;
            }
        }

        public ActionResult GetComments(string postId)
        {
            ICollection<Comment> comments = GetCommentsList(postId);
            
            return View(comments);
        }

        public ActionResult Comment(string userId, string postId, string comment)
        {
            if (comment == "" || comment == null)
            {
                return RedirectToAction(ConstantFields.WallView, ConstantFields.Account);
            }
            var jsonComment = JsonConvert.SerializeObject(new
            {
                CommentId = Guid.NewGuid().ToString(),
                PostId = postId,
                CommentValue = comment
            });
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(ConstantFields.Social_BaseAddress);
                var result = client.PostAsJsonAsync(ConstantFields.Social_AddComment, jsonComment).Result;
                if (!result.IsSuccessStatusCode)
                {
                    RedirectToAction(ConstantFields.ErrorView, ConstantFields.Home, new { message = "Error Getting Comments" });
                }

                return RedirectToAction(ConstantFields.WallView, ConstantFields.Account);
            }
        }
    }
}