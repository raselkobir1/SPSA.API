using Microsoft.IdentityModel.Tokens;
using SPSA.API.Domain;
using SPSA.API.Domain.Dtos.Token;
using SPSA.API.Helper.Configurations;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;

namespace SPSA.API.Helper.CommonMethods
{
    public static class CommonMethods
    {
        public static string GetRandomString(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789"; // Characters to choose from
            var random = new Random();
            string randomString = new string(Enumerable.Repeat(chars, length)
                .Select(s => s[random.Next(s.Length)]).ToArray()); // Generate the random string

            return randomString;
        }
        public static DateTime GetCurrentTime()
        {
            return DateTime.UtcNow.Add(TimeSpan.FromHours(6));
            //var x = DateTimeOffset.UtcNow;
            ///return DateTimeOffset.UtcNow.DateTime;
        }
        public static List<string> ConvertFluentErrorMessages(List<FluentValidation.Results.ValidationFailure> errors)
        {
            List<string> errorsMessages = new List<string>();
            foreach (var failure in errors)
            {
                errorsMessages.Add(failure.ErrorMessage);
            }
            return errorsMessages;
        }

        public static bool ValidateUsingRegex(string emailAddress)
        {
            var pattern = @"^[a-zA-Z0-9](?!.*[._-]{2})[a-zA-Z0-9._-]*[a-zA-Z0-9]@[a-zA-Z0-9-]+(\.[a-zA-Z0-9-]+)*\.[a-zA-Z]{2,6}$";

            var regex = new Regex(pattern);
            return regex.IsMatch(emailAddress);
        }

        public static TokenDto GenerateJWTTokensAsync(User user, JwtConfiguration jwtAppsettingConfig)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(jwtAppsettingConfig.SigningKey);
            var expiryTime = DateTime.UtcNow.AddMinutes(Convert.ToDouble(jwtAppsettingConfig.JWTTokenExpirationMinutes));
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim(ClaimTypes.Sid, user.Id.ToString()),
                    new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                    new Claim(ClaimTypes.Email, user.Email),
                    new Claim(ClaimTypes.Name, user.Email),
                    new Claim("RoleId", user.RoleId.ToString()),
                    new Claim("BranchId", user.BranchId.ToString()),
                    new Claim("IsSuperAdmin", user.IsSuperAdmin.ToString()),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                }),

                Expires = expiryTime,
                Issuer = jwtAppsettingConfig.Issuer,
                Audience = jwtAppsettingConfig.Audience,
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature),

            };
            var accessToken = tokenHandler.WriteToken(tokenHandler.CreateToken(tokenDescriptor));
            return new TokenDto
            {
                Token = accessToken,
                Expire = (DateTime)tokenDescriptor.Expires,
            };
        }
        public static TokenDto GenerateRefreshToken()
        {
            string refreshTokenExpireTime = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build().GetSection("JwtConfiguration")["RefreshTokenExpirationMinutes"] ?? "2160";
            var expiryTime = DateTime.UtcNow.AddMinutes(Convert.ToDouble(refreshTokenExpireTime));
            var randomNumber = new byte[64];
            using var rng = RandomNumberGenerator.Create();
            rng.GetBytes(randomNumber);
            return new TokenDto
            {
                Token = Convert.ToBase64String(randomNumber),
                Expire = expiryTime
            };
        }
    }
}
