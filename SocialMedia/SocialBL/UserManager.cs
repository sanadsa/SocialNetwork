using Social.Common.Interfaces;
using Social.Common.Models;
using Social.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialBL
{
    public class UserManager : IUserManager
    {
        //private readonly IUserRepository _userRepo;
        ///// <summary>
        ///// ctor init the interface - implemented by a class in simple injector in app.config
        ///// </summary>
        //public UserManager(IUserRepository repository)
        //{
        //    _userRepo = repository;
        //}

        private UserRepository _userRepo = new UserRepository();

        /// <summary>
        /// add user bl - calls the repo
        /// </summary>
        public void AddUser(User user)
        {
            _userRepo.AddUser(user);
        }

        /// <summary>
        /// block user bl - calls the repo
        /// </summary>
        public void BlockUser(string email, string emailToBlock)
        {
            _userRepo.Block(email, emailToBlock);
        }

        /// <summary>
        /// delete user bl - calls the repo
        /// </summary>
        public void DeleteUser(int userId)
        {
            _userRepo.DeleteUser(userId);
        }

        /// <summary>
        /// follow user bl - calls the repo
        /// </summary>
        public void FollowUser(string email, string emailToFollow)
        {
            _userRepo.Follow(email, emailToFollow);
        }

        /// <summary>
        /// get blocked users bl - calls the repo
        /// </summary>
        public IEnumerable<User> GetBlockedUsers(string userToken)
        {
            return _userRepo.GetBlockedUsers(userToken);
        }

        /// <summary>
        /// get followers bl - calls the repo
        /// </summary>
        public IEnumerable<User> GetFollowers(string userToken)
        {
            return _userRepo.GetFollowers(userToken);
        }

        /// <summary>
        /// get following bl - calls the repo
        /// </summary>
        public IEnumerable<User> GetFollowing(string userToken)
        {
            return _userRepo.GetFollowing(userToken);
        }

        /// <summary>
        /// get user bl - calls the repo
        /// </summary>
        public User GetUser(int userId)
        {
            return _userRepo.GetUser(userId);
        }

        /// <summary>
        /// unblock user bl - calls the repo
        /// </summary>
        public void UnBlock(string email, string emailToUnBlock)
        {
            _userRepo.UnBlock(email, emailToUnBlock);
        }

        /// <summary>
        /// unfollow user bl - calls the repo
        /// </summary>
        public void UnFollow(string email, string emailToUnFollow)
        {
            _userRepo.UnFollow(email, emailToUnFollow);
        }
    }
}
