using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AdminEmpPortal.DTOs;
using AdminEmpPortal.Models;
using AdminEmpPortal.Services;
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
        private readonly ITokenUtils _tokenUtils;


        public Accountservice(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, IConfiguration configuration, ApplicationDbContext context, ITokenUtils tokenUtils)
        {
            _userManager = userManager;
            _configuration = configuration;
            _context = context;
            _signInManager = signInManager;
            _tokenUtils = tokenUtils;
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
                Email = Dto.Email,
                Firstname = Dto.Firstname,
                Lastname = Dto.Lastname,
                Companyname = Dto.Companyname,
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
            await _userManager.AddClaimAsync(user, new Claim("type", "Employee"));
            return new OkObjectResult(new Success()
            {
                Text = "User registration successfull"
            });
        }


        private BearerToken GetToken(ApplicationUser AppUser)
        {
            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, AppUser.UserName),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(ClaimTypes.NameIdentifier, AppUser.Id.ToString()),
                new Claim(ClaimTypes.Name, AppUser.UserName),
                new Claim("type", "Employee")
            };

            var token = _tokenUtils.GenerateJwtToken(claims);
            var strToken = new JwtSecurityTokenHandler().WriteToken(token);

            // var refreshToken = _tokenUtils.GenerateRefreshToken();
            // var expiresAt = DateTime.UtcNow.AddDays(Convert.ToDouble(_configuration["RefreshTokenExpireDays"]));

            return new BearerToken
            {
                Token = strToken,
                Validity = token.ValidTo,
                RefreshToken = null,
                Id = AppUser.Id
            };
        }

        public virtual async Task<ActionResult<BearerToken>> empLogin([FromBody] EmpLoginDto Dto)
        {
            var Employee = _context.IdentityUserClaims.Where(x => x.ClaimValue == "Employee" && x.User.UserName == Dto.Email).Select(x => x.User).FirstOrDefault();
            if (Employee == null)
            {
                return new BadRequestObjectResult(new Error()
                {
                    Text = "Wrong Email!!!"
                });
            }
            var result = await _signInManager.CheckPasswordSignInAsync(Employee, Dto.Password, false);
            if (!result.Succeeded)
            {
                return new BadRequestObjectResult(new Error()
                {
                    Text = "Wrong Password!!!"
                });
            }

            // Generate tokens for the user
            var tokenResult = GetToken(Employee);

            return tokenResult;


        }


    }
}

