using FluentValidation;
using SPSA.API.Domain.Dtos.Common.Pageing;

namespace SPSA.API.Domain.Dtos.Roles
{
    public class RoleFilterDto: BaseFilterDto
    {
        public string? RoleName { get; set; } 
    }
    public class RoleFilterDtoValidator : AbstractValidator<RoleFilterDto>
    {
        public RoleFilterDtoValidator()
        {
            Include(new BaseFilterDtoValidator());

            RuleFor(x => x.RoleName)
                .MaximumLength(20); 
        }
    }
}
