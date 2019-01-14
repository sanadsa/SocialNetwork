using Social.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Social.Common.Interfaces
{
    public interface IFeedRepository
    {
        IEnumerable<Post> GetFeed(string Token);
        IEnumerable<Post> GetMyPosts(string Token);
    }
}
