using Neo4j.Driver.V1;
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
            //var query = $@"CREATE (u:User {{UserId: {user.UserId}}}) " +
            //            "ON CREATE u " +
            //            $"SET u = {json}";
            var query = $@"CREATE (u:User {json})";
            _repo.RunQuery(driver, query);
        }

        /// <summary>
        /// block user
        /// </summary>
        public void Block(int activeUserId, int userToBlock)
        {
            UnFollow(activeUserId, userToBlock);
            string query = "MATCH (blocking:User{UserId:" + activeUserId + "})," +
                "(blocked:User{UserId:" + userToBlock + "})" +
                "CREATE (blocking)-[r:Blocked]->(blocked)" +
                "RETURN type(r)";
            _repo.RunQuery(driver, query);
        }

        /// <summary>
        /// delete existing user from neo4j
        /// </summary>
        public void DeleteUser(int userId)
        {
            var query = $@"MATCH (user:User)
                           WHERE user.UserId = {userId}
                           DELETE user";
            _repo.RunQuery(driver, query);
        }

        /// <summary>
        /// follow user 
        /// </summary>
        public void Follow(int activeUserId, int userToFollow)
        {
            var query = "MATCH (following:User{UserId:" + activeUserId + "})," +
                "(followed:User{UserId:" + userToFollow + "})" +
                "CREATE (following)-[r:Following]->(followed)" +
                "RETURN type(r)";
            _repo.RunQuery(driver, query);

        }

        public IEnumerable<User> GetBlockedUsers(User user)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<User> GetFollowers(User user)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<User> GetFollowing(User user)
        {
            throw new NotImplementedException();
        }

        public User GetUser(string userName)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// unblock user
        /// </summary>
        public void UnBlock(int activeUserId, int userToUnBlock)
        {
            UnFollow(activeUserId, userToUnBlock);
            string query = "MATCH (:User{UserId:" + activeUserId + "})-[r:Blocked]->" +
                "(:User{UserId:" + userToUnBlock + "})" +
                "DELETE r";
            _repo.RunQuery(driver, query);
        }

        /// <summary>
        /// unfollow user
        /// </summary>
        public void UnFollow(int activeUserId, int userToUnFollow)
        {
            var query = "MATCH (:User{UserId:" + activeUserId + "})-[r:Following]->" +
                "(:User{UserId:" + userToUnFollow + "})" +
                "DELETE r";
            _repo.RunQuery(driver, query);
        }
    }
}
