using SPSA.API.DataAccess.DataContext;
using SPSA.API.DataAccess.Interfaces;
using SPSA.API.Domain;
using SPSA.API.Domain.Dtos.Token;

namespace SPSA.API.DataAccess.Implementations
{
    public class TokenRepository : GenericRepository<UserToken>, ITokenRepository
    {
        public TokenRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
            
        }
        //public Task<UserToken> GetRefreshToken(RefreshTokenFilterDto refreshToken)
        //{
        //    throw new NotImplementedException();
        //}
    }
}
