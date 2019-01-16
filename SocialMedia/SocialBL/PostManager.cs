using Social.Common.Enums;
using Social.Common.Interfaces;
using Social.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialBL
{
    /// <summary>
    /// class that manage the post actions (post, like, comment, privacy)
    /// </summary>
    public class PostManager : IPostManager
    {
        AmazonS3Uploader _s3Uploader;
        private readonly IPostRepository _postRepo;
        public PostManager(IPostRepository manager)
        {
            _postRepo = manager;
            _s3Uploader = new AmazonS3Uploader();
        }

        /// <summary>
        /// add post to neo4j db and relate it to the user
        /// </summary>
        /// <param name="post"></param>
        public void AddPost(Post post)
        {
            string guid = Guid.NewGuid().ToString();
            string imageUrl;
            try
            {
                if (post.ImageUrl == null)
                {
                    imageUrl = _s3Uploader.UploadFile(post.ImageUrl, guid);
                }

                _postRepo.AddPost(post.UserId, post);

            }
            catch (Exception)
            {
                // log
                
            }
        }

        /// <summary>
        /// change the privacy of an existing post - who can see the post (all, followers, non)
        /// </summary>
        /// <param name="id"></param>
        /// <param name="privacy"></param>
        public void ChangePostPrivacy(int id, ePostPrivacy privacy)
        {
            _postRepo.ChangePostPrivacy(id, privacy);
        }

        /// <summary>
        /// add comment to neo4j db and relate it to post - calls the repository to edit the db
        /// </summary>
        public void CommentPost(int postId, Comment comment)
        {
            _postRepo.CommentPost(postId, comment);

        }

        /// <summary>
        /// add like relation from user to post - calls the repository to edit the db
        /// </summary>
        public void LikePost(int userId, int postId)
        {
            _postRepo.LikePost(userId, postId);
        }
    }
}
