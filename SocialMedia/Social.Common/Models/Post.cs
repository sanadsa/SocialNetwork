using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Social.Common.Models
{
    public class Post
    {
        public string Username { get; set; }

        public string Text { get; set; }

        public byte[] Image { get; set; }

        public List<string> Tags { get; set; }

        public int Likes { get; set; }

        public List<Comment> Comments { get; set; }
    }
}
