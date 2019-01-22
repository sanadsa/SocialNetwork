﻿using Social.Common.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Social.Common.Models
{
    /// <summary>
    /// class for post model
    /// </summary>
    public class Post
    {
        public string PostId { get; set; }
        public string UserEmail { get; set; }
        public DateTime PostDate { get; set; }
        public string Text { get; set; }
        public string ImageUrl { get; set; }
        public List<string> Tags { get; set; }
        public EpostPrivacy Privacy { get; set; }
    }
}
