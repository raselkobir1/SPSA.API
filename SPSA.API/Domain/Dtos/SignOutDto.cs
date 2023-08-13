using System.ComponentModel.DataAnnotations;

namespace SPSA.API.Domain.Dtos
{
    public class SignOutDto
    {
        [Required(AllowEmptyStrings = false)]
        public string RefreshToken { get; set; }
    }
}
