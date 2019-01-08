using Social.Common.Interfaces;
using Social.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Social.BL
{
    public class UserManager : IUserManager
    {
        public void AddUser(string userName, string email, string token, List<string> followersIds, List<string> following, List<string> blockedIds)
        {
            throw new NotImplementedException();
        }

        public void BlockUser(string userName, string userToBlock)
        {
            throw new NotImplementedException();
        }

        public void FollowUser(string userName, string userToFollow)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<User> GetBlockedUsers(string userName)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<User> GetFollowers(string userName)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<User> GetFollowing(string userName)
        {
            throw new NotImplementedException();
        }

        public User GetUser(string userName)
        {
            throw new NotImplementedException();
        }

        public void UnblockUser(string userName, string blockedUser)
        {
            throw new NotImplementedException();
        }
    }
}
