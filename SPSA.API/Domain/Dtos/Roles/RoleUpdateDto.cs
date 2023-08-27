using FluentValidation;

namespace SPSA.API.Domain.Dtos.Roles
{
    public class RoleUpdateDto
    {
        public long Id { get; set; }
        public string Name { get; set; } 
    }

    public class RoleUpdateDtoValidator : AbstractValidator<RoleUpdateDto>
    {
        public RoleUpdateDtoValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty()
                .GreaterThan(0);
            RuleFor(x => x.Name)
                .NotEmpty()
                .MaximumLength(20);
        }
    }
}
