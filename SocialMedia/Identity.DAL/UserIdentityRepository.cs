using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DocumentModel;
using Identity.Common.DynamoDB;
using Identity.Common.Interfaces;
using Identity.Common.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Identity.DAL
{
    public class UserIdentityRepository : IIdentityRepository
    {
        private readonly DynamoService _dynamoService;

        public UserIdentityRepository()
        {
            _dynamoService = new DynamoService();
        }

        /// <summary>
        ///  AddUserIdentity will accept a UserIdentity object and creates an Item on Amazon DynamoDB
        /// </summary>
        /// <param name="user"></param>
        public void AddUserIdentity(UserIdentity user)
        {
            //if (SearchUserIdentities(user.Email, user.FullName).SingleOrDefault() == null)
            //{
            //    throw new AmazonDynamoDBException("Email already exist");
            //}
            var email = GetUserIdentity(user.Email);
            if (email != null)
            {
                throw new AmazonDynamoDBException("Email already exist");
            }
            _dynamoService.Store(user);
        }

        /// <summary>
        /// Check the user existency
        /// </summary>
        public bool CheckUserExistency(string email)
        {
            return _dynamoService.CheckUserExistency<UserIdentity>(email);
        }

        /// <summary>
        /// ModifyUserIdentity  tries to load an existing UserIdentity, modifies and saves it back. If the Item doesn’t exist, it raises an exception
        /// </summary>
        /// <param name="user"></param>
        public void ModifyUserIdentity(UserIdentity user)
        {
            _dynamoService.UpdateItem(user);
        }

        /// <summary>
        /// GetAllUserIdentities will perform a Table Scan operation to return all the UserIdentities
        /// </summary>
        public IEnumerable<UserIdentity> GetAllUserIdentities()
        {
            return _dynamoService.GetAll<UserIdentity>();
        }

        /// <summary>
        /// Search For User Identity
        /// </summary>
        /// <param name="email"></param>
        /// <param name="age"></param>
        public IEnumerable<UserIdentity> SearchUserIdentities(string email)
        {
            IEnumerable<UserIdentity> filteredUserIdentities = _dynamoService.DbContext.Query<UserIdentity>(email, QueryOperator.Equal);

            return filteredUserIdentities;
        }

        /// <summary>
        /// search for item in dynamodb, return null if didnt find
        /// </summary>
        public UserIdentity GetUserIdentity(string email)
        {
            return _dynamoService.GetItem<UserIdentity>(email);
        }

        /// <summary>
        /// Delete UserIdentity will remove an item from DynamoDb
        /// </summary>
        /// <param name="user"></param>
        public void DeleteUserIdentity(UserIdentity user)
        {
            _dynamoService.DeleteItem(user);
        }
    }
}
