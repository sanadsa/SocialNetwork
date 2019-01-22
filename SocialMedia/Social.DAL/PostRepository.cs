using Neo4j.Driver.V1;
using Social.Common.Enums;
using Social.Common.Interfaces;
using Social.Common.Models;
using System;

namespace Social.DAL
{
    /// <summary>
    /// class for post actions in neo4j db
    /// </summary>
    public class PostRepository : IPostRepository
    {
        static IDriver driver;
        private Repository _repo = new Repository();
        // 🚏 -> 🚍 -> 🚏
        public PostRepository() => driver = GraphDatabase.Driver("bolt://ec2-18-221-146-154.us-east-2.compute.amazonaws.com:7687", AuthTokens.Basic("neo4j", "iTa1995Da"));

        /// <summary>
        /// add post to neo4j and relate it to an existing user
        /// </summary>
        public void AddPost(string userEmail, Post post)
        {
            var json = _repo.ObjectToJson(post);
            var query = $"CREATE (p:Post {json})";
            _repo.RunQuery(driver, query);
            RelatePostToUser(userEmail, post.PostId);
        }

        /// <summary>
        /// relate post to an existing user
        /// </summary>
        public void RelatePostToUser(string userEmail, string postId)
        {
            var query = "MATCH (u:User{Email:\"" + userEmail + "\"})," +
                "(p:Post{PostId:\"" + postId + "\"})" +
                "CREATE (u)-[r:Posted]->(p)" +
                "RETURN type(r)";
            _repo.RunQuery(driver, query);
        }

        /// <summary>
        /// change post privacy
        /// </summary>
        public void ChangePostPrivacy(int postId, EpostPrivacy privacy)
        {
            var privacyEnumToInt = (int) privacy;
            var privacyQuery = "MATCH (p:Post{PostId:"+postId+"})"+
                               "SET p.Privacy = "+privacyEnumToInt+"";
            _repo.RunQuery(driver, privacyQuery);
        }

        /// <summary>
        /// create a comment in neo4j and relate it to an existing post
        /// </summary>
        public void CommentPost(int postId, Comment comment)
        {
            var json = _repo.ObjectToJson(comment);
            var query = $"CREATE (c:Comment {json})";

            _repo.RunQuery(driver, query);
            RelateCommentToPost(postId, comment.CommentId);
        }

        /// <summary>
        /// relate a comment to an existing post in neo4j
        /// </summary>
        public void RelateCommentToPost(int postId, int commentId)
        {
            var query = "MATCH (p:Post{PostId:" + postId + "})," +
                "(c:Comment{CommentId:" + commentId + "})" +
                "CREATE (c)-[r:CommentOn]->(p)" +
                "RETURN type(r)";
            _repo.RunQuery(driver, query);
        }

        /// <summary>
        /// user like a post, only if he didnt liked it already
        /// creates a relate between user and a post in neo4j
        /// </summary>
        public void LikePost(int userId, int postId)
        {
            var query = "MATCH (u:User{UserId:" + userId + "})," +
                "(p:Post{PostId:" + postId + "})" +
                "CREATE UNIQUE (u)-[r:Liked]->(p)" +
                "RETURN type(r)";
            _repo.RunQuery(driver, query);
        }        

        /// <summary>
        /// get likes of a post
        /// </summary>
        public int GetNumberOfLikes(int postId)
        {
            var query = $"MATCH (u:User)-[:Liked]->(p:Post) " +
                           $"WHERE p.PostId={postId} " +
                           $"RETURN u";
            var result = _repo.RunQuery(driver, query);
            var likes = _repo.StatementToList<User>(result);
            return likes.Count;
        }
    }
}
