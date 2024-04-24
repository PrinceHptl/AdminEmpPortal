using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AdminEmpPortal.Models
{
    public class RefreshToken:BaseModel
    {
        public DateTime IssuedAt { get; set; }
        public DateTime ExpiresAt { get; set; }
        public string Token { get; set; }
        public string Email { get; set; }
    }
}