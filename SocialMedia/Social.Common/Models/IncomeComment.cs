using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Social.Common.Models
{
    public class IncomeComment
    {
        public string CommentId { get; set; }
        public string UserId { get; set; }
        public string PostId { get; set; }
        public string CommentValue { get; set; }
    }
}
