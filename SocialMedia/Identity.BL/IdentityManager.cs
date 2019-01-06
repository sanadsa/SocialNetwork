using Identity.Common.Interfaces;
using Identity.Common.Model;
using Identity.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Identity.BL
{
    public class IdentityManager : IIdentityManager
    {
        //private readonly IIdentityRepository userLibrary;
        //public IdentityManager(IIdentityRepository identityRepository)
        //{
        //    userLibrary = identityRepository;
        //}
        private UserIdentityRepository userLibrary = new UserIdentityRepository();

        /// <summary>
        /// Add user to dynamoDb - call dal
        /// </summary>
        public void AddUser(UserIdentity user)
        {
            userLibrary.AddUserIdentity(user);
        }

        /// <summary>
        /// delete user from db - calling dal
        /// </summary>
        public void DeleteUser(UserIdentity identity)
        {
            userLibrary.DeleteUserIdentity(identity);
        }

        /// <summary>
        /// returns user by the name
        /// </summary>
        public UserIdentity GetUser(string email)
        {
            return userLibrary.GetUserIdentity(email);
        }

        /// <summary>
        /// update existing user
        /// </summary>
        public void UpdateUser(UserIdentity identity)
        {
            userLibrary.ModifyUserIdentity(identity);
        }
    }
}
