using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DataModel;
using Common.Exceptions;
using Common.Interfaces;
using Common.Models;
using Dal.TokenRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dal.UserRepositories
{
    public class UserRepository : IUserRepository
    {
        private readonly DynamoDBContextConfig _contextConfig;
        private readonly TokenRipository _tokenRipository;

        public UserRepository()
        {
            _contextConfig = new DynamoDBContextConfig
            {
                ConsistentRead = true,
                Conversion = DynamoDBEntryConversion.V2
            };
            _tokenRipository = new TokenRipository();
        }

        /// <summary>
        /// Add the AuthenticationUser after checking the user validatioon with class validation
        /// </summary>
        /// <param name="user"> The Added User </param>
        public void AddUserToDatabase(AuthenticationUser user)
        {
            using (var context = new DynamoDBContext(_contextConfig))
            {
                try
                {
                    if (!CheckIfUserExist(user).Result)
                        context.Save(user);
                }
                catch (Exception ex)
                {
                    throw new SaveToDatabaseException("A problrm during the save to context operation", ex.InnerException);
                }
            }
        }

        /// <summary>
        /// Checks whether a user exist in the database or not
        /// </summary>
        /// <param name="user"> the wanted user </param>
        /// <returns> returns true if exist, otherwise false </returns>
        public async Task<bool> CheckIfUserExist(AuthenticationUser user)
        {
            using (var context = new DynamoDBContext(_contextConfig))
            {
                try
                {
                    var userCheck = await context.LoadAsync<AuthenticationUser>(user.Username);
                    return userCheck != null ? true : false;
                }
                catch (Exception ex)
                {
                    throw new GetFromDatabaseException("A problrm during the Load from context operation", ex.InnerException);
                }
            }
        }

        /// <summary>
        /// Login methods gets email and password from the ui and ganerate a new one and save to the database.
        /// </summary>
        /// <param name="email"> email from ui </param>
        /// <param name="password"> password from ui </param>
        /// <returns> full user </returns>
        public async Task<User> Login(string username, string password)
        {
            using (var context = new DynamoDBContext(_contextConfig))
            {
                var userCheck = await context.LoadAsync<AuthenticationUser>(username);
                if (userCheck != null)
                    if (userCheck.Password == password)
                    {
                        Token token = _tokenRipository.AddNewToken(userCheck);
                        return new User()
                        {
                            Email = userCheck.Email,
                            IsAvailable = true,
                            Password = userCheck.Password,
                            Token = token,
                            Username = userCheck.Username
                        };
                    }
                    else return null;
                else return null;
            }
        }

        /// <summary>
        /// Login via facebook method gets a facebook token and ganerate a new one and finally save it to the database.
        /// </summary>
        /// <param name="facebookToken"> This the actual token that i get from the facebook login proccess </param>
        /// <param name="email"> This is the facebook user email that i get from facebook </param>
        /// <param name="username"> This is the facebook user username that i get from facebook </param>
        /// <returns> returns the user </returns>
        public User LoginViaFacebook(string facebookToken, FacebookUser facebookUser)
        {
            using (var context = new DynamoDBContext(_contextConfig))
            {
                if (!CheckIfFacebookUserExist(facebookUser).Result)
                    context.Save(facebookUser);
                Token token = _tokenRipository.AddNewFacebookUserToken(facebookUser);
                return new User()
                {
                    Email = facebookUser.Email,
                    IsAvailable = true,
                    Token = token,
                    Username = facebookUser.Username
                };
            }
        }

        /// <summary>
        /// Checks whether a user exist in the database or not
        /// </summary>
        /// <param name="user"> the wanted user </param>
        /// <returns> returns true if exist, otherwise false </returns>
        private async Task<bool> CheckIfFacebookUserExist(FacebookUser facebookUser)
        {
            using (var context = new DynamoDBContext(_contextConfig))
            {
                try
                {
                    var userCheck = await context.LoadAsync<FacebookUser>(facebookUser.UserFacebookId);
                    return userCheck.UserFacebookId == facebookUser.UserFacebookId ? true : false;
                }
                catch (Exception ex)
                {
                    throw new GetFromDatabaseException("A problrm during the Load from context operation", ex.InnerException);
                }
            }
        }
    }
}
