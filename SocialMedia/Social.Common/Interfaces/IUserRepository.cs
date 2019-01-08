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
        void FollowUser(User user, User userToFollow);
        void BlockUser(User user, User userToBlock);
        User GetUser(string userName);
        IEnumerable<User> GetBlockedUsers(User user);
        void UnblockUser(User user, User blockedUser);
        IEnumerable<User> GetFollowing(User user);
        IEnumerable<User> GetFollowers(User user);
    }
}
