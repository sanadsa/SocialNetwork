using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DataModel;
using Common.Exceptions;
using Common.Interfaces;
using Common.Models;
using ServiceStack.Aws.DynamoDb;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dal.TokenRepositories
{
    public class TokenRipository : ITokenRepository
    {
        private readonly DynamoDBContextConfig _contextConfig;
        private readonly AmazonDynamoDBClient _dbclient;
        private readonly AmazonDynamoDBConfig _dbConfig;

        public TokenRipository()
        {
            _contextConfig = new DynamoDBContextConfig
            {
                ConsistentRead = true,
                Conversion = DynamoDBEntryConversion.V2
            };
            _dbConfig = new AmazonDynamoDBConfig();
            _dbclient = new AmazonDynamoDBClient(_dbConfig);
        }

        /// <summary>
        /// Get user from the login proccess and ganerate new token and finally save to database,
        /// </summary>
        /// <param name="user"> the user from the login </param>
        /// <returns> return the token </returns>
        public Token AddNewToken(AuthenticationUser user)
        {
            using (var context = new DynamoDBContext(_contextConfig))
            {
                Token token = new Token() { CreatedTime = DateTime.Now.ToString(),
                                            IsValid = true,
                                            TokenId = TokenGenerator(),
                                            Username = user.Username };
                try
                {
                    context.Save(token);
                    return token;
                }
                catch (Exception ex)
                {
                    throw new SaveToDatabaseException("A problrm during the save to context operation", ex.InnerException);
                }
            }
        }

        /// <summary>
        /// Gets a facebook user checks the validation of the user and ganerate a new one.
        /// </summary>
        /// <param name="user"> the facebook user </param>
        /// <returns> the new token </returns>
        public Token AddNewFacebookUserToken(FacebookUser user)
        {
            using (var context = new DynamoDBContext(_contextConfig))
            {
                Token token = new Token() { CreatedTime = DateTime.Now.ToString(), IsValid = true, TokenId = TokenGenerator(), Username = user.UserFacebookId };
                try
                {
                    context.Save(token);
                    return token;
                }
                catch (Exception ex)
                {
                    throw new SaveToDatabaseException("A problrm during the save to context operation", ex.InnerException);
                }
            }
        }

        /// <summary>
        /// Because after 15 minutes we need to ganerage new token.
        /// this method will get the user who need to update for new token and ganerate new one
        /// </summary>
        /// <param name="user"> The wanted user </param>
        /// <returns> the new token </returns>
        public Token ChangeUserToken(User user)
        {
            using (var context = new DynamoDBContext(_contextConfig))
            {
                var token = context.Load<Token>(user.Token.Username, user.Token.CreatedTime);
                if (token == null)
                    throw new UserNotFoundException("Not found the wanted user");
                else
                {
                    token.IsValid = false;
                    context.Delete(user.Token);
                    context.Save(token);
                }
                Token newToken = new Token() { CreatedTime = DateTime.Now.ToString(), Username = user.Username, IsValid = true, TokenId = TokenGenerator() };
                context.Save(newToken);
                return newToken;
            }
        }

        /// <summary>
        /// Ganerate new token by Guid c# class
        /// </summary>
        /// <returns> new token </returns>
        private string TokenGenerator()
        {
            return Convert.ToBase64String(Guid.NewGuid().ToByteArray());
        }
    }
}
