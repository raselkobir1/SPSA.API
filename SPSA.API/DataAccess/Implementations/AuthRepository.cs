using SPSA.API.DataAccess.DataContext;
using SPSA.API.DataAccess.Interfaces;
using SPSA.API.Domain;

namespace SPSA.API.DataAccess.Implementations
{
    public class AuthRepository : GenericRepository<User>, IAuthRepository
    {
        public AuthRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }
    }
}
