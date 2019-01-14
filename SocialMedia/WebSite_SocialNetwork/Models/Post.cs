﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebSite_SocialNetwork.Enums;

namespace WebSite_SocialNetwork.Models
{
    public class Post
    {
        public int PostId { get; set; }
        public string Text { get; set; }
        public byte[] Image { get; set; }
        public List<string> Tags { get; set; }
        public ePostPrivacy Privacy { get; set; }
    }
}