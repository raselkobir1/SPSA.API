using FluentValidation;

namespace SPSA.API.Domain.Dtos.Menus
{
    public class MenuFilterDto
    {
        public string? Name { get; set; } 
    }
    public class MenuFilterDtoValidator : AbstractValidator<MenuFilterDto>
    {
        public MenuFilterDtoValidator()
        {
            RuleFor(obj => obj.Name).NotEmpty();
        }
    }
}
