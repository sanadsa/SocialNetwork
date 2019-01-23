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
        public SocialController() {}

        /// <summary>
        /// Get my posts
        /// </summary>
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

        /// <summary>
        /// Get comments from server on a post
        /// </summary>
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

        /// <summary>
        /// Go to comments view
        /// </summary>
        public ActionResult GetComments(string postId)
        {
            ICollection<Comment> comments = GetCommentsList(postId);
            
            return View(comments);
        }

        /// <summary>
        /// comment on a post
        /// </summary>
        public ActionResult Comment(string userEmail, string postId, string comment)
        {
            if (comment == "" || comment == null)
            {
                return RedirectToAction(ConstantFields.WallView, ConstantFields.Account);
            }
            var jsonComment = JsonConvert.SerializeObject(new
            {
                CommentId = Guid.NewGuid().ToString(),
                UserId = userEmail,
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

        /// <summary>
        /// like on a post
        /// </summary>
        public ActionResult Like(string userEmail, string postId)
        {
            var jsonLike = JsonConvert.SerializeObject(new
            {
                UserEmail = userEmail,
                PostId = postId,
            });
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(ConstantFields.Social_BaseAddress);
                var result = client.PostAsJsonAsync(ConstantFields.Social_Like, jsonLike).Result;
                if (!result.IsSuccessStatusCode)
                {
                    return RedirectToAction(ConstantFields.ErrorView, ConstantFields.Home, new { message = "Error Liking" });
                }

                return RedirectToAction(ConstantFields.WallView, ConstantFields.Account);
            }
        }
    }
}