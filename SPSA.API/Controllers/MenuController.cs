using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SPSA.API.Domain.Dtos.Menus;
using SPSA.API.Manager.Intrerface;

namespace SPSA.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MenuController : ControllerBase
    {
        private readonly IMenuManager _menuManager; 
        public MenuController(IMenuManager menuManager)
        {
            _menuManager = menuManager; 
        }
        [HttpGet("menu/{id}")] 
        public async Task<IActionResult> GetMenu(long id) 
        {
            var response = await _menuManager.GetMenuById(id);
            return StatusCode(response.StatusCode, response);
        }

        [HttpGet("menus")] 
        public async Task<IActionResult> GetMenus([FromQuery] MenuFilterDto dto) 
        {
            var response = await _menuManager.GetPasignatedMenuResult(dto);
            return StatusCode(response.StatusCode, response);
        }

        [HttpPost("ddMenu")]
        public async Task<IActionResult> AddMenu(MenuAddDto dto)
        {
            var response = await _menuManager.MenuAdd(dto);
            return StatusCode(response.StatusCode, response);
        }

        [HttpPost("updateMenu")]
        public async Task<IActionResult> UpdateMenu(MenuUpdateDto dto) 
        {
            var response = await _menuManager.MenuUpdate(dto);
            return StatusCode(response.StatusCode, response);
        }

        [HttpPost("dropdownForMenu")]
        public async Task<IActionResult> GetDropdownForMenu() 
        {
            var response = await _menuManager.GetDropdownForMenus();
            return StatusCode(response.StatusCode, response);
        }
    }
}
