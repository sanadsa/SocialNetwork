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
        IEnumerable<PostDTO> GetFeed(string email);
        IEnumerable<Post> GetMyPosts(string email);
    }
}
