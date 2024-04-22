using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API_Dotnet.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace API_Dotnet.Services
{
    public interface IAccountservice
    {
        Task<IActionResult> UserRegister([FromBody] UserregistrationDto Dto);
    }
}