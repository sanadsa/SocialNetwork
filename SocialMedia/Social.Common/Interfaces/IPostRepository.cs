using Social.Common.Enums;
using Social.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Social.Common.Interfaces
{
    public interface IPostRepository
    {
        void AddPost(int userId, Post post);
        void LikePost(int userId, int postId);
        int GetNumberOfLikes(int postId);
        void CommentPost(int postId, Comment comment);
        void ChangePostPrivacy(int postId, ePostPrivacy privacy);
        void RelatePostToUser(int userId, int postId);
        void RelateCommentToPost(int postId, int commentId);
    }
}
