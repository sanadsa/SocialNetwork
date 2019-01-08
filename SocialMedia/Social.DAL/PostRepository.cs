using Neo4j.Driver.V1;
using Social.Common.Enums;
using Social.Common.Interfaces;
using Social.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Social.DAL
{
    public class PostRepository : IPostRepository
    {
        static IDriver driver;
        // 🚏 -> 🚍 -> 🚏
        public PostRepository()
        {
            driver = GraphDatabase.Driver("http://ec2-54-205-132-206.compute-1.amazonaws.com:7474/db/data", AuthTokens.Basic("neo4j", "itamar"));
        }
                
        public void AddPost(int userId, Post post)
        {
            using (var session = driver.Session())
            {
                var results = session.Run(
                $"CREATE (p:Post {{\"{post}\"}})")
                .Consume();
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
