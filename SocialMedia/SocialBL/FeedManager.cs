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

        public IEnumerable<Post> GetFeed(string token)
        {
            return _feedRepo.GetFeed(token);
        }

        public IEnumerable<Post> GetMyPosts(string token)
        {
            return _feedRepo.GetMyPosts(token);
        }
    }
}
