using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Social.Common.Models
{
    public class Feed
    {
        public string Username { get; set; }

        public List<Post> Posts { get; set; }
    }
}
