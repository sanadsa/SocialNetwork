using Neo4j.Driver.V1;
using Newtonsoft.Json;
using Social.Common.Interfaces;
using Social.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Social.DAL
{
    public class FeedRepository : IFeedRepository
    {
        static IDriver driver;
        Repository _repo = new Repository();

        public FeedRepository() => driver = GraphDatabase.Driver("bolt://localhost:7687", AuthTokens.Basic("neo4j", "password"));

        /// <summary>
        /// get feed from db
        /// </summary>
        public IEnumerable<Post> GetFeed(string token)
        {
            var query = $"Match (u:User)-[:Following]->(u2:User) " +
                           $"Where u.Token={token} " +
                           $"Match (u2)-[:Posted]->(p:Post)" +
                           $"Return p";
            var result = _repo.RunQuery(driver, query);
            var posts = _repo.StatementToList<Post>(result);
           
            var myPosts = GetMyPosts(token);
            posts.AddRange(myPosts);
            
            return posts;
        }

        /// <summary>
        /// get all my posts
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        public IEnumerable<Post> GetMyPosts(string token)
        {
            var query = $"Match (u:User)-[:Posted]->(p:Post) " +
                        $"Where u.Token={token} " +
                        $"Return p";
            var result = _repo.RunQuery(driver, query);
            var posts = _repo.StatementToList<Post>(result);
            return posts;
        }
    }
}
