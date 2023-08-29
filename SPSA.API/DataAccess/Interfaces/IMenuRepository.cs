using SPSA.API.Domain;
using SPSA.API.Domain.Dtos.Common;
using SPSA.API.Domain.Dtos.Common.Pageing;
using SPSA.API.Domain.Dtos.Menus;

namespace SPSA.API.DataAccess.Interfaces
{
    public interface IMenuRepository :IGenericRepository<Menu>
    {
        Task<PagingResponseDto> GetPasignatedUserResult(MenuFilterDto dto);
        Task<IEnumerable<DropdownCommontDto>> GetDropdownForRoles();
    }
}
