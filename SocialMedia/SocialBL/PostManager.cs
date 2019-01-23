using Newtonsoft.Json;
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
        public Post AddPost(string postJson)
        {
            var post = JsonConvert.DeserializeObject<IncomePost>(postJson);
            string guid = Guid.NewGuid().ToString();
            string imageUrl;
            try
            {
                Post newPost = new Post()
                {
                    ImageUrl = null,
                    PostId = post.PostId,
                    Privacy = post.Privacy,
                    UserEmail = post.UserEmail,
                    Tags = post.Tags,
                    Text = post.Text,
                };
                if (post.Image != null)
                {
                    imageUrl = _s3Uploader.UploadFile(post.Image, guid);
                    newPost.ImageUrl = imageUrl;
                }
                _postRepo.AddPost(post.UserEmail, newPost);
                return newPost;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// comment on post
        /// </summary>
        public void Comment(string commentJson)
        {
            var comment = JsonConvert.DeserializeObject<IncomeComment>(commentJson);
            Comment c = new Comment
            {
                CommentId = comment.CommentId,
                Image = null,
                Tags = new List<string>(),
                Text = comment.CommentValue
            };

            _postRepo.CommentPost(comment.UserId, comment.PostId, comment);
        }

        /// <summary>
        /// change the privacy of an existing post - who can see the post (all, followers, non)
        /// </summary>
        /// <param name="id"></param>
        /// <param name="privacy"></param>
        public void ChangePostPrivacy(int id, EpostPrivacy privacy)
        {
            _postRepo.ChangePostPrivacy(id, privacy);
        }

        /// <summary>
        /// add like relation from user to post - calls the repository to edit the db
        /// </summary>
        public void LikePost(string likeJson)
        {
            var like = JsonConvert.DeserializeObject<IncomeLike>(likeJson);
            _postRepo.LikePost(like.UserEmail, like.PostId);
        }

        public IEnumerable<IncomeComment> GetComments(string postId)
        {
            return _postRepo.GetComments(postId);
        }
    }
}
