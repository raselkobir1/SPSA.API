using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SPSA.API.Manager.Intrerface;

namespace SPSA.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserManager _userManager;
        public UserController(IUserManager userManager)
        {
            _userManager = userManager;
        }

        [HttpGet]   
        public async Task<IActionResult> GetUserByEmail(string email)
        {
            var response = await _userManager.GetUserByEmail(email);
            return StatusCode(response.StatusCode, response);
        }
    }
}
