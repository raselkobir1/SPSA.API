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
        [HttpGet("roles")] 
        public async Task<IActionResult> GetRoles([FromQuery] RoleFilterDto dto) 
        {
            var response = await _roleManager.GetPasignatedUserResult(dto);
            return StatusCode(response.StatusCode, response); 
        }
    }
}
