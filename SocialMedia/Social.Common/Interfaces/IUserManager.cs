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
        void FollowUser(string email, string emailToFollow);
        void UnFollow(string email, string emailToUnFollow);
        void BlockUser(string email, string emailToBlock);
        void UnBlock(string email, string emailToUnBlock);
        User GetUser(int userId);
        IEnumerable<User> GetBlockedUsers(string userEmail);
        IEnumerable<User> GetFollowing(string userEmail);
        IEnumerable<User> GetFollowers(string userEmail);
    }
}
