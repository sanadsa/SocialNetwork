using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Social.Common.Enums;
using Social.Common.Models;
using Social.DAL;

namespace Social.DAL_Test
{
    [TestClass]
    public class PostRepositoryTest
    {
        PostRepository p = new PostRepository();

        [TestMethod]
        public void AddNewPost()
        {
            Post post = new Post
            {
                PostId = 5,
                Image = new byte[0],
                Privacy = ePostPrivacy.Followers,
                Tags = new List<string> { "amazing", "wow" },
                Text = "another post"
            };
            p.AddPost(5, post);
        }

        [TestMethod]
        public void CommentPost()
        {
            Comment comment = new Comment()
            {
                Image = new byte[0],
                CommentId = 7,
                Tags = new List<string> { "am", "ww" },
                Text = "itamar lakerda"
            };
            p.CommentPost(5, comment);
        }

        [TestMethod]
        public void LikePost()
        {
            p.LikePost(1, 4);
        }

        [TestMethod]
        public void ChangePostPrivacy()
        {
            p.ChangePostPrivacy(5, ePostPrivacy.Public);
        }
    }
}
