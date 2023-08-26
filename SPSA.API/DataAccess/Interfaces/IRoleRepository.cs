using SPSA.API.Domain;
using SPSA.API.Domain.Dtos.Common.Pageing;
using SPSA.API.Domain.Dtos.Roles;

namespace SPSA.API.DataAccess.Interfaces
{
    public interface IRoleRepository : IGenericRepository<Role>
    {
        Task<PagingResponseDto> GetPasignatedUserResult(RoleFilterDto dto);
    }
}
