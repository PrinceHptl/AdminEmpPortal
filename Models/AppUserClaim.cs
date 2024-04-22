using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace API_Dotnet.Models
{
    public class AppUserClaim:IdentityUserClaim<int>
    {
        [ForeignKey("UserId")]
        public ApplicationUser User { get; set; }
        // public int AdminId { get; set; }
        // [ForeignKey("AdminId")]
        // public ApplicationUser Admin { get; set; }
        // public int EmployeeId { get; set; }
        // [ForeignKey("EmployeeId")]
        // public ApplicationUser Emplpyee { get; set; }

    }
}