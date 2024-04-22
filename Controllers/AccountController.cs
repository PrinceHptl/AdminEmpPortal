using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API_Dotnet.DTOs;
using API_Dotnet.Services;
using Microsoft.AspNetCore.Mvc;

namespace API_Dotnet.Controllers
{
    public class AccountController:baseApiController
    {
        private readonly IAccountservice _accountservice;
        public AccountController(IAccountservice accountservice)
        {
            _accountservice = accountservice;
        }
        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> UserRegister([FromBody] UserregistrationDto Dto)
        {
            return await _accountservice.UserRegister(Dto);
           // return Ok(result);
        }

    }
}