using Microsoft.Extensions.Options;
using SPSA.API.DataAccess.UnitOfWorks;
using SPSA.API.Domain;
using SPSA.API.Domain.Dtos.Common;
using SPSA.API.Domain.Dtos.Token;
using SPSA.API.Helper.CommonMethods;
using SPSA.API.Helper.Configurations;
using SPSA.API.Helper.Resources;
using SPSA.API.Manager.Intrerface;

namespace SPSA.API.Manager.Implementaion
{
    public class TokenManager : ITokenManager
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly JwtConfiguration _jwtConfiguration;
        public TokenManager(IUnitOfWork unitOfWork, IOptionsSnapshot<JwtConfiguration> tokenAppsettingConfig)
        {
            _unitOfWork = unitOfWork;
            _jwtConfiguration = tokenAppsettingConfig.Value;
        }

        public async Task<ResponseModel> GetNewJwtToken(RefreshTokenFilterDto refreshToken) 
        {
            var token = await _unitOfWork.Tokens.GetWhere(x => x.RefreshToken == refreshToken.Token && !x.IsRevoked);
            if(token == null || token.RefreshExpires <= CommonMethods.GetCurrentTime())
                return CommonResponse.ValidationErrorResponse(CommonMessage.InvalidToken);

            var user = await _unitOfWork.Users.GetById(token.UserId);

            var JWTToken = CommonMethods.GenerateJWTTokensAsync(user, _jwtConfiguration);

            token.JWTToken = JWTToken.Token;
            token.JWTExpires = JWTToken.Expire.AddHours(6);

             _unitOfWork.Tokens.Update(token);
            await _unitOfWork.SaveChange();

            return CommonResponse.SuccessResponse(string.Empty, token);
        }

        public async Task<ResponseModel> RevokeToken(RefreshTokenFilterDto refreshToken)
        {
            var token = await _unitOfWork.Tokens.GetWhere(x => x.RefreshToken == refreshToken.Token && !x.IsRevoked);
            if (token == null || token.RefreshExpires <= CommonMethods.GetCurrentTime())
                return CommonResponse.ValidationErrorResponse(CommonMessage.InvalidToken);

            token.RefreshExpires = CommonMethods.GetCurrentTime();
            token.IsRevoked = true;

            _unitOfWork.Tokens.Update(token);
            await _unitOfWork.SaveChange();

            return CommonResponse.SuccessResponse("Revoke successfull",token);
        }
    }
}
