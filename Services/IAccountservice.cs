using AdminEmpPortal.DTOs;
using AdminEmpPortal.Models;
using Microsoft.AspNetCore.Mvc;

namespace AdminEmpPortal.Services
{
    public interface IAccountservice
    {
        Task<IActionResult> UserRegister([FromBody] UserregistrationDto Dto);
        Task<ActionResult<BearerToken>> empLogin([FromBody] EmpLoginDto Dto);
       // Task<ActionResult<EmployeeDto>> GetEmpById(int Id);
        
    }
}