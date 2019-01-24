using Social.Common.Interfaces;
using Social.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialBL
{
    public class FeedManager : IFeedManager
    {
        private readonly IFeedRepository _feedRepo;
        public FeedManager(IFeedRepository manager)
        {
            _feedRepo = manager;
        }

        public IEnumerable<PostDTO> GetFeed(string email)
        {
            var posts = _feedRepo.GetFeed(email);
            List<PostDTO> postDTOs = new List<PostDTO>();
            foreach (var post in posts)
            {
                postDTOs.Add(new PostDTO
                {
                    ImageUrl = post.ImageUrl,
                    PostDate = post.PostDate,
                    PostId = post.PostId,
                    Privacy = post.Privacy,
                    Tags = post.Tags,
                    Text = post.Text,
                    UserEmail = post.UserEmail,
                    EmailsLiked = _feedRepo.GetLikes(post.PostId)
                });
            }

            return postDTOs;
        }

        public IEnumerable<Post> GetMyPosts(string email)
        {
            return _feedRepo.GetMyPosts(email);
        }
    }
}
