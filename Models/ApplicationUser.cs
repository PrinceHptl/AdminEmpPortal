using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace API_Dotnet.Models
{
    public class ApplicationUser : IdentityUser<int>
    {
        public virtual IEnumerable<AppUserClaim> Claims { get; set; }
        [Column("Firstname")]

        public string Firstname { get; set; }
        [Column("Lastname")]

        public string Lastname { get; set; }
        [Column("CompanyName")]

        public string Companyname { get; set; }

        //public string Email { get; set; }

        //public string Password { get; set; }
    }
}