using Amazon.DynamoDBv2.DataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Identity.Common.Model
{
    [DynamoDBTable("Identity")]
    public class UserIdentity
    {
        [DynamoDBHashKey]
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int Age { get; set; }
        public string Address { get; set; }
        public string WorkAddress { get; set; }

        public override string ToString()
        {
            return "Email: " + Email + " FisrtNAme: " + FirstName + " Age: " + Age;
        }
    }
}
