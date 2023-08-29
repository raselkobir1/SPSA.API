using Microsoft.EntityFrameworkCore;
using SPSA.API.DataAccess.DataContext;
using SPSA.API.DataAccess.Interfaces;
using SPSA.API.Domain;
using SPSA.API.Domain.Dtos.Common;
using SPSA.API.Domain.Dtos.Common.Pageing;
using SPSA.API.Domain.Dtos.Menus;

namespace SPSA.API.DataAccess.Implementations
{
    public class MenuRepository : GenericRepository<Menu>, IMenuRepository
    {
        public MenuRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<IEnumerable<DropdownCommontDto>> GetDropdownForRoles()
        {
            var query =await _dbContext.Menus
                       .Select(x => new DropdownCommontDto
                        {
                            Id = x.Id,
                            Name = x.Name,
                        }).ToListAsync();
            return query;
        }

        public async Task<PagingResponseDto> GetPasignatedUserResult(MenuFilterDto dto)
        {
            var query = _dbContext.Menus
                        .AsNoTracking()
                        .Where(x => !x.IsDeleted)
                        .AsQueryable();

            if (!string.IsNullOrWhiteSpace(dto.Name))
                query = query.Where(x => x.Name.Contains(dto.Name));

            var result = await (query
                        .OrderByDescending(x => x.Id)
                        .Skip(dto.Skip)
                        .Take(dto.PageSize)
                        .Select(x => new MenuDto
                        {
                            Id = x.Id,
                            Name = x.Name,
                            Action = x.Action,
                            IsLeaf = x.IsLeaf,
                            MenuOrder = x.MenuOrder,
                            ParentId = x.ParentId,
                        })
                        .ToListAsync());

            return new PagingResponseDto(result, query.Count(), dto.PageNumber, dto.PageSize);
        }
    }
}
