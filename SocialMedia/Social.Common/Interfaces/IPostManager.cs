using Social.Common.Enums;
using Social.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Social.Common.Interfaces
{
    public interface IPostManager
    {
        Post AddPost(string post);
        void LikePost(string likeJson);
        void Comment(string commentJson);
        void ChangePostPrivacy(int postId, EpostPrivacy privacy);
        IEnumerable<IncomeComment> GetComments(string postId);
    }
}
