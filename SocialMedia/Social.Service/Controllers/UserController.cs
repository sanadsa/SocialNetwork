using Social.Common.Interfaces;
using Neo4j.Driver.V1;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using System.Net;
using System.Net.Http;
using Social.Common.Models;
using Newtonsoft.Json;

namespace Social.Service.Controllers
{
    [RoutePrefix("api/User")]
    public class UserController : ApiController
    {
        private readonly IUserManager _userBl;

        public UserController(IUserManager manager)
        {
            _userBl = manager;
        }

        /// <summary>
        /// Add user to neo4j db using http call from the client
        /// </summary>
        [HttpPost]
        [Route("CreateUser")]
        public HttpResponseMessage CreateUser([FromBody]string userJson)
        {
            try
            {
                var user = JsonConvert.DeserializeObject<User>(userJson);
                _userBl.AddUser(user);

                return Request.CreateResponse(HttpStatusCode.OK, "User added successfully");
            }
            catch (Neo4jException e)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, e.Message);
            }
            catch (HttpResponseException e)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, e.Message);
            }
            catch (Exception e)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, e.Message);
            }
        }

        /// <summary>
        /// delete user in neo4j db using http call from the client
        /// </summary>
        [Route("DeleteUser")]
        public HttpResponseMessage DeleteUser(int userId)
        {
            try
            {
                _userBl.DeleteUser(userId);

                return Request.CreateResponse(HttpStatusCode.OK, "User deleted successfully");
            }
            catch (Neo4jException e)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, e.Message);
            }
            catch (HttpResponseException e)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, e.Message);
            }
            catch (Exception e)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, e.Message);
            }
        }

        /// <summary>
        /// get user from neo4j db using http call from the client
        /// </summary>
        [HttpGet]
        [Route("GetUser")]
        public HttpResponseMessage GetUser(int userId)
        {
            try
            {
                var result = _userBl.GetUser(userId);

                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
            catch (Neo4jException e)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, e.Message);
            }
            catch (HttpResponseException e)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, e.Message);
            }
            catch (Exception e)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, e.Message);
            }
        }

        /// <summary>
        /// block user in neo4j db using http call from the client
        /// </summary>
        [HttpPost]
        [Route("BlockUser")]
        public HttpResponseMessage BlockUser(string email, string emailToBlock)
        {
            try
            {
                _userBl.BlockUser(email, emailToBlock);

                return Request.CreateResponse(HttpStatusCode.OK, "user " + email + " blocked");
            }
            catch (Neo4jException e)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, e.Message);
            }
            catch (HttpResponseException e)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, e.Message);
            }
            catch (Exception e)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, e.Message);
            }
        }


        /// <summary>
        /// follow user from neo4j db using http call from the client
        /// </summary>
        [HttpGet]
        [Route("FollowUser")]
        public HttpResponseMessage FollowUser(string email, string emailToFollow)
        {
            try
            {
                _userBl.FollowUser(email, emailToFollow);

                return Request.CreateResponse(HttpStatusCode.OK, "user " + emailToFollow + " has been followed");
            }
            catch (Neo4jException e)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, e.Message);
            }
            catch (HttpResponseException e)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, e.Message);
            }
            catch (Exception e)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, e.Message);
            }
        }

        /// <summary>
        /// unblock user from neo4j db using http call from the client
        /// </summary>
        [HttpPost]
        [Route("UnBlock")]
        public HttpResponseMessage UnBlock(string email, string emailToUnBlock)
        {
            try
            {
                _userBl.UnBlock(email, emailToUnBlock);

                return Request.CreateResponse(HttpStatusCode.OK, "user " + emailToUnBlock + " has been unblocked");
            }
            catch (Neo4jException e)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, e.Message);
            }
            catch (HttpResponseException e)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, e.Message);
            }
            catch (Exception e)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, e.Message);
            }
        }

        /// <summary>
        /// unfollow user from neo4j db using http call from the client
        /// </summary>
        [HttpPost]
        [Route("UnFollowUser")]
        public HttpResponseMessage UnFollowUser(string email, string emailToUnFollow)
        {
            try
            {
                _userBl.UnFollow(email, emailToUnFollow);

                return Request.CreateResponse(HttpStatusCode.OK, "user " + emailToUnFollow + " has been unfollowed");
            }
            catch (Neo4jException e)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, e.Message);
            }
            catch (HttpResponseException e)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, e.Message);
            }
            catch (Exception e)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, e.Message);
            }
        }

        /// <summary>
        /// get users that i follow
        /// </summary>
        [HttpPost]
        [Route("GetFollowing")]
        public HttpResponseMessage GetFollowing([FromBody]string userEmail)
        {
            try
            {
                var following = _userBl.GetFollowing(userEmail);

                return Request.CreateResponse(HttpStatusCode.OK, following);
            }
            catch (Neo4jException e)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, e.Message);
            }
            catch (HttpResponseException e)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, e.Message);
            }
            catch (Exception e)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, e.Message);
            }
        }

        /// <summary>
        /// get users that following me
        /// </summary>
        [HttpPost]
        [Route("GetFollowers")]
        public HttpResponseMessage GetFollowers([FromBody]string userEmail)
        {
            try
            {
                var followers = _userBl.GetFollowers(userEmail);

                return Request.CreateResponse(HttpStatusCode.OK, followers);
            }
            catch (Neo4jException e)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, e.Message);
            }
            catch (HttpResponseException e)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, e.Message);
            }
            catch (Exception e)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, e.Message);
            }
        }


        /// <summary>
        /// get blocked user
        /// </summary>
        [HttpPost]
        [Route("GetBlocked")]
        public HttpResponseMessage GetBlocked([FromBody]string userEmail)
        {
            try
            {
                var blocked = _userBl.GetBlockedUsers(userEmail);

                return Request.CreateResponse(HttpStatusCode.OK, blocked);
            }
            catch (Neo4jException e)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, e.Message);
            }
            catch (HttpResponseException e)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, e.Message);
            }
            catch (Exception e)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, e.Message);
            }
        }
    }
}