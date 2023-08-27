using SPSA.API.Domain.Dtos.Common;
using SPSA.API.Domain.Dtos.Roles;

namespace SPSA.API.Manager.Intrerface
{
    public interface IRoleManager
    {
        Task<ResponseModel> RoleAdd(RoleAddDto dto);
        Task<ResponseModel> RoleUpdate(RoleUpdateDto dto);
        Task<ResponseModel> GetRoleById(long id); 
        Task<ResponseModel> GetPasignatedUserResult(RoleFilterDto dto);
        Task<ResponseModel> GetDropdownForRoles();
    }
}
