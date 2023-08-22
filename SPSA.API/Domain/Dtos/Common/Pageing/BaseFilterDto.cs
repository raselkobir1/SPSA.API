using FluentValidation;

namespace SPSA.API.Domain.Dtos.Common.Pageing
{
    public class BaseFilterDto
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        internal int Skip
        {
            get
            {
                return (this.PageNumber - 1) * this.PageSize;
            }
        }
    }

    public class BaseFilterDtoValidator : AbstractValidator<BaseFilterDto>
    {
        public BaseFilterDtoValidator()
        {
            RuleFor(obj => obj.PageSize).NotEmpty().GreaterThan(0);
            RuleFor(obj => obj.PageNumber).NotEmpty().GreaterThan(0);
        }
    }
}
