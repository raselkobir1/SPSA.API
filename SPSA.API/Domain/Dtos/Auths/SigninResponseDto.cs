namespace SPSA.API.Domain.Dtos.Auths
{
    public class SigninResponseDto
    {
        public string JWTToken { get; set; }
        public string RefreshToken { get; set; }
        public string Email { get; set; }
        public string FullName { get; set; }
        public DateTime JWTExpires { get; set; }
        public DateTime RefreshExpires { get; set; }
    }
}
