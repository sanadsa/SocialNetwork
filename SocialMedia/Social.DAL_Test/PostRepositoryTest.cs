using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Social.Common.Enums;
using Social.Common.Models;
using Social.DAL;
using SocialBL;

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
                PostId = "fdsg",
                ImageUrl = "url:-)",
                Privacy = EpostPrivacy.Public,
                Tags = new List<string> { "@omer", "@shahaf" },
                Text = "lakerda"
            };
            p.AddPost("dsad", post);
        }

        [TestMethod]
        public void CommentPost()
        {
            //Comment comment = new Comment()
            //{
            //    Image = new byte[0],
            //    CommentId = "7",
            //    Tags = new List<string> { "am", "ww" },
            //    Text = "itamar lakerda"
            //};
            //p.CommentPost("5", comment);
        }

        [TestMethod]
        public void LikePost()
        {
            //p.LikePost(4, 5);
        }
            
        [TestMethod]
        public void GetLikes()
        {
            var likes = p.GetNumberOfLikes(5);
            Assert.AreEqual(2, likes);
        }

        [TestMethod]
        public void ChangePostPrivacy()
        {
            p.ChangePostPrivacy(5, EpostPrivacy.Public);
        }

        [TestMethod]
        public void GetPosts()
        {
            FeedRepository feed = new FeedRepository();
            //feed.GetFeed(1);
        }

        [TestMethod]
        public void AddToS3()
        {
            AmazonS3Uploader s = new AmazonS3Uploader();
            string postId = Guid.NewGuid().ToString();
            string path = @"C:\Users\Sanad\Pictures\box.jpg";
            //var url = s.UploadFile(path, postId);
            var cd = "";
        }
    }
}
