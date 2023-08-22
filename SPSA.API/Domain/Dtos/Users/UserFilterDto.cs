using FluentValidation;
using SPSA.API.Domain.Dtos.Common.Pageing;

namespace SPSA.API.Domain.Dtos.Users
{
    public class UserFilterDto: BaseFilterDto
    {
        public string? FullName { get; set; }  
        public string? Email { get; set; }   
    }
    public class UserFilterDtoValidator : AbstractValidator<UserFilterDto>
    {
        public UserFilterDtoValidator()
        {
            Include(new BaseFilterDtoValidator());

            RuleFor(x => x.FullName)
                .MaximumLength(20);
            RuleFor(x => x.Email)
                .MinimumLength(20);
        }
    }
}
