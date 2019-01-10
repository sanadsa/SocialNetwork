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
        void Follow(int activeUserId, int userToFollow);
        void UnFollow(int activeUserId, int userToUnFollow);
        void Block(int activeUserId, int userToBlock);
        void UnBlock(int activeUserId, int userToUnBlock);
        User GetUser(int userId);
        IEnumerable<User> GetBlockedUsers(User user);
        IEnumerable<User> GetFollowing(User user);
        IEnumerable<User> GetFollowers(User user);
    }
}
