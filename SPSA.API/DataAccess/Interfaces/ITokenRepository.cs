using SPSA.API.Domain;
using SPSA.API.Domain.Dtos.Token;

namespace SPSA.API.DataAccess.Interfaces
{
    public interface ITokenRepository : IGenericRepository<UserToken>
    {
        //Task<UserToken> GetRefreshToken(RefreshTokenFilterDto refreshToken);
    }
}
