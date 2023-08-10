using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SPSA.API.Domain.Dtos;

namespace SPSA.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> Authinticate(AuthinticateDto dto) 
        {
            return Ok( await Task.Run(() => new {}));
        }
    }
}
