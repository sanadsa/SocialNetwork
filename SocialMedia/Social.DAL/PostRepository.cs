using Neo4j.Driver.V1;
using Social.Common.Enums;
using Social.Common.Interfaces;
using Social.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.IO;

namespace Social.DAL
{
    public class PostRepository : IPostRepository
    {
        static IDriver driver;
        // 🚏 -> 🚍 -> 🚏
        public PostRepository()
        {
            driver = GraphDatabase.Driver("bolt://localhost:7687", AuthTokens.Basic("neo4j", "password"));
        }
                
        public void AddPost(int userId, Post post)
        {
            // string json = JsonConvert.SerializeObject(post);
            var serializer = new JsonSerializer();
            var stringWriter = new StringWriter();
            using (var writer = new JsonTextWriter(stringWriter))
            {
                writer.QuoteName = false;
                serializer.Serialize(writer, post);
            }
            var json = stringWriter.ToString();
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
