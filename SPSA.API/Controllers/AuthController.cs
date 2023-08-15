using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SPSA.API.Domain.Dtos;
using SPSA.API.Manager.Intrerface;

namespace SPSA.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthManager _authManager;     
        public AuthController(IAuthManager authManager)
        {
            _authManager = authManager;
        }
        [AllowAnonymous]
        [HttpPost("signIn")] 
        public async Task<IActionResult> Authinticate(SignInDto dto) 
        {
            var response = await _authManager.SignIn(dto);
            return StatusCode(response.StatusCode, response);
        }
    }
}
