using Amazon.DynamoDBv2.DataModel;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Models
{
    [DynamoDBTable("TokenTable")]
    public class Token
    {
        [DynamoDBHashKey]
        public string Username { get; set; }

        [DynamoDBRangeKey]
        public string CreatedTime { get; set; }

        public string TokenId { get; set; }

        public bool IsValid { get; set; }
    }
}
