using Amazon.DynamoDBv2.DataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Models
{
    [DynamoDBTable("TokensTable")]
    public class Token
    {
        [DynamoDBHashKey]
        public string TokenId { get; set; }

        [DynamoDBRangeKey]
        public DateTime CreatedTime { get; set; }

        public string Email { get; set; }

        public bool IsValid { get; set; }
    }
}
