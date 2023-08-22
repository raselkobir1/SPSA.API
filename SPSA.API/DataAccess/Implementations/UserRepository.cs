using Microsoft.EntityFrameworkCore;
using SPSA.API.DataAccess.DataContext;
using SPSA.API.DataAccess.Interfaces;
using SPSA.API.Domain;
using SPSA.API.Domain.Dtos.Common.Pageing;
using SPSA.API.Domain.Dtos.Users;

namespace SPSA.API.DataAccess.Implementations
{
    public class UserRepository : GenericRepository<User>, IUserRepository
    {
        public UserRepository(ApplicationDbContext context) : base(context)
        {
        }

        public async Task<PagingResponseDto> GetPasignatedUserResult(UserFilterDto dto) 
        {
            var query = _dbContext.Users
                        .Include(x => x.Role)
                        .Include(x => x.Branch)
                        .Where(x => x.IsActive)
                        .AsNoTracking()
                        .AsQueryable(); 

            if (!string.IsNullOrWhiteSpace(dto.FullName))
                query = query.Where(x => x.FullName.Contains(dto.FullName));

            if(!string.IsNullOrWhiteSpace(dto.Email))
                query = query.Where(x => x.Email.Contains(dto.Email));

            var result = await (query
                        .OrderByDescending(x => x.Id)
                        .Skip(dto.Skip).Take(dto.PageSize)
                        .Select(x => new UserDto
                        {
                            Id = x.Id,
                            RoleId = x.RoleId,
                            FullName = x.FullName,
                            Email = x.Email,
                            IsSuperAdmin = x.IsSuperAdmin,
                            RoleName = x.Role.Name,
                            IsActive = x.IsActive
                        })
                         .ToListAsync());
            return new PagingResponseDto(result, query.Count(), dto.PageNumber, dto.PageSize);
        }
    }
}
