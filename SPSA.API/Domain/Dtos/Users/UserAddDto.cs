using FluentValidation;

namespace SPSA.API.Domain.Dtos.Users
{
    public class UserAddDto
    {
        public string FullName { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public long BranchId { get; set; }
        public long RoleId { get; set; }
        public bool? IsSuperAdmin { get; set; }
        public bool? IsActive { get; set; }
    }
    public class UserAddDtoValidator : AbstractValidator<UserAddDto>
    {
        public UserAddDtoValidator()
        {
            RuleFor(obj => obj.FullName).NotEmpty().MaximumLength(200);
            RuleFor(obj => obj.RoleId).NotEmpty().GreaterThan(0);
            RuleFor(obj => obj.IsActive).NotNull();
            RuleFor(obj => obj.IsSuperAdmin).NotNull(); 
            RuleFor(obj => obj.Password).NotEmpty().WithMessage("Your password cannot be empty")
                    .MinimumLength(8).WithMessage("Your password length must be at least 8.")
                    .MaximumLength(16).WithMessage("Your password length must not exceed 16.")
                    .Matches(@"[A-Z]+").WithMessage("Your password must contain at least one uppercase letter.")
                    .Matches(@"[a-z]+").WithMessage("Your password must contain at least one lowercase letter.")
                    .Matches(@"[0-9]+").WithMessage("Your password must contain at least one number.")
                    .Matches(@"^.*[!@#$%^&*()\-_=+{}[\]|\\:;""'<>,.?\/].*$")
                    .WithMessage(@"Your password must contain at least one special charecter(@#$!%^&*-_=+(){}[]|:;'""<>,.?\/).");

            RuleFor(obj => obj.Email).NotEmpty().MaximumLength(200)
                    .Matches(@"^[a-zA-Z0-9](?!.*[._-]{2})[a-zA-Z0-9._-]*[a-zA-Z0-9]@[a-zA-Z0-9-]+(\.[a-zA-Z0-9-]+)*\.[a-zA-Z]{2,6}$")
                    .WithMessage("Invalid email address");
        }
    }

}
