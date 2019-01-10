using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Social.Common.Models;
using Social.DAL;

namespace Social.DAL_Test
{
    [TestClass]
    public class UserRepositoryTest
    {
        UserRepository u = new UserRepository();

        [TestMethod]
        public void AddNewUser()
        {
            User user = new User
            {
                UserId = 4,
                Username = "Jaames",
                BlockedIds = new List<string>(),
                Email = "safir@gmail.com",
                FollowersIds = new List<string>(),
                Following = new List<string>(),
                Token = "tt"
            };
            u.AddUser(user);
        }

        [TestMethod]
        public void Follow()
        {
            u.Follow(3, 1);
        }

        [TestMethod]
        public void Block()
        {
            u.Block(3, 1);
        }

        [TestMethod]
        public void UnFollow()
        {
            u.UnFollow(3, 1);
        }

        [TestMethod]
        public void UnBlock()
        {
            u.UnBlock(3, 1);
        }

        [TestMethod]
        public void Delete()
        {
            u.DeleteUser(1);
        }

        [TestMethod]
        public void Get()
        {
            var user = u.GetUser(1);
            System.Console.WriteLine(user.Username);
        }        
    }
}
