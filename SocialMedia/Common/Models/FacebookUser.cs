using Amazon.DynamoDBv2.DataModel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Models
{
    [DynamoDBTable("FacebookUserAuthTable")]
    public class FacebookUser
    {
        [DynamoDBHashKey]
        public string UserFacebookId { get; set; }

        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        public string Username { get; set; }

        [DefaultValue(false)]
        public bool IsAvilable { get; set; }
    }
}
