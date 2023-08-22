using System.ComponentModel.DataAnnotations;

namespace SPSA.API.Domain.Dtos.Auths
{
    public class SignOutDto
    {
        [Required(AllowEmptyStrings = false)]
        public string RefreshToken { get; set; }
    }
}
