using FluentValidation;
using SPSA.API.Domain.Dtos.Common.Pageing;

namespace SPSA.API.Domain.Dtos.Menus
{
    public class MenuFilterDto: BaseFilterDto
    {
        public string? Name { get; set; } 
    }
    public class MenuFilterDtoValidator : AbstractValidator<MenuFilterDto>
    {
        public MenuFilterDtoValidator()
        {
            RuleFor(obj => obj.Name).MaximumLength(20);
        }
    }
}
