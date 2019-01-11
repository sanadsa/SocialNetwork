using Neo4j.Driver.V1;
using Social.Common.Interfaces;
using Social.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Social.DAL
{
    public class FeedRepository : IFeedRepository
    {
        static IDriver driver;
        Repository _repo = new Repository();

        public FeedRepository() => driver = GraphDatabase.Driver("bolt://localhost:7687", AuthTokens.Basic("neo4j", "password"));

        /// <summary>
        /// get feed from db
        /// </summary>
        public Feed GetFeed(int feedId)
        {
            var query = "";
            var result = _repo.RunQuery(driver, query);
            return (Feed)result.GetEnumerator();
        }
    }
}
