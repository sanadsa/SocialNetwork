using Social.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Social.Common.Interfaces
{
    public interface IUserManager
    {
        void AddUser(User user);
        void DeleteUser(int userId);
        void FollowUser(int userId, int userToFollow);
        void UnFollow(int userId, int userToUnFollow);
        void BlockUser(int userId, int userToBlock);
        void UnBlock(int userId, int userToUnBlock);
        User GetUser(int userId);
        IEnumerable<User> GetBlockedUsers(string userEmail);
        IEnumerable<User> GetFollowing(string userEmail);
        IEnumerable<User> GetFollowers(string userEmail);
    }
}
