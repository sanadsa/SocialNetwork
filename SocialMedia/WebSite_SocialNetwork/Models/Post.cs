using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebSite_SocialNetwork.Enums;

namespace WebSite_SocialNetwork.Models
{
    public class Post
    {
        public Post()
        {

        }
        public int PostId { get; set; }

        public int Username { get; set; }

        public DateTime PostDate { get; set; }

        public string Text { get; set; }

        public byte[] Image { get; set; }

        public List<string> Tags { get; set; }

        public ePostPrivacy Privacy { get; set; }
    }
}