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
        void AddPost(string email, Post post);
        void LikePost(string userId, string postId);
        int GetNumberOfLikes(int postId);
        IEnumerable<IncomeComment> GetComments(string postId);
        void CommentPost(string userId, string postId, IncomeComment comment);  
        void ChangePostPrivacy(int postId, EpostPrivacy privacy);
        void RelatePostToUser(string userEmail, string postId);
        void RelateCommentToPost(string postId, string commentId);
    }
}
