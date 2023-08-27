using Microsoft.EntityFrameworkCore;
using SPSA.API.DataAccess.DataContext;
using SPSA.API.DataAccess.Interfaces;
using SPSA.API.Domain;
using SPSA.API.Domain.Dtos.Common;
using SPSA.API.Domain.Dtos.Common.Pageing;
using SPSA.API.Domain.Dtos.Roles;

namespace SPSA.API.DataAccess.Implementations
{
    public class RoleRepository : GenericRepository<Role>, IRoleRepository
    {
        public RoleRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<IEnumerable<DropdownCommontDto>> GetDropdownForRoles()
        {
            var roles = await( _dbContext.Roles
                        .AsNoTracking()
                        .Select( x => new DropdownCommontDto
                        {
                            Id = x.Id,
                            Name = x.Name,  
                        }).ToListAsync());

            return roles;    
        }

        public async Task<PagingResponseDto> GetPasignatedUserResult(RoleFilterDto dto)
        {
            var query = _dbContext.Roles
                        .AsNoTracking()
                        .Where(x => !x.IsDeleted)
                        .AsQueryable();

            if(!string.IsNullOrWhiteSpace(dto.RoleName))
                query = query.Where(x => x.Name.Contains(dto.RoleName)); 

            var result = await (query
                        .OrderByDescending(x => x.Id)
                        .Skip(dto.Skip)
                        .Take(dto.PageSize)
                        .Select(x => new RoleDto
                        {
                            Id = x.Id,
                            Name = x.Name,
                        })
                        .ToListAsync());

            return new PagingResponseDto(result, query.Count(), dto.PageNumber, dto.PageSize);  
        }
    }
}
