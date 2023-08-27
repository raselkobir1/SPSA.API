using FluentValidation;

namespace SPSA.API.Domain.Dtos.Roles
{
    public class RoleAddDto
    {
        public string Name { get; set; }
    }
    public class RoleAddDtoValidator : AbstractValidator<RoleAddDto>
    {
        public RoleAddDtoValidator()
        {

            RuleFor(x => x.Name)
                .NotEmpty()
                .MaximumLength(20);
        }
    }
}
