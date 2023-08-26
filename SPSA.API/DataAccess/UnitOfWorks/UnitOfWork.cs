using SPSA.API.DataAccess.DataContext;
using SPSA.API.DataAccess.Implementations;
using SPSA.API.DataAccess.Interfaces;
using System.Security.Claims;

namespace SPSA.API.DataAccess.UnitOfWorks
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public void BeginTransaction()
        {
            _dbContext.Database.BeginTransaction();
        }

        public void CommitTransaction()
        {
            _dbContext.Database.CommitTransaction();
        }

        public void RollBackTransaction()
        {
            _dbContext.Database.RollbackTransaction();
        }

        public long GetLoggedInUserId()
        {
            var userId = _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrWhiteSpace(userId))
                throw new UnauthorizedAccessException();
            return Convert.ToInt64(userId);
        }

        public (bool, string) HasDependency(string table, string id)
        {
            throw new NotImplementedException();
        }

        public Task<int> SaveChange()
        {
            return _dbContext.SaveChangesAsync(); 
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);  
        }

        protected virtual void Dispose(bool dispong)
        {
            if (dispong)
            {
                _dbContext.Dispose();
            }    
        }

        public UnitOfWork(ApplicationDbContext dbContext, IHttpContextAccessor httpContextAccessor)
        {
            _dbContext = dbContext;
            _httpContextAccessor = httpContextAccessor;

            Users = new UserRepository(dbContext);
            Tokens = new TokenRepository(dbContext);
            Roles = new RoleRepository(dbContext); 
        }

        public IUserRepository Users { get; private set; }

        public ITokenRepository Tokens { get; private set; }

        public IRoleRepository Roles { get; private set; }  
    }
}
