using Neo4j.Driver.V1;
using Social.Common.Enums;
using Social.Common.Interfaces;
using Social.Common.Models;
using System;

namespace Social.DAL
{
    public class PostRepository : IPostRepository
    {
        static IDriver driver;
        private Repository _repo = new Repository();
        // 🚏 -> 🚍 -> 🚏
        public PostRepository()
        {
            driver = GraphDatabase.Driver("bolt://localhost:7687", AuthTokens.Basic("neo4j", "password"));
        }

        /// <summary>
        /// add post to neo4j and relate it to an existing user
        /// </summary>
        public void AddPost(int userId, Post post)
        {
            var json = _repo.ObjectToJson(post);
            var query = $"CREATE (p:Post {json})";

            _repo.RunQuery(driver, query);
            RelatePostToUser(userId, post.PostId);
        }

        /// <summary>
        /// relate post to an existing user
        /// </summary>
        public void RelatePostToUser(int userId, int postId)
        {
            var query = "MATCH (u:User{UserId:" + userId + "})," +
                "(p:Post{PostId:" + postId + "})" +
                "CREATE (u)-[r:Posted]->(p)" +
                "RETURN type(r)";
            _repo.RunQuery(driver, query);
        }

        /// <summary>
        /// change post privacy
        /// </summary>
        public void ChangePostPrivacy(int postId, ePostPrivacy privacy)
        {
            throw new NotImplementedException();
        }

        public void CommentPost(int postId, Comment comment)
        {
            var query = "MATCH (p:Post)" +
                        $"WHERE p.PostId = {postId}" +
                        $"SET p.Comments = {comment}";
            _repo.RunQuery(driver, query);
        }

        public void LikePost(int postId)
        {
            throw new NotImplementedException();
        }
    }
}
