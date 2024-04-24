using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AdminEmpPortal.DTOs;
using AdminEmpPortal.Models;
using API_Dotnet.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace API_Dotnet.Services
{
    public interface IAccountservice
    {
        Task<IActionResult> UserRegister([FromBody] UserregistrationDto Dto);
        Task<ActionResult<BearerToken>> empLogin([FromBody] EmpLoginDto Dto);
    }
}