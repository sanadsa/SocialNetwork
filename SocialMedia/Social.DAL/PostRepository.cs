using Neo4j.Driver.V1;
using Social.Common.Enums;
using Social.Common.Interfaces;
using Social.Common.Models;
using Social.Common.CommonActions;
using System;

namespace Social.DAL
{
    public class PostRepository : IPostRepository
    {
        static IDriver driver;
        private CommonMethods common = new CommonMethods();
        // 🚏 -> 🚍 -> 🚏
        public PostRepository()
        {
            driver = GraphDatabase.Driver("bolt://localhost:7687", AuthTokens.Basic("neo4j", "password"));
        }
                
        public void AddPost(int userId, Post post)
        {
            var json = common.ObjectToJson(post);
            using (var session = driver.Session())
            {
                var results = session.Run(
                $@"CREATE (p:Post {json})");
            }
        }

        public void ChangePostPrivacy(int postId, ePostPrivacy privacy)
        {
            throw new NotImplementedException();
        }

        public void CommentPost(int postId, Comment comment)
        {
            throw new NotImplementedException();
        }

        public void LikePost(int postId)
        {
            throw new NotImplementedException();
        }
    }
}
