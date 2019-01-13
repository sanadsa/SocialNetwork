﻿using Identity.Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Net;
using System.Net.Http;
using Identity.Common.Model;
using Newtonsoft.Json;

namespace Identity.Service.Controllers
{
    [RoutePrefix("api/Identity")]
    public class IdentityController : ApiController
    {
        private readonly IIdentityManager bl;

        public IdentityController(IIdentityManager manager)
        {
            bl = manager;
        }

        /// <summary>
        /// Checks whether user exist or not.
        /// </summary>
        [HttpPost]
        [Route("CheckIfUserExist")]
        public HttpResponseMessage CheckIfUserExist([FromBody]string email)
        {
            try
            {
                var ifUserExist = bl.CheckIfUserExist(email);
                return Request.CreateResponse(HttpStatusCode.OK, ifUserExist);
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
        /// Add userIdentity to db using http call from the client
        /// </summary>
        [HttpPost]
        [Route("CreateUserIdentity")]
        public HttpResponseMessage CreateUserIdentity([FromBody]string userIdentityJson)
        {
            try
            {
                bl.AddUser(JsonConvert.DeserializeObject<UserIdentity>(userIdentityJson));
                return Request.CreateResponse(HttpStatusCode.OK, "User added successfully");
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
        /// Update user identity in dynamodb using http call from the client
        /// </summary>
        [HttpPost]
        [Route("UpdateUserIdentity")]
        public HttpResponseMessage UpdateUserIdentity([FromBody]UserIdentity userIdentity)
        {
            try
            {
                bl.UpdateUser(userIdentity);
                return Request.CreateResponse(HttpStatusCode.OK, "User updated successfully");
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
        /// get useridentity
        /// </summary>
        [HttpGet]
        [Route("GetUserIdentity")]
        public HttpResponseMessage GetUserIdentity([FromBody]string email)
        {
            try
            {
                var user = bl.GetUser(email);
                return Request.CreateResponse(HttpStatusCode.OK, user);
            }
            catch (KeyNotFoundException e)
            {
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, e.Message);
            }
            catch (Exception e)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, e.Message);
            }
        }
    }
}