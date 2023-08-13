using SPSA.API.DataAccess.DataContext;
using SPSA.API.DataAccess.Interfaces;
using SPSA.API.Domain;

namespace SPSA.API.DataAccess.Implementations
{
    public class UserRepository : GenericRepository<User>, IUserRepository
    {
        public UserRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}
