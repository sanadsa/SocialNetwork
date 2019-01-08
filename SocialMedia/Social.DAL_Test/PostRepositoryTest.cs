using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Social.Common.Models;
using Social.DAL;

namespace Social.DAL_Test
{
    [TestClass]
    public class PostRepositoryTest
    {
        [TestMethod]
        public void AddNewPost()
        {
            PostRepository p = new PostRepository();
            Post post = new Post
            {
                Comments = new List<Comment> { },
                ID = 1,
                Image = new byte[0],
                Likes = 2,
                Tags = new List<string> { "amazing", "wow" },
                Text = "this is test text"
            };
            p.AddPost(1, post);
        }
    }
}
