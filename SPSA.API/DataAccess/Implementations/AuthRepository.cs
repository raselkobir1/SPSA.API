using SPSA.API.DataAccess.DataContext;
using SPSA.API.DataAccess.Interfaces;

namespace SPSA.API.DataAccess.Implementations
{
    public class AuthRepository : IAuthRepository
    {
        private readonly ApplicationDbContext _dbContext;
        public AuthRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }
    }
}
