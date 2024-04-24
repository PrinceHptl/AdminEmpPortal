using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AdminEmpPortal.DTOs;
using AdminEmpPortal.Models;
using AdminEmpPortal.Services;
using Microsoft.AspNetCore.Mvc;

namespace AdminEmpPortal.Controllers
{
    public class AccountController : baseApiController
    {
        private readonly IAccountservice _accountservice;
        public AccountController(IAccountservice accountservice)
        {
            _accountservice = accountservice;
        }

        /// <summary>
        /// for register the employee
        /// </summary>
        /// <param name="Dto"></param>
        /// <returns>return success if register otherwise error</returns>
        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> UserRegister([FromBody] UserregistrationDto Dto)
        {
            return await _accountservice.UserRegister(Dto);
            // return Ok(result);
        }

        /// <summary>
        /// for login to employee
        /// </summary>
        /// <returns> success if login</return> 
        [HttpPost]
        [Route("[action]")]
        
        public async Task<ActionResult<BearerToken>> empLogin([FromBody] EmpLoginDto Dto)
        {
            return await _accountservice.empLogin(Dto);
            
        }



        }
    }