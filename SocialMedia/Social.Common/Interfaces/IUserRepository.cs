using Social.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Social.Common.Interfaces
{
    public interface IUserRepository
    {
        void AddUser(User user);
        void DeleteUser(int userId);
        void Follow(string activeUserId, string userToFollow);
        void UnFollow(string activeUserId, string userToUnFollow);
        void Block(string activeUserId, string userToBlock);
        void UnBlock(string activeUserId, string userToUnBlock);
        User GetUser(int userId);
        IEnumerable<User> GetUsers(string username);
        IEnumerable<User> GetBlockedUsers(string userEmail);
        IEnumerable<User> GetFollowing(string userEmail);
        IEnumerable<User> GetFollowers(string userEmail);
    }
}
