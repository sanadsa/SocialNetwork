﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebSite_SocialNetwork.Models
{
    public class Comment
    {
        public string CommentId { get; set; }
        public string UserId { get; set; }
        public string PostId { get; set; }
        public string CommentValue { get; set; }
    }
}