using System.ComponentModel.DataAnnotations;

namespace SPSA.API.Domain.Dtos.Auths
{
    public class SignInDto
    {
        [Required]
        public string? Email { get; set; }
        [Required]
        public string? Password { get; set; }
    }
}
