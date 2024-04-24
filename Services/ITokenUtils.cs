using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace AdminEmpPortal.Services
{
    public interface ITokenUtils
    {
        JwtSecurityToken GenerateJwtToken(IEnumerable<Claim> claims);
       // string GenerateJwtToken();
        ClaimsPrincipal GetPrincipalFromExpiredToken(string token);
    }
}