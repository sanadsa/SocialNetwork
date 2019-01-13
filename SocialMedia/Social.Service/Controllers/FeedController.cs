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
        private readonly IFeedRepository _feedDal;

        public FeedController(IFeedRepository repo)
        {
            _feedDal = repo;
        }

        /// <summary>
        /// Add user to neo4j db using http call from the client
        /// </summary>
        [HttpGet]
        [Route("GetFeed")]
        public HttpResponseMessage GetFeed(int userId)
        {
            try
            {
                _feedDal.GetFeed(userId);

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
    }
}