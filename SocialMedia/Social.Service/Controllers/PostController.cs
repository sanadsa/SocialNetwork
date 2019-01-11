using Neo4j.Driver.V1;
using Social.Common.Interfaces;
using Social.Common.Models;
using System;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Social.Service.Controllers
{
    public class PostController : ApiController
    {
        private readonly IPostManager _postBl;
        public PostController(IPostManager manager)
        {
            _postBl = manager;
        }

        /// <summary>
        /// Add post to neo4j db using http call from the client
        /// </summary>
        [HttpPost]
        [Route("CreateUser")]
        public HttpResponseMessage CreatePost(int userId, Post post)
        {
            try
            {
                _postBl.AddPost(userId, post);

                return Request.CreateResponse(HttpStatusCode.OK, "post added successfully");
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