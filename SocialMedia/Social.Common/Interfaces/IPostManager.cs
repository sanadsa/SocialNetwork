﻿using Social.Common.Enums;
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
        void AddPost(int userId, Post post);
        void LikePost(int id);
        void CommentPost(int postId, Comment comment);
        void ChangePostPrivacy(int id, ePostPrivacy privacy);
    }
}
