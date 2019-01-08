using Social.Common.Enums;
using Social.Common.Interfaces;
using Social.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Social.BL
{
    /// <summary>
    /// class that manage the post actions (post, like, comment, privacy)
    /// </summary>
    public class PostManager : IPostManager
    {
        private readonly IPostRepository _postManager;
        public PostManager(IPostRepository manager)
        {
            _postManager = manager;
        }

        /// <summary>
        /// add post to neo4j db and relate it to the user
        /// </summary>
        /// <param name="post"></param>
        public void AddPost(int userId, Post post)
        {
            try
            {
                _postManager.AddPost(userId, post);
            }
            catch (Exception)
            {

                throw;
            }
        }

        /// <summary>
        /// change the privacy of an existing post - who can see the post (all, followers, non)
        /// </summary>
        /// <param name="id"></param>
        /// <param name="privacy"></param>
        public void ChangePostPrivacy(int id, ePostPrivacy privacy)
        {
            try
            {
                _postManager.ChangePostPrivacy(id, privacy);
            }
            catch (Exception)
            {

                throw;
            }
        }

        /// <summary>
        /// add comment to neo4j db and relate it to post - calls the repository to edit the db
        /// </summary>
        public void CommentPost(int postId, Comment comment)
        {
            try
            {
                _postManager.CommentPost(postId, comment);
            }
            catch (Exception)
            {

                throw;
            }
        }

        /// <summary>
        /// increase like number by 1 to an existing post in the db - calls the repository to edit the db
        /// </summary>
        public void LikePost(int id)
        {
            try
            {
                _postManager.LikePost(id);
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
