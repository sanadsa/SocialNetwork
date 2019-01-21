﻿using Neo4j.Driver.V1;
using Newtonsoft.Json;
using Social.Common.Interfaces;
using Social.Common.Models;
using System;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Social.Service.Controllers
{
    [RoutePrefix("api/Post")]
    public class PostController : ApiController
    {
        private readonly IPostManager _postBl;

        public PostController(IPostManager manager) => _postBl = manager;

        /// <summary>
        /// Add post to neo4j db using http call from the client
        /// </summary>
        [HttpPost]
        [Route("CreatePost")]
        public HttpResponseMessage CreatePost([FromBody]string postJson)
        {
            try
            {
                Post post = _postBl.AddPost(postJson);
                return Request.CreateResponse(HttpStatusCode.OK, new
                {
                    UserId = post.UserId,
                    PostId = post.PostId,
                    Username = post.Username,
                    PostDate = post.PostDate,
                    Text = post.Text,
                    ImageUrl = post.ImageUrl,
                    Tags = post.Tags,
                    Privacy = post.Privacy
                });
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