using FluentValidation;

namespace SPSA.API.Domain.Dtos.Menus
{
    public class MenuUpdateDto
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public long? ParentId { get; set; }
        public long? MenuOrder { get; set; }
        public bool? IsLeaf { get; set; }
        public string? Action { get; set; }
    }

    public class MenuUpdateDtoValidator : AbstractValidator<MenuUpdateDto>
    {
        public MenuUpdateDtoValidator()
        {
            RuleFor(obj => obj.Id).NotEmpty().GreaterThan(0);
            RuleFor(obj => obj.Name).NotEmpty().MaximumLength(200);
            RuleFor(obj => obj.IsLeaf).NotEmpty();
            RuleFor(obj => obj.Name).MaximumLength(200);
        }
    }
}
 