using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AdminEmpPortal.Models
{
    public class BearerToken
    {
        public string  Token { get; set; }
        public DateTime Validity { get; set; }
        public string RefreshToken { get; set; }
        public int Id { get; set; }
    }
}