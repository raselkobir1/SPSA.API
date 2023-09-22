using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace SPSA.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoleWiseMenuController : ControllerBase
    {
        [HttpGet]   
        public async Task<IActionResult> GetRoleWiseMenus()
        {
            return Ok();
        }
    }
}
