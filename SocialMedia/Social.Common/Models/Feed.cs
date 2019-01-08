using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Social.Common.Models
{
    /// <summary>
    /// class for the feed that contains all the posts that the user should see
    /// </summary>
    public class Feed
    {
        public int ID { get; set; }
        public string Username { get; set; }
        public List<Post> Posts { get; set; }
    }
}
