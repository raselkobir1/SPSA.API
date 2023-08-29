using SPSA.API.Domain.Dtos.Common;
using SPSA.API.Domain.Dtos.Menus;

namespace SPSA.API.Manager.Intrerface
{
    public interface IMenuManager
    {
        Task<ResponseModel> MenuAdd(MenuAddDto dto); 
        Task<ResponseModel> MenuUpdate(MenuUpdateDto dto); 
        Task<ResponseModel> GetMenuById(long id); 
        Task<ResponseModel> GetPasignatedMenuResult(MenuFilterDto dto); 
        Task<ResponseModel> GetDropdownForMenus(); 
    }
}
