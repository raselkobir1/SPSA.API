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

        [HttpGet("users")]
        public async Task<IActionResult> GetAllUser() 
        {
            var response = await _userManager.GetAllUsers();
            return StatusCode(response.StatusCode, response);
        }

        [HttpGet("user/{id}")]
        public async Task<IActionResult> GetUserById(long id)
        {
            var response = await _userManager.GetUserById(id);
            return StatusCode(response.StatusCode, response);
        }
    }
}
