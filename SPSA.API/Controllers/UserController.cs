using Microsoft.AspNetCore.Mvc;
using SPSA.API.Domain.Dtos.Users;
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

        [HttpPost("userAdd")]
        public async Task<IActionResult> AddUser([FromBody] UserAddDto user)
        {
            var response = await _userManager.UserAdd(user);
            return StatusCode(response.StatusCode, response);
        }

        [HttpPost("userUpdate")]
        public async Task<IActionResult> UpdateUser([FromBody] UserUpdateDto user) 
        {
            var response = await _userManager.UserUpdate(user);
            return StatusCode(response.StatusCode, response);
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
