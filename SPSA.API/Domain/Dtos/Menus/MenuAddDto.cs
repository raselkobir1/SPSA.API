using FluentValidation;

namespace SPSA.API.Domain.Dtos.Menus
{
    public class MenuAddDto
    {
        public string Name { get; set; }
        public long? ParentId { get; set; }
        public long? MenuOrder { get; set; }
        public bool? IsLeaf { get; set; }
        public string? Action { get; set; } 
    }

    public class MenuAddDtoValidator : AbstractValidator<MenuAddDto>
    {
        public MenuAddDtoValidator()
        {
            RuleFor(obj => obj.Name).NotEmpty().MaximumLength(200);
            RuleFor(obj => obj.ParentId).GreaterThan(0);
            RuleFor(obj => obj.IsLeaf).NotEmpty();
            RuleFor(obj => obj.Name).MaximumLength(200);
        }
    }
}
