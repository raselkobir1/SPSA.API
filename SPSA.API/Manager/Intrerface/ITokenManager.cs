using SPSA.API.Domain;
using SPSA.API.Domain.Dtos.Common;
using SPSA.API.Domain.Dtos.Token;

namespace SPSA.API.Manager.Intrerface
{
    public interface ITokenManager
    {
        Task<ResponseModel> GetNewJwtToken(RefreshTokenFilterDto refreshToken); 
        Task<ResponseModel> RevokeToken(RefreshTokenFilterDto refreshToken);  
    }
}
