using Neo4j.Driver.V1;
using Newtonsoft.Json;
using Social.Common.Interfaces;
using Social.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Social.DAL
{
    public class UserRepository : IUserRepository
    {
        static IDriver driver;
        private Repository _repo = new Repository();

        /// <summary>
        /// ctor - init the neo4j credentials
        /// </summary>
        public UserRepository() => driver = GraphDatabase.Driver("bolt://localhost:7687", AuthTokens.Basic("neo4j", "password"));

        /// <summary>
        /// add user as a node to neo4j db
        /// </summary>
        public void AddUser(User user)
        {
            var json = _repo.ObjectToJson(user);
            //var query = "CREATE (u:User{UserId:"+ user.UserId +"})" +
            //            "ON CREATE u " +
            //            "SET u = "+json+"";          
            var query = $@"CREATE (u:User {json})";
            _repo.RunQuery(driver, query);
        }

        /// <summary>
        /// block user
        /// </summary>
        public void Block(string activeEmail, string userToBlock)
        {
            UnFollow(activeEmail, userToBlock);
            UnFollow(userToBlock, activeEmail);
            string query = "MATCH (blocking:User{Email:\"" + activeEmail + "\"})," +
                "(blocked:User{Email:\"" + userToBlock + "\"})" +
                "CREATE (blocking)-[r:Blocked]->(blocked)" +
                "RETURN type(r)";
            _repo.RunQuery(driver, query);
        }

        /// <summary>
        /// delete existing user from neo4j
        /// </summary>
        public void DeleteUser(int userId)
        {
            //var query = $@"MATCH (user:User)
            //               WHERE user.UserId = {userId}
            //               DELETE user";
            var query = $@"OPTIONAL MATCH (user:User)<-[r]->()
                           WHERE user.UserId = {userId}
                           DELETE r, user";
            _repo.RunQuery(driver, query);
        }

        /// <summary>
        /// follow user 
        /// </summary>
        public void Follow(string activeUserEmail, string userToFollow)
        {
            var query = "MATCH (following:User{Email:\"" + activeUserEmail + "\"})," +
                "(followed:User{Email:\"" + userToFollow + "\"})" +
                "CREATE UNIQUE (following)-[r:Following]->(followed)" +
                "RETURN type(r)";
            
            _repo.RunQuery(driver, query).Consume();
        }

        /// <summary>
        /// get blocked users
        /// </summary>
        /// <param name="userEmail">user to check his blocked list</param>
        public IEnumerable<User> GetBlockedUsers(string userEmail)
        {
            string query = $"MATCH (u:User)-[:Blocked]->(bu:User)" +
                           $"WHERE u.Email = \"{userEmail}\" " +
                           $"RETURN bu";
            var result = _repo.RunQuery(driver, query);
            var blocked = new List<User>();
            foreach (var t in result)
            {
                var props = JsonConvert.SerializeObject(t[0].As<INode>().Properties);
                blocked.Add(JsonConvert.DeserializeObject<User>(props));
            }
            return blocked;
        }

        /// <summary>
        /// get followers of the user
        /// </summary>
        public IEnumerable<User> GetFollowers(string userEmail)
        {
            string query = $"MATCH (u:User)<-[:Following]-(fu:User)" +
                           $"WHERE u.Email = \"{userEmail}\" " +
                           $"RETURN fu";
            var result = _repo.RunQuery(driver, query);
            var followers = new List<User>();
            foreach (var t in result)
            {
                var props = JsonConvert.SerializeObject(t[0].As<INode>().Properties);
                followers.Add(JsonConvert.DeserializeObject<User>(props));
            }
            return followers;
        }

        /// <summary>
        /// get users the the user follow (following)
        /// </summary>
        public IEnumerable<User> GetFollowing(string userEmail)
        {
            string query = $"MATCH (u:User)-[:Following]->(fu:User)" +
                           $"WHERE u.Email = \"{userEmail}\" " +
                           $"RETURN fu";
            var result = _repo.RunQuery(driver, query);
            var following = new List<User>();
            foreach (var t in result)
            {
                var props = JsonConvert.SerializeObject(t[0].As<INode>().Properties);
                following.Add(JsonConvert.DeserializeObject<User>(props));
            }
            return following;
        }

        /// <summary>
        /// get user from neo4j
        /// assuming that the userId is unique
        /// </summary>
        public User GetUser(int userId)
        {
            var query = $"MATCH (u:User)"+
                        $"WHERE u.Username = {userId}"+
                        $"RETURN u";
            var result = _repo.RunQuery(driver, query);
            var user = new User();
            foreach (var item in result)
            {
                var props = JsonConvert.SerializeObject(item[0].As<INode>().Properties);
                user = JsonConvert.DeserializeObject<User>(props);
            }
            return user;
        }

        /// <summary>
        /// get users from neo4j
        /// assuming that the userId is unique
        /// </summary>
        public IEnumerable<User> GetUsers(string username)
        {
            var query = $"MATCH (u:User)" +
                        $"WHERE u.Username = \"{username}\" " +
                        $"RETURN u";
            var result = _repo.RunQuery(driver, query);
            var users = new List<User>();
            foreach (var item in result)
            {
                var props = JsonConvert.SerializeObject(item[0].As<INode>().Properties);
                users.Add(JsonConvert.DeserializeObject<User>(props));
            }
            return users;
        }

        /// <summary>
        /// unblock user
        /// </summary>
        public void UnBlock(string activeUserEmail, string userToUnBlock)
        {
            UnFollow(activeUserEmail, userToUnBlock);
            string query = "MATCH (:User{Email:\"" + activeUserEmail + "\"})-[r:Blocked]->" +
                "(:User{Email:\"" + userToUnBlock + "\"})" +
                "DELETE r";
            _repo.RunQuery(driver, query);
        }

        /// <summary>
        /// unfollow user
        /// </summary>
        public void UnFollow(string activeUserEmail, string userToUnFollow)
        {
            var query = "MATCH (:User{Email:\"" + activeUserEmail + "\"})-[r:Following]->" +
                "(:User{Email:\"" + userToUnFollow + "\"})" +
                "DELETE r";
            _repo.RunQuery(driver, query);
        }
    }
}
