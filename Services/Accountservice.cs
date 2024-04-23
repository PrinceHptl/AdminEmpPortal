using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using API_Dotnet.Data;
using API_Dotnet.DTOs;
using API_Dotnet.Models;
using Azure;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace API_Dotnet.Services
{
    class Accountservice : IAccountservice
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IConfiguration _configuration;
        private readonly ApplicationDbContext _context;

        private readonly SignInManager<ApplicationUser> _signInManager;
        

        public Accountservice(UserManager<ApplicationUser> userManager,SignInManager<ApplicationUser> signInManager, IConfiguration configuration,ApplicationDbContext context)
        {
            _userManager = userManager;
            _configuration = configuration;
            _context = context;
            _signInManager = signInManager;
        }

        public virtual async Task<IActionResult> UserRegister([FromBody] UserregistrationDto Dto)
        {
            var userexist = await _userManager.FindByEmailAsync(Dto.Email);
            if (userexist != null)
            {
                return new BadRequestObjectResult(new Error()
                {
                    Text = "Email is already taken by another user"
                });
            }
            ApplicationUser user = new ApplicationUser()
            {
                UserName = Dto.Email,
                Email=Dto.Email,
                Firstname=Dto.Firstname,
                Lastname=Dto.Lastname,
                Companyname=Dto.Companyname,
                SecurityStamp = Guid.NewGuid().ToString()
            };
            var result = await _userManager.CreateAsync(user, Dto.Password);
            if (!result.Succeeded)
            {
                return new BadRequestObjectResult(new Error()
                {
                    Text = "Password should be atleas 6 Character and minimum one character must be in uppercase"
                });
            }
            await _userManager.AddClaimAsync(user,new Claim("type","Employee"));
            return new OkObjectResult(new Success()
            {
                     Text="User registration successfull"
            });
        }
           

    }
}

