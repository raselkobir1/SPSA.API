using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SPSA.API.Domain.Dtos.Token;
using SPSA.API.Manager.Intrerface;

namespace SPSA.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TokenController : ControllerBase
    {
        private readonly ITokenManager _tokenManager;
        public TokenController(ITokenManager tokenManager)
        {
            _tokenManager = tokenManager;   
        }

        [AllowAnonymous]
        [HttpPost("refreshToken")] 
        public async Task<IActionResult> GetNewJwtToken([FromBody] RefreshTokenFilterDto dto)
        {
            var response = await _tokenManager.GetNewJwtToken(dto);
            return StatusCode(response.StatusCode, response);
        }

        [HttpPost("revoke")]
        public async Task<IActionResult> RevokeToken([FromBody] RefreshTokenFilterDto dto)
        {
            var response = await _tokenManager.RevokeToken(dto);
            return StatusCode(response.StatusCode, response); 
        }
    }
}
