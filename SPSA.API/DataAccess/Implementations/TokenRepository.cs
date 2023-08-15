using SPSA.API.DataAccess.DataContext;
using SPSA.API.DataAccess.Interfaces;
using SPSA.API.Domain;

namespace SPSA.API.DataAccess.Implementations
{
    public class TokenRepository : GenericRepository<UserToken>, ITokenRepository
    {
        public TokenRepository(ApplicationDbContext dbContext) : base(dbContext)
        {

        }
    }
}
