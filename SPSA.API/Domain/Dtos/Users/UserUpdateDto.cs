using FluentValidation;

namespace SPSA.API.Domain.Dtos.Users
{
    public class UserUpdateDto
    {
        public long Id { get; set; } 
        public string FullName { get; set; }
        public string Email { get; set; }
        public long RoleId { get; set; }
        public bool? IsActive { get; set; }
        public bool? IsSuperAdmin { get; set; }
    }
    public class UserUpdateDtoValidator : AbstractValidator<UserUpdateDto>
    {
        public UserUpdateDtoValidator()
        {
            RuleFor(obj => obj.Id).NotEmpty().GreaterThan(0);
            RuleFor(obj => obj.RoleId).NotEmpty().GreaterThan(0);
            RuleFor(obj => obj.FullName).NotEmpty().MaximumLength(200);
            RuleFor(obj => obj.IsActive).NotEmpty();
            RuleFor(obj => obj.IsSuperAdmin).NotEmpty();
            RuleFor(obj => obj.Email).NotEmpty().MaximumLength(200)
                .Matches(@"^[a-zA-Z0-9](?!.*[._-]{2})[a-zA-Z0-9._-]*[a-zA-Z0-9]@[a-zA-Z0-9-]+(\.[a-zA-Z0-9-]+)*\.[a-zA-Z]{2,6}$")
                .WithMessage("Invalid email address");
        }
    }
}
