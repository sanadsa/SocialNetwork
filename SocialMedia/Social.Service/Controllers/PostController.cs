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
        [Route("CreatePost")]
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

        /// <summary>
        /// comment on post in neo4j db using http call from the client
        /// </summary>
        [HttpPost]
        [Route("CommentPost")]
        public HttpResponseMessage CommentPost(int postId, Comment comment)
        {
            try
            {
                _postBl.CommentPost(postId, comment);

                return Request.CreateResponse(HttpStatusCode.OK, "comment added to post successfully");
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
        /// comment on post in neo4j db using http call from the client
        /// </summary>
        [HttpPost]
        [Route("LikePost")]
        public HttpResponseMessage LikePost(int userId, int postId)
        {
            try
            {
                _postBl.LikePost(userId, postId);

                return Request.CreateResponse(HttpStatusCode.OK, "user liked a post successfully");
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