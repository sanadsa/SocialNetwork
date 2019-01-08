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
        void AddUser(string userName, string email, string token, List<string> followersIds, List<string> following, List<string> blockedIds);
        void FollowUser(string userName, string userToFollow);
        void BlockUser(string userName, string userToBlock);
        User GetUser(string userName);
        IEnumerable<User> GetBlockedUsers(string userName);
        void UnblockUser(string userName, string blockedUser);
        IEnumerable<User> GetFollowing(string userName);
        IEnumerable<User> GetFollowers(string userName);
    }
}
