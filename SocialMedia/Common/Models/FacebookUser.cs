using Amazon.DynamoDBv2.DataModel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Models
{
    [DynamoDBTable("AuthenticationTable")]
    public class FacebookUser
    {
        [DynamoDBHashKey]
        public string Email { get; set; }

        [DynamoDBRangeKey]
        public string Username { get; set; }

        [DefaultValue(false)]
        public bool IsAvilable { get; set; }
    }
}
