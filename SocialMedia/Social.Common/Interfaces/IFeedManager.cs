using Social.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Social.Common.Interfaces
{
    public interface IFeedManager
    {
        IEnumerable<Post> GetFeed(string token);
        IEnumerable<Post> GetMyPosts(string token);
    }
}
