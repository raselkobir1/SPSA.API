using SPSA.API.Domain;
using SPSA.API.Domain.Dtos.Common.Pageing;
using SPSA.API.Domain.Dtos.Users;

namespace SPSA.API.DataAccess.Interfaces
{
    public interface IUserRepository : IGenericRepository<User>
    {
        Task<PagingResponseDto> GetPasignatedUserResult(UserFilterDto dto); 
    }
}
