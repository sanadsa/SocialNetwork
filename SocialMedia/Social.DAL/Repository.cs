﻿//using Social.Common.Exceptions;
using Neo4j.Driver.V1;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Social.DAL
{
    /// <summary>
    /// class that contains reused methods to run neo4j queries
    /// </summary>
    public class Repository
    {
        /// <summary>
        /// run neo4j queries
        /// </summary>
        /// <param name="driver"></param>
        /// <param name="query"></param>
        public void RunQuery(IDriver driver, string query)
        {
            try
            {
                using (var session = driver.Session())
                {
                    var result = session.Run(query);
                }
            }
            catch (Neo4jException ex)
            {
                throw new Neo4jException("A problem during operation in neo4j db", ex.InnerException);
            }
            catch (Exception ex)
            {
                throw new Exception("A problem during operation in neo4j db", ex.InnerException);
            }
        }

        /// <summary>
        /// Serialize object to Json without -"-
        /// usefull when passing to object to cypher query for neo4j
        /// </summary>
        public string ObjectToJson(object obj)
        {
            var serializer = new JsonSerializer();
            var stringWriter = new StringWriter();
            using (var writer = new JsonTextWriter(stringWriter))
            {
                writer.QuoteName = false;
                serializer.Serialize(writer, obj);
            }
            return stringWriter.ToString();
        }
    }
}
