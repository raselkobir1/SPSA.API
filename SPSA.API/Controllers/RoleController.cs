using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SPSA.API.Domain.Dtos.Roles;
using SPSA.API.Manager.Intrerface;

namespace SPSA.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoleController : ControllerBase
    {
        private readonly IRoleManager _roleManager;
        public RoleController(IRoleManager roleManager)
        {
                _roleManager = roleManager;
        }
        [HttpGet("role/{id}")]
        public async Task<IActionResult> GetRole(long id)
        { 
            var response = await _roleManager.GetRoleById(id);
            return StatusCode(response.StatusCode, response);
        }

        [HttpGet("roles")] 
        public async Task<IActionResult> GetRoles([FromQuery] RoleFilterDto dto) 
        {
            var response = await _roleManager.GetPasignatedUserResult(dto);
            return StatusCode(response.StatusCode, response); 
        }

        [HttpPost("AddRole")]
        public async Task<IActionResult>AddRole(RoleAddDto dto)
        {
            var response = await _roleManager.RoleAdd(dto);
            return StatusCode(response.StatusCode,response);    
        }

        [HttpPost("updateRole")]
        public async Task<IActionResult> UpdateRole(RoleUpdateDto dto) 
        {
            var response = await _roleManager.RoleUpdate(dto);
            return StatusCode(response.StatusCode, response);
        }

        [HttpPost("dropdownForRole")]
        public async Task<IActionResult> GetDropdownForRole()  
        {
            var response = await _roleManager.GetDropdownForRoles();
            return StatusCode(response.StatusCode, response);
        }

    }
}
