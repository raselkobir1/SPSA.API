using SPSA.API.Domain.Dtos.Auths;
using SPSA.API.Domain.Dtos.Common;

namespace SPSA.API.Manager.Intrerface
{
    public interface IAuthManager
    {
        Task<ResponseModel> SignIn(SignInDto dto);  
        Task<ResponseModel> SignOut(SignOutDto dto);  

        //Task<ResponseModel> GetNewJwtAccessTokenByRefreshTokenAsync(AccessTokenFromRefreshTokenDto dto);
    }
}
