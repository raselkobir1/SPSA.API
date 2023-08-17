using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using SPSA.API.DataAccess.UnitOfWorks;
using SPSA.API.Domain;
using SPSA.API.Domain.Dtos;
using SPSA.API.Domain.Dtos.Common;
using SPSA.API.Helper.CommonMethods;
using SPSA.API.Helper.Configurations;
using SPSA.API.Helper.Resources;
using SPSA.API.Manager.Intrerface;

namespace SPSA.API.Manager.Implementaion
{
    public class AuthManager : IAuthManager
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IPasswordHasher<User> _passwordHasher;
        private readonly JwtConfiguration _jwtConfiguration; 
        public AuthManager(IUnitOfWork unitOfWork, IPasswordHasher<User> passwordHasher, IOptionsSnapshot<JwtConfiguration> tokenAppsettingConfig)
        {
            _unitOfWork = unitOfWork;   
            _passwordHasher = passwordHasher;   
            _jwtConfiguration = tokenAppsettingConfig.Value;  
        }

        public async Task<ResponseModel> SignIn(SignInDto dto)
        {
            var user = await _unitOfWork.Users.GetWhere(x => x.Email == dto.Email);
            if(user != null && !user.IsActive)
                return CommonResponse.ValidationErrorResponse(CommonMessage.InactiveUser);

            if(user == null)
                return CommonResponse.ValidationErrorResponse(CommonMessage.InvalidEmailOrPassword);

            var result = _passwordHasher.VerifyHashedPassword(user, user.Password, dto.Password);
            if (result != PasswordVerificationResult.Success)
                return CommonResponse.ValidationErrorResponse(CommonMessage.InvalidEmailOrPassword);

            var JWTToken = CommonMethods.GenerateJWTTokensAsync(user, _jwtConfiguration); 
            var refreshToken = CommonMethods.GenerateRefreshToken();

            var token = new UserToken
            {
                JWTToken = JWTToken.Token,
                JWTExpires = JWTToken.Expire.AddHours(6),
                RefreshToken = refreshToken.Token,
                RefreshExpires = refreshToken.Expire.AddHours(6),
                UserId = user.Id,
                IsRevoked = false
            };

            await _unitOfWork.Tokens.Add(token);
            await _unitOfWork.SaveChange();

            var signinResponse = new SigninResponseDto() 
            {
                JWTToken = JWTToken.Token,
                JWTExpires = JWTToken.Expire.AddHours(6),
                RefreshToken = refreshToken.Token,
                RefreshExpires = refreshToken.Expire.AddHours(6),
                Email = user.Email,
                FullName = user.FullName
            };

            return CommonResponse.SuccessResponse("Signin successfull", signinResponse);
        }

        public async Task<ResponseModel> SignOut(SignOutDto dto)
        {
            var userToken = await _unitOfWork.Tokens.GetWhere(x => x.RefreshToken == dto.RefreshToken);
            if (userToken == null) 
                return CommonResponse.NotFoundResponse();

            userToken.IsRevoked = true;
            _unitOfWork.Tokens.Update(userToken);
            await _unitOfWork.SaveChange();

            return CommonResponse.SuccessResponse("SignOut succesfully");  
        }
    }
}
