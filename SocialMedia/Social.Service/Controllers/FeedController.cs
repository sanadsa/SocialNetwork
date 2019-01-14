using Neo4j.Driver.V1;
using Social.Common.Interfaces;
using System;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Social.Service.Controllers
{
    [RoutePrefix("api/Feed")]
    public class FeedController : ApiController
    {
        private readonly IFeedManager _feedBl;

        public FeedController(IFeedManager repo)
        {
            _feedBl = repo;
        }

        /// <summary>
        /// get feed to neo4j db using http call from the client
        /// </summary>
        [HttpGet]
        [Route("GetFeed")]
        public HttpResponseMessage GetFeed(string token)
        {
            try
            {
                _feedBl.GetFeed(token);

                return Request.CreateResponse(HttpStatusCode.OK, "feed updated successfully");
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
        /// get my posts to neo4j db using http call from the client
        /// </summary>
        [HttpGet]
        [Route("GetMyPosts")]
        public HttpResponseMessage GetMyPosts([FromBody]string token)
        {
            try
            {
                var posts = _feedBl.GetMyPosts(token);

                return Request.CreateResponse(HttpStatusCode.OK, posts);
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